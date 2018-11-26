using Assecor.Data.Entities;
using Assecor.Services.Dtos;
using AutoMapper;
using Assecor.Services.CsvHelper;

namespace Assecor.Services.Mappings
{
    public class Configuration : Profile
    {
        public Configuration()
        {
            CreateMap<Person, PersonDto>().ForMember(dest=>dest.Color, opts => opts.MapFrom(src=>src.Color!=null?src.Color.Name:null));
            CreateMap<ColorDto, Color>().ForMember(dest => dest.Persons, opts => opts.Ignore());
            CreateMap<Color, ColorDto>();
            CreateMap<Person, CsvDataModel>().ForMember(dest => dest.Address, opts => opts.MapFrom(src => src.Zipcode+" "+src.City));
        }
    }
}
