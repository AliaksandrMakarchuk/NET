using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebRestApi.WebApp.Models;

namespace WebRestApi.WebApp.Pages
{
    public class LoginModel : PageModel {
        private readonly CustomerDbContext _context;
        private readonly INetworkManager _networkManager;
        private readonly ICredentialsManager _credentialsManager;
        private string _from;

        [BindProperty]
        public Credentials Credentials { get; set; }

        public LoginModel (CustomerDbContext context, INetworkManager networkManager, ICredentialsManager credentialsManager) {
            this._context = context;
            this._networkManager = networkManager;
            this._credentialsManager = credentialsManager;
        }

        public IActionResult OnGet(string from) {
            _from = from;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync() {
            var result = await _networkManager.SendRequest(Credentials);

            Console.WriteLine($"Result: {result}");

            _credentialsManager.IsAuthorized = result;

            if(result) {
                _from = string.IsNullOrWhiteSpace(_from) ? "/Index" : _from;
                Console.WriteLine($"Go to: {_from}");
                return RedirectToPage(_from);
            }

            Console.WriteLine("Reload");
            return Page();
        }
    }
}