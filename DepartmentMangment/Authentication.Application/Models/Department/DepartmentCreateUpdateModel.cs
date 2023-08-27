using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepartManagment.Application.Models.Department
{
    public record DepartmentCreateUpdateModel
    (
      string Name,
       string ManagerId
    );

}
