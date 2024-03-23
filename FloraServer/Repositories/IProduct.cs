using FloraSharedLibrary.Models;
using FloraSharedLibrary.Responses;
namespace FloraServer.Repositories
{
    public interface IProduct
    {
        Task<ServiceResponse> AddProduct(Product model);
        Task<List<Product>> GetAllProducts(bool featuredProducts);
    }
}

