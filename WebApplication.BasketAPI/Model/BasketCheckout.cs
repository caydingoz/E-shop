using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.BasketAPI.Model
{
    public class BasketCheckout
    {
        public string CustomerName { get; set; }
        public string Adress { get; set; }
        public string CardNumber { get; set; }
    }
}
