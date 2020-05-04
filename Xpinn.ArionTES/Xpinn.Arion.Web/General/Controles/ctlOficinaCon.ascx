<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ctlOficinaCon.ascx.cs" Inherits="General_Controles_ctlOficinaCon" %>
<asp:DropDownList ID="ddlOficina" runat="server" CssClass="textbox" Width="300px" AppendDataBoundItems="True">
</asp:DropDownList>
<asp:RequiredFieldValidator ID="rfvOficina" runat="server" ErrorMessage="Seleccione oficina" Display="Dynamic" ControlToValidate="ddlOficina" 
    ValidationGroup="vgGuardar" InitialValue="0" Font-Size="X-Small" ForeColor="Red" />
