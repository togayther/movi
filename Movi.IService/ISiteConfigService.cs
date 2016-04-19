using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Movi.Model;

namespace Movi.IService
{
    public interface ISiteConfigService
    {
        void SaveConfig(SiteConfig config, string configPath);

        SiteConfig LoadConfig(string configPath);
    }
}
