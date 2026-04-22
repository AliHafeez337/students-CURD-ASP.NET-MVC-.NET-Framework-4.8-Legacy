using System.Data.Entity;

namespace CrudApp.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() : base("DefaultConnection")
        {
        }

        public DbSet<Student> Students { get; set; }
    }
}