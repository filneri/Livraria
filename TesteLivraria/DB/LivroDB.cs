using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using TesteLivraria.Models;

namespace TesteLivraria.DB
{
    public class LivroDB : Conector
    {
        public Livro Livro { get; set; }

        public LivroDB()
        {
       
        }

        public LivroDB(Livro livro)
        {
            Livro = livro;
        }

        public int Cadastrar()
        {
            String sql = "Insert into Livro(ISBN,Nome,Preco,DataPublicacao,idautor,imagem) Values(?0,?1,?2,?3,?4,?5)";
            ArrayList parametros = new ArrayList();
            parametros.Add(this.Livro.ISBN);
            parametros.Add(this.Livro.Nome);
            parametros.Add(this.Livro.Preco);
            parametros.Add(this.Livro.DataPublicacao);
            parametros.Add(this.Livro.Autor.Id);
            parametros.Add(this.Livro.ISBN + "." + this.Livro.Capa.ContentType.Replace("image/", ""));
            return ExecutarComParametros(sql, parametros);

        }

        public Livro BuscarPorISBN(String ISBN)
        {
            this.Livro = new Livro();
            this.Livro.Autor = new Autor();
            string sql = "select l.idlivro,l.isbn,l.nome as livro,l.preco,l.datapublicacao,l.idautor,a.nome as autor from livro l inner join autor a on l.idautor = a.idautor where ISBN ='" + ISBN+"'";
            DataSet dados = LeitorDeDados(sql);
            foreach (DataRow row in dados.Tables[0].Rows)
            {

                this.Livro.Autor = new Autor();
                this.Livro.Id = Convert.ToInt32(row["idLivro"].ToString());
                this.Livro.ISBN = row["ISBN"].ToString();
                this.Livro.Nome = row["livro"].ToString();
                this.Livro.Preco = Convert.ToSingle(row["preco"].ToString());
                this.Livro.DataPublicacao = Convert.ToDateTime(row["DataPublicacao"].ToString());
                this.Livro.Autor.Id = Convert.ToInt32(row["idAutor"].ToString());
                this.Livro.Autor.Nome = row["autor"].ToString();
            }

            return this.Livro;
        }

        internal int Atualizar()
        {
            String sql = "update Livro set ISBN=?0,Nome=?1,Preco=?2,DataPublicacao=?3,idautor=?4 where idLivro=?5";
            ArrayList parametros = new ArrayList();
            parametros.Add(this.Livro.ISBN);
            parametros.Add(this.Livro.Nome);
            parametros.Add(this.Livro.Preco);
            parametros.Add(this.Livro.DataPublicacao);
            parametros.Add(this.Livro.Autor.Id);
            parametros.Add(this.Livro.Id);
            return ExecutarComParametros(sql, parametros);

        }

        public Livro BuscarPorId(int id)
        {
            this.Livro = new Livro();
            this.Livro.Autor = new Autor();
            string sql = "select l.idlivro,l.isbn,l.nome as livro,l.preco,l.datapublicacao,l.idautor,a.nome as autor from livro l inner join autor a on l.idautor = a.idautor where idlivro ='" + id + "'";
            DataSet dados = LeitorDeDados(sql);
            foreach (DataRow row in dados.Tables[0].Rows)
            {

                this.Livro.Autor = new Autor();
                this.Livro.Id = Convert.ToInt32(row["idLivro"].ToString());
                this.Livro.ISBN = row["ISBN"].ToString();
                this.Livro.Nome = row["livro"].ToString();
                this.Livro.Preco = Convert.ToSingle(row["preco"].ToString());
                this.Livro.DataPublicacao = Convert.ToDateTime(row["DataPublicacao"].ToString());
                this.Livro.Autor.Id = Convert.ToInt32(row["idAutor"].ToString());
                this.Livro.Autor.Nome = row["autor"].ToString();
            }

            return this.Livro;


        }

        public List<Livro> Listar()
        {
            String sql = "select l.idlivro,l.isbn,l.nome as livro,l.preco,l.datapublicacao,l.idautor,a.nome as autor,l.imagem from livro l inner join autor a on l.idautor = a.idautor";
            List<Livro> livros = new List<Livro>();
            DataSet dados = LeitorDeDados(sql);
            Livro livroTemp;

            foreach (DataRow row in dados.Tables[0].Rows)
            {
                livroTemp = new Livro();
                livroTemp.Autor = new Autor();
                livroTemp.Id = Convert.ToInt32(row["idLivro"].ToString());
                livroTemp.ISBN = row["ISBN"].ToString();
                livroTemp.Nome = row["livro"].ToString();
                livroTemp.Preco = Convert.ToSingle(row["preco"].ToString());
                livroTemp.DataPublicacao = Convert.ToDateTime(row["DataPublicacao"].ToString());
                livroTemp.Autor.Id = Convert.ToInt32(row["idAutor"].ToString());
                livroTemp.Autor.Nome = row["autor"].ToString();
                livroTemp.Caminho = row["imagem"].ToString();
                livros.Add(livroTemp);
            }

            return livros;
        }

        public int Excluir()
        {
            String sql = "delete from Livro where idlivro = "+this.Livro.Id;
            return Executar(sql);
        }
    }
}