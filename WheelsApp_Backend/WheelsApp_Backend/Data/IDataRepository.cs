using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WheelsApp_Backend.Data
{
    public interface IDataRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T Get(object id);
        T Add(T obj);
        T Update(T obj);
        void Delete(object id);
        void Save();
    }
}
