using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.OrderingAPI.Applications.Models;


namespace WebApplication.OrderingAPI.IntegrationEvents.Events
{
    public class UserCheckoutIntegrationEvent
    {
        public int BuyerId { get; set; }
        public string BuyerName { get; set; }
        public string Adress { get; set; }
        public string CardNumber { get; set; }
        public CustomerBasket Basket { get; set; }

    }
}
