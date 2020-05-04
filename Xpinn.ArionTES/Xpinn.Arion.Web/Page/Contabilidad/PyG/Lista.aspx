<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Lista.aspx.cs" Inherits="Lista" Title=".: Xpinn - Usuario :." %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../../../General/Controles/fecha.ascx" TagName="fecha" TagPrefix="uc" %>
<%@ Register Src="../../../General/Controles/ctlMoneda.ascx" TagName="ddlMoneda"
    TagPrefix="ctl" %>
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
                width: CalcularAncho(),
                height: 500,
                freezesize: 3,
                arrowsize: 30,
                varrowtopimg: "../../../Images/arrowvt.png",
                varrowbottomimg: "../../../Images/arrowvb.png",
                harrowleftimg: "../../../Images/arrowhl.png",
                harrowrightimg: "../../../Images/arrowhr.png"
            });
        }

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
            <br />
            <asp:Panel ID="pConsulta" runat="server">
                <table id="tbCriterios1" border="0" cellpadding="0" cellspacing="0" width="50%">
                    <tr>
                        <td class="tdI" style="text-align: left">
                            Fecha de corte
                        </td>
                        <td class="tdD" style="text-align: left">
                            <asp:Label ID="lblCentroCosto" runat="server" Text="Centro Costo"></asp:Label>
                        </td>
                        <td class="tdD" style="text-align: left">
                            Nivel&nbsp;
                        </td>
                        <td class="tdD" style="text-align: left" colspan="2">
                            <asp:Label ID="lblMoneda0" runat="server" Text="Moneda"></asp:Label>
                        </td>
                        <td class="tdD" style="text-align: left">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="tdI" style="text-align: left">
                            <asp:DropDownList ID="ddlFechaCorte" Height="30px" runat="server" CssClass="dropdown" Width="158px">
                            </asp:DropDownList>
                        </td>
                        <td class="tdD" style="text-align: left">
                            <asp:DropDownList ID="ddlcentrocosto" Height="30px" runat="server" CssClass="dropdown" Width="168px" />
                        </td>
                        <td class="tdD" style="text-align: left">
                            <asp:DropDownList ID="ddlNivel" runat="server" Height="30px" CssClass="dropdown" Width="80px">
                                <asp:ListItem>3</asp:ListItem>
                                <asp:ListItem>4</asp:ListItem>
                                <asp:ListItem>5</asp:ListItem>
                                <asp:ListItem>6</asp:ListItem>
                                <asp:ListItem>7</asp:ListItem>
                                <asp:ListItem>8</asp:ListItem>
                                <asp:ListItem>9</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td class="tdD" style="text-align: left" colspan="2">
                            <ctl:ddlMoneda ID="ddlMoneda" runat="server" CssClass="dropdown" Width="151px" />
                        </td>
                        <td class="tdD" style="text-align: left">
                            &nbsp;
                        </td>
                    </tr>
                </table>
                <table id="tbCriterios2" border="0" cellpadding="0" cellspacing="0" width="50%">
                    <tr>
                        <td class="tdI" style="text-align: left">
                            <asp:CheckBox ID="chkCuentasCero" runat="server" Text="Mostrar Cuentas En Cero"
                            Style="font-size: x-small" />
                        </td>
                        <td class="tdD" style="text-align: left">
                            <asp:CheckBox ID="chkPeriodo" runat="server" Text="Saldos del Período"
                                Style="font-size: x-small" />
                        </td>
                        <td class="tdD" style="text-align: left">
                            <asp:CheckBox ID="chkCierreAnual" runat="server" Text="Cierre Anual" Style="font-size: x-small" />
                        </td>
                        <td class="tdD" style="text-align: left">
                            <asp:CheckBox ID="chkCompCentroCosto" runat="server"
                                Style="font-size: x-small" Text="Comparativo Centro De Costo" />
                        </td>
                        <td class="tdD" style="text-align: left">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left" colspan="5">
                            <asp:Button ID="btnInforme" runat="server" CssClass="btn8" Height="23px" Width="130px" OnClick="btnInforme_Click"
                                OnClientClick="btnInforme_Click" Text="Visualizar informe" />
                            &nbsp;<asp:Button ID="btnExportar" runat="server" CssClass="btn8" Height="23px" Width="130px" OnClick="btnExportar_Click"
                                Text="Exportar a Excel" />
                            <br />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <table border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        <hr style="width: 100%; text-align: left" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:GridView ID="gvLista" runat="server" AutoGenerateColumns="False" AllowPaging="False"
                            OnPageIndexChanging="gvLista_PageIndexChanging" PageSize="20" HeaderStyle-CssClass="gridHeader"
                            PagerStyle-CssClass="gridPager" RowStyle-CssClass="gridItem" Width="70%">
                            <Columns>
                                <asp:BoundField DataField="cod_cuenta" HeaderText="Cod.Cuenta">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="nombrecuenta" HeaderText="Nombre Cuenta">
                                    <ItemStyle HorizontalAlign="Left" Width="400px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="valor" HeaderText="Valor" DataFormatString="{0:c}">
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                            </Columns>
                            <HeaderStyle CssClass="gridHeader" />
                            <PagerStyle CssClass="gridPager" />
                            <RowStyle CssClass="gridItem" />
                        </asp:GridView>
                        <asp:Label ID="lblTotalRegs" runat="server" Visible="False" />
                        &nbsp;
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
                        <asp:Button ID="btnDatos" runat="server" CssClass="btn8" Height="23px" Width="130px" OnClick="btnDatos_Click"
                            Text="Visualizar Datos" />&#160;&#160;&#160;&#160;
                        <asp:Button ID="btnImprime" runat="server" CssClass="btn8" Height="23px" Width="130px"
                            Text="Imprimir" OnClick="btnImprime_Click" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left">
                        <iframe id="frmPrint" name="IframeName" width="100%" src="../../Reportes/Reporte.aspx"
                            height="550px" runat="server" style="border-style: dotted; float: left;"></iframe>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left">
                        <rsweb:ReportViewer ID="RptReporte" runat="server" Width="100%" Height="500px">
                            <LocalReport ReportPath="Page\Contabilidad\PyG\RptPyG.rdlc">
                            </LocalReport>
                        </rsweb:ReportViewer>
                    </td>
                </tr>
            </table>            
        </asp:View>
    </asp:MultiView>
</asp:Content>
