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
using ServiceLayer.ServiceInterfaces;

namespace EducationalSystem.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly DBContext db;        
        private readonly IMapper _mapper;
        private readonly ICourseService _courseService;

        public CoursesController(DBContext context, IMapper mapper, ICourseService courseService)
        {
            db = context;            
            _mapper = mapper;
            _courseService = courseService;
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
            return Ok(_mapper.Map<Course, CreateCourseViewModel>(course));
        }

        [HttpPost("{action}/{studentId}/{courseId}")]
        public ActionResult<Student> AddStudentToCourse(int studentId, int courseId)
        {
            var student = db.Students.FirstOrDefault(s => s.Id == studentId);
            var course = db.Courses.FirstOrDefault(c => c.Id == courseId);
            if (student == null && course == null)
            {
                return NotFound();
            }
            _courseService.AddStudent(course, student);
            return Ok(_mapper.Map<Student, PersonViewModel>(student));
        }

        [HttpDelete("{action}/{studentId}")]
        public ActionResult<Student> DeleteStudentFromCourse([FromBody]Course course, int studentId)
        {
            var student = db.Students.FirstOrDefault(s => s.Id == studentId);
            if (course == null && student == null)
            {
                return NotFound();
            }
            _courseService.DeleteStudent(course, student);
            return Ok();
        }

        [HttpGet("{action}/{code}")]
        public bool IsCourseCodeUnique(int code)
        {
            return _courseService.IsCodeUnique(code);
        }
    }
}