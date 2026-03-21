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
                                 WHERE " + (isNumero ? "pessoa_id = @termo" : "pessoa_nome LIKE @termo");


                using (SqlCommand comando = new SqlCommand(query, conexao))
                {
                    if (isNumero)
                        comando.Parameters.AddWithValue("@termo", idBusca);
                    else
                        comando.Parameters.AddWithValue("@termo", "%" + termo + "%");

                    using (SqlDataAdapter adapter = new SqlDataAdapter(comando))
                    {
                        DataTable tabela = new DataTable();
                        adapter.Fill(tabela);
                        return tabela;
                    }
                }
            }
        }
    }
}