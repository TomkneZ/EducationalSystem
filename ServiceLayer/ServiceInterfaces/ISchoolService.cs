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
        void AddSchool(string name);

        List<Student> GetActiveStudents(int schoolId);
    }
}