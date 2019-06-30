using AutoMapper;
using TesteLivraria.Dto;
using TesteLivraria.Models;

namespace TesteLivraria.App_Start
{
    public class MappingProfile : Profile
    {

        public MappingProfile()
        {
            Mapper.CreateMap<Autor, AutorDto>();
            Mapper.CreateMap<AutorDto, Autor>();
            Mapper.CreateMap<Livro, LivroDto>();
            Mapper.CreateMap<LivroDto, Livro>();


        }
    }
}