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
        private readonly CustomerDbContext _context;

        public EditModel(CustomerDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet(int id)
        {
            Customer = _context.Customers.SingleOrDefault(c => c.Id == id);

            if(Customer == null){
                return RedirectToPage("./Index");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Customers.Update(Customer);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}