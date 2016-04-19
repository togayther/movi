using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Movi.IRepository;

namespace Movi.MongoRepository
{
    public class BaseRepository<T>:IBaseRepository<T> where T : class
    {
        protected readonly DbHelper DbHelper = new DbHelper(typeof(T).Name);

        public bool Insert(T entity)
        {
            return DbHelper.Insert(entity);
        }

        public bool InsertBatch(List<T> datas) 
        {
            return DbHelper.InsertBatch<T>(datas);
        }

        public void Update(T entity)
        {
            DbHelper.Update(entity);
        }

        public void Delete(Expression<Func<T, bool>> expression)
        {
            DbHelper.Delete(expression);
        }

        public IQueryable<T> GetAll()
        {
            return DbHelper.GetAll<T>();
        }

        public T Find(Expression<Func<T, bool>> expression)
        {
            return DbHelper.GetById(expression);
        }
    }
}
