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
using ServiceLayer.ServiceInterfaces;

namespace EducationalSystem.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfessorsController : ControllerBase
    {
        private readonly DBContext db;        
        private readonly IMapper _mapper;
        private readonly IProfessorService _professorService;

        public ProfessorsController(DBContext context, IMapper mapper, IProfessorService professorService)
        {
            db = context;            
            _mapper = mapper;
            _professorService = professorService;
        }

        [HttpGet("{action}")]
        public ActionResult<IEnumerable<Professor>> GetActiveProfessors()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Professor, ActivePersonViewModel>()
                .ForMember("Name", opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
                .ForMember("SchoolName", opt => opt.MapFrom(src => db.Schools.FirstOrDefault(s => s.Id == src.SchoolId).Name)));            
            var mapper = new Mapper(config);
            var professors = mapper.Map<IEnumerable<Professor>, List<ActivePersonViewModel>>(_professorService.GetActiveProfessors());
            return Ok(professors);
        }

        [HttpPut("{action}")]
        public ActionResult<Professor> EditProfessor([FromBody]Professor professor)
        {
            if (professor == null)
            {
                return NotFound();
            }
            _professorService.EditProfessor(professor);
            return Ok(_mapper.Map<Professor, PersonViewModel>(professor));
        }

        [HttpGet("{action}/{professorId}")]
        public ActionResult<Course> GetProfessorActiveCourses(int professorId)
        {
            var courses = _mapper.Map<IEnumerable<Course>, List<ActiveProfessorCoursesViewModel>>(_professorService.GetActiveCourses(professorId));
            return Ok(courses);
        }
    }
}