using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoPostProject.Core.Map;
using VideoPostProject.Model.Entities;

namespace VideoPostProject.Model.Map
{
    public class LikeVideoMap :CoreMap<LikeVideo>
    {
        public LikeVideoMap()
        {
            ToTable("dbo.LikeVideos");
            Property(x => x.Liked).IsRequired();
        }
    }
}
