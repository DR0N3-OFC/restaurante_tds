using Aula03.Data;
using Aula03.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Aula03.pages.Garcons
{
    public class Edit : PageModel
    {
        private AppDbContext _context;
        [BindProperty]
        public Garcom Garcom { get; set; } = new();
        
        public Edit(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {

            var garcomModel = await _context.Garcom!.FirstOrDefaultAsync(e => e.GarcomID == id);

            if (garcomModel == null) return NotFound();

			Garcom = garcomModel;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (!ModelState.IsValid)
                return Page();

            var garcomToUpdate = await _context.Garcom!.FindAsync(id);

            if (garcomToUpdate == null)
                return NotFound();

			garcomToUpdate.Name = Garcom.Name;
            garcomToUpdate.SecondName = Garcom.SecondName;
            garcomToUpdate.Telefone = Garcom.Telefone;
            garcomToUpdate.BirthDate = Garcom.BirthDate;

            try
            { 
                await _context.SaveChangesAsync();
                return RedirectToPage("/Garcons/Index");
            }   catch(DbUpdateException)
			{
                return Page();
			}

        }
    }
}
