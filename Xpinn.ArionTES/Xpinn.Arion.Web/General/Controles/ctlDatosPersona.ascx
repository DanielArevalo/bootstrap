<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ctlDatosPersona.ascx.cs" Inherits="ctlDatosPersona" %>

<table>
    <tr>
        <td style="text-align: left">
            Identificación
        </td>
        <td style="text-align: left">
            Tipo Identificación
        </td>
        <td colspan="2" style="text-align: left">
            Nombres y Apellidos
        </td>
    </tr>
    <tr>
        <td style="text-align: left">
            <asp:TextBox ID="txtCodigo" runat="server" CssClass="textbox" Width="30px" Visible="false"
                Style="text-align: left" />
            <asp:TextBox ID="txtIdentificacion" runat="server" CssClass="textbox" Width="110px" Style="text-align: left" Enabled="False" />
        </td>
        <td style="text-align: left">
            <asp:DropDownList ID="ddlTipoIdentificacion" runat="server" CssClass="textbox" Width="150px" Style="text-align: left" Enabled="false" />
        </td>
        <td style="text-align: left">
            <asp:TextBox ID="txtNombres" runat="server" CssClass="textbox" Width="400px" Style="text-align: left" Enabled="False"></asp:TextBox>
        </td>
    </tr>
</table>
