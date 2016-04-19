using System.Data.Entity;
using System.Runtime.Remoting.Messaging;
namespace Movi.EFRepository.Framework
{
    /// <summary>
    /// 数据库操作上下文
    /// <remarks>
    /// </remarks>
    /// </summary>
    public class ContextFactory
    {
        /// <summary>
        /// 获取数据库操作上下文
        /// </summary>
        /// <returns></returns>
        public static MoviDbContext GetCurrentContext()
        {
            var dbContext = CallContext.GetData("MoviContext") as MoviDbContext;
            if (dbContext == null)
            {
                dbContext = new MoviDbContext();
                CallContext.SetData("MoviContext", dbContext);
            }
            return dbContext;
        }
    }
}
