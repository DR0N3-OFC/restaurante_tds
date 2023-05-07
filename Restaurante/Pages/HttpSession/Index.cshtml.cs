using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Aula03.pages.HttpSession
{
    public class SessionPage : PageModel
    {
        public HttpContext httpContext => this.HttpContext;
    }
}
