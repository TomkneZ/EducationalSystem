using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using AutoMapper;
using DatabaseStructure;
using DatabaseStructure.Models;
using EducationalSystem.WebAPI.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EducationalSystem.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        DBContext db;
        DataManager dataManager;

        public CoursesController(DBContext context)
        {
            db = context;
            dataManager = new DataManager(db);            
        }        

        [HttpPost("{studentId}")]
        public ActionResult<Student> Post(int studentId)
        {
            var student = db.Students.FirstOrDefault(s => s.Id == studentId);
            var course = db.Courses.FirstOrDefault(c => c.Id == 4);
            if (student == null)
            {
                return NotFound("student wasn't found!");
            }
            dataManager.CoursesService.AddStudent(course, student);
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Student, PersonViewModel>()
            .ForMember("Name", opt => opt.MapFrom(src => src.FirstName + " " + src.LastName)));
            var mapper = new Mapper(config);
            var studentViewModel = mapper.Map<Student,PersonViewModel>(student);
            return Ok(studentViewModel);
        }

        [HttpDelete("{courseId}")]
        public ActionResult<Student> Delete(int courseId)
        {
            var student = db.Students.FirstOrDefault(s => s.Id == 2);
            var course = db.Courses.FirstOrDefault(c => c.Id == courseId);
            if (course == null && student == null)
            {
                return NotFound("Student/Course wasn't found");
            }           
            dataManager.CoursesService.DeleteStudent(course, student);        
            return Ok(student);
        }

        [HttpGet("{courseId}")]
        public ContentResult IsCourseCodeUnique(int courseId)
        {
            var course = db.Courses.FirstOrDefault(c => c.Id == courseId);
            var IsCourseCodeUnique = dataManager.CoursesService.IsCodeUnique(course.UniqueCode);
            if (IsCourseCodeUnique)
            {
                return Content("Unique");
            }
            else
            {
                return Content("Not Unique");
            }
        }
    }
}