<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ctlPersonaEd.ascx.cs"
    Inherits="ctlPersonaEd" %>
<%@ Register Src="~/General/Controles/ctlBusquedaRapida.ascx" TagName="ListadoPersonas" TagPrefix="uc1" %>

<table>
    <tr>
        <td style="text-align: left" colspan="2">
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
            <asp:TextBox ID="txtIdentificacion" runat="server" CssClass="textbox" Width="110px" AutoPostBack="true"
                Style="text-align: left" OnTextChanged="txtIdentificacion_TextChanged" />          
            <asp:RequiredFieldValidator ID="rfvIdentificacion" runat="server" ErrorMessage="Debe ingresar la identificación"
                ControlToValidate="txtIdentificacion" Font-Size="XX-Small" ValidationGroup="vgGuardar" style="color:Red"
                Display="Dynamic"></asp:RequiredFieldValidator>            
        </td>
        <td style="text-align: left">
            <asp:Button ID="btnConsultaPersonas" CssClass="btn8" runat="server" Text="..." Height="26px"
                OnClick="btnConsultaPersonas_Click" />
            <uc1:ListadoPersonas ID="ctlBusquedaPersonas" runat="server" />            
        </td>
        <td style="text-align: left">
            <asp:DropDownList ID="ddlTipoIdentificacion" runat="server" CssClass="textbox" Width="150px"
                Style="text-align: left">
            </asp:DropDownList>
        </td>
        <td style="text-align: left">
            <asp:TextBox ID="txtNombres" runat="server" CssClass="textbox" Width="400px" Style="text-align: left"
                Enabled="False"></asp:TextBox>
        </td>
    </tr>
</table>
