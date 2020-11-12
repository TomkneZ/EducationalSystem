using AutoMapper;
using DatabaseStructure.Models;
using EducationalSystem.WebAPI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationalSystem.WebAPI.Profiles
{
    public class ActiveProfessorCoursesProfile : Profile
    {
        public ActiveProfessorCoursesProfile()
        {
            CreateMap<Course, ActiveProfessorCoursesViewModel>();
        }
    }
}
