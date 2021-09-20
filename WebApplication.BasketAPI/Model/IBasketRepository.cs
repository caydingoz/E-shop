using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.BasketAPI.Model
{
    public interface IBasketRepository 
    {
        Task<CustomerBasket> GetBasketAsync(int customerId);
        Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basket);
        Task<bool> DeleteBasketAsync(int customerId);
        IEnumerable<string> GetUsers();
    }
}
