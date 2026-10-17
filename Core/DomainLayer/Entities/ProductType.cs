using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Entities
{
    public class ProductType : BaseEntity<int>
    {
        public string Name { get; set; } = string.Empty;
        public List<Product> Products { get; set; } = new List<Product>();

    }
}
