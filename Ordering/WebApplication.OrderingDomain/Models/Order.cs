using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication.OrderingDomain.Models
{
    public class Order
    {
        [Key]
        public int Id { get; private set; }
        public int BuyerId { get; set; }
        public string BuyerName { get; set; }
        public string Adress { get; set; }
        public List<OrderItem> OrderItems { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public string CardNumber { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal TotalPrice { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public Order(int buyerId, string buyerName, string adress, string cardNumber )
        {
            BuyerId = buyerId;
            BuyerName = buyerName;
            Adress = adress;
            OrderItems = new List<OrderItem>();
            OrderStatus = new OrderStatus("Submitted");
            CardNumber = cardNumber;
            TotalPrice = 0;
            CreatedDate = DateTime.Now.AddHours(3);
            LastModifiedDate = CreatedDate;
        }

        //Gelen basketteki item listesini ordera atmak icin
        public void AddOrderItem(int productId, string productName, decimal unitPrice, int quantity = 1) 
        {
            var existingOrderForProduct = OrderItems.Where(o => o.ProductId == productId).SingleOrDefault();

            if (existingOrderForProduct != null)
            {
                existingOrderForProduct.AddUnits(quantity); //duplicate ise quantity artirioz.
            }
            else
            {
                var orderItem = new OrderItem(productId, productName, unitPrice, quantity);
                OrderItems.Add(orderItem);
            }
        }
        public decimal GetTotal()
        {
            return OrderItems.Sum(o => o.Quantity * o.UnitPrice);
        }
        public bool SetCancelledStatus()
        {
            Console.WriteLine(OrderStatus.Id);
            if(OrderStatus.State == OrderStatus.Cancelled.State) return false;
            OrderStatus.State= OrderStatus.Cancelled.State;
            return true;
        }
    }
}
