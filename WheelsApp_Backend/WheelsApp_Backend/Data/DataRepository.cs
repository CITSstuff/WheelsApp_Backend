using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WheelsApp_Backend.Models;

namespace WheelsApp_Backend.Data
{
    public class DataRepository<T> : IDataRepository<T> where T : class
    {

        DbContext _db;
        private DbSet<T> dbSet;

        public DataRepository()
        {
            this._db = new WheelsContext();
            dbSet = _db.Set<T>();
        }

        IEnumerable<T> IDataRepository<T>.GetAll()
        {
            return dbSet.ToList();
        }

        public T Get(object id)
        {
            return dbSet.Find(id);
        }

        public T Add(T obj)
        {
            dbSet.Add(obj);
            Save();
            return obj;
        }

        public T Update(T obj)
        {
            dbSet.Attach(obj);
            _db.Entry(obj).State = EntityState.Modified;
            Save();
            return obj;
        }

        public void Delete(object id)
        {
            T existing = dbSet.Find(long.Parse(id.ToString()));
            if (existing != null) { dbSet.Remove(existing); }
            Save();
        }
        public void Save()
        {
            try
            {
                _db.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException.Message);
            }
        }
    }
}
