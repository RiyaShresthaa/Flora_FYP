using FloraSharedLibrary.Models;
using FloraSharedLibrary.Responses;
namespace FloraClient.Services
{
    public interface ICategoryService
    {
        Task<ServiceResponse> AddCategory(Category model);
        Task GetAllCategories();
    }
}
