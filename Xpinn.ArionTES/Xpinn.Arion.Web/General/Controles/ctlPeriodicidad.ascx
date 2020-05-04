<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ctlPeriodicidad.ascx.cs" Inherits="ctlPeriodicidad" %>
<asp:DropDownList ID="ddlPeriodicidad" runat="server" CssClass="textbox" Width="150px" AppendDataBoundItems="True" AutoPostBack="false">
    <asp:ListItem Value="0" Text="Seleccion un Item" />
</asp:DropDownList>
<asp:RequiredFieldValidator ID="rfvPeriodicidad" runat="server" 
    ErrorMessage="Seleccione periodicidad" Display="Dynamic" 
    ControlToValidate="ddlPeriodicidad" ValidationGroup="" InitialValue = "0" 
    Font-Size="X-Small" ForeColor="Red" />