using System.Data.Entity.ModelConfiguration;
using Movi.Model;

namespace Movi.EFRepository.Mapping
{
    public class MediaCommentMap : EntityTypeConfiguration<MediaComment>
    {
        public MediaCommentMap()
        {
            HasKey(c => c.Id).ToTable("MediaComment");
            Property(c => c.Content).HasMaxLength(1024);
            Property(c => c.UserEmail).HasMaxLength(64);
            Property(c => c.UserIp).HasMaxLength(32);
            Property(c => c.AddTime).HasColumnType("datetime");

            HasRequired(c => c.Media)
                .WithMany(c => c.Comments)
                .HasForeignKey(c => c.MediaId);
        }
    }
}
