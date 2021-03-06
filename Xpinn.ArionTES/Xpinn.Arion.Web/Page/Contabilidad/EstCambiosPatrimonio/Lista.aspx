﻿<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Lista.aspx.cs" Inherits="Lista" %>

<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar"
    TagPrefix="uc4" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../../../General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:MultiView ActiveViewIndex="0" ID="mvReporteFlujo" runat="server">
        <asp:View runat="server">
            <asp:Panel ID="pConsulta" runat="server">
                <br />
                <table cellpadding="2" cellspacing="2">
                    <tr>
                        <td style="text-align: left">
                            Periodo Actual:<br />
                            <asp:DropDownList ID="ddPeriodoAc" ClientIDMode="Static" Font-Size="10px" CssClass="textbox"
                                runat="server">
                            </asp:DropDownList>
                        </td>
                        <td style="text-align: left">
                            Periodo Anterior:<br />
                            <asp:DropDownList ID="ddlPeriodoAn" Width="191px" ClientIDMode="Static" CssClass="textbox"
                                runat="server">
                            </asp:DropDownList>
                        </td>
                        <td style="text-align: left">
                            Centro de Costo:<br />
                            <asp:DropDownList ID="ddlCentroCosto" Width="191px" ClientIDMode="Static" CssClass="textbox"
                                runat="server">
                            </asp:DropDownList>
                            <asp:Button ID="btnInforme" runat="server" CssClass="btn8" OnClick="btnInforme_Click"
                                Text="Visualizar Informe" />
                        </td>
                    </tr>
                </table>
                <hr style="width: 100%" />
            </asp:Panel>
            <table style="width: 100%">
                <tr>
                    <td style="text-align: left">
                        &nbsp;
                    </td>
                    <td>
                        <asp:GridView ID="gvLista" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                            BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px"
                            CellPadding="4" ForeColor="Black" GridLines="Horizontal" PageSize="20" OnPageIndexChanging="gvLista_PageIndexChanging"
                            Style="font-size: x-small" OnRowDataBound="gvLista_RowDataBound" DataKeyNames="descripcion">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:BoundField DataField="Descripcion_Moviminto" HeaderText="Descripcion Moviemiento">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="aporte_Sociales" HeaderText="Aportes Sociales">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="reservas" HeaderText="Reservas">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="fondos_Destinacion_especificas" HeaderText="Fondos Destinacion Especificas">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="valorizacion" HeaderText="Valorizacion">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="excedentes_netos" HeaderText="Excedentes Netos">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="totales" HeaderText="Totales">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                            </Columns>
                            <FooterStyle BackColor="#CCCC99" />
                            <HeaderStyle CssClass="gridHeader" />
                            <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                            <RowStyle CssClass="gridItem" />
                            <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                            <SortedAscendingCellStyle BackColor="#FBFBF2" />
                            <SortedAscendingHeaderStyle BackColor="#848384" />
                            <SortedDescendingCellStyle BackColor="#EAEAD3" />
                            <SortedDescendingHeaderStyle BackColor="#575357" />
                        </asp:GridView>
                        <asp:Label ID="lblTotalRegs" runat="server" Visible="False" />
                        <asp:Label ID="lblInfo" runat="server" Text="Su consulta no obtuvo ningún resultado."
                            Visible="False" />
                    </td>
                </tr>
            </table>
        </asp:View>
        <asp:View ID="vwReporte" runat="server">
            <br />
            <br />
            <hr width="100%" />
            &nbsp;
            <asp:Button ID="btnDatos" runat="server" CssClass="textbox" OnClick="btnDatos_Click"
                Text="Visualizar Datos" />
            <rsweb:ReportViewer ID="RptReporte" runat="server" Width="905px" Height="777px">
                <LocalReport ReportPath="Page\Contabilidad\EstCambiosPatrimonio\RptCambioPatrimonio.rdlc">
                </LocalReport>
            </rsweb:ReportViewer>
        </asp:View>
    </asp:MultiView>
    <uc4:mensajegrabar ID="ctlMensaje" runat="server" />
</asp:Content>
