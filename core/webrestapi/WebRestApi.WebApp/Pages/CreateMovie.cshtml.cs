using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebRestApi.WebApp.Models;

namespace WebRestApi.WebApp.Pages
{
    public class MovieModel : PageModel
    {
        [BindProperty]
        public Movie Movie { get; set; }

        public MovieModel()
        { }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return await Task.FromResult(Page());
            }

            return RedirectToPage("./Index");
        }
    }
}