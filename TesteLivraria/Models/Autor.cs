using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;
using TesteLivraria.DB;

namespace TesteLivraria.Models
{
    public class Autor : Conector
    {
        [Required(ErrorMessage = "Favor selecionar Autor")]
        public Int32 Id { get; set; }
        public String Nome { get; set; }


        public List<Autor> Listar()
        {
            

            return new AutorDB().Listar();
        }

    }
}