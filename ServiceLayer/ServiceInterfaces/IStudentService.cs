using DatabaseStructure.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.ServiceInterfaces
{
    public interface IStudentService
    {
        Collection<Course> GetStudentCourses(int studentId);

        void AddCourse(int studentId, int courseId);

    }
}
