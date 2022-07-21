using actiuon.DAL;
using actiuon.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace actiuon.Controllers
{
    public class CategoryController : Controller
    {

        
        private readonly AppDbContext dbContext;
        public CategoryController(AppDbContext db) => dbContext = db;

        

        public IActionResult Index()
        {
            ProductCategoryViewModel pvm = new ProductCategoryViewModel
            {
                Categories = dbContext.Categories.ToList(),
                Products = dbContext.Products.Where(x => x.EndDate > DateTime.Now).Take(6).ToList()
            };
            return View(pvm);

        }

        [Route("Category/{Id}/Products")]
        public IActionResult Get(int Id)
        {
            ProductCategoryViewModel pvm = new ProductCategoryViewModel
            {
                Categories = dbContext.Categories.ToList(),
                Products = dbContext.Products.Where(x => x.CategoryId==Id).ToList()
            };
            ViewBag.Selected = dbContext.Categories.Find(Id).Name;
            return View(pvm);
        }

    }
}
