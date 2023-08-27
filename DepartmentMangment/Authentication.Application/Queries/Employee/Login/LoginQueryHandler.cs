using DepartManagment.Application.CommandInterfaces;
using DepartManagment.Application.Interfaces;
using DepartManagment.Application.Models;
using DepartManagment.Application.Models.Employee;
using DepartManagment.Domain.Entities.ApplicationUser;
using ErrorOr;
using Mapster;
using Microsoft.AspNetCore.Identity;

namespace DepartManagment.Application.Queries.Login;
public class LoginQueryHandler : IHandler<LoginQuery, ErrorOr<Results>>
{
    private readonly UserManager<Employee> _userManager;
    private readonly ITokenGenerator _tokenGenerator;

    public LoginQueryHandler(UserManager<Employee> userManager, ITokenGenerator tokenGenerator)
    {
        _userManager = userManager;
        _tokenGenerator = tokenGenerator;
    }

    public async Task<ErrorOr<Results>> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        var DepartManagmentResults = new Results();
        var user = await _userManager.FindByNameAsync(request.UserName);
        if (user is null)
        {
            DepartManagmentResults.AddErrorMessages("username doesn't exist, Please register");
            return DepartManagmentResults;
        }
        if (!await _userManager.CheckPasswordAsync(user, request.Password))
        {
            DepartManagmentResults.AddErrorMessages("Incorrect password");
            return DepartManagmentResults;
        }

        // if (!user.EmailConfirmed)
        // {
        //     DepartManagmentResults.AddErrorMessages("you email is not confirmed please confirm it first");
        // return DepartManagmentResults;
        // }
        var token = _tokenGenerator.Generate(user);
        DepartManagmentResults.SetToken(token);
        DepartManagmentResults.IsSuccess = true;
        DepartManagmentResults.User = user.Adapt<EmployeeViewModel>();
        return DepartManagmentResults;
    }
}
