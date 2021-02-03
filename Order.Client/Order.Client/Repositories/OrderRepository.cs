using System;
using System.Linq;
using System.Threading.Tasks;
using DAL.EntityFramework;

namespace Order.Client
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _dbContext;

        public OrderRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateOrder(Models.Order order)
        {
            try
            {

                if (_dbContext.Orders.FirstOrDefault(s => s.Id == order.Id) == null)
                {
                    await _dbContext.Orders.AddAsync(order);
                }

                await _dbContext.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                await Task.FromException(ex);
            }
        }
    }
}