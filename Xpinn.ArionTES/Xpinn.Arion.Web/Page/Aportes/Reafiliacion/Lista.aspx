<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Lista.aspx.cs" Inherits="Lista" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Src="~/General/Controles/ctlSeleccionarPersona.ascx" TagName="ListadoPersonas" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:MultiView ID="mvReporte" runat="server" ActiveViewIndex="0">
        <asp:View ID="vmirar" runat="server">
            <br />
            <br />
            <div style="text-align: left">
                Fecha proyectada<br />
                <ucFecha:fecha ID="txtfechaProy" runat="server" />
            </div>
            <uc1:ListadoPersonas ID="ctlBusquedaPersonas" runat="server" />
        </asp:View>
        <asp:View ID="vReporte" runat="server">
            <table width="100%">
                <tr>
                    <td>
                        <iframe id="frmPrint" name="IframeName" width="100%" src="../../Reportes/Reporte.aspx"
                            height="500px" runat="server" style="border-style: groove; float: left;"></iframe>
                    </td>
                </tr>
                <tr>
                    <td>
                        <rsweb:ReportViewer ID="RpviewEstado" runat="server" Font-Names="Verdana" Font-Size="8pt"
                            Height="500px" InteractiveDeviceInfos="(Colección)" WaitMessageFont-Names="Verdana"
                            WaitMessageFont-Size="14pt" Width="100%">
                            <LocalReport ReportPath="Page/Cartera/CertificadosAsociados/Reportcertificadosasociados.rdlc">
                            </LocalReport>
                        </rsweb:ReportViewer>
                    </td>
                </tr>
            </table>
        </asp:View>
    </asp:MultiView>
</asp:Content>
