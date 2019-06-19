using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TesteLivraria.Dto;
using TesteLivraria.Models;

namespace TesteLivraria.Controllers.Api
{
    public class LivroController : ApiController
    {

        //Get api/Livro
        public IEnumerable<LivroDto> GetLivros()
        {
            return   new Livro().Listar().Select(Mapper.Map<Livro, LivroDto>);
        }

        //Get api/Livro/1
        public IHttpActionResult GetLivro(int id)
        {
            Livro livroTemp = new Livro();
            livroTemp.Id = id;

            var retorno = Mapper.Map<Livro, LivroDto>(livroTemp.BuscarLivro("Id"));

            if (retorno == null)
            {
                return NotFound();
            }


            return Ok(retorno);
        }

        //POST /api/Livro
        [HttpPost]
        public IHttpActionResult CreateLivro(LivroDto livroDto)
        {

            if (!ModelState.IsValid)
            {
            
                return BadRequest();
            }

            if (livroDto != null)
            {
                var livro = Mapper.Map<LivroDto, Livro>(livroDto);
                livro.Cadastrar();

                Mapper.Map<Livro, LivroDto>(livro.BuscarLivro("ISNB"));

                livroDto.Id = livro.BuscarLivro("ISNB").Id;

                return Created(new Uri(Request.RequestUri +"/"+livro.Id), livroDto);
            }

            return null;
        }

        //PUT api/Livro/1
        [HttpPut]
        public IHttpActionResult UpdateLivro(int id,LivroDto livroDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            Livro livroTemp = new Livro();
            livroTemp.Id = id;
            
            var info = livroTemp.BuscarLivro("Id");

            if (info == null)
                return NotFound();
            else
            {
                Mapper.Map(livroDto, info);
                info.Atualizar();
                return Ok();
            }
                 


        }

        //DELETE api/Livro/1
        [HttpDelete]
        public IHttpActionResult DeleteLivro(int id)
        {
            Livro livroTemp = new Livro();
            livroTemp.Id = id;




            int resultado = livroTemp.Excluir();

            if(resultado == 0)
            {
                return NotFound();
            }
            else
            return Ok();

        }
    }
}
