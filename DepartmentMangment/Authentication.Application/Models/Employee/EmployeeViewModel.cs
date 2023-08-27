using DepartManagment.Application.Models.Department;
using DepartManagment.Application.Models.EmployeeTask;
using DepartManagment.Domain.Entities.ApplicationUser.Enums;

namespace DepartManagment.Application.Models.Employee;
public record EmployeeViewModel(
    string Id,
    string Name,
    string Email,
    string UserName,
    Gender Gender,
    Role Role,
   string DepartmentName,
    List<TaskViewModel> tasks

    );
