using ErrorOr;

namespace DepartManagment.Domain.Entities.ApplicationUser.Errors;
public static partial class DomainErrors
{
    public static class UserErrors
    {
        public static string EmailAlreadyExists => "Email already exists, please login";
        public static string EmailDoesNotExist => "User with this email does not exist";
        public static string UserNameAlreadyExists => "Username already exists, please login";
    }
    public static class Employees
    {
 
        public static Error InvalidEmployee => Error.Failure("Employee.InvalidEmployee", "Invalid Employee, couldn't complete your request");
        public static Error NotFound => Error.NotFound("Employee.NotFound", "Employee not found, create Employee");
        public static Error CannotJoinEmployee => Error.Failure("Employee.CannotJoin", "Can't join Employee, please check your input");

        public static Error AlreadyExest => Error.Failure("Employee.AlreadyExest", "Employee aleaready exists");

        public static Error CannotDeleteEmployee => Error.Failure("Employee.CannaotLeaveEmployee","this employee cant be Deleted");


    }
    public static class Tasks
    {

        public static Error InvalidTask => Error.Failure("Task.InvalidTask", "Invalid Task, couldn't complete your request");
        public static Error NotFound => Error.NotFound("Task.NotFound", "Task not found, create Task");
        public static Error CannotJoinTask => Error.Failure("Task.CannotJoin", "Can't join Task, please check your input");

        public static Error AlreadyExest => Error.Failure("Task.AlreadyExest", "Task aleaready exists");

        public static Error CannotDeleteTask => Error.Failure("Task.CannaotLeaveTask", "this Task cant be Deleted");


    }


    public static class Departments
    {

        public static Error InvalidDepartment => Error.Failure("Department.InvalidDepartment", "Invalid Department, couldn't complete your request");
        public static Error NotFound => Error.NotFound("Department.NotFound", "Department not found, create Department");
        public static Error CannotJoinDepartment => Error.Failure("Department.CannotJoin", "Can't join Department, please check your input");

        public static Error AlreadyExest => Error.Failure("Department.AlreadyExest", "Department aleaready exists");

        public static Error CannotDeleteDepartment => Error.Failure("Department.CannaotLeaveDepartment", "this Department cant be Deleted");


    }
}