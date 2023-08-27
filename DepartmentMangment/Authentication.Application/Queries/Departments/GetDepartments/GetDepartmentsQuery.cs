using DepartManagment.Application.CommandInterfaces;
using DepartManagment.Application.Models.Department;
using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepartManagment.Application.Queries.Departments.GetDepartments
{
    public record GetAllDepartmentsQuery(int PageNumber, int PageSize) : IQuery<ErrorOr<List<DepartmentsViewModel>>>;
}
