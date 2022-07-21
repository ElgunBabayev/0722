using actiuon.DAL;
using actiuon.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace actiuon.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class BannerController : Controller
    {
        private readonly AppDbContext dbContext;
        public BannerController(AppDbContext db) => dbContext = db;
        public async Task<IActionResult> Index()
        {
            return View(await dbContext.Banners.ToListAsync());
        }
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(Banner banner)
        {
            if (!banner.Image.ContentType.Contains("image/"))
            {
                return Content("Faylin adi ok deyil");
            }
            if (!(banner.Image.Length / 1024 > 500))
            {
                return Content("Faylin adi ok deyil");
            }
            if (!ModelState.IsValid)
            {
                return View(banner);
            }
            await dbContext.Banners.AddAsync(banner);
            await dbContext.SaveChangesAsync();
            return RedirectToAction("Index", "Banner");
        }
        public async Task<IActionResult> Delete(int id)
        {
            Banner bannerdelete = await dbContext.Banners.FindAsync(id);
            dbContext.Banners.Remove(bannerdelete);
            await dbContext.SaveChangesAsync();
            return RedirectToAction("Index", "Banner");
        }
        public async Task<IActionResult> Edit(int id)
        {
            return View(await dbContext.Banners.FindAsync(id));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Banner banner)
        {
            dbContext.Banners.Update(banner);
                await dbContext.SaveChangesAsync();

            return RedirectToAction("Index", "Banner");
        }
    }
}
