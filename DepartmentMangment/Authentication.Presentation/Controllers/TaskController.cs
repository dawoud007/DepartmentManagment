using BusinessLogic.Presentation;
using DepartManagment.Application.Commands.Tasks;
using DepartManagment.Application.Commands.Tasks.DeleteTasks;
using DepartManagment.Application.Commands.Tasks.UpdateTask;
using DepartManagment.Application.Models.Employee;
using DepartManagment.Application.Models.EmployeeTask;
using DepartManagment.Application.Queries.Tasks.GetTask;
using DepartManagment.Application.Queries.Tasks.GetTasks;
using DepartManagment.Domain.Entities.ApplicationUser;
using DepartManagment.Domain.Entities.ApplicationUser.Enums;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DepartManagment.Presentation.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class TaskController : BaseController
    {
        private readonly ISender _sender;
        private readonly ILogger<TaskController> _logger;
        private readonly UserManager<Employee> _userManager;
        public TaskController(ISender sender, ILogger<TaskController> logger, UserManager<Employee> userManager)
        {
            _sender = sender;
            _logger = logger;
            _userManager = userManager;
        }

        [HttpPost]
    /*    [Authorize(AuthenticationSchemes = "Bearer")]*/
        public async Task<IActionResult> CreateTask(TaskCreateUpdateModel taskCreateUpdateModel)
        {
            string Admin = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            Employee AdminEmployee = await _userManager.FindByNameAsync(Admin);
            if (AdminEmployee.Role == Role.Admin || AdminEmployee.Role == Role.Manager)
            {
                var command = new CreateTaskCommand(taskCreateUpdateModel.Title, taskCreateUpdateModel.Description, taskCreateUpdateModel.EmployeeId);
                var result = await _sender.Send(command);

                return result.Match(
             hub => Ok(hub),
             errors => Problem(errors)
         );
            }
            var error = new Results();
            error.AddErrorMessages("You are not authorized");
            return Unauthorized(error);

        }

        [HttpPut("{id}")]
    /*    [Authorize(AuthenticationSchemes = "Bearer")]*/
        public async Task<IActionResult> UpdateTask(Guid id, [FromBody] TaskCreateUpdateModel taskUpdateModel)
        {
            string Admin = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            Employee AdminEmployee = await _userManager.FindByNameAsync(Admin);
            if (AdminEmployee.Role == Role.Admin || AdminEmployee.Role == Role.Manager)
            {
               

                var updateTaskCommand = new UpdateTaskCommand(
               id,
               taskUpdateModel.Title,
               taskUpdateModel.Description,
               taskUpdateModel.IsCompleted
           );

                ErrorOr<TaskCreateUpdateModel> result = await _sender.Send(updateTaskCommand);

                return result.Match(
                    task => Ok(task),
                    errors => Problem(errors)
                );
            }
            var error = new Results();
            error.AddErrorMessages("You are not authorized");
            return Unauthorized(error);

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(Guid id)
        {
            string Admin = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            Employee AdminEmployee = await _userManager.FindByNameAsync(Admin);
            if (AdminEmployee.Role == Role.Admin || AdminEmployee.Role == Role.Manager)
            {
               



                var deleteTaskCommand = new DeleteTaskCommand(id);

                ErrorOr<Unit> result = await _sender.Send(deleteTaskCommand);

                return result.Match(
                    _ => NoContent(),
                    errors => Problem(errors)
                );
            }
            var error = new Results();
            error.AddErrorMessages("You are not authorized");
            return Unauthorized(error);

        }


        [HttpGet]
        public async Task<IActionResult> GetAllTasks()
        {
            string Admin = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            Employee AdminEmployee = await _userManager.FindByNameAsync(Admin);
            if (AdminEmployee.Role == Role.Admin || AdminEmployee.Role == Role.Manager)
            {
              
                var query = new GetAllTasksQuery();
                ErrorOr<List<TaskViewModel>> result = await _sender.Send(query);

                return result.Match(
                    tasks => Ok(tasks),
                    errors => Problem(errors)
                );
            }
            var error = new Results();
            error.AddErrorMessages("You are not authorized");
            return Unauthorized(error);


        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTaskById(Guid id)
        {
            string Admin = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            Employee AdminEmployee = await _userManager.FindByNameAsync(Admin);
            if (AdminEmployee.Role == Role.Admin || AdminEmployee.Role == Role.Manager)
            {
               

                var query = new GetTaskByIdQuery(id);
                ErrorOr<TaskViewModel> result = await _sender.Send(query);

                return result.Match(
                    task => Ok(task),
                    errors => Problem(errors)
                );
            }
            var error = new Results();
            error.AddErrorMessages("You are not authorized");
            return Unauthorized(error);

        }
    }
}

