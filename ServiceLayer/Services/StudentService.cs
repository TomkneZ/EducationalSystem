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
    public class StudentService : IStudentService
    {
        private readonly DBContext context;

        public StudentService(DBContext context)
        {
            this.context = context;
        }

        public void AddCourse(int studentId, int courseId)
        {
            var student = context.Students.FirstOrDefault(s => s.PersonId == studentId);
            var course = context.Courses.FirstOrDefault(c => c.CourseId == courseId);
            student.StudentCourses.Add(course);
            context.SaveChanges();
        }

        public IQueryable<Course> ShowStudentCourses(int studentId)
        {
            return context.Courses.Where(c => c.CourseId == studentId);
        }
    }
}
