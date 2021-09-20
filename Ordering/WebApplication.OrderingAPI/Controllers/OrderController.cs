using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.OrderingAPI.Applications.Commands;
using WebApplication.OrderingAPI.Applications.Queries;
using WebApplication.OrderingAPI.Infrastructure.Auth;
using WebApplication.OrderingDomain.Models;

namespace WebApplication.OrderingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : Controller
    {
        private readonly ILogger<OrderController> _logger;
        private readonly IMediator _mediator;
        public OrderController(ILogger<OrderController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrderAsync([FromBody] CreateOrderCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }

        [Route("cancel")]
        [HttpPut]
        public async Task<IActionResult> CancelOrderAsync([FromBody] CancelOrderCommand command)
        {
            //auth check
            //order check
            //is order belongs to user check
            var jwt = Request.Cookies["jwt"];
            if (jwt is null) return BadRequest(new { message = "Unauthorized" });
            int customerId = AuthCheck.GetCustomerId(jwt);
            
            if (await _mediator.Send(new GetOrderByIdQuery(command.OrderId)) == null)
            {
                return NotFound();
            }

            var requestCancelOrder = new IdentifiedCommand<CancelOrderCommand>(command, customerId);
            _logger.LogInformation(
                "Sending command: IdentifiedCommand - OrderId: {OrderId}, CustomerId: {CustomerId} ({@Command})",
                requestCancelOrder.Command.OrderId,
                requestCancelOrder.CustomerId,
                requestCancelOrder);

            bool requestValid = await _mediator.Send(requestCancelOrder);

            if (!requestValid) return BadRequest(new { message = "Order and Customer are not matching!" });

            _logger.LogInformation(
                "Sending command: CancelOrderCommand - OrderId: {OrderId} ({@Command})",
                command.OrderId,
                command);

            await _mediator.Send(command);

            return Ok();
        }

        [Route("{orderId:int}")]
        [HttpGet]
        public async Task<ActionResult> GetOrderAsync(int orderId)
        {
            var jwt = Request.Cookies["jwt"];
            if (jwt is null) return BadRequest(new { message = "Unauthorized" });
            int customerId = AuthCheck.GetCustomerId(jwt);
            try
            {
                var requestGetOrderById = new GetOrderByIdQuery(orderId);
                _logger.LogInformation(
                    "Sending query: GetOrderByIdQuery - OrderId: {OrderId} ({@Query})",
                    orderId,
                    requestGetOrderById
                    );
                var order = await _mediator.Send(requestGetOrderById);
                if (customerId != order.BuyerId) return BadRequest(new { message = "OrderId not matching with user" });

                return Ok(order);
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<Order>>> GetOrdersAsync()
        {
            var jwt = Request.Cookies["jwt"];
            if (jwt is null) return BadRequest(new { message = "Unauthorized" });
            int customerId = AuthCheck.GetCustomerId(jwt);
            try
            {
                var requestGetOrdersByUserId = new GetOrdersByUserIdQuery(customerId);
                _logger.LogInformation(
                    "Sending query: GetOrdersByUserIdQuery - CustomerId: {CustomerId} ({@Query})",
                    customerId,
                    requestGetOrdersByUserId
                    );
                var orders = await _mediator.Send(requestGetOrdersByUserId);

                return Ok(orders);
            }
            catch
            {
                return NotFound();
            }
        }
    }
}
