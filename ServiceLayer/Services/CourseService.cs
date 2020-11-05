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
    public class CourseService : ICourseService
    {
        private readonly DBContext context;

        public CourseService(DBContext context)
        {
            this.context = context;
        }

        public void AddCourseInDb(string name, int uniquecode, bool isActive)
        {
            var course = new Course()
            {
                Name = name,
                UniqueCode = uniquecode,
                IsActive = isActive
            };
            context.Courses.Add(course);
            context.SaveChanges();
        }

        public void AddProfessor(int professorId, int courseId)
        {
            var course = context.Courses.FirstOrDefault(c => c.UniqueCode == courseId);
            var professor = context.Professors.FirstOrDefault(p => p.ProfessorId == professorId);
            course.CourseProfessor = professor;
            context.SaveChanges();
        }

        public void AddStudent(int studentId, int courseId)
        {
            var course = context.Courses.FirstOrDefault(c => c.UniqueCode == courseId);
            var student = context.Students.FirstOrDefault(s => s.StudentId == studentId);
            course.CourseStudents.Add(student);
            context.SaveChanges();
        }

    }
}
