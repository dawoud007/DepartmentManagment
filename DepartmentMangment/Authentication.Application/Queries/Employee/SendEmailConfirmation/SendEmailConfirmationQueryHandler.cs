using System.Web;
using DepartManagment.Application.CommandInterfaces;
using DepartManagment.Application.Interfaces;
using DepartManagment.Application.Models;
using DepartManagment.Application.Models.Employee;
using DepartManagment.Domain.Entities.ApplicationUser;
using DepartManagment.Domain.Entities.ApplicationUser.Errors;
using ErrorOr;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using static DepartManagment.Domain.Entities.ApplicationUser.Errors.DomainErrors;

namespace DepartManagment.Application.Queries.SendEmailConfirmation;
public class SendEmailConfirmationQueryHandler : IHandler<SendEmailConfirmationQuery, ErrorOr<Results>>
{
    private readonly UserManager<Employee> _userManager;
    private readonly IConfiguration _configuration;
    private readonly IConfirmationEmailSender _emailSender;

    public SendEmailConfirmationQueryHandler(
        UserManager<Employee> userManager,
        IConfiguration configuration,
        IConfirmationEmailSender confirmationEmailSender)
    {
        _userManager = userManager;
        _emailSender = confirmationEmailSender;
        _configuration = configuration;
    }

    public async Task<ErrorOr<Results>> Handle(SendEmailConfirmationQuery request, CancellationToken cancellationToken)
    {
        var DepartManagmentResults = new Results();
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user is null)
        {
            DepartManagmentResults.AddErrorMessages(UserErrors.EmailDoesNotExist);
            return DepartManagmentResults;
        }
        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        try
        {
            await _emailSender.SendConfirmationAsync(toEmail: request.Email, request.ConfirmationLink, token);
            DepartManagmentResults.IsSuccess = true;
        }
        catch
        {
            DepartManagmentResults.AddErrorMessages("Couldn't send confirmation email");
        }
        return DepartManagmentResults;
    }
}
