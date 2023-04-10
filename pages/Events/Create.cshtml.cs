using Aula03.Data;
using Aula03.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Aula03.pages.Events
{
    public class Create : PageModel
    {
        private AppDbContext _context;
        [BindProperty]
        public EventsModel EventModel { get; set; } = new();
        public void OnGet()
        {

        }
        public Create(AppDbContext context)
        {
            _context = context;
        }
        
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            try
            {
                _context.Add(EventModel);
                await _context.SaveChangesAsync();
                return RedirectToPage("/Events/Index");
            } catch(DbUpdateException)
            {
                return Page();
            }
        }
    }
}
