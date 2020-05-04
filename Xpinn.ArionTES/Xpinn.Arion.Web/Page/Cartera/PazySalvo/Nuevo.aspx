<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" EnableEventValidation="false" AutoEventWireup="true" CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" Title=".: Xpinn - Paz y Salvo :." %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<%@ Register src="../../../General/Controles/fecha.ascx" tagname="fecha" tagprefix="uc1" %>
<%@ Register src="../../../General/Controles/decimales.ascx" tagname="decimales" tagprefix="uc2" %>
<%@ Register src="../../../General/Controles/PlanPagos.ascx" tagname="planpagos" tagprefix="uc3" %>
<%@ Register Src="../../../General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>
<%@ Register Src="~/General/Controles/ctlFormatoDocum.ascx" TagName="FormatoDocu" TagPrefix="uc4" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table border="0" cellpadding="5" cellspacing="0" width="100%" >
        <tr>
            <td class="tdI">
                <asp:Panel ID="Panel2" runat="server">
                    <table style="width:100%;">
                        <tr>
                            <td class="logo" colspan="3" style="text-align:left">
                                <strong>DATOS DEL DEUDOR</strong>
                            </td>
                            <td class="logo" style="text-align:left">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td class="logo" style="width: 150px; text-align:left">
                                Identificación
                            </td>
                            <td style="text-align:left">
                                Tipo Identificación
                            </td>
                            <td style="text-align:left">
                                Nombre
                            </td>
                            <td style="text-align:left">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td class="logo" style="width: 150px; text-align:left">
                                <asp:TextBox ID="txtIdentificacion" runat="server" CssClass="textbox" 
                                    Enabled="false" />
                            </td>
                            <td style="text-align:left">
                                <asp:TextBox ID="txtTipo_identificacion" runat="server" CssClass="textbox" Enabled="false" />
                            </td>
                            <td style="text-align:left">
                                <asp:TextBox ID="txtNombre" runat="server" CssClass="textbox" Enabled="false" Width="377px" />
                            </td>
                            <td style="text-align:left">
                                &nbsp;</td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
    </table>

    <asp:MultiView ID="mvCertificado" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwDatos" runat="server">
            <table border="0" cellpadding="5" cellspacing="0" width="100%" >
                <tr>
                    <td colspan="4" style="text-align:left">
                        <strong>DATOS DEL CRÉDITO</strong>
                    </td>
                    <td style="text-align:left">
                        &nbsp;</td>
                    <td style="text-align:left">
                        &nbsp;</td>
                    <td style="text-align:left">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td style="text-align:left; width: 151px;">
                        Número Radicación<br />
                        <asp:TextBox ID="txtNumero_radicacion" runat="server" CssClass="textbox"  Enabled="false" />
                    </td>
                    <td colspan="3" style="text-align:left">
                        Línea de crédito<br />
                        <asp:TextBox ID="txtLinea_credito" runat="server" CssClass="textbox" 
                            Enabled="false" Width="347px" />
                    </td>
                    <td style="text-align:left">
                        &nbsp;</td>
                    <td style="text-align:left">
                        &nbsp;</td>
                    <td style="text-align:left">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td style="text-align:left; width: 151px;">
                        Monto<br />
                        <uc2:decimales ID="txtMonto" runat="server" Enabled="false" />                                
                    </td>
                    <td style="text-align:left">
                        Plazo<br />
                        <asp:TextBox ID="txtPlazo" runat="server" CssClass="textbox" Enabled="false" />
                    </td>
                    <td style="text-align:left">
                        Periodicidad<br />
                        <asp:TextBox ID="txtPeriodicidad" runat="server" CssClass="textbox" Enabled="false" />
                    </td>
                    <td style="text-align:left">
                        Valor de la Cuota<br />
                        <uc2:decimales ID="txtValor_cuota" runat="server" Enabled="false" />                                
                    </td>
                    <td style="text-align:left">
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    </td>
                    <td style="text-align:left">
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    </td>
                    <td style="text-align:left">
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="text-align:left; width: 151px;">
                        Forma de Pago<br />
                        <asp:TextBox ID="txtForma_pago" runat="server" CssClass="textbox" Enabled="false" />
                    </td>
                    <td style="text-align: left">         
                        Moneda<br />
                        <asp:TextBox ID="txtMoneda" runat="server" CssClass="textbox" Enabled="false" />                       
                    </td>
                    <td style="text-align: left">    
                        F.Proximo Pago<br />
                        <asp:TextBox ID="txtFechaProximoPago" runat="server" CssClass="textbox" Enabled="false" />                               
                    </td>
                    <td style="text-align: left">    
                        F.Ultimo Pago <br />
                        <asp:TextBox ID="txtFechaUltimoPago" runat="server" CssClass="textbox" Enabled="false" />                              
                    </td>
                    <td style="text-align: left">     
                        &nbsp;</td>
                    <td style="text-align: left">     
                        &nbsp;</td>
                    <td style="text-align: left">     
                        &nbsp;</td>
                </tr>
                <tr>
                    <td style="text-align:left; width: 151px;">     
                        Saldo Capital<br />
                        <uc2:decimales ID="txtSaldoCapital" runat="server" Enabled="false" />                              
                    </td>
                    <td style="text-align: left">
                        Estado<br />
                        <asp:TextBox ID="txtEstado" runat="server" CssClass="textbox" ReadOnly="True"></asp:TextBox>
                    </td>
                    <td style="text-align: left">   
                        F.Aprobación<br />                         
                        <asp:TextBox ID="txtFechaAprobacion" runat="server" CssClass="textbox" Enabled="false" />                                         
                    </td>
                    <td style="text-align: left">                                
                    </td>
                    <td style="text-align: left">                                
                        &nbsp;</td>
                    <td style="text-align: left">                                
                        &nbsp;</td>
                    <td style="text-align: left">                                
                        &nbsp;</td>
                </tr>
            </table>
            <table border="0" cellpadding="5" cellspacing="0" width="70%" >
                <tr>                    
                    <td style="text-align:left">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td style="text-align:left">
                        <table border="0" cellpadding="5" cellspacing="0" width="100%">
                            <tr>
                                <td style="text-align: left">
                                    Fecha de Paz y Salvo<br />
                                    <uc1:fecha ID="txtFechaCertificado" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <br />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>          
        </asp:View>
        <asp:View ID="vwReporte" runat="server">
            <asp:Panel ID="panelReporte" runat="server" Visible="false" Height="600px">
                <div style="text-align: left">
                    <asp:Button ID="btnVerData" runat="server" CssClass="btn8" Text="Regresar" OnClick="btnVerData_Click"
                        Width="280px" Height="30px" />
                </div>
                <iframe id="frmPrint" name="IframeName" width="100%" src="../../Reportes/Reporte.aspx"
                    height="100%" runat="server" style="border-style: groove; float: left;"></iframe>
            </asp:Panel>
        </asp:View>
        <asp:View ID="vwRDLC" runat="server">
            <br />
            <asp:Button ID="btnRegresar" runat="server" CssClass="btn8" OnClientClick="btnRegresar_Click"
                Text="Regresar" OnClick="btnRegresar_Click" />
            <rsweb:ReportViewer ID="rvCertificado" runat="server" Font-Names="Verdana" Font-Size="8pt"
                InteractiveDeviceInfos="(Colección)" AsyncRendering="false" WaitMessageFont-Names="Verdana"
                WaitMessageFont-Size="10pt" Width="100%">
                <LocalReport ReportPath="Page\Cartera\PazySalvo\RepPazySalvo.rdlc">
                    <DataSources>
                        <rsweb:ReportDataSource />
                    </DataSources>
                </LocalReport>
            </rsweb:ReportViewer>
        </asp:View>
    </asp:MultiView>    



    <uc4:mensajeGrabar ID="ctlMensaje" runat="server"/>

    <uc4:FormatoDocu ID="ctlFormatos" runat="server" />
    <script type='text/javascript'>
        function Forzar() {
            __doPostBack('', '');
        }
    </script>

</asp:Content>