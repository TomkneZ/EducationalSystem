using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseStructure.Models
{
    public class Professor
    {
        public virtual School ProfessorSchool { get; set; }

        public virtual ICollection<Course> ProfessorCourses { get; set; }
    }
}
