using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TesteLivraria.DB;
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