using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Equinox.Models;

namespace Equinox.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ClubController : Controller
    {
        private readonly EquinoxContext _context;
        public ClubController(EquinoxContext ctx) => _context = ctx;

        // LIST
        public IActionResult Index()
        {
            var clubs = _context.Clubs.ToList();
            return View(clubs);
        }

        // CREATE: GET
        public IActionResult Create()
        {
            return View(new Club());
        }

        // CREATE: POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Club club)
        {
            if (!ModelState.IsValid) return View(club);
            _context.Clubs.Add(club);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        // EDIT: GET
        public IActionResult Edit(int id)
        {
            var club = _context.Clubs.Find(id);
            if (club == null) return NotFound();
            return View(club);
        }

        // EDIT: POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Club club)
        {
            if (!ModelState.IsValid) return View(club);
            _context.Entry(club).State = EntityState.Modified;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        // DELETE: GET confirm
        public IActionResult Delete(int id)
        {
            var club = _context.Clubs.Find(id);
            if (club == null) return NotFound();
            return View(club);
        }

        // DELETE (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int clubId)
        {
            return DeleteClub(clubId);
        }

        private IActionResult DeleteClub(int clubId)
        {
            var club = _context.Clubs.Find(clubId);
            if (club != null)
            {
                _context.Clubs.Remove(club);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
