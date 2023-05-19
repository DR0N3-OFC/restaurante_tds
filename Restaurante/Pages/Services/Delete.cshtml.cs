
using Aula03.Constants;
using Aula03.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Aula03.pages.Services
{
    public class Delete : PageModel
    {
        [BindProperty]
        public Service? Service { get; set; } = new();

        public Delete()
        {
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {

            var httpClient = new HttpClient();
            var url = $"{Constant.url}Services/{id}";
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, url);
            var response = await httpClient.SendAsync(requestMessage);
            var content = await response.Content.ReadAsStringAsync();
            Service = JsonConvert.DeserializeObject<Service>(content);

            if (Service == null) return NotFound();


            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {


            try
            {
                var httpClient = new HttpClient();
                var url = $"{Constant.url}Services/{id}";
                var requestMessage = new HttpRequestMessage(HttpMethod.Delete, url);
                var response = await httpClient.SendAsync(requestMessage);

                return RedirectToPage("/Services/Index");
            } catch(DbUpdateException)
            {
                return Page();
            }

        }
    }
}
