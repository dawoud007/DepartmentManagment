using DepartManagment.Application.CommandInterfaces;
using DepartManagment.Application.Interfaces;
using DepartManagment.Application.Models.EmployeeTask;
using DepartManagment.Domain.Entities;
using DepartManagment.Domain.Entities.ApplicationUser.Errors;
using ErrorOr;
using Mapster;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DepartManagment.Application.Commands.Tasks.UpdateTask
{
    public class UpdateTaskCommandHandler : IHandler<UpdateTaskCommand, ErrorOr<TaskCreateUpdateModel>>
    {
        private readonly IEmployeeTaskRepository _taskRepository;

        public UpdateTaskCommandHandler(IEmployeeTaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<ErrorOr<TaskCreateUpdateModel>> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
        {
            var existingTask = await _taskRepository.GetByIdAsync(request.TaskId);
            if (existingTask == null)
            {
                return DomainErrors.Tasks.NotFound;
            }

            // Update task properties
            existingTask.Title = request.NewTitle;
            existingTask.Description = request.NewDescription;
            existingTask.IsCompleted = request.NewIsCompleted;

            await _taskRepository.UpdateAsync(existingTask);
            await _taskRepository.SaveAsync();

            // Map entity to view model
            var updatedTaskViewModel = existingTask.Adapt<TaskCreateUpdateModel>();

            return updatedTaskViewModel;
        }
    }
}
