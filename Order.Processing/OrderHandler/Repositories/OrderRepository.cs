using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Order
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _dbContext;

        public OrderRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public async Task<List<Order>> GetUnprocessedOrdersAsync() => await _dbContext.Orders.Where(s => s.OrderStatus == OrderStatus.Unprocessed).ToListAsync();

        public void UpdateOrder(Order order)
        {
             _dbContext.Orders.Update(order);
        }
    }
}