
using Aula03.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Aula03.pages.Waiters
{
    public class Index : PageModel
    {

        public List<Waiter> WaitersList { get; set; } = new();

        public Index()
        {
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var httpClient = new HttpClient();
            var url = "https://localhost:7048/Waiters/";
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, url);
            var response = await httpClient.SendAsync(requestMessage);

            var content = await response.Content.ReadAsStringAsync();

            WaitersList = JsonConvert.DeserializeObject<List<Waiter>>(content);
            
            return Page();
        }
    }
}
