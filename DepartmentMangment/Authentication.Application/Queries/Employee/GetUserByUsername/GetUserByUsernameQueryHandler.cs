using DepartManagment.Application.CommandInterfaces;
using DepartManagment.Application.Interfaces;
using DepartManagment.Application.Models;
using DepartManagment.Application.Models.Employee;
using DepartManagment.Domain.Entities.ApplicationUser;
using ErrorOr;
using Mapster;
using Microsoft.AspNetCore.Identity;
using System.Text.Json.Serialization;
using System.Text.Json;
using DepartManagment.Application.Models.Department;
using DepartManagment.Application.Models.EmployeeTask;
using DepartManagment.Domain.Entities.ApplicationUser.Enums;
using System.Xml.Linq;

namespace DepartManagment.Application.Queries.GetUserByUsername;
public class GetUserByUsernameQueryHandler : IHandler<GetUserByUsername, ErrorOr<EmployeeViewModel>>
{
    private readonly UserManager<Employee> _userManager;
    private readonly IEmployeeRepository _employeeRepository;

    public GetUserByUsernameQueryHandler(UserManager<Employee> userManager, IEmployeeRepository employeeRepository)
    {
        _userManager = userManager;
        _employeeRepository = employeeRepository;
    }

    public async Task<ErrorOr<EmployeeViewModel>> Handle(GetUserByUsername request, CancellationToken cancellationToken)
    {
        string DepartmentValue = "Leqaa";
        var user = await _userManager.FindByNameAsync(request.UserName);
        var employee = (await _employeeRepository.Get(e => e.UserName == request.UserName, null, "Tasks,Department")).FirstOrDefault()!;
        var tasks = employee.Tasks!.Adapt<List<TaskViewModel>>();
        var department = employee.Department.Adapt<DepartmentViewModel>();
        if (department != null)
        {
           DepartmentValue = department.Name;
        }
     
        var result = new EmployeeViewModel(
       employee.Id,
       employee.Name,
       employee.Email,
       employee.UserName, 
       employee.Gender,
       employee.Role,
       DepartmentValue,
       tasks
   );


        return result;
    }
}
