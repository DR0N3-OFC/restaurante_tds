using Aula03.Data;
using Aula03.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Aula03.pages.Atendimentos
{
    public class Index : PageModel
    {
        AppDbContext _context;

        public List<Atendimento> AtendimentosList { get; set; } = new();

        public Index(AppDbContext context)
        {
            _context = context;

        }
        public async Task<IActionResult> OnGetAsync()
        {
            AtendimentosList = await _context.Atendimento!
                .Include(a => a.Garcom)
                .Include(a => a.Mesa)
                .Include(a => a.Produtos)
                .ToListAsync();
            return Page();
        }
    }
}
