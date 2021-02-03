using System;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using FluentAssertions;
using Newtonsoft.Json;
using OrderHandler;
using Xunit;
using Xunit.Abstractions;

namespace Order.Handler.Test
{
    public class ProcessOrderTest
    {
        [Fact]
        public void UberOrderHandlerTest_Throw()
        {
            var order = new Order()
            {
                Id = 1,
                OrderNumber = 123123,
                OrderStatus = OrderStatus.Unprocessed,
            };
            IOrderHandler orderHandler = new UberOrderHandler();
            Func<Task> act = async () => await orderHandler.ProcessOrder(order);
            act.Should().ThrowExactly<InvalidCastException>().WithMessage("Не удалось обработать заказ");
        }

        [Theory]
        [InlineData(2, 10.0)]
        [InlineData(5, 15.0)]
        public async Task ZomatoOrderHandlerTest_Success(int quantity, decimal paidPrice)
        {
            var order = new Order()
            {
                OrderStatus = OrderStatus.Unprocessed,
                SourceOrder =
                    @"{
                'id': 0,
                'orderNumber': 1,
                'products': [
                {" + $@"
                'id': 1,
                'quantity': {quantity},
                'paidPrice': {paidPrice},
                    "
                   + @"}
                    ]
            }"
            };

            IOrderHandler orderHandler = new ZomatoOrderHandler();
            var processedOrder = await orderHandler.ProcessOrder(order);
            var s = JsonConvert.DeserializeObject<SourceOrder>(order.SourceOrder);
            var s1 = JsonConvert.DeserializeObject<SourceOrder>(processedOrder.ConvertedOrder);
            foreach (var product in s.Products)
            {
                var convertedProduct = s1.Products.FirstOrDefault(s => product.Id == s.Id);
                convertedProduct.Should().NotBeNull();
                convertedProduct.PaidPrice.Should().Be(product.PaidPrice / product.Quantity);
            }

            processedOrder.OrderStatus.Should().Be(OrderStatus.Processed);
        }
        
        [Theory]
        [InlineData(2)]
        [InlineData(12183)]
        public async Task TalabatOrderHandlerTest_Success(decimal paidPrice)
        {
            var order = new Order()
            {
                OrderStatus = OrderStatus.Unprocessed,
                SourceOrder =
                    @"{
                'id': 0,
                'orderNumber': 1,
                'products': [
                {" + $@"
                'id': 1,
                'paidPrice': {paidPrice},
                    "
                   + @"}
                    ]
            }"
            };

            IOrderHandler orderHandler = new TalabatOrderHandler();
            var processedOrder = await orderHandler.ProcessOrder(order);
            var s = JsonConvert.DeserializeObject<SourceOrder>(order.SourceOrder);
            var s1 = JsonConvert.DeserializeObject<SourceOrder>(processedOrder.ConvertedOrder);
            foreach (var product in s.Products)
            {
                var convertedProduct = s1.Products.FirstOrDefault(s => product.Id == s.Id);
                convertedProduct.Should().NotBeNull();
                convertedProduct.PaidPrice.Should().Be(product.PaidPrice * -1);
            }

            processedOrder.OrderStatus.Should().Be(OrderStatus.Processed);
        }
    }
}