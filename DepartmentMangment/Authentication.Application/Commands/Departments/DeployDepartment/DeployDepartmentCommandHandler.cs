using DepartManagment.Application.CommandInterfaces;
using DepartManagment.Application.Interfaces;
using DepartManagment.Application.Models.Department;
using DepartManagment.Application.Models.Employee;
using DepartManagment.Domain.Entities;
using DepartManagment.Domain.Entities.ApplicationUser;
using ErrorOr;
using Mapster;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace DepartManagment.Application.Commands.Departments
{
    public class DeployDepartmentCommandHandler : IHandler<DeployDepartmentCommand, ErrorOr<DepartmentViewModel>>
    {

        private readonly IDepartmentRepository _departmentRepository;
        private readonly UserManager<Employee> _userManager;
        public DeployDepartmentCommandHandler(IDepartmentRepository departmentRepository, UserManager<Employee> userManager)
        {
            _departmentRepository = departmentRepository;
            _userManager = userManager;
        }
        public async Task<ErrorOr<DepartmentViewModel>> Handle(DeployDepartmentCommand request, CancellationToken cancellationToken)
        {

            Employee user = await _userManager.FindByIdAsync(request.MangerId)!;
            if (user != null)
            {

            }

            var department = new Department
            {
                Id = new Guid(),
                Name = request.Name,
                ManagerId = user.Id,
                Manager = user,
            };

            // Add the department to the repository
            var Manager = department.Manager.Adapt<EmployeeViewModel>();
            await _departmentRepository.AddAsync(department);
            await _departmentRepository.SaveAsync();

            // Map entity to view model
            var departmentViewModel = new DepartmentViewModel(
             department.Id,
              department.Name,
              Manager,
             new List<EmployeeViewModel>() // You might need to populate this list appropriately
  );


            return departmentViewModel;
        }
    }

}
