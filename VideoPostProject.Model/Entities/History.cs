using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoPostProject.Core.Entity;

namespace VideoPostProject.Model.Entities
{
    public class History : CoreEntity
    {
        public virtual User User { get; set; }
        public virtual Video Video { get; set; }
    }
}
