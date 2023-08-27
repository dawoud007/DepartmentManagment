using CommonGenericClasses;
using DepartManagment.Application.Interfaces;
using DepartManagment.Domain.Entities;
using DepartManagment.Domain.Entities.ApplicationUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepartManagment.Persistence.Repositories
{

    public class EmployeeRepository : BaseRepoForIdentity<Employee>, IEmployeeRepository
    {
        private readonly ApplicationDbContext _context;
        public EmployeeRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
