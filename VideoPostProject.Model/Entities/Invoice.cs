using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoPostProject.Core.Entity;

namespace VideoPostProject.Model.Entities
{
    public class Invoice : CoreEntity
    {
        public Agency Agency { get; set; }
        public User Client { get; set; }
        public virtual List<Payment> Payments { get; set; }
    }
}
