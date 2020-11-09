using DatabaseStructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.ServiceInterfaces
{
    public interface IProfessorService
    {
        void AddProfessor(string firstName, string lastName, string email, string phone, bool isActive);

        List<Professor> GetActiveProfessors();

        void EditProfessor(Professor professor);

        List<Course> GetActiveCourses(int professorId);
    }
}