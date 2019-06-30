using TesteLivraria.Models;
using System;
using System.Web.Mvc;
using TesteLivraria.Models.ViewsModels;

namespace TesteLivraria.Controllers
{
    public class LivrariaController : Controller
    {
        public ActionResult Index()
        {

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
            int execucao;
            if (livroAutor.Livro.Id == 0)//verifica se é cadasrto ou update
            {
                livroAutor.Livro.Caminho = Server.MapPath("~/Uploads");
                execucao = livroAutor.Livro.Cadastrar();
            }
            else
            {
                try
                {
                    if (livroAutor.Livro.Capa.ContentLength > 0)
                        livroAutor.Livro.Caminho = Server.MapPath("~/Uploads");
                }
                catch (Exception e)
                {

                }
                execucao = livroAutor.Livro.Atualizar();
            }
                
            
                if (execucao == 1004)
                {
                    livroAutor.Autores = new Autor().Listar();
                    ViewBag.errormsg = "ISBN já cadastrado!";
                    return View("Livro", livroAutor);
                }
                if(execucao == 1001)
                {
                ModelState.Clear();
                livroAutor.Livro = new Livro();
                    livroAutor.Autores =new Autor().Listar();
                ViewBag.resultado = "Livro atualizado com sucesso!";
        
                    return View("Livro", livroAutor);
                }
                else
                {
                    livroAutor.Autores = new Autor().Listar();
                    ViewBag.errormsg = "Erro ao cadastrar!";
                    return View("Livro", livroAutor);
                }
         
            
        }
    }
}