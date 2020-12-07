using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebRestApi.WebApp.Models;

namespace WebRestApi.WebApp.Pages {
    public class LoginModel : PageModel {
        private readonly CustomerDbContext _context;

        [BindProperty]
        public Credentials Credentials { get; set; }

        public LoginModel (CustomerDbContext context) {
            this._context = context;
        }

        public IActionResult OnGet() {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync() {
            
        }
    }
}