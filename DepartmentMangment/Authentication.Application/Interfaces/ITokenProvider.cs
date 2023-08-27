using Microsoft.AspNetCore.Identity;

namespace DepartManagment.Application.Interfaces;
public interface ITokenGenerator
{
    string Generate(IdentityUser user);
}