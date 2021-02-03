using System;
using System.ComponentModel.DataAnnotations;

namespace Order.Models
{
    public class BaseEntity
    {
        [Key]
        public long Id { get; set; }
        
        public DateTime CreatedAt { get; set; }
    }
}