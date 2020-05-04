<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Detalle.aspx.cs" Inherits="Detalle" Title=".: Xpinn - Modulo :." %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <table border="0" cellpadding="5" cellspacing="0" width="100%">
        <tr>
            <td class="tdI">
                C&oacute;digo&nbsp;*<br />
                <asp:TextBox ID="txtCod_modulo" runat="server" CssClass="textbox" Enabled="false" />
            </td>
            <td class="tdD">
                Nombre&nbsp;*<br />
                <asp:TextBox ID="txtNom_modulo" runat="server" CssClass="textbox" Enabled="false" />
            </td>
        </tr>
    </table>
</asp:Content>
