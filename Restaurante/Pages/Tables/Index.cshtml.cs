
using Aula03.Constants;
using Aula03.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Aula03.pages.Tables
{
    public class Index : PageModel
    {
        
        public List<Table> TablesList { get; set; } = new();

        public Index()
        {
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var httpClient = new HttpClient();
            var url = $"{Constant.url}Tables/";
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, url);
            var response = await httpClient.SendAsync(requestMessage);

            var content = await response.Content.ReadAsStringAsync();

            TablesList = JsonConvert.DeserializeObject<List<Table>>(content);

            return Page();
        }

        public async Task<IActionResult> OnPostReservarAsync([FromForm] int id)
        {
            var httpClient = new HttpClient();
            var url = $"{Constant.url}TableOpen/{id}";
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, url);
            var response = await httpClient.SendAsync(requestMessage);

            return Redirect("/Tables");
        }

        public async Task<IActionResult> OnPostFecharAsync([FromForm] int id)
        {
            var httpClient = new HttpClient();
            var url = $"{Constant.url}TableClose/{id}";
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, url);
            var response = await httpClient.SendAsync(requestMessage);

            return Redirect("/Tables");
        }
    }
}
