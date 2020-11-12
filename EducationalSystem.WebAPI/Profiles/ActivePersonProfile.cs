using AutoMapper;
using DatabaseStructure;
using DatabaseStructure.AbstractModels;
using DatabaseStructure.Models;
using EducationalSystem.WebAPI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationalSystem.WebAPI.Profiles
{
    public class ActivePersonProfile : Profile
    {    
        public ActivePersonProfile()
        {
            CreateMap<Student, ActivePersonViewModel>()
                .ForMember("Name", opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
                .ForMember("SchoolName", opt => opt.MapFrom(src => src.School.Name));

            CreateMap<Professor, ActivePersonViewModel>()
                .ForMember("Name", opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
                .ForMember("SchoolName", opt => opt.MapFrom(src => src.School.Name));
        }
    }
}
