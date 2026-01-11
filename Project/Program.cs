//using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Models; // Your ApplicationUser class

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllersWithViews();
// Add services
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add session services
builder.Services.AddDistributedMemoryCache(); // Required for session storage
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // session timeout
    options.Cookie.HttpOnly = true; // secure
    options.Cookie.IsEssential = true;
});

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

// Use middleware
app.UseStaticFiles();
app.UseRouting();

// Use session BEFORE endpoints
app.UseSession();

app.UseAuthorization();



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
    pattern: "{controller=SignIn}/{action=Index}");

//app.MapRazorPages(); // Needed for Identity UI

app.Run();
