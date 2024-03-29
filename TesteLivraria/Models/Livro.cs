﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Web;
using TesteLivraria.DAO;
using TesteLivraria.Util.Validators;

namespace TesteLivraria.Models
{
    public class Livro
    {
        #region constantes
        private static readonly int erroAoCriarArquivo = 1002;
        private static readonly int modificacaoNaoRealizadaNaBase = 1003;
        private static readonly int sucesso = 1001;
        private static readonly int livroJaCadastrado = 1004;
        #endregion        
        #region atributos
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
        [AllowExtensions(Extensions = "PNG,png,jpg", ErrorMessage = "Favior selecionar imagem. arquivos permitidos png | .jpg")]
        public HttpPostedFileBase Capa { get; set; }
        public String Caminho { get; set; }
        #endregion
        #region metodos
        public int Cadastrar()
        {
            LivroDB livrodb = new LivroDB();
            if (livrodb.BuscarPorISBN(this.ISBN).Id != 0)
                return livroJaCadastrado;
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
                        return erroAoCriarArquivo;
                    }

                    return sucesso;
                }
                else
                    return modificacaoNaoRealizadaNaBase;
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
                    Livro livro = new LivroDB().BuscarPorId(this.Id);
                    return livro;
                case "ISBN":
                    return new LivroDB().BuscarPorISBN(this.ISBN);
                default:
                    return null;
                    

            }
        }

        public int Atualizar()
        {
            LivroDB livroDB = new LivroDB();
            Livro  livro = livroDB.BuscarPorISBN(this.ISBN);
            if (livro.Id !=0 && livro.Id != this.Id)
                return livroJaCadastrado;
            else

            if (new LivroDB(this).Atualizar() > 0)
                {
                    try
                    {
                        if (!Caminho.Contains(ISBN))
                        {
                        Capa.SaveAs(Path.Combine(this.Caminho, ISBN + "." + Capa.ContentType.Replace("image/", "")));


                        }
                    }
                    catch (Exception e)
                    {
                        return erroAoCriarArquivo;
                    }

                    return sucesso;
            }
            return modificacaoNaoRealizadaNaBase;
        }

        public int Excluir()
        {
            return new LivroDB(this).Excluir();
        }

        #endregion
    }
}