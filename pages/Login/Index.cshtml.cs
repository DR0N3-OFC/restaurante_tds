using Aula03.Data;
using Aula03.Models;
using Aula03.pages.HttpSession;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Aula03.pages.Login
{
    public class Index : PageModel
    {
        private AppDbContext _context;
        [BindProperty]
        public User user { get; set; } = new();
        public List<User> UserList { get; set; } = new();

        public String? LoginName { get; set; }
        public String? LoginPass { get; set; }

        private readonly IHttpContextAccessor _httpContextAccessor;

        public Index(IHttpContextAccessor httpContextAccessor, AppDbContext context)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
        }



        public async Task<IActionResult> OnGetAsync()
        {
            UserList = await _context.Users!.ToListAsync();
            return Page();
        }




        public async Task<IActionResult> OnPostAsync(string submitType)
        {
            /*if (!ModelState.IsValid)
                return Page();
            */
            if(submitType == "Cadastrar")
            { 
                try
                {
                    _context.Add(user);
                    await _context.SaveChangesAsync();
                    return RedirectToPage("/Index");
                }
                catch (DbUpdateException)
                {
                    return Page();
                }
            }
            else if (submitType == "Login")
            {

                if (_context.Users == null)
                    return NotFound();

                LoginName = Request.Form["LoginName"];
                LoginPass = Request.Form["LoginPass"];

                var userModel = await _context.Users.FirstOrDefaultAsync(u => u.Name == LoginName);

                if (userModel == null)
                {
                    ModelState.AddModelError("", "Invalid username or password.");
                    return Page();
                }



                if (userModel.Password == LoginPass)
                {
                    HttpContext httpContext = _httpContextAccessor.HttpContext;
                    httpContext.Session.SetInt32("UserID", userModel.ID ?? 0);
                    if (userModel.IsAdm == true)
                        return RedirectToPage("/IndexAdm");
                    return Redirect($"/Index");
                }

                return Page();
            }

            else
            {

                return Page();
            }

        }
    }
}
