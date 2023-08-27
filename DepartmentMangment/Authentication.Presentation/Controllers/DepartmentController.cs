using BusinessLogic.Presentation;
using DepartManagment.Application.Commands.Departments;
using DepartManagment.Application.Commands.Departments.DeleteDepartments;
using DepartManagment.Application.Commands.Departments.UpdateDepartment;
using DepartManagment.Application.Models.Department;
using DepartManagment.Application.Models.Employee;
using DepartManagment.Application.Queries.Departments.GetDepartment;
using DepartManagment.Application.Queries.Departments.GetDepartments;
using DepartManagment.Domain.Entities.ApplicationUser;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;




namespace DepartManagment.Presentation.Controllers;

[ApiController]
[Route("api/v1/[controller]/[action]")]

public class DepartmentController : BaseController
{
    private readonly ISender _sender;
    private readonly ILogger<DepartmentController> _logger;

    public DepartmentController(ISender sender, ILogger<DepartmentController> logger)
    {
        _sender = sender;
        _logger = logger;
    }


    [HttpPost]
    /*    [HasPermission(Permission.CanDeployDepartments)]*/
    public async Task<IActionResult> CreareDepartment(DepartmentCreateUpdateModel departmentCreateUpdateModel)
    {
      /*  string username = User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)!.Value;*/

        var DeployDepartmentCommand = new DeployDepartmentCommand(departmentCreateUpdateModel.Name,departmentCreateUpdateModel.ManagerId);
        ErrorOr<DepartmentViewModel> results = await _sender.Send(DeployDepartmentCommand);
        return results.Match(
            Department => Ok(Department),
            errors => Problem(errors)
        );
    }

    [HttpPost]
/*    [Authorize(AuthenticationSchemes = "Bearer")]*/
    public async Task<IActionResult> AddEmployeeToDepartment(Guid departmentId, string employeeId)
    {
        var command = new AddEmployeeToDepartmentCommand(departmentId, employeeId);
        ErrorOr<EmployeeViewModel> result = await _sender.Send(command);
        return result.Match(
                   Department => Ok(Department),
                   errors => Problem(errors)
               );
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateDepartment(Guid id, [FromBody] UpdateDepartmentModel departmentUpdateModel)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var command = new UpdateDepartmentCommand(id, departmentUpdateModel.Name, departmentUpdateModel.ManagerId);
        var results = await _sender.Send(command);

        return results.Match(
           Department => Ok(Department),
           errors => Problem(errors)
       );
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetDepartmentById(Guid id)
    {
        var query = new GetDepartmentByIdQuery(id);
        ErrorOr<DepartmentViewModel> result = await _sender.Send(query);

        return result.Match(
            department => Ok(department),
            errors => Problem(errors)
        );
    }





    [HttpGet]
    public async Task<IActionResult> GetDepartments(int pageNumber, int pageSize)
    {
        var query = new GetAllDepartmentsQuery(pageNumber, pageSize);
        ErrorOr<List<DepartmentsViewModel>> result = await _sender.Send(query);

        return result.Match(
            departments => Ok(departments),
            errors => Problem(errors)
        );
    }




    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDepartment(Guid id)
    {
        var command = new DeleteDepartmentCommand(id);
        var results = await _sender.Send(command);

        return results.Match(
            Department => Ok(Department),
            errors => Problem(errors)
        );
    }



}

