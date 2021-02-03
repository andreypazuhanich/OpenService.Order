using System.Collections.Generic;

namespace OrderHandler
{
    public class SourceOrder
    {
        public long Id { get; set; }
        
        public long OrderNumber { get; set; }
        
        public ICollection<Product> Products { get; set; }
    }
}