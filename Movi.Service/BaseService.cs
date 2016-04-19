using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Movi.IRepository;
using Movi.IService;
using Movi.RepositoryFactary;

namespace Movi.Service
{
    public class BaseService<T>:IBaseService<T> where T : class
    {
        private readonly IBaseRepository<T> _repository = RepositoryAccess.CreateDataInstance<IBaseRepository<T>>();

        public bool InsertBatch(List<T> datas) 
        {
            return _repository.InsertBatch(datas);
        }

        public bool Insert(T entity)
        {
            return _repository.Insert(entity);
        }

        public void Update(T entity)
        {
            _repository.Update(entity);
        }

        public void Delete(Expression<Func<T, bool>> expression)
        {
            _repository.Delete(expression);
        }

        public IQueryable<T> GetAll()
        {
            return _repository.GetAll();
        }

        public T Find(Expression<Func<T, bool>> expression)
        {
            return _repository.Find(expression);
        }
    }
}
