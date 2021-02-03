using System;
using System.ComponentModel.DataAnnotations;

namespace Order.Models
{
    public class Order : BaseEntity
    {
        public string SystemType { get; set; }

        public long OrderNumber { get; set; }

        public string SourceOrder { get; set; }

        public string ConvertedOrder { get; set; }
        
        public OrderStatus OrderStatus { get; set; }
    }
}