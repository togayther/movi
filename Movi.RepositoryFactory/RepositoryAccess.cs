using System;
using System.Collections.Generic;
using System.Reflection;
using Movi.Common;

namespace Movi.RepositoryFactary
{
    /// <summary>
    /// 数据工厂
    /// </summary>
    public static class RepositoryAccess
    {
        private static readonly string AssemblyPath;
        private static readonly Dictionary<RuntimeTypeHandle, String> AssemblyClass;
        static RepositoryAccess()
        {
            AssemblyPath = ConfigHelper.GetConfigString("StoreProvider");
            if (String.IsNullOrEmpty(AssemblyPath))
            {
                throw new Exception("获取数据提供程序失败。请检查web.config文件中是否已配置数据提供程序！");
            }

            var types = Assembly.Load(AssemblyPath).GetExportedTypes();
            AssemblyClass = new Dictionary<RuntimeTypeHandle, string>();
            foreach (var type in types)
            {
                AssemblyClass.Add(type.TypeHandle, type.Name);
            }
        }

        private static string GetDataProviderClass(Type interfaceType)
        {
            var dataProviderClass = String.Empty;
            foreach (var item in AssemblyClass)
            {
                var type = Type.GetTypeFromHandle(item.Key);
                if (interfaceType.IsAssignableFrom(type))
                {
                    dataProviderClass = item.Value;
                }
            }
            return dataProviderClass;
        }

        /// <summary>
        /// 获取数据提供程序
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="ArgumentException">获取数据提供程序失败</exception>
        public static T CreateDataInstance<T>()
        {
            var className = GetDataProviderClass(typeof(T));
            if (!String.IsNullOrEmpty(className))
            {
                var assembly = Assembly.Load(AssemblyPath);
                var obj = assembly.CreateInstance(AssemblyPath + "." + className);
                return (T)obj;
            }
            throw new ArgumentException("在指定的数据提供程序中未查询到：" + typeof(T));
        }
    }
}
