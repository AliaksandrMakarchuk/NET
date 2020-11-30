using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebRestApi.WebApp.Models;

namespace WebRestApi.WebApp.Pages {
    public class LoginModel : PageModel {
        private readonly CustomerDbContext _context;

        public Credentials Credentials { get; set; }

        public LoginModel (CustomerDbContext context) {
            this._context = context;
        }

        public IActionResult OnGet() {
            return Page();
        }
    }
}