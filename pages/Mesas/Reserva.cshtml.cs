using Aula03.Data;
using Aula03.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Aula03.pages.Mesas
{
    public class Reserva : PageModel
    {
        AppDbContext _context;

        public Atendimento? Atendimento { get; set; } = new();
        public Mesa? Mesa { get; set; }
        public List<Produto>? ProdutosList { get; set; } = new();
        public int? mesa_id { get; set; }

        public Reserva(AppDbContext context)
        {
            _context = context;

        }
        public async Task<IActionResult> OnGetAsync(int id)
        {
            ProdutosList = await _context.Produto!.Include(p => p.Categoria).ToListAsync();

            var mesa = await _context.Mesa!.FirstOrDefaultAsync(e => e.MesaID == id);
            var garcom = await _context.Garcom!.FirstOrDefaultAsync(e => e.GarcomID == 1);

            Atendimento!.Mesa = mesa;
            Atendimento!.Garcom = garcom;

            Atendimento!.InitDate = DateTime.Now;    
            Atendimento!.FinishDate = DateTime.Now.AddMinutes(30);

            //_context.Add(Atendimento);
            //await _context.SaveChangesAsync();

            return Page();
        }

        public async Task OnPostAdicionarAsync([FromForm] int id)
        {
            var produto = await _context.Produto!.FirstOrDefaultAsync(e => e.ProdutoID == id);

            Atendimento!.Produtos!.Add(produto);

            return;
        }
    }
}
