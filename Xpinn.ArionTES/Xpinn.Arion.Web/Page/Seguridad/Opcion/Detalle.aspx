<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Detalle.aspx.cs" Inherits="Detalle" Title=".: Xpinn - Opcion :." %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <table border="0" cellpadding="5" cellspacing="0" width="100%">
        <tr>
            <td class="tdI">
                C&oacute;digo&nbsp;*<br />
                <asp:TextBox ID="txtCod_opcion" runat="server" CssClass="textbox" Enabled="false" />
            </td>
            <td class="tdD">
                Nombre&nbsp;*<br />
                <asp:TextBox ID="txtNombre" runat="server" CssClass="textbox" Enabled="false" />
            </td>
        </tr>
        <tr>
            <td class="tdI">
                Proceso&nbsp;*<br />
                <asp:DropDownList ID="txtCod_proceso" runat="server" CssClass="textbox" Enabled="false" />
            </td>
            <td class="tdD">
                Ruta&nbsp;*<br />
                <asp:TextBox ID="txtRuta" runat="server" CssClass="textbox" Enabled="false" />
            </td>
        </tr>
        <tr>
            <td class="tdD">
                Genera Log&nbsp;*<br />
                <asp:DropDownList ID="txtGeneralog" runat="server" CssClass="textbox" Enabled="false" >
                        <asp:ListItem Text="Si" Value="1" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="No" Value="0"></asp:ListItem>
                       </asp:DropDownList>
            </td>
            <td class="tdI">
                Ayuda&nbsp;<br />
                <asp:TextBox ID="txtRefayuda" runat="server" CssClass="textbox" Enabled="false" />
            </td>
        </tr>
        <tr>
            <td class="tdD">
                Visualizar desde Menu *<br />
                <asp:DropDownList ID="txtAccion" runat="server" CssClass="textbox" Enabled="false" >
                        <asp:ListItem Text="Si" Value="1" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="No" Value="0"></asp:ListItem>
                       </asp:DropDownList>
            </td>
            <td class="tdI">
                &nbsp;</td>
        </tr>
        </table>
</asp:Content>
