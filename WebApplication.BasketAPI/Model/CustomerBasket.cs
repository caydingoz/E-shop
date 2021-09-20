using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.BasketAPI.Model
{
    public class CustomerBasket
    {
        public int BuyerId { get; set; }
        public List<BasketItem> Items { get; set; } = new List<BasketItem>();
        public decimal TotalPrice
        {
            get
            {
                decimal totalprice = 0;
                foreach (var item in Items)
                {
                    totalprice += item.UnitPrice * item.Quantity;
                }
                return totalprice;
            }
        }
        public CustomerBasket() { }
        public CustomerBasket(int customerId)
        {
            BuyerId = customerId;
        }
    }
}
