using FloraSharedLibrary.Models;
using FloraSharedLibrary.Responses;
namespace FloraClient.Services
{
    public interface ICategoryService
    {
        Action? CategoryAction { get; set; }
        Task<ServiceResponse> AddCategory(Category model);

        Task GetAllCategories();

        List<Category> AllCategories { get; set; }
    }
}
