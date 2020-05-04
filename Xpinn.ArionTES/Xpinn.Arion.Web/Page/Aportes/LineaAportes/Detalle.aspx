<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Detalle.aspx.cs" Inherits="Nuevo" %>
 <%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="../../../General/Controles/fecha.ascx" tagname="fecha" tagprefix="ucFecha" %>
<%@ Register Src="~/General/Controles/direccion.ascx" TagName="direccion" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/tnumero.ascx" TagName="txtPesos" TagPrefix="uc1" %>

<%@ Register src="../../../General/Controles/decimales.ascx" tagname="decimales" tagprefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
 <asp:ScriptManager ID="ScriptManager1" runat="server">
  </asp:ScriptManager>
    <asp:Panel ID="pConsulta" runat="server" style="margin-right: 0px" 
        Width="868px">
        <table cellpadding="5" cellspacing="0" style="width: 119%">
            <tr>
                <td style="height: 15px; text-align: left; width: 130px;">
                    &nbsp;</td>
                <td colspan="2" style="height: 15px; text-align: left; ">
                    &nbsp;</td>
                <td style="height: 15px; text-align: left; width: 193px;">
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="height: 15px; text-align: left; width: 130px;">
                    Tipo Producto
                    <br />
                    <asp:DropDownList ID="DdlTipoProducto" runat="server" Height="25px" Width="190px" Enabled="False">
                    </asp:DropDownList>
                    <br />
                </td>
                <td colspan="2" style="height: 15px; text-align: left; ">
                    Cod Linea&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Nombre Linea Aporte &nbsp;<br />
                    <asp:TextBox ID="TxtCodLinea" runat="server" CssClass="textbox" Width="76px" Enabled="False" />
                    &nbsp;&nbsp;<asp:TextBox ID="TxtLinea" runat="server" CssClass="textbox" Width="331px" Enabled="False" />
                </td>
                <td style="height: 15px; text-align: left; width: 193px;">
                    <asp:CheckBox ID="chkDistribuye" runat="server" AutoPostBack="True" 
                        style="font-size: 7pt; text-align: left;" Text="Distribuye" 
                        Enabled="False" />
                    &nbsp; % Distribución&nbsp;&nbsp;&nbsp;&nbsp;
                    <br />
                    &nbsp;<asp:TextBox ID="txtDistribucion" runat="server" CssClass="textbox" 
                        Width="185px" Enabled="False" />
                </td>
            </tr>
            <tr>
                <td style="height: 15px; text-align: left; width: 130px;">
                    Retiro Mínimo<br />
                    <uc2:decimales ID="Txtretirominimo" runat="server" Enabled="False" />
                </td>
                <td style="height: 15px; text-align: left; width: 161px;">
                    Retiro Máximo<br />
                    <uc2:decimales ID="Txtretiromaximo" runat="server" Enabled="False" />
                </td>
                <td style="height: 15px; text-align: left; width: 88px;">
                    <asp:Label ID="Label2" runat="server" Text="Valor Cuota Mínima" Width="166px"></asp:Label>
                    <br />
                    <uc2:decimales ID="Txtcuotaminima" runat="server" Enabled="False" />
                </td>
                <td style="height: 15px; text-align: left; width: 193px;">
                    Valor cuota Máxima<br />
                    <uc2:decimales ID="Txtcuotamaxima" runat="server" Enabled="False" />
                </td>
            </tr>
            <tr>
                <td style="height: 6px; text-align: left; width: 130px;">
                    Cruzar<br />
                    <asp:CheckBox ID="ChkcruceSI" runat="server" AutoPostBack="True"                        
                        style="font-size: x-small; text-align: left;" Text="SI" 
                        oncheckedchanged="ChkcruceSI_CheckedChanged" Enabled="False" />
                    <asp:CheckBox ID="ChkcruceNO" runat="server" AutoPostBack="True"                        
                        style="font-size: x-small; text-align: left;" Text="NO" 
                        oncheckedchanged="ChkcruceNO_CheckedChanged" Enabled="False" />
                </td>
                <td style="text-align: left; width: 161px;">
                    &nbsp;% cruce&nbsp;&nbsp;&nbsp;&nbsp;
                    <br />
                    <asp:TextBox ID="txtCruce" runat="server" CssClass="textbox" Width="182px" 
                        Enabled="False" />
                </td>
                <td style="text-align: left; width: 88px;" class="logo">
                    Tipo De Cuota<br />
                    <asp:DropDownList ID="DdlTipoCuota" runat="server" Height="24px" Width="190px" Enabled="False" CssClass="textbox">
                        <asp:ListItem Value="1">CUOTA FIJA</asp:ListItem>
                        <asp:ListItem Value="2">RANGOS POR LINEA</asp:ListItem>
                        <asp:ListItem Value="3">DISTRIBUCION</asp:ListItem>
                        <asp:ListItem Value="4">PORCENTAJE DEL SUELDO</asp:ListItem>
                        <asp:ListItem Value="5">PORCENTAJE SMLV</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td style="height: 15px; text-align: left; width: 193px;">
                    Tipo De Liquidacion<br />
                    <asp:DropDownList ID="Ddltipoliquidacion" runat="server" Height="25px" Width="190px" Enabled="False" CssClass="textbox" />                    
                </td>
            </tr>
            <tr>
                <td style="height: 15px; text-align: left; width: 130px;">
                    Estado<br />
                    <asp:CheckBox ID="Ckhactiva" runat="server" AutoPostBack="True" 
                        style="font-size: x-small; text-align: left;" 
                        Text="Activa" oncheckedchanged="Ckhactiva_CheckedChanged" 
                        Enabled="False" />
                    <asp:CheckBox ID="Ckhinactiva" runat="server" AutoPostBack="True"                        
                        style="font-size: x-small; text-align: left;" Text="Inactiva" 
                        oncheckedchanged="Ckhinactiva_CheckedChanged" Enabled="False" />
                    <asp:CheckBox ID="Ckhcerrada" runat="server" AutoPostBack="True" 
                        style="font-size: x-small; text-align: left;" Text="Cerrada" 
                        oncheckedchanged="Ckhcerrada_CheckedChanged" Enabled="False" />
                </td>
                <td style="text-align: left; width: 161px; height: 15px;">
                    Saldo mínimo Liquidación<uc2:decimales ID="Txtsaldominliquid" runat="server" 
                        Enabled="False" />
                </td>
                <td style="text-align: left; height: 15px; width: 88px;">
                    Saldo Mínimo<br />
                    <uc2:decimales ID="TxtSaldoMinimo" runat="server" Enabled="False" />
                </td>
                <td style="height: 15px; text-align: left; width: 193px;">
                    &nbsp;<br />
                </td>
            </tr>
            <tr>
                <td style="height: 15px; text-align: left; font-size: x-small; width: 130px;">
                    &nbsp;</td>
            </tr>
        </table>
    </asp:Panel>
    <table style="width:70%">
        <tr>  
            <td style="width: 920px">
                <asp:Label ID="lblTotalRegs" runat="server" Visible="False" />
                <asp:Label ID="lblInfo" runat="server" Text="Su consulta no obtuvo ningún resultado." Visible="False" />
            </td>
        </tr>        
    </table>
 
</asp:Content>
