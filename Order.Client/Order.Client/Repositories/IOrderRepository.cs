using System.Threading.Tasks;

namespace Order.Client
{
    public interface IOrderRepository
    {
        Task CreateOrder(Models.Order order);
    }
}