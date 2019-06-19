using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TesteLivraria.Dto
{
    public class AutorDto
    {

        [Required(ErrorMessage = "Favor selecionar Autor")]
        public Int32 Id { get; set; }
        public String Nome { get; set; }

    }
}