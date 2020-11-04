using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseStructure.Models;

namespace DatabaseStructure
{
    public class DBContext : DbContext
    {
        public DBContext() : base("DbConnectionString")
        {

        }

        public DbSet<Student> Students { get; set; }

        public DbSet<Course> Courses { get; set; }

        public DbSet<Professor> Professors { get; set; }

        public DbSet<School> Schools { get; set; }

        public DbSet<SchoolType> SchoolTypes { get; set; }


    }
}
