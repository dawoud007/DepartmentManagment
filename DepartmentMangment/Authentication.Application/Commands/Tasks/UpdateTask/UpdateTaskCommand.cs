using DepartManagment.Application.CommandInterfaces;
using DepartManagment.Application.Models.EmployeeTask;
using ErrorOr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepartManagment.Application.Commands.Tasks.UpdateTask
{
    public record UpdateTaskCommand(
       Guid TaskId,
       string NewTitle,
       string NewDescription,
       bool NewIsCompleted
   ) : ICommand<ErrorOr<TaskCreateUpdateModel>>;
}
