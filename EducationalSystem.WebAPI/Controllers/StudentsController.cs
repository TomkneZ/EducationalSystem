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
        public ActionResult<IEnumerable<Student>> GetActiveStudent()
        {            
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Student, ActivePersonViewModel>()
               .ForMember("Name", opt => opt.MapFrom(src => src.FirstName + " " + src.LastName))
               .ForMember("SchoolName", opt => opt.MapFrom(src => db.Schools.FirstOrDefault(s => s.Id == src.SchoolId).Name)));
            var mapper = new Mapper(config);
            var students = mapper.Map<IEnumerable<Student>, List<ActivePersonViewModel>>(dataManager.StudentsService.GetActiveStudents());
            return Ok(students);           
        }

        [HttpGet("{schoolId}")]
        public ActionResult<Student> GetSchoolActiveStudents(int schoolId)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Student, PersonViewModel>()
              .ForMember("Name", opt => opt.MapFrom(src => src.FirstName + " " + src.LastName)));              
            var mapper = new Mapper(config);          
            var students = mapper.Map<IEnumerable<Student>, List<PersonViewModel>>(dataManager.SchoolsService.GetActiveStudents(schoolId));
            return Ok(students);
        }
    }
}