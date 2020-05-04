<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" EnableEventValidation="true" CodeFile="Lista.aspx.cs" Inherits="Page_Asesores_Colocacion_Lista" Title=".: Xpinn - Asesores :." %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../../../General/Controles/ctlseleccionmultipledropdown.ascx" TagName="dropdownmultiple" TagPrefix="ucDrop" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:Panel ID="pConsulta" runat="server">
        <table style="width: 90%;">
            <tr>
                <td align="center">Fecha de corte<br />
                    <asp:DropDownList ID="ddlFechaCorte" runat="server" CssClass="dropdown"
                        Width="158px">
                    </asp:DropDownList>
                </td>
                <td style="text-align: left">Oficina:
                    <ucDrop:dropdownmultiple ID="ddloficina" runat="server" Height="24px" Width="120px"></ucDrop:dropdownmultiple>
                </td>
                <td style="text-align: left">Categoria:<br />
                    <ucDrop:dropdownmultiple ID="ddlCategoria" runat="server" Height="24px" Width="120px"></ucDrop:dropdownmultiple>
                </td>
                <td style="text-align: left">Linea de Crédito:<br />
                    <ucDrop:dropdownmultiple ID="ddlLinea" runat="server" Height="24px" Width="120px"></ucDrop:dropdownmultiple>
                </td>
            </tr>
        </table>
    </asp:Panel>

    <asp:MultiView ID="mvLista" runat="server">
        <asp:View ID="vGridReporteCierre" runat="server">
            <div style="overflow: scroll">
                <div style="width: 233%;">
                    <asp:Button ID="btnExportar" runat="server" CssClass="btn8"
                        OnClick="btnExportar_Click" Text="Exportar a excel" />
                    <br />
                    <asp:GridView ID="gvReportecierre" runat="server" AutoGenerateColumns="False"
                        DataKeyNames="NumRadicacion" HeaderStyle-CssClass="gridHeader"
                        PagerStyle-CssClass="gridPager" RowStyle-CssClass="gridItem" ShowFooter="True"
                        Style="font-size: x-small" Width="100%" GridLines="Horizontal"
                        OnRowDataBound="gvReportecierre_RowDataBound">
                        <Columns>
                            <asp:BoundField HeaderText="Oficina" DataField="oficina" />
                            <asp:BoundField DataField="icodigo" HeaderText="Código">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="identificacion" HeaderText="Identificación">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Apellidos" HeaderText="Apellidos Cliente">
                                <ItemStyle HorizontalAlign="left" Width="120px" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Nombres Cliente" DataField="Nombres">
                                <ItemStyle HorizontalAlign="left" Width="100px" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Ciudad" DataField="ciudad">
                                <ItemStyle HorizontalAlign="left" Width="50px" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Dirección" DataField="direccion">
                                <ItemStyle HorizontalAlign="left" Width="100px" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Telefóno" DataField="telefono">
                                <ItemStyle HorizontalAlign="left" Width="100px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="NumRadicacion" HeaderText="Número de Radicación">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Fecha_aprobacion" DataFormatString="{0:d}" HeaderText="Fecha Aprobación">
                                <ItemStyle HorizontalAlign="center" Width="90" />
                            </asp:BoundField>
                            <asp:BoundField DataField="cod_linea" HeaderText="Cod Linea Credito">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="linea" HeaderText="Linea Credito">
                                <ItemStyle HorizontalAlign="left" Width="90" />
                            </asp:BoundField>
                            <asp:BoundField DataField="monto_aprobado" DataFormatString="{0:C}" HeaderText="Monto">
                                <ItemStyle HorizontalAlign="left" Width="90" />
                            </asp:BoundField>
                            <asp:BoundField DataField="saldo" DataFormatString="{0:C}" HeaderText="Saldo Capital">
                                <ItemStyle ForeColor="#009999" HorizontalAlign="Right" Width="90" />
                            </asp:BoundField>
                            <asp:BoundField DataField="valor_cuota" DataFormatString="{0:C}" HeaderText="Valor Cuota">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Fecha_vencimiento" HeaderText="Fecha Vencimiento" DataFormatString="{0:d}">
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="dias_mora" HeaderText="Dias Mora">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>

                            <asp:BoundField DataField="ValorCapital" DataFormatString="{0:C}" HeaderText="Valor Capital">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ValorInteres" DataFormatString="{0:C}" HeaderText="Interes Corriente">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ValorMora" DataFormatString="{0:C}" HeaderText="Saldo Mora">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ValorOtros" DataFormatString="{0:C}" HeaderText="Valor Otros">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>

                            <asp:BoundField DataField="valor_a_pagar" DataFormatString="{0:C}" HeaderText="Vr. a Pagar">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Fecha_ult_pago" HeaderText="Fecha Ultimo Pago" DataFormatString="{0:d}"></asp:BoundField>
                            <asp:BoundField DataField="codigo_asesor" HeaderText="Codigo Asesor"></asp:BoundField>
                            <asp:BoundField DataField="nombre_asesor" HeaderText="Asesor">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="plazo" HeaderText="Plazo">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="cuotas_pagadas" HeaderText="Cuotas Pagadas"></asp:BoundField>
                            <asp:BoundField HeaderText="Periodicidad" DataField="periodicidad">
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Categoria Crédito" DataField="cod_categoria" HeaderStyle-Width="100px">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Categoria Cliente" DataField="cod_categoria_cli" HeaderStyle-Width="100px">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="tasa_interes" HeaderText="Tasa Int." DataFormatString="{0:N2}">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="forma_pago" HeaderText="Forma Pago">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="empresa_recaudo" HeaderText="Empresa Recaudo">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                        </Columns>
                        <HeaderStyle CssClass="gridHeader" />
                        <PagerStyle CssClass="gridPager" />
                        <RowStyle CssClass="gridItem" />
                    </asp:GridView>
                </div>
            </div>
            <asp:Label ID="lblTotalRegs" runat="server" />
            <br />
            <br />
            &nbsp;                                                            
        </asp:View>
    </asp:MultiView>

</asp:Content>

