using Movi.Common;
using Movi.Model;

namespace Movi.EFRepository.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Movi.EFRepository.Framework.MoviDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Framework.MoviDbContext context)
        {
            //var salt = StringHelper.GetGuidString();
            //var superManager = new User
            //{
            //    Id = StringHelper.GetGuidString(),
            //    Avatar = "/Upload/Avatars/avatar.jpg",
            //    Name = "admin",
            //    Password = CryptoHelper.MD5Encrypt("admin" + salt),
            //    NickName = "一天到晚游泳的鱼",
            //    Email = "bigbrotherbiger@gmail.com",
            //    Phone = "13540312451",
            //    Qq = "353066897",
            //    AddTime = DateTime.Now,
            //    IsSuperUser = true,
            //    IsLock = false,
            //    Salt = salt
            //};
            //context.Users.Add(superManager);
        }
    }
}
