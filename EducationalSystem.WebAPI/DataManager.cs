using DatabaseStructure;
using ServiceLayer.Services;
using ServiceLayer.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationalSystem.WebAPI
{
    public class DataManager
    {
        public ICourseService CoursesService { get; set; }

        public IProfessorService ProfessorsService { get; set; }

        public ISchoolService SchoolsService { get; set; }

        public IStudentService StudentsService { get; set; }

        public DataManager(DBContext dbContext)
        {
            CoursesService = new CourseService(dbContext);
            ProfessorsService = new ProfessorService(dbContext);
            SchoolsService = new SchoolService(dbContext);
            StudentsService = new StudentService(dbContext);
        }
    }
}
