using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.CatalogAPI.Model;

namespace WebApplication.CatalogAPI.Application.Commands.Request
{
    public class UpdateProductCommandRequest : IRequest<bool>
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Category { get; private set; }
        public decimal Price { get; private set; }
        public int Stock { get; private set; }
        public bool IsSalable { get; private set; }

        public UpdateProductCommandRequest(ProductDTO product)
        {
            Id = product.Id;
            Name = product.Name;
            Category = product.Category;
            Price = product.Price;
            Stock = product.Stock;
            IsSalable = product.IsSalable;
        }
    }
}
