using Aula03.Data;
using Aula03.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Aula03.pages.BooksAdm
{
    public class Create : PageModel
    {
        private AppDbContext _context;
        [BindProperty]
        public BooksModel Book { get; set; } = new();
        
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
                String bookPath = Request.Form["Link"].ToString();
                Book.GetPdfContent(bookPath);

                _context.Add(Book);
                await _context.SaveChangesAsync();
                return RedirectToPage("/BooksAdm/Index");
            } catch(DbUpdateException)
            {
                return Page();
            }
        }
    }
}
