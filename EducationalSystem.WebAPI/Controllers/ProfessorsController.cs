﻿using System;
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
        public ActionResult<IEnumerable<Professor>> GetActiveProfessors()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Professor, ActivePersonViewModel>()
               .ForMember("Name", opt => opt.MapFrom(src => src.FirstName + " " + src.LastName))
               .ForMember("SchoolName", opt => opt.MapFrom(src => db.Schools.FirstOrDefault(s => s.Id == src.SchoolId).Name)));
            var mapper = new Mapper(config);
            var professors = mapper.Map<IEnumerable<Professor>, List<ActivePersonViewModel>>(dataManager.ProfessorsService.GetActiveProfessors());
            return Ok(professors);
        }

        [HttpPut]
        public ActionResult<Professor> EditProfessor(int professorId)
        {
            var professor = db.Professors.FirstOrDefault(p => p.Id == professorId);
            if (professor == null)
            {
                return NotFound();
            }
            professor.FirstName = "Edited";
            professor.LastName = "Professor";
            dataManager.ProfessorsService.EditProfessor(professor);
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Professor, PersonViewModel>()
             .ForMember("Name", opt => opt.MapFrom(src => src.FirstName + " " + src.LastName)));             
            var mapper = new Mapper(config);
            var professorViewModel = mapper.Map<Professor, PersonViewModel>(professor);
            return Ok(professorViewModel);
        }

        [HttpGet("{professorId}")]
        public ActionResult<Course> GetProfessorActiveCourses(int professorId)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Course, ActiveProfessorCoursesViewModel>());             
            var mapper = new Mapper(config);
            var courses = mapper.Map<IEnumerable<Course>, List<ActiveProfessorCoursesViewModel>>(dataManager.ProfessorsService.GetActiveCourses(professorId));
            return Ok(courses);
        }
    }
}