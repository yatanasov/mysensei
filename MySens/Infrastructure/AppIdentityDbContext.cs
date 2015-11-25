using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using MySens.Models;
using Microsoft.AspNet.Identity;
using System.Data.Entity.ModelConfiguration.Conventions;
namespace MySens.Infrastructure
{

    public class AppIdentityDbContext : IdentityDbContext<AppUser>
    {
        public DbSet<Course> Courses { get; set; } // Extra DbSet



        public AppIdentityDbContext() : base("IdentityDb") {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

           // modelBuilder.Conventions.Remove<PluralizingTableNameConvention>(); // Identity use pluralized table names
            // one-to-many relation between Course (1) and User (N)
            modelBuilder.Entity<Course>()
            .HasRequired<AppUser>(s => s.AppUser)
            .WithMany(s => s.Courses)
            .HasForeignKey(s => s.AppUserID);
            // the all important base class call! Add this line to make your problems go away.
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
    }

    public class IdentityDbInit : NullDatabaseInitializer<AppIdentityDbContext>
    {

    }

}