using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoPostProject.Core.Entity;

namespace VideoPostProject.Model.Entities
{
    public class Video : CoreEntity
    {
        [Required(ErrorMessage = "Lütfen bir başlık giriniz")]
        public string Title { get; set; }
        public string VideoDescription { get; set; }
        public string VideoPath { get; set; }
        [ForeignKey("Category")]
        [Required(ErrorMessage = "Lütfen bir kategori adı giriniz")]
        public Guid CategoryID { get; set; }
        [ForeignKey("User")]
        public Guid UserID { get; set; }
        public int DisplayingNumber { get; set; }

        public virtual User User { get; set; }
        public virtual Category Category { get; set; }
        public virtual List<Favorite> Favorites { get; set; }
        public virtual List<History> Histories { get; set; }
        public virtual List<Comment> Comments { get; set; }
        public virtual List<LikeVideo> LikeVideos { get; set; }
        public virtual List<DislikeVideo> DislikeVideos { get; set; }
        public virtual List<Displaying> Displayings { get; set; }
    }
}
