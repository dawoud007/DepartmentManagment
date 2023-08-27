using DepartManagment.Application.CommandInterfaces;
using DepartManagment.Application.Models.Employee;
using ErrorOr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepartManagment.Application.Commands.Employees.DeleteEmployee
{
 
    public record DeleteEmployeeCommand(
  string EmployeeId

) : ICommand<ErrorOr<Results>>;
}
