using Aula03.Data;
using Aula03.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Aula03.pages.Garcons
{
    public class Delete : PageModel
    {
        private AppDbContext _context;
        [BindProperty]
        public Garcom Garcom { get; set; } = new();

        public Delete(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {

            var eventModel = await _context.Garcom!.FirstOrDefaultAsync(e => e.GarcomID == id);

            if (eventModel == null) return NotFound();

			Garcom = eventModel;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var eventToDelete = await _context.Garcom!.FindAsync(id);
            
            if (eventToDelete == null) return NotFound();

            try
            {
                _context.Garcom!.Remove(eventToDelete);
                await _context.SaveChangesAsync();
                return RedirectToPage("/Garcons/Index");
            } catch(DbUpdateException)
            {
                return Page();
            }

        }
    }
}
