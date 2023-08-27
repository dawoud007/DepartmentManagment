using DepartManagment.Application.Commands.Tasks;
using DepartManagment.Application.Interfaces;
using DepartManagment.Application.Models.EmployeeTask;
using DepartManagment.Domain.Entities;
using DepartManagment.Domain.Entities.ApplicationUser;
using DepartManagment.Domain.Entities.ApplicationUser.Errors;
using ErrorOr;
using Mapster;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DepartManagment.Application.CommandHandlers.Tasks
{
    public class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand, ErrorOr<TaskViewModel>>
    {
        private readonly IEmployeeTaskRepository _taskRepository;
        private readonly IEmployeeRepository _employeerRepository;

        public CreateTaskCommandHandler(IEmployeeTaskRepository taskRepository, IEmployeeRepository employeerRepository)
        {
            _taskRepository = taskRepository;
            _employeerRepository = employeerRepository;
        }

        public async Task<ErrorOr<TaskViewModel>> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
        {

            Employee employee = await _employeerRepository.GetByIdAsync(request.EmployeeId);
            if (employee == null)
            {
                return DomainErrors.Employees.NotFound;
            }

            var task = new EmployeeTask
            {
                Title = request.Title,
                Description = request.Description,
                EmployeeId = request.EmployeeId
            };


            await _taskRepository.AddAsync(task);
            employee.Tasks!.Add(task);
            await _employeerRepository.SaveAsync();
            await _taskRepository.SaveAsync();

            var taskViewModel = task.Adapt<TaskViewModel>();

            return taskViewModel;
        }
    }
}
