using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoPostProject.Core.Entity;

namespace VideoPostProject.Model.Entities
{
    public class AdvertisingType :  CoreEntity
    {
        public string TypeName { get; set; }
        public virtual List<Advertisement> Advertisements { get; set; }
    }
}
