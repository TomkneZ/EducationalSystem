using DatabaseStructure.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.ServiceInterfaces
{
    public interface IStudentService
    {
        Student AddStudent(string firstName, string lastName, string email, string phone, bool isActive);

        List<Student> GetActiveStudents();
    }
}