using DatabaseStructure;
using DatabaseStructure.Models;
using ServiceLayer.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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

        public void AddProfessor(string firstName, string lastName, string email, string phone, bool isActive)
        {
            var IsProfessorExists = context.Professors.Any(p => p.Email == email);
            if (IsProfessorExists)
            {
                var professor = new Professor()
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    Phone = phone,
                    IsAccountActive = isActive
                };
                context.Professors.Add(professor);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("This professor already in Database!");
            }
        }

        public void EditProfessor(Professor professor)
        {
            context.Update(professor);
            context.SaveChanges();
        }

        public List<Course> GetActiveCourses(int professorId)
        {
            return context.Courses.Where(c => (c.ProfessorId == professorId ) && (c.IsActive)).ToList();
        }

        public List<Professor> GetActiveProfessors()
        {
            return context.Professors.Where(p => p.IsAccountActive).ToList();
        }
    }
}