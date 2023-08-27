using DepartManagment.Domain.Commons;
using DepartManagment.Domain.Entities.ApplicationUser;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepartManagment.Domain.Entities
{
    public class Department:BaseEntity
    {
        public string? Name { get; set; }

        public string? ManagerId { get; set; }
        public Employee? Manager { get; set; }

        [NotMapped]

        public ICollection<Employee> Employees { get; set; } = new List<Employee>();
    }

}
