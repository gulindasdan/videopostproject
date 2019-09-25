using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using VideoPostProject.Core.Entity;
using VideoPostProject.Model.Entities;
using VideoPostProject.Model.Map;

namespace VideoPostProject.Model.Context
{
    public class ProjectContext : DbContext
    {
        public ProjectContext()
        {
            Database.Connection.ConnectionString = "Server=.; Database=YouTubeDB; UID=sa; PWD=123";
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Configurations.Add(new UserMap());
            modelBuilder.Configurations.Add(new VideoMap());
            modelBuilder.Configurations.Add(new CategoryMap());
            modelBuilder.Configurations.Add(new CommentMap());
            modelBuilder.Configurations.Add(new CountryMap());
            modelBuilder.Configurations.Add(new LikeVideoMap());
            modelBuilder.Configurations.Add(new SubCategoryMap());
            modelBuilder.Configurations.Add(new CampaignMap());
            modelBuilder.Configurations.Add(new AdvertisingTypeMap());
            modelBuilder.Configurations.Add(new SubscribeMap());

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Video> Videos { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<History> Histories { get; set; }
        public DbSet<DislikeVideo> DislikeVideos { get; set; }
        public DbSet<LikeVideo> LikeVideos { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<Subsribe> Subsribes { get; set; }
        public DbSet<Agency> Agencies { get; set; }
        public DbSet<Advertisement> Advertisements { get; set; }
        public DbSet<AdvertisingType> AdvertisingTypes { get; set; }
        public DbSet<Campaign> Campaigns { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Displaying> Displayings { get; set; }

        public override int SaveChanges()
        {
            var modifiedEntities = ChangeTracker.Entries().Where(x => x.State == EntityState.Modified || x.State == EntityState.Added).ToList();

            DateTime datetime = DateTime.Now;
            string ip = HttpContext.Current.Request.UserHostAddress;


            foreach (var item in modifiedEntities)
            {
                CoreEntity entity = item.Entity as CoreEntity;
                if (item != null)
                {
                    switch (item.State)
                    {
                        case EntityState.Added:
                            entity.CreatedIP = ip;
                            entity.CreatedDate = datetime;
                            break;
                        case EntityState.Modified:
                            entity.ModifiedIP = ip;
                            entity.ModifiedDate = datetime;
                            break;
                    }
                }
            }

            try
            {
                return base.SaveChanges();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                Exception raise = dbEx;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string message = string.Format("{0}:{1}",
                            validationErrors.Entry.Entity.ToString(),
                            validationError.ErrorMessage);
                        // raise a new exception nesting
                        // the current instance as InnerException
                        raise = new InvalidOperationException(message, raise);
                    }
                }
                throw raise;
            }
        }
    }
}
