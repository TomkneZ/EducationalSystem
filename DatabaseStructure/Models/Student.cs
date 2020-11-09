using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseStructure.AbstractModels;

namespace DatabaseStructure.Models
{
    public class Student : Person
    {
        public int? SchoolId { get; set; }

        public virtual School School { get; set; }

        public virtual ICollection<Course> Courses { get; set; }

        public Student()
        {
            Courses = new List<Course>();
        }
    }
}
