using Microsoft.EntityFrameworkCore;
using api.Data.Models;

namespace api.Data.DbContexts
{
    public class ToDoDbContext : DbContext
    {
        public DbSet<ToDo> ToDos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("ToDos");
        }
    }
}