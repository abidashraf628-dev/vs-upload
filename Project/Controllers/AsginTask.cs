using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Filters;
using Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Project.Controllers
{
    [SessionAuthorize]
    public class AssignTaskController : Controller
    {
        private readonly AppDbContext _context;

        public AssignTaskController(AppDbContext context)
        {
            _context = context;
        }

        // =========================
        // GET: AssignTask
        // =========================
        public async Task<IActionResult> Index()
        {
            // Fetch all tasks (past + today)
            var assignTasks = await _context.AssignTasks
                .Include(a => a.Employee)
                .Include(a => a.Task)
                .OrderByDescending(a => a.DateAssigned)
                .ToListAsync();

            ViewBag.Employees = await _context.Employees.ToListAsync();

            return View(assignTasks);
        }

        // =========================
        // POST: AssignTask/Create
        // =========================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int EmployeeId, string TaskIds)
        {
            if (EmployeeId <= 0 || string.IsNullOrWhiteSpace(TaskIds))
            {
                TempData["Error"] = "All fields are required.";
                return RedirectToAction(nameof(Index));
            }

            List<TaskItem> taskList;
            try
            {
                taskList = JsonSerializer.Deserialize<List<TaskItem>>(TaskIds);
            }
            catch
            {
                TempData["Error"] = "Invalid task data.";
                return RedirectToAction(nameof(Index));
            }

            if (taskList == null || taskList.Count == 0)
            {
                TempData["Error"] = "No tasks selected.";
                return RedirectToAction(nameof(Index));
            }

            DateTime DateAssigned = DateTime.Today;

            foreach (var task in taskList)
            {
                var assignTask = new AssignTask
                {
                    EmployeeId = EmployeeId,
                    TaskId = task.id,
                    DateAssigned = DateAssigned,
                    Status = "Pending"
                };
                _context.AssignTasks.Add(assignTask);
            }

            await _context.SaveChangesAsync();
            TempData["Success"] = "Task(s) assigned successfully.";
            return RedirectToAction(nameof(Index));
        }

        // =========================
        // POST: AssignTask/MarkDone
        // =========================
        [HttpPost("AssignTask/MarkDone/{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult MarkDone(int id)
        {
            var task = _context.AssignTasks.Find(id);
            if (task == null) return NotFound();

            task.Status = "Completed";
            _context.SaveChanges();

            return Ok();
        }

        // =========================
        // POST: AssignTask/Delete
        // =========================
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("AssignTask/Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var assignTask = await _context.AssignTasks.FindAsync(id);
            if (assignTask != null)
            {
                _context.AssignTasks.Remove(assignTask);
                await _context.SaveChangesAsync();
                return Ok();
            }
            return BadRequest("Task not found");
        }

        // =========================
        // GET: AssignTask/GetTasks
        // =========================
        [HttpGet]
        public async Task<IActionResult> GetTasks()
        {
            var tasks = await _context.tasks
                .Select(t => new { id = t.Id, title = t.Title })
                .ToListAsync();
            return Json(tasks);
        }
    }

    // Simple DTO for deserializing TaskIds from Tagify
    public class TaskItem
    {
        public int id { get; set; }
        public string value { get; set; }
    }
}
