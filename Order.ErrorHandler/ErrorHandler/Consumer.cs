using EasyNetQ;
using EasyNetQ.AutoSubscribe;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace ErrorHandler
{
    public class Consumer :  IConsumer
    {
        private readonly ILogger<string> _logger;
        private readonly IBus _bus;

        public Consumer(ILogger<string> logger, IBus bus)
        {
            _logger = logger;
            _bus = bus;
        }

        public async Task ConsumeAsync()
        {
            await _bus.PubSub.SubscribeAsync<string>("#", HandleErrorMessage);
        }

        private async Task HandleErrorMessage(string msg)
        {
            _logger.LogError(msg);
            Thread.Sleep(10000);
        }
    }
}
