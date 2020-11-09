using DatabaseStructure;
using DatabaseStructure.Models;
using ServiceLayer.ServiceInterfaces;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
    public class SchoolService : ISchoolService
    {
        private readonly DBContext context;

        public SchoolService(DBContext context)
        {
            this.context = context;
        }

        public void AddSchool(string name)
        {
            var IsSchoolExists = context.Schools.Any(s => s.Name == name);
            if (IsSchoolExists)
            {
                var school = new School()
                {
                    Name = name
                };
                context.Schools.Add(school);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("This school already exists!");
            }               
        }

        public List<Student> GetActiveStudents(int schoolId)
        {
            return context.Students.Where(s => (s.SchoolId == schoolId) && (s.IsAccountActive)).ToList();
        }
    }
}