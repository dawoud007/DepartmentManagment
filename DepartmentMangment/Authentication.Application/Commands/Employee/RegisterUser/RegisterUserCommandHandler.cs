using DepartManagment.Application.CommandInterfaces;
using DepartManagment.Application.Interfaces;
using DepartManagment.Application.Models;
using DepartManagment.Application.Models.Employee;
using DepartManagment.Domain.Entities.ApplicationUser;
using DepartManagment.Domain.Entities.ApplicationUser.Errors;
using ErrorOr;
using Mapster;
using Microsoft.AspNetCore.Identity;
using static DepartManagment.Domain.Entities.ApplicationUser.Errors.DomainErrors;

namespace DepartManagment.Application.Commands.RegisterUser;
public class RegisterUserCommandHandler : IHandler<RegisterUserCommand, ErrorOr<Results>>
{
    private readonly UserManager<Employee> _userManager;
    private readonly ITokenGenerator _tokenGenerator;
    private readonly IConfirmationEmailSender _emailSender;

    public RegisterUserCommandHandler(
        UserManager<Employee> userManager,
        IConfirmationEmailSender confirmationEmailSender,
        ITokenGenerator tokenGenerator
        )
    {
        _emailSender = confirmationEmailSender;
        _userManager = userManager;
        _tokenGenerator = tokenGenerator;
    }

    public async Task<ErrorOr<Results>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var DepartManagmentResults = new Results();
        var user = request.Adapt<Employee>();
        if (await _userManager.FindByNameAsync(user.UserName) is not null)
        {
            DepartManagmentResults.AddErrorMessages(UserErrors.UserNameAlreadyExists);
        }
        if (await _userManager.FindByEmailAsync(user.Email) is not null)
        {
            DepartManagmentResults.AddErrorMessages(UserErrors.EmailAlreadyExists);
        }

        if (DepartManagmentResults.ErrorMessages.Count > 0)
            return DepartManagmentResults;
        var result = await _userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
        {
            DepartManagmentResults.AddErrorMessages(result.Errors.Select(e => e.Description).ToArray());
            return DepartManagmentResults;
        }

        string token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        try
        {
            await _emailSender.SendConfirmationAsync(request.Email, request.ConfirmationLink, token);
        }
        catch
        {
            DepartManagmentResults.AddErrorMessages("Couldn't send confirmation email, try to confirm your email later");
        }
        var userReadModel = user.Adapt<EmployeeViewModel>();
        DepartManagmentResults.IsSuccess = true;

        return DepartManagmentResults;
    }
}
