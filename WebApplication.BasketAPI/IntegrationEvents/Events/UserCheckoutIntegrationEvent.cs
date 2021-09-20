using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.BasketAPI.Model;

namespace WebApplication.BasketAPI.IntegrationEvents.Events
{
    public class UserCheckoutIntegrationEvent
    {
        public int BuyerId { get; private set; }
        public string BuyerName { get; private set; }
        public string Adress { get; private set; }
        public string CardNumber { get; private set; }
        public CustomerBasket Basket { get; private set; }

        public UserCheckoutIntegrationEvent(int userId, string userName, string adress, 
            string cardNumber ,CustomerBasket basket)
        {
            BuyerId = userId;
            BuyerName = userName;
            Adress = adress;
            CardNumber = cardNumber;
            Basket = basket;
        }
    }
}
