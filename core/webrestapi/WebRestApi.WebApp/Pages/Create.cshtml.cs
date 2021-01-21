using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebRestApi.WebApp.ViewModels;

namespace WebRestApi.WebApp.Pages
{
    public class CreateModel : PageModel
    {
        public CreateModel() { }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Customer Customer { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
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