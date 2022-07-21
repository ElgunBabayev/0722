using actiuon.DAL;
using actiuon.Models;
using actiuon.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace actiuon.Controllers
{
    public class ProductController : Controller
    {
        private readonly AppDbContext dbContext;

        public ProductController(AppDbContext db) => dbContext = db;
        [Route("Products")]
        public IActionResult Index()
        {
            ProductCategoryViewModel pvm = new ProductCategoryViewModel
            {
                Categories = dbContext.Categories.ToList(),
                Products = dbContext.Products.Where(x => x.EndDate > DateTime.Now).Take(6).ToList()
            };
            return View(pvm);
        }
        [Route("Product/{Id}")]
        public IActionResult Get(int Id)
        {
            return View(dbContext.Products.FirstOrDefault(x => x.Id == Id));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(Product product)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }
            await dbContext.Products.AddAsync(product);
            await dbContext.SaveChangesAsync();
            return Ok();/*RedirectToAction("Add", "Product");*/


        }




    }
}
