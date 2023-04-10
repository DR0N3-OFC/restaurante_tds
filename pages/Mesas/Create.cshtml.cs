using Aula03.Data;
using Aula03.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Aula03.pages.Mesas
{
    public class Create : PageModel
    {
        private AppDbContext _context;
        [BindProperty]
        public Mesa Mesa { get; set; } = new();
        
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
                _context.Add(Mesa);
                await _context.SaveChangesAsync();
                return RedirectToPage("/Mesas/Index");
            } catch(DbUpdateException)
            {
                return Page();
            }
        }
    }
}
