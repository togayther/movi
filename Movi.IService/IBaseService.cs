using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Movi.IService
{
    public interface IBaseService<T> where T : class
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity">数据实体</param>
        /// <returns>添加后的数据实体</returns>
        bool Insert(T entity);

        /// <summary>
        /// 批量添加
        /// </summary>
        /// <param name="datas"></param>
        /// <returns></returns>
        bool InsertBatch(List<T> datas);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity">数据实体</param>
        /// <returns>是否成功</returns>
        void Update(T entity);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="expression">删除条件</param>
        /// <returns>是否成功</returns>
        void Delete(Expression<Func<T, bool>> expression);

        /// <summary>
        /// 获取所有
        /// </summary>
        /// <returns></returns>
        IQueryable<T> GetAll();

        /// <summary>
        /// 查找数据
        /// </summary>
        /// <param name="expression">查询条件</param>
        /// <returns>实体</returns>
        T Find(Expression<Func<T, bool>> expression);
    }
}
