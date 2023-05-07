using Aula03.Data;
using Aula03.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Aula03.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WaiterController : ControllerBase
    {
        [HttpGet]
        [Route("/Waiters/")]
        public IActionResult Get([FromServices] AppDbContext context)
        {
            
            return Ok(context.Waiters!.ToList());
        }


        [HttpGet("/Waiters/{id:int}")]
        public IActionResult GetByID([FromRoute] int id, [FromServices] AppDbContext context)
        {

            var products = context.Waiters!.FirstOrDefault(o => o.WaiterID == id);
            if (products == null)
            {
                return NotFound();
            }
            return Ok(products);
        }


        [HttpPost("/Waiters/")]
        public IActionResult Post([FromBody] Waiter waiter, [FromServices] AppDbContext context)
        {

            var existingProduct = context.Waiters!.SingleOrDefault(p => p.WaiterID == waiter.WaiterID);
            if (existingProduct != null)
            {
                var new_product = new Waiter();
                new_product.Name = waiter.Name;
                new_product.LastName = waiter.LastName;
                new_product.Cellphone = waiter.Cellphone;
                new_product.BirthDate = waiter.BirthDate;

                waiter = new_product;
            }
            
            context.Waiters!.Add(waiter);
            context.SaveChanges();
            return Created($"/{waiter.WaiterID}", waiter);
        }

        [HttpPut("/Waiters/{id:int}")]
        public IActionResult Put([FromRoute] int id, [FromBody] Waiter waiter, [FromServices] AppDbContext context)
        {

            var dBwaiter = context.Waiters!.FirstOrDefault(o => o.WaiterID == id);
            if (dBwaiter == null)
            {
                return NotFound();
            }

            dBwaiter.Name = waiter.Name;
            dBwaiter.LastName = waiter.LastName;
            dBwaiter.Cellphone = waiter.Cellphone;
            dBwaiter.BirthDate = waiter.BirthDate;

            context.Waiters!.Update(dBwaiter);
            context.SaveChanges();
            return Ok(dBwaiter);
        }

        [HttpDelete("/Waiters/{id:int}")]
        public IActionResult Delete([FromRoute] int id, [FromServices] AppDbContext context)
        {

            var waiter = context.Waiters.FirstOrDefault(o => o.WaiterID == id);
            if (waiter == null)
            {
                return NotFound();
            }

            context.Waiters!.Remove(waiter);
            context.SaveChanges();
            return Ok(waiter);
        }
    }
}
