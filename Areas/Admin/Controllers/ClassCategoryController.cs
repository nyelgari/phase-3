using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Equinox.Models;

namespace Equinox.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ClassCategoryController : Controller
    {
        private readonly EquinoxContext _context;
        public ClassCategoryController(EquinoxContext context) => _context = context;

        // LIST
        public IActionResult Index()
        {
            var categories = _context.ClassCategories.ToList();
            return View(categories);
        }

        // CREATE (GET)
        public IActionResult Create()
        {
            return View(new ClassCategory());
        }

        // CREATE (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ClassCategory category)
        {
            if (!ModelState.IsValid) return View(category);

            _context.ClassCategories.Add(category);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        // EDIT (GET)
        public IActionResult Edit(int id)
        {
            var category = _context.ClassCategories.Find(id);
            if (category == null) return NotFound();

            return View(category);
        }

        // EDIT (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ClassCategory category)
        {
            if (!ModelState.IsValid) return View(category);

            _context.Entry(category).State = EntityState.Modified;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        // DELETE (GET)
        public IActionResult Delete(int id)
        {
            var category = _context.ClassCategories.Find(id);
            if (category == null) return NotFound();

            return View(category);
        }

        // DELETE (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int classCategoryId)
        {
            return DeleteCategory(classCategoryId);
        }

        private IActionResult DeleteCategory(int classCategoryId)
        {
            var category = _context.ClassCategories.Find(classCategoryId);
            if (category != null)
            {
                _context.ClassCategories.Remove(category);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
