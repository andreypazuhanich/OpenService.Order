using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ErrorHandler
{
    public class SubscriberService : IHostedService, IDisposable
    {
        private IConsumer _consumer;

        public SubscriberService(IConsumer consumer)
        {
            _consumer = consumer;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("Timed Background Service is starting.");

            await _consumer.ConsumeAsync();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("Timed Background Service is stopping.2");
            return Task.CompletedTask;
        }

        public void Dispose()
        {

        }
    }
}
