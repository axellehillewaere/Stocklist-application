using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace EE.Hillewaere.Domain.Models
{
    public class OrderList
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        [XmlIgnore]
        public ICollection<Order> Orders { get; set; }

        public string OrdersAsString
        {
            get
            {
                return Orders?.ToString();
            }
            set
            {
                Orders?.ToString();
            }
        }
    }
}
