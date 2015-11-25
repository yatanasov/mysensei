using System;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MySens.Models
{
    public class AppUser: IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

    }
}