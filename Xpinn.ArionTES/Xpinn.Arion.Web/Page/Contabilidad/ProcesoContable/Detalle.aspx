<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Detalle.aspx.cs" Inherits="Detalle" Title=".: Xpinn - Concepto :." %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register src="~/General/Controles/fecha.ascx" tagname="fecha" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
    <asp:Panel ID="pConsulta" runat="server">
        <table id="tbCriterios" border="0" cellpadding="5" cellspacing="0" width="50%">
            <tr>
                <td colspan="2" style="text-align:left">
                    Código<br/>
                    <asp:TextBox ID="txtCodigo" runat="server" CssClass="textbox" Width="113px" 
                        Enabled="False" />
                </td>
                <td class="tdD" >
                    <br/>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align:left">
                    Tipo de Operación<br />                       
                    <asp:DropDownList ID="ddlTipoOpe" runat="server" Height="25px" 
                        Width="350px" CssClass="dropdown" AppendDataBoundItems="True" 
                        Enabled="False">
                        <asp:ListItem Value=""></asp:ListItem>  
                    </asp:DropDownList>
                    <br/>
                </td>
                <td style="width: 203px; text-align:left">
                    Tipo de Comprobante<br/>
                    <asp:DropDownList ID="ddlTipoComp" runat="server" Height="25px" Width="200px" 
                        CssClass="dropdown" AppendDataBoundItems="True" Enabled="False"> 
                        <asp:ListItem Value=""></asp:ListItem> 
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="text-align:left;">
                    Fecha Inicial<br/>
                    <asp:TextBox ID="txtFechaInicial" runat="server" Enabled="False"></asp:TextBox>
                </td>
                <td style="text-align:left;">
                    Fecha Final<br/>
                    <asp:TextBox ID="txtFechaFinal" runat="server" Enabled="False"></asp:TextBox>
                </td>
                <td style="text-align:left">
                    Concepto<br />
                    <asp:DropDownList ID="ddlConcepto" runat="server" Height="26px" 
                        Width="200px" CssClass="dropdown" AppendDataBoundItems="True" 
                        Enabled="False"> 
                        <asp:ListItem Value=""></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align:left">
                    Cod.Cuenta<br />
                    <asp:DropDownList ID="ddlCodCuenta" runat="server" Height="26px" 
                        Width="100px" CssClass="dropdown" AppendDataBoundItems="True" 
                        Enabled="False"> 
                        <asp:ListItem Value=""></asp:ListItem>
                    </asp:DropDownList>
                    <asp:DropDownList ID="ddlNomCuenta" runat="server" Height="26px" 
                        Width="250px" CssClass="dropdown" AppendDataBoundItems="True" 
                        Enabled="False"> 
                        <asp:ListItem Value=""></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td style="text-align:left">
                    Tipo Mov.<br />
                    <asp:DropDownList ID="ddlTipoMov" runat="server" Height="26px" 
                        Width="200px" CssClass="dropdown" AppendDataBoundItems="True" 
                        Enabled="False"> 
                        <asp:ListItem Value="1">Débito</asp:ListItem>
                        <asp:ListItem Value="2">Crédito</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align:left">
                    Estructura de Detalle<br />
                    <asp:DropDownList ID="ddlEstructura" runat="server" Width="350px" 
                        CssClass="dropdown" Enabled="False" >
                    </asp:DropDownList>
                </td>
                <td class="tdD" style="text-align:left">
                    &nbsp;
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>