namespace DepartManagment.Application.Interfaces;
public interface IResetPasswordEmailSender
{
    Task SendPasswordResetAsync(string toEmail, string token);
}