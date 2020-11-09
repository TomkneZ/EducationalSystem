using DatabaseStructure.Models;
using Microsoft.EntityFrameworkCore;

namespace DatabaseStructure
{
    public class DBContext : DbContext
    {
        public DbSet<SchoolType> SchoolTypes { get; set; }

        public DbSet<Course> Courses { get; set; }

        public DbSet<Professor> Professors { get; set; }

        public DbSet<School> Schools { get; set; }

        public DbSet<Student> Students { get; set; }

        public DBContext(DbContextOptions<DBContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}