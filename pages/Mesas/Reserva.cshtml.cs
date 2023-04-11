using Aula03.Data;
using Aula03.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Aula03.pages.Mesas
{
    public class Reserva : PageModel
    {
        AppDbContext _context;
        [BindProperty]
        public Atendimento? Atendimento { get; set; } = new();
        public Mesa? Mesa { get; set; }
        public Garcom? Garcom { get; set; }
        public List<Garcom> GarcomList { get; set; }
        public List<Produto>? ProdutosList { get; set; } = new();
        public List<Produto>? AtendimentoProdutos { get; set; } = new();
        public Produto? Produto { get; set; }

        [DisplayFormat(DataFormatString = "R${0:N2}")]
        public double? Total { get; set; }
        public int? mesa_id { get; set; }
        public int? garcom_id { get; set; }
        public int? atendimento_id { get; set; }
        public string? check { get; set; }


        private readonly IHttpContextAccessor _httpContextAccessor;

        public Reserva(IHttpContextAccessor httpContextAccessor, AppDbContext context)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;


            //Recupera Dados do Atendimento
            HttpContext httpContext = _httpContextAccessor.HttpContext!;
            
            atendimento_id = httpContext!.Session.GetInt32("AtendimentoID");
            check = httpContext!.Session.GetString("Checked");

            if(check == "T")
            { 

                Atendimento = _context.Atendimento!
                            //.OrderByDescending(e => e.AtendimentoID)
                            .Include(a => a.Garcom)
                            .Include(a => a.Mesa)
                            .Include(a => a.Produtos)
                            .FirstOrDefault(e => e.AtendimentoID == atendimento_id);
            }
            //Recupera a Lista de Produtos
            ProdutosList = _context.Produto!.Include(p => p.Categoria).ToList();
            GarcomList = _context.Garcom!.ToList();
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {

            HttpContext httpContext = _httpContextAccessor.HttpContext!;

            Mesa = await _context.Mesa!.FirstOrDefaultAsync(e => e.MesaID == id);
            Garcom = await _context.Garcom!.FirstOrDefaultAsync(e => e.GarcomID == 1);

            httpContext!.Session.SetInt32("MesaID", id);
            Mesa.HoraLiberacao = DateTime.Now.AddMinutes(1);
            _context.Update(Mesa);

            mesa_id = id;



            Atendimento!.Mesa = Mesa;
            Atendimento!.Garcom = Garcom;

            Atendimento!.InitDate = DateTime.Now;    
            Atendimento!.FinishDate = DateTime.Now.AddMinutes(1);


            _context.Add(Atendimento);
            await _context.SaveChangesAsync();


            Atendimento = _context.Atendimento!
                            .OrderByDescending(e => e.AtendimentoID)
                            .Include(a => a.Garcom)
                            .Include(a => a.Mesa)
                            .Include(a => a.Produtos)
                            .FirstOrDefault();

            httpContext!.Session.SetInt32("AtendimentoID", Atendimento!.AtendimentoID ?? 1);
            httpContext!.Session.SetString("Checked", "T");


            return Page();
        }

        public async Task<IActionResult> OnPostAdicionarAsync([FromForm] int id)
        {
            Produto = await _context.Produto!.FirstOrDefaultAsync(e => e.ProdutoID == id);

            Atendimento = await _context.Atendimento!
                            //.OrderByDescending(e => e.AtendimentoID)
                            .Include(a => a.Garcom)
                            .Include(a => a.Mesa)
                            .Include(a => a.Produtos)
                            .FirstOrDefaultAsync(e => e.AtendimentoID == atendimento_id);


            //HttpContext httpContext = _httpContextAccessor.HttpContext!;
            //httpContext!.Session.SetInt32("AtendimentoID", Atendimento.AtendimentoID ?? 1);


            Atendimento!.Produtos!.Add(Produto);
            await _context.SaveChangesAsync();
            Total = Atendimento.Produtos!.Sum(p => p.Price);

            return Page();
        }
        public async Task<IActionResult> OnPostDeletarAsync([FromForm] int id)
        {
            Produto = await _context.Produto!.FirstOrDefaultAsync(e => e.ProdutoID == id);

            Atendimento = await _context.Atendimento!
                            .OrderByDescending(e => e.AtendimentoID)
                            .Include(a => a.Garcom)
                            .Include(a => a.Mesa)
                            .FirstOrDefaultAsync();


            //HttpContext httpContext = _httpContextAccessor.HttpContext!;
            //httpContext!.Session.SetInt32("AtendimentoID", Atendimento.AtendimentoID ?? 1);


            Atendimento!.Produtos!.Remove(Produto);
            await _context.SaveChangesAsync();

            return Page();
        }

        public async Task<IActionResult> OnPostDesignarAsync([FromForm] int id)
        {
            Garcom = await _context.Garcom!.FirstOrDefaultAsync(e => e.GarcomID == id);

            Atendimento = await _context.Atendimento!
                            .OrderByDescending(e => e.AtendimentoID)
                            .Include(a => a.Garcom)
                            .Include(a => a.Mesa)
                            .FirstOrDefaultAsync();


            //HttpContext httpContext = _httpContextAccessor.HttpContext!;
            //httpContext!.Session.SetInt32("AtendimentoID", Atendimento.AtendimentoID ?? 1);

            Atendimento!.Garcom = Garcom;
            _context.Update(Atendimento);
            await _context.SaveChangesAsync();

            return Page();
        }

        public async Task<IActionResult> OnPostCancelarAsync([FromForm] int id)
        {

            HttpContext httpContext = _httpContextAccessor.HttpContext!;
            mesa_id = httpContext!.Session.GetInt32("MesaID");


            Mesa = await _context.Mesa!.FirstOrDefaultAsync(e => e.MesaID == mesa_id);
            Mesa.HoraLiberacao = DateTime.Now;

            Atendimento = await _context.Atendimento!
                            //.OrderByDescending(e => e.AtendimentoID)
                            .Include(a => a.Garcom)
                            .Include(a => a.Mesa)
                            .Include(a => a.Produtos)
                            .FirstOrDefaultAsync(e => e.AtendimentoID == atendimento_id);

            _context.Atendimento!.Remove(Atendimento);
            await _context.SaveChangesAsync();

            httpContext!.Session.SetInt32("AtendimentoID", 1);
            httpContext!.Session.SetString("Checked", "F");

            return Redirect("/Mesas");
        }

        public async Task<IActionResult> OnPostSalvarAsync([FromForm] int id)
        {
            HttpContext httpContext = _httpContextAccessor.HttpContext!;

            Atendimento = await _context.Atendimento!
                            //.OrderByDescending(e => e.AtendimentoID)
                            .Include(a => a.Garcom)
                            .Include(a => a.Mesa)
                            .Include(a => a.Produtos)
                            .FirstOrDefaultAsync(e => e.AtendimentoID == atendimento_id);

            if(Atendimento!.Garcom!.GarcomID == 1)
            {
                ModelState.AddModelError("", "Erro: Precisa escolher um garçom.");
                return Page();
            }
            if (Atendimento.Produtos!.Count == 0)
            {
                ModelState.AddModelError("", "Erro: Nenhum produto foi adicionado ainda.");
                return Page();
            }

            httpContext!.Session.SetInt32("AtendimentoID", 1);
            httpContext!.Session.SetString("Checked", "F");

            return Redirect("/Mesas");
        }
    }
}
