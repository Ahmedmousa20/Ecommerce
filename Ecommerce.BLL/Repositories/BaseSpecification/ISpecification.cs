﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.BLL.Repositories
{
    public interface ISpecification<T>
    {
        
        public Expression<Func<T , bool>> Criteria { get; set; }
        public List<Expression<Func<T , object>>> Includes { get; set; }
        //public Expression<Func<T , object>> OrderBy { get; set; }
        //public Expression<Func<T , object>> OrderByDescending { get; set; }

    }
}
