
using BusinessLogic.Presentation;
using DepartManagment.Application.Commands.ConfirmEmail;
using DepartManagment.Application.Commands.ConfirmResetPasswordToken;
using DepartManagment.Application.Commands.Employees.DeleteEmployee;
using DepartManagment.Application.Commands.Employees.UpdateEmployee;
using DepartManagment.Application.Commands.RegisterUser;
using DepartManagment.Application.Interfaces;
using DepartManagment.Application.Models;
using DepartManagment.Application.Models.Employee;
using DepartManagment.Application.Queries.GetUserByUsername;
using DepartManagment.Application.Queries.Login;
using DepartManagment.Application.Queries.SendEmailConfirmation;
using DepartManagment.Application.Queries.SendEmailResetPassword;
using DepartManagment.Domain.Entities.ApplicationUser;
using DepartManagment.Domain.Entities.ApplicationUser.Enums;
using ErrorOr;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using System.Security.Claims;

namespace DepartManagment.Presentation.Controllers;
[ApiController]
[Route("api/v1/[controller]/[action]")]
[Authorize(AuthenticationSchemes = "Bearer")]
public class DepartManagmentController : BaseController
{
    private readonly ISender _sender;
    private readonly UserManager<Employee> _userManager;
    private readonly IEmployeeRepository _employeeRepository;
    public DepartManagmentController(ISender sender, UserManager<Employee> userManager, IEmployeeRepository employeeRepository)
    {
        _sender = sender;
        _userManager = userManager;
        _employeeRepository = employeeRepository;
    }
    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Register(EmployeeCreateUpdateModel userWriteModel)
    {
       /* string Admin = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        Employee AdminEmployee = await _userManager.FindByNameAsync(Admin);
        if (AdminEmployee.Role != Role.Admin)
        {
            var error = new Results();
            error.AddErrorMessages("You are not authorized");
            return Unauthorized(error);
        }*/


        var registerUserCommand = userWriteModel.Adapt<RegisterUserCommand>();
        registerUserCommand = registerUserCommand with
        {
            ConfirmationLink = Url.Action(nameof(ConfirmEmail))
        };
       ErrorOr<Results> results = await _sender.Send(registerUserCommand);
        return results.Match(
         hub => Ok(hub),
         errors => Problem(errors)
     );
    }
    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Login(LoginCredentials credentials)
    {
        var loginQuery = credentials.Adapt<LoginQuery>();
        ErrorOr<Results> results = await _sender.Send(loginQuery);


        return results.Match(
         hub => Ok(hub),
         errors => Problem(errors)
     );
    }
    [HttpGet]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public async Task<IActionResult> GetEmployeeByName([FromQuery] string Name)
    {
     
        string Admin = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        Employee AdminEmployee=await _userManager.FindByNameAsync(Admin);
        if (AdminEmployee.Role !=Role.Admin)
        {
            var error = new Results();
            error.AddErrorMessages("You are not authorized");
            return Unauthorized(error);
        }
    


        var getUserByUsernameQuery = new GetUserByUsername(Name);
       var results = await _sender.Send(getUserByUsernameQuery);
        return results.Match(
          hub => Ok(hub),
          errors => Problem(errors)
      );
    }


    [HttpPut("{employeeId}")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public async Task<IActionResult> UpdateEmployee(string employeeId, EmployeeCreateUpdateModel employeeUpdateModel)
    {
        string adminUsername = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        Employee adminEmployee = await _userManager.FindByNameAsync(adminUsername);

        if (adminEmployee.Role != Role.Admin)
        {
            var error = new Results();
            error.AddErrorMessages("You are not authorized");
            return Unauthorized(error);
        }

        var updateEmployeeCommand = new UpdateEmployeeCommand
        (
         employeeId,
       employeeUpdateModel
        );

        ErrorOr<Results> results = await _sender.Send(updateEmployeeCommand);

        return results.Match(
            success => Ok(success),
            errors => Problem(errors)
        );
    }



    [HttpDelete("{employeeId}")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public async Task<IActionResult> DeleteEmployee(string employeeId)
    {
        string adminUsername = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        Employee adminEmployee = await _userManager.FindByNameAsync(adminUsername);

        if (adminEmployee.Role != Role.Admin)
        {
            var error = new Results();
            error.AddErrorMessages("You are not authorized");
            return Unauthorized(error);
        }

        var deleteEmployeeCommand = new DeleteEmployeeCommand
        (
            employeeId
       );

        ErrorOr<Results> results = await _sender.Send(deleteEmployeeCommand);

        return results.Match(
            success => Ok(success),
            errors => Problem(errors)
        );
    }



    [HttpGet]
    [Authorize]
    public IActionResult CheckIfAuthenticated()
    {
        return Ok("You are authenticated");
    }
    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> SendConfirmationEmail(string email)
    {
        var emailConfirmationQuery = new SendEmailConfirmationQuery(email, Url.Action(nameof(ConfirmEmail)));
        var results = await _sender.Send(emailConfirmationQuery);
        return results.Match(
          hub => Ok(hub),
          errors => Problem(errors)
      );
    }
    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> SendResetPasswordEmail(string email)
    {
        var emailResetPasswordQuery = new SendEmailResetPasswordQuery(email);
        var results = await _sender.Send(emailResetPasswordQuery);
         return results.Match(
          hub => Ok(hub),
          errors => Problem(errors)
      );
    }
    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> ConfirmEmail([FromQuery] ConfirmEmailCommand confirmEmailRequest)
    {
        ErrorOr<Results> results = await _sender.Send(confirmEmailRequest);
        return results.Match(
          hub => Ok(hub),
          errors => Problem(errors)
      );
    }
    [HttpPut]
    [AllowAnonymous]
    public async Task<IActionResult> ResetPassword(ConfirmResetPasswordTokenCommand resetPasswordRequest)
    {

        var results = await _sender.Send(resetPasswordRequest);
        return results.Match(
          hub => Ok(hub),
          errors => Problem(errors)
      );
    }

}