using System.Data.Entity.ModelConfiguration;
using Movi.Model;

namespace Movi.EFRepository.Mapping
{
    public class MediaResourceMap : EntityTypeConfiguration<MediaResource>
    {
        public MediaResourceMap()
        {
            HasKey(c => c.Id).ToTable("MediaResource");
            Property(c => c.Name).HasMaxLength(255);
            Property(c => c.Address).HasMaxLength(1024);
            Property(c => c.Quality).HasMaxLength(32);
            Property(c=>c.Size).HasMaxLength(32);

            HasRequired(c => c.Media)
                .WithMany(c => c.Resources)
                .HasForeignKey(c => c.MediaId);
        }
    }
}
