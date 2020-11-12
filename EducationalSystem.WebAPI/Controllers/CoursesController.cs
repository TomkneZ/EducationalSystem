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
        private readonly IMapper _mapper;

        public CoursesController(DBContext context, IMapper mapper)
        {
            db = context;
            dataManager = new DataManager(db);
            _mapper = mapper;
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
            dataManager.CoursesService.AddStudent(course, student);
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
            dataManager.CoursesService.DeleteStudent(course, student);
            return Ok();
        }

        [HttpGet("{action}/{code}")]
        public bool IsCourseCodeUnique(int code)
        {
            return dataManager.CoursesService.IsCodeUnique(code);
        }
    }
}