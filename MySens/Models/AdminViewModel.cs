using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MySens.Models
{
    public class AdminViewModel
    {
        public  IEnumerable<AppUser> AppUsers { get; set; }
        public IEnumerable<Course>Courses { get; set; }
        public IEnumerable<Tag> Tags { get; set; }

        public AdminViewModel()
        {
            //List<Course> Courses = new List<Course>(); 
            //List<AppUser> AppUsers = new List<AppUser>();
        }
    }
}