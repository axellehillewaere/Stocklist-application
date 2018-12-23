using System;
using System.Collections.Generic;
using System.Text;

namespace EE.Hillewaere.Domain.Models
{
    public class Category
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<SubCategory> SubCategories { get; set; }
    }
}
