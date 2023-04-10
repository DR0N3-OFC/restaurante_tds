using Aula03.Data;
using Aula03.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Aula03.pages.Atendimentos
{
    public class Delete : PageModel
    {
        private AppDbContext _context;
        [BindProperty]
        public Atendimento? Atendimento { get; set; } = new();

        public Delete(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {

            Atendimento = await _context.Atendimento!
                                .Include(a => a.Garcom)
                                .Include(a => a.Mesa)
                                .Include(a => a.Produtos)
                                .FirstOrDefaultAsync(e => e.AtendimentoID == id);

            if (Atendimento == null) return NotFound();


            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var eventToDelete = await _context.Atendimento!.FindAsync(id);
            
            if (eventToDelete == null) return NotFound();

            try
            {
                _context.Atendimento!.Remove(eventToDelete);
                await _context.SaveChangesAsync();
                return RedirectToPage("/Atendimentos/Index");
            } catch(DbUpdateException)
            {
                return Page();
            }

        }
    }
}
