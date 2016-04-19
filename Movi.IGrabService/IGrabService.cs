using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Movi.Model;

namespace Movi.IGrabService
{
    public interface IGrabService<T> where T : class,new()
    {
        /// <summary>
        /// 采集数量
        /// </summary>
        int GrabCount { get; set; }

        /// <summary>
        /// 采集数据
        /// </summary>
        List<T> Grab();
    }
}
