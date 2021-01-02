using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebRestApi.WebApp.Models;

namespace WebRestApi.WebApp.Pages
{
    public class EditModel : PageModel
    {
        [BindProperty]
        public Customer Customer { get; set; }

        public EditModel()
        {
        }

        public IActionResult OnGet(int id)
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            return RedirectToPage("./Index");
        }
    }
}