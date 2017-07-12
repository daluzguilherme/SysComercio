using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace CamadaDados
{
    class DCategoria
    {
        private int _Idcategoria;
        private string _Nome;
        private string _Descricao;
        private string _TextoBuscar;

        public int Idcategoria { get => _Idcategoria; set => _Idcategoria = value; }
        public string Nome { get => _Nome; set => _Nome = value; }
        public string Descricao { get => _Descricao; set => _Descricao = value; }
        public string TextoBuscar { get => _TextoBuscar; set => _TextoBuscar = value; }

        //Construtor vazio
        public DCategoria()
        {
        }

        //Construtor com param
        public DCategoria(int idcategoria, string nome, string descricao, string textobuscar)
        {
            this.Idcategoria = idcategoria;
            this.Nome = nome;
            this.Descricao = descricao;
            this.TextoBuscar = textobuscar;
        }

        //Metodo Inserir
        public string Inserir(DCategoria Categoria)
        {
            string resp = "";
            SqlConnection sqlCon = new SqlConnection();
            try
            {
                sqlCon.ConnectionString = Conexao.Cn;
                sqlCon.Open();

                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.Connection = sqlCon;
                sqlCmd.CommandText = "spinserir_categoria";
                sqlCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter ParIdcategoria = new SqlParameter();
                ParIdcategoria.ParameterName = "@idcategoria";
                ParIdcategoria.SqlDbType = SqlDbType.Int;
                ParIdcategoria.Direction = ParameterDirection.Output;
                sqlCmd.Parameters.Add(ParIdcategoria);

                SqlParameter ParNome = new SqlParameter();
                ParNome.ParameterName = "@nome";
                ParNome.SqlDbType = SqlDbType.VarChar;
                ParNome.Size = 50;
                ParNome.Value = Categoria.Nome;
                sqlCmd.Parameters.Add(ParNome);

                SqlParameter ParDescriao = new SqlParameter();
                ParDescriao.ParameterName = "@nome";
                ParDescriao.SqlDbType = SqlDbType.VarChar;
                ParDescriao.Size = 100;
                ParDescriao.Value = Categoria.Descricao;
                sqlCmd.Parameters.Add(ParDescriao);

                //Executa o comando
                resp = sqlCmd.ExecuteNonQuery()==1? "Ok" : "Registro não foi inserido";
            }
            catch (Exception ex)
            {
                resp = ex.Message;
            }
            finally
            {
                if(sqlCon.State == ConnectionState.Open)
                {
                    sqlCon.Close();
                }
            }
        }

        //Metodo Editar
        public string Editar(DCategoria Categoria)
        {
            string resp = "";
            SqlConnection sqlCon = new SqlConnection();
            try
            {
                sqlCon.ConnectionString = Conexao.Cn;
                sqlCon.Open();

                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.Connection = sqlCon;
                sqlCmd.CommandText = "speditar_categoria";
                sqlCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter ParIdcategoria = new SqlParameter();
                ParIdcategoria.ParameterName = "@idcategoria";
                ParIdcategoria.SqlDbType = SqlDbType.Int;
                ParIdcategoria.Value = Categoria.Idcategoria;
                sqlCmd.Parameters.Add(ParIdcategoria);

                SqlParameter ParNome = new SqlParameter();
                ParNome.ParameterName = "@nome";
                ParNome.SqlDbType = SqlDbType.VarChar;
                ParNome.Size = 50;
                ParNome.Value = Categoria.Nome;
                sqlCmd.Parameters.Add(ParNome);

                SqlParameter ParDescriao = new SqlParameter();
                ParDescriao.ParameterName = "@nome";
                ParDescriao.SqlDbType = SqlDbType.VarChar;
                ParDescriao.Size = 100;
                ParDescriao.Value = Categoria.Descricao;
                sqlCmd.Parameters.Add(ParDescriao);

                //Executa o comando
                resp = sqlCmd.ExecuteNonQuery() == 1 ? "Ok" : "A edição nao foi feita";
            }
            catch (Exception ex)
            {
                resp = ex.Message;
            }
            finally
            {
                if (sqlCon.State == ConnectionState.Open)
                {
                    sqlCon.Close();
                }
            }
        }

        //Metodo Excluir
        public string Excluir(DCategoria Categoria)
        {
            string resp = "";
            SqlConnection sqlCon = new SqlConnection();
            try
            {
                sqlCon.ConnectionString = Conexao.Cn;
                sqlCon.Open();

                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.Connection = sqlCon;
                sqlCmd.CommandText = "spdeletar_categoria";
                sqlCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter ParIdcategoria = new SqlParameter();
                ParIdcategoria.ParameterName = "@idcategoria";
                ParIdcategoria.SqlDbType = SqlDbType.Int;
                ParIdcategoria.Value = Categoria.Idcategoria;
                sqlCmd.Parameters.Add(ParIdcategoria);
                
                //Executa o comando
                resp = sqlCmd.ExecuteNonQuery() == 1 ? "Ok" : "A exclusão nao foi feita";
            }
            catch (Exception ex)
            {
                resp = ex.Message;
            }
            finally
            {
                if (sqlCon.State == ConnectionState.Open)
                {
                    sqlCon.Close();
                }
            }
        }

        //Metodo Mostrar
        public DataTable Mostrar(DCategoria Categoria)
        {
            DataTable DtResultado = new DataTable("categorias");

            SqlConnection sqlCon = new SqlConnection();
            try
            {
                sqlCon.ConnectionString = Conexao.Cn;
                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.Connection = sqlCon;
                sqlCmd.CommandText = "spmostrar_categoria";
                sqlCmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter sqlDat = new SqlDataAdapter(sqlCmd);
                sqlDat.Fill(DtResultado);
            }
            catch (Exception ex)
            {
                DtResultado = null;
            }
            finally
            {
                if (sqlCon.State == ConnectionState.Open)
                {
                    sqlCon.Close();
                }
            }
        }

        //Metodo Buscar Nome
        public DataTable BuscarNome(DCategoria Categoria)
        {
            DataTable DtResultado = new DataTable("categorias");

            SqlConnection sqlCon = new SqlConnection();
            try
            {
                sqlCon.ConnectionString = Conexao.Cn;
                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.Connection = sqlCon;
                sqlCmd.CommandText = "spbuscar_nome";
                sqlCmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter sqlDat = new SqlDataAdapter(sqlCmd);
                sqlDat.Fill(DtResultado);

                SqlParameter ParTextoBuscar = new SqlParameter();
                ParTextoBuscar.ParameterName = "@textobuscar";
                ParTextoBuscar.SqlDbType = SqlDbType.VarChar;
                ParTextoBuscar.Size = 50;
                ParTextoBuscar.Value = Categoria.TextoBuscar;
                sqlCmd.Parameters.Add(ParTextoBuscar);
            }
            catch (Exception ex)
            {
                DtResultado = null;
            }
            finally
            {
                if (sqlCon.State == ConnectionState.Open)
                {
                    sqlCon.Close();
                }
            }
        }

    }
}