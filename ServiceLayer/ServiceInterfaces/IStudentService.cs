using DatabaseStructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.ServiceInterfaces
{
    public interface IStudentService
    {
        IQueryable<Course> ShowStudentCourses(int studentId);

        void AddCourse(int studentId, int courseId);

    }
}
