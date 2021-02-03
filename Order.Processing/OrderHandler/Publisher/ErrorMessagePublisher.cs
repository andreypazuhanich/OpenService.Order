using System.ComponentModel;
using System.Threading.Tasks;
using EasyNetQ;

namespace OrderHandler
{
    public class ErrorMessagePublisher : IErrorMessagePublisher
    {
        private readonly IBus _bus;

        public ErrorMessagePublisher(IBus bus)
        {
            _bus = bus;
        }
        public async Task Publish(string error)
        { 
            await _bus.PubSub.PublishAsync<string>(error);
        }
    }
}