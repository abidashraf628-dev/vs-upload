using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.Data;       // <- your AppDbContext namespace
using Project.Filters;
using Project.Models;     // AssignTask, Employee, Task
using Project.ViewModels; // EmployeeTaskReportVM
using System;
using System.Linq;

namespace Project.Controllers
{
    [SessionAuthorize]
    public class EmployeeTaskReportController : Controller
    {
        private readonly AppDbContext _context;

        public EmployeeTaskReportController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.Employees = _context.Employees.ToList();
            return View(new EmployeeTaskReportVM());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(EmployeeTaskReportVM model)
        {
            ViewBag.Employees = _context.Employees.ToList();

            var query = _context.AssignTasks
                .Include(x => x.Employee)
                .Include(x => x.Task)
                .AsQueryable();

            // Filter by Employee
            if (model.EmployeeId.HasValue)
                query = query.Where(x => x.EmployeeId == model.EmployeeId);

            // Filter by Status
            if (!string.IsNullOrEmpty(model.Status))
            {
                if (model.Status == "Failed")
                {
                    query = query.Where(x => x.Status != "Completed"
                                             && x.DateAssigned.AddDays(x.DurationDays) < DateTime.Today);
                }
                else
                {
                    query = query.Where(x => x.Status == model.Status);
                }
            }

            // Filter by FromDate and ToDate (using only the Date part)
            if (model.FromDate.HasValue)
                query = query.Where(x => x.DateAssigned.Date >= model.FromDate.Value.Date);

            if (model.ToDate.HasValue)
                query = query.Where(x => x.DateAssigned.Date <= model.ToDate.Value.Date);

            // Compute dynamic Failed status
            model.Results = query
                .AsEnumerable() // switch to in-memory to compute Failed
                .Select(x =>
                {
                    if (x.Status != "Completed" && x.DateAssigned.AddDays(x.DurationDays) < DateTime.Today)
                        x.Status = "Failed";
                    return x;
                })
                .OrderByDescending(x => x.DateAssigned)
                .ToList();

            return View(model);
        }

    }
}
