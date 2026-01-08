using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Project.Data;
using Project.Models;
using System.Linq;

namespace Project.Controllers
{
    public class SignUpController : Controller
    {
        private readonly AppDbContext _context;

        public SignUpController(AppDbContext context)
        {
            _context = context;
        }

        // GET: SignUp
        [HttpGet]
        public IActionResult Index()
        {
            return View(new SignUp());
        }

        // POST: SignUp
        [HttpPost]
        public IActionResult Index(SignUp model)
        {
            if (!ModelState.IsValid)
                return View(model);

            // Check if email already exists
            var existingUser = _context.signups.FirstOrDefault(u => u.Email == model.Email);
            if (existingUser != null)
            {
                ModelState.AddModelError("Email", "This email is already registered.");
                return View(model);
            }

            // Check passwords match
            if (model.Password != model.ConfirmPassword)
            {
                ModelState.AddModelError("Password", "Passwords do not match.");
                ModelState.AddModelError("ConfirmPassword", "Passwords do not match.");
                return View(model);
            }

            // Hash password
            var hasher = new PasswordHasher<SignUp>();
            model.Password = hasher.HashPassword(model, model.Password);

            // Set default values
            model.CreatedAt = DateTime.Now;
            model.Status = true;

            _context.signups.Add(model);
            _context.SaveChanges();

            TempData["Success"] = "Sign Up Successful! Please sign in.";
            return RedirectToAction("Index", "SignIn");
        }

        // Optional AJAX endpoint to check email uniqueness
        [HttpGet]
        public JsonResult CheckEmail(string email)
        {
            var exists = _context.signups.Any(u => u.Email == email);
            return Json(new { available = !exists });
        }

        // Logout
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
    }
}
