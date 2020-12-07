using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebRestApi.WebApp.Models;

namespace WebRestApi.WebApp.Pages {
    public class IndexModel : PageModel {
        private readonly CustomerDbContext _context;
        private readonly CredentialsManager _credentialsManager;

        public IndexModel (CustomerDbContext context, CredentialsManager credentialsManager) {
            _context = context;
            this._credentialsManager = credentialsManager;
        }

        public IList<Customer> Customer { get; set; }

        public IActionResult OnGet () {
            if(!_credentialsManager.IsAuthorized) {
                return RedirectToPage("/Login");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostDeleteAsync (int id) {
            var contact = await _context.Customers.FindAsync (id);

            if (contact != null) {
                _context.Customers.Remove (contact);
                await _context.SaveChangesAsync ();
            }

            return RedirectToPage ();
        }
    }
}