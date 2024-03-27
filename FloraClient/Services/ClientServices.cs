using FloraSharedLibrary.Models;
using FloraSharedLibrary.Responses;

namespace FloraClient.Services
{
    public class ClientServices(HttpClient httpClient) : IProductService, ICategoryService
    {
        private const string ProductBaseUrl = "api/product";
        private const string CategoryBaseUrl = "api/category";




        //Products

        public async Task<ServiceResponse> AddProduct(Product model)
        {
            var response = await httpClient.PostAsync(ProductBaseUrl, General.GenerateStringContent(General.SerializedObj(model)));

            if (!response.IsSuccessStatusCode)
                return new ServiceResponse(false, "Error occured. Try again later...");

            var apiResponse = await response.Content.ReadAsStringAsync();
            return General.DeserializeJsonString<ServiceResponse>(apiResponse);
        }

        public async Task<List<Product>> GetAllProducts(bool featuredProducts)
        {
            var response = await httpClient.GetAsync($"{ProductBaseUrl}?featured={featuredProducts}");
            if (!response.IsSuccessStatusCode) return null!;

            var result = await response.Content.ReadAsStringAsync();
            return [.. General.DeserializeJsonStringList<Product>(result)];
        }



        //categories

        public Task<ServiceResponse> AddCategory(Category model)
        {
            throw new NotImplementedException();
        }

        public Task GetAllCategories()
        {
            throw new NotImplementedException();
        }
    }
}
