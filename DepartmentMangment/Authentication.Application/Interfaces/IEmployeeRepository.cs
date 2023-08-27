using DepartManagment.Domain.Entities;
using DepartManagment.Domain.Entities.ApplicationUser;
using Microsoft.AspNetCore.Identity;

namespace DepartManagment.Application.Interfaces;
public interface IEmployeeRepository : IBaseRepoForIdentity<Employee>
{

}