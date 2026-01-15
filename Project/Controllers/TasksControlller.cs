using Microsoft.AspNetCore.Mvc;
using Project.Data;
using Project.Filters;
using Project.Models;
using System.Linq;

namespace Project.Controllers
{
    [SessionAuthorize]
    public class TasksController : Controller
    {
        private readonly AppDbContext _context;

        public TasksController(AppDbContext context)
        {
            _context = context;
        }

        /* ===============================
           INDEX
           =============================== */
        public IActionResult Index()
        {
            var vm = new
            {
                tasks = _context.tasks.ToList()
            };

            return View(vm);
        }

        /* ===============================
           CREATE
           =============================== */
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Tasks model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Index));
            }

            _context.tasks.Add(model);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        /* ===============================
           EDIT
           =============================== */
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Tasks model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Index));
            }

            var task = _context.tasks.FirstOrDefault(t => t.Id == model.Id);
            if (task == null)
            {
                return NotFound();
            }

            task.Title = model.Title;
            task.Description = model.Description;
            task.Status = model.Status;

            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        /* ===============================
           DELETE
           =============================== */
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var task = _context.tasks.Find(id);
            if (task == null)
            {
                return NotFound();
            }

            _context.tasks.Remove(task);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
