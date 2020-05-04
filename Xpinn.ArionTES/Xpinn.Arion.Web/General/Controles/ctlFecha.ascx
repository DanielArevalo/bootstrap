<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ctlFecha.ascx.cs" Inherits="ctlFecha" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:TextBox ID="txtFecha" runat="server" CssClass="textbox" Width="120px" ></asp:TextBox>
<asp:CalendarExtender ID="calExtFecha" Format="dd/MM/yyyy" runat="server" TargetControlID="txtFecha" />
<asp:RequiredFieldValidator ID="rfvFecha" runat="server" ErrorMessage="Campo Requerido"
    ControlToValidate="txtFecha" Display="Dynamic" ForeColor="Red" 
    ValidationGroup="vgGuardar" style="font-size: xx-small" />