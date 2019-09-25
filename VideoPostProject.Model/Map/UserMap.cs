using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoPostProject.Core.Map;
using VideoPostProject.Model.Entities;

namespace VideoPostProject.Model.Map
{
    public class UserMap : CoreMap<User>
    {
        public UserMap()
        {
            ToTable("dbo.Users");
            Property(x => x.Name).HasColumnName("Name").HasMaxLength(100).IsRequired();
            Property(x => x.Surname).HasColumnName("Surname").HasMaxLength(100).IsRequired();
            Property(x => x.Username).HasColumnName("Username").HasMaxLength(100).IsRequired();
            Property(x => x.EmailAddress).HasColumnName("EmailAddress").HasMaxLength(200).IsRequired();
            Property(x => x.Password).HasColumnName("Password").HasMaxLength(20).IsRequired();
            Property(x => x.BirthDate).HasColumnName("BirthDate").HasColumnType("date").IsOptional();
            Property(x => x.Phone).HasColumnName("Phone").HasMaxLength(15).IsOptional();
            Property(x => x.Gender).HasColumnName("Gender").IsOptional();
            Property(x=>x.ChannelName).HasColumnName("ChannelName").IsOptional();
            Property(x=>x.BioDescription).HasColumnName("BioDescription").HasMaxLength(2000).IsOptional();
            Property(x=>x.ImagePath).HasColumnName("ImagePath").HasMaxLength(200).IsOptional();
            Property(x=>x.CoverImagePath).HasColumnName("CoverImagePath").HasMaxLength(200).IsOptional();
            Property(x => x.isAdministrator).HasColumnName("isAdministrator").IsRequired();
            Property(x => x.RememberMe).HasColumnName("RememberMe").IsRequired();
        }
    }
}
