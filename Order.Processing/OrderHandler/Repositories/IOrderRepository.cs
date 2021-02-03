using System.Collections.Generic;
using System.Threading.Tasks;

namespace Order
{
    public interface IOrderRepository
    {
        Task<List<Order>> GetUnprocessedOrdersAsync();

        void UpdateOrder(Order order);
    }
}