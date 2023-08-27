using DepartManagment.Domain.Commons;
using DepartManagment.Domain.Entities.ApplicationUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepartManagment.Domain.Entities
{
    public class EmployeeTask : BaseEntity
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public bool IsCompleted { get; set; }
        public string? EmployeeId { get; set; }
        public Employee? Employee { get; set; }
    }

}
