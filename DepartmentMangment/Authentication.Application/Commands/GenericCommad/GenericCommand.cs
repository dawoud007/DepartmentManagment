using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepartManagment.Application.Commands.GenericCommad
{
    public class GenericCommand<TResponse> : IRequest<TResponse>
    {
        public TResponse Data { get; set; }
    }

}
