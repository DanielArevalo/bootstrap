<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Lista.aspx.cs" Inherits="garantiascomunitarias" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/imprimir.ascx" TagName="imprimir" TagPrefix="ucImprimir" %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div id="gvDiv">
        <table style="width: 100%">
            <tr>
                <td colspan="3">
                    <asp:Label ID="lblInformacion" runat="server" Text="Seleccione un Rango de Fechas" Visible="false"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="text-align: left" colspan="2">
                    <asp:Label ID="lblFechaInicial" runat="server" Text="Fecha Inicial"></asp:Label><br />
                    <ucFecha:fecha ID="ucFecha" runat="server" />
                </td>
                <td style="text-align: left" colspan="2">
                    <asp:Label ID="lblFechaFinal" runat="server" Text="Fecha Final"></asp:Label><br />
                    <ucFecha:fecha ID="ucFecha0" runat="server" />
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td style="text-align: left">
                    Oficina<br />
                    <asp:DropDownList ID="ddlOficina" runat="server" AutoPostBack="True" CssClass="dropdown">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        <table width="100%">
            <tr>
                <td style="width: 308px; text-align:left">
                    <asp:Button ID="Button1" runat="server" CssClass="btn8" OnClick="btnExportarExcel_Click"
                        Text="Exportar a Excel" Width="124px" Height="28px" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <hr />
                </td>
            </tr>
        </table>
        <table style="width: 100%">
            <tr>
                <td>
                    <div style="overflow:scroll;height:500px;width:700px;">
                        <div style="width: 100%;">
                            <asp:GridView ID="gvMovGeneral" runat="server" Width="100%" PageSize="3"
                                GridLines="Horizontal" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False"
                                OnPageIndexChanging="gvMovGeneral_PageIndexChanging" SelectedRowStyle-Font-Size="XX-Small"
                                Style="font-size: small; margin-bottom: 0px;">
                                <Columns>
                                    <asp:BoundField DataField="IDENTIFICACION" HeaderText="Crédito/Nit" />
                                    <asp:BoundField DataField="NUMERO_RADICACION" HeaderText="Número de Pagaré" />
                                    <asp:BoundField DataField="VALOR_PAGADO" HeaderText="Valor Pagado" 
                                        DataFormatString="{0:c}" />
                                    <asp:BoundField DataField="FECHA_DESEMBOLSO" HeaderText="Fecha de Pago" />
                                </Columns>
                                <HeaderStyle CssClass="gridHeader" />
                                <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                                <RowStyle CssClass="gridItem" />
                                <SelectedRowStyle Font-Size="XX-Small"></SelectedRowStyle>
                            </asp:GridView>
                        </div>
                    </div>
                    <asp:Label ID="lblTotalReg" runat="server" Visible="False" />
                    <asp:Label ID="lblInfo" runat="server" Text="Su consulta no obtuvo ningun resultado."
                        Visible="False" />                                                      
                </td>
            </tr>
            <tr>
                <td>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
