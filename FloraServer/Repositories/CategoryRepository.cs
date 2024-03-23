using FloraServer.Data;
using FloraSharedLibrary.Models;
using FloraSharedLibrary.Responses;
using Microsoft.EntityFrameworkCore;

namespace FloraServer.Repositories
{
    public class CategoryRepository(AppDbContext appDbContext) : ICategory
    {
        private readonly AppDbContext _appDbContext = appDbContext;

        
        public async Task<ServiceResponse> AddCategory(Category model)
        {
            if (model is null) return new ServiceResponse(false,"model is null");
            var (flag, message) = await CheckName(model.Name!);
            if (flag)
            {
                _appDbContext.Catetgories.Add(model);
                await Commit();
                return new ServiceResponse(true, "Saved");
                
            }
            return new ServiceResponse(flag, message);


        }

        public async Task<List<Category>> GetAllCategories() =>  await _appDbContext.Catetgories.ToListAsync();
        

        private async Task<ServiceResponse> CheckName (string name)
        {
            var category = await _appDbContext.Catetgories.FirstOrDefaultAsync(x => x.Name.ToLower()!.Equals(name.ToLower()));
            return category is null ? new ServiceResponse(true, null!) : new ServiceResponse(false, "Category already exists");
        }
        private async Task Commit() => await _appDbContext.SaveChangesAsync();
    }
}
