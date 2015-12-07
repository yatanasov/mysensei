using System;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;

namespace MySens.Models
{
    public enum Cities
    {
        LONDON, PARIS, CHICAGO
    }
    public class AppUser: IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Cities City { get; set; }
        public string About { get; set; }
        //public int PhoneNumber { get; set; }
       // public string PasswordHash { get; set; }
       // public string UserName { get; set; }
       // public string Id { get; set; }
              
        public virtual ICollection<Course> CoursesEnrolled { get; set; }

        public virtual ICollection<Course> CoursesTeaching { get; set; }

//        public virtual ICollection<AppRole> Roles { get; set; }
    }
}