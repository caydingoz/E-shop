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
    public class GetByCategoryProductQueryHandler : IRequestHandler<GetByCategoryProductQueryRequest, List<GetByCategoryProductQueryResponse>>
    {
        private readonly ProductDbContext _context;
        public GetByCategoryProductQueryHandler(ProductDbContext context)
        {
            _context = context;
        }
        public async Task<List<GetByCategoryProductQueryResponse>> Handle(GetByCategoryProductQueryRequest request, CancellationToken cancellationToken)
        {
            return await _context.Products.Where(product => product.Category == request.Category).Select(product => new GetByCategoryProductQueryResponse
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
