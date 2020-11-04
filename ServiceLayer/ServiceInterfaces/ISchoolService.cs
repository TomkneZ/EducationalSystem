using DatabaseStructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.ServiceInterfaces
{
    public interface ISchoolService
    {
        ICollection<Student> GetStudents(int schoolId);

        ICollection<Professor> GetProfessors(int schoolId);

        void AddStudent(int studentId, int schoolId);

        void AddProfessor(int professorId, int schoolId);

    }
}
