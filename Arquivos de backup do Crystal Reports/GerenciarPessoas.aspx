<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GerenciarPessoas.aspx.cs" Inherits="projeto_esig.GerenciarPessoas" %>

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
            <div>
                <h2>Gestão de Pessoas (CRUD)</h2>

                <asp:Label ID="lblMensagem" runat="server" Font-Bold="true"></asp:Label>
                <br /><br />

                <div style="margin-bottom: 20px; padding: 15px; background-color: #f1f1f1; border: 1px solid #ccc; max-width: 600px;">
                    <label>Pesquisar (Digite o ID ou parte do Nome):</label><br />
                    <asp:TextBox ID="txtPesquisa" runat="server" Width="300px"></asp:TextBox>
                    <asp:Button ID="btnPesquisar" runat="server" Text="Buscar" OnClick="btnPesquisar_Click" formnovalidate="true" />
                    <asp:Button ID="btnLimparPesquisa" runat="server" Text="Limpar Filtro" OnClick="btnLimparPesquisa_Click" formnovalidate="true" />
                </div>
    
                <div style="margin-bottom: 20px; padding: 15px; border: 1px solid #ccc; max-width: 600px;">
                    <asp:HiddenField ID="hfPessoaId" runat="server" />

                    <label>Nome:</label><br />
                    <asp:TextBox ID="txtNome" runat="server" Width="100%" required="true"></asp:TextBox><br /><br />

                    <div style="display: flex; gap: 10px;">
                        <div style="flex: 1;">
                            <label>Data Nascimento:</label><br />
                            <asp:TextBox ID="txtDataNascimento" runat="server" TextMode="Date" Width="100%" required="true"></asp:TextBox>
                        </div>
                        <div style="flex: 1;">
                            <label>ID do Cargo:</label><br />
                            <asp:TextBox ID="txtCargoId" runat="server" TextMode="Number" Width="100%" required="true"></asp:TextBox>
                        </div>
                    </div><br />

                    <label>E-mail:</label><br />
                    <asp:TextBox ID="txtEmail" runat="server" Width="100%"></asp:TextBox><br /><br />

                    <div style="display: flex; gap: 10px;">
                        <div style="flex: 1;"><label>Telefone:</label><br /><asp:TextBox ID="txtTelefone" runat="server" Width="100%"></asp:TextBox></div>
                        <div style="flex: 1;"><label>Usuário:</label><br /><asp:TextBox ID="txtUsuario" runat="server" Width="100%"></asp:TextBox></div>
                    </div><br />

                    <div style="display: flex; gap: 10px;">
                        <div style="flex: 1;"><label>CEP:</label><br /><asp:TextBox ID="txtCep" runat="server" Width="100%"></asp:TextBox></div>
                        <div style="flex: 2;"><label>Endereço:</label><br /><asp:TextBox ID="txtEndereco" runat="server" Width="100%"></asp:TextBox></div>
                    </div><br />

                    <div style="display: flex; gap: 10px;">
                        <div style="flex: 1;"><label>Cidade:</label><br /><asp:TextBox ID="txtCidade" runat="server" Width="100%"></asp:TextBox></div>
                        <div style="flex: 1;"><label>País:</label><br /><asp:TextBox ID="txtPais" runat="server" Width="100%"></asp:TextBox></div>
                    </div><br />

                    <asp:Button ID="btnSalvar" runat="server" Text="Salvar Registro" OnClick="btnSalvar_Click" />
                    <asp:Button ID="btnLimpar" runat="server" Text="Cancelar / Limpar" OnClick="btnLimpar_Click" formnovalidate="true" />
                </div>

                <asp:GridView ID="gridPessoas" runat="server" AutoGenerateColumns="False" DataKeyNames="id" 
                              OnRowCommand="gridPessoas_RowCommand" CellPadding="4" ForeColor="#333333" GridLines="None">
                    <AlternatingRowStyle BackColor="White" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <RowStyle BackColor="#EFF3FB" />
                    <Columns>
                        <asp:BoundField DataField="id" HeaderText="ID" />
                        <asp:BoundField DataField="nome" HeaderText="Nome" />
                        <asp:BoundField DataField="cargo_nome" HeaderText="Cargo" />
                        <asp:BoundField DataField="cidade" HeaderText="Cidade" />
            
                        <asp:TemplateField HeaderText="Ações">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnEditar" runat="server" CommandName="Editar" CommandArgument='<%# Eval("id") %>'>Editar</asp:LinkButton> | 
                                <asp:LinkButton ID="btnExcluir" runat="server" CommandName="Excluir" CommandArgument='<%# Eval("id") %>' OnClientClick="return confirm('Tem certeza que deseja excluir?');">Excluir</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </form>
</body>
</html>
