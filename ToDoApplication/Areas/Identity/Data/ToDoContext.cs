using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ToDoApplication.Areas.Identity.Data;
using ToDoApplication.Models;

namespace ToDoApplication.Data;

public class ToDoContext : IdentityDbContext<ToDoApplicationUser>
{
    public ToDoContext(DbContextOptions<ToDoContext> options)
        : base(options)
    {
    }
    public DbSet<ToDoModel> Todos { get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }
}
