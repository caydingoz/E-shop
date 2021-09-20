using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebApplication.CatalogAPI.Application.Commands.Request;
using WebApplication.CatalogAPI.Data;

namespace WebApplication.CatalogAPI.Application.Handlers.CommandHandlers
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommandRequest, bool>
    {
        private readonly ProductDbContext _context;
        public DeleteProductCommandHandler(ProductDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Handle(DeleteProductCommandRequest request, CancellationToken cancellationToken)
        {
            var deleteProduct = await _context.Products.FirstOrDefaultAsync(p => p.Id == request.Id);
            if(deleteProduct == null)
            {
                return false;
            }
            _context.Products.Remove(deleteProduct);
            _context.SaveChanges();
            return true;
        }
    }
}
