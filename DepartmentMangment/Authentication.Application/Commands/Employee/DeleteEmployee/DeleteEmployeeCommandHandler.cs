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

namespace DepartManagment.Application.Commands.Employees.DeleteEmployee
{
    public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand, ErrorOr<Results>>
    {
        private readonly UserManager<Employee> _userManager;

        public DeleteEmployeeCommandHandler(UserManager<Employee> userManager)
        {
            _userManager = userManager;
        }

        public async Task<ErrorOr<Results>> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            var results = new Results();

            var employeeToDelete = await _userManager.FindByIdAsync(request.EmployeeId.ToString());
            if (employeeToDelete == null)
            {
                results.AddErrorMessages("Employee not found");
                return results;
            }

            var deleteResult = await _userManager.DeleteAsync(employeeToDelete);

            if (!deleteResult.Succeeded)
            {
                results.AddErrorMessages(deleteResult.Errors.Select(e => e.Description).ToArray());
                return results;
            }

            results.IsSuccess = true;
            return results;
        }
    }
}
