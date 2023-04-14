using Ecommerce.BLL.Interfaces;
using Ecommerce.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.BL.Interfaces;

namespace Ecommerce.BLL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public IGenericRepo<Product> ProductRepo { get; }
        public IGenericRepo<Category> CategoryRepo { get; }

        public UnitOfWork(IGenericRepo<Product> productRepo , IGenericRepo<Category> categoryRepo)
        {
            ProductRepo = productRepo;
            CategoryRepo = categoryRepo;
        }

       
    }
}
