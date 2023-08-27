using DepartManagment.Application.CommandInterfaces;
using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepartManagment.Application.Commands.Departments.DeleteDepartments
{
    public record DeleteDepartmentCommand(
        Guid DepartmentId
    ) : ICommand<ErrorOr<Unit>>;
}
