using Microsoft.AspNetCore.Mvc;
using Project.Data;
using Project.Filters;
using Project.Models;
using Project.ViewModels;
using System;
using System.Linq;

namespace Project.Controllers
{
    [SessionAuthorize]
    public class EmployeeController : Controller
    {
        private readonly AppDbContext _context;

        public EmployeeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            ViewBag.Companies = _context.Companies.ToList();
            ViewBag.Units = _context.Units.ToList();

            var employees = _context.Employees.Select(e => new EmployeeViewModel
            {
                Id = e.Id,
                Company = e.Company.Name,
                Unit = e.Unit.Name,
                CompanyId = e.CompanyId,
                UnitId = e.UnitId,
                EmployeeId = e.EmployeeId,
                FullName = e.FullName,
                Status = e.Status
            }).ToList();

            return View(new EmployeeListViewModel { Employees = employees });
        }

        [HttpGet]
        public IActionResult GetUnitsByCompany(int companyId)
        {
            var units = _context.Units
                .Where(u => u.CompanyId == companyId)
                .Select(u => new
                {
                    u.Id,
                    u.Name
                })
                .ToList();

            return Json(units);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Employee model)
        {
            // 🔴 TEMP DEBUG — DO NOT KEEP PERMANENTLY
            try
            {
                model.CreatedAt = DateTime.Now;
                model.UpdatedAt = DateTime.Now;
                model.CreatedBy = 1;
                model.UpdatedBy = 1;

                _context.Employees.Add(model);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                return Content(ex.InnerException?.Message ?? ex.Message);
            }

            return RedirectToAction("Index");
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Employee model)
        {
            var emp = _context.Employees.Find(model.Id);
            if (emp == null) return NotFound();

            emp.CompanyId = model.CompanyId;
            emp.UnitId = model.UnitId;
            emp.EmployeeId = model.EmployeeId;
            emp.FullName = model.FullName;
            emp.Status = model.Status;
            emp.UpdatedAt = DateTime.Now;
            emp.UpdatedBy = 1;

            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var emp = _context.Employees.Find(id);
            if (emp != null)
            {
                _context.Employees.Remove(emp);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
