using DepartManagment.Application.CommandInterfaces;
using DepartManagment.Application.Interfaces;
using DepartManagment.Application.Models.EmployeeTask;
using ErrorOr;
using Mapster;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepartManagment.Application.Queries.Tasks.GetTasks
{
    public class GetAllTasksQueryHandler : IHandler<GetAllTasksQuery, ErrorOr<List<TaskViewModel>>>
    {
        private readonly IEmployeeTaskRepository _taskRepository;
        private readonly IEmployeeRepository _employeerRepository;

        public GetAllTasksQueryHandler(IEmployeeTaskRepository taskRepository, IEmployeeRepository employeerRepository)
        {
            _taskRepository = taskRepository;
            _employeerRepository= employeerRepository;
          
        }

        public async Task<ErrorOr<List<TaskViewModel>>> Handle(GetAllTasksQuery request, CancellationToken cancellationToken )
        {
            var tasks = (await _taskRepository.GetAsync(t=>t.IsCompleted==false,null, "Employee")).ToList();

        
            var taskViewModels = tasks
                .Select(task => task.Adapt<TaskViewModel>())
                .ToList();

            return taskViewModels;
        }
    }
}