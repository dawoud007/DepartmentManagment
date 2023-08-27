using DepartManagment.Domain.Entities.ApplicationUser;

namespace DepartManagment.Application.Models.Employee;
public class Results
{
    public Results()
    {
        ErrorMessages = new List<string>();
    }
    public bool IsSuccess { get; set; }
 
    public string Token { get; private set; } = string.Empty;
    public List<string> ErrorMessages { get; private set; }
    public EmployeeViewModel? User { get; set; }
    public void AddErrorMessages(params string[] errorMessage)
    {
        ErrorMessages.AddRange(errorMessage);
    }
    public void SetToken(string token)
    {
        if (string.IsNullOrEmpty(Token))
            Token = token;
    }


}