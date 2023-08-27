
    using DepartManagment.Application.Interfaces;
    using DepartManagment.Domain.Entities;
using DepartManagment.Domain.Entities.ApplicationUser.Errors;
using ErrorOr;
    using global::DepartManagment.Application.CommandInterfaces;
    using MediatR;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

 namespace DepartManagment.Application.Commands.Departments.DeleteDepartments
{
        public class DeleteDepartmentCommandHandler : IHandler<DeleteDepartmentCommand, ErrorOr<Unit>>
        {
            private readonly IDepartmentRepository _departmentRepository;

            public DeleteDepartmentCommandHandler(IDepartmentRepository departmentRepository)
            {
                _departmentRepository = departmentRepository;
            }

            public async Task<ErrorOr<Unit>> Handle(DeleteDepartmentCommand request, CancellationToken cancellationToken)
            {
                // Retrieve the department
                var department = await _departmentRepository.GetByIdAsync(request.DepartmentId);
            if (department == null)
            {
                return DomainErrors.Employees.NotFound;

            }

            // Delete the department
            await _departmentRepository.RemoveAsync(d=>d.Id==request.DepartmentId);
                await _departmentRepository.SaveAsync();

            return Unit.Value;
        }
        }
    }


