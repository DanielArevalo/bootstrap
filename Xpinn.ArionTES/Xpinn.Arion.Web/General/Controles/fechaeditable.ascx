<%@ Control Language="C#" AutoEventWireup="true" CodeFile="fechaeditable.ascx.cs" Inherits="fechaeditable" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:TextBox ID="txtFechaIngreso" runat="server" CssClass="textbox" 
    ValidationGroup="vgFechaIngreso" 
    ontextchanged="txtFechaIngreso_TextChanged" AutoPostBack="True" ></asp:TextBox>
<asp:CalendarExtender ID="calExtFechaIngreso" Format="dd/MM/yyyy" runat="server" TargetControlID="txtFechaIngreso">
</asp:CalendarExtender>
    <asp:MaskedEditExtender ID="MEEfecha" runat="server" TargetControlID="txtFechaIngreso" Mask="99/99/9999"
    MessageValidatorTip="true" MaskType="Date" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" />
<asp:RequiredFieldValidator ID="rfvFecha" runat="server" ErrorMessage="Campo Requerido"
    ControlToValidate="txtFechaIngreso" Display="Dynamic" ForeColor="Red" 
    ValidationGroup="vgGuardar" style="font-size: xx-small" Enabled="False" />
