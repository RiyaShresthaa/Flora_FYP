﻿using Microsoft.AspNetCore.Mvc;
using FloraSharedLibrary.Models;
using FloraSharedLibrary.Responses;
using FloraServer.Repositories;


namespace FloraServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController(IProduct productService) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetAllProducts(bool featured)
        {
            var products = await productService.GetAllProducts(featured); return Ok(products);
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse>> AddProduct(Product model)
        {
            if (model is null) return BadRequest("Model is Null");
            var response = await productService.AddProduct(model);
            return Ok(response);
        }
    }
}
