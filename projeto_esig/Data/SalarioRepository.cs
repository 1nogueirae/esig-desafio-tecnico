using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace projeto_esig.Data
{
    public class SalarioRepository
    {
        // Puxa a string de conexão que guardamos no Web.config
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["EsigConexao"].ConnectionString;

        //public void CalcularSalarios()
        //{
        //    // O bloco 'using' garante que a conexão será fechada e destruída da memória após o uso
        //    using (SqlConnection conexao = new SqlConnection(_connectionString))
        //    {
        //        // Prepara o comando para executar a Stored Procedure
        //        using (SqlCommand comando = new SqlCommand("sp_CalcularSalarios", conexao))
        //        {
        //            // Avisa ao banco que não é uma query em texto livre, e sim uma procedure
        //            comando.CommandType = CommandType.StoredProcedure;

        //            try
        //            {
        //                conexao.Open();
        //                comando.ExecuteNonQuery(); // Executa a procedure no banco
        //            }
        //            catch (Exception ex)
        //            {
        //                // Se algo der errado (ex: banco desligado), o erro será capturado aqui
        //                throw new Exception("Erro ao calcular salários: " + ex.Message);
        //            }
        //        }
        //    }
        //}

        public async Task CalcularSalariosAsync()
        {
            using (SqlConnection conexao = new SqlConnection(_connectionString))
            {
                using (SqlCommand comando = new SqlCommand("sp_CalcularSalarios", conexao))
                {
                    comando.CommandType = CommandType.StoredProcedure;

                    try
                    {
                        // Note o "await" e os métodos terminados em "Async"
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
                            // O SqlDataAdapter executa a query e preenche a tabela automaticamente
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
    }
}