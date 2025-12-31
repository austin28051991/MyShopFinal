using Microsoft.EntityFrameworkCore;
using MyShop.Data;
using MyShop.Models;
using MyShop.ViewModels;

namespace MyShop.Respository
{
    public class SubCategoryRepository : ISubCategory
    {
        public List<SubCategoryViewModel> GetSubCategories(AppDbContext _context)
        {
            var subcategories = _context.SubCategories
     .Select(sc => new SubCategoryViewModel
     {
         CategoryId=sc.CategoryId,
         Name = sc.Name,
         Description = sc.Description,
         CategoryName = sc.Category.CategoryName
     })
     .ToList();

            return subcategories;

        }

        
        public SubCategoryViewModel GetSubCategoryById(AppDbContext _context,int id)
        {
            var subcategory = _context.SubCategories.Where
                (x => x.Id == id).Select(x => new SubCategoryViewModel()
                {
                    CategoryId = x.CategoryId,
                    Name = x.Name,
                    Description = x.Description,
                    CategoryName = x.Category.CategoryName

                }).
                FirstOrDefault();
            return subcategory;
        }

        public  int InsertSubCategory(AppDbContext context, SubCategory subcategory)
        {
            var result=context.SubCategories.Add(subcategory);
            context.SaveChanges();
            int newId = subcategory.Id;
            return newId;
        }

        
    }
}
