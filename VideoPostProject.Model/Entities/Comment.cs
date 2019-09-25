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
    public class Comment : CoreEntity
    {
        [Required(ErrorMessage = "Lütfen bir kategori adı giriniz")]
        public string CommentText { get; set; }
        //[ForeignKey("Videos")]
        public Guid VideoID { get; set; }
        //[ForeignKey("User")]
        public Guid UserID { get; set; }
        //[ForeignKey("CommentOfComment")]
        //public Guid CommentOfCommentID { get; set; }

        public virtual Video Video { get; set; }
        public virtual User User { get; set; }
        public virtual Comment CommentOfComment { get; set; }
    }
}
