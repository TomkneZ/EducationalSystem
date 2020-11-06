using DatabaseStructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalSystem.DatabaseFillIn
{
    class Program
    {
        static void Main(string[] args)
        {
            DataManager dataManager;
            using (DBContext context = new DBContext())
            {
                dataManager = new DataManager(context);
                try
                {
                    dataManager.CoursesService.AddCourse("Digital marketing", 456, true);
                    dataManager.ProfessorsService.AddProfessor("Dmitry", "Surkov", "d.surkov@mail.ru", "+3752936787096", true);
                    dataManager.StudentsService.AddStudent("Anna", "Safronova", "saffr@mail.ru", "+375293294601", true);
                    dataManager.SchoolsService.AddSchool("School n4");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }               
            }
            Console.ReadLine();
        }
    }
}
