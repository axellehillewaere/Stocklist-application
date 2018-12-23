using System;
using System.Collections.Generic;
using System.Text;

namespace EE.Hillewaere.Domain.Models
{
    public class SubCategory
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<Product> Products { get; set; }
        public Category Category { get; set; }
    }
}
