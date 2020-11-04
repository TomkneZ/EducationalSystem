using DatabaseStructure;
using DatabaseStructure.Models;
using ServiceLayer.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
    public class ProfessorService : IProfessorService
    {
        private readonly DBContext context;

        public ProfessorService(DBContext context)
        {
            this.context = context;
        }

        public void AddCourse(int professorId, int courseId)
        {
            var professor = context.Professors.FirstOrDefault(p => p.PersonId == professorId);
            var course = context.Courses.FirstOrDefault(c => c.CourseId == courseId);
            professor.ProfessorCourses.Add(course);
            context.SaveChanges();
        }

        public IQueryable<Course> ShowProfessorCourses(int professorId)
        {
            return context.Courses.Where(c => c.CourseId == professorId);
        }
    }
}
