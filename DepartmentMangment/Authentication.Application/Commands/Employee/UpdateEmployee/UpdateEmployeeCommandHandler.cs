using DepartManagment.Application.Commands.Employees.UpdateEmployee;
using DepartManagment.Application.Models.Employee;
using DepartManagment.Domain.Entities.ApplicationUser;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepartManagment.Application.Commands.Employees.UpdateEmployee
{
    public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, ErrorOr<Results>>
    {
        private readonly UserManager<Employee> _userManager;

        public UpdateEmployeeCommandHandler(UserManager<Employee> userManager)
        {
            _userManager = userManager;
        }

        public async Task<ErrorOr<Results>> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var results = new Results();

            var existingEmployee = await _userManager.FindByIdAsync(request.EmployeeId.ToString());
            if (existingEmployee == null)
            {
                results.AddErrorMessages("Employee not found");
                return results;
            }
  
            existingEmployee.Name = request.EmployeeUpdateModel.Name;
            existingEmployee.Email = request.EmployeeUpdateModel.Email;
            existingEmployee.UserName = request.EmployeeUpdateModel.UserName;
            existingEmployee.Gender = request.EmployeeUpdateModel.Gender;
            existingEmployee.Role = request.EmployeeUpdateModel.Role;


            var updateResult = await _userManager.UpdateAsync(existingEmployee);

            if (!updateResult.Succeeded)
            {
                results.AddErrorMessages(updateResult.Errors.Select(e => e.Description).ToArray());
                return results;
            }

            results.IsSuccess = true;
            return results;
        }
    }
}
