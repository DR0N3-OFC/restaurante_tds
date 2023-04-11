using Aula03.Data;
using Aula03.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Aula03.pages.Dashboard
{
    public class Index : PageModel
    {
        AppDbContext _context;

        public List<Garcom> GarconsList { get; set; } = new();
        public List<Mesa> MesasList { get; set; } = new();
        public List<Produto> ProdutosList { get; set; } = new();
        public List<Categoria> CategoriasList { get; set; } = new();

        public Index(AppDbContext context)
        {
            _context = context;

        }
        public async Task<IActionResult> OnGetAsync()
        {
			GarconsList = await _context.Garcom!.ToListAsync();
            MesasList = await _context.Mesa!.ToListAsync();
            CategoriasList = await _context.Categoria!.ToListAsync();
            ProdutosList = await _context.Produto!.ToListAsync();
            return Page();
        }
    }
}
