using Microsoft.EntityFrameworkCore;
using MyShop.Data;
using MyShop.Models;

namespace MyShop.Respository
{
    public class CategoryRepository : ICategory
    {
        public List<Category> GetCategories(AppDbContext _context)
        {
           var categories=_context.categories.ToList();
            return categories;
        }

        
        public Category GetCategoryById(AppDbContext _context,int id)
        {
            var category = _context.categories.FirstOrDefault(x=>x.Id==id);
            return category;
        }

        public  int InsertCategory(AppDbContext context, Category category)
        {
            var result=context.categories.Add(category);
            context.SaveChanges();
            int newId = category.Id;
            return newId;
        }

    }
}
