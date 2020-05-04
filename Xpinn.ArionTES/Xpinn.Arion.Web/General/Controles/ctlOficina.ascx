<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ctlOficina.ascx.cs" Inherits="ctlOficina" %>
<asp:DropDownList ID="ddlOficina" runat="server" CssClass="textbox" Width="300px" AppendDataBoundItems="True">
    <asp:ListItem Value="0">Seleccion un Item</asp:ListItem>
</asp:DropDownList>
<asp:RequiredFieldValidator ID="rfvOficina" runat="server" ErrorMessage="Seleccione oficina" Display="Dynamic" ControlToValidate="ddlOficina" 
    ValidationGroup="vgGuardar" InitialValue="0" Font-Size="X-Small" ForeColor="Red" />