using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoPostProject.Core.Entity;

namespace VideoPostProject.Model.Entities
{
    public class Category : CoreEntity
    {
        [Required(ErrorMessage = "Lütfen bir kategori adı giriniz")]
        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; }
        public virtual List<Video> Videos { get; set; }
        public virtual List<User> Users { get; set; }
        public virtual List<SubCategory> SubCategories { get; set; }

    }
    
}
