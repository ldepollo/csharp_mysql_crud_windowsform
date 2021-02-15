using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Empregado
{
    private const string stringConexao = "SERVER=localhost; PORT=3306; DATABASE=mydb; UID=root; PWD=";
    public int Matricula { get; set; }
    public string CPF { get; set; }
    public string Nome { get; set; }
    public string Endereco { get; set; }

    public DataTable Pesquisar()
    {
        try
        {
            StringBuilder stringSql = new StringBuilder();
            stringSql.Append("select Matricula, CPF, Nome, Endereco ");
            stringSql.Append("from mydb.empregado ");
            stringSql.Append("where 1");

            if (Matricula != 0)
            {
                stringSql.Append(" and matricula=@matricula");
            }
            if (!CPF.Equals(""))
            {
                stringSql.Append(" and cpf=@cpf");
            }
            if (!Nome.Equals(""))
            {
                stringSql.Append(" and nome like @nome");
            }

            MySqlConnection conexao = new MySqlConnection(stringConexao);
            MySqlCommand comando = new MySqlCommand(stringSql.ToString(), conexao);

            comando.Parameters.AddWithValue("@matricula", Matricula);
            comando.Parameters.AddWithValue("@cpf", CPF);
            comando.Parameters.AddWithValue("@nome", "%" + Nome + "%");

            MySqlDataAdapter adaptador = new MySqlDataAdapter();
            adaptador.SelectCommand = comando;

            DataTable dados = new DataTable();
            adaptador.Fill(dados);
            return dados;
        }
        catch (Exception e)
        {
            throw e;
        }
    }

    public void Incluir()
    {
        try
        {
            MySqlConnection conexao = new MySqlConnection(stringConexao);
            MySqlCommand comando = new MySqlCommand();

            comando.Connection = conexao;
            comando.CommandText = "insert into mydb.empregado (CPF, Nome, Endereco) values (@CPF, @nome, @endereco)";

            conexao.Open();
            comando.Parameters.AddWithValue("@CPF", CPF);
            comando.Parameters.AddWithValue("@nome", Nome);
            comando.Parameters.AddWithValue("@endereco", Endereco);
            comando.Prepare();
            comando.ExecuteNonQuery();
            conexao.Close();
        }
        catch (Exception e)
        {
            throw e;
        }
    }

    public void Alterar()
    {
        try
        {
            MySqlConnection conexao = new MySqlConnection(stringConexao);
            MySqlCommand comando = new MySqlCommand();

            comando.Connection = conexao;
            comando.CommandText = "UPDATE mydb.empregado SET CPF=@CPF, Nome=@Nome, Endereco=@Endereco WHERE Matricula=@Matricula";

            conexao.Open();
            comando.Parameters.AddWithValue("@Matricula", Matricula);
            comando.Parameters.AddWithValue("@CPF", CPF);
            comando.Parameters.AddWithValue("@Nome", Nome);
            comando.Parameters.AddWithValue("@Endereco", Endereco);
            comando.Prepare();
            comando.ExecuteNonQuery();
            conexao.Close();
        }
        catch (Exception e)
        {
            throw e;
        }
    }

    public void Excluir()
    {
        try
        {
            MySqlConnection conexao = new MySqlConnection(stringConexao);
            MySqlCommand comando = new MySqlCommand();

            comando.Connection = conexao;
            comando.CommandText = "DELETE FROM mydb.empregado WHERE Matricula=@Matricula";

            conexao.Open();
            comando.Parameters.AddWithValue("@Matricula", Matricula);
            comando.Prepare();
            comando.ExecuteNonQuery();
            conexao.Close();
        } 
        catch (Exception e)
        {
            throw e;
        }
    }   
}