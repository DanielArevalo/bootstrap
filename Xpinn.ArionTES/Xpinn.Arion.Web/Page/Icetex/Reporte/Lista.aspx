<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Lista.aspx.cs" Inherits="Lista" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Src="~/General/Controles/ctlListarCodigo.ascx" TagName="ctlListarCodigo" TagPrefix="ctl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <br />
    <asp:Panel ID="pConsulta" runat="server">
        <table cellpadding="3" cellspacing="3" style="width:100%">
            <tr>
                <td style="text-align: left" colspan="6">
                    <strong>Criterios de búsqueda</strong>
                </td>
            </tr>
            <tr>
                <td style="text-align: left">Convocatoria
                </td>
                <td style="text-align: left" colspan="2">Rango fecha de revisión
                </td>
                <td style="text-align: left">Tipo de Programa
                </td>
                <td style="text-align: left">Tipo de Beneficiario
                </td>
                <td style="text-align: left">Estrato
                </td>
            </tr>
            <tr>
                <td style="text-align: left; vertical-align: top">
                    <asp:Panel runat="server" Width="200px">
                        <ctl:ctlListarCodigo ID="ctllistar" runat="server" />
                    </asp:Panel>
                </td>
                <td style="text-align: left">
                    <ucFecha:fecha ID="txtFecIni" runat="server" />
                </td>
                <td style="text-align: left">
                    <ucFecha:fecha ID="txtFecFin" runat="server" />
                </td>
                <td style="text-align: left; vertical-align: top">
                    <asp:DropDownList ID="ddlTipoPrograma" runat="server" CssClass="textbox" Style="margin: 0px"
                        Width="165px">
                        <asp:ListItem Value="-1">Seleccione un item</asp:ListItem>
                        <asp:ListItem Value="0">Especialización(1 año)</asp:ListItem>
                        <asp:ListItem Value="1">Maestria(2 años)</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td style="text-align: left; vertical-align: top">
                    <asp:DropDownList ID="ddlTipoBeneficiario" runat="server" CssClass="textbox" Width="160px" />
                </td>
                <td style="text-align: left; vertical-align: top">
                    <asp:DropDownList ID="ddlEstrato" runat="server" CssClass="textbox" Style="margin: 0px"
                        Width="125px">
                        <asp:ListItem Value="0">Seleccione un item</asp:ListItem>
                        <asp:ListItem Value="1">1</asp:ListItem>
                        <asp:ListItem Value="2">2</asp:ListItem>
                        <asp:ListItem Value="3">3</asp:ListItem>
                    </asp:DropDownList>
                </td>                
            </tr>
            <tr>
                <td style="text-align: left; vertical-align: top">
                    Semestre<br />
                    <asp:DropDownList ID="ddlPeriodos" runat="server" CssClass="textbox" Style="margin: 0px"
                        Width="130px">
                        <asp:ListItem Value="0">Seleccione un item</asp:ListItem>
                        <asp:ListItem Value="1">1 Semestre</asp:ListItem>
                        <asp:ListItem Value="2">2 Semestre</asp:ListItem>
                        <asp:ListItem Value="3">3 Semestre</asp:ListItem>
                        <asp:ListItem Value="4">4 Semestre</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td style="text-align: left; vertical-align: top">
                    Estado<br />
                    <asp:DropDownList ID="ddlEstado" runat="server" CssClass="textbox" Style="margin: 0px"
                        Width="130px">
                        <asp:ListItem Value="0">Seleccione un item</asp:ListItem>
                        <asp:ListItem Value="S" Selected="True">Pre-Inscrito</asp:ListItem>
                        <asp:ListItem Value="A">Aprobado</asp:ListItem>
                        <asp:ListItem Value="Z">Aplazado</asp:ListItem>
                        <asp:ListItem Value="N">Negado</asp:ListItem>
                        <asp:ListItem Value="I">Inscrito</asp:ListItem>
                        <asp:ListItem Value="T">Terminado</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td colspan="4">&nbsp;</td>
            </tr>
        </table>
    </asp:Panel>
    <hr style="width: 100%" />
    <table cellpadding="3" cellspacing="3" width="100%">
        <tr>
            <td colspan="5">
                <rsweb:ReportViewer ID="rptIcetex" runat="server" Width="100%" Font-Names="Verdana"
                    Font-Size="8pt" InteractiveDeviceInfos="(Colección)" WaitMessageFont-Names="Verdana"
                    WaitMessageFont-Size="14pt" Height="450px">
                    <LocalReport ReportPath="Page\Icetex\Reporte\ReporteIcetex.rdlc">
                    </LocalReport>
                </rsweb:ReportViewer>
            </td>
        </tr>
        <tr>
            <td style="text-align: center">
                <asp:Label ID="lblInfo" runat="server" Text="Su consulta no obtuvo ningún resultado" />
            </td>
        </tr>
    </table>
</asp:Content>
