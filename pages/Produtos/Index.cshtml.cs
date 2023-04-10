using Aula03.Data;
using Aula03.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Aula03.pages.Produtos
{
    public class Index : PageModel
    {
        AppDbContext _context;

        public List<Produto> ProdutosList { get; set; } = new();

        public Index(AppDbContext context)
        {
            _context = context;

        }
        public async Task<IActionResult> OnGetAsync()
        {
            ProdutosList = await _context.Produto!.ToListAsync();
            return Page();
        }
    }
}
