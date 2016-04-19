using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Movi.IService;
using Movi.Model;
using Movi.Model.Enums;
using Movi.Service;

namespace Movi.UnitTest.ServiceTest
{
    [TestClass]
    public class SiteConfigServiceTest
    {
        private readonly ISiteConfigService _configService = new SiteConfigService();

        [TestMethod]
        public void SaveConfigTest()
        {
            var saveConfig = new SiteConfig()
            {
                EmailSmtp = "163.smtp.com"
            };

            _configService.SaveConfig(saveConfig, Constant.SiteConfigPath);


            var loadConfig = _configService.LoadConfig(Constant.SiteConfigPath);


            Assert.AreEqual(saveConfig.ToString(),loadConfig.ToString());
        }
    }
}
