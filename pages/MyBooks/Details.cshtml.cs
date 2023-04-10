using Aula03.Data;
using Aula03.Models;
using Aula03.pages.HttpSession;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Aula03.pages.MyBooks
{
    public class Details : PageModel
    {
        private AppDbContext _context;
        [BindProperty]
        public BooksModel Book { get; set; } = new();
        public User user { get; set; }
        public int? userid;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public Details(IHttpContextAccessor httpContextAccessor, AppDbContext context)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            HttpContext httpContext = _httpContextAccessor.HttpContext;
            userid = httpContext.Session.GetInt32("UserID");
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Books == null)
                return NotFound();

            var eventModel = await _context.Books!.FirstOrDefaultAsync(e => e.BookID == id);
            
            if (eventModel == null) return NotFound();

            Book = eventModel;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            int bookId = int.Parse(Request.Form["BookID"]);
            Book = await _context.Books.FindAsync(bookId);

            Book.ReadPdfContent();
            
            return Page();
        }
    }
}
