<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ctlModalidad.ascx.cs" Inherits="ctlModalidad" %>
<asp:DropDownList ID="ddlModalidad" runat="server" CssClass="textbox" 
    Width="165px" AppendDataBoundItems="True" 
    onselectedindexchanged="ddlModalidad_SelectedIndexChanged" 
    AutoPostBack="True">
    <asp:ListItem Value="">Seleccion un Item</asp:ListItem>
    <asp:ListItem Value="1">INDIVIDUAL</asp:ListItem>
    <asp:ListItem Value="2">CONJUNTA</asp:ListItem>
    <asp:ListItem Value="3">ALTERNA</asp:ListItem>
</asp:DropDownList>
<asp:RequiredFieldValidator ID="rfvModalidad" runat="server" 
    ErrorMessage="Seleccione Modalidad" Display="Dynamic" 
    ControlToValidate="ddlModalidad" ValidationGroup="vgGuardar" InitialValue = "" 
    Font-Size="X-Small" ForeColor="Red" />