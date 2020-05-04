<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ctlPersona.ascx.cs" Inherits="ctlPersona" %>
<table style="width:100px;">
    <tr>
        <td style="text-align:left">
            Identificación</td>
        <td style="text-align:left">
            Tipo Identificación</td>
        <td colspan="2" style="text-align:left">
            Nombres y Apellidos</td>
    </tr>
    <tr>
        <td style="text-align:left">
            <asp:DropDownList ID="txtIdentificacion" runat="server" CssClass="textbox" 
                Width="125px" style="text-align:left" AppendDataBoundItems="True" 
                onselectedindexchanged="txtIdentificacion_SelectedIndexChanged" AutoPostBack="True">
                <asp:ListItem Value="">Seleccion un Item</asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvIdentificacion" runat="server" 
                ErrorMessage="Debe ingresar la identificación" 
                ControlToValidate="txtIdentificacion" Font-Size="XX-Small" 
                ValidationGroup="vgGuardar" Display="Dynamic"></asp:RequiredFieldValidator>
        </td>
        <td style="text-align:left">
            <asp:DropDownList ID="ddlTipoIdentificacion" runat="server" CssClass="textbox" 
                Width="150px" style="text-align:left">
            </asp:DropDownList>
        </td>
        <td style="text-align:left">
            <asp:TextBox ID="txtNombres" runat="server" CssClass="textbox" Width="400px" 
                style="text-align:left" Enabled="False"></asp:TextBox>
        </td>
    </tr>
</table>