
using Aula03.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Aula03.pages.Categories
{
    public class Index : PageModel
    {

        public List<Category> CategoriesList { get; set; } = new();

        public Index()
        {
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var httpClient = new HttpClient();
            var url = "https://localhost:7048/Categories/";
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, url);
            var response = await httpClient.SendAsync(requestMessage);

            var content = await response.Content.ReadAsStringAsync();

			CategoriesList = JsonConvert.DeserializeObject<List<Category>>(content);
            
            return Page();
        }
    }
}
