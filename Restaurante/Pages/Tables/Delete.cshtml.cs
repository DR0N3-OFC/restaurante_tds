
using Aula03.Constants;
using Aula03.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Aula03.pages.Tables
{
    public class Delete : PageModel
    {
        [BindProperty]
        public Table Table { get; set; } = new();

        public Delete()
        {
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {

            var httpClient = new HttpClient();
            var url = $"{Constant.url}Tables/{id}";
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, url);
            var response = await httpClient.SendAsync(requestMessage);

            var content = await response.Content.ReadAsStringAsync();

            var eventModel = JsonConvert.DeserializeObject<Table>(content);

            if (eventModel == null) return NotFound();

            Table = eventModel;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            
            
            try
            {
                var httpClient = new HttpClient();
                var url = $"{Constant.url}Tables/{id}";
                var requestMessage = new HttpRequestMessage(HttpMethod.Delete, url);
                var response = await httpClient.SendAsync(requestMessage);
                return RedirectToPage("/Tables/Index");
            } catch(DbUpdateException)
            {
                return Page();
            }

        }
    }
}
