using BusinessLogic.Presentation;
using DepartManagment.Application.Commands.Tasks;
using DepartManagment.Application.Commands.Tasks.DeleteTasks;
using DepartManagment.Application.Commands.Tasks.UpdateTask;
using DepartManagment.Application.Models.Employee;
using DepartManagment.Application.Models.EmployeeTask;
using DepartManagment.Application.Queries.Tasks.GetTask;
using DepartManagment.Application.Queries.Tasks.GetTasks;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading.Tasks;

namespace DepartManagment.Presentation.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class TaskController : BaseController
    {
        private readonly ISender _sender;
        private readonly ILogger<TaskController> _logger;

        public TaskController(ISender sender, ILogger<TaskController> logger)
        {
            _sender = sender;
            _logger = logger;
        }

        [HttpPost]
    /*    [Authorize(AuthenticationSchemes = "Bearer")]*/
        public async Task<IActionResult> CreateTask(TaskCreateUpdateModel taskCreateUpdateModel)
        {
            var command = new CreateTaskCommand(taskCreateUpdateModel.Title, taskCreateUpdateModel.Description, taskCreateUpdateModel.EmployeeId);
            var result = await _sender.Send(command);

            return result.Match(
         hub => Ok(hub),
         errors => Problem(errors)
     );
        }

        [HttpPut("{id}")]
    /*    [Authorize(AuthenticationSchemes = "Bearer")]*/
        public async Task<IActionResult> UpdateTask(Guid id, [FromBody] TaskCreateUpdateModel taskUpdateModel)
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(Guid id)
        {
            var deleteTaskCommand = new DeleteTaskCommand(id);

            ErrorOr<Unit> result = await _sender.Send(deleteTaskCommand);

            return result.Match(
                _ => NoContent(),
                errors => Problem(errors)
            );
        }


        [HttpGet]
        public async Task<IActionResult> GetAllTasks()
        {
            var query = new GetAllTasksQuery();
            ErrorOr<List<TaskViewModel>> result = await _sender.Send(query);

            return result.Match(
                tasks => Ok(tasks),
                errors => Problem(errors)
            );
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTaskById(Guid id)
        {
            var query = new GetTaskByIdQuery(id);
            ErrorOr<TaskViewModel> result = await _sender.Send(query);

            return result.Match(
                task => Ok(task),
                errors => Problem(errors)
            );
        }
    }
}

