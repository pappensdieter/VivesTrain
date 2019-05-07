using System;
using System.Collections.Generic;
using System.Text;

namespace VivesTrein.Storage.Interfaces
{
    public interface IDAO<T> where T : class
    {
        IEnumerable<T> GetAll();
        T FindById(int? Id);
        void Create(T entity);
        void Delete(T entity);
        void Update(T entity);
    }
}
