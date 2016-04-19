using Movi.Common;
using Movi.IService;
using Movi.Model;

namespace Movi.Service
{
    public class SiteConfigService : ISiteConfigService
    {
        private static readonly object LockHelper = new object();
        public void SaveConfig(SiteConfig config, string configPath)
        {
            lock (LockHelper)
            {
                SerializeHelper.Serialize(config, configPath);
            }
        }

        public SiteConfig LoadConfig(string configPath)
        {
            return (SiteConfig)SerializeHelper.DeSerialize(typeof(SiteConfig), configPath);
        }
    }
}
