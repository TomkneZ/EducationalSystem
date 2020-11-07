using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using DatabaseStructure.Models;
using AutoMapper;
using EducationalSystem.WebAPI.ViewModels;
using System.Net.Http;
using System.Net;

namespace EducationalSystem.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : ControllerBase
    {
        DBContext db;
        public StudentsController(DBContext context)
        {
            db = context;
        }

        [HttpGet]
        public HttpResponseMessage Get()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Student, ActivePersonViewModel>()
                    .ForMember("Name", opt => opt.MapFrom(src => src.FirstName + " " + src.LastName)));
            var mapper = new Mapper(config);
            var students = mapper.Map<IEnumerable<Student>, List<ActivePersonViewModel>>(db.Students.Where(s => s.IsAccountActive == true));
            var response = Request.CreateResponse<IEnumerable<Student>>(HttpStatusCode.OK, db.Students);
            return response;
        }

        [HttpGet("{id}")]
        public ContentResult Get(int SchoolId)
        {           
            return Content("ZZZZ");
        }
    }
}