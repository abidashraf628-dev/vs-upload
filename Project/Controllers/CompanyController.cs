using Microsoft.AspNetCore.Mvc;
using Project.Data;
using Project.Models;
using Project.ViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Controllers
{
    public class CompanyController : Controller
    {
        private readonly AppDbContext _context;
        public CompanyController(AppDbContext context) => _context = context;

        public IActionResult Index()
        {
            var vm = new CompanyFormViewModel
            {
                Company = new Company(),
                Companies = _context.Companies.ToList()
            };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Company company)
        {
            if (!ModelState.IsValid)
            {
                var vm = new CompanyFormViewModel
                {
                    Company = company,
                    Companies = _context.Companies.ToList()
                };
              //  return View("Index", vm);
            }

            company.CreatedAt = DateTime.Now;
            company.UpdatedAt = DateTime.Now;
            company.CreatedBy = 1;
            company.UpdatedBy = 1;

            _context.Companies.Add(company);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Company company)
        {
            var existing = await _context.Companies.FindAsync(company.Id);
            if (existing == null) return NotFound();

            existing.Name = company.Name;
            existing.ShortName = company.ShortName;
            existing.Status = company.Status;
            existing.UpdatedAt = DateTime.Now;
            existing.UpdatedBy = 1;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var company = await _context.Companies.FindAsync(id);
            if (company == null) return NotFound();

            _context.Companies.Remove(company);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
