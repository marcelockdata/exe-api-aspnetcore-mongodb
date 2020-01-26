using System.Collections.Generic;
using System.Threading.Tasks;
using apimongodb.Models;
using apimongodb.Services;
using Microsoft.AspNetCore.Mvc;


namespace apimongodb.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductController :  ControllerBase
    {
        private readonly ProductService _productService;

        public ProductController(ProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> Get() =>
            await _productService.Get();

        [HttpGet("{id:length(24)}", Name = "GetProduct")]
        public async Task<ActionResult<Product>> Get(string id)
        {
            var product = await _productService.Get(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        [HttpPost]
        public async Task<ActionResult<Product>> Create(Product product)
        {
            await _productService.Create(product);

            return CreatedAtRoute("GetProduct", new { id = product.Id.ToString() }, product);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Product productIn)
        {
            var book = await _productService.Get(id);

            if (book == null)
            {
                return NotFound();
            }

            _productService.Update(id, productIn);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var product = await _productService.Get(id);

            if (product == null)
            {
                return NotFound();
            }

            _productService.Remove(product.Id);

            return NoContent();
        }
    }
}