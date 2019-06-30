using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TesteLivraria.Models;

namespace TesteLivraria.Models.ViewsModels
{
    public class LivroAutorViewModel
    {
        #region atributos
        public Livro Livro { get; set; }
        public List<Autor> Autores { get; set; }
        #endregion
    }
}