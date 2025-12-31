using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyShop.Data;
using MyShop.Models;
using MyShop.Respository;
using MyShop.ViewModels;
using System;

namespace MyShop.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdminController : Controller
    {

        private readonly CategoryRepository _categoryRepository = null;
        private readonly SubCategoryRepository _subCategoryRepository = null;
        private readonly SignInManager<Users> signInManager;
        private readonly UserManager<Users> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly AppDbContext _appDbContext;
        public AdminController(SignInManager<Users> _signInManager, UserManager<Users> _userManager, RoleManager<IdentityRole> _roleManager, AppDbContext appDbContext)
        {
            signInManager = _signInManager;
            userManager = _userManager;
            roleManager = _roleManager;
            _appDbContext = appDbContext;

            _categoryRepository = new CategoryRepository();
            _subCategoryRepository = new SubCategoryRepository();
        }

        //public AdminController()
        //{
        //    _categoryRepository=new CategoryRepository();
        //}
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory(CategoryViewModel model)
        {
            if(!ModelState.IsValid)
            {
                ModelState.AddModelError("","Problem with the inputs.");
                return View(model);
            }
            var user = await userManager.GetUserAsync(User);
            var userId = user?.Id;
            Category category = new Category
            {
                CategoryName=model.CategoryName,
                DisplayOrder=model.DisplayOrder,
                CreatedOn = DateTime.Now,
                CreatedBy=userId
            };
           int id=_categoryRepository.InsertCategory(_appDbContext,category);
            if(id!=null)
            {
                return View();
            }
            else
            {
                ModelState.AddModelError("","Some problem in insertion.");
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult AddSubCategory()
        {
            var model = new SubCategoryViewModel
            {
                categories = _appDbContext.categories
           .Select(c => new SelectListItem
           {
               Value = c.Id.ToString(),
               Text = c.CategoryName
           }).ToList()
            };
            
            return View(model);

        }

        [HttpPost]
        public async Task<IActionResult> AddSubCategory(SubCategoryViewModel model)
        {
            if(!ModelState.IsValid)
            {
                ModelState.AddModelError("","Some problem with inputs");
                return View(model);
            }
            else
            {
                var user = await userManager.GetUserAsync(User);
                var userId = user?.Id;
                var subcategory = new SubCategory()
                {
                    Name = model.Name,
                    Description = model.Description,
                    CreatedBy = userId,
                    CreatedOn=System.DateTime.Now,
                    CategoryId=model.CategoryId
                };
                var result = _subCategoryRepository.InsertSubCategory(_appDbContext, subcategory);
                if(result!=null)
                {
                    TempData["success"] = "SubCategory created successfully!";
                    return RedirectToAction("ViewSubCategory", "Admin");
                }
                else
                {
                    ModelState.AddModelError("","Some problem while insertion.");
                    return View(model);
                }
            }
        }

        [HttpGet]

        public IActionResult ViewSubCategory()
        {
            var model = _subCategoryRepository.GetSubCategories(_appDbContext);
            TempData["success"] = "SubCategory fetched successfully!";
            return View(model);

        }
    }
}
