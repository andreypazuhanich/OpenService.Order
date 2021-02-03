using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Order.Client.Controllers
{
    [ApiController]
    [Route("api/order")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public OrderController(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("uber")]
        public async Task<IActionResult> CreateUberOrder([FromBody]OrderDto order)
        {
            var createdOrder = _mapper.Map<Models.Order>(order);
            createdOrder.SystemType = "uber";
            await _orderRepository.CreateOrder(createdOrder);
            return Ok();
        }
        
        [HttpPost]
        [Route("talabat")]
        public async Task<IActionResult> CreateTalabatOrder([FromBody]OrderDto order)
        {
            var createdOrder = _mapper.Map<Models.Order>(order);
            createdOrder.SystemType = "talabat";
            await _orderRepository.CreateOrder(createdOrder);
            return Ok();
        }
        
        [HttpPost]
        [Route("zomato")]
        public async Task<IActionResult> CreateZomatoOrder([FromBody]OrderDto order)
        {
            var createdOrder = _mapper.Map<Models.Order>(order);
            createdOrder.SystemType = "zomato";
            await _orderRepository.CreateOrder(createdOrder);
            return Ok();
        }
    }
}