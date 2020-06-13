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
    public class SpecialTagsController : Controller
    {
        private readonly ApplicationDbContext _db;
        public SpecialTagsController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {

            return View(_db.SpecialTags.ToList());
        }
        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SpecialTags specialTags)
        {
            if (!ModelState.IsValid)
            {
                return NotFound();
            }

            _db.SpecialTags.Add(specialTags);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));


            
        }


        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var specialTags = _db.SpecialTags.Find(id);
            //if (specialTags.Id != id)
            //{
            //    return NotFound();
            //}

            return View(specialTags);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SpecialTags specialTags)
        {
            
            if (specialTags.Id != id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                _db.Update(specialTags);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(specialTags);


        }


        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var specialTags = _db.SpecialTags.Find(id);
            if (specialTags.Id != id)
            {
                return NotFound();
            }

            return View(specialTags);
        }

        public IActionResult Delete(int? id)
        {
            var specialTags = _db.SpecialTags.Find(id);
            if(specialTags==null)
            return NotFound();

            return View(specialTags);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var specialTags = await _db.SpecialTags.FindAsync(id);
            _db.SpecialTags.Remove(specialTags);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}