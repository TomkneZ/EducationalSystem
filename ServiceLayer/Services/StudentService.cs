using DatabaseStructure;
using DatabaseStructure.Models;
using ServiceLayer.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        void IStudentService.AddCourse(int studentId, int courseId)
        {
            var student = context.Students.FirstOrDefault(s => s.StudentId == studentId);
            var course = context.Courses.FirstOrDefault(c => c.UniqueCode == courseId);
            student.StudentCourses.Add(course);
            context.SaveChanges();
        }

        Collection<Course> IStudentService.GetStudentCourses(int studentId)
        {
            var student = context.Students.FirstOrDefault(c => c.StudentId == studentId);
            return student.StudentCourses;
        }
    }
}
