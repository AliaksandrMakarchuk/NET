using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebRestApi.WebApp.Models;

namespace WebRestApi.WebApp.Pages
{
    public class LoginModel : PageModel
    {
        private readonly ICredentialsManager _credentialsManager;
        private string _from;
        private readonly UserContext _userContext;

        [BindProperty]
        public Credentials Credentials { get; set; }

        public LoginModel(
            ICredentialsManager credentialsManager,
            UserContext userContext)
        {
            _credentialsManager = credentialsManager;
            _userContext = userContext;
        }

        public IActionResult OnGet(string from)
        {
            _from = from;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // var result = await _networkManager.SendRequest(Credentials);
            var user = await _userContext.Users.FirstOrDefaultAsync(x => x.Email == Credentials.Email && x.Password == Credentials.Password);
            if (user != null)
            {
                var claims = new List<Claim>
                {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email)
                };
                var claimsIdentity = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
                var identity = HttpContext.User.Identity;
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                _from = string.IsNullOrWhiteSpace(_from) ? "/Index" : _from;
                return RedirectToPage(_from);
            }

            // await _signInManager.SignInAsync(new ApplicationUser { Email = Credentials.Email }, new AuthenticationProperties());

            return RedirectToPage("/Register");
        }
    }
}