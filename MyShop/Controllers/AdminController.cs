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



    }
}
