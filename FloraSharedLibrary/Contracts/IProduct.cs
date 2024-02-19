using FloraSharedLibrary.Models;
using FloraSharedLibrary.Responses;
namespace FloraSharedLibrary.Contracts
{
    public interface IProduct
    {
        Task<ServiceResponse> AddProduct(Product model);
        Task<List<Product>> GetAllProducts(bool featuredProducts);
    }
}

