<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ctlFechaHora.ascx.cs" Inherits="ctlFechaHora" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:TextBox ID="txtFecha" runat="server" CssClass="textbox" Width="90px" ></asp:TextBox>
<asp:CalendarExtender ID="calExtFecha" Format="dd-MM-yyyy HH':'mm':'ss" runat="server" TargetControlID="txtFecha" />
<asp:RequiredFieldValidator ID="rfvFecha" runat="server" ErrorMessage="Campo Requerido"
    ControlToValidate="txtFecha" Display="Dynamic" ForeColor="Red" 
    ValidationGroup="vgGuardar" style="font-size: xx-small" />
