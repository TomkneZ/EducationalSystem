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

        public void AddProfessor(int professorId, int schoolId)
        {
            var school = context.Schools.FirstOrDefault(s => s.SchoolId == schoolId);
            var professor = context.Professors.FirstOrDefault(p => p.ProfessorId == professorId);
            school.SchoolProfessors.Add(professor);
            context.SaveChanges();
        }

        public void AddSchool(string name)
        {
            if (context.Schools.FirstOrDefault(s => s.Name == name) == null)
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

        public void AddStudent(int studentId, int schoolId)
        {
            var school = context.Schools.FirstOrDefault(s => s.SchoolId == schoolId);
            var student = context.Students.FirstOrDefault(s => s.StudentId == studentId);
            school.SchoolStudents.Add(student);
            context.SaveChanges();
        }

        public List<Professor> GetProfessors(int schoolId)
        {
            var school = context.Schools.FirstOrDefault(s => s.SchoolId == schoolId);
            return school.SchoolProfessors.ToList();
        }

        public List<Student> GetStudents(int schoolId)
        {
            var school = context.Schools.FirstOrDefault(s => s.SchoolId == schoolId);
            return school.SchoolStudents.ToList();
        }
    }
}