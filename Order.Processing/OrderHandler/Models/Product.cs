using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderHandler
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Comment { get; set; }
        public int Quantity { get; set; }
        public decimal PaidPrice { get; set; }
        public decimal UnitPrice { get; set; }
        public string RemoteCode { get; set; }
        public string Description { get; set; }
        public int VatPercentage { get; set; }
        public decimal DiscountAmount { get; set; }
    }
}
