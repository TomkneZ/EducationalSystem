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
        void AddCourse(string name, int uniqueCode, bool isActive);

        bool IsCodeUnique(int uniqueCode);

        void AddStudent(Course course, Student student);

        void DeleteStudent(Course course, Student student);
    }
}
