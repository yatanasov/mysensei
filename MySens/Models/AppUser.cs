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
        public virtual ICollection<Course> Courses { get; set; }
    }
}