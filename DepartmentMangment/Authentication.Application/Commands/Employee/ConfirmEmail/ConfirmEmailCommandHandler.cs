using System.Web;
using DepartManagment.Application.CommandInterfaces;
using DepartManagment.Application.Models;
using DepartManagment.Application.Models.Employee;
using DepartManagment.Domain.Entities.ApplicationUser;
using DepartManagment.Domain.Entities.ApplicationUser.Errors;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Identity;
using static DepartManagment.Domain.Entities.ApplicationUser.Errors.DomainErrors;

namespace DepartManagment.Application.Commands.ConfirmEmail;
public class ConfirmEmailCommandHandler : IHandler<ConfirmEmailCommand, ErrorOr<Results>>
{
    private readonly UserManager<Employee> _userManager;

    public ConfirmEmailCommandHandler(UserManager<Employee> userManager)
    {
        _userManager = userManager;
    }

    public async Task<ErrorOr<Results>> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
    {
        var DepartManagmentResults = new Results();
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user is null)
        {
            DepartManagmentResults.AddErrorMessages(UserErrors.EmailDoesNotExist);
            return DepartManagmentResults;
        }
        var result = await _userManager.ConfirmEmailAsync(user, request.Token);
        if (!result.Succeeded)
        {
            DepartManagmentResults.AddErrorMessages(result.Errors.Select(e => e.Description).ToArray());
            return DepartManagmentResults;
        }


        DepartManagmentResults.IsSuccess = true;
    

        return DepartManagmentResults;
    }
}