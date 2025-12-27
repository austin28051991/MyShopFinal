using MyShop.Data;
using MyShop.Models;

namespace MyShop.Respository
{
    public interface ICategory
    {
        int InsertCategory(AppDbContext context, Category category);
        List<Category> GetCategories(AppDbContext context);
        Category GetCategoryById(AppDbContext context,int id);

    }
}
