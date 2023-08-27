using DepartManagment.Application.CommandInterfaces;
using DepartManagment.Application.Interfaces;
using DepartManagment.Application.Models.Department;
using DepartManagment.Domain.Entities;
using DepartManagment.Domain.Entities.ApplicationUser;
using ErrorOr;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DepartManagment.Application.Queries.Departments.GetDepartments
{
      public class GetAllDepartmentsQueryHandler : IHandler<GetAllDepartmentsQuery, ErrorOr<List<DepartmentsViewModel>>>
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IEmployeeRepository _employeerRepository;
        private readonly UserManager<Employee> _userManager;

        public GetAllDepartmentsQueryHandler(IDepartmentRepository departmentRepository, UserManager<Employee> userManager, IEmployeeRepository employeerRepository)
        {
            _departmentRepository = departmentRepository;
            _userManager = userManager;
            _employeerRepository= employeerRepository;

        }

        public async Task<ErrorOr<List<DepartmentsViewModel>>> Handle(GetAllDepartmentsQuery request, CancellationToken cancellationToken)
        {
            var departments = await _departmentRepository.GetAllAsync();
            /*foreach (var department in departments)
            {
                department.Manager = (await _employeerRepository.Get(u => u.DepartmentId == department.Id, null, "")).FirstOrDefault();
            }*/


            var departmentViewModels = departments
                .Select(department =>
               
            department.Adapt<DepartmentsViewModel>())
                .ToList();

            var totalItems = departmentViewModels.Count;
            var totalPages = (int)Math.Ceiling(totalItems / (double)request.PageSize);

            var pagedResults = departmentViewModels
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToList();

            return pagedResults;
        }
    }
}