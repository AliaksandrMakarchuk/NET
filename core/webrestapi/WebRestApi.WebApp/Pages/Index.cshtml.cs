using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebRestApi.WebApp.ViewModels;

namespace WebRestApi.WebApp.Pages
{
    [Authorize]
    public class IndexModel : PageModel
    {
        public string Name { get; private set; }

        [BindProperty]
        public IList<Customer> Customers { get; set; }

        public IndexModel()
        {
            Customers = new List<Customer>();
        }

        public IActionResult OnGet()
        {
            Name = User.Identity.Name;
            return Page();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            return await Task.FromResult(RedirectToPage());
        }
    }
}