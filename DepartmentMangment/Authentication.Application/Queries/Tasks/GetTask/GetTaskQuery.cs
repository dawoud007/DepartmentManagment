using DepartManagment.Application.CommandInterfaces;
using DepartManagment.Application.Models.EmployeeTask;
using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepartManagment.Application.Queries.Tasks.GetTask
{
    public record GetTaskByIdQuery(Guid TaskId) : IQuery<ErrorOr<TaskViewModel>>;
}
