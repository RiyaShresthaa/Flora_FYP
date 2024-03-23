using Microsoft.AspNetCore.Mvc;
using FloraSharedLibrary.Models;
using FloraSharedLibrary.Responses;
using FloraServer.Repositories;


namespace FloraServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController(ICategory categoryService) : ControllerBase
    {
   
        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetAllProducts()
        {
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse>> AddProduct(Category model)
        {
            if (model is null) return BadRequest("Model is Null");
            var response = await categoryService.AddCategory(model);
            return Ok(response);
        }
    }
}
