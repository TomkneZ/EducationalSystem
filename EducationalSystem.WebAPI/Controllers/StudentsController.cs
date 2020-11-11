using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using DatabaseStructure.Models;
using AutoMapper;
using EducationalSystem.WebAPI.ViewModels;
using System.Net.Http;
using System.Net;
using System.Threading.Tasks;
using DatabaseStructure;
using System;
using Microsoft.AspNetCore.Routing.Constraints;
using System.Net.Mail;

namespace EducationalSystem.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]    
    public class StudentsController : ControllerBase
    {
        DBContext db;
        DataManager dataManager;

        public StudentsController(DBContext context)
        {
            db = context;
            dataManager = new DataManager(db);
        }
        
        [HttpGet("{action}")]
        public ActionResult<IEnumerable<Student>> GetActiveStudents()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Student, ActivePersonViewModel>()
               .ForMember("Name", opt => opt.MapFrom(src => src.FirstName + " " + src.LastName))
               .ForMember("SchoolName", opt => opt.MapFrom(src => db.Schools.FirstOrDefault(s => s.Id == src.SchoolId).Name)));
            var mapper = new Mapper(config);
            var students = mapper.Map<IEnumerable<Student>, List<ActivePersonViewModel>>(dataManager.StudentsService.GetActiveStudents());
            return Ok(students);
        }

        [HttpGet("{action}/{schoolId}")]
        public ActionResult<Student> GetSchoolActiveStudents(int schoolId)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Student, PersonViewModel>()
              .ForMember("Name", opt => opt.MapFrom(src => src.FirstName + " " + src.LastName)));
            var mapper = new Mapper(config);
            var students = mapper.Map<IEnumerable<Student>, List<PersonViewModel>>(dataManager.SchoolsService.GetActiveStudents(schoolId));
            return Ok(students);
        }

        [HttpGet("{action}/{studentId}")]
        public ActionResult<Student> ActivateStudent(int studentId)
        {
            var student = db.Students.FirstOrDefault(s => s.Id == studentId);
            student.IsAccountActive = true;
            db.SaveChanges();
            return Ok(student);
        }

        [HttpPost("{action}")]
        public ActionResult<Student> AddStudent([FromBody]Student student)
        {
            if (student == null)
            {
                return NotFound();
            }
            db.Students.Add(student);
            db.SaveChanges();
            SmtpClient smtpClient = new SmtpClient("localhost", 25);
            MailAddress from = new MailAddress("support@educationalsystem.com", "Educational System");
            MailAddress to = new MailAddress(student.Email);
            MailMessage m = new MailMessage(from, to);
            m.Subject = "Activate your account";
            m.IsBodyHtml = true;
            m.Body = $"<p>Hi, dear student {student.FirstName} {student.LastName}</p> <a href='https://localhost:44370/api/Students/ActivateStudent/{student.Id}'>Activate</a>";
            smtpClient.Send(m);
            return Ok("Message to activate account was sent");           
        }
    }
}