using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Equinox.Models;

namespace Equinox.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly EquinoxContext _context;
        public UserController(EquinoxContext context) => _context = context;

        // LIST
        public IActionResult Index()
        {
            var users = _context.Users.ToList();
            return View(users);
        }

        // CREATE (GET)
        public IActionResult Create()
        {
            return View(new User());
        }

        // CREATE (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(User user)
        {
            if (!ModelState.IsValid)
            {
                TempData["ModelError"] = "Please fix the error";
                return View(user);
            }

            _context.Users.Add(user);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        // EDIT (GET)
        public IActionResult Edit(string id)
        {
            var user = _context.Users.Find(id);
            if (user == null) return NotFound();
            return View(user);
        }

        // EDIT (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(User user)
        {
            if (!ModelState.IsValid)
            {
                TempData["ModelError"] = "Please fix the error";
                return View(user);
            }

            _context.Entry(user).State = EntityState.Modified;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        // DELETE (GET)
        public IActionResult Delete(string id)
        {
            var user = _context.Users.Find(id);
            if (user == null) return NotFound();
            return View(user);
        }

        // DELETE (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(string userId)
        {
            return DeleteUser(userId);
        }

        private IActionResult DeleteUser(string userId)
        {
            var user = _context.Users.Find(userId);
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }

        // REMOTE VALIDATION
        [AcceptVerbs("GET", "POST")]
        public IActionResult VerifyPhone(string phoneNumber, string userId)
        {
            bool taken = _context.Users.Any(u => u.PhoneNumber == phoneNumber
                                          && u.UserId != userId);
            return taken
                ? Json($"Phone {phoneNumber} already in use")
                : Json(true);
        }
    }
}
