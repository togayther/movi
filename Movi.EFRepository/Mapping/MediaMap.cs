using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Movi.Model;

namespace Movi.EFRepository.Mapping
{
    public class MediaMap : EntityTypeConfiguration<Media>
    {
        public MediaMap()
        {
            HasKey(c => c.Id).ToTable("Movie");

            Property(c => c.Name).HasMaxLength(255);
            Property(c => c.AnotherName).HasMaxLength(255);
            Property(c => c.Summary).HasMaxLength(2048);
            Property(c => c.Area).HasMaxLength(32);
            Property(c => c.Cover).HasMaxLength(128);
            Property(c => c.LocalCover).HasMaxLength(128);
            Property(c => c.Mins).HasColumnType("int");
            Property(c => c.Language).HasMaxLength(32);
            Property(c => c.Director).HasMaxLength(32);
            Property(c => c.Year).HasColumnType("int");
            Property(c => c.AddTime).HasColumnType("datetime");
            Property(c => c.Rank).HasColumnType("float");
            Property(c => c.ViewCount).HasColumnType("int");
            Property(c => c.OuterLink).HasMaxLength(128);
            Property(c => c.CategoriesSerialized).HasMaxLength(255);
            Property(c => c.ActorsSerialized).HasMaxLength(255);

            Ignore(c => c.Categories);
            Ignore(c => c.Actors);

            HasMany(c => c.Captions)
                .WithRequired(c => c.Media)
                .HasForeignKey(c => c.MediaId);

            HasMany(c => c.Resources)
                .WithRequired(c => c.Media)
                .HasForeignKey(c => c.MediaId);

            HasMany(c => c.Comments)
                .WithRequired(c => c.Media)
                .HasForeignKey(c => c.MediaId);
        }
    }
}
