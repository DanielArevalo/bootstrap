<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="GestionDiaria.aspx.cs" Inherits="Nuevo" %>

<%@ Register Src="../../../General/Controles/ctlBusquedaRapida.ascx" TagName="BusquedaRapida"
    TagPrefix="uc1" %>
<%@ Register Src="../../../General/Controles/decimales.ascx" TagName="decimales"
    TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>  
    <asp:Label ID="lablerror" runat="server" Visible="false" ></asp:Label>
    <asp:Label ID="lablerror0" runat="server" Visible="false" ></asp:Label>
    <table>
        <tr>
            <td>
                <asp:Button ID="btnInforme" 
                    runat="server" CssClass="btn8" 
                    onclick="btnInforme_Click" onclientclick="btnInforme_Click" 
                    Text="Genear Reporte Gestión Diaria Comercial" />
            </td>
        </tr>
    </table>
  
    <asp:Panel id="PanelComprobanteImpr" runat="server">
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana"  Width="100%"
            Font-Size="8pt" InteractiveDeviceInfos="(Colección)" 
            WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" 
            DocumentMapWidth="30%" Height="600px">   
            <localreport reportpath="Page\Indicadores\GestionDiariaComercial\GestionDiaria.rdlc">
            </localreport> 
        </rsweb:ReportViewer>
    </asp:Panel>
   
</asp:Content>
