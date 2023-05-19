using Aula03.Data;
using Aula03.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Aula03.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        [HttpGet]
        [Route("/Categories/")]
        public IActionResult Get([FromServices] AppDbContext context)
        {
            
            return Ok(context.Categories!.OrderBy(o=>o.Name).ToList());
        }


        [HttpGet("/Categories/{id:int}")]
        public IActionResult GetByID([FromRoute] int id, [FromServices] AppDbContext context)
        {

            var category = context.Categories!.FirstOrDefault(o => o.CategoryID == id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }


        [HttpPost("/Categories/")]
        public IActionResult Post([FromBody] Category category, [FromServices] AppDbContext context)
        {

            var existingProduct = context.Categories!.SingleOrDefault(p => p.CategoryID == category.CategoryID);
            if (existingProduct != null)
            {
                var new_product = new Category();
                new_product.Name = category.Name;

                category = new_product;
            }
            
            context.Categories!.Add(category);
            context.SaveChanges();
            return Created($"/{category.CategoryID}", category);
        }

        [HttpPut("/Categories/{id:int}")]
        public IActionResult Put([FromRoute] int id, [FromBody] Category category, [FromServices] AppDbContext context)
        {

            var dBcategory = context.Categories!.FirstOrDefault(o => o.CategoryID == id);
            if (dBcategory == null)
            {
                return NotFound();
            }

            dBcategory.Name = category.Name;
            

            context.Categories!.Update(dBcategory);
            context.SaveChanges();
            return Ok(dBcategory);
        }

        [HttpDelete("/Categories/{id:int}")]
        public IActionResult Delete([FromRoute] int id, [FromServices] AppDbContext context)
        {

            var category = context.Categories!.FirstOrDefault(o => o.CategoryID == id);
            if (category == null)
            {
                return NotFound();
            }

            context.Categories!.Remove(category);
            context.SaveChanges();
            return Ok(category);
        }
    }
}
