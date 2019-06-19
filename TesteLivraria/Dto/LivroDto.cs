using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TesteLivraria.Models;

namespace TesteLivraria.Dto
{
    public class LivroDto
    {

        public Int32 Id { get; set; }
        [Required(ErrorMessage = "Favor inserir ISBN.")]
        [StringLength(13)]
        public String ISBN { get; set; }
        [Required(ErrorMessage = "Favor inserir Nome.")]
        [StringLength(150)]
        public String Nome { get; set; }
        [Required(ErrorMessage = "Favor inserir Preco.")]
        public float Preco { get; set; }
        [Required(ErrorMessage = "Favor inserir Data de Publicação.")]
        public DateTime? DataPublicacao { get; set; }
        public AutorDto Autor { get; set; }
    }
}