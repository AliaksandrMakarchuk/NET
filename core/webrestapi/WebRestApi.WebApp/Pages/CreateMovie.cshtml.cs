using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebRestApi.WebApp.Models;

namespace WebRestApi.WebApp.Pages {
    public class MovieModel : PageModel {
        private readonly CustomerDbContext _context;

        [BindProperty]
        public Movie Movie { get; set; }

        public MovieModel(CustomerDbContext context) {
            this._context = context;
        }

        public IActionResult OnGet() {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync() {
            if(!ModelState.IsValid) {
                return Page();
            }

            await _context.Customers.FindAsync(1);

            return RedirectToPage("./Index");
        }
    }
}