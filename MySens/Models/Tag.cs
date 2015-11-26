using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MySens.Models
{
    public class Tag
    {
        public int TagID { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Course>TagCourses { get; set; }

    }
}