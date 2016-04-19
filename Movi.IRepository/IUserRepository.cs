using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Movi.Model;

namespace Movi.IRepository
{
    public interface IUserRepository : IBaseRepository<User>
    {
        User CheckLogin(string userName, string pwd);
    }
}
