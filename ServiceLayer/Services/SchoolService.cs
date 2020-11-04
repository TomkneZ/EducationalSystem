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
            var professor = context.Professors.FirstOrDefault(p => p.PersonId == professorId);
            school.SchoolProfessors.Add(professor);
            context.SaveChanges();
        }

        public void AddStudent(int studentId, int schoolId)
        {
            var school = context.Schools.FirstOrDefault(s => s.SchoolId == schoolId);
            var student = context.Students.FirstOrDefault(s => s.PersonId == studentId);
            school.SchoolStudents.Add(student);
            context.SaveChanges();
        }

        public IQueryable<Professor> ShowProfessors(int schoolId)
        {

            return context.Professors.Where(p => p.ProfessorSchool.SchoolId == schoolId);
        }

        public IQueryable<Student> ShowStudents(int schoolId)
        {
            return context.Students.Where(s => s.StudentSchool.SchoolId == schoolId);
        }
    }
}

