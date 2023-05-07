
using Aula03.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Text;

namespace Aula03.pages.Products
{
    public class Create : PageModel
    {
        [BindProperty]
        public Product Product { get; set; } = new();
        public Category? Category { get; set; } = new();
        public int? category_id  { get; set; }

        public List<Category> CategoriesList { get; set; } = new();


        public Create()
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
        
        public async Task<IActionResult> OnPostAsync()
        {
            category_id = int.Parse(Request.Form["CatID"]);

            var httpClient = new HttpClient();
            var url = $"https://localhost:7048/Categories/{category_id}";
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, url);
            var response = await httpClient.SendAsync(requestMessage);

            var content = await response.Content.ReadAsStringAsync();

            Category = JsonConvert.DeserializeObject<Category>(content);


            if (!ModelState.IsValid)
            { 
                ModelState.AddModelError("", "Erro: Nenhum produto foi adicionado ainda.");
                return NotFound();
            }
            try
            {
                Product.Category = Category;
                httpClient = new HttpClient();
                url = $"https://localhost:7048/Products/";

                requestMessage = new HttpRequestMessage(HttpMethod.Post, url);
                var jsonProduct = JsonConvert.SerializeObject(Product);
                requestMessage.Content = new StringContent(jsonProduct, Encoding.UTF8, "application/json");

                response = await httpClient.SendAsync(requestMessage);

                return RedirectToPage("/Products/Index");
            } catch(DbUpdateException)
            {
                ModelState.AddModelError("", "Erro: Banco de Dados.");
                return Page();
            }
            return NotFound();
        }

    }
}
