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
        public ProfessorsController(DBContext context)
        {
            db = context;
            dataManager = new DataManager(db);
        }

        [HttpGet]
        public ActionResult<IEnumerable<Professor>> Get()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Professor, ActivePersonViewModel>()
               .ForMember("Name", opt => opt.MapFrom(src => src.FirstName + " " + src.LastName)));
            var mapper = new Mapper(config);
            var professors = mapper.Map<IEnumerable<Professor>, List<ActivePersonViewModel>>(dataManager.ProfessorsService.GetActiveProfessors());
            return Ok(professors);
        }

        [HttpPut]
        public ActionResult<Professor> Put(int professorId)
        {            
            var professor = db.Professors.FirstOrDefault(x => x.ProfessorId == professorId);           
            if (professor == null)
            {
                return BadRequest("Professor Not Found");
            }
            professor.FirstName = "Edited";
            professor.LastName = "Professor";
            dataManager.ProfessorsService.EditProfessor(professor);
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Professor, EditProfessorViewModel>()
               .ForMember("Name", opt => opt.MapFrom(src => src.FirstName + " " + src.LastName)));
            var mapper = new Mapper(config);
            var professorViewModel = mapper.Map<Professor, EditProfessorViewModel>(professor);
            return Ok(professorViewModel);
        }
    }
}