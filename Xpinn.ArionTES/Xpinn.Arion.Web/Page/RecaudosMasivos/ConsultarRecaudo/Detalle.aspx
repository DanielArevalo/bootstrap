<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Detalle.aspx.cs" Inherits="Detalle" %>

<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <script type="text/javascript" src="../../../Scripts/gridviewScroll.min.js"></script>
    <script type="text/javascript">

        $(document).ready(function () {
            gridviewScroll();
        });

        function gridviewScroll() {
            $('#<%=gvLista.ClientID%>').gridviewScroll({
                width: <%=ancho%>,
                height: 500,
                freezesize: 3,
                arrowsize: 30,
                varrowtopimg: "../../../Images/arrowvt.png",
                varrowbottomimg: "../../../Images/arrowvb.png",
                harrowleftimg: "../../../Images/arrowhl.png",
                harrowrightimg: "../../../Images/arrowhr.png"
            });
        } 


    </script>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:MultiView ID="mvAplicar" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwLista" runat="server">
            <table style="width: 95%">
                <tr>
                    <td style="text-align: left">
                        Empresa Recaudadora<br />
                        <asp:DropDownList ID="ddlEntidad" runat="server" CssClass="dropdown" Width="280px"
                            Enabled="False">
                        </asp:DropDownList>
                    </td>
                    <td style="text-align: left">
                        <asp:Label ID="lblFechaAplica" runat="server" Visible="True" Text="Fecha de Aplicacion" /><br />
                        <ucFecha:fecha ID="ucFechaAplicacion" runat="server" Enabled="False" Requerido="False" />
                    </td>
                    <td style="text-align: left">
                        <asp:Label ID="lblNumeroLista" runat="server" Visible="True" Text="Núm. Aplicación" /><br />
                        <asp:TextBox ID="txtNumeroLista" runat="server" Enabled="false" Width="130px"></asp:TextBox>
                    </td>
                    <td style="text-align: left">
                        <asp:Label ID="lblNumeroNovedad" runat="server" Visible="True" Text="Número de Novedad" /><br />
                        <asp:TextBox ID="txtNumeroNovedad" runat="server" Enabled="false" Width="130px"></asp:TextBox>
                    </td>                    
                    <td style="text-align: left">
                        <asp:Label ID="lblPeriodo" runat="server" Visible="True" Text="Período" /><br />
                        <ucFecha:fecha ID="txtPeriodo" runat="server" Enabled="False" Requerido="False" Width="70px" />
                    </td>
                    <td>
                        <br />
                        <asp:CheckBox ID="cbDetallado" runat="server" Checked="false" Text="Detallado" 
                            AutoPostBack="True" oncheckedchanged="cbDetallado_CheckedChanged" />
                    </td>
                    <td>
                        &nbsp;&nbsp;
                    </td>
                </tr>
            </table>
            <table style="width: 100%">
                <tr>
                    <td colspan="2">
                        <asp:Button ID="btnExportar" runat="server" CssClass="btn8" OnClick="btnExportar_Click"
                            Text="Exportar a Excel" />&nbsp;
                        <asp:Button ID="btnConciliacion" runat="server" CssClass="btn8" OnClick="btnConciliacion_Click"
                            Text=" Conciliación " />&nbsp;
                        <asp:Button ID="btnReporte" runat="server" CssClass="btn8" OnClick="btnReporte_Click"
                            Text="   Reporte   " />&nbsp;
                        <asp:Button ID="btnRptConsolidado" runat="server" CssClass="btn8" OnClick="btnRptConsolidado_Click"
                            Text=" Reporte Consolidado " />
                        <br />
                        <asp:GridView ID="gvLista" runat="server" Width="100%" PageSize="3" ShowHeaderWhenEmpty="True"
                            AutoGenerateColumns="False" SelectedRowStyle-Font-Size="XX-Small" Style="font-size: small;
                            margin-bottom: 0px;">
                            <Columns>
                                <asp:BoundField DataField="identificacion" HeaderText="Cedula/Nit">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="nombre" HeaderText="Nombres">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="tipo_producto" HeaderText="Tipo de Producto" />
                                <asp:BoundField DataField="numero_producto" HeaderText="Número de Producto" />
                                <asp:BoundField DataField="tipo_aplicacion" HeaderText="Tipo Aplicacion" />
                                <asp:BoundField DataField="num_cuotas" HeaderText="Num.Cuotas" />
                                <asp:BoundField DataField="valor" HeaderText="Valor" DataFormatString="{0:N}">
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="capital" HeaderText="Capital" DataFormatString="{0:N}">
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="intcte" HeaderText="Int.Cte" DataFormatString="{0:N}">
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="intmora" HeaderText="Int.Mora" DataFormatString="{0:N}">
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="seguro" HeaderText="Seguro" DataFormatString="{0:N}">
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="leymipyme" HeaderText="Ley MiPyme" DataFormatString="{0:N}">
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="ivaleymipyme" HeaderText="Iva Ley MiPyme" DataFormatString="{0:N}">
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="otros" HeaderText="Otros/Ahorro" DataFormatString="{0:N}">
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="devolucion" HeaderText="Devolucion" DataFormatString="{0:N}">
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                            </Columns>
                            <HeaderStyle CssClass="gridHeader" />
                            <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                            <RowStyle CssClass="gridItem" />
                            <SelectedRowStyle Font-Size="XX-Small"></SelectedRowStyle>
                        </asp:GridView>                        
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right; padding-right: 5px">Total Recaudo :&nbsp;<b><asp:Label ID="lblTotalRecaudo" runat="server" /></b>
                    </td>
                    <td style="text-align: left; padding-left: 5px">
                        <asp:Label ID="lblTotalReg" runat="server" Visible="False" />
                    </td>
                </tr>
            </table>
        </asp:View>
        <asp:View ID="vwReporte" runat="server">
            <asp:Button ID="btnDatos" runat="server" CssClass="btn8" Height="28px" OnClick="btnDatos_Click"
                Text="Visualizar Datos" />
            <br />
            <br />
            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                <tr>
                    <td>
                        <rsweb:ReportViewer ID="rvConsultaRecaudo" runat="server" Font-Names="Verdana" Font-Size="8pt"
                            Enabled="false" InteractiveDeviceInfos="(Colección)" WaitMessageFont-Names="Verdana"
                            WaitMessageFont-Size="10pt" Width="100%" Visible="False">
                            <LocalReport ReportPath="Page\RecaudosMasivos\ConsultarRecaudo\rptConsultaRecaudo.rdlc">
                                <DataSources>
                                    <rsweb:ReportDataSource />
                                </DataSources>
                            </LocalReport>
                        </rsweb:ReportViewer>
                    </td>
                </tr>
                <tr>
                    <td>
                        <rsweb:ReportViewer ID="rvReporte" runat="server" Font-Names="Verdana" Font-Size="8pt"
                            Enabled="false" InteractiveDeviceInfos="(Colección)" WaitMessageFont-Names="Verdana"
                            WaitMessageFont-Size="10pt" Width="100%" Visible="False">
                            <LocalReport ReportPath="Page\RecaudosMasivos\ConsultarRecaudo\rptReporte.rdlc">
                                <DataSources>
                                    <rsweb:ReportDataSource />
                                </DataSources>
                            </LocalReport>
                        </rsweb:ReportViewer>
                    </td>
                </tr>
                <tr>
                    <td>
                        <rsweb:ReportViewer ID="rvConsolidado" runat="server" Font-Names="Verdana" Font-Size="8pt"
                            Enabled="false" InteractiveDeviceInfos="(Colección)" WaitMessageFont-Names="Verdana"
                            WaitMessageFont-Size="10pt" Width="100%" Visible="False">
                            <LocalReport ReportPath="Page\RecaudosMasivos\ConsultarRecaudo\rptConsolidadoProductoAtributo.rdlc">
                                <DataSources>
                                    <rsweb:ReportDataSource />
                                </DataSources>
                            </LocalReport>
                        </rsweb:ReportViewer>
                    </td>
                </tr>
            </table>
        </asp:View>
    </asp:MultiView>
    <asp:Label ID="msg" runat="server" Font-Bold="true" ForeColor="Red" />
</asp:Content>
