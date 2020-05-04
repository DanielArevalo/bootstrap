<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ctlMoneda.ascx.cs" Inherits="ctlMoneda" %>
<asp:DropDownList ID="ddlMoneda" runat="server" Width="120px" CssClass="textbox" >        
</asp:DropDownList>
<asp:RequiredFieldValidator ID="rfvMoneda" runat="server" ErrorMessage="Seleccione la moneda" Display="Dynamic" ControlToValidate="ddlMoneda" 
    ValidationGroup="vgGuardar" InitialValue="0" Font-Size="X-Small" ForeColor="Red" />