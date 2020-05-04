<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" Title=".: Xpinn - Usuario :." %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../../../General/Controles/fecha.ascx" TagName="fecha" TagPrefix="uc" %>
<%@ Register Src="../../../General/Controles/ctlMoneda.ascx" TagName="ddlMoneda" TagPrefix="ctl" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <script type="text/javascript" src="../../../Scripts/gridviewScroll.min.js"></script>

    <script type="text/javascript">

        function CalcularAncho() {
            if (navigator.platform == 'Win32') {
                return screen.width - 300;
            }
            else {
                return 1000;
            }
        }

    </script>

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <asp:MultiView ID="mvBalance" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwDatos" runat="server">
            <asp:Panel ID="pConsulta" runat="server">
                <table id="tbCriterios" border="0" cellpadding="5" cellspacing="0" width="70%">
                    <tr>
                        <td class="tdI" style="text-align: left">Fecha de Cierre</td>
                    </tr>
                    <tr>
                        <td class="tdI" style="text-align: left">
                            <asp:DropDownList ID="ddlFechaCorte" runat="server" CssClass="dropdown"
                                Width="158px">
                            </asp:DropDownList>
                        </td>
                        <td style="text-align: left">
                            <asp:CheckBox ID="chkincluyeRetirados" runat="server" Text="Incluir retirados" TextAlign="Right"
                                Width="158px">
                            </asp:CheckBox>
                        </td>
                    </tr>
                </table>
            </asp:Panel>

            <asp:Panel ID="Panel1" runat="server">
                <table border="0" cellpadding="0" cellspacing="0" style="width: 95%">
                    <tr>
                        <td class="tdI" colspan="4">
                            <asp:Button ID="btnInforme" runat="server" CssClass="btn8"
                                OnClick="btnInforme_Click" OnClientClick="btnInforme_Click"
                                Text="Visualizar informe" />
                            &nbsp;<asp:Button ID="btnExportar" runat="server" CssClass="btn8"
                                OnClick="btnExportar_Click" Text="Exportar a Excel" />
                            <br />
                        </td>
                        <td class="tdD">&nbsp;</td>
                        <td class="tdD">&nbsp;</td>
                    </tr>
                </table>
            </asp:Panel>
            <table border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        <hr style="width: 95%; text-align: left" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <div id="divgrid" style="overflow: scroll; height: 400px; width: 1000px; margin-right: 0px;" visible="false" runat="server">
                            <asp:GridView ID="gvLista" runat="server"
                                AutoGenerateColumns="False" OnRowDataBound="gvLista_RowDataBound"
                                OnPageIndexChanging="gvLista_PageIndexChanging" PageSize="20"
                                HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager"
                                RowStyle-CssClass="gridItem" Style="font-size: x-small" Width="970px">
                                <Columns>
                                    <asp:BoundField DataField="cod_nomina" HeaderText="CODIGO NOMINA">
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="identificacion" HeaderText="IDENTIFICACION">
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="nombre" HeaderText="APELLIDOS Y NOMBRES">
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="fecha_apertura" HeaderText="FECHA NACIMIENTO" DataFormatString="{0:d}">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="direccion" HeaderText="DIRECCION">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="fecha_crea" HeaderText="FECHA INGRESO" DataFormatString="{0:d}">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="otros" HeaderText="AHORRO PERMANENTE" DataFormatString="{0:c}">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="valor_acumulado" HeaderText="APORTE SOCIAL" DataFormatString="{0:c}">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="total" HeaderText="TOTAL AHORROS Y APORTES" DataFormatString="{0:c}">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                       <asp:BoundField DataField="cartera" HeaderText="TOTAL CARTERA" DataFormatString="{0:c}">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                   <asp:BoundField DataField="estado_modificacion" HeaderText="ESTADO">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="fecha_ultima_mod" HeaderText="FECHA RETIRO" DataFormatString="{0:d}">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>

                                    <asp:BoundField DataField="telefono" HeaderText="TELEFONO">
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>

                                    <asp:BoundField DataField ="celular" HeaderText="CELULAR">
                                        <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>

                                    <asp:BoundField DataField ="sexo" HeaderText="GENERO DEL ASOCIADO">
                                        <ItemStyle HorizontalAlign="center" />
                                        </asp:BoundField>

                                    <asp:BoundField DataField ="cod_empresa" HeaderText="CODIGO DE LA EMPRESA">
                                        <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>

                                    <asp:BoundField DataField ="nom_empresa" HeaderText="NOMBRE DE LA EMPRESA">
                                        <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>

                                </Columns>

                                <HeaderStyle CssClass="gridHeader" />
                                <PagerStyle CssClass="gridPager" />
                                <RowStyle CssClass="gridItem" />
                            </asp:GridView>

                            &nbsp;
                        </div>
                        <asp:Label ID="lblTotalRegs" runat="server" Visible="true" />
                    </td>
                </tr>              
            </table>
        </asp:View>

        <asp:View ID="vwReporte" runat="server">
            <br />
            <br />
            <table width="100%">
                <tr>
                    <td style="text-align: left">
                        <asp:Button ID="btnDatos" runat="server" CssClass="btn8" Height="25px" Width="130px" OnClick="btnDatos_Click"
                            Text="Visualizar Datos" />
                        &nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnImprime" runat="server" CssClass="btn8" Height="25px" Width="130px"
                            Text="Imprimir" OnClick="btnImprime_Click" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <iframe id="frmPrint" name="IframeName" width="100%" src="../../LibroSocios/RptLibroSocios.rdlc"
                            height="600px" runat="server" style="border-style: groove; float: left;"></iframe>
                    </td>
                </tr>
                <tr>
                    <td>
                        <rsweb:ReportViewer ID="RptReporte" runat="server" Width="100%" Height="600px" InteractiveDeviceInfos="(Colección)">
                            <LocalReport ReportPath="Page\Reporteador\LibroSocios\RptLibroSocios.rdlc">
                            </LocalReport>
                        </rsweb:ReportViewer>
                    </td>
                </tr>
            </table>
        </asp:View>
    </asp:MultiView>

</asp:Content>