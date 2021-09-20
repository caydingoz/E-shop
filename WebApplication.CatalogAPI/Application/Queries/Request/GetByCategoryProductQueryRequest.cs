using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.CatalogAPI.Application.Queries.Response;

namespace WebApplication.CatalogAPI.Application.Queries.Request
{
    public class GetByCategoryProductQueryRequest : IRequest<List<GetByCategoryProductQueryResponse>>
    {
        public string Category { get; set; }
    }
}
