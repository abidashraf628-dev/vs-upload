using Microsoft.AspNetCore.Mvc;
using Project.Data;
using Project.Models;
using Microsoft.AspNetCore.Identity;

namespace Project.Controllers
{
    public class SignInController : Controller
    {
        private readonly AppDbContext _context;

        public SignInController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(SignInModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = _context.signups.FirstOrDefault(u => u.Email == model.Email);

            if (user == null)
            {
                TempData["Error"] = "Invalid email or password.";
                return View(model);
            }

            var hasher = new PasswordHasher<SignUp>();

            var result = hasher.VerifyHashedPassword(user, user.Password, model.Password);

            if (result == PasswordVerificationResult.Success)
            {
                HttpContext.Session.SetString("IsSignedUp", "true");
                HttpContext.Session.SetString("FullName", user.FullName);
                HttpContext.Session.SetString("Email", user.Email);

                return RedirectToAction("Index", "Company");
            }

            TempData["Error"] = "Invalid email or password.";
            return View(model);
        }

    }
}
