using DepartManagment.Application.CommandInterfaces;
using DepartManagment.Application.Commands.ConfirmResetPasswordToken;
using DepartManagment.Application.Models;
using DepartManagment.Application.Models.Employee;
using DepartManagment.Domain.Entities.ApplicationUser;
using DepartManagment.Domain.Entities.ApplicationUser.Errors;
using ErrorOr;
using Microsoft.AspNetCore.Identity;
using static DepartManagment.Domain.Entities.ApplicationUser.Errors.DomainErrors;
using System.Linq;

namespace DepartManagment.Application.Commands.ResetPassword
{
    public class ConfirmResetPasswordTokenCommandHandler : IHandler<ConfirmResetPasswordTokenCommand, ErrorOr<string>>
    {
        private readonly UserManager<Employee> _userManager;
        private readonly SignInManager<Employee> _signInManager;

        public ConfirmResetPasswordTokenCommandHandler(UserManager<Employee> userManager, SignInManager<Employee> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<ErrorOr<string>> Handle(ConfirmResetPasswordTokenCommand request, CancellationToken cancellationToken)
        {
            var result = new Results();

            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
             
                return "Email does not exist";
            }

            var signInResult = await _signInManager.CheckPasswordSignInAsync(user, request.CurrentPassword, false);
            if (!signInResult.Succeeded)
            {
               
                return "Invalid current password";

            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var resetResult = await _userManager.ResetPasswordAsync(user, token, request.NewPassword);
            if (!resetResult.Succeeded)
            {
                
                return "operation please check your inputs";
            }

            result.IsSuccess = true;
            return "password was reset sucessfully";
        }
    }
}