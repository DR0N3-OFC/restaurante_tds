using Aula03.Data;
using Aula03.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Aula03.pages.Mesas
{
    public class Delete : PageModel
    {
        private AppDbContext _context;
        [BindProperty]
        public Mesa Mesa { get; set; } = new();

        public Delete(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {

            var eventModel = await _context.Mesa!.FirstOrDefaultAsync(e => e.MesaID == id);

            if (eventModel == null) return NotFound();

            Mesa = eventModel;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var eventToDelete = await _context.Mesa!.FindAsync(id);
            
            if (eventToDelete == null) return NotFound();

            try
            {
                _context.Mesa!.Remove(eventToDelete);
                await _context.SaveChangesAsync();
                return RedirectToPage("/Mesas/Index");
            } catch(DbUpdateException)
            {
                return Page();
            }

        }
    }
}
