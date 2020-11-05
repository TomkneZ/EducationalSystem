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
        List<Course> GetProfessorCourses(int professorId);

        void AddCourse(int professorId, int courseId);

        void AddProfessor(string firstName, string lastName, string email, string phone, bool isActive);
    }
}