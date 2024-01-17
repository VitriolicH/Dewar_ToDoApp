using Microsoft.EntityFrameworkCore;

namespace ToDoApplication.Models;

public class ToDoCon : DbContext
{
    public ToDoCon(DbContextOptions<ToDoCon> opts) : base(opts) { }

    public DbSet<ToDoModel> ToDos { get; set; } = null!;
}
