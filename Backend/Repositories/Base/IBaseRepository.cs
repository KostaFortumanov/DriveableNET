using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Base
{
    public interface IBaseRepository<T> where T : class
    {
       public Task<IEnumerable<T>> GetAll();
       public Task<T> GetById(Guid Id);
       public Task<T> Add(T entity);
       public T Update(T entity);
       public Task<T> Delete(Guid Id);
    }
}
