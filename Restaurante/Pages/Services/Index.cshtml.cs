using Aula03.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace Aula03.pages.Services
{
    public class Index : PageModel
    {
        public List<Service> ServicesList { get; set; } = new();

        public Index()
        {
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var httpClient = new HttpClient();
            var url = "https://localhost:7048/Services/";
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, url);
            var response = await httpClient.SendAsync(requestMessage);

            var content = await response.Content.ReadAsStringAsync();

            ServicesList = JsonConvert.DeserializeObject<List<Service>>(content);


            return Page();
        }
    }
}
