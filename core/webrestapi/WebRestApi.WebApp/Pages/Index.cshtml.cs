using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebRestApi.WebApp.Models;

namespace WebRestApi.WebApp.Pages {
    public class IndexModel : PageModel {
        private readonly CustomerDbContext _context;
        private readonly ICredentialsManager _credentialsManager;

        public IndexModel (CustomerDbContext context, ICredentialsManager credentialsManager) {
            _context = context;
            this._credentialsManager = credentialsManager;
            Customer = new List<Customer>();
        }

        [BindProperty]
        public IList<Customer> Customer { get; set; }

        public IActionResult OnGet () {
            Console.WriteLine($"GET: {_credentialsManager.IsAuthorized}");
            if(!_credentialsManager.IsAuthorized) {
                return Redirect("/Login?from=/Index");
                // return RedirectToPage("/Login/Index");
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