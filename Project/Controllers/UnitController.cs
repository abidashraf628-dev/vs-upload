using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Models;
using Project.ViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Controllers
{
    public class UnitController : Controller
    {
        private readonly AppDbContext _context;
        //public UnitController(AppDbContext context) => _context = context;

        public UnitController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var vm = new UnitFormViewModel
            {
                Unit = new Unit(),
                Units = _context.Units.Include(u => u.Company).ToList(),
                Companies = _context.Companies.ToList()
            };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Unit unit)
        {
            if (!ModelState.IsValid)
            {
                var vm = new UnitFormViewModel
                {
                    Unit = unit,
                    Units = _context.Units.Include(u => u.Company).ToList(),
                    Companies = _context.Companies.ToList()
                };
                //return View("Index", vm);
            }

            unit.CreatedAt = DateTime.Now;
            unit.UpdatedAt = DateTime.Now;
            unit.CreatedBy = 1;
            unit.UpdatedBy = 1;

            _context.Units.Add(unit);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Unit unit)
        {
            var existing = await _context.Units.FindAsync(unit.Id);
            if (existing == null) return NotFound();

            existing.Name = unit.Name;
            existing.CompanyId = unit.CompanyId;
            existing.Status = unit.Status;
            existing.UpdatedAt = DateTime.Now;
            existing.UpdatedBy = 1;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var unit = await _context.Units.FindAsync(id);
            if (unit == null) return NotFound();

            _context.Units.Remove(unit);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
