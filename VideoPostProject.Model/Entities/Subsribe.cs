using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoPostProject.Core.Entity;

namespace VideoPostProject.Model.Entities
{
    public class Subsribe : CoreEntity
    {
        public Guid FolowedID { get; set; }
        public Guid FolowerID { get; set; }
        public virtual User Folowed { get; set; }
        public virtual User Folower { get; set; }


        public bool takipEdiyorMu { get; set; }
    }
}
