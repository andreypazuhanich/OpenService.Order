using System.Composition;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Internal;
using Newtonsoft.Json;
using OrderHandler;

namespace Order
{
    [TypeName("talabat")]
    public class TalabatOrderHandler : IOrderHandler
    {
        public TalabatOrderHandler()
        {
            
        }
        public async Task<Order> ProcessOrder(Order order)
        {
            var sourceOrderDeserialize = JsonConvert.DeserializeObject<SourceOrder>(order.SourceOrder);
            foreach (var product in sourceOrderDeserialize.Products)
            {
                product.PaidPrice *= -1;
            }
            
            order.ConvertedOrder = JsonConvert.SerializeObject(sourceOrderDeserialize);
            order.OrderStatus = OrderStatus.Processed;
            return order;
        }
    }
}