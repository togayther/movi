using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Movi.IRepository;
using Movi.Model;

namespace Movi.EFRepository
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public User CheckLogin(string userName, string pwd)
        {
            throw new NotImplementedException();
        }
    }
}
