using DepartManagment.Application.CommandInterfaces;
using DepartManagment.Application.Commands.Tasks.DeleteTasks;
using DepartManagment.Application.Interfaces;
using DepartManagment.Domain.Entities.ApplicationUser.Errors;
using ErrorOr;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DepartManagment.Application.Commands.Tasks.DeleteTask
{
    public class DeleteTaskCommandHandler : IHandler<DeleteTaskCommand, ErrorOr<Unit>>
    {
        private readonly IEmployeeTaskRepository _taskRepository;

        public DeleteTaskCommandHandler(IEmployeeTaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public IEmployeeTaskRepository TaskRepository => _taskRepository;

        public async Task<ErrorOr<Unit>> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
        {
            var task = await TaskRepository.GetByIdAsync(request.TaskId);
            if (task == null)
            {
                return DomainErrors.Tasks.NotFound;
            }

            await TaskRepository.RemoveAsync(t=>t.Id==request.TaskId);
            await TaskRepository.SaveAsync();

            return Unit.Value;
        }
    }
}
