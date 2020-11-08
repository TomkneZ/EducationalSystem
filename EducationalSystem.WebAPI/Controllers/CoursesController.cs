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

        [HttpPost]
        public ActionResult<Course> Post()
        {
            dataManager.CoursesService.AddCourse("Programming languages", 101, true);
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Course, CreateCourseViewModel>());             
            var mapper = new Mapper(config);
            var course = mapper.Map<Course, CreateCourseViewModel>(db.Courses.FirstOrDefault(c => c.UniqueCode == 101));
            return Ok(course);
        }       
    }
}