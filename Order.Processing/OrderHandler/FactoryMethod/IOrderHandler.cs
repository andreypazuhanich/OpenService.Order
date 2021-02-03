using System.Threading.Tasks;

namespace Order
{
    public interface IOrderHandler
    {
        Task<Order> ProcessOrder(Order order);
    }
}