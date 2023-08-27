using DepartManagment.Domain.Entities.ApplicationUser.Enums;

namespace DepartManagment.Application.Models.Employee;
public record EmployeeCreateUpdateModel(
    string Name,
    string Email,
    string Password,
    string UserName,
    Gender Gender,
    Role Role
    );