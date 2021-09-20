using DotNetCore.CAP;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.CatalogAPI.Application.Commands.Request;
using WebApplication.CatalogAPI.Application.Queries.Request;
using WebApplication.CatalogAPI.Application.Queries.Response;
using WebApplication.CatalogAPI.IntegrationEvents.Events;
using WebApplication.CatalogAPI.Model;

namespace WebApplication.CatalogAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private readonly IMediator _mediator;
        private readonly ICapPublisher _eventBus;
        private readonly ILogger<ProductController> _logger;

        public ProductController(IMediator mediator, ILogger<ProductController> logger, ICapPublisher eventBus)
        {
            _mediator = mediator;
            _logger = logger;
            _eventBus = eventBus;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProduct()
        {
            List<GetAllProductQueryResponse> allProducts = await _mediator.Send(new GetAllProductQueryRequest());
            return Ok(allProducts);
        }

        [HttpGet("{category}")]
        public async Task<IActionResult> GetById([FromQuery] int id, [FromRoute] string category)
        {
            GetByIdProductQueryResponse product = await _mediator.Send(new GetByIdProductQueryRequest(id, category)); // category kullanmıyorum gerekli olur diye aldım.
            
            return Ok(product);
        }

        [HttpGet("category")]
        public async Task<IActionResult> GetByCategory([FromQuery] GetByCategoryProductQueryRequest requestModel)
        {
            List<GetByCategoryProductQueryResponse> productList = await _mediator.Send(requestModel);
            
            return Ok(productList);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductCommandRequest requestModel)
        {
            var response = await _mediator.Send(requestModel);
            
            return Ok(response);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteProduct([FromQuery] DeleteProductCommandRequest requestModel)
        {
            var response = await _mediator.Send(requestModel);
            
            return Ok(response);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateProductPriceAsync([FromBody] ProductDTO productToUpdate)
        {
            GetByIdProductQueryResponse product = await _mediator.Send( new GetByIdProductQueryRequest(productToUpdate.Id, productToUpdate.Category));

            if (product == null) return NotFound( new { message = "Item not found." });

            bool raiseProductPriceChangedEvent = product.Price == productToUpdate.Price;

            if (raiseProductPriceChangedEvent) return BadRequest( new { message = "Item price same with the given new price!" });

            await _mediator.Send(new UpdateProductCommandRequest(productToUpdate));

            var priceChangedEvent = new ProductPriceChangedIntegrationEvent(productToUpdate.Id, productToUpdate.Price, product.Price);

            _eventBus.Publish(nameof(ProductPriceChangedIntegrationEvent), priceChangedEvent);

            return Ok();
        }

    }
}
