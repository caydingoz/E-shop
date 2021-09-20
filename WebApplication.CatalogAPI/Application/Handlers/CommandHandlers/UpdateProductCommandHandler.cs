using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebApplication.CatalogAPI.Application.Commands.Request;
using WebApplication.CatalogAPI.Data;
using WebApplication.CatalogAPI.Model;

namespace WebApplication.CatalogAPI.Application.Handlers.CommandHandlers
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommandRequest, bool>
    {
        private readonly ProductDbContext _context;
        public UpdateProductCommandHandler(ProductDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Handle(UpdateProductCommandRequest request, CancellationToken cancellationToken)
        {
            var updateProduct = await _context.Products.SingleOrDefaultAsync(p => p.Id == request.Id);

            updateProduct.Name = request.Name;
            updateProduct.Category = request.Category;
            updateProduct.Price = request.Price;
            updateProduct.Stock = request.Stock;
            updateProduct.IsSalable = request.IsSalable;
            updateProduct.LastUpdateTime = DateTime.Now;

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
