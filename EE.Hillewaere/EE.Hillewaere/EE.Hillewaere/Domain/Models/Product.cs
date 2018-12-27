using System;
using System.Collections.Generic;
using System.Text;

namespace EE.Hillewaere.Domain.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public SubCategory SubCategory { get; set; }
        public Category Category { get; set; }
    }
}
