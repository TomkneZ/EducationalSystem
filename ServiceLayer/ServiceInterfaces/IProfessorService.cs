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
        IQueryable<Course> ShowProfessorCourses(int professorId);

        void AddCourse(int professorId, int courseId);
    }
}
