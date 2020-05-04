<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ctlResponsable.ascx.cs" Inherits="General_Controles_ctlResponsable" %>

<asp:DropDownList 
    ID="ddlResponsable" 
    runat="server" 
    CssClass="textbox" 
    Width="200px" 
    AppendDataBoundItems="True">
    <asp:ListItem Value="0">Seleccion un Item</asp:ListItem>
</asp:DropDownList>
<asp:RequiredFieldValidator ID="rfrespon" runat="server" ErrorMessage="Seleccione un Item" Display="Dynamic" ControlToValidate="ddlResponsable" 
    ValidationGroup="vgGuardar" InitialValue="0" Font-Size="X-Small" ForeColor="Red" />