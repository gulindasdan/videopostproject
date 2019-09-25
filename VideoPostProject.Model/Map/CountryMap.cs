using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoPostProject.Core.Map;
using VideoPostProject.Model.Entities;

namespace VideoPostProject.Model.Map
{
    public class CountryMap : CoreMap<Country>
    {
        public CountryMap()
        {
            ToTable("dbo.Countries");
            Property(x => x.CountryName).HasColumnName("CountryName").HasMaxLength(250).IsRequired();
        }
    }
}
