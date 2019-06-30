using System;
using System.Collections;
using System.Configuration;
using System.Data;
using MySql.Data.MySqlClient;

namespace TesteLivraria.DAO
{
    public class Conector
    {
        #region atributos
        private String strConexao = ConfigurationManager.ConnectionStrings["Livraria"].ToString();
        public String MensagemDeErro { get; set; }
        #endregion
        #region metodos
        public Int32 Executar(String strSQL)
        {
            using (MySqlConnection conexao = new MySqlConnection(strConexao))
            {
                conexao.Open(); //Abre conexao
                MySqlCommand comando = new MySqlCommand(strSQL, conexao);
                int rowsaffect = comando.ExecuteNonQuery();//executa comando
                conexao.Close(); //Fecha conexao
                if (comando.LastInsertedId == 0)
                    return rowsaffect;
                else
                    return Convert.ToInt32(comando.LastInsertedId);
            }
        }

        public Int32 ExecutarComParametros(String strSQL, ArrayList parametros)
        {
            using (MySqlConnection conexao = new MySqlConnection(strConexao))
            {
                conexao.Open(); //Abre conexao
                MySqlCommand comando = new MySqlCommand(strSQL, conexao);
                for (int i = 0; i < parametros.Count; i++)
                {
                    comando.Parameters.AddWithValue("?" + i, parametros[i]);
                }
                comando.Prepare();
                int rowsaffect = comando.ExecuteNonQuery();
                conexao.Close(); //Fecha conexao
                if (comando.LastInsertedId == 0)
                    return rowsaffect;
                else
                    return Convert.ToInt32(comando.LastInsertedId);
            }
        }

        public DataSet LeitorDeDados(String strSQL)
        {
            using (MySqlConnection conexao = new MySqlConnection(strConexao))
            {
                conexao.Open(); //Abre conexao
                MySqlDataAdapter adptador = new MySqlDataAdapter(strSQL, conexao);
                DataSet ds = new DataSet();
                adptador.Fill(ds);
                conexao.Close(); //Fecha conexao
                return ds;
            }
        }

        public DataSet LeitorDeDadosComParametros(String strSQL, ArrayList parametros)
        {
            using (MySqlConnection conexao = new MySqlConnection(strConexao))
            {
                conexao.Open(); //Abre conexao
                MySqlCommand comando = new MySqlCommand(strSQL, conexao);
                for (int i = 0; i < parametros.Count; i++)
                {
                    comando.Parameters.AddWithValue("?" + i, parametros[i]);
                }
                comando.Prepare();
                MySqlDataAdapter adptador = new MySqlDataAdapter(comando);
                DataSet ds = new DataSet();
                adptador.Fill(ds);
                conexao.Close(); //Fecha conexao
                return ds;
            }
        }
        #endregion
    }
}