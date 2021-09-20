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
    public class GetAllProductQueryHandler : IRequestHandler<GetAllProductQueryRequest, List<GetAllProductQueryResponse>>
    {
        private readonly ProductDbContext _context;
        public GetAllProductQueryHandler(ProductDbContext context)
        {
            _context = context;
        }
        public async Task<List<GetAllProductQueryResponse>> Handle(GetAllProductQueryRequest request, CancellationToken cancellationToken)
        {
            return await _context.Products.Select(product => new GetAllProductQueryResponse
            {
                Id = product.Id,
                Name = product.Name,
                Category = product.Category,
                Price = product.Price,
                Stock = product.Stock,
                IsSalable = product.IsSalable
            }).ToListAsync();
        }
    }
}
