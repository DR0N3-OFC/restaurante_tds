using Aula03.Data;
using Aula03.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Aula03.pages.BooksAdm
{
    public class Edit : PageModel
    {
        private AppDbContext _context;
        [BindProperty]
        public BooksModel Book { get; set; } = new();
        
        public Edit(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Events == null)
                return NotFound();

            var eventModel = await _context.Books!.FirstOrDefaultAsync(e => e.BookID == id);

            if (eventModel == null) return NotFound();

            Book = eventModel;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (!ModelState.IsValid)
                return Page();

            var eventToUpdate = await _context.Books!.FindAsync(id);

            if (eventToUpdate == null)
                return NotFound();

            eventToUpdate.Name = Book.Name;
            eventToUpdate.Author = Book.Author;
            eventToUpdate.Desc = Book.Desc;
            eventToUpdate.PublicationYear = Book.PublicationYear;
            eventToUpdate.Price = Book.Price;


            String bookPath = Request.Form["Link"].ToString();
            eventToUpdate.GetPdfContent(bookPath);

            try
            { 
                await _context.SaveChangesAsync();
                return RedirectToPage("/BooksAdm/Index");
            }   catch(DbUpdateException)
			{
                return Page();
			}

        }
    }
}
