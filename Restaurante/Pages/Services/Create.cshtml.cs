using Aula03.Constants;
using Aula03.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Aula03.pages.Services
{
    public class Create : PageModel
    {
        HttpContext httpContext;
        [BindProperty]
        public Service Service { get; set; } = new();
        public Table? Table { get; set; }
        public Waiter? Waiter { get; set; }
        public List<Waiter> WaitersList { get; set; }
        public List<Product>? ProductsList { get; set; } = new();
        public List<Product>? ServiceProducts { get; set; } = new();
        public List<Table>? TablesList { get; set; } = new();
        public Product? Product { get; set; }
        public ServiceProduct? ServiceProduct { get; set; }
        public Category? Category { get; set; }

        [DisplayFormat(DataFormatString = "R${0:N2}")]
        public double? Total { get; set; }
        public int? product_id { get; set; }
        public int? category_id { get; set; }
        public int? table_id { get; set; }
        public int? waiter_id { get; set; }
        public int? service_id { get; set; }
        public string? check { get; set; }

        HttpClient httpClient;
        string url;
        HttpRequestMessage requestMessage;
        HttpResponseMessage response;
        string content;


        private readonly IHttpContextAccessor _httpContextAccessor;

        public Create(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;


            //Recupera Dados do Atendimento
            httpContext = _httpContextAccessor.HttpContext!;

            service_id = httpContext!.Session.GetInt32("AtendimentoID");
            check = httpContext!.Session.GetString("Checked");

            if (check == "T")
            {
                httpClient = new HttpClient();
                url = $"{Constant.url}Services/{service_id}";
                requestMessage = new HttpRequestMessage(HttpMethod.Get, url);
                response = httpClient.Send(requestMessage);
                content = response.Content.ReadAsStringAsync().Result;
                Service = JsonConvert.DeserializeObject<Service>(content);
            }

            //Recupera a Lista de Produtos
            httpClient = new HttpClient();
            url = $"{Constant.url}Products/";
            requestMessage = new HttpRequestMessage(HttpMethod.Get, url);
            response = httpClient.Send(requestMessage);
            content = response.Content.ReadAsStringAsync().Result;
            ProductsList = JsonConvert.DeserializeObject<List<Product>>(content);


            //Recupera a Lista de Garçons
            httpClient = new HttpClient();
            url = $"{Constant.url}Waiters/";
            requestMessage = new HttpRequestMessage(HttpMethod.Get, url);
            response = httpClient.Send(requestMessage);
            content = response.Content.ReadAsStringAsync().Result;
            WaitersList = JsonConvert.DeserializeObject<List<Waiter>>(content);


            httpClient = new HttpClient();
            url = $"{Constant.url}Tables/";
            requestMessage = new HttpRequestMessage(HttpMethod.Get, url);
            response = httpClient.Send(requestMessage);
            content = response.Content.ReadAsStringAsync().Result;
            TablesList = JsonConvert.DeserializeObject<List<Table>>(content);



        }

        void resetState()
        {
            httpContext!.Session.SetInt32("AtendimentoID", 1);
            httpContext!.Session.SetString("Checked", "F");
            httpContext!.Session.SetInt32("GarcomID", 1);
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {


            httpClient = new HttpClient();
            url = $"{Constant.url}Tables/1";
            requestMessage = new HttpRequestMessage(HttpMethod.Get, url);
            response = await httpClient.SendAsync(requestMessage);
            content = await response.Content.ReadAsStringAsync();
            Table = JsonConvert.DeserializeObject<Table>(content);


            httpClient = new HttpClient();
            url = $"{Constant.url}Waiters/1";
            requestMessage = new HttpRequestMessage(HttpMethod.Get, url);
            response = await httpClient.SendAsync(requestMessage);
            content = await response.Content.ReadAsStringAsync();
            Waiter = JsonConvert.DeserializeObject<Waiter>(content);



            httpContext!.Session.SetInt32("MesaID", 1);
            //Table.LiberationTime = DateTime.Now.AddMinutes(1);
            Table.LiberationTime = DateTime.Now.AddSeconds(5);

            httpClient = new HttpClient();
            url = $"{Constant.url}Tables/1";
            requestMessage = new HttpRequestMessage(HttpMethod.Put, url);
            var jsonProduct = JsonConvert.SerializeObject(Table);
            requestMessage.Content = new StringContent(jsonProduct, Encoding.UTF8, "application/json");
            response = await httpClient.SendAsync(requestMessage);

            table_id = id;



            Service.Table = Table;
            Service.Waiter = Waiter;
            Service.InitDate = DateTime.Now;
            Service.FinishDate = DateTime.Now.AddMinutes(1);



            httpClient = new HttpClient();
            url = $"{Constant.url}Services/";
            requestMessage = new HttpRequestMessage(HttpMethod.Post, url);
            jsonProduct = JsonConvert.SerializeObject(Service);
            requestMessage.Content = new StringContent(jsonProduct, Encoding.UTF8, "application/json");
            response = await httpClient.SendAsync(requestMessage);


            Console.WriteLine("");
            //_context.Add(Service);
            //await _context.SaveChangesAsync();



            httpClient = new HttpClient();
            url = $"{Constant.url}LastService/";
            requestMessage = new HttpRequestMessage(HttpMethod.Get, url);
            response = await httpClient.SendAsync(requestMessage);
            content = await response.Content.ReadAsStringAsync();
            Service = JsonConvert.DeserializeObject<Service>(content);


            httpContext!.Session.SetInt32("AtendimentoID", Service!.ServiceID ?? 1);
            httpContext!.Session.SetString("Checked", "T");
            


            return Page();
        }

        public async Task<IActionResult> OnPostAdicionarAsync([FromForm] int id, [FromForm] int amount)
        {
            httpClient = new HttpClient();
            url = $"{Constant.url}Products/{id}";
            requestMessage = new HttpRequestMessage(HttpMethod.Get, url);
            response = await httpClient.SendAsync(requestMessage);
            content = await response.Content.ReadAsStringAsync();
            Product = JsonConvert.DeserializeObject<Product>(content);
            //Product = await _context.Products!.Include(p => p.Category).FirstOrDefaultAsync(e => e.ProductID == id);


            httpClient = new HttpClient();
            url = $"{Constant.url}LastService/";
            requestMessage = new HttpRequestMessage(HttpMethod.Get, url);
            response = httpClient.Send(requestMessage);
            content = response.Content.ReadAsStringAsync().Result;
            Service = JsonConvert.DeserializeObject<Service>(content);


            var service_product = new ServiceProduct[]
            {
                new ServiceProduct{
                    Product = Product,
                    Amount = amount
                }
            };

            Service!.ServiceProducts!.Add(service_product[0]);
            service_id = httpContext!.Session.GetInt32("AtendimentoID");


            httpClient = new HttpClient();
            url = $"{Constant.url}Services/{service_id}";
            requestMessage = new HttpRequestMessage(HttpMethod.Put, url);
            var jsonProduct = JsonConvert.SerializeObject(Service);
            requestMessage.Content = new StringContent(jsonProduct, Encoding.UTF8, "application/json");
            response = await httpClient.SendAsync(requestMessage);
            content = response.Content.ReadAsStringAsync().Result;
            Service = JsonConvert.DeserializeObject<Service>(content);


            /*
            category_id = Product.Category!.CategoryID;
            Category = await _context.Categories!.FirstOrDefaultAsync(e => e.CategoryID == category_id);

            Product.Estatistica = Product.Estatistica + Product.Price;
            _context.Update(Product);

            Category.Estatistica = Category.Estatistica + Product.Price;
            _context.Update(Category);

            await _context.SaveChangesAsync();
            Total = Service.Products!.Sum(p => p.Price);
            */

            //Recupera a Lista de Produtos

            Total = Service.ServiceProducts.Sum(o => o.Product.Price*o.Amount);

            return Page();
        }


        public async Task<IActionResult> OnPostDeletarAsync([FromForm] int id)
        {
            var httpClient = new HttpClient();
            var url = $"{Constant.url}ServiceProducts/{id}";
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, url);
            var response = await httpClient.SendAsync(requestMessage);
            var content = await response.Content.ReadAsStringAsync();
            ServiceProduct = JsonConvert.DeserializeObject<ServiceProduct>(content);



            httpClient = new HttpClient();
            url = $"{Constant.url}Services/{service_id}";
            requestMessage = new HttpRequestMessage(HttpMethod.Get, url);
            response = httpClient.Send(requestMessage);
            content = response.Content.ReadAsStringAsync().Result;
            Service = JsonConvert.DeserializeObject<Service>(content);



            /*
            category_id = Product.Category!.CategoryID;
            Category = await _context.Categories!.FirstOrDefaultAsync(e => e.CategoryID == category_id);


            Product.Estatistica = Product.Estatistica - Product.Price;
            _context.Update(Product);

            Category.Estatistica = Category.Estatistica - Product.Price;
            _context.Update(Category);
            */



            //Service!.Products!.Remove(Product);
            var productsToRemove = new List<ServiceProduct>();

            foreach (var product in Service.ServiceProducts!)
            {
                if (product.ServiceProductID == ServiceProduct.ServiceProductID)
                {
                    productsToRemove.Add(product);
                }
            }

            foreach (var product in productsToRemove)
            {
                Service.ServiceProducts.Remove(product);
            }


            httpClient = new HttpClient();
            url = $"{Constant.url}Services/{service_id}";
            requestMessage = new HttpRequestMessage(HttpMethod.Put, url);
            var jsonProduct = JsonConvert.SerializeObject(Service);
            requestMessage.Content = new StringContent(jsonProduct, Encoding.UTF8, "application/json");
            response = await httpClient.SendAsync(requestMessage);

            Total = Service.ServiceProducts.Sum(o => o.Product.Price*o.Amount);
            return Page();
        }

        public async Task<IActionResult> OnPostDesignarAsync([FromForm] int id)
        {
            httpClient = new HttpClient();
            url = $"{Constant.url}Waiters/{id}";
            requestMessage = new HttpRequestMessage(HttpMethod.Get, url);
            response = await httpClient.SendAsync(requestMessage);
            content = await response.Content.ReadAsStringAsync();
            Waiter = JsonConvert.DeserializeObject<Waiter>(content);


            httpClient = new HttpClient();
            url = $"{Constant.url}LastService/";
            requestMessage = new HttpRequestMessage(HttpMethod.Get, url);
            response = httpClient.Send(requestMessage);
            content = response.Content.ReadAsStringAsync().Result;
            Service = JsonConvert.DeserializeObject<Service>(content);


            httpContext!.Session.SetInt32("GarcomID", id);
            Service!.Waiter = Waiter;

            service_id = httpContext!.Session.GetInt32("AtendimentoID");

            httpClient = new HttpClient();
            url = $"{Constant.url}Services/{service_id}";
            requestMessage = new HttpRequestMessage(HttpMethod.Put, url);
            var jsonProduct = JsonConvert.SerializeObject(Service);
            requestMessage.Content = new StringContent(jsonProduct, Encoding.UTF8, "application/json");
            response = await httpClient.SendAsync(requestMessage);

            return Page();
        }

        public async Task<IActionResult> OnPostMesarAsync([FromForm] int id)
        {
            httpClient = new HttpClient();
            url = $"{Constant.url}Tables/{id}";
            requestMessage = new HttpRequestMessage(HttpMethod.Get, url);
            response = await httpClient.SendAsync(requestMessage);
            content = await response.Content.ReadAsStringAsync();
            Table = JsonConvert.DeserializeObject<Table>(content);


            httpClient = new HttpClient();
            url = $"{Constant.url}LastService/";
            requestMessage = new HttpRequestMessage(HttpMethod.Get, url);
            response = httpClient.Send(requestMessage);
            content = response.Content.ReadAsStringAsync().Result;
            Service = JsonConvert.DeserializeObject<Service>(content);


            httpContext!.Session.SetInt32("MesaID", id);
            Service!.Table = Table;

            service_id = httpContext!.Session.GetInt32("AtendimentoID");

            httpClient = new HttpClient();
            url = $"{Constant.url}Services/{service_id}";
            requestMessage = new HttpRequestMessage(HttpMethod.Put, url);
            var jsonProduct = JsonConvert.SerializeObject(Service);
            requestMessage.Content = new StringContent(jsonProduct, Encoding.UTF8, "application/json");
            response = await httpClient.SendAsync(requestMessage);

            return Page();
        }


        public async Task<IActionResult> OnPostCancelarAsync([FromForm] int id)
        {

            table_id = httpContext!.Session.GetInt32("MesaID");


            httpClient = new HttpClient();
            url = $"{Constant.url}Tables/{table_id}";
            requestMessage = new HttpRequestMessage(HttpMethod.Get, url);
            response = await httpClient.SendAsync(requestMessage);
            content = await response.Content.ReadAsStringAsync();
            Table = JsonConvert.DeserializeObject<Table>(content);

            Table.LiberationTime = DateTime.Now;


            httpClient = new HttpClient();
            url = $"{Constant.url}Services/{service_id}";
            requestMessage = new HttpRequestMessage(HttpMethod.Delete, url);
            response = await httpClient.SendAsync(requestMessage);



            resetState();

            return Redirect("/Services");
        }


        public async Task<IActionResult> OnPostSalvarAsync([FromForm] int id)
        {

            httpClient = new HttpClient();
            url = $"{Constant.url}Services/{service_id}";
            requestMessage = new HttpRequestMessage(HttpMethod.Get, url);
            response = httpClient.Send(requestMessage);
            content = response.Content.ReadAsStringAsync().Result;
            Service = JsonConvert.DeserializeObject<Service>(content);

            //Total = Service!.Products!.Sum(p => p.Price);


            if (Service!.Waiter!.WaiterID == 1)
            {
                ModelState.AddModelError("", "Erro: Precisa escolher um garçom.");
                return Page();
            }
            if (Service!.Table!.TableID == 1)
            {
                ModelState.AddModelError("", "Erro: Precisa escolher uma mesa.");
                return Page();
            }
            if (Service.ServiceProducts!.Count == 0)
            {
                ModelState.AddModelError("", "Erro: Nenhum produto foi adicionado ainda.");
                return Page();
            }

            /*
            waiter_id = Service.Waiter.WaiterID;
            Waiter = await _context.Waiters!.FirstOrDefaultAsync(g => g.WaiterID == waiter_id);
            Waiter!.Estatistica = Waiter.Estatistica + Total;
            _context.Update(Waiter);

            table_id = Service.Table.TableID;
            Table = await _context.Tables!.FirstOrDefaultAsync(g => g.TableID == table_id);
            Table.Estatistica = Table.Estatistica + Total;
            _context.Update(Table);
            */

            //await _context.SaveChangesAsync();

            resetState();

            return Redirect("/Services");
        }
    }
}
