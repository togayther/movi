using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Movi.IRepository;
using Movi.IService;
using Movi.Model;
using Movi.RepositoryFactary;

namespace Movi.Service
{
    public class UserService : BaseService<User>, IUserService
    {
        private readonly IUserRepository _userRepository = RepositoryAccess.CreateDataInstance<IUserRepository>();

        /// <summary>
        /// 检测用户登录
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public User CheckLogin(string userName, string pwd)
        {
            if (String.IsNullOrEmpty(userName) || String.IsNullOrEmpty(pwd))
            {
                return null;
            }
            return _userRepository.CheckLogin(userName, pwd);
        }
    }
}
