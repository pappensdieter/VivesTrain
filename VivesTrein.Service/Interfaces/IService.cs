using System;
using System.Collections.Generic;
using System.Text;

namespace VivesTrein.Service.Interfaces
{
    public interface IService<T> where T : class
    {
        IEnumerable<T> GetAll();
        T FindById(int? Id);
        void Create(T entity);
        void Delete(T entity);
        void Update(T entity);
    }
}
