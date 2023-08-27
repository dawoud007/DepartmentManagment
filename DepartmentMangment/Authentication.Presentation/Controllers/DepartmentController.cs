using BusinessLogic.Presentation;
using DepartManagment.Application.Commands.Departments;
using DepartManagment.Application.Commands.Departments.DeleteDepartments;
using DepartManagment.Application.Commands.Departments.UpdateDepartment;
using DepartManagment.Application.Models.Department;
using DepartManagment.Application.Models.Employee;
using DepartManagment.Application.Queries.Departments.GetDepartment;
using DepartManagment.Application.Queries.Departments.GetDepartments;
using DepartManagment.Domain.Entities.ApplicationUser;
using DepartManagment.Domain.Entities.ApplicationUser.Enums;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
[Authorize(AuthenticationSchemes = "Bearer")]
public class DepartmentController : BaseController
{
    private readonly ISender _sender;
    private readonly ILogger<DepartmentController> _logger;
    private readonly UserManager<Employee> _userManager;
    public DepartmentController(ISender sender, ILogger<DepartmentController> logger, UserManager<Employee> userManager)
    {
        _sender = sender;
        _logger = logger;
        _userManager = userManager;
    }


    [HttpPost]
    /*    [HasPermission(Permission.CanDeployDepartments)]*/
    public async Task<IActionResult> CreareDepartment(DepartmentCreateUpdateModel departmentCreateUpdateModel)
    {
        string Admin = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        Employee AdminEmployee = await _userManager.FindByNameAsync(Admin);
        if (AdminEmployee.Role != Role.Admin)
        {
            var error = new Results();
            error.AddErrorMessages("You are not authorized");
            return Unauthorized(error);
        }


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
        string Admin = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        Employee AdminEmployee = await _userManager.FindByNameAsync(Admin);
        if (AdminEmployee.Role != Role.Admin)
        {
            var error = new Results();
            error.AddErrorMessages("You are not authorized");
            return Unauthorized(error);
        }

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
        string Admin = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        Employee AdminEmployee = await _userManager.FindByNameAsync(Admin);
        if (AdminEmployee.Role != Role.Admin)
        {
            var error = new Results();
            error.AddErrorMessages("You are not authorized");
            return Unauthorized(error);
        }

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
        string Admin = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        Employee AdminEmployee = await _userManager.FindByNameAsync(Admin);
        if (AdminEmployee.Role != Role.Admin)
        {
            var error = new Results();
            error.AddErrorMessages("You are not authorized");
            return Unauthorized(error);
        }

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
        string Admin = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        Employee AdminEmployee = await _userManager.FindByNameAsync(Admin);
        if (AdminEmployee.Role != Role.Admin)
        {
            var error = new Results();
            error.AddErrorMessages("You are not authorized");
            return Unauthorized(error);
        }

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
        string Admin = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        Employee AdminEmployee = await _userManager.FindByNameAsync(Admin);
        if (AdminEmployee.Role != Role.Admin)
        {
            var error = new Results();
            error.AddErrorMessages("You are not authorized");
            return Unauthorized(error);
        }

        var command = new DeleteDepartmentCommand(id);
        var results = await _sender.Send(command);

        return results.Match(
            Department => Ok(Department),
            errors => Problem(errors)
        );
    }



}

