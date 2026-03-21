using projeto_esig.Data;
using System;

namespace projeto_esig
{
    public partial class GerenciarSalarios : System.Web.UI.Page
    {
        private SalarioRepository _repository = new SalarioRepository();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregarGrid();
            }
        }

        protected async void btnCalcular_Click(object sender, EventArgs e)
        {
            try
            {
                await _repository.CalcularSalariosAsync();
                CarregarGrid();
                MostrarMensagem("Salários calculados com sucesso!", false);
            }
            catch (Exception ex)
            {
                MostrarMensagem("Erro ao calcular: " + ex.Message, true);
            }
        }

        private void CarregarGrid()
        {
            gridSalarios.DataSource = _repository.ObterSalarios();
            gridSalarios.DataBind();
        }


        protected void btnPesquisar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtPesquisa.Text))
                {
                    CarregarGrid();
                    return;
                }

                gridSalarios.DataSource = _repository.Buscar(txtPesquisa.Text.Trim());
                gridSalarios.DataBind();
                lblMensagem.Text = "";

            }
            catch (Exception ex)
            {
                MostrarMensagem("Erro na pesquisa: " + ex.Message, true);
            }
        }

        protected void btnLimparPesquisa_Click(object sender, EventArgs e)
        {
            txtPesquisa.Text = "";
            CarregarGrid();
            lblMensagem.Text = "";
        }
        private void MostrarMensagem(string mensagem, bool isErro)
        {
            lblMensagem.Text = mensagem;
            lblMensagem.ForeColor = isErro ? System.Drawing.Color.Red : System.Drawing.Color.Green;
        }
    }
}