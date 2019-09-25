using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoPostProject.Core.Entity;

namespace VideoPostProject.Model.Entities
{
    public class Advertisement : CoreEntity
    {
        public Agency Agency { get; set; }
        public Campaign Campaign { get; set; }
        public User Channel { get; set; }
        public AdvertisingType AdvertisingType { get; set; }
        
    }
}
