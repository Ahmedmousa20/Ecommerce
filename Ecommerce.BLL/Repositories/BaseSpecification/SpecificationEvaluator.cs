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


            //if (spec.OrderBy != null)
            //    query = query.OrderBy(spec.OrderBy);
            ////context.set<Product>().Where(p=>p.id==id).OrederBy(p=>p.Name)
            ////or --> context.set<Product>().OrederBy(p=>p.Name)

            //if (spec.OrderByDescending != null)
            //    query = query.OrderByDescending(spec.OrderByDescending);
            ////context.set<Product>().Where(p=>p.id==id).OrederByDesc(p=>p.Name)
            ////or --> context.set<Product>().OrederByDesc(p=>p.Name)
            if(spec.Includes !=null)
                query = spec.Includes.Aggregate(query, (currentQuery, include) => currentQuery.Include(include));



            return query;
        }
    }
}
