using System.Data.Entity.ModelConfiguration;
using Movi.Model;

namespace Movi.EFRepository.Mapping
{
    public class MediaCaptionMap : EntityTypeConfiguration<MediaCaption>
    {
        public MediaCaptionMap()
        {
            HasKey(c => c.Id).ToTable("MediaCaption");
            Property(c => c.Name).HasMaxLength(255);
            Property(c => c.Address).HasMaxLength(1024);

            HasRequired(c => c.Media)
                .WithMany(c => c.Captions)
                .HasForeignKey(c => c.MediaId);
        }
    }
}
