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
using ServiceLayer.ServiceInterfaces;

namespace EducationalSystem.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly DBContext db;        
        private readonly IStudentService _studentService;
        private readonly ISchoolService _schoolService;
        private readonly IMapper _mapper;

        public StudentsController(DBContext context, IMapper mapper, IStudentService studentService, ISchoolService schoolService)
        {
            db = context;            
            _mapper = mapper;
            _studentService = studentService;
            _schoolService = schoolService;
        }    

        [HttpGet("{action}")]
        public ActionResult<IEnumerable<Student>> GetActiveStudents()
        {            
            var students = _mapper.Map<IEnumerable<Student>, List<ActivePersonViewModel>>(_studentService.GetActiveStudents());
            return Ok(students);
        }

        [HttpGet("{action}/{schoolId}")]
        public ActionResult<Student> GetSchoolActiveStudents(int schoolId)
        {
            var students = _mapper.Map<IEnumerable<Student>, List<PersonViewModel>>(_schoolService.GetActiveStudents(schoolId));
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
            SendActivationMessage(student.FirstName, student.LastName, student.Email, student.Id);
            return Ok("Message to activate account was sent");
        }

        private void SendActivationMessage(string firstName, string lastName, string email, int studentId)
        {
            SmtpClient smtpClient = new SmtpClient(Config.SmtpHost, Config.SmtpPort);
            MailAddress from = new MailAddress("support@educationalsystem.com", "Educational System");
            MailAddress to = new MailAddress(email);
            MailMessage m = new MailMessage(from, to);
            m.Subject = "Activate your account";
            m.IsBodyHtml = true;
            m.Body = $"<p>Hi, dear student {firstName} {lastName}</p> <a href='https://localhost:44370/api/Students/ActivateStudent/{studentId}'>Activate</a>";
            smtpClient.Send(m);
        }
    }
}