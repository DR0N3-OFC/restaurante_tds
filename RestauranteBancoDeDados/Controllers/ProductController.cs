using Aula03.Data;
using Aula03.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Aula03.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        [HttpGet]
        [Route("/Products/")]
        public IActionResult Get([FromServices] AppDbContext context)
        {
            var products = context.Products!.ToList();
            foreach(var aux in products)
            {
                context.Entry(aux).Reference(o => o.Category).Load();
            }
            return Ok(context.Products!.ToList());
        }


        [HttpGet("/Products/{id:int}")]
        public IActionResult GetByID([FromRoute] int id, [FromServices] AppDbContext context)
        {

            var products = context.Products!.Include(o => o.Category).FirstOrDefault(o => o.ProductID == id);
            if (products == null)
            {
                return NotFound();
            }
            return Ok(products);
        }


        [HttpPost("/Products/")]
        public IActionResult Post([FromBody] Product product, [FromServices] AppDbContext context)
        {

            var existingProduct = context.Products!.SingleOrDefault(p => p.ProductID == product.ProductID);
            var category = context.Categories!.SingleOrDefault(c => c.CategoryID == product.Category!.CategoryID);
            if (existingProduct != null)
            {
                var new_product = new Product();
                new_product.Name = product.Name;
                new_product.Description = product.Description;
                new_product.Price = product.Price;

                product = new_product;
            }
            product.Category = category;
            context.Products!.Add(product);
            context.SaveChanges();
            return Created($"/{product.ProductID}", product);
        }

        [HttpPut("/Products/{id:int}")]
        public IActionResult Put([FromRoute] int id, [FromBody] Product product, [FromServices] AppDbContext context)
        {

            var dbProduct = context.Products.FirstOrDefault(o => o.ProductID == id);
            var category = context.Categories!.SingleOrDefault(c => c.CategoryID == product.Category.CategoryID);

            if (dbProduct == null)
            {
                return NotFound();
            }

            dbProduct.Name = product.Name;
            dbProduct.Description = product.Description;
            dbProduct.Category = category;
            dbProduct.Price = product.Price;


            context.Products.Update(dbProduct);
            context.SaveChanges();
            return Ok(dbProduct);
        }

        [HttpDelete("/Products/{id:int}")]
        public IActionResult Delete([FromRoute] int id, [FromServices] AppDbContext context)
        {

            var products = context.Products.FirstOrDefault(o => o.ProductID == id);
            if (products == null)
            {
                return NotFound();
            }

            context.Products.Remove(products);
            context.SaveChanges();
            return Ok(products);
        }
    }
}
