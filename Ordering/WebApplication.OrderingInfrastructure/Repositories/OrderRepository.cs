using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using WebApplication.OrderingDomain.Models;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication.OrderingInfrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly OrderDbContext _context;

        public OrderRepository(OrderDbContext context)
        {
            _context = context;
        }

        public async Task<Order> AddOrder(Order order)
        {
            await _context.Orders.AddAsync(order);
            await SaveChangesDb();
            return order;

        }

        public async Task<Order> GetOrderByIdAsync(int orderId)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == orderId);

            await _context.Entry(order).Collection(i => i.OrderItems).LoadAsync();
            //await _context.Entry(order).Reference(i => i.OrderStatus).LoadAsync();

            return order;
        }
        public async Task<List<Order>> GetOrdersByUserIdAsync(int userId)
        {
            var orders = await _context.Orders.Where(o => o.BuyerId == userId).ToListAsync();

            foreach(var order in orders)
            {
                await _context.Entry(order).Collection(i => i.OrderItems).LoadAsync();
                //await _context.Entry(order).Reference(i => i.OrderStatus).LoadAsync();
            }

            return orders;
        }
        public async Task<bool> SaveChangesDb()
        {
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
