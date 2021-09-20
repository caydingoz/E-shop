using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebApplication.CatalogAPI.Application.Commands.Request;
using WebApplication.CatalogAPI.Data;

namespace WebApplication.CatalogAPI.Application.Handlers.Request
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommandRequest, bool>
    {
        private readonly ProductDbContext _context;
        public CreateProductCommandHandler(ProductDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
        {
            await _context.Products.AddAsync(new()
            {
                Name = request.Name,
                Category = request.Category,
                Price = request.Price,
                Stock = request.Stock,
                IsSalable = request.IsSalable,
                CreateTime = DateTime.Now,
                LastUpdateTime = DateTime.Now
            });
            _context.SaveChanges();
            return true ;
        }
    }
}
