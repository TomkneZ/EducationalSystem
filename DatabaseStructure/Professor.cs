using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatabaseStructure
{
    public class Professor
    {
        public int ProfessorId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime RegistrationDate { get; set; }

        public bool IsAccountActive { get; set; }

        public virtual School ProfessorSchool { get; set; }

        public virtual ICollection<Course> ProfessorCourses { get; set; }

    }
}
