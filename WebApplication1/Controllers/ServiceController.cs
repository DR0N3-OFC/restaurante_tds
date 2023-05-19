using Aula03.Data;
using Aula03.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Aula03.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServiceController : ControllerBase
    {
        [HttpGet]
        [Route("/Services/")]
        public IActionResult Get([FromServices] AppDbContext context)
        {
            var services = context.Services!.ToList();

            foreach(var aux in services)
            {
                context.Entry(aux).Reference(o => o.Waiter).Load();
                context.Entry(aux).Reference(o => o.Table).Load();
                context.Entry(aux)
                       .Collection(o => o.ServiceProducts)
                       .Query()
                       .Include(o => o.Product)
                       .ThenInclude(p => p.Category)
                       .Load();


            }
            return Ok(context.Services!.ToList());
        }


        [HttpGet("/Services/{id:int}")]
        public IActionResult GetByID([FromRoute] int id, [FromServices] AppDbContext context)
        {

            var service = context.Services!
                                    .Include(o => o.Waiter)
                                    .Include(o => o.Table)
                                    .FirstOrDefault(o => o.ServiceID == id);
            if (service != null)
            {
                context.Entry(service)
                       .Collection(p => p.ServiceProducts)
                       .Query()
                       .Include(p => p.Product)
                       .Load();
                foreach (var sp in service.ServiceProducts)
                {
                    // Load the Product navigation property
                    context.Entry(sp)
                        .Reference(p => p.Product)
                        .Load();

                    // Load the Category navigation property for the Product
                    context.Entry(sp.Product)
                        .Reference(p => p.Category)
                        .Load();
                }
            }

            if (service == null)
            {
                return NotFound();
            }
            return Ok(service);
        }

        [HttpGet("/ServiceProducts/{id:int}")]
        public IActionResult GetServiceProductByID([FromRoute] int id, [FromServices] AppDbContext context)
        {

            var service = context.ServiceProducts!
                                    .FirstOrDefault(o => o.ServiceProductID == id);
            
            return Ok(service);
        }


        [HttpGet("/LastService/")]
        public IActionResult GetLastService([FromServices] AppDbContext context)
        {

            var service = context.Services!
                                    .OrderByDescending(o => o.ServiceID)
                                    .Include(o => o.Waiter)
                                    .Include(o => o.Table)
                                    .FirstOrDefault();

            if(service != null)
            { 
                context.Entry(service)
                       .Collection(p => p.ServiceProducts)
                       .Query()
                       .Include(p => p.Product)
                       .Load();
                foreach (var sp in service.ServiceProducts)
                {
                    // Load the Product navigation property
                    context.Entry(sp)
                        .Reference(p => p.Product)
                        .Load();

                    // Load the Category navigation property for the Product
                    context.Entry(sp.Product)
                        .Reference(p => p.Category)
                        .Load();
                }
            }

            if (service == null)
            {
                return NotFound();
            }
            return Ok(service);
        }


        [HttpPost("/Services/")]
        public IActionResult Post([FromBody] Service service, [FromServices] AppDbContext context)
        {

            var existingProduct = context.Services!.SingleOrDefault(o => o.ServiceID == service.ServiceID);
            var table = context.Tables!.SingleOrDefault(o => o.TableID == service.Table.TableID);
            var waiter = context.Waiters!.SingleOrDefault(c => c.WaiterID == service.Waiter!.WaiterID);

            if (existingProduct != null)
            {
                var new_product = new Service();
                new_product.InitDate = service.InitDate;
                new_product.FinishDate = service.FinishDate;

                service = new_product;
            }
            service.Table = table;
            service.Waiter = waiter;
            context.Services!.Add(service);
            context.SaveChanges();
            return Created($"/{service.ServiceID}", service);
        }



        [HttpPut("/Services/{id:int}")]
        public IActionResult Put([FromRoute] int id, [FromBody] Service service, [FromServices] AppDbContext context)
        {

            var dBservice = context.Services!
                                    .Include(o => o.Waiter)
                                    .Include(o => o.Table)
                                    .Include(o => o.ServiceProducts)
                                    .FirstOrDefault(o => o.ServiceID == id);

            var table = context.Tables!.SingleOrDefault(o => o.TableID == service.Table.TableID);
            var waiter = context.Waiters!.SingleOrDefault(c => c.WaiterID == service.Waiter!.WaiterID);




            for (int i = 0; i < service.ServiceProducts.Count; i++)
            {
                var sp = service.ServiceProducts[i];
                var product = context.Products!
                                        .Include(o => o.Category)
                                        .FirstOrDefault(o => o.ProductID == sp.Product.ProductID);
                sp.Product = product;

                if(sp.ServiceProductID==null)
                { 
                    context.ServiceProducts!.Add(sp);
                    context.SaveChanges();
                    sp = context.ServiceProducts!
                                        .OrderByDescending(o => o.ServiceProductID)
                                        .Include(o => o.Product)
                                        .FirstOrDefault();
                    service.ServiceProducts[i] = sp;
                } else
                {
                    service.ServiceProducts[i] = context.ServiceProducts!
                                        .Include(o => o.Product)
                                        .FirstOrDefault(o => o.ServiceProductID == sp.ServiceProductID);
                }

                context.Entry(sp.Product)
                    .Reference(p => p.Category)
                    .Load();

                var category = context.Categories!
                                        .FirstOrDefault(o => o.CategoryID==sp!.Product!.Category!.CategoryID);
                service.ServiceProducts[i].Product.Category = category;

                // code to access properties of the product at index i
            }


            if (dBservice == null)
            {
                return NotFound();
            }

            dBservice.ServiceProducts.Clear();
            dBservice.ServiceProducts = service.ServiceProducts;
            dBservice.Table = table;
            dBservice.Waiter = waiter;
            dBservice.InitDate = service.InitDate;
            dBservice.FinishDate = service.FinishDate;



            context.Services!.Update(dBservice);
            context.SaveChanges();
            return Ok(dBservice);
        }


        [HttpDelete("/Services/{id:int}")]
        public IActionResult Delete([FromRoute] int id, [FromServices] AppDbContext context)
        {

            var products = context.Services!.FirstOrDefault(o => o.ServiceID == id);
            if (products == null)
            {
                return NotFound();
            }

            context.Services!.Remove(products);
            context.SaveChanges();
            return Ok(products);
        }
    }
}
