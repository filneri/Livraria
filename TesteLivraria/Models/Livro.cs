using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;
using TesteLivraria.DB;

namespace TesteLivraria.Models
{
    public class Livro
    {
       
        public Int32 Id { get; set; }
        [Required(ErrorMessage = "Favor inserir ISBN.")]
        [StringLength(13)]
        public String ISBN { get; set;}
        [Required(ErrorMessage = "Favor inserir Nome.")]
        [StringLength(150)]
        public String Nome { get; set; }
        [Required(ErrorMessage = "Favor inserir Preco.")]
        [Display(Name = "Preço")]
        public float Preco { get; set; }
        [Required(ErrorMessage = "Favor inserir Data de Publicação.")]
        [Display(Name = "Data de Publicação")]
        public DateTime? DataPublicacao { get; set; }
        public Autor Autor { get; set; }


        public int Cadastrar()
        {
            LivroDB livrodb = new LivroDB();
            if (livrodb.BuscarPorISBN(this.ISBN).Id != 0)
                return 1001;
            else
                return new LivroDB(this).Cadastrar();

        }

        public List<Livro> Listar()
        {
            return new LivroDB().Listar();
        }

        public Livro BuscarLivro(String comando)
        {
            switch (comando)
            {
                case ("Id"):
                    return new LivroDB().BuscarPorId(this.Id);
                case "ISBN":
                    return new LivroDB().BuscarPorISBN(this.ISBN);
                default:
                    return null;
                    

            }
        }

        public int Atualizar()
        {
                return new LivroDB(this).Atualizar();
        }
    }
}