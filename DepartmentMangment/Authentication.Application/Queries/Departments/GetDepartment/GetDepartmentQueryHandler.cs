using DepartManagment.Application.CommandInterfaces;
using DepartManagment.Application.Interfaces;
using DepartManagment.Application.Models.Department;
using DepartManagment.Application.Models.Employee;
using DepartManagment.Application.Queries.Departments.GetDepartment;
using DepartManagment.Domain.Entities;
using DepartManagment.Domain.Entities.ApplicationUser;
using DepartManagment.Domain.Entities.ApplicationUser.Errors;
using ErrorOr;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace DepartManagment.Application.Queries.Departments.GetDepartment
{
    public class GetDepartmentByIdQueryHandler : IHandler<GetDepartmentByIdQuery, ErrorOr<DepartmentViewModel>>
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly UserManager<Employee> _userManager;
        public GetDepartmentByIdQueryHandler(IDepartmentRepository departmentRepository, UserManager<Employee> userManager)
        {
            _departmentRepository = departmentRepository;
            _userManager = userManager;
        }

        public async Task<ErrorOr<DepartmentViewModel>> Handle(GetDepartmentByIdQuery request, CancellationToken cancellationToken)
        {
            var department = await _departmentRepository.GetByIdAsync(request.DepartmentId);

            if (department == null)
            {
                return DomainErrors.Employees.NotFound;
            }

          
        var Employees=(_userManager.Users.Where(u=>u.DepartmentId==request.DepartmentId)).ToList();
            Employees.Adapt<List<EmployeeViewModel>>();
            department.Employees = Employees;

            return department.Adapt<DepartmentViewModel>();
        }
    }
}