using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Web;
using TesteLivraria.DB;
using TesteLivraria.Util.Validators;

namespace TesteLivraria.Models
{
    public class Livro
    {
       
        public Int32 Id { get; set; }
        [Required(ErrorMessage = "Favor inserir ISBN.")]
        [StringLength(13)]
        [DisplayFormat(DataFormatString = "{}", ApplyFormatInEditMode = true)]
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
        [Required(ErrorMessage = "Favor selecionar autor.")]
        public Autor Autor { get; set; }
        [Required(ErrorMessage = "Favor selecionar arquivo.")]
        [AllowExtensions(Extensions = "png,jpg", ErrorMessage = "Favior selecionar imagem. arquivos permitidos png | .jpg")]
        public HttpPostedFileBase Capa { get; set; }
        public String Caminho { get; set; }


        public int Cadastrar()
        {
            LivroDB livrodb = new LivroDB();
            if (livrodb.BuscarPorISBN(this.ISBN).Id != 0)
                return 1001;
            else
            {
                if (new LivroDB(this).Cadastrar() > 0)
                {
                    try
                    {
                        if (Capa.ContentLength > 0)
                        {
                            var caminho = Path.Combine(this.Caminho, ISBN +"."+ Capa.ContentType.Replace("image/",""));

                            Capa.SaveAs(caminho);

                        }
                    }
                    catch (Exception e)
                    {
                        return 1002;
                    }

                    return 1;
                }
                else
                    return 1003;
            }

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

        public int Excluir()
        {
            return new LivroDB(this).Excluir();
        }
    }
}