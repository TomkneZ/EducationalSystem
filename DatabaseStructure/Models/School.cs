using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseStructure.Models
{
    public class School
    {
        public int SchoolId { get; set; }

        public string Name { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public bool IsActive { get; set; }

        public virtual ICollection<Student> SchoolStudents { get; set; }

        public virtual ICollection<Professor> SchoolProfessors { get; set; }
    }
}
