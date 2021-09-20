using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.CatalogAPI.Application.Commands.Request
{
    public class DeleteProductCommandRequest : IRequest<bool>
    {
        public int Id { get; set; }
    }
}
