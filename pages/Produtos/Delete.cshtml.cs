using Aula03.Data;
using Aula03.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Aula03.pages.Produtos
{
    public class Delete : PageModel
    {
        private AppDbContext _context;
        [BindProperty]
        public Produto Produto { get; set; } = new();

        public Delete(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {

            var eventModel = await _context.Produto!.FirstOrDefaultAsync(e => e.ProdutoID == id);

            if (eventModel == null) return NotFound();

            Produto = eventModel;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var eventToDelete = await _context.Produto!.FindAsync(id);
            
            if (eventToDelete == null) return NotFound();

            try
            {
                _context.Produto!.Remove(eventToDelete);
                await _context.SaveChangesAsync();
                return RedirectToPage("/Produtos/Index");
            } catch(DbUpdateException)
            {
                return Page();
            }

        }
    }
}
