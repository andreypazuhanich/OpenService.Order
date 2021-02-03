using System;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OrderHandler;

namespace Order
{
    [TypeName("zomato")]
    public class ZomatoOrderHandler : IOrderHandler
    {
        public ZomatoOrderHandler()
        {
            
        }
        
        public async Task<Order> ProcessOrder(Order order)
        {
            var sourceOrderDeserialize = JsonConvert.DeserializeObject<SourceOrder>(order.SourceOrder);
            if (sourceOrderDeserialize.Products.Any())
            {
                foreach (var product in sourceOrderDeserialize.Products)
                {
                    if(product.Quantity == 0)
                        continue;
                    product.PaidPrice = product.PaidPrice / product.Quantity;
                }
            }
            order.ConvertedOrder = JsonConvert.SerializeObject(sourceOrderDeserialize);
            order.OrderStatus = OrderStatus.Processed;
            return order;
        }
    }
}