﻿using FloraSharedLibrary.Models;
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

            var result = CheckResponse(response);
            if (!result.Flag)
                return result;

            var apiResponse = await ReadContent(response);
            return General.DeserializeJsonString<ServiceResponse>(apiResponse);
        }

        public async Task<List<Product>> GetAllProducts(bool featuredProducts)
        {
            var response = await httpClient.GetAsync($"{ProductBaseUrl}?featured={featuredProducts}");
            var (flag, _) = CheckResponse(response);
            if (!flag) return null!;

            var result = await ReadContent(response);
            return [.. General.DeserializeJsonStringList<Product>(result)];
        }

        
        //categories

        public async Task<ServiceResponse> AddCategory(Category model)
        {
            var response = await httpClient.PostAsync(CategoryBaseUrl, General.GenerateStringContent(General.SerializedObj(model)));

            var result = CheckResponse(response);
            if (!result.Flag)
                return result;

            var apiResponse = await ReadContent(response);
            return General.DeserializeJsonString<ServiceResponse>(apiResponse);
        }

        public async Task<List<Category>> GetAllCategories()
        {
            var response = await httpClient.GetAsync($"{CategoryBaseUrl}");
            var (flag, _) = CheckResponse(response);
            if (!flag) return null!;

            var result = await ReadContent(response);
            return [.. General.DeserializeJsonStringList<Category>(result)];
        }

        //General Method

        private static async Task<string> ReadContent(HttpResponseMessage response) => await response.Content.ReadAsStringAsync();


        private ServiceResponse CheckResponse(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
                return new ServiceResponse(false, "Error occured. Try again later...");
            else
                return new ServiceResponse(true, null!);

        }
    }
}
