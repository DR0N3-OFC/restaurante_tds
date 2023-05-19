
using Aula03.Constants;
using Aula03.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Text;

namespace Aula03.pages.Waiters
{
    public class Edit : PageModel
    {
        [BindProperty]
        public Waiter waiter { get; set; } = new();
        
        public Edit()
        {
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {

            var httpClient = new HttpClient();
            var url = $"{Constant.url}Waiters/{id}";
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, url);
            var response = await httpClient.SendAsync(requestMessage);

            var content = await response.Content.ReadAsStringAsync();

            var waiterModel = JsonConvert.DeserializeObject<Waiter>(content);

            if (waiterModel == null) return NotFound();

			waiter = waiterModel;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            
            var httpClient = new HttpClient();
            var url = $"{Constant.url}Waiters/{id}";
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, url);
            var response = await httpClient.SendAsync(requestMessage);

            var content = await response.Content.ReadAsStringAsync();

            var waiterToUpdate = JsonConvert.DeserializeObject<Waiter>(content);

            if (waiterToUpdate == null)
                return NotFound();

            waiterToUpdate.Name = waiter.Name;
            waiterToUpdate.LastName = waiter.LastName;
            waiterToUpdate.BirthDate = waiter.BirthDate;
            waiterToUpdate.Cellphone = waiter.Cellphone;

            try
            {
                httpClient = new HttpClient();
                url = $"{Constant.url}Waiters/{id}";

                requestMessage = new HttpRequestMessage(HttpMethod.Put, url);
                var jsonProduct = JsonConvert.SerializeObject(waiterToUpdate);
                requestMessage.Content = new StringContent(jsonProduct, Encoding.UTF8, "application/json");

                response = await httpClient.SendAsync(requestMessage);

                return RedirectToPage("/Waiters/Index");
            }   catch(DbUpdateException)
			{
                return Page();
			}

        }
    }
}
