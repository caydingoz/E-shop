using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.BasketAPI.Model;
using DotNetCore.CAP;
using WebApplication.BasketAPI.IntegrationEvents.Events;
using WebApplication.BasketAPI.Infrastructure.Auth;

namespace WebApplication.BasketAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository _repository;
        private readonly ICapPublisher _eventBus;
        private readonly ILogger<BasketController> _logger;

        public BasketController( ILogger<BasketController> logger, IBasketRepository repository, ICapPublisher eventBus)
        {
            _logger = logger;
            _repository = repository;
            _eventBus = eventBus;
        }

        [HttpGet]
        public async Task<ActionResult<CustomerBasket>> GetBasketByIdAsync()
        {
            var jwt = Request.Cookies["jwt"];
            var customerId = AuthCheck.GetCustomerId(jwt);
            var basket = await _repository.GetBasketAsync(customerId);

            return Ok(basket ?? new CustomerBasket(customerId));
        }

        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> UpdateBasketAsync([FromBody] CustomerBasket basket)
        {
            return Ok(await _repository.UpdateBasketAsync(basket));
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteBasketByIdAsync()
        {
            var jwt = Request.Cookies["jwt"];
            var customerId = AuthCheck.GetCustomerId(jwt);
            return Ok(await _repository.DeleteBasketAsync(customerId));
        }

        [Route("checkout")]
        [HttpPost]
        public async Task<ActionResult> CheckoutAsync([FromBody] BasketCheckout basketCheckout)
        {
            var jwt = Request.Cookies["jwt"];
            var customerId = AuthCheck.GetCustomerId(jwt);

            var basket = await _repository.GetBasketAsync(customerId);

            if (basket == null) return BadRequest( new { message = " Basket empty!" });


            var eventMessage = new UserCheckoutIntegrationEvent(customerId, basketCheckout.CustomerName, basketCheckout.Adress, basketCheckout.CardNumber, basket);
        
            _eventBus.Publish(nameof(UserCheckoutIntegrationEvent), eventMessage);

            return Ok(eventMessage);
        }
    }
}
