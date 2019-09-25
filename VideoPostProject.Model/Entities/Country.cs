using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoPostProject.Core.Entity;

namespace VideoPostProject.Model.Entities
{
    public class Country : CoreEntity
    {
        //[Required(ErrorMessage = "Lütfen bir kategori adı giriniz")]
        public string CountryName { get; set; }
        public virtual List<User> Users { get; set; }
    }
}
