using System.Threading.Tasks;

namespace Order
{
    public interface ILogicCreator
    {
         IOrderHandler CreateLogic(Order order);
    }
}