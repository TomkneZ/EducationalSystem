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

        public void AddCourse(string name, int uniqueCode, bool isActive)
        {
            var IsCourseExists = context.Courses.Any(c => c.Name == name);
            if (!IsCourseExists)
            {
                var course = new Course()
                {
                    Name = name,
                    UniqueCode = uniqueCode,
                    IsActive = isActive
                };
                context.Courses.Add(course);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("This course exists!");
            }
        }

        public void AddStudent(Course course, Student student)
        {
            student.StudentCourses.Add(new StudentCourse { CourseId = course.Id , StudentId = student.Id });
            context.SaveChanges();
        }

        public void DeleteStudent(Course course, Student student)
        {
            var studentCourse = student.StudentCourses.FirstOrDefault(sc => sc.CourseId == course.Id);
            student.StudentCourses.Remove(studentCourse);
            context.SaveChanges();
        }

        public bool IsCodeUnique(int uniqueCode)
        {
            var courses = context.Courses.Where(c => c.UniqueCode == uniqueCode).ToList();
            if (courses.Count > 1)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}