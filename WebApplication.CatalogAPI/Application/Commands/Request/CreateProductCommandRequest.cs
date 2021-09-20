using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.CatalogAPI.Application.Commands.Request
{
    public class CreateProductCommandRequest : IRequest<bool>
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public bool IsSalable { get; set; }
    }
}
