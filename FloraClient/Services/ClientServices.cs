using FloraSharedLibrary.Contracts;
using FloraSharedLibrary.Models;
using FloraSharedLibrary.Responses;

namespace FloraClient.Services
{
    public class ClientServices(HttpClient httpClient) : IProduct
    {
        private const string BaseUrl = "api/product";
        public Task<ServiceResponse> AddProduct(Product model)
        {
            throw new NotImplementedException();
        }

        public Task<List<Product>> GetAllProducts(bool featuredProducts)
        {
            throw new NotImplementedException();
        }
    }
}
