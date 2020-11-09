using System;
using System.Collections.Generic;
using System.Linq;
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

        [HttpGet]
        public ActionResult<IEnumerable<Course>> Get()
        {
            var course = db.Courses.FirstOrDefault(c => c.Id == 1);
            var student = db.Students.FirstOrDefault(c => c.Id == 2);
            student.Courses.Add(course);
            db.SaveChanges();   
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