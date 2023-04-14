using Ecommerce.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.BLL.Repositories.BaseSpecification
{
    public class ProductWithCategorySpecifications : BaseSpecification<Product>
    {
        public ProductWithCategorySpecifications()
        {
            AddInclude(P => P.Category);
        }

        public ProductWithCategorySpecifications(int? id) : base(P => P.Id == id)
        {
            AddInclude(P => P.Category);
        }
    }
}
