using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebApplication.CatalogAPI.Application.Queries.Request;
using WebApplication.CatalogAPI.Application.Queries.Response;
using WebApplication.CatalogAPI.Data;

namespace WebApplication.CatalogAPI.Application.Handlers.QueryHandlers
{
    public class GetByIdProductQueryHandler : IRequestHandler<GetByIdProductQueryRequest, GetByIdProductQueryResponse>
    {
        private readonly ProductDbContext _context;
        public GetByIdProductQueryHandler(ProductDbContext context)
        {
            _context = context;
        }
        public async Task<GetByIdProductQueryResponse> Handle(GetByIdProductQueryRequest request, CancellationToken cancellationToken)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == request.Id);
            return new GetByIdProductQueryResponse
            {
                Id = product.Id,
                Name = product.Name,
                Category = product.Category,
                Price = product.Price,
                Stock = product.Stock,
                IsSalable = product.IsSalable
            };
        }
    }
}
