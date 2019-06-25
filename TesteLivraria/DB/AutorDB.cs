using System;
using System.Collections;
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

        public AutorDB(Autor autor)
        {
            Autor = autor;
        }


        public AutorDB()
        {

        }

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

        public Autor BuscarPorId(int id)
        {
            this.Autor = new Autor();

            string sql = "select a.idautor,a.nome as autor from autor a where idautor ='" + id + "'";
            DataSet dados = LeitorDeDados(sql);
            foreach (DataRow row in dados.Tables[0].Rows)
            {

                this.Autor.Id = Convert.ToInt32(row["idAutor"].ToString());
                this.Autor.Nome = row["autor"].ToString();
            }

            return this.Autor;


        }

        public Autor BuscarPorNome(String nome)
        {
            this.Autor = new Autor();
            string sql = "select a.idautor,a.nome as autor from autor a where a.nome ='" + nome + "'";
            DataSet dados = LeitorDeDados(sql);
            foreach (DataRow row in dados.Tables[0].Rows)
            {

                this.Autor.Id = Convert.ToInt32(row["idAutor"].ToString());
                this.Autor.Nome = row["autor"].ToString();
            }

            return this.Autor;
        }

        public int Cadastrar()
        {
            String sql = "Insert into Autor(Nome) Values(?0)";
            ArrayList parametros = new ArrayList();
            parametros.Add(this.Autor.Nome);
            return ExecutarComParametros(sql, parametros);

        }

        internal int Atualizar()
        {
            String sql = "update Autor set Nome=?0 where idAutor=?1";
            ArrayList parametros = new ArrayList();
            parametros.Add(this.Autor.Nome);
            parametros.Add(this.Autor.Id);
            return ExecutarComParametros(sql, parametros);

        }

        public int Excluir()
        {
            String sql = "delete from Autor where idautor = " + this.Autor.Id;
            return Executar(sql);
        }

    }
}