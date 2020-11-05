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

        public void AddStudent(string firstName, string lastName, string email, string phone, bool isActive)
        {
            if (context.Students.FirstOrDefault(s => s.Email == email) == null)
            {
                var student = new Student()
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    Phone = phone,
                    IsAccountActive = isActive
                };
                context.Students.Add(student);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("This student already exists!");
            }
        }

        void IStudentService.AddCourse(int studentId, int courseId)
        {
            var student = context.Students.FirstOrDefault(s => s.StudentId == studentId);
            var course = context.Courses.FirstOrDefault(c => c.UniqueCode == courseId);
            student.StudentCourses.Add(course);
            context.SaveChanges();
        }

        List<Course> IStudentService.GetStudentCourses(int studentId)
        {
            var student = context.Students.FirstOrDefault(c => c.StudentId == studentId);
            return student.StudentCourses.ToList();
        }
    }
}