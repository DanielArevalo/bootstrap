<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Detalle.aspx.cs" Inherits="Detalle" Title=".: Xpinn - Tercero :." %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="Fecha" TagPrefix="ucFecha" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:Panel ID="pConsulta" runat="server" >
        <table id="tbCriterios" border="0" cellpadding="5" cellspacing="0" >
            <tr>
                <td class="logo" style="width: 49px; text-align:left">
                    Código<br/>
                    <asp:TextBox ID="txtCodigo" runat="server" CssClass="textbox" Enabled="False" />
                </td>
                <td style="width: 231px; text-align:left">
                    Nit<br/>
                    <asp:TextBox ID="txtIdentificacion" runat="server" CssClass="textbox" Enabled="False" />-
                    <asp:TextBox ID="txtDigitoVerificacion" runat="server" CssClass="textbox" Enabled="False" Width="30px" />
                </td>
                <td class="logo" style="width: 49px; text-align:left" colspan="2">
                    &nbsp;
                </td>
                <td class="tdD">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="tdI" colspan="4" style="text-align:left">
                    Razón Social<br />
                    <asp:TextBox ID="txtRazonSocial" runat="server" CssClass="textbox" MaxLength="128"  Enabled="False" 
                        Width="574px" />
                </td>
                <td class="tdI">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="tdI" colspan="4" style="text-align:left">
                    Sigla<br />
                    <asp:TextBox ID="txtSigla" runat="server" CssClass="textbox" MaxLength="128"  Enabled="False" 
                        Width="574px" />
                </td>
                <td class="tdI">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="tdI" colspan="4" style="text-align:left">
                    Dirección<br />
                    <asp:TextBox ID="txtDireccion" runat="server" CssClass="textbox" MaxLength="128"  Enabled="False" 
                        Width="574px" />
                </td>
                <td class="tdI">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="logo" colspan="2" style="text-align:left">
                    Ciudad<br/>
                    <asp:DropDownList ID="ddlCiudad" runat="server" Width="340px"  Enabled="False"
                        CssClass="dropdown" Height="25px">
                    </asp:DropDownList>                           
                </td>
                <td style="text-align:left">
                    Telefóno<br/>
                    <asp:TextBox ID="txtTelefono" runat="server" CssClass="textbox" Enabled="False" />
                </td>
                <td style="text-align:left">
                    &nbsp;</td>
                <td class="tdD">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="logo" colspan="2" style="text-align:left">
                    E-Mail<br/>
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="textbox" Width="340px" Enabled="False"  />
                </td>
                <td style="text-align:left">
                    Fecha<br/>                   
                   <ucFecha:Fecha ID="txtFecha" runat="server" Enabled="False" />
                </td>
                <td style="text-align:left">
                    &nbsp;</td>
                <td class="tdD">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="tdI" colspan="4" style="text-align:left">
                    Actividad<br/>
                    <asp:DropDownList ID="ddlActividad" runat="server" Width="574px"  Enabled="False"
                        CssClass="dropdown" Height="25px">
                    </asp:DropDownList>                           
                </td>
                <td class="tdI">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="tdI" colspan="4" style="text-align:left">
                    Regimen<br/>
                    <asp:DropDownList ID="ddlRegimen" runat="server" Width="574px"  Enabled="False"
                        CssClass="dropdown" Height="25px">
                        <asp:ListItem Value="C">COMUN</asp:ListItem>
                        <asp:ListItem Value="S">SIMPLIFICADO</asp:ListItem>
                        <asp:ListItem Value="E">ESPECIAL</asp:ListItem>
                        <asp:ListItem Value="GCA">GRAN CONTRIBUYENTE AUTORETENEDOR</asp:ListItem>
                        <asp:ListItem Value="GCNA">GRAN CONTRIBUYENTE NO AUTORETENEDOR</asp:ListItem>
                    </asp:DropDownList>                           
                </td>
                <td class="tdI">
                    &nbsp;</td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>