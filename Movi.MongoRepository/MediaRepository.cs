using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Movi.IRepository;
using Movi.Model;

namespace Movi.MongoRepository
{
    public class MediaRepository : BaseRepository<Media>, IMediaRepository
    {
       
    }
}
