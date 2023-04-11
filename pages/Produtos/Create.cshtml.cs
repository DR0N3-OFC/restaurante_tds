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
        public Categoria? Categoria { get; set; } = new();
        public int? categoria_id  { get; set; }

        public List<Categoria> CategoriasList { get; set; } = new();

        private readonly IHttpContextAccessor _httpContextAccessor;

        public Create(IHttpContextAccessor httpContextAccessor, AppDbContext context)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
            CategoriasList = _context.Categoria.ToList();
        }
        
        public async Task<IActionResult> OnPostAsync()
        {
            categoria_id = int.Parse(Request.Form["CatID"]);

            Categoria = await _context.Categoria!.FirstOrDefaultAsync(c => c.CategoriaID == categoria_id);

            if (!ModelState.IsValid)
            { 
                ModelState.AddModelError("", "Erro: Nenhum produto foi adicionado ainda.");
                return NotFound();
            }
            try
            {
                Produto.Categoria = Categoria;
                _context.Add(Produto);
                await _context.SaveChangesAsync();
                return RedirectToPage("/Produtos/Index");
            } catch(DbUpdateException)
            {
                ModelState.AddModelError("", "Erro: Banco de Dados.");
                return Page();
            }
            return NotFound();
        }

    }
}
