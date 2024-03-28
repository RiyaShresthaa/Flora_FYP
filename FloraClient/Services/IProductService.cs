using FloraSharedLibrary.Models;
using FloraSharedLibrary.Responses;
namespace FloraClient.Services
{
    public interface IProductService
    {
        //Action? ProductAction { get; set; }
        //Task<ServiceResponse> AddProduct(Product model);
        //Task GetAllProducts(bool featuredProducts);
        //List<Product> AllProducts { get; set; }
        //List<Product> FeaturedProducts { get; set; }
        Task<ServiceResponse> AddProduct(Product model);
        Task<List<Product>> GetAllProducts(bool featuredProducts);

    }
}

