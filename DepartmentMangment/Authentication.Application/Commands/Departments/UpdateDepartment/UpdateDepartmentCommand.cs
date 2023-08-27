using DepartManagment.Application.CommandInterfaces;
using DepartManagment.Application.Models.Department;
using ErrorOr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepartManagment.Application.Commands.Departments.UpdateDepartment
{
    public record UpdateDepartmentCommand(
        Guid DepartmentId,
        string NewName,
        string NewManagerId
    ) : ICommand<ErrorOr<UpdateDepartmentModel>>;
}
