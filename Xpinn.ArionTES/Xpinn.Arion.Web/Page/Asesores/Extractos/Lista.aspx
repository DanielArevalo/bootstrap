<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Lista.aspx.cs" Inherits="Page_Asesores_Colocacion_Lista" Title=".: Xpinn - Asesores :." %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Assembly="BarcodeX" Namespace="Fath" TagPrefix="bcx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphMain">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:MultiView ID="mvPrincipal" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwDatos" runat="server">
        <asp:Panel ID="pConsulta" runat="server">
            <table style="width: 80%;">
                <tr>
                    <td colspan="3" style="text-align: left">
                        <strong>Criterios de Búsqueda</strong>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left">
                        Fecha Corte
                        <br />
                        <ucFecha:fecha ID="txtfechaCorte" runat="server" />
                    </td>
                    <td style="text-align: left">
                        Fecha Pago
                        <br />
                        <ucFecha:fecha ID="txtfechaPago" runat="server" />
                    </td>
                    <td style="width: 50%;text-align: left" colspan="2">
                        <asp:CheckBox ID="chkActivarDetalle" runat="server" Text="Mostrar Detalle de Pago"
                            TextAlign="Right" /><br />
                        <ucFecha:fecha ID="txtFechaDetaIni" runat="server" />
                        a
                        <ucFecha:fecha ID="txtFechaDetaFin" runat="server" />
                        <br />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left;" colspan="2">
                        Código<br />
                        <asp:TextBox ID="txtCodigoDesde" runat="server" CssClass="textbox" Width="130px" />
                        <asp:FilteredTextBoxExtender ID="ftb1" runat="server" Enabled="True" FilterType="Numbers, Custom"
                    TargetControlID="txtCodigoDesde" ValidChars="" />
                    &nbsp;a&nbsp;
                        <asp:TextBox ID="txtCodigoHasta" runat="server" CssClass="textbox" Width="130px" />
                        <asp:FilteredTextBoxExtender ID="ftb2" runat="server" Enabled="True" FilterType="Numbers, Custom"
                    TargetControlID="txtCodigoHasta" ValidChars="" />
                    </td>
                    <td style="text-align: left;" colspan="2">
                        Identificación<br />
                        <asp:TextBox ID="txtIdentDesde" runat="server" CssClass="textbox" Width="130px" />&nbsp;a&nbsp;
                        <asp:TextBox ID="txtIdentHasta" runat="server" CssClass="textbox" Width="130px" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left;" colspan="2">
                        Nombres<br />
                        <asp:TextBox ID="txtNombres" runat="server" CssClass="textbox" Width="88%" />
                    </td>
                    <td style="text-align: left;" colspan="2">
                        Apellidos<br />
                        <asp:TextBox ID="txtApellidos" runat="server" CssClass="textbox" Width="88%" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left;" colspan="2">
                        Empresa<br />
                        <asp:DropDownList ID="ddlEmpresa" runat="server" CssClass="textbox" Width="90%" />
                    </td>
                    <td style="text-align: left;" colspan="2">
                        Ciudad de Residencia<br />
                        <asp:DropDownList ID="ddlCiudad" runat="server" CssClass="textbox" Width="90%" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left;" colspan="2">
                        Número de Aporte<br />
                        <asp:TextBox ID="txtNumAporteIni" runat="server" CssClass="textbox" Width="120px" />
                        <asp:FilteredTextBoxExtender ID="fte3" runat="server" Enabled="True" FilterType="Numbers, Custom"
                    TargetControlID="txtNumAporteIni" ValidChars="" />&nbsp;a&nbsp;
                        <asp:TextBox ID="txtNumAporteFin" runat="server" CssClass="textbox" Width="120px" />
                        <asp:FilteredTextBoxExtender ID="fte4" runat="server" Enabled="True" FilterType="Numbers, Custom"
                    TargetControlID="txtNumAporteFin" ValidChars="" />
                    </td>
                    <td style="text-align: left;" colspan="2">
                        Número de Créditos<br />
                        <asp:TextBox ID="txtNumCredIni" runat="server" CssClass="textbox" Width="120px" />
                        <asp:FilteredTextBoxExtender ID="fte5" runat="server" Enabled="True" FilterType="Numbers, Custom"
                    TargetControlID="txtNumCredIni" ValidChars="" />&nbsp;a&nbsp;
                        <asp:TextBox ID="txtNumCredFin" runat="server" CssClass="textbox" Width="120px" />
                        <asp:FilteredTextBoxExtender ID="fte6" runat="server" Enabled="True" FilterType="Numbers, Custom"
                    TargetControlID="txtNumCredFin" ValidChars="" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left" colspan="2">
                        Fecha Vencimiento de Aporte
                        <br />
                        <ucFecha:fecha ID="txtFecVencAporteIni" runat="server" />
                        &nbsp;a&nbsp;<ucFecha:fecha ID="txtFecVencAporteFin" runat="server" />
                    </td>
                    <td style="text-align: left" colspan="2">
                        Fecha Vencimiento de Crédito
                        <br />
                        <ucFecha:fecha ID="txtFecVencCredIni" runat="server" />
                        &nbsp;a&nbsp;<ucFecha:fecha ID="txtFecVencCredFin" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td colspan="4" style="text-align: left">
                        Observaciones para el Extracto<br />
                        <asp:TextBox ID="txtObservaciones" runat="server" Height="36px" MaxLength="500" TextMode="MultiLine"
                            Width="80%"></asp:TextBox>
                    </td>
                </tr>
            </table>
         </asp:Panel>
        </asp:View>
        <asp:View ID="vwListado" runat="server">
            <table width="100%">
                <tr>
                    <td style="text-align: left">
                        <asp:UpdatePanel ID="UdpGrilla" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                            <ContentTemplate>
                            <strong>Lista de Clientes a generar Extracto:</strong>
                                <asp:GridView ID="gvListaClientesExtracto" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                    GridLines="Horizontal" HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager"
                                    RowStyle-CssClass="gridItem" Width="100%" Height="16px" RowStyle-Font-Size="Small"
                                    OnPageIndexChanging="gvListaClientesExtracto_PageIndexChanging">
                                    <Columns>
                                        <asp:TemplateField HeaderStyle-CssClass="gridIco" ItemStyle-CssClass="gridIco">
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="cbSeleccionarEncabezado" runat="server" Checked="false" OnCheckedChanged="cbSeleccionarEncabezado_CheckedChanged"
                                                    AutoPostBack="True"/></HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="cbSeleccionar" runat="server" Checked="true" /></ItemTemplate>
                                            <HeaderStyle CssClass="gridIco"></HeaderStyle>
                                            <ItemStyle CssClass="gridIco"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="cod_persona" HeaderText="Código" />                                        
                                        <asp:BoundField DataField="identificacion" HeaderText="Identificación" />
                                        <asp:BoundField DataField="tipo_persona" HeaderText="Tipo Persona" />
                                        <asp:BoundField DataField="nombres" HeaderText="Nombres" />
                                        <asp:BoundField DataField="apellidos" HeaderText="Apellidos" />
                                        <asp:BoundField DataField="nomciudad" HeaderText="Ciudad" />
                                        <asp:BoundField DataField="direccion" HeaderText="Dirección" />
                                        <asp:BoundField DataField="telefono" HeaderText="Teléfono" />
                                        <asp:BoundField DataField="email" HeaderText="E-Mail" />
                                        <asp:BoundField DataField="fechaProx_pago_Aporte" HeaderText="Fec Prox Pago Aportes" 
                                            DataFormatString="{0:d}" />
                                        <asp:BoundField DataField="vr_totalPagar_aporte" HeaderText="Vr. Pagar Aportes" 
                                            DataFormatString="{0:n}" />
                                        <asp:BoundField DataField="fechaProx_pago_Credito" HeaderText="Fec Prox Pago Créditos" 
                                            DataFormatString="{0:d}" />
                                        <asp:BoundField DataField="vr_totalPagar_credito" HeaderText="Vr. Pagar Créditos" 
                                            DataFormatString="{0:n}" />
                                    </Columns>
                                    <HeaderStyle CssClass="gridHeader"/>
                                    <PagerStyle CssClass="gridPager" />
                                    <RowStyle CssClass="gridItem" />
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <asp:ObjectDataSource ID="ClienteExtracto" runat="server"></asp:ObjectDataSource>
                   </td>
                </tr>
                <tr>
                <td style="text-align: center">
                <asp:Label ID="lblTotalRegs" runat="server" />
                </td>
                </tr>              
            </table>
        </asp:View>
        <asp:View ID="vReporteExtracto" runat="server">
            <table>
                <tr>
                    <td style="width: 100%">
                       <br /><br />
                        <rsweb:ReportViewer ID="rvExtracto" runat="server" Width="100%" Font-Names="Verdana"
                            Font-Size="8pt" InteractiveDeviceInfos="(Colección)" WaitMessageFont-Names="Verdana"
                            WaitMessageFont-Size="14pt" Height="500px">
                            <LocalReport ReportPath="Page\Asesores\Extractos\rptExtracto.rdlc" EnableExternalImages="True">
                            </LocalReport>
                        </rsweb:ReportViewer>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                </tr>
            </table>
        </asp:View>
    </asp:MultiView>
    <asp:HiddenField ID="hdFileName" runat="server" />
    <asp:HiddenField ID="hdFileNameThumb" runat="server" />
    <%--<asp:ButtonField CommandName="Seleccionar" Text="Seleccionar" />--%>
    <br />
</asp:Content>
