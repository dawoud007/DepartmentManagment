using System.Web;
using DepartManagment.Application.Interfaces;
using DepartManagment.Infrastructure.Models;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MimeKit;

namespace DepartManagment.Infrastructure.NetworkCalls.EmailSender;
public class ConfirmationEmailSender : BaseEmailSender, IConfirmationEmailSender
{
    public ConfirmationEmailSender(IOptions<Smtp> smtp, IConfiguration configuration) : base(smtp, configuration)
    {
    }
    
    
}