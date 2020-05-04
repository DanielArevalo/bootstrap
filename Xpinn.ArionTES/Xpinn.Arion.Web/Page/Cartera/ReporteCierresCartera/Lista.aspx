<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" EnableEventValidation="true" CodeFile="Lista.aspx.cs" Inherits="Page_Asesores_Colocacion_Lista" Title=".: Xpinn - Asesores :." %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../../../General/Controles/ctlseleccionmultipledropdown.ascx" TagName="dropdownmultiple" TagPrefix="ucDrop" %>
<%@ Register Src="~/General/Controles/ctlFechaCierre.ascx" TagName="ddlCierreFecha" TagPrefix="ctl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:Panel ID="pConsulta" runat="server">
        <table style="width: 90%;">
            <tr>
                <td align="center">Consultar<br />
                    <asp:DropDownList ID="ddlConsultar" runat="server" AutoPostBack="True" CssClass="textbox" Width="200px" OnSelectedIndexChanged="ddlConsultar_SelectedIndexChanged">
                        <asp:ListItem Value="0">&lt;Seleccione un Item&gt;</asp:ListItem>
                        <asp:ListItem Value="R">CIERRE DE CARTERA</asp:ListItem>
                        <asp:ListItem Value="U">CAUSACION DE CARTERA</asp:ListItem>
                        <asp:ListItem Value="S">PROVISION DE CARTERA</asp:ListItem>
                    </asp:DropDownList>
                    <br />
                </td>
                <td style="text-align:center">Fecha Cierre:<br />
                    <ctl:ddlCierreFecha ID="ddlCierreFecha" runat="server" Requerido="True" />
                </td>
                <td style="text-align: left">Oficina:<br />
                    <ucDrop:dropdownmultiple ID="ddloficina" runat="server" Height="24px" Width="120px"></ucDrop:dropdownmultiple>
                </td>
                <td style="text-align: left">Categoria:<br />
                    <ucDrop:dropdownmultiple ID="ddlCategoria" runat="server" Height="24px" Width="120px" cssClass="textbox"></ucDrop:dropdownmultiple>
                </td>
                <td style="text-align: left">Linea de Crédito:<br />
                    <ucDrop:dropdownmultiple ID="ddlLinea" runat="server" Height="24px" Width="120px" cssClass="textbox"></ucDrop:dropdownmultiple>
                </td>
                <td style="text-align: left">
                    <asp:Label ID="lblAtributos" runat="server" Text="Atributos Credito:" /><br />
                    <ucDrop:dropdownmultiple ID="ddlAtributos" Visible="false" runat="server" Height="24px" Width="120px" cssClass="textbox"></ucDrop:dropdownmultiple>
                </td>
                <td style="text-align: left">Identificación:<br />
                    <asp:TextBox ID="txtIdentificacion" runat="server" Width="100px" cssClass="textbox"></asp:TextBox>
                </td>
            </tr>
        </table>
    </asp:Panel>

    <asp:MultiView ID="mvLista" runat="server">
        <asp:View ID="vGridReporteCierre" runat="server">
            <div style="overflow: scroll; height: 500px; width: 100%;">
                <div style="width: 1500px;">
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
                            <asp:BoundField DataField="NumRadicacion" HeaderText="Número de Radicación">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="pagare" HeaderText="Pagaré">
                                <ItemStyle HorizontalAlign="Right" />
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
                            <asp:BoundField DataField="monto_aprobado" DataFormatString="{0:C}" HeaderText="Monto Aprobado">
                                <ItemStyle HorizontalAlign="left" Width="90" />
                            </asp:BoundField>
                            <asp:BoundField DataField="monto_desembolsado" DataFormatString="{0:C}" HeaderText="Monto Desembolsado">
                                <ItemStyle HorizontalAlign="left" Width="90" />
                            </asp:BoundField>
                            <asp:BoundField DataField="saldo" DataFormatString="{0:C}" HeaderText="Saldo al cierre">
                                <ItemStyle ForeColor="#009999" HorizontalAlign="Right" Width="90" />
                            </asp:BoundField>
                            <asp:BoundField DataField="valor_cuota" HeaderText="Valor Cuota">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Fecha_vencimiento" HeaderText="Fecha Vencimiento" DataFormatString="{0:d}">
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="dias_mora" HeaderText="Dias Mora">
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
                            <asp:BoundField DataField="nom_tipo_garantia" HeaderText="Tipo de Garantia">
                                <ItemStyle HorizontalAlign="Center"/>
                            </asp:BoundField>
                            <asp:BoundField DataField="valor_garantia" DataFormatString="{0:C}" HeaderText="Valor Garantia">
                                <ItemStyle HorizontalAlign="Center"/>
                            </asp:BoundField>
                            <asp:BoundField DataField="aporte_resta" DataFormatString="{0:C}" HeaderText="Valor Aportes">
                                <ItemStyle HorizontalAlign="Center"/>
                            </asp:BoundField>
                        </Columns>
                        <HeaderStyle CssClass="gridHeader" />
                        <PagerStyle CssClass="gridPager" />
                        <RowStyle CssClass="gridItem" />
                    </asp:GridView>
                    <asp:GridView ID="gvReportecausacion" runat="server" AutoGenerateColumns="False"
                        DataKeyNames="NumRadicacion" HeaderStyle-CssClass="gridHeader" Visible="false"
                        PagerStyle-CssClass="gridPager" RowStyle-CssClass="gridItem" ShowFooter="True"
                        Style="font-size: x-small" Width="100%" GridLines="Horizontal"
                        OnRowDataBound="gvReportecausacion_RowDataBound">
                        <Columns>
                            <asp:BoundField HeaderText="Cod.Ofi." DataField="codigo_oficina" />
                            <asp:BoundField HeaderText="Oficina" DataField="oficina" />
                            <asp:BoundField DataField="NumRadicacion" HeaderText="Número de Radicación">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="identificacion" HeaderText="Identificación">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Nombre del Cliente" DataField="Nombres">
                                <ItemStyle HorizontalAlign="left" Width="100px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="cod_linea" HeaderText="Cod Linea Credito">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="linea" HeaderText="Linea Credito">
                                <ItemStyle HorizontalAlign="left" Width="90" />
                            </asp:BoundField>
                            <asp:BoundField DataField="saldo" DataFormatString="{0:C}" HeaderText="Saldo al cierre">
                                <ItemStyle ForeColor="#009999" HorizontalAlign="Right" Width="90" />
                            </asp:BoundField>
                            <asp:BoundField DataField="valor_cuota" HeaderText="Valor Cuota">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="cod_atr" HeaderText="Cod.Atr.">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="nom_atr" HeaderText="Nombre Atributo">
                                <ItemStyle HorizontalAlign="left" Width="90" />
                            </asp:BoundField>
                            <asp:BoundField DataField="valor_causado" DataFormatString="{0:C}" HeaderText="Vr.Causado Periodo">
                                <ItemStyle HorizontalAlign="Right" Width="90" />
                            </asp:BoundField>
                            <asp:BoundField DataField="valor_orden" DataFormatString="{0:C}" HeaderText="Vr.Orden Periodo">
                                <ItemStyle HorizontalAlign="Right" Width="90" />
                            </asp:BoundField>
                            <asp:BoundField DataField="saldo_causado" DataFormatString="{0:C}" HeaderText="Saldo Causado">
                                <ItemStyle HorizontalAlign="Right" Width="90" />
                            </asp:BoundField>
                            <asp:BoundField DataField="saldo_orden" DataFormatString="{0:C}" HeaderText="Saldo Orden">
                                <ItemStyle HorizontalAlign="Right" Width="90" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Categoria Crédito" DataField="cod_categoria" HeaderStyle-Width="100px">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Categoria Cliente" DataField="cod_categoria_cli" HeaderStyle-Width="100px">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="tasa_interes" HeaderText="Tasa Int." DataFormatString="{0:N3}">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="dias_causados" HeaderText="Dias Cau." DataFormatString="{0:N0}">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="periodicidad" HeaderText="Periodicidad">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>                            
                            <asp:BoundField DataField="dias_amortiza" HeaderText="Dias Amortización" DataFormatString="{0:N0}">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="fecha_proximo_pago" DataFormatString="{0:d}" HeaderText="Fecha Prox.Pago">
                                <ItemStyle HorizontalAlign="center" Width="90" />
                            </asp:BoundField>
                            <asp:BoundField DataField="saldo_causado_ant" DataFormatString="{0:C}" HeaderText="Saldo Causado Ant.">
                                <ItemStyle HorizontalAlign="Right" Width="90" />
                            </asp:BoundField>
                             <asp:BoundField DataField="valor_movimiento" DataFormatString="{0:C}" HeaderText="Movimientos">
                                <ItemStyle HorizontalAlign="Right" Width="90" />
                            </asp:BoundField>
                             <asp:BoundField DataField="saldo_contable" DataFormatString="{0:C}" HeaderText="Valor Contabilizado">
                                <ItemStyle HorizontalAlign="Right" Width="90" />
                            </asp:BoundField>
                        </Columns>
                        <HeaderStyle CssClass="gridHeader" />
                        <PagerStyle CssClass="gridPager" />
                        <RowStyle CssClass="gridItem" />
                    </asp:GridView>
                    <asp:GridView ID="gvReporteprovision" runat="server" AutoGenerateColumns="False"
                        DataKeyNames="NumRadicacion" HeaderStyle-CssClass="gridHeader" Visible="false"
                        PagerStyle-CssClass="gridPager" RowStyle-CssClass="gridItem" ShowFooter="True"
                        Style="font-size: x-small" Width="100%" GridLines="Horizontal"
                        OnRowDataBound="gvReporteprovision_RowDataBound">
                        <Columns>
                            <asp:BoundField HeaderText="Cod.Ofi." DataField="codigo_oficina" />
                            <asp:BoundField HeaderText="Oficina" DataField="oficina" />
                            <asp:BoundField DataField="NumRadicacion" HeaderText="Número de Radicación">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="identificacion" HeaderText="Identificación">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Nombre del Cliente" DataField="Nombres">
                                <ItemStyle HorizontalAlign="left" Width="100px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="cod_linea" HeaderText="Cod Linea Credito">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="linea" HeaderText="Linea Credito">
                                <ItemStyle HorizontalAlign="left" Width="90" />
                            </asp:BoundField>
                            <asp:BoundField DataField="saldo" DataFormatString="{0:C}" HeaderText="Saldo al cierre">
                                <ItemStyle ForeColor="#009999" HorizontalAlign="Right" Width="90" />
                            </asp:BoundField>
                            <asp:BoundField DataField="valor_cuota" HeaderText="Valor Cuota">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="cod_atr" HeaderText="Cod.Atr.">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="nom_atr" HeaderText="Nombre Atributo">
                                <ItemStyle HorizontalAlign="left" Width="90" />
                            </asp:BoundField>
                            <asp:BoundField DataField="porc_provision" DataFormatString="{0:N2}" HeaderText="%Provisión">
                                <ItemStyle HorizontalAlign="Right" Width="90" />
                            </asp:BoundField>
                            <asp:BoundField DataField="valor_provision" DataFormatString="{0:C}" HeaderText="Vr.Provisión">
                                <ItemStyle HorizontalAlign="Right" Width="90" />
                            </asp:BoundField>
                            <asp:BoundField DataField="base_provision" DataFormatString="${0:#,##0.00}" HeaderText="Base Provisión">
                                <ItemStyle HorizontalAlign="Right" Width="90" />
                            </asp:BoundField>
                            <asp:BoundField DataField="aporte_resta" DataFormatString="{0:C}" HeaderText="Aporte Resta">
                                <ItemStyle HorizontalAlign="Right" Width="90" />
                            </asp:BoundField>
                            <asp:BoundField DataField="diferencia_provision" DataFormatString="{0:C}" HeaderText="Diferencia Provisión">
                                <ItemStyle HorizontalAlign="Right" Width="90" />
                            </asp:BoundField>
                            <asp:BoundField DataField="diferencia_actual" HeaderText="Dif.Provisión Actual" HeaderStyle-Width="100px">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="diferencia_anterior" HeaderText="Dif.Provisión Anterior" DataFormatString="{0:N2}">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="cod_categoria" HeaderText="Cod. Categoría">
                                <ItemStyle HorizontalAlign="Right" Width="90" />
                            </asp:BoundField>
                            <asp:BoundField DataField="cod_categoria_cli" HeaderText="Cod.Categoria Cliente">
                                <ItemStyle HorizontalAlign="Right" Width="90" />
                            </asp:BoundField>
                            <asp:BoundField DataField="cod_clasificacion" HeaderText="Cod.Clasificación" HeaderStyle-Width="100px">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="descripcion_clasificacion" HeaderText="Desc. Clasificación">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                             <asp:BoundField DataField="dias_mora" HeaderText="Dias de mora">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                             <asp:BoundField DataField="valor_garantia" DataFormatString="{0:C}" HeaderText="Valor de la garantia">
                                <ItemStyle HorizontalAlign="Right" />
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

