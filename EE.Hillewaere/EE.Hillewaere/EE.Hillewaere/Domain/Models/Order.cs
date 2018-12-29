using System;
using System.Collections.Generic;
using System.Text;

namespace EE.Hillewaere.Domain.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Amount { get; set; }
        public decimal PricePerProduct { get; set; }
    }
}
