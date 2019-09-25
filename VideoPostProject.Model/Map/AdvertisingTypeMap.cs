using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoPostProject.Core.Map;
using VideoPostProject.Model.Entities;

namespace VideoPostProject.Model.Map
{
    public class AdvertisingTypeMap : CoreMap<AdvertisingType>
    {
        public AdvertisingTypeMap()
        {
            ToTable("dbo.AdvertisingTypes");
            Property(x => x.TypeName).HasColumnName("TypeName").HasMaxLength(200).IsOptional();
        }
    }
}
