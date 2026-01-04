//using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Models; // Your ApplicationUser class

var builder = WebApplication.CreateBuilder(args);

// ---------------- DATABASES -----------------

// 1️⃣ Application database (Company / Unit)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


//builder.Services.Add();

// ---------------- MVC -----------------

builder.Services.AddControllersWithViews();

// ---------------- BUILD APP -----------------

var app = builder.Build();

// ---------------- MIDDLEWARE -----------------

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// **Authentication must come before Authorization**
//app.UseAuthentication();
app.UseAuthorization();

// ---------------- ROUTES -----------------

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

//app.MapRazorPages(); // Needed for Identity UI

app.Run();
