using DepartManagment.Application.CommandInterfaces;
using DepartManagment.Application.Commands.Departments.UpdateDepartment;
using DepartManagment.Application.Interfaces;
using DepartManagment.Application.Models.Department;
using DepartManagment.Application.Models.Employee;
using DepartManagment.Domain.Entities;
using DepartManagment.Domain.Entities.ApplicationUser;
using DepartManagment.Domain.Entities.ApplicationUser.Errors;
using ErrorOr;
using Mapster;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DepartManagment.Application.Commands.Departments
{
    public class UpdateDepartmentCommandHandler : IHandler<UpdateDepartmentCommand, ErrorOr<UpdateDepartmentModel>>
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly UserManager<Employee> _userManager;

        public UpdateDepartmentCommandHandler(IDepartmentRepository departmentRepository, UserManager<Employee> userManager)
        {
            _departmentRepository = departmentRepository;
            _userManager = userManager;
        }

        public async Task<ErrorOr<UpdateDepartmentModel>> Handle(UpdateDepartmentCommand request, CancellationToken cancellationToken)
        {
            // Retrieve the existing department
            var existingDepartment = await _departmentRepository.GetByIdAsync(request.DepartmentId);
            if (existingDepartment == null)
            {
                return DomainErrors.Employees.NotFound;
            }

            // Update properties
            existingDepartment.Name = request.NewName;

            if (!string.IsNullOrEmpty(request.NewManagerId))
            {
                Employee newManager = await _userManager.FindByIdAsync(request.NewManagerId);
                if (newManager != null)
                {
                    existingDepartment.ManagerId = newManager.Id;
                    existingDepartment.Manager = newManager;
                }
                else
                {
                    return DomainErrors.Employees.NotFound;

                }
            }

            // Update the department
            await _departmentRepository.UpdateAsync(existingDepartment);
            await _departmentRepository.SaveAsync();

            // Map entity to view model
            var departmentViewModel = existingDepartment.Adapt<UpdateDepartmentModel>();

            return departmentViewModel;
        }
    }
}
