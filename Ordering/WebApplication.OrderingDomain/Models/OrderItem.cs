using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication.OrderingDomain.Models
{
    public class OrderItem
    {
        [Key]
        public int Id { get; private set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public OrderItem(int productId, string productName, decimal unitPrice, int quantity = 1)
        {
            ProductId = productId;
            ProductName = productName;
            UnitPrice = unitPrice;
            Quantity = quantity;
        }
        public void AddUnits(int units)
        {
            if (units < 0)
            {
                throw new Exception("Invalid units");
            }

            Quantity += units;
        }
    }
}
