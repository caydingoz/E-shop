using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication.OrderingDomain.Models
{
    public class OrderStatus
    {
        [Key]
        public int Id { get; set; }
        public string State { get; set; }
        
        public static OrderStatus Submitted = new OrderStatus(nameof(Submitted).ToLowerInvariant());
        public static OrderStatus Shipped = new OrderStatus(nameof(Shipped).ToLowerInvariant());
        public static OrderStatus Cancelled = new OrderStatus(nameof(Cancelled).ToLowerInvariant());

        public OrderStatus(string state)
        {
            State = state;
        }
    }
}
