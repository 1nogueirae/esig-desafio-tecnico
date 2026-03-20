<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GerenciarSalarios.aspx.cs" Inherits="projeto_esig.GerenciarSalarios" Async="true"%>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div style="background-color: #507CD1; overflow: hidden; margin-bottom: 20px; border-radius: 5px;">
            <a href="GerenciarSalarios.aspx" style="float: left; display: block; color: white; text-align: center; padding: 14px 16px; text-decoration: none; font-family: Arial, sans-serif; font-weight: bold;">📊 Gestão de Salários</a>
            <a href="GerenciarPessoas.aspx" style="float: left; display: block; color: white; text-align: center; padding: 14px 16px; text-decoration: none; font-family: Arial, sans-serif; font-weight: bold;">👥 Cadastro de Pessoas (CRUD)</a>
        </div>
        <div>
            <h2>Gestão de Salários</h2>
    
            <asp:Button ID="btnCalcular" runat="server" Text="Calcular / Recalcular Salários" OnClick="btnCalcular_Click" />
            <asp:Button ID="btnImprimir" runat="server" Text="Imprimir Relatório" OnClientClick="window.open('ExibirRelatorio.aspx', '_blank'); return false;" />
    
            <br /><br />
    
            <asp:GridView ID="gridSalarios" runat="server" AutoGenerateColumns="true" 
                          CellPadding="4" ForeColor="#333333" GridLines="None">
                <AlternatingRowStyle BackColor="White" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <RowStyle BackColor="#EFF3FB" />
            </asp:GridView>
        </div>
    </form>
</body>
</html>
