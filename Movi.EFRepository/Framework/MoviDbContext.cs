using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Movi.EFRepository.Mapping;
using Movi.Model;

namespace Movi.EFRepository.Framework
{
    public class MoviDbContext : DbContext
    {
        public MoviDbContext()
            : base("MoviEfConnection")
        {
        }

        public DbSet<Media> Movies { get; set; }

        public DbSet<Visitor> Visitors { get; set; }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            #region Register map
            modelBuilder.Configurations.Add(new MediaMap());
            modelBuilder.Configurations.Add(new MediaResourceMap());
            modelBuilder.Configurations.Add(new MediaCaptionMap());
            modelBuilder.Configurations.Add(new MediaCommentMap());

            modelBuilder.Configurations.Add(new VisitorMap());
            modelBuilder.Configurations.Add(new UserMap());

            #endregion
        }
    }
}
