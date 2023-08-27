using DepartManagment.Application.CommandInterfaces;
using DepartManagment.Application.Models.Department;
using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepartManagment.Application.Queries.Departments.GetDepartment
{
    public record GetDepartmentByIdQuery(Guid DepartmentId) : IQuery<ErrorOr<DepartmentViewModel>>;
}
