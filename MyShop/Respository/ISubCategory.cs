using MyShop.Data;
using MyShop.Models;
using MyShop.ViewModels;

namespace MyShop.Respository
{
    public interface ISubCategory
    {
        int InsertSubCategory(AppDbContext context, SubCategory subcategory);
        List<SubCategoryViewModel> GetSubCategories(AppDbContext context);
        SubCategoryViewModel GetSubCategoryById(AppDbContext context,int id);
        int UpdateSubCategory(AppDbContext context, SubCategory subcategory);

    }
}
