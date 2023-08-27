using DepartManagment.Application.Models.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepartManagment.Application.Models.Department
{
    public record UpdateDepartmentModel
    (
      string Name,
      string ManagerId
    );

}
