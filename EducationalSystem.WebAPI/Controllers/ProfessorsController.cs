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
    public class ProfessorsController : ControllerBase
    {
        DBContext db;
        DataManager dataManager;
        private readonly IMapper _mapper;

        public ProfessorsController(DBContext context, IMapper mapper)
        {
            db = context;
            dataManager = new DataManager(db);
            _mapper = mapper;
        }

        [HttpGet("{action}")]
        public ActionResult<IEnumerable<Professor>> GetActiveProfessors()
        {
            var professors = _mapper.Map<IEnumerable<Professor>, List<ActivePersonViewModel>>(dataManager.ProfessorsService.GetActiveProfessors());
            return Ok(professors);
        }

        [HttpPut("{action}")]
        public ActionResult<Professor> EditProfessor([FromBody]Professor professor)
        {
            if (professor == null)
            {
                return NotFound();
            }
            dataManager.ProfessorsService.EditProfessor(professor);
            return Ok(_mapper.Map<Professor, PersonViewModel>(professor));
        }

        [HttpGet("{action}/{professorId}")]
        public ActionResult<Course> GetProfessorActiveCourses(int professorId)
        {
            var courses = _mapper.Map<IEnumerable<Course>, List<ActiveProfessorCoursesViewModel>>(dataManager.ProfessorsService.GetActiveCourses(professorId));
            return Ok(courses);
        }
    }
}