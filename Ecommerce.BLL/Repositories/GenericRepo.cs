using Ecommerce.DAL.Contexts;
using Ecommerce.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.BL.Interfaces;

namespace Ecommerce.BLL.Repositories
{
    public class GenericRepo<T> : IGenericRepo<T> where T : BaseEntity
    {
        private readonly StoreContext context;

        public GenericRepo(StoreContext context)
        {
            this.context = context;
        }

        public async Task<IReadOnlyList<T>> GeAllAsync()
         => await context.Set<T>().ToListAsync();

        public async Task<T> GetByIdAsync(int? id)
            => await context.Set<T>().FindAsync(id);

        public IQueryable<T> ApplySpecifications(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(context.Set<T>(), spec);
        }

        public async Task<T> GetByIdWithSpecAsync(ISpecification<T> spec)
        {
            return await ApplySpecifications(spec).FirstOrDefaultAsync();
            //context.set<Product>.Where(p=>p.id==id).Include(P=>P.productBrand).Include(P=>P.ProductType).FirstOrDefaultAsync();
        }

        public async Task<IReadOnlyList<T>> GeAllWithSpecAsync(ISpecification<T> spec)
        {
            return await ApplySpecifications(spec).ToListAsync();
        }

        public async Task<int> Add(T Item)
        {
            context.Set<T>().Add(Item);
            return await context.SaveChangesAsync();
        }

        public async Task<int> Delete(T Item)
        {
            context.Set<T>().Remove(Item);
            return await context.SaveChangesAsync();
        }

        public async Task<int> Update(T Item)
        {
            context.Set<T>().Update(Item);
            return await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> SearchByName(string name)
        {
            if (typeof(T) == typeof(Product))
            {
                var data = (IEnumerable<T>)await context.Products.Where(a => a.Name.Contains(name)).ToListAsync();
                return data;

            }
            else
            {
                var data = (IEnumerable<T>)await context.Categories.Where(a => a.Name.Contains(name)).ToListAsync();
                return data;
            }


        }
    }
}
