using Aula03.Data;
using Aula03.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Aula03.pages.Events
{
    public class Edit : PageModel
    {
        private AppDbContext _context;
        [BindProperty]
        public EventsModel EventModel { get; set; } = new();
        
        public Edit(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Events == null)
                return NotFound();

            var eventModel = await _context.Events.FirstOrDefaultAsync(e => e.EventID == id);

            if (eventModel == null) return NotFound();

            EventModel = eventModel;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (!ModelState.IsValid)
                return Page();

            var eventToUpdate = await _context.Events!.FindAsync(id);

            if (eventToUpdate == null)
                return NotFound();

            eventToUpdate.Name = EventModel.Name;
            eventToUpdate.Desc = EventModel.Desc;
            eventToUpdate.Date = EventModel.Date;

			try
            { 
                await _context.SaveChangesAsync();
                return RedirectToPage("/Events/Index");
            }   catch(DbUpdateException)
			{
                return Page();
			}

        }
    }
}
