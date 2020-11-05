using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseStructure.AbstractModels;

namespace DatabaseStructure.Models
{
    public class Professor : Person
    {
        public int ProfessorId { get; set; }

        public virtual School ProfessorSchool { get; set; }

        public virtual ICollection<Course> ProfessorCourses { get; set; }
    }
}
