using Aula03.Data;
using Aula03.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Aula03.pages.Events
{
    public class Index : PageModel
    {
        private AppDbContext _context;
        public List<EventsModel> EventsList { get; set; } = new();
        
        public Index(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            EventsList = await _context.Events!.ToListAsync();
            return Page();
        }
    }
}
