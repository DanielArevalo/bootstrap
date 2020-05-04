<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
        <asp:Panel ID="panelLocal" runat="server">
            <table border:"0" cellpadding:"0" cellspacing:"0" width:"100%">
                <tr>
                    <td style="text-align: left; width:250px;">Descripccion<br />
                       <asp:TextBox ID="txtDescripccion" runat="server" CssClass="textbox" Width="200px"  Enabled="false" AutoPostBack="True"></asp:TextBox>
                    </td>
                     <td colspan="3" style="text-align: left">Valor<br />
                         <asp:DropDownList ID="ddlValor" runat="server" CssClass="textbox" Width="150px">
                              <asp:ListItem Value="0">Selecione un valor </asp:ListItem>
                              <asp:ListItem Value="1">Bajo</asp:ListItem>
                              <asp:ListItem Value="2">Medio</asp:ListItem>
                              <asp:ListItem Value="3">Alto</asp:ListItem>
                     </asp:DropDownList>
                    </td>
                    <td colspan="3" style="text-align: left">Tipo persona<br />
                         <asp:DropDownList ID="ddlTipoPersona" required runat="server" CssClass="textbox" Width="150px">
                             <asp:ListItem Value="">Selecione un valor</asp:ListItem>
                              <asp:ListItem Value="N">Natural</asp:ListItem>
                              <asp:ListItem Value="J">Jurídica</asp:ListItem>
                              <asp:ListItem Value="T">Todos</asp:ListItem>
                     </asp:DropDownList>
                    </td>
                    <td>
                        <asp:HiddenField ID="hdCodperfil" runat="server" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
</asp:Content>

