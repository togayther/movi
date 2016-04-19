using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Movi.Model;

namespace Movi.EFRepository.Mapping
{
    public class UserMap : EntityTypeConfiguration<User>
    {
        public UserMap()
        {
            HasKey(c => c.Id).ToTable("Monkey");

            Property(c => c.Name).HasMaxLength(255);
            Property(c => c.Password).HasMaxLength(255);
            Property(c => c.NickName).HasMaxLength(32);
            Property(c => c.Avatar).HasMaxLength(128);
            Property(c => c.Salt).HasMaxLength(32);
            Property(c => c.Email).HasMaxLength(32);
            Property(c => c.Phone).HasMaxLength(32);
            Property(c => c.Qq).HasMaxLength(16);
            Property(c => c.IsLock).HasColumnType("boolean");
            Property(c => c.LastLoginIp).HasMaxLength(32);
            Property(c => c.IsSuperUser).HasColumnType("boolean");

            Property(c => c.AddTime).HasColumnType("datetime");

            Property(c => c.LastLoginTime).HasColumnType("datetime");

        }
    }
}
