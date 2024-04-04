using FloraServer.Data;
using FloraSharedLibrary.DTOs;
using FloraSharedLibrary.Models;
using FloraSharedLibrary.Responses;
using Microsoft.EntityFrameworkCore;

namespace FloraServer.Repositories
{
    public class CategoryRepository : ICategory
    {
        private readonly AppDbContext _appDbContext;

        public CategoryRepository(AppDbContext appDbContext)
        {
            _appDbContext= appDbContext;
        }


        public async Task<ServiceResponse> AddCategory(Category model)
        {
            if (model is null) return new ServiceResponse(false,"model is null");
            var (flag, message) = await CheckName(model.Name!);
            if (flag)
            {
                _appDbContext.Categories.Add(model);
                await Commit();
                return new ServiceResponse(true, "Category Saved");
                
            }
            return new ServiceResponse(flag, message);


        }

        public Task<ServiceResponse> AddProduct(Category model)
        {
            throw new NotImplementedException();
        }
        
        public async Task<List<Category>> GetAllCategories() => await _appDbContext.Categories.ToListAsync();

        private async Task<ServiceResponse> CheckName (string name)
        {
            var category
                = await _appDbContext.Categories.FirstOrDefaultAsync(x => x.Name!.ToLower()!.Equals(name.ToLower()));
            return category is null ? new ServiceResponse(true, null!) : new ServiceResponse(false, "Product already exists");
        }
        private async Task Commit() => await _appDbContext.SaveChangesAsync();
    }
}
