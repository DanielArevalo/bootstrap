<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ctlFormaPago.ascx.cs" Inherits="ctlFormaPago" %>
<asp:DropDownList ID="ddlFormaPago" runat="server" Width="120px" 
    CssClass="textbox" onselectedindexchanged="ddlFormaPago_SelectedIndexChanged">
    <asp:ListItem Value="1">Caja</asp:ListItem>
    <asp:ListItem Value="2">Nomina</asp:ListItem>
</asp:DropDownList>
<asp:RequiredFieldValidator ID="rfvFormaPago" runat="server" ErrorMessage="Seleccione forma de pago" Display="Dynamic" ControlToValidate="ddlFormaPago" 
    ValidationGroup="vgGuardar" InitialValue="0" Font-Size="X-Small" ForeColor="Red" />