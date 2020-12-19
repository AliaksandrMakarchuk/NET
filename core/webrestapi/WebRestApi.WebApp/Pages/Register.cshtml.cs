using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebRestApi.WebApp.Models;

namespace WebRestApi.WebApp.Pages {
    public class RegisterModel : PageModel {
        public string ReturnUrl { get; set; }
        public InputModel Input { get; set; }

        public async Task<IActionResult> OnPostAsync () {
            if (ModelState.IsValid) {
                var user = new ApplicationUser {
                    UserName = Input.UserName,
                    Email = Input.Email,
                    FirstName = Input.FirstName,
                    LastName = Input.LastName,
                    BirthDate = Input.BirthDate
                };
            }

            return RedirectToPage("/Index");
        }
    }

    public class InputModel : PageModel {
        [Required]
        [Display (Name = "User Name")]
        public string UserName { get; set; }

        [Required]
        [Display (Name = "Email")]
        public string Email { get; set; }

        [Display (Name = "First Name")]
        public string FirstName { get; set; }

        [Display (Name = "Last Name")]
        public string LastName { get; set; }

        [Display (Name = "Date of birth")]
        public DateTime BirthDate { get; set; }
    }
}