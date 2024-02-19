using FloraSharedLibrary.Contracts;
using Microsoft.AspNetCore.Mvc;
using FloraSharedLibrary.Models;
using FloraSharedLibrary.Responses;


namespace FloraServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProduct _productService;

        public ProductController(IProduct productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetAllProducts(bool featured)
        {
            var products = await _productService.GetAllProducts(featured); return Ok(products);
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse>> AddProduct(Product model)
        {
            if (model is null) return BadRequest("Model is Null");
            var response = await _productService.AddProduct(model);
            return Ok(response);
        }
    }
}
