using projeto_esig.Data;
using projeto_esig.Models;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace projeto_esig
{
    public partial class GerenciarPessoas : System.Web.UI.Page
    {
        private PessoaRepository _repository = new PessoaRepository();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregarGrid();
            }
        }

        private void CarregarGrid()
        {
            gridPessoas.DataSource = _repository.Listar();
            gridPessoas.DataBind();
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Validação prévia de conversão para evitar quebra do .Parse()
                if (!DateTime.TryParse(txtDataNascimento.Text, out DateTime dataNasc))
                    throw new Exception("Data de nascimento inválida.");

                if (!int.TryParse(txtCargoId.Text, out int cargoId))
                    throw new Exception("O ID do Cargo deve ser um número válido.");

                Pessoa pessoa = new Pessoa
                {
                    Nome = txtNome.Text,
                    Cidade = txtCidade.Text,
                    Email = txtEmail.Text,
                    Cep = txtCep.Text,
                    Endereco = txtEndereco.Text,
                    Pais = txtPais.Text,
                    Usuario = txtUsuario.Text,
                    Telefone = txtTelefone.Text,
                    DataNascimento = dataNasc,
                    CargoId = cargoId
                };

                if (string.IsNullOrEmpty(hfPessoaId.Value))
                {
                    _repository.Inserir(pessoa);
                    MostrarMensagem("Pessoa cadastrada com sucesso!", false);
                }
                else
                {
                    pessoa.Id = int.Parse(hfPessoaId.Value);
                    _repository.Atualizar(pessoa);
                    MostrarMensagem("Registro atualizado com sucesso!", false);
                }

                LimparFormulario();
                CarregarGrid();
            }
            catch (System.Data.SqlClient.SqlException sqlEx)
            {
                // Captura erros específicos do banco de dados (como o FK do cargo_id)
                if (sqlEx.Number == 547)
                    MostrarMensagem("Erro: O ID do Cargo informado não existe cadastrado na tabela de Cargos.", true);
                else if (sqlEx.Number == 8152)
                    MostrarMensagem("Erro: O texto digitado é maior do que o limite permitido pelo banco.", true);
                else
                    MostrarMensagem("Erro no banco de dados: " + sqlEx.Message, true);
            }
            catch (Exception ex)
            {
                // Captura qualquer outro erro genérico
                MostrarMensagem("Erro ao salvar: " + ex.Message, true);
            }
        }

        // Lógica do botão de busca
        protected void btnPesquisar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtPesquisa.Text))
                {
                    CarregarGrid(); // Se buscar vazio, traz tudo
                    return;
                }

                gridPessoas.DataSource = _repository.Buscar(txtPesquisa.Text.Trim());
                gridPessoas.DataBind();
                lblMensagem.Text = ""; // Limpa mensagens anteriores
            }
            catch (Exception ex)
            {
                MostrarMensagem("Erro na pesquisa: " + ex.Message, true);
            }
        }

        // Lógica do botão de limpar pesquisa
        protected void btnLimparPesquisa_Click(object sender, EventArgs e)
        {
            txtPesquisa.Text = "";
            CarregarGrid();
            lblMensagem.Text = "";
        }

        // Método auxiliar para colorir o feedback do usuário
        private void MostrarMensagem(string mensagem, bool isErro)
        {
            lblMensagem.Text = mensagem;
            lblMensagem.ForeColor = isErro ? System.Drawing.Color.Red : System.Drawing.Color.Green;
        }

        protected void btnLimpar_Click(object sender, EventArgs e)
        {
            LimparFormulario();
        }

        // Intercepta os cliques nos botões "Editar" e "Excluir" dentro da tabela
        protected void gridPessoas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int idSelecionado = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "Excluir")
            {
                _repository.Excluir(idSelecionado);
                CarregarGrid();
            }
            else if (e.CommandName == "Editar")
            {
                // Busca os dados reais e completos no banco
                Pessoa pessoa = _repository.ObterPorId(idSelecionado);

                if (pessoa != null)
                {
                    hfPessoaId.Value = pessoa.Id.ToString();
                    txtNome.Text = pessoa.Nome;
                    txtCidade.Text = pessoa.Cidade;
                    txtEmail.Text = pessoa.Email;
                    txtCep.Text = pessoa.Cep;
                    txtEndereco.Text = pessoa.Endereco;
                    txtPais.Text = pessoa.Pais;
                    txtUsuario.Text = pessoa.Usuario;
                    txtTelefone.Text = pessoa.Telefone;
                    txtCargoId.Text = pessoa.CargoId.ToString();
                    
                    // Detalhe técnico vital: campos TextMode="Date" no HTML5 exigem o formato yyyy-MM-dd
                    txtDataNascimento.Text = pessoa.DataNascimento.ToString("yyyy-MM-dd"); 
                    
                    btnSalvar.Text = "Atualizar Registro"; 
                    lblMensagem.Text = ""; // Limpa qualquer mensagem de erro anterior
                }
            }
        }

        private void LimparFormulario()
        {
            hfPessoaId.Value = "";
            txtNome.Text = "";
            txtCidade.Text = "";
            txtEmail.Text = "";
            txtCep.Text = "";
            txtEndereco.Text = "";
            txtPais.Text = "";
            txtUsuario.Text = "";
            txtTelefone.Text = "";
            txtDataNascimento.Text = "";
            txtCargoId.Text = "";
            btnSalvar.Text = "Salvar Registro";
        }
    }
}
