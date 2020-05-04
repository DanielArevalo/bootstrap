<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ctlDestinacion_Ahorros.ascx.cs" Inherits="ctlDestinacionAhorros" %>
<asp:DropDownList ID="ddlDestinacion" runat="server" CssClass="textbox" Width="300px" AppendDataBoundItems="True">
    <asp:ListItem Value="0">Seleccion un Item</asp:ListItem>
</asp:DropDownList>
<asp:RequiredFieldValidator ID="rfvDestinacion" runat="server" ErrorMessage="Seleccione oficina" Display="Dynamic" ControlToValidate="ddlDestinacion" 
    ValidationGroup="vgGuardar" InitialValue="0" Font-Size="X-Small" ForeColor="Red" />