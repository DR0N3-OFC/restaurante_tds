using Aula03.Data;
using Aula03.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Aula03.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TableController : ControllerBase
    {
        [HttpGet]
        [Route("/Tables/")]
        public IActionResult Get([FromServices] AppDbContext context)
        {
            
            return Ok(context.Tables!.OrderBy(o=>o.Name).ToList());
        }


        [HttpGet("/Tables/{id:int}")]
        public IActionResult GetByID([FromRoute] int id, [FromServices] AppDbContext context)
        {

            var products = context.Tables!.FirstOrDefault(o => o.TableID == id);
            if (products == null)
            {
                return NotFound();
            }
            return Ok(products);
        }

        [HttpGet("/TableOpen/{id:int}")]
        public IActionResult TableOpen([FromRoute] int id, [FromServices] AppDbContext context)
        {

            var table = context.Tables!.FirstOrDefault(o => o.TableID == id);
            if (table == null)
            {
                return NotFound();
            }

            table.Status = true;
            context.Tables!.Update(table);
            context.SaveChanges();

            return Ok(table);
        }

        [HttpGet("/TableClose/{id:int}")]
        public IActionResult TableClose([FromRoute] int id, [FromServices] AppDbContext context)
        {

            var table = context.Tables!.FirstOrDefault(o => o.TableID == id);
            if (table == null)
            {
                return NotFound();
            }

            table.Status = false;
            context.Tables!.Update(table);
            context.SaveChanges();

            return Ok(table);
        }

        [HttpPost("/Tables/")]
        public IActionResult Post([FromBody] Table table, [FromServices] AppDbContext context)
        {

            var existingProduct = context.Tables!.SingleOrDefault(p => p.TableID == table.TableID);
            if (existingProduct != null)
            {
                var new_product = new Table();
                new_product.Name = table.Name;
                new_product.Status = false;

                table = new_product;
            }
            
            context.Tables!.Add(table);
            context.SaveChanges();
            return Created($"/{table.TableID}", table);
        }

        [HttpPut("/Tables/{id:int}")]
        public IActionResult Put([FromRoute] int id, [FromBody] Table table, [FromServices] AppDbContext context)
        {

            var dBtable = context.Tables!.FirstOrDefault(o => o.TableID == id);
            if (dBtable == null)
            {
                return NotFound();
            }

            dBtable.Name = table.Name;
            dBtable.LiberationTime = table.LiberationTime;

            context.Tables!.Update(dBtable);
            context.SaveChanges();
            return Ok(dBtable);
        }

        [HttpDelete("/Tables/{id:int}")]
        public IActionResult Delete([FromRoute] int id, [FromServices] AppDbContext context)
        {

            var waiter = context.Tables!.FirstOrDefault(o => o.TableID == id);
            if (waiter == null)
            {
                return NotFound();
            }

            context.Tables!.Remove(waiter);
            context.SaveChanges();
            return Ok(waiter);
        }
    }
}
