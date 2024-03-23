using FloraSharedLibrary.Models;
using FloraSharedLibrary.Responses;
namespace FloraServer.Repositories
{
    public interface ICategory
    {
        Task<ServiceResponse> AddCategory(Category model);
        Task<List<Category>> GetAllCategories();
    }
}

