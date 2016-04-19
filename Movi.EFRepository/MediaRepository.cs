using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Movi.IRepository;
using Movi.Model;

namespace Movi.EFRepository
{
    public class MediaRepository : BaseRepository<Media>, IMediaRepository
    {
        public void DeleteBatch(Expression<Func<Media, bool>> expression)
        {
            throw new NotImplementedException();
        }
    }
}
