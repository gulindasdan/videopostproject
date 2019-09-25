using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoPostProject.Core.Map;
using VideoPostProject.Model.Entities;

namespace VideoPostProject.Model.Map
{
    public class CampaignMap : CoreMap<Campaign>
    {
        public CampaignMap()
        {
            ToTable("dbo.Campaigns");
            Property(x => x.CampaignName).HasColumnName("CampaignName").HasMaxLength(200).IsOptional();
            Property(x => x.StartDate).HasColumnName("StartDate").IsOptional();
            Property(x => x.EndDate).HasColumnName("EndDate").IsOptional();
        }
    }
}
