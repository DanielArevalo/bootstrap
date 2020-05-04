<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ctlLineaAhorro.ascx.cs" Inherits="ctlLineaAhorro" %>
<asp:DropDownList ID="ddlLineaAhorro" runat="server" CssClass="textbox" 
    Width="300px" AppendDataBoundItems="True" 
    onselectedindexchanged="ddlLineaAhorro_SelectedIndexChanged">
    <asp:ListItem Value="">Seleccion un Item</asp:ListItem>
</asp:DropDownList>
<asp:RequiredFieldValidator ID="rfvLineaAhorro" runat="server" 
    ErrorMessage="Seleccione Línea" Display="Dynamic" 
    ControlToValidate="ddlLineaAhorro" ValidationGroup="vgGuardar" InitialValue = "" 
    Font-Size="X-Small" ForeColor="Red" />