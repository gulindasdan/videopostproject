using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoPostProject.Model.Entities;
using VideoPostProject.Service.Base;

namespace VideoPostProject.Service.Option
{
    public class VideoService : BaseService<Video>
    {
        public Video FindByName(string title)
        {
            return GetByDefault(x => x.Title == title);
        }
    }
}
