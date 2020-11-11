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

        [HttpPost("{action}")]
        public ActionResult<Course> AddCourse([FromBody]Course course)
        {            
            if (course == null)
            {
                return NotFound();
            }            
            db.Courses.Add(course);            
            var professor = db.Professors.FirstOrDefault(p => p.Id == course.ProfessorId);           
            course.Professor = professor;            
            db.SaveChanges();
            var school = db.Schools.FirstOrDefault(s => s.Id == professor.SchoolId);
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Course, CreateCourseViewModel>()
            .ForMember("ProfessorName", opt => opt.MapFrom(src => professor.FirstName +" " + professor.LastName))
            .ForMember("SchoolName", opt => opt.MapFrom(src => school.Name)));           
            var mapper = new Mapper(config);
            var courseViewModel = mapper.Map<Course, CreateCourseViewModel>(course);
            return Ok(courseViewModel);
        }

        [HttpPost("{action}/{studentId}")]
        public ActionResult<Student> AddStudentToCourse([FromBody]Course course, int studentId)
        {
            var student = db.Students.FirstOrDefault(s => s.Id == studentId);
            if (student == null && course == null)
            {
                return NotFound();
            }
            dataManager.CoursesService.AddStudent(course, student);
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Student, PersonViewModel>()
            .ForMember("Name", opt => opt.MapFrom(src => src.FirstName + " " + src.LastName)));
            var mapper = new Mapper(config);
            var studentViewModel = mapper.Map<Student,PersonViewModel>(student);
            return Ok(studentViewModel);
        }

        [HttpDelete("{action}/{studentId}")]
        public ActionResult<Student> DeleteStudentFromCourse([FromBody]Course course, int studentId)
        {
            var student = db.Students.FirstOrDefault(s => s.Id == studentId);        
            if (course == null && student == null)
            {
                return NotFound();
            }           
            dataManager.CoursesService.DeleteStudent(course, student);        
            return Ok();
        }

        [HttpGet("{action}/{courseId}")]
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