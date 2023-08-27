using DepartManagment.Application.Models;
using DepartManagment.Application.Models.Employee;
using MediatR;

namespace DepartManagment.Application.CommandInterfaces;
public interface ICommand<TResponse> : IRequest<TResponse>
{
}