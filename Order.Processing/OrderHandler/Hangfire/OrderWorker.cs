using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OrderHandler;

namespace Order.Hangfire
{
    public class OrderWorker : IWorker
    {
        private readonly AppDbContext _dbContext;
        private readonly IOrderRepository _orderRepository;
        private readonly ILogicCreator _logicCreator;
        private readonly IErrorMessagePublisher _errorMessagePublisher;

        public OrderWorker(AppDbContext dbContext, IOrderRepository orderRepository, ILogicCreator logicCreator, IErrorMessagePublisher errorMessagePublisher)
        {
            _dbContext = dbContext;
            _orderRepository = orderRepository;
            _logicCreator = logicCreator;
            _errorMessagePublisher = errorMessagePublisher;
        }
        public async Task Run()
        {
            try
            {
                var orders = await _orderRepository.GetUnprocessedOrdersAsync();
                foreach (var order in orders)
                {
                    var processedOrder = await _logicCreator.CreateLogic(order).ProcessOrder(order);
                    _orderRepository.UpdateOrder(processedOrder);
                }
            
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _errorMessagePublisher.Publish(ex.Message);
            }
        }
    }
}