
using Aula03.Constants;
using Aula03.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Aula03.pages.Waiters
{
    public class Create : PageModel
    {
        [BindProperty]
		public Waiter Waiter { get; set; } = new();
        
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
                var url = $"{Constant.url}Waiters/";

                var requestMessage = new HttpRequestMessage(HttpMethod.Post, url);
                var jsonProduct = JsonConvert.SerializeObject(Waiter);
                requestMessage.Content = new StringContent(jsonProduct, Encoding.UTF8, "application/json");

                var response = await httpClient.SendAsync(requestMessage);

                return RedirectToPage("/Waiters/Index");
            } catch(DbUpdateException)
            {
                return Page();
            }
        }
    }
}
