using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseStructure
{
    public class Course
    {
        public int CourseId { get; set; }

        public string Name { get; set; }

        public int UniqueCode { get; set; }

        public bool IsActive { get; set; }

        public virtual Professor CourseProfessor { get; set; }

        public virtual ICollection<Student> CourseStudents { get; set; }

    }
}
