using System;
using System.Composition;
using System.Threading.Tasks;

namespace Order
{
    [TypeName("uber")]
    public class UberOrderHandler : IOrderHandler
    {
        public UberOrderHandler()
        {
            
        }
        public async Task<Order> ProcessOrder(Order order)
        {
            throw new InvalidCastException("Не удалось обработать заказ");
        }
    }
}