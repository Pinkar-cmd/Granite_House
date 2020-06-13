using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraniteHouse.Data;
using GraniteHouse.Models;
using GraniteHouse.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GraniteHouse.Areas.Admin.Controllers
{
    [Authorize(Roles = SD.SuperAdminEndUser)]
    [Area("Admin")]
    public class ProductTypesController : Controller
    {
        private readonly ApplicationDbContext _db;
        public ProductTypesController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {

            return View(_db.ProductTypes.ToList());
        }
        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductTypes productTypes)
        {
            if (!ModelState.IsValid)
            {
                return NotFound();
            }

            _db.ProductTypes.Add(productTypes);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));


            
        }


        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var producttype = _db.ProductTypes.Find(id);
            if (producttype.Id != id)
            {
                return NotFound();
            }

            return View(producttype);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,ProductTypes productTypes)
        {
            
            if (productTypes.Id != id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                _db.Update(productTypes);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(productTypes);


        }


        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var producttype = _db.ProductTypes.Find(id);
            if (producttype.Id != id)
            {
                return NotFound();
            }

            return View(producttype);
        }

        public IActionResult Delete(int? id)
        {
            var productType = _db.ProductTypes.Find(id);
            if(productType==null)
            return NotFound();

            return View(productType);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productTypes = await _db.ProductTypes.FindAsync(id);
            _db.ProductTypes.Remove(productTypes);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}