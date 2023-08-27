using DepartManagment.Application.CommandInterfaces;
using DepartManagment.Application.Interfaces;
using DepartManagment.Application.Models;
using DepartManagment.Application.Models.Employee;
using DepartManagment.Domain.Entities.ApplicationUser;
using DepartManagment.Domain.Entities.ApplicationUser.Errors;
using ErrorOr;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using static DepartManagment.Domain.Entities.ApplicationUser.Errors.DomainErrors;

namespace DepartManagment.Application.Queries.SendEmailResetPassword;
public class SendEmailResetPasswordQueryHandler : IHandler<SendEmailResetPasswordQuery, ErrorOr<Results>>
{
    private readonly UserManager<Employee> _userManager;
    private readonly IResetPasswordEmailSender _emailSender;
    public SendEmailResetPasswordQueryHandler(UserManager<Employee> userManager, IResetPasswordEmailSender resetPasswordEmailSender)
    {
        _userManager = userManager;
        _emailSender = resetPasswordEmailSender;
    }

    public async Task<ErrorOr<Results>> Handle(SendEmailResetPasswordQuery request, CancellationToken cancellationToken)
    {
        var DepartManagmentResults = new Results();
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user is null)
        {
            DepartManagmentResults.AddErrorMessages(UserErrors.EmailDoesNotExist);
            return DepartManagmentResults;
        }
        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        try
        {
            await _emailSender.SendPasswordResetAsync(toEmail: request.Email, token);
            DepartManagmentResults.IsSuccess = true;
        }
        catch
        {
            DepartManagmentResults.AddErrorMessages("Couldn't send change password email");
        }
        return DepartManagmentResults;
    }
}
