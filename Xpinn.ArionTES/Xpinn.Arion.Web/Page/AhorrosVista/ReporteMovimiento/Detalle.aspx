<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Detalle.aspx.cs" Inherits="Detalle" Title=".: Xpinn - Reporte de Movimiento :." %>

<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar"
    TagPrefix="uc4" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/ctlNumero.ascx" TagName="numero" TagPrefix="ucnumero" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:MultiView ID="mvPrincipal" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwData" runat="server">
            <table width="90%">
                <tr>
                    <td style="text-align: left;" colspan="7">
                        <strong>Datos de la Cuenta</strong>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left">
                        Num Cuenta<br />
                        <asp:TextBox ID="txtNumCta" runat="server" CssClass="textbox" Width="120px" Enabled="false" />
                    </td>
                    <td style="text-align: left" colspan="2">
                        L�nea<br />                       
                        <asp:TextBox ID="txtLinea" runat="server" CssClass="textbox" Width="90%" Enabled="false" />
                    </td>
                    <td style="text-align: left">
                        Saldo Total<br />
                        <asp:TextBox ID="txtSaldoTotal" runat="server"  Enabled="false" style="text-align: center"
                            Width="160px" ></asp:TextBox>
                    </td>
                    <td style="text-align: left">
                        Saldo Canje<br />
                        <asp:TextBox ID="txtSaldoCanje" runat="server" CssClass="textbox" Enabled="false" Style="text-align: right;" />
                        <asp:FilteredTextBoxExtender ID="fteSaldoCanje"
                            runat="server" Enabled="True" FilterType="Custom" TargetControlID="txtSaldoCanje"
                            ValidChars="0123456789,.-">
                        </asp:FilteredTextBoxExtender>
                    </td>
                    <td style="text-align: left">
                        Estado<br />
                        <asp:TextBox ID="txtEstado" runat="server" CssClass="textbox" Width="140px" Enabled="false" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left">
                        F. Apertura<br />
                        <ucFecha:fecha ID="txtFechaApertura" runat="server" CssClass="textbox" Enabled="false" />
                    </td>
                    <td style="text-align: left">
                        F. Ult Mov<br />
                        <ucFecha:fecha ID="txtFechaUltMov" runat="server" CssClass="textbox" Enabled="false" />
                    </td>
                    <td style="text-align: left">
                        F. Prox Pago<br />
                        <ucFecha:fecha ID="txtProxPago" runat="server" CssClass="textbox" Enabled="false" />
                    </td>
                    <td style="text-align: left">
                        Forma Pago<br />
                        <asp:TextBox ID="txtFormaPago" runat="server" CssClass="textbox" Width="170px" Enabled="false" />
                    </td>
                    <td style="text-align: left">
                        Periodicidad<br />
                        <asp:TextBox ID="txtPeriodicidad" runat="server" CssClass="textbox" Width="150px"
                            Enabled="false" />
                    </td>
                    <td style="text-align: left">
                        Cuota<br />
                        <uc1:decimales ID="txtCuota" runat="server" CssClass="textbox" Enabled="false" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left;" colspan="6">
                        <hr style="width: 100%" />
                        <strong>Datos del Titular</strong>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left">
                        Cod. Persona<br />
                        <asp:TextBox ID="txtCodPersona" runat="server" CssClass="textbox" Width="120px" Enabled="false" />
                    </td>
                    <td style="text-align: left">
                        Identificaci�n<br />
                        <asp:TextBox ID="txtIdentificacion" runat="server" CssClass="textbox" Width="140px"
                            Enabled="false" />
                    </td>
                    <td style="text-align: left" colspan="2">
                        Nombre<br />
                        <asp:TextBox ID="txtNombre" runat="server" CssClass="textbox" Width="90%" Enabled="false" />
                    </td>
                    <td style="text-align: left">
                        &nbsp;
                    </td>                    
                    <td style="text-align: left">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left;" colspan="6">
                        <hr style="width: 100%" />
                        <strong>Movimiento</strong>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left">
                        Fecha Inicial<br />
                        <ucFecha:fecha ID="ucFechaInicial" runat="server" CssClass="textbox" />
                    </td>
                    <td style="text-align: left">
                        Fecha Final<br />
                        <ucFecha:fecha ID="ucFechaFinal" runat="server" CssClass="textbox" />
                    </td>
                    <td style="text-align: left">
                        <asp:ImageButton ID="btnConsultar" runat="server" Visible="true"
                            ImageUrl="~/Images/btnConsultar.jpg" onclick="btnConsular_Click" />
                    </td>
                    <td style="text-align: left" colspan="3">
                        &nbsp;
                    </td>
                </tr>
            </table>
            <table style="width: 100%">
                <tr>
                    <td style="text-align: left">
                        <asp:Panel ID="panelGrilla" runat="server">
                            <asp:GridView ID="gvLista" runat="server" Width="100%" AutoGenerateColumns="False"
                                AllowPaging="True" OnPageIndexChanging="gvLista_PageIndexChanging" PageSize="20">
                                <Columns>
                                    <asp:BoundField DataField="fecha" HeaderText="Fecha" DataFormatString="{0:d}"><ItemStyle HorizontalAlign="center" /></asp:BoundField>
                                    <asp:BoundField DataField="cod_ope" HeaderText="Nro Operaci�n"><ItemStyle HorizontalAlign="left" /></asp:BoundField>
                                    <asp:BoundField DataField="tipo_ope" HeaderText="Tipo Operaci�n"><ItemStyle HorizontalAlign="left" /></asp:BoundField>
                                    <asp:BoundField DataField="tipo_tran" HeaderText="Transacci�n"><ItemStyle HorizontalAlign="left" /></asp:BoundField>
                                    <asp:BoundField DataField="num_comp" HeaderText="Num. Comp"><ItemStyle HorizontalAlign="left" /></asp:BoundField>
                                    <asp:BoundField DataField="tipo_comp" HeaderText="Tipo Comp"><ItemStyle HorizontalAlign="left" /></asp:BoundField>
                                    <asp:BoundField DataField="tipo_mov" HeaderText="Tipo Mov"><ItemStyle HorizontalAlign="left" /></asp:BoundField>
                                    <asp:BoundField DataField="valor" HeaderText="Valor" DataFormatString="{0:c}"><ItemStyle HorizontalAlign="Right" /></asp:BoundField>
                                    <asp:BoundField DataField="saldo" HeaderText="Saldo" DataFormatString="{0:c}"><ItemStyle HorizontalAlign="Right" /></asp:BoundField>
                                    <asp:BoundField DataField="soporte" HeaderText="Doc.Soporte"><ItemStyle HorizontalAlign="Right" /></asp:BoundField>
                                </Columns>
                                <HeaderStyle CssClass="gridHeader"></HeaderStyle>
                                <PagerStyle CssClass="gridPager"></PagerStyle>
                                <RowStyle CssClass="gridItem"></RowStyle>
                            </asp:GridView>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center">
                        <asp:Label ID="lblInfo" runat="server" Text="Su consulta no obtuvo ning�n resultado"
                            Visible="False" />
                    </td>
                </tr>
            </table>
        </asp:View>
        <asp:View ID="vwReporte" runat="server">
            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                <tr>
                    <td style="text-align:left">
                        <asp:Button ID="btnDatos" runat="server" CssClass="btn8" Height="28px" Width="120px" OnClick="btnDatos_Click"
                            Text="Visualizar Datos" />
                            &#160;&#160;&#160;&#160;&#160;
                            <asp:Button ID="btnImpresion" runat="server" CssClass="btn8" Height="28px" Width="120px" OnClick="btnImpresion_Click"
                            Text="Imprimir" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <iframe id="frmPrint" name="IframeName" width="100%" src="../../Reportes/Reporte.aspx" height="500px"
                            runat="server" style="border-style: groove; float: left;"></iframe>
                    </td>
                </tr>
                <tr>
                    <td>
                        <rsweb:reportviewer id="rvReportMov" runat="server" font-names="Verdana" font-size="8pt"
                            enabled="false" interactivedeviceinfos="(Colecci�n)" waitmessagefont-names="Verdana"
                            waitmessagefont-size="10pt" width="100%" Height="500px"><localreport reportpath="Page\AhorrosVista\ReporteMovimiento\rptReporteMov.rdlc"><datasources><rsweb:ReportDataSource /></datasources></localreport></rsweb:reportviewer>
                    </td>
                </tr>
            </table>
        </asp:View>
    </asp:MultiView>
</asp:Content>