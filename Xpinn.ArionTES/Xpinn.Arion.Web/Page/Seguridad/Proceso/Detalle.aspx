<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Detalle.aspx.cs" Inherits="Detalle" Title=".: Xpinn - Proceso :." %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <table border="0" cellpadding="5" cellspacing="0" width="100%">
        <tr>
            <td class="tdI">
                C&oacute;digo&nbsp;*<br />
                <asp:TextBox ID="txtCod_proceso" runat="server" CssClass="textbox" Enabled="false" />
            </td>
            <td class="tdD">
                Nombre&nbsp;*<br />
                <asp:TextBox ID="txtNombre" runat="server" CssClass="textbox" Enabled="false" />
                <br />
            </td>
        </tr>
        <tr>
            <td class="tdI">
                M&oacute;dulo&nbsp;*<br />
                <asp:DropDownList ID="txtCod_modulo" runat="server" CssClass="dropdown" Enabled="false" />
            </td>
            <td class="tdD">
                &nbsp;
            </td>
    </table>
</asp:Content>
