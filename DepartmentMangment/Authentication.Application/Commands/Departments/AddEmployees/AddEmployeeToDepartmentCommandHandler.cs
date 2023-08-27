using DepartManagment.Application.CommandInterfaces;
using DepartManagment.Application.Interfaces;
using DepartManagment.Application.Models.Employee;
using DepartManagment.Domain.Entities.ApplicationUser;
using DepartManagment.Domain.Entities.ApplicationUser.Errors;
using ErrorOr;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepartManagment.Application.Commands.Departments.AddEmployees
{
    public class AddEmployeeToDepartmentCommandHandler : IHandler<AddEmployeeToDepartmentCommand, ErrorOr<EmployeeViewModel>>
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly UserManager<Employee> _userManager;

        public AddEmployeeToDepartmentCommandHandler(IDepartmentRepository departmentRepository, UserManager<Employee> userManager)
        {
            _departmentRepository = departmentRepository;
            _userManager = userManager;

        }

        public async Task<ErrorOr<EmployeeViewModel>> Handle(AddEmployeeToDepartmentCommand request, CancellationToken cancellationToken)
        {
            var department = await _departmentRepository.GetByIdAsync(request.DepartmentId);
            if (department == null)
            {
                return DomainErrors.Departments.NotFound;
            }

            var employee = await _userManager.FindByIdAsync(request.EmployeeId);
            if (employee == null)
            {
                return DomainErrors.Employees.NotFound;
            }
            employee.DepartmentId = request.DepartmentId;   
            department.Employees.Add(employee);

            await _departmentRepository.UpdateAsync(department);
            await _departmentRepository.SaveAsync();

            return employee.Adapt<EmployeeViewModel>();
        }
    }
}