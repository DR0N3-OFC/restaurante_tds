using Aula03.Data;
using Aula03.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Aula03.pages.Atendimentos
{
    public class Edit : PageModel
    {
        private AppDbContext _context;
        [BindProperty]
        public Produto Produto { get; set; } = new();
        
        public Edit(AppDbContext context)
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
            if (!ModelState.IsValid)
                return Page();

            var eventToUpdate = await _context.Produto!.FindAsync(id);

            if (eventToUpdate == null)
                return NotFound();

            eventToUpdate.Name = Produto.Name;
            eventToUpdate.Price = Produto.Price;


            try
            { 
                await _context.SaveChangesAsync();
                return RedirectToPage("/Produtos/Index");
            }   catch(DbUpdateException)
			{
                return Page();
			}

        }
    }
}
