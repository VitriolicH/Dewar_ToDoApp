using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoApplication.Areas.Identity.Data;
using ToDoApplication.Data;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("ToDoContextConnection") ?? throw new InvalidOperationException("Connection string 'ToDoContextConnection' not found.");

builder.Services.AddDbContext<ToDoContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<ToDoApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false).AddEntityFrameworkStores<ToDoContext>();


// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "Task",
    pattern: "Home/Edit/{*id}",
    defaults: new {Controller = "Task", Action = "Index" });


app.MapRazorPages();

app.Run();
