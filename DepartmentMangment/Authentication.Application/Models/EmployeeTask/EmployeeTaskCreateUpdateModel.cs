using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepartManagment.Application.Models.EmployeeTask
{
    public record TaskCreateUpdateModel(
        string Title,
        string Description,
        bool IsCompleted,
        string EmployeeId
    );

}
