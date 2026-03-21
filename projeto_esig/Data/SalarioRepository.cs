using projeto_esig.Models;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace projeto_esig.Data
{
    public class SalarioRepository
    {
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["EsigConexao"].ConnectionString;

        public async Task CalcularSalariosAsync()
        {
            using (SqlConnection conexao = new SqlConnection(_connectionString))
            {
                using (SqlCommand comando = new SqlCommand("sp_CalcularSalarios", conexao))
                {
                    comando.CommandType = CommandType.StoredProcedure;

                    try
                    {
                        await conexao.OpenAsync();
                        await comando.ExecuteNonQueryAsync();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Erro ao calcular salários: " + ex.Message);
                    }
                }
            }
        }

        public DataTable ObterSalarios()
        {
            using (SqlConnection conexao = new SqlConnection(_connectionString))
            {
                using (SqlCommand comando = new SqlCommand("SELECT * FROM pessoa_salario", conexao))
                {
                    DataTable tabela = new DataTable();
                    try
                    {
                        conexao.Open();
                        using (SqlDataAdapter adapter = new SqlDataAdapter(comando))
                        {
                            adapter.Fill(tabela);
                        }
                        return tabela;
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Erro ao buscar salários: " + ex.Message);
                    }
                }
            }
        }

        public DataTable Buscar(string termo)
        {
            using (SqlConnection conexao = new SqlConnection(_connectionString))
            {
                bool isNumero = int.TryParse(termo, out int idBusca);

                string query = @"SELECT * 
                                 FROM pessoa_salario 
                                 WHERE " + (isNumero ? "pessoa_id = @termo" : "pessoa_nome LIKE '%' + @termo + '%'");

                using (SqlCommand comando = new SqlCommand(query, conexao))
                {
                    if (isNumero)
                        comando.Parameters.AddWithValue("@termo", idBusca);
                    else
                        comando.Parameters.AddWithValue("@termo", termo);

                    using (SqlDataAdapter adapter = new SqlDataAdapter(comando))
                    {
                        DataTable tabela = new DataTable();
                        adapter.Fill(tabela);
                        return tabela;
                    }
                }
            }
        }

        public Pessoa ObterPorId(int id)
        {
            using (SqlConnection conexao = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM pessoa WHERE id = @id";
                using (SqlCommand comando = new SqlCommand(query, conexao))
                {
                    comando.Parameters.AddWithValue("@id", id);
                    conexao.Open();

                    using (SqlDataReader reader = comando.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Pessoa
                            {
                                Id = Convert.ToInt32(reader["id"]),
                                Nome = reader["nome"].ToString(),
                                Cidade = reader["cidade"] != DBNull.Value ? reader["cidade"].ToString() : "",
                                Email = reader["email"] != DBNull.Value ? reader["email"].ToString() : "",
                                Cep = reader["cep"] != DBNull.Value ? reader["cep"].ToString() : "",
                                Endereco = reader["endereco"] != DBNull.Value ? reader["endereco"].ToString() : "",
                                Pais = reader["pais"] != DBNull.Value ? reader["pais"].ToString() : "",
                                Usuario = reader["usuario"] != DBNull.Value ? reader["usuario"].ToString() : "",
                                Telefone = reader["telefone"] != DBNull.Value ? reader["telefone"].ToString() : "",
                                DataNascimento = Convert.ToDateTime(reader["data_nascimento"]),
                                CargoId = Convert.ToInt32(reader["cargo_id"])
                            };
                        }
                    }
                }
            }
            return null;
        }
    }
}