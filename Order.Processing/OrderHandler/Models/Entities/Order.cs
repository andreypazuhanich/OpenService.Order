using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Order
{
    public class Order
    {
        [Key]
        public long Id { get; set; }

        public string SystemType { get; set; }

        public long OrderNumber { get; set; }
        
        public string SourceOrder { get; set; }

        public string ConvertedOrder
        { get; set; }

        public DateTime CreatedAt { get; set; }
        public OrderStatus OrderStatus { get; set; }
    }
}