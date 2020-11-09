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
            var IsStudentExists = context.Students.Any(s => s.Email == email);
            if (IsStudentExists)
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

        public List<Student> GetActiveStudents()
        {
            return context.Students.Where(s => s.IsAccountActive).ToList();
        }
    }
}