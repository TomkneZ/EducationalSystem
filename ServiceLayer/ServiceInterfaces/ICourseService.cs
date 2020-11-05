using DatabaseStructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.ServiceInterfaces
{
    public interface ICourseService
    {
        void AddStudent(int studentId, int courseId);

        void AddProfessor(int professorId, int courseId);

        void AddCourse(string name, int uniqueCode, bool isActive);
    }
}
