using System;
using Microsoft.AspNetCore.Identity;

namespace WebRestApi.WebApp.Models {
    public class ApplicationUser : IdentityUser {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
    }
}