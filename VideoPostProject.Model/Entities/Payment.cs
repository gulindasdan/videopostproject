using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoPostProject.Core.Entity;

namespace VideoPostProject.Model.Entities
{
    public class Payment : CoreEntity
    {
        public virtual Invoice Invoice { get; set; }
    }
}
