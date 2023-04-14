using Ecommerce.BLL.Repositories;
using Ecommerce.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Talabat.BL.Interfaces
{
    public interface IGenericRepo<T> where T : BaseEntity
    {
        Task<T> GetByIdAsync(int? id);
        Task<IReadOnlyList<T>> GeAllAsync();
        Task<T> GetByIdWithSpecAsync(ISpecification<T> spec);
        Task<IReadOnlyList<T>> GeAllWithSpecAsync(ISpecification<T> spec);
        Task<int> Add(T Item);
        Task<int> Update(T T);
        Task<int> Delete(T T);
        Task<IEnumerable<T>> SearchByName(string name);


    }
}
