
using Aula03.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Text;

namespace Aula03.pages.Products
{
    public class Edit : PageModel
    {
        [BindProperty]
        public Product product { get; set; } = new();

		public Category? Category { get; set; } = new();
		public int? category_id { get; set; }

		public List<Category> CategoriesList { get; set; } = new();

		public Edit()
        {
        }

		public async Task<IActionResult> OnGetAsync(int? id)
        {
            var httpClient = new HttpClient();
            var url = $"https://localhost:7048/Products/{id}";
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, url);
            var response = await httpClient.SendAsync(requestMessage);

            var content = await response.Content.ReadAsStringAsync();

            var eventModel = JsonConvert.DeserializeObject<Product>(content);

            if (eventModel == null) return NotFound();

            product = eventModel;

            return Page();
        }

		public async Task<IActionResult> OnPostAsync(int id)
        {

			var httpClient = new HttpClient();
			var url = $"https://localhost:7048/Products/{id}";
			var requestMessage = new HttpRequestMessage(HttpMethod.Get, url);
			var response = await httpClient.SendAsync(requestMessage);

			var content = await response.Content.ReadAsStringAsync();

            var productToUpdate = JsonConvert.DeserializeObject<Product>(content);

            if (productToUpdate == null)
                return NotFound();

			productToUpdate.Name = product.Name;
            productToUpdate.Price = product.Price;
            productToUpdate.Category = Category;

            try
            {
                httpClient = new HttpClient();
                url = $"https://localhost:7048/Products/{id}";

                requestMessage = new HttpRequestMessage(HttpMethod.Put, url);
                var jsonProduct = JsonConvert.SerializeObject(productToUpdate);
                requestMessage.Content = new StringContent(jsonProduct, Encoding.UTF8, "application/json");

                response = await httpClient.SendAsync(requestMessage);
                return RedirectToPage("/Products/Index");
            }   catch(DbUpdateException)
			{
                return Page();
			}

        }
    }
}
