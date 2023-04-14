using Ecommerce.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.DTOs
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? CategoryId { get; set; }
        public virtual Category ParentCategory { get; set; }

        public virtual ICollection<Product> MyProperty { set; get; } = new HashSet<Product>();
    }
}
