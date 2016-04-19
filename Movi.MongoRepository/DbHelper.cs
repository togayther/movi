using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Linq.Expressions;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;

namespace Movi.MongoRepository
{
    /// <summary>
    /// Mongo数据库操作
    /// </summary>
    public class DbHelper
    {
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["MoviMongoConnection"].ConnectionString;

        private string _useCollectionName ;
        private readonly MongoDatabase _mongoDatabase;
        public DbHelper(string collectionName)
        {
            if (String.IsNullOrEmpty(collectionName) 
                || String.IsNullOrEmpty(_connectionString))
            {
                throw new ArgumentException("mongo connectionString is null or empty");
            }
            var dbName = _connectionString.Substring(_connectionString.LastIndexOf('/')+1);
            var mongoClient = new MongoClient(_connectionString);
            var mongoServer = mongoClient.GetServer();
            _useCollectionName = collectionName;
            _mongoDatabase = mongoServer.GetDatabase(dbName);
        }

        public void UseCollection(string collectionName)
        {
            if (string.IsNullOrEmpty(collectionName))
                throw new Exception("collectionName can't empty or null.");
            _useCollectionName = collectionName;
        }

        private MongoCollection<T> GetCollection<T>()
        {
            return _mongoDatabase.GetCollection<T>(_useCollectionName);
        }

        /// <summary>
        /// 获取所有
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IQueryable<T> GetAll<T>() where T : class
        {
            var collection = GetCollection<T>();

            return collection.AsQueryable<T>();
        }

        /// <summary>
        /// 根据编号获取
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        public T GetById<T>(Expression<Func<T, bool>> expression) where T : class
        {
            var query = Query<T>.Where(expression);
            var document = GetCollection<T>().FindOne(query);
            return document;
        }

        /// <summary>
        /// 插入记录
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Insert<T>(T entity) where T : class
        {
            return GetCollection<T>().Insert(entity).Ok;
        }

        /// <summary>
        /// 批量插入数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="datas"></param>
        /// <returns></returns>
        public bool InsertBatch<T>(List<T> datas) where T : class
        {
            var result = GetCollection<T>().InsertBatch(datas);
            return result.Count() == datas.Count;
        }

        /// <summary>
        /// 更新记录
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Update<T>(T entity) where T : class
        {
            return GetCollection<T>().Save(entity).Ok;
        }

        /// <summary>
        /// 删除记录
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        public bool Delete<T>(Expression<Func<T, bool>> expression) where T : class
        {
            var query = Query<T>.Where(expression);
            return GetCollection<T>().Remove(query).Ok;
        }
    }
}
