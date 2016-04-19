using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Movi.EFRepository.Framework;
using Movi.IRepository;

namespace Movi.EFRepository
{
    public class BaseRepository<T>:IBaseRepository<T> where T : class
    {
        protected MoviDbContext DbContext = ContextFactory.GetCurrentContext();

        public bool Insert(T entity)
        {
            DbContext.Set<T>().Add(entity);
            return DbContext.SaveChanges()>0;
        }

        public bool InsertBatch(List<T> datas)
        {
            DbContext.Set<T>().AddRange(datas);
            return DbContext.SaveChanges() > 0;
        }

        public void Update(T entity)
        {
            DbContext.Set<T>().Attach(entity);
            DbContext.Entry<T>(entity).State = EntityState.Modified;
            DbContext.SaveChanges();
        }

        public void Delete(Expression<Func<T, bool>> expression)
        {
            var reDeleteds = DbContext.Set<T>().Where(expression);
            if (!reDeleteds.Any()) return;

            foreach (var reDeleted in reDeleteds)
            {
                DbContext.Entry<T>(reDeleted).State = EntityState.Deleted;
            }

            DbContext.SaveChanges();
        }

        public IQueryable<T> GetAll()
        {
            return DbContext.Set<T>().AsQueryable();
        }

        public T Find(Expression<Func<T, bool>> expression)
        {
            return DbContext.Set<T>().FirstOrDefault(expression);
        }
    }
}
