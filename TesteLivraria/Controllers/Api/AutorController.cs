using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using TesteLivraria.Dto;
using TesteLivraria.Models;

namespace TesteLivraria.Controllers.Api
{
    public class AutorController : ApiController
    {

        //Get api/Autor
        public IEnumerable<AutorDto> GetAutors()
        {
            return   new Autor().Listar().Select(Mapper.Map<Autor, AutorDto>);
        }

        //Get api/Autor/1
        public IHttpActionResult GetAutor(int id)
        {
            Autor AutorTemp = new Autor();
            AutorTemp.Id = id;

            var retorno = Mapper.Map<Autor, AutorDto>(AutorTemp.BuscarAutor("Id"));

            if (retorno == null)
            {
                return NotFound();
            }


            return Ok(retorno);
        }

        //POST /api/Autor
        [HttpPost]
        public IHttpActionResult CreateAutor(AutorDto AutorDto)
        {

            if (!ModelState.IsValid)
            {
            
                return BadRequest();
            }

            if (AutorDto != null)
            {
                var Autor = Mapper.Map<AutorDto, Autor>(AutorDto);
                Autor.Cadastrar();

                Mapper.Map<Autor, AutorDto>(Autor.BuscarAutor("Id"));

                AutorDto.Id = Autor.BuscarAutor("Id").Id;

                return Created(new Uri(Request.RequestUri +"/"+Autor.Id), AutorDto);
            }

            return null;
        }

        //PUT api/Autor/1
        [HttpPut]
        public IHttpActionResult UpdateAutor(int id,AutorDto AutorDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            Autor AutorTemp = new Autor();
            AutorTemp.Id = id;
            
            var info = AutorTemp.BuscarAutor("Id");

            if (info == null)
                return NotFound();
            else
            {
                Mapper.Map(AutorDto, info);
                info.Atualizar();
                return Ok();
            }
                 


        }

        //DELETE api/Autor/1
        [HttpDelete]
        public IHttpActionResult DeleteAutor(int id)
        {
            Autor AutorTemp = new Autor();
            AutorTemp.Id = id;




            int resultado = AutorTemp.Excluir();

            if(resultado == 0)
            {
                return NotFound();
            }
            else
            return Ok();

        }
    }
}
