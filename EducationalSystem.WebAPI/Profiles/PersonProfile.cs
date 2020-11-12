using AutoMapper;
using DatabaseStructure.AbstractModels;
using DatabaseStructure.Models;
using EducationalSystem.WebAPI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationalSystem.WebAPI.Profiles
{
    public class PersonProfile : Profile
    {
        public PersonProfile()
        {
            CreateMap<Person, PersonViewModel>()
              .ForMember("Name", opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"));
        }
    }
}
