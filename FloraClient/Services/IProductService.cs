using FloraSharedLibrary.Models;
using FloraSharedLibrary.Responses;
namespace FloraClient.Services
{
    public interface IProductService
    {
        Task<ServiceResponse> AddProduct(Product model);
        Task<List<Product>> GetAllProducts(bool featuredProducts);
    }
}

