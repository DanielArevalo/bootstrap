<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table cellpadding="5" cellspacing="0" style="width: 100%">
     <tr>
        <td>
            Fecha de Reintegro<br />
            <asp:TextBox ID="txtFechaReintegro" Enabled="false" runat="server" CssClass="textbox" 
                MaxLength="10" Width="150" style="text-align: center"></asp:TextBox>
        </td>
         <td>
            &nbsp;<br/>
        </td>
         <td class="tdI">
             &nbsp;</td>
    </tr>
     <tr>
        <td style="background-color: #3599F7; text-align: center;" colspan="3">
            <strong style="color: #FFFFFF">
            Información de Reintegro</strong>
        </td>
        <td style="background-color: #3599F7; text-align: center;">
            &nbsp;</td>
    </tr>
    <tr>
      <td class="tdI">
            Oficina <br/>
            <asp:TextBox ID="txtOficina" runat="server" Enabled="False" CssClass="textbox" Width="150px"></asp:TextBox>
        </td>
         <td>
            Caja<br/>
            <asp:TextBox ID="txtCaja" runat="server" Enabled="False" CssClass="textbox" Width="150px"></asp:TextBox>
        </td>
         <td class="tdI">
            Cajero <br/>
            <asp:TextBox ID="txtCajero" runat="server" Enabled="False" CssClass="textbox" Width="150px"></asp:TextBox>
        </td>
    </tr>
    <tr>
     <td class="tdI">
            Banco<br/>
            <asp:DropDownList ID="ddlBancos" CssClass="dropdown"  runat="server" 
                Height="27px" Width="155px">
            </asp:DropDownList> 
        </td>
        <td>
             Moneda<br/>
             <asp:DropDownList ID="ddlMonedas" CssClass="dropdown"  runat="server" 
                Height="27px" Width="155px">
            </asp:DropDownList> 
        </td>
        <td>
            Valor
            <br/>           
            <uc1:decimales ID="txtValor" runat="server" CssClass="textbox" Width="260px" 
            MaxLength="9" style="text-align: right">
            </uc1:decimales>
        </td>
    </tr>
</table>
</asp:Content>