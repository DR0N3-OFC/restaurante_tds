
using Aula03.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Text;

namespace Aula03.pages.Categories
{
    public class Edit : PageModel
    {
        [BindProperty]
        public Category Category { get; set; } = new();
        
        public Edit()
        {
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {

            var httpClient = new HttpClient();
            var url = $"https://localhost:7048/Categories/{id}";
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, url);
            var response = await httpClient.SendAsync(requestMessage);

            var content = await response.Content.ReadAsStringAsync();

            var categoryModel = JsonConvert.DeserializeObject<Category>(content);

            if (categoryModel == null) return NotFound();

			Category = categoryModel;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            
            var httpClient = new HttpClient();
            var url = $"https://localhost:7048/Categories/{id}";
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, url);
            var response = await httpClient.SendAsync(requestMessage);

            var content = await response.Content.ReadAsStringAsync();

            var categoryToUpdate = JsonConvert.DeserializeObject<Category>(content);

            if (categoryToUpdate == null)
                return NotFound();

			categoryToUpdate.Name = Category.Name;

            try
            {
                httpClient = new HttpClient();
                url = $"https://localhost:7048/Categories/{id}";

                requestMessage = new HttpRequestMessage(HttpMethod.Put, url);
                var jsonProduct = JsonConvert.SerializeObject(categoryToUpdate);
                requestMessage.Content = new StringContent(jsonProduct, Encoding.UTF8, "application/json");

                response = await httpClient.SendAsync(requestMessage);

                return RedirectToPage("/Categories/Index");
            }   catch(DbUpdateException)
			{
                return Page();
			}

        }
    }
}
