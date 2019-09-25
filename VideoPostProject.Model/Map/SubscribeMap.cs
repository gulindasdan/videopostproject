using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoPostProject.Core.Map;
using VideoPostProject.Model.Entities;

namespace VideoPostProject.Model.Map
{
    public class SubscribeMap : CoreMap<Subsribe>
    {
        public SubscribeMap()
        {
            ToTable("dbo.Subsribe");
            Property(x => x.takipEdiyorMu).IsRequired();
        }
    }
}
