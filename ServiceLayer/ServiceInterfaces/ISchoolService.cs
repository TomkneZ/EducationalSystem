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
        IQueryable<Student> ShowStudents(int schoolId);

        IQueryable<Professor> ShowProfessors(int schoolId);

        void AddStudent(int studentId, int schoolId);

        void AddProfessor(int professorId, int schoolId);

    }
}
