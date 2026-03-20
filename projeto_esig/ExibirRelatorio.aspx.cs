using CrystalDecisions.CrystalReports.Engine;
using projeto_esig.Data;
using System;

namespace projeto_esig
{
    public partial class ExibirRelatorio : System.Web.UI.Page
    {
        private SalarioRepository _repository = new SalarioRepository();

        protected void Page_Init(object sender, EventArgs e)
        {
            GerarRelatorio();
        }

        private void GerarRelatorio()
        {
            try
            {
                // 1. Cria a instância do motor do relatório
                ReportDocument relatorio = new ReportDocument();

                // 2. Localiza o arquivo físico .rpt no servidor
                string caminhoRelatorio = Server.MapPath("~/Reports/RelatorioSalarios.rpt");
                relatorio.Load(caminhoRelatorio);

                // 3. Busca os dados reais do banco usando o método que já tínhamos feito
                var dados = _repository.ObterSalarios();

                // 4. Injeta os dados no relatório
                relatorio.SetDataSource(dados);

                // 5. Acopla o relatório gerado no visualizador da tela HTML
                CrystalReportViewer1.ReportSource = relatorio;
                CrystalReportViewer1.DataBind();
            }
            catch (Exception ex)
            {
                // Se o Crystal falhar (e ele gosta de falhar por permissão de pasta), capturamos aqui
                Response.Write("Erro ao carregar o relatório: " + ex.Message);
            }
        }

        // Previne vazamento de memória do servidor (o Crystal consome muita RAM)
        protected void Page_Unload(object sender, EventArgs e)
        {
            if (CrystalReportViewer1 != null)
            {
                CrystalReportViewer1.Dispose();
            }
        }
    }
}