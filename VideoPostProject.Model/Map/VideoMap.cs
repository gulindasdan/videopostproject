using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoPostProject.Core.Map;
using VideoPostProject.Model.Entities;

namespace VideoPostProject.Model.Map
{
    public class VideoMap : CoreMap<Video>
    {
        public VideoMap()
        {
            ToTable("dbo.Videos");
            Property(x => x.Title).HasColumnName("Title").HasMaxLength(200).IsRequired();
            Property(x => x.VideoDescription).HasColumnName("VideoDescription").HasMaxLength(10000).IsOptional();
            Property(x => x.VideoPath).HasColumnName("VideoPath").HasMaxLength(300).IsOptional();
            Property(x => x.DisplayingNumber).HasColumnName("DisplayingNumber").IsOptional();
        }
    }
}
