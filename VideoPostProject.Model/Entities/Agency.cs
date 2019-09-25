using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoPostProject.Core.Entity;

namespace VideoPostProject.Model.Entities
{
    public class Agency : CoreEntity
    {
        [Required(ErrorMessage = "Lütfen bir ajans adı giriniz")]
        public String AgencyName { get; set; }
        public String Address { get; set; }
        public String Phone { get; set; }
        public virtual List<Invoice> Invoices { get; set; }
    }
}
