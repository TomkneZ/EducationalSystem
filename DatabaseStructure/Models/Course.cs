using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseStructure.Models
{
    public class Course
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int UniqueCode { get; set; }

        public bool IsActive { get; set; }

        public int? ProfessorId { get; set; }

        public virtual Professor Professor { get; set; }

        public virtual ICollection<Student> Students { get; set; }

        public Course()
        {
            Students = new List<Student>();
        }            
    }
}
