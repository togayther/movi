using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Movi.IService;
using Movi.Model;
using Movi.Service;

namespace Movi.UnitTest.ServiceTest
{
    [TestClass]
    public class MovieServiceTest
    {
        private readonly IMediaService _mediaService = new MediaService();

        [TestMethod]
        public void MovieTest()
        {
            var datas = _mediaService.GetAll();

            Assert.AreEqual(datas.Count(),40);
        }
    }
}
