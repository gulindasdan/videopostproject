using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoPostProject.Core.Entity;

namespace VideoPostProject.Model.Entities
{
    public class SubCategory : CoreEntity
    {
        [Required(ErrorMessage = "Lütfen bir kategori adı giriniz")]
        public string SubCategoryName { get; set; }
        public string Description { get; set; }
        public Guid CategoryID { get; set; }
        
        public virtual Category Category { get; set; }

    }
}
