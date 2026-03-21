using projeto_esig.Models;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace projeto_esig.Data
{
    public class PessoaRepository
    {
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["EsigConexao"].ConnectionString;

        // CREATE: Insere uma nova pessoa no banco
        public void Inserir(Pessoa pessoa)
        {
            using (SqlConnection conexao = new SqlConnection(_connectionString))
            {
                string query = @"INSERT INTO pessoa (nome, cidade, email, cep, endereco, pais, usuario, telefone, data_nascimento, cargo_id) 
                                 VALUES (@nome, @cidade, @email, @cep, @endereco, @pais, @usuario, @telefone, @data_nascimento, @cargo_id)";

                using (SqlCommand comando = new SqlCommand(query, conexao))
                {
                    AdicionarParametros(comando, pessoa);
                    conexao.Open();
                    comando.ExecuteNonQuery();
                }
            }
        }

        // READ: Retorna todas as pessoas.
        public DataTable Listar()
        {
            using (SqlConnection conexao = new SqlConnection(_connectionString))
            {
                string query = @"SELECT p.*, c.nome AS cargo_nome 
                                 FROM pessoa p 
                                 INNER JOIN cargo c ON p.cargo_id = c.id";

                using (SqlCommand comando = new SqlCommand(query, conexao))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(comando))
                    {
                        DataTable tabela = new DataTable();
                        adapter.Fill(tabela);
                        return tabela;
                    }
                }
            }
        }

        // UPDATE: Atualiza os dados de uma pessoa existente
        public void Atualizar(Pessoa pessoa)
        {
            using (SqlConnection conexao = new SqlConnection(_connectionString))
            {
                string query = @"UPDATE pessoa SET 
                                    nome = @nome, cidade = @cidade, email = @email, cep = @cep, 
                                    endereco = @endereco, pais = @pais, usuario = @usuario, 
                                    telefone = @telefone, data_nascimento = @data_nascimento, 
                                    cargo_id = @cargo_id 
                                 WHERE id = @id";

                using (SqlCommand comando = new SqlCommand(query, conexao))
                {
                    AdicionarParametros(comando, pessoa);
                    conexao.Open();
                    comando.ExecuteNonQuery();
                }
            }
        }

        // DELETE: Remove uma pessoa pelo ID
        public void Excluir(int id)
        {
            using (SqlConnection conexao = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM pessoa WHERE id = @id";
                using (SqlCommand comando = new SqlCommand(query, conexao))
                {
                    comando.Parameters.AddWithValue("@id", id);
                    conexao.Open();
                    comando.ExecuteNonQuery();
                }
            }
        }

        // Método auxiliar para evitar repetição e tratar valores nulos
        private void AdicionarParametros(SqlCommand comando, Pessoa pessoa)
        {
            comando.Parameters.AddWithValue("@id", pessoa.Id);
            comando.Parameters.AddWithValue("@nome", pessoa.Nome);

            comando.Parameters.AddWithValue("@cidade", string.IsNullOrEmpty(pessoa.Cidade) ? (object)DBNull.Value : pessoa.Cidade);
            comando.Parameters.AddWithValue("@email", string.IsNullOrEmpty(pessoa.Email) ? (object)DBNull.Value : pessoa.Email);
            comando.Parameters.AddWithValue("@cep", string.IsNullOrEmpty(pessoa.Cep) ? (object)DBNull.Value : pessoa.Cep);
            comando.Parameters.AddWithValue("@endereco", string.IsNullOrEmpty(pessoa.Endereco) ? (object)DBNull.Value : pessoa.Endereco);
            comando.Parameters.AddWithValue("@pais", string.IsNullOrEmpty(pessoa.Pais) ? (object)DBNull.Value : pessoa.Pais);
            comando.Parameters.AddWithValue("@usuario", string.IsNullOrEmpty(pessoa.Usuario) ? (object)DBNull.Value : pessoa.Usuario);
            comando.Parameters.AddWithValue("@telefone", string.IsNullOrEmpty(pessoa.Telefone) ? (object)DBNull.Value : pessoa.Telefone);

            comando.Parameters.AddWithValue("@data_nascimento", pessoa.DataNascimento);
            comando.Parameters.AddWithValue("@cargo_id", pessoa.CargoId);
        }

        // SEARCH: Busca inteligente por ID ou Nome
        public DataTable Buscar(string termo)
        {
            using (SqlConnection conexao = new SqlConnection(_connectionString))
            {
                bool isNumero = int.TryParse(termo, out int idBusca);

                string query = @"SELECT p.*, c.nome AS cargo_nome 
                                 FROM pessoa p 
                                 INNER JOIN cargo c ON p.cargo_id = c.id
                                 WHERE " + (isNumero ? "p.id = @termo" : "p.nome LIKE @termo");


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
