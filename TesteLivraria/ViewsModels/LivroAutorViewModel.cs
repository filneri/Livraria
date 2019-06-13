using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TesteLivraria.Models;

namespace TesteLivraria.ViewsModels
{
    public class LivroAutorViewModel
    {
        public Livro Livro { get; set; }
        public List<Autor> Autores { get; set; }
    }
}