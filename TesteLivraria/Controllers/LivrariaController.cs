﻿using TesteLivraria.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TesteLivraria.ViewsModels;

namespace TesteLivraria.Controllers
{
    public class LivrariaController : Controller
    {
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult LivrosCadastro()
        {
            LivroAutorViewModel livroAutores = new LivroAutorViewModel();
            livroAutores.Livro = new Livro();
            livroAutores.Autores = new Autor().Listar();
            ViewBag.acao = "Cadastrar Livro";

            return View("Livro", livroAutores);
        }



        public ActionResult LivrosConsulta()
        {
           return View(new Livro().Listar());
        }

        public ActionResult LivrosConsultaAPI()
        {
            return View(new Livro().Listar());
        }

        public ActionResult LivrosAtualizacao(int id)
        {

            Livro livroTemp = new Livro();
            livroTemp.Id = id;
            
            
            var info = livroTemp.BuscarLivro("Id");

            if (info == null)
            
                return HttpNotFound();
            else
            {
                LivroAutorViewModel livroAutores = new LivroAutorViewModel();
                livroAutores.Autores = new Autor().Listar();
                livroAutores.Livro = info;
                ViewBag.acao = "Atualizar Livro";
                return View("Livro", livroAutores);
            }
               
        }


        public ActionResult LivrosExcluir(int id)
        {

            Livro livroTemp = new Livro();
            livroTemp.Id = id;


            int resultado = livroTemp.Excluir();

            return RedirectToAction("LivrosConsulta", "Livraria");

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(LivroAutorViewModel livroAutor)
        {

            if (!ModelState.IsValid)
            {
                livroAutor.Autores = new Autor().Listar();
                ViewBag.acao = "Cadastrar Livro";
                return View("Livro", livroAutor);

            }

            if (livroAutor.Livro.Id == 0)
            {
                if (livroAutor.Livro.Cadastrar() > 0)
                {
                    livroAutor.Livro.Caminho = Server.MapPath("~/Uploads");
                    return RedirectToAction("LivrosConsultaApi", "Livraria");
                }
            }
            else
            {
                try { 
                    if (livroAutor.Livro.Capa.ContentLength > 0)
                    livroAutor.Livro.Caminho = Server.MapPath("~/Uploads");
                 }catch(Exception e)
                {

                }
                if (livroAutor.Livro.Atualizar() > 0)
                {

                    return RedirectToAction("LivrosConsultaApi", "Livraria");
                }
            }

            return HttpNotFound();

        }
    }
}