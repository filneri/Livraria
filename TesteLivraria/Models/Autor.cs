﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TesteLivraria.DAO;

namespace TesteLivraria.Models
{
    public class Autor : Conector
    {
        #region atributos
        [Required(ErrorMessage = "Favor selecionar Autor")]
        public Int32 Id { get; set; }
        public String Nome { get; set; }
        #endregion
        #region construtores
        public List<Autor> Listar()
        {
            

            return new AutorDB().Listar();
        }
        #endregion
        #region metodos
        public Autor BuscarAutor(String comando)
        {
            switch (comando)
            {
                case ("Id"):
                    return new AutorDB().BuscarPorId(this.Id);
                case "Nome":
                    return new AutorDB().BuscarPorNome(this.Nome);
                default:
                    return null;
            }
        }

        public int Atualizar()
        {
            return new AutorDB(this).Atualizar();
        }

        public int Cadastrar()
        {
            AutorDB autordb = new AutorDB();
            if (autordb.BuscarPorNome(this.Nome).Id != 0)
                return 1001;
            else
            {
                autordb.Autor = this;
                return autordb.Cadastrar();
            }

        }

        public int Excluir()
        {
            return new AutorDB(this).Excluir();
        }

        #endregion
    }
}