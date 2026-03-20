using projeto_esig.Data;
using System;

namespace projeto_esig
{
    public partial class GerenciarSalarios : System.Web.UI.Page
    {
        // Instancia nosso repositório para podermos usar os métodos
        private SalarioRepository _repository = new SalarioRepository();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregarGrid();
            }
        }

        // Este é o evento acionado quando o usuário clica no botão da tela
        protected async void btnCalcular_Click(object sender, EventArgs e)
        {
            // 1. Vai no banco e roda a Stored Procedure para calcular
            await _repository.CalcularSalariosAsync();

            // 2. Atualiza a tela com os dados novos
            CarregarGrid();
        }

        // Método auxiliar para buscar os dados e "colar" na grade visual
        private void CarregarGrid()
        {
            gridSalarios.DataSource = _repository.ObterSalarios();
            gridSalarios.DataBind(); // Comando obrigatório para injetar os dados no HTML
        }
    }
}