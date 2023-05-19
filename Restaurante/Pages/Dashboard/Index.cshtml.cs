
using Aula03.Constants;
using Aula03.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Aula03.pages.Dashboard
{
    public class Index : PageModel
    {

        public List<Waiter> WaitersList { get; set; } = new();
        public List<Table> TablesList { get; set; } = new();
        public List<Product> ProductsList { get; set; } = new();
        public List<Category> CategoriesList { get; set; } = new();
        public List<Service> ServicesList { get; set; } = new();

        HttpClient httpClient;
        string url;
        HttpRequestMessage requestMessage;
        HttpResponseMessage response;
        string content;

        public Index()
        {
        }
        public async Task<IActionResult> OnGetAsync()
        {
            httpClient = new HttpClient();
            url = $"{Constant.url}Tables/";
            requestMessage = new HttpRequestMessage(HttpMethod.Get, url);
            response = await httpClient.SendAsync(requestMessage);
            content = await response.Content.ReadAsStringAsync();
            TablesList = JsonConvert.DeserializeObject<List<Table>>(content);

            httpClient = new HttpClient();
            url = $"{Constant.url}Waiters/";
            requestMessage = new HttpRequestMessage(HttpMethod.Get, url);
            response = await httpClient.SendAsync(requestMessage);
            content = await response.Content.ReadAsStringAsync();
            WaitersList = JsonConvert.DeserializeObject<List<Waiter>>(content);

            httpClient = new HttpClient();
            url = $"{Constant.url}Categories/";
            requestMessage = new HttpRequestMessage(HttpMethod.Get, url);
            response = await httpClient.SendAsync(requestMessage);
            content = await response.Content.ReadAsStringAsync();
            CategoriesList = JsonConvert.DeserializeObject<List<Category>>(content);

            httpClient = new HttpClient();
            url = $"{Constant.url}Products/";
            requestMessage = new HttpRequestMessage(HttpMethod.Get, url);
            response = await httpClient.SendAsync(requestMessage);
            content = await response.Content.ReadAsStringAsync();
            ProductsList = JsonConvert.DeserializeObject<List<Product>>(content);

            httpClient = new HttpClient();
            url = $"{Constant.url}Services/";
            requestMessage = new HttpRequestMessage(HttpMethod.Get, url);
            response = await httpClient.SendAsync(requestMessage);
            content = await response.Content.ReadAsStringAsync();
            ServicesList = JsonConvert.DeserializeObject<List<Service>>(content);

            return Page();
        }
    }
}
