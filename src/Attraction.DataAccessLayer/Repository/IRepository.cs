using System.Collections.Generic;

namespace Attraction.DataAccessLayer.Repository
{
    public interface IRepository<T>
    {
        int Insert(T entity);
        int Update(T entity);
        int Delete(T entity);
        IEnumerable<T> GetAll();
        T GetById(int id);
    }
}
