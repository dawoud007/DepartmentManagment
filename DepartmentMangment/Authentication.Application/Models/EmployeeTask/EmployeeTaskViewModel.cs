using DepartManagment.Application.Models.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepartManagment.Application.Models.EmployeeTask
{
    public record TaskViewModel(
      Guid Id,
      string Title,
      string Description,
      bool IsCompleted,
      string EmployeeId
  );

}
