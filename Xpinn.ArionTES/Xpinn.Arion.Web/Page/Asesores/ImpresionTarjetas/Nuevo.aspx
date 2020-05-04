<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Nuevo.aspx.cs" Inherits="Detalle" MasterPageFile="~/General/Master/site.master"%>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register assembly="BarcodeX" namespace="Fath" tagprefix="bcx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
    <script type="text/javascript">
        function click7(TextBox) {
        }

        function ActiveTabChanged(sender, e) {
        }

        var HighlightAnimations = {};

        function Highlight(el) {
            if (HighlightAnimations[el.uniqueID] == null) {
                HighlightAnimations[el.uniqueID] = Sys.Extended.UI.Animation.createAnimation({
                    AnimationName: "color",
                    duration: 0.5,
                    property: "style",
                    propertyKey: "backgroundColor",
                    startValue: "#FFFF90",
                    endValue: "#FFFFFF"
                }, el);
            }
            HighlightAnimations[el.uniqueID].stop();
            HighlightAnimations[el.uniqueID].play();
        }

    </script>

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>    
    <div id="DivButtons">
        <hr style="width: 98%" />
        <table style="width:100%;">
            <tr>
                <td colspan="4">
                    <div style="font-size: xx-small">
                    </div>
                </td>
            </tr>
            <tr>
                <td style="width: 97px; text-align:left">
                    Identificación</td>
                <td style="width: 29px; text-align:left">
                    <asp:TextBox ID="txtIdentific" runat="server" Width="139px" 
                        CssClass="textbox2" ontextchanged="txtIdentific_TextChanged" 
                        AutoPostBack="True"></asp:TextBox>
                </td>
                <td style="text-align: left; width: 60px;">
                </td>
                <td style="text-align: left">                            
                </td>
            </tr>
            <tr>
                <td style="width: 97px; text-align:left">
                        Nombres</td>
                <td style="width: 29px; text-align:left">
                    <asp:TextBox ID="txtNombre" runat="server" Enabled="False" Width="335px" 
                        CssClass="textbox2"></asp:TextBox>
                </td>
                <td style="text-align: left; width: 60px;">
                    &nbsp;</td>
                <td style="text-align: left">
                    &nbsp;</td>
            </tr>
        </table>
        <br />
        <div style="text-align:left">
            &nbsp;&nbsp;
            <asp:Button ID="btnGenerar" runat="server" CssClass="btn8" Text="Generar Código Barras" onclick="btnGenerar_Click" onclientclick="btnGenerarr_Click" Width="150px" />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnImprimir" runat="server" CssClass="btn8" Text="Imprimir" 
                onclick="btnImprimir_Click"  />
        </div>
        <br />     
        <iframe id="frmPrint" name="IframeName" width="500" src="ImpresionTarjeta.aspx"
            height="200" runat="server" style="border-style: dotted; float: left;" ></iframe>   
        <asp:TabContainer runat="server" ID="TabsTarjeta" 
            OnClientActiveTabChanged="ActiveTabChanged" ActiveTabIndex="0" Width="350px" 
            style="margin-right: 5px" CssClass="CustomTabStyle" >
            <asp:TabPanel runat="server" ID="tabReporte" HeaderText="Reporte" >
                <HeaderTemplate>
                    Reporte                                                
                </HeaderTemplate>             
                <ContentTemplate>                    
                    <rsweb:ReportViewer ID="rvTarjeta" runat="server" Font-Names="Verdana" 
                        Font-Size="8pt" InteractiveDeviceInfos="(Colección)" 
                        WaitMessageFont-Names="Verdana" WaitMessageFont-Size="10pt" Width="100%" 
                        Height="280px" ShowBackButton="False" ShowCredentialPrompts="False" 
                        ShowDocumentMapButton="False" ShowFindControls="False" 
                        ShowPageNavigationControls="False" ShowParameterPrompts="False" 
                        ShowPromptAreaButton="False" ShowWaitControlCancelLink="False" SizeToReportContent="True"
                        ShowZoomControl="False" AsyncRendering="False" InteractivityPostBackMode="AlwaysSynchronous" >
                        <LocalReport ReportPath="Page\Asesores\ImpresionTarjetas\Tarjeta.rdlc">
                            <DataSources>
                                <rsweb:ReportDataSource />
                            </DataSources>
                        </LocalReport>
                    </rsweb:ReportViewer>
                </ContentTemplate>
            </asp:TabPanel>
            <asp:TabPanel runat="server" ID="tabHtml" HeaderText="Html" >
                <HeaderTemplate>
                    Html                                                
                </HeaderTemplate>             
                <ContentTemplate>
                    <div style="text-align:left" ID="gvDiv">
                    <br /><br /><br />
                    <table style="width:300px">                        
                        <tr>
                            <td style="text-align: center">
                                <asp:Label ID="lblNombre" runat="server" style="font-size: xx-small"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                <asp:HiddenField ID="hdFileName" runat="server" />
                                <asp:HiddenField ID="hdFileNameThumb" runat="server" />
                                <asp:Image ID="imgCodigoBarras" runat="server" Height="60px" Width="300px" ImageUrl="bcx.aspx?data=0"/>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblCodigoBarras" runat="server" Visible="False"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    </div>
                </ContentTemplate>
            </asp:TabPanel>
        </asp:TabContainer>
    </div>

</asp:Content>
