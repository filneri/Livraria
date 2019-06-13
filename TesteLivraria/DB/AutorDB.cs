using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using TesteLivraria.Models;

namespace TesteLivraria.DB
{
    public class AutorDB : Conector
    {
        public Autor Autor { get; set; }


        public List<Autor> Listar()
        {
            String sql = "select * from autor";
            List<Autor> autores = new List<Autor>();
            DataSet dados = LeitorDeDados(sql);
            Autor autor;
            foreach (DataRow row in dados.Tables[0].Rows)
            {
                autor = new Autor();
                autor.Id = Convert.ToInt16(row["idAutor"].ToString());
                autor.Nome = row["Nome"].ToString();
                autores.Add(autor);
            }

            return autores;
        }

    }
}