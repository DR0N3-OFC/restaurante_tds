using Aula03.Data;
using Aula03.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Aula03.pages.Produtos
{
    public class Create : PageModel
    {
        private AppDbContext _context;
        [BindProperty]
        public Produto Produto { get; set; } = new();
        
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
                _context.Add(Produto);
                await _context.SaveChangesAsync();
                return RedirectToPage("/Produtos/Index");
            } catch(DbUpdateException)
            {
                return Page();
            }
        }
    }
}
