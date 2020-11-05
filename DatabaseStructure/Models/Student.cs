using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseStructure.AbstractModels;

namespace DatabaseStructure.Models
{
    public class Student : Person
    {
        public int StudentId { get; set; }

        public virtual School StudentSchool { get; set; }

        public virtual Collection<Course> StudentCourses { get; set; }
    }
}
