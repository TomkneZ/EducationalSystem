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
        public int Id { get; set; }

        public string Name { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public bool IsActive { get; set; }
        
        public virtual ICollection<Student> Students { get; set; }
        
        public virtual ICollection<Professor> Professors { get; set; }

        public School()
        {
            Students = new List<Student>();
            Professors = new List<Professor>();
        }
    }
}
