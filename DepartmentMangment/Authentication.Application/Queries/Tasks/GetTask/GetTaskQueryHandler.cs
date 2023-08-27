using DepartManagment.Application.Interfaces;
using DepartManagment.Application.Models.EmployeeTask;
using DepartManagment.Domain.Entities.ApplicationUser.Errors;
using ErrorOr;
using Mapster;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepartManagment.Application.Queries.Tasks.GetTask
{
    public class GetTaskByIdQueryHandler : IRequestHandler<GetTaskByIdQuery, ErrorOr<TaskViewModel>>
    {
        private readonly IEmployeeTaskRepository _taskRepository;

        public GetTaskByIdQueryHandler(IEmployeeTaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<ErrorOr<TaskViewModel>> Handle(GetTaskByIdQuery request, CancellationToken cancellationToken)
        {
            var task = await _taskRepository.GetByIdAsync(request.TaskId);

            if (task == null)
            {
                return DomainErrors.Tasks.NotFound;
            }

            var taskViewModel = task.Adapt<TaskViewModel>();

            return taskViewModel;
        }
    }
}