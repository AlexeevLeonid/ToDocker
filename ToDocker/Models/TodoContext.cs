using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Collections.Generic;

namespace ToDocker.Models
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<TodoItem> todos { get; set; }
    }
}
