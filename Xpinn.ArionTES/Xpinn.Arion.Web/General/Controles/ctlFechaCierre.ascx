<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ctlFechaCierre.ascx.cs" Inherits="ctlFechaCierre" %>
<asp:DropDownList ID="ddlFechaCierre" runat="server" CssClass="textbox" Width="150px" AppendDataBoundItems="True" AutoPostBack="false">
    <asp:ListItem Value="0" Text="Seleccion un Item" />
</asp:DropDownList>
<asp:RequiredFieldValidator ID="rfvFechaCierre" runat="server" 
    ErrorMessage="Seleccione fecha cierre" Display="Dynamic" 
    ControlToValidate="ddlFechaCierre" ValidationGroup="" InitialValue = "0" 
    Font-Size="X-Small" ForeColor="Red" />