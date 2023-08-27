using DepartManagment.Application.Models.EmployeeTask;
using ErrorOr;
using MediatR;
using System;

namespace DepartManagment.Application.Commands.Tasks
{
    public record CreateTaskCommand(
        string Title,
        string Description,
        string EmployeeId
    ) : IRequest<ErrorOr<TaskViewModel>>;
}
