using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebRestApi.WebApp.Models;

namespace WebRestApi.WebApp.Pages
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly ICredentialsManager _credentialsManager;
        public string Name { get; private set; }

        [BindProperty]
        public IList<Customer> Customers { get; set; }

        public IndexModel(ICredentialsManager credentialsManager)
        {
            this._credentialsManager = credentialsManager;
            Customers = new List<Customer>();
        }

        public IActionResult OnGet()
        {
            Name = User.Identity.Name;
            return Page();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            return RedirectToPage();
        }
    }
}