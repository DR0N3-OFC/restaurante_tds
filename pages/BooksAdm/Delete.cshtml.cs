using Aula03.Data;
using Aula03.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Aula03.pages.BooksAdm
{
    public class Delete : PageModel
    {
        private AppDbContext _context;
        [BindProperty]
        public BooksModel Book { get; set; } = new();

        public Delete(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Events == null)
                return NotFound();

            var eventModel = await _context.Books.FirstOrDefaultAsync(e => e.BookID == id);

            if (eventModel == null) return NotFound();

            Book = eventModel;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var eventToDelete = await _context.Books!.FindAsync(id);
            
            if (eventToDelete == null) return NotFound();

            try
            {
                _context.Books!.Remove(eventToDelete);
                await _context.SaveChangesAsync();
                return RedirectToPage("/Events/Index");
            } catch(DbUpdateException)
            {
                return Page();
            }

        }
    }
}
