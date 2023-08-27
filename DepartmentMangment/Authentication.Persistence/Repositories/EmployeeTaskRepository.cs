using CommonGenericClasses;
using DepartManagment.Application.Interfaces;
using DepartManagment.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepartManagment.Persistence.Repositories
{
    public class EmployeeTaskRepository : BaseRepo<EmployeeTask>, IEmployeeTaskRepository
    {
        private readonly ApplicationDbContext _context;
        public EmployeeTaskRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
