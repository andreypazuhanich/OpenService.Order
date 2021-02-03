using System.Collections;
using System.Collections.Generic;
using Order.Models;

namespace Order.Client
{
    public class OrderDto
    {
        public long OrderNumber { get; set; }
        
        public ICollection<Product> Products { get; set; }
        
    }
}