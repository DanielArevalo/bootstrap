<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" EnableEventValidation = "false" AutoEventWireup="true" CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" Title=".: Xpinn - Servicios :." %>

<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/tnumero.ascx" TagName="numero" TagPrefix="ucnumero" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <br />
    <br />
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:Panel runat="server" ID="pnlPlanPagos">
        <table style="width: 80%">
            <tr>
                <td>Num. Servicio
                </td>
                <td style="width: 15%; text-align: left">
                    <asp:TextBox ID="txtNumServ" ReadOnly="true" runat="server" CssClass="textbox" Width="150px" />
                </td>
                <td>Linea Servicio
                </td>
                <td style="text-align: left; width: 25%">
                    <asp:TextBox ID="txtLinea" runat="server" ReadOnly="true" CssClass="textbox" Width="300px" />
                </td>
            </tr>
            <tr>
                <td>Identificación
                </td>
                <td style="text-align: left; width: 15%">
                    <asp:TextBox ID="txtIdentificacion" runat="server" ReadOnly="true" CssClass="textbox" Width="150px" />
                </td>
                <td>Nombre
                </td>
                <td style="text-align: left; width: 30%">
                    <asp:TextBox ID="txtNombre" runat="server" ReadOnly="true" CssClass="textbox" Width="300px" />
                </td>
            </tr>
            <tr>
                <td>Plazo
                </td>
                <td style="text-align: left; width: 15%">
                    <asp:TextBox ID="txtPlazo" runat="server" ReadOnly="true" CssClass="textbox" Width="150px" />
                </td>
                <td>Valor Total
                </td>
                <td style="text-align: left; width: 30%">
                    <asp:TextBox ID="txtValorTotal" runat="server" ReadOnly="true" CssClass="textbox" Width="300px" />
                </td>
            </tr>
            <tr>
                <td style="width: 20%">Periodicidad
                </td>
                <td style="text-align: left; width: 20%">
                    <asp:TextBox ID="txtPeriodicidad" runat="server" ReadOnly="true" CssClass="textbox" Width="150px" />
                </td>
                <td style="width: 20%">Valor Cuota Aprox.
                </td>
                <td style="text-align: left; width: 30%">
                    <asp:TextBox ID="txtValorCuota" runat="server" ReadOnly="true" CssClass="textbox" Width="300px" />
                </td>
            </tr>
            <tr>
                <td style="width: 20%">Tipo Cuota
                </td>
                <td style="text-align: left; width: 20%">
                    <asp:TextBox ID="txtTipoCuota" runat="server" ReadOnly="true" CssClass="textbox" Width="150px" />
                </td>
                <td style="width: 20%">Días Ajuste
                </td>
                <td style="text-align: left; width: 30%">
                    <asp:TextBox ID="txtDiasAjuste" runat="server" ReadOnly="true" CssClass="textbox" Width="300px" />
                </td>
            </tr>
            <tr>
                <td style="width: 20%">Fecha Aprobacion
                </td>
                <td style="text-align: left; width: 20%">
                    <asp:TextBox ID="txtFechaAprobacion" runat="server" ReadOnly="true" CssClass="textbox" Width="150px" />
                </td>
                <td style="width: 20%">Tasa
                </td>
                <td style="text-align: left; width: 30%">
                    <asp:TextBox ID="txtTasa" runat="server" ReadOnly="true" CssClass="textbox" Width="300px" />
                </td>
            </tr>
        </table>
        <br />
        <br />
        <asp:Panel ID="panelGrilla" runat="server">
            <table style="width: 70%; text-align: center;">
                <tr>
                    <td>
                        <asp:GridView ID="gvLista" runat="server" Width="80%" GridLines="Horizontal" AutoGenerateColumns="False"
                            AllowPaging="True" OnPageIndexChanging="gvLista_PageIndexChanging"
                            PageSize="24" HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager"
                            RowStyle-CssClass="gridItem" HorizontalAlign="center"
                            Style="font-size: small">
                            <Columns>
                                <asp:BoundField DataField="NumeroCuota" HeaderText="No. Cuota">
                                    <ItemStyle HorizontalAlign="center" Width="10%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Fecha" HeaderText="Fecha"
                                    DataFormatString="{0:d}">
                                    <ItemStyle HorizontalAlign="center" Width="10%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="ValorAbonarCapital" DataFormatString="${0:#,##0.00}" HeaderText="Capital">
                                    <ItemStyle HorizontalAlign="center" Width="10%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="ValorAbonarInteres" DataFormatString="${0:#,##0.00}" HeaderText="Interes">
                                    <ItemStyle HorizontalAlign="center" Width="10%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="TotalValorCuota" DataFormatString="${0:#,##0.00}" HeaderText="Total Valor">
                                    <ItemStyle HorizontalAlign="center" Width="10%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="SaldoCapitalPendiente" DataFormatString="${0:#,##0.00}" HeaderText="Saldo">
                                    <ItemStyle HorizontalAlign="Center" Width="10%" />
                                </asp:BoundField>
                            </Columns>
                            <HeaderStyle CssClass="gridHeader"></HeaderStyle>
                            <PagerStyle CssClass="gridPager"></PagerStyle>
                            <RowStyle CssClass="gridItem"></RowStyle>
                        </asp:GridView>
                        <asp:Label ID="lblTotalRegs" runat="server" Visible="true" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </asp:Panel>
    <br />
    <br />
</asp:Content>
