using DepartManagment.Application.CommandInterfaces;
using DepartManagment.Application.Models.EmployeeTask;
using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepartManagment.Application.Queries.Tasks.GetTasks
{
    public record GetAllTasksQuery : IQuery<ErrorOr<List<TaskViewModel>>>;
}
