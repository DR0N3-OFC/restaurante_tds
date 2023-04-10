using Aula03.Data;
using Aula03.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Aula03.pages.MyBooks
{
    public class Index : PageModel
    {
        AppDbContext _context;
        public int? userid;
        public List<BooksModel> BooksList { get; set; } = new();
        public User? user;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public Index(IHttpContextAccessor httpContextAccessor, AppDbContext context)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            HttpContext httpContext = _httpContextAccessor.HttpContext;
            userid = httpContext.Session.GetInt32("UserID");

        }
        public async Task<IActionResult> OnGetAsync()
        {
            user = await _context.Users!.FirstOrDefaultAsync(e => e.ID == userid);
            
            if (user == null) return NotFound();

            var books = await _context.Entry(user).Collection(u => u.Books).Query().ToListAsync();

            if (books==null) return NotFound();
            
            BooksList = user.Books!.ToList();
            return Page();
        }        
    }
}
