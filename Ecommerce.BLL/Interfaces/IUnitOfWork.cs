using Ecommerce.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.BL.Interfaces;

namespace Ecommerce.BLL.Interfaces
{
    public interface IUnitOfWork
    {
        public IGenericRepo<Product> ProductRepo { get; }
        public IGenericRepo<Category> CategoryRepo { get; }
    }
}
