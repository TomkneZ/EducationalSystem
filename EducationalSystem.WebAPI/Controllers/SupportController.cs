using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatabaseStructure;
using DatabaseStructure.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;

namespace EducationalSystem.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupportController : ControllerBase
    {
        DBContext db;
        DataManager dataManager;

        public SupportController(DBContext context)
        {
            db = context;
            dataManager = new DataManager(db);
        }

        [HttpGet("{studentId}")]
        public ActionResult<Student> ActivateStudent(int studentId)
        {
            var student = db.Students.FirstOrDefault(s => s.Id == studentId);
            student.IsAccountActive = true;            
            return Ok(student);
        }

        [HttpPost]
        public ActionResult<Student> Post()
        {
            var student = dataManager.StudentsService.AddStudent("Alinaa", "Sinyaeva", "ssinalinakit@mail.ru", "+375447887887", false);
            SmtpClient smtpClient = new SmtpClient("localhost", 25);
            MailAddress from = new MailAddress("support@educationalsystem.com", "Educational System");
            MailAddress to = new MailAddress(student.Email);
            MailMessage m = new MailMessage(from, to);
            m.Subject = "Activate your account";
            m.IsBodyHtml = true;
            m.Body = $"<p>Hi, dear student {student.FirstName} {student.LastName}</p> <a href='https://localhost:44370/api/Support/{student.Id}'>Activate</a>";
            smtpClient.Send(m);
            return Ok("Message to activate account was sent");
        }
    }
}