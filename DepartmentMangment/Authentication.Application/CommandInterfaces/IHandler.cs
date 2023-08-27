using DepartManagment.Application.Models;
using DepartManagment.Application.Models.Employee;
using MediatR;

namespace DepartManagment.Application.CommandInterfaces;
public interface IHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
where TRequest : IRequest<TResponse>
{

}
