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
                    dataManager.CoursesService.AddCourse("OOOP", 123, true);
                    dataManager.ProfessorsService.AddProfessor("Lia", "Silver", "liasilver@mail.ru", "+375292477096", true);
                    dataManager.StudentsService.AddStudent("Lina", "Rusinovich", "lina.rusinovichr@mail.ru", "+375293245096", true);
                    dataManager.SchoolsService.AddSchool("School n5");
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
