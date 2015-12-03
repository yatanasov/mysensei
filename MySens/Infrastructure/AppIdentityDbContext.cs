using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using MySens.Models;
using Microsoft.AspNet.Identity;
using System.Data.Entity.ModelConfiguration.Conventions;
namespace MySens.Infrastructure
{

    public class AppIdentityDbContext : IdentityDbContext<AppUser>
    {
        public DbSet<Course> Courses { get; set; }
        public DbSet<Tag> Tags { get; set; }
        // Extra DbSet

        public AppIdentityDbContext() : base("IdentityDb") {
        }
      
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppUser>()
            .HasMany<Course>(s => s.CoursesEnrolled)
            .WithMany(c => c.EnrolledStudents)
            .Map(cs =>
            {
                cs.MapLeftKey("StudentId");
                cs.MapRightKey("CourseId");
                cs.ToTable("StudentEnrollments");
            });

            modelBuilder.Entity<Course>()
         .HasRequired<AppUser>(c => c.Teacher)
         .WithMany(s => s.CoursesTeaching)
         .HasForeignKey(c => c.AppUserID)
          .WillCascadeOnDelete(false);
            base.OnModelCreating(modelBuilder);
        }


        static AppIdentityDbContext()
        {
            Database.SetInitializer<AppIdentityDbContext>(new IdentityDbInit());
        }

        public static AppIdentityDbContext Create()
        {
            return new AppIdentityDbContext();
        }

       // public System.Data.Entity.DbSet<MySens.Models.AppUser> AppUsers { get; set; }
    }

    public class IdentityDbInit : NullDatabaseInitializer<AppIdentityDbContext>
    {

    }

}