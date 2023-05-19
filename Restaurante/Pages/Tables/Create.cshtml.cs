
using Aula03.Constants;
using Aula03.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Text;

namespace Aula03.pages.Tables
{
    public class Create : PageModel
    {
        [BindProperty]
        public Table Table { get; set; } = new();
        
        public Create()
        {
        }
        
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();
            try
            {
                var httpClient = new HttpClient();
                var url = $"{Constant.url}Tables/";

                var requestMessage = new HttpRequestMessage(HttpMethod.Post, url);
                Table.Status = false;
                var jsonProduct = JsonConvert.SerializeObject(Table);
                requestMessage.Content = new StringContent(jsonProduct, Encoding.UTF8, "application/json");

                var response = await httpClient.SendAsync(requestMessage);

                return RedirectToPage("/Tables/Index");
            } catch(DbUpdateException)
            {
                return Page();
            }
        }
    }
}
