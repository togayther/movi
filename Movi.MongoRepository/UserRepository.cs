using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Movi.Common;
using Movi.IRepository;
using Movi.Model;

namespace Movi.MongoRepository
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        /// <summary>
        /// 检测用户登录
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public User CheckLogin(string username, string password)
        {
            if (String.IsNullOrEmpty(username) || String.IsNullOrEmpty(password))
            {
                return null;
            }
            var user = DbHelper.GetAll<User>().FirstOrDefault(p => p.Name.Equals(username));
            if (user == null)
            {
                return null;
            }
            var passwordAndSaltEncryptStr = CryptoHelper.MD5Encrypt(password + user.Salt);
            if (passwordAndSaltEncryptStr.Equals(user.Password))
            {
                user.Salt = StringHelper.GetGuidString();
                user.Password = CryptoHelper.MD5Encrypt(password + user.Salt);

                DbHelper.Update(user);

                return user;
            }
            return null;
        }
    }
}
