﻿<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" Title=".: Xpinn - Tipo Soporte :." %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
    <table border="0" cellpadding="5" cellspacing="0" width="100%" >
        <tr>
            <td class="tdI" style="text-align:left">
                Tipo de Soporte&nbsp;*&nbsp;<asp:RequiredFieldValidator ID="rfvtipopago" runat="server" ControlToValidate="txtTipSopCaj" ErrorMessage="Campo Requerido" SetFocusOnError="True" ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic"/><br />
                <asp:TextBox ID="txtTipSopCaj" runat="server" CssClass="textbox" MaxLength="128" />
            </td>
            <td class="tdD">
            </td>
        </tr>
        <tr>
            <td class="tdI" style="text-align:left">
                Descripción&nbsp;*&nbsp;<asp:RequiredFieldValidator ID="rfvDescripcion" runat="server" ControlToValidate="txtDescripcion" ErrorMessage="Campo Requerido" SetFocusOnError="True" ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic"/><br />
                <asp:TextBox ID="txtDescripcion" runat="server" CssClass="textbox" MaxLength="128" Width="519px" />
            </td>
            <td class="tdD">
            </td>
        </tr>
        <tr>
            <td class="tdI" style="text-align:left">
                Cod.Cuenta&nbsp;*&nbsp;<br />            
                <asp:DropDownList ID="ddlCodCuenta" runat="server" AppendDataBoundItems="True" 
                    CssClass="dropdown" style="font-size:xx-small; text-align:left" Height="24" 
                    Width="120px">
                    <asp:ListItem Value=""></asp:ListItem>
                </asp:DropDownList>
            </td>
            <td class="tdD">
            </td>
        </tr>
    </table>
</asp:Content>