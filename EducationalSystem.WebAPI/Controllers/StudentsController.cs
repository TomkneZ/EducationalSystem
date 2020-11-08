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

namespace EducationalSystem.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : ControllerBase
    {
        DBContext db;
        DataManager dataManager;
        public StudentsController(DBContext context)
        {
            db = context;
            dataManager = new DataManager(db);
        }

        [HttpGet]
        public ActionResult<IEnumerable<Student>> Get()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Student, ActivePersonViewModel>()
               .ForMember("Name", opt => opt.MapFrom(src => src.FirstName + " " + src.LastName)));
            var mapper = new Mapper(config);
            var students = mapper.Map<IEnumerable<Student>, List<ActivePersonViewModel>>(dataManager.StudentsService.GetActiveStudents());
            return Ok(students);
        }

        [HttpPost]
        public ActionResult<IEnumerable<Student>> Post(int studentId, int courseId)
        {
            var student = db.Students.FirstOrDefault(s => s.StudentId == studentId);
            return Ok(student);
        }

        [HttpGet("{id}")]
        public ContentResult Get(int SchoolId)
        {
            return Content("ZZZZ");
        }

    }
}