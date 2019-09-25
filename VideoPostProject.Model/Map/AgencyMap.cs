using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoPostProject.Core.Map;
using VideoPostProject.Model.Entities;

namespace VideoPostProject.Model.Map
{
    public class AgencyMap : CoreMap<Agency>
    {
        public AgencyMap()
        {
            ToTable("dbo.Agencies");
            Property(x => x.AgencyName).HasColumnName("AgencyName").HasMaxLength(200).IsOptional();
            Property(x => x.Address).HasColumnName("Address").IsOptional();
            Property(x => x.Phone).HasColumnName("Phone").IsOptional();
        }
    }
}
