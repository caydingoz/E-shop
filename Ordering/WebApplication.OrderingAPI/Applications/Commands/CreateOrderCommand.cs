using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.OrderingAPI.Applications.Models;

namespace WebApplication.OrderingAPI.Applications.Commands
{
    public class CreateOrderCommand : IRequest<bool>
    {
        public int BuyerId { get; set; }
        public string BuyerName { get; set; }
        public string Adress { get; set; }
        public string CardNumber { get; set; }

        private readonly List<OrderItemDTO> orderItems;
        public IEnumerable<OrderItemDTO> OrderItems => orderItems; //{ get; set; } gibi ama orderItemsla set oluyor.

        public CreateOrderCommand()
        {
            orderItems = new List<OrderItemDTO>();
        }

        public CreateOrderCommand(List<BasketItem> basketItems, int customerId, string customerName,
            string adress, string cardNumber) : this() //this ile diger constructor calisti ve liste initlendi.
        {
            orderItems = ToOrderItemsDTO(basketItems).ToList();
            BuyerId = customerId;
            BuyerName = customerName;
            Adress = adress;
            CardNumber = cardNumber;
        }

        public record OrderItemDTO
        {
            public int ProductId { get; init; } //init immutable object icin constructor gibi calisir. 
            public string ProductName { get; init; }
            public decimal UnitPrice { get; init; }
            public int Quantity { get; init; }
        }
        public IEnumerable<OrderItemDTO> ToOrderItemsDTO(IEnumerable<BasketItem> basketItems)
        {
            foreach (var item in basketItems)
            {
                yield return ToOrderItemDTO(item);
            }
        }

        public OrderItemDTO ToOrderItemDTO(BasketItem item)
        {
            return new OrderItemDTO()
            {
                ProductId = item.ProductId,
                ProductName = item.ProductName,
                UnitPrice = item.UnitPrice,
                Quantity = item.Quantity
            };
        }
    }
}
