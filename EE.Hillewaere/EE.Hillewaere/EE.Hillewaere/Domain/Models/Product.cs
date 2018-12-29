using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace EE.Hillewaere.Domain.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public int Amount { get; set; }
        [XmlIgnore]
        public SubCategory SubCategory { get; set; }
        [XmlIgnore]
        public Category Category { get; set; }

        public string SubCategoryAsString
        {
            get
            {
                return SubCategory?.ToString();
            }
            set
            {
                SubCategory?.ToString();
            }
        }

        public string CategoryAsString
        {
            get
            {
                return Category?.ToString();
            }
            set
            {
                Category?.ToString();
            }
        }
    }
}
