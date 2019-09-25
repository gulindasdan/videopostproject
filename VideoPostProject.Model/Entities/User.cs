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
    public enum Gender
    {
        Kadın = 0,
        Erkek = 1
    }
    public class User : CoreEntity
    {
        [Required(ErrorMessage = "Lütfen adınızı giriniz")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Lütfen soyadınızı giriniz")]
        public string Surname { get; set; }
        [Required(ErrorMessage = "Lütfen bir kullanıcı giriniz")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Lütfen bir e-mail adresi giriniz")]
        public string EmailAddress { get; set; }
        [Required(ErrorMessage = "Lütfen bir şifre giriniz")]
        public string Password { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Phone { get; set; }
        public Gender Gender { get; set; }
        
        public Guid CountryID { get; set; }

        //[ForeignKey("CountryID")]
        public virtual Country Country { get; set; }

        public string ChannelName { get; set; }
        
        public Guid CategoryID { get; set; }

        //[ForeignKey("CategoryID")]
        public virtual Category Category { get; set; }

        public string BioDescription { get; set; }
        public string ImagePath { get; set; }
        public string CoverImagePath { get; set; }
        public bool isAdministrator { get; set; }
        public bool RememberMe { get; set; }

        public virtual List<Video> Videos { get; set; }
        public virtual List<Favorite> Favorites { get; set; }
        public virtual List<Subsribe> Subsribes { get; set; }
        public virtual List<History> Histories { get; set; }
        public virtual List<Comment> Comments { get; set; }
        
    }
}
