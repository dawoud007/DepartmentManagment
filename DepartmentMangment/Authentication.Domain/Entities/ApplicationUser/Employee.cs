using DepartManagment.Domain.Entities.ApplicationUser.Enums;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace DepartManagment.Domain.Entities.ApplicationUser
{
    public class Employee : IdentityUser
    {
        public string Name { get; set; } = string.Empty;
        public Gender Gender { get; set; }
        public Role Role { get; set; }

        public Guid? DepartmentId { get; set; }
        [NotMapped]
        public Department? Department { get; set; }
    
        [NotMapped]
        public ICollection<EmployeeTask>? Tasks { get; set; } = new List<EmployeeTask>();

    }
}
