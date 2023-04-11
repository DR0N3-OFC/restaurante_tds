using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Aula03.pages
{
    public class Index : PageModel
    {

        private readonly IHttpContextAccessor _httpContextAccessor;

        //public Index(ILogger<Index> logger)
        public Index(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;

            HttpContext httpContext = _httpContextAccessor.HttpContext;
            //Reserva
            httpContext.Session.SetInt32("GarcomID", 1);
            httpContext.Session.SetInt32("MesaID", 1);
            httpContext.Session.SetInt32("AtendimentoID", 1);
            httpContext.Session.SetString("Checked", "F");

            //Create Produtos
            httpContext.Session.SetInt32("CategoriaID", 1);
        }

    }
}
