using System.Data.Entity.ModelConfiguration;
using Movi.Model;

namespace Movi.EFRepository.Mapping
{
    public class VisitorMap : EntityTypeConfiguration<Visitor>
    {
        public VisitorMap()
        {
            HasKey(c => c.Id).ToTable("Visitor");

            Property(c => c.Ip).HasMaxLength(32);
            Property(c => c.Browser).HasMaxLength(64);
            Property(c => c.Source).HasMaxLength(255);
            Property(c => c.VisitTime).HasColumnType("datetime");
            Property(c => c.UserAgent).HasMaxLength(1024);
        }
    }
}
