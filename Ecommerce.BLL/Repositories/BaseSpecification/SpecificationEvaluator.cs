using Ecommerce.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.BLL.Repositories
{
    public class SpecificationEvaluator<TEntity> where TEntity:BaseEntity
    {
 
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery , ISpecification<TEntity> spec)
        {
            var query = inputQuery; //context.set<Product>()
            if (spec.Criteria != null)
                query = query.Where(spec.Criteria);  //context.set<Product>().Where(p=>p.id==id)

            if(spec.Includes !=null)
                query = spec.Includes.Aggregate(query, (currentQuery, include) => currentQuery.Include(include));



            return query;
        }
    }
}
