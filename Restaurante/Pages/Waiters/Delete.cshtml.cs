
using Aula03.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Aula03.pages.Waiters
{
    public class Delete : PageModel
    {
        [BindProperty]
        public Waiter Waiter { get; set; } = new();

        public Delete()
        {
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {

            var httpClient = new HttpClient();
            var url = $"https://localhost:7048/Waiters/{id}";
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, url);
            var response = await httpClient.SendAsync(requestMessage);

            var content = await response.Content.ReadAsStringAsync();

            var eventModel = JsonConvert.DeserializeObject<Waiter>(content);

            if (eventModel == null) return NotFound();

			Waiter = eventModel;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {

            try
            {
                var httpClient = new HttpClient();
                var url = $"https://localhost:7048/Waiters/{id}";
                var requestMessage = new HttpRequestMessage(HttpMethod.Delete, url);
                var response = await httpClient.SendAsync(requestMessage);
                return RedirectToPage("/Waiters/Index");
            } catch(DbUpdateException)
            {
                return Page();
            }

        }
    }
}
