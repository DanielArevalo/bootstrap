<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Nuevo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>

<%@ Register Src="../../../General/Controles/ctlColorPicker.ascx" TagName="ctlColorPicker" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table style="width: 80%">
        <tr>
            <td style="text-align: left; font-size: small;" colspan="4">
                <strong>Criterios de Generación:</strong></td>
            <td style="text-align: left; font-size: small;">&nbsp;</td>
            <td style="text-align: center; width: 497px;">&nbsp;</td>
            <td class="logo" style="width: 194px">&nbsp;</td>
        </tr>
        <tr>
            <td style="text-align: left">Fecha de Corte<br />
                <asp:DropDownList ID="ddlFechaCorte" runat="server" Height="24px"
                    Width="165px" AutoPostBack="True"
                    OnSelectedIndexChanged="ddlFechaCorte_SelectedIndexChanged">
                </asp:DropDownList>
                <br />
            </td>
            <td style="text-align: left">Periodo:<br />
                <asp:DropDownList ID="ddlPeriodo" runat="server" Height="24px"
                    Width="165px" OnSelectedIndexChanged="ddlPeriodo_SelectedIndexChanged"
                    AutoPostBack="True">

                    <asp:ListItem Value="1">Anual</asp:ListItem>
                    <asp:ListItem Value="2">Semetral</asp:ListItem>
                    <asp:ListItem Value="3">Trimestral</asp:ListItem>
                    <asp:ListItem Value="4" Selected="True">Mensual</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td style="text-align: left; width: 200px;">Color de Fondo<br />
                <uc1:ctlColorPicker ID="txtColorFondo" runat="server" />
            </td>
            <td style="text-align: left; width: 200px;">
                <asp:CheckBox ID="ch3D" runat="server" OnCheckedChanged="ch3D_CheckedChanged"
                    Text="Mostrar en 3D" AutoPostBack="True" />
            </td>
            <td style="text-align: left; width: 200px;">Tipo Producto<asp:DropDownList ID="ddlTipoProducto" runat="server" AutoPostBack="True" CssClass="textbox"
                Width="182px">
            </asp:DropDownList>
            </td>
            <td style="text-align: left">
                <asp:Button ID="btnInforme" runat="server" CssClass="btn8" OnClick="btnInforme_Click"
                    OnClientClick="btnInforme_Click" Text="Generar Reporte" />
            </td>
        </tr>
        <tr>
             <td style="text-align: left; width: 200px;">
                <asp:CheckBox ID="chKMillones" runat="server" OnCheckedChanged="chKMillones_CheckedChanged"
                    Text="Mostrar en Millones" AutoPostBack="True" />
            </td>
            <td style="text-align: left">Tipo de Gráfica</td>
            <td style="text-align: left">
                <asp:DropDownList ID="ddlTipoGrafica1" runat="server" Height="24px" Width="150px"
                    OnSelectedIndexChanged="ddlTipoGrafica1_SelectedIndexChanged" AutoPostBack="True">
                    <asp:ListItem Value="1">Barras</asp:ListItem>
                    <asp:ListItem Value="2">Pie</asp:ListItem>
                    <asp:ListItem Value="3">Lineas</asp:ListItem>
                    <asp:ListItem Value="4">Area</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td style="text-align: left; width: 200px;">&nbsp;</td>
            <td style="text-align: left; width: 200px;">Tipo de Gráfica</td>
            <td style="text-align: left">
                <asp:DropDownList ID="ddlTipoGrafica2" runat="server" Height="24px" Width="150px"
                    OnSelectedIndexChanged="ddlTipoGrafica1_SelectedIndexChanged" AutoPostBack="True">
                    <asp:ListItem Value="1">Barras</asp:ListItem>
                    <asp:ListItem Value="2">Pie</asp:ListItem>
                    <asp:ListItem Value="3">Lineas</asp:ListItem>
                    <asp:ListItem Value="4">Area</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
           
        </tr>
    </table>
    <asp:Panel ID="PanelComprobanteImpr" runat="server">
        <table width="90%">
            <tr>
                <td>
                    <asp:Chart ID="Chart1" runat="server" BackColor="42, 58, 87" BackGradientStyle="TopBottom"
                        BackSecondaryColor="White" BorderDashStyle="Solid" BorderWidth="2px"
                        BorderlineDashStyle="Solid" BorderColor="26, 59, 105"
                        Visible="False" Width="500px" Height="400px">
                        <BorderSkin SkinStyle="Emboss"></BorderSkin>
                        <Series>
                            <asp:Series Name="Series1" Color="DarkMagenta" ChartType="Line"
                                Font="Microsoft Sans Serif, 6.75pt, style=Bold" IsValueShownAsLabel="True" MarkerSize="7" MarkerBorderColor="#CC0099" MarkerBorderWidth="1"
                                MarkerColor="#CC0099" MarkerStyle="Circle">
                            </asp:Series>
                        </Series>
                        <Legends>
                            <asp:Legend Alignment="Center" Docking="Bottom" IsTextAutoFit="False" Name="Default"
                                LegendStyle="Row" Font="Trebuchet MS, 8pt, style=Bold" />
                        </Legends>
                        <ChartAreas>
                            <asp:ChartArea Name="ChartArea1" BorderColor="64, 64, 64, 64" BackSecondaryColor="Transparent" BackColor="White" ShadowColor="Transparent" BackGradientStyle="TopBottom" BorderDashStyle="DashDot">
                                <AxisY LineDashStyle="DashDotDot">
                                    <MajorGrid LineDashStyle="Dot" />
                                    <MinorGrid LineDashStyle="Dot" />
                                    <MajorTickMark LineDashStyle="Dot" />
                                    <MinorTickMark LineDashStyle="Dot" />
                                    <LabelStyle Enabled="False" />
                                </AxisY>
                                <AxisX LineDashStyle="Dot">
                                    <MajorGrid LineDashStyle="Dot" />
                                    <MinorGrid LineDashStyle="Dot" />
                                    <LabelStyle Font="Trebuchet MS, 7pt" />
                                </AxisX>
                            </asp:ChartArea>
                        </ChartAreas>
                        <Titles>
                            <asp:Title Name="Title1" Text="" ForeColor="#FFFFFF" Font="Arial, 11pt , style=Bold" TextStyle="Shadow">
                            </asp:Title>
                            <asp:Title Name="Title2" Text="" ForeColor="#FFFFFF" Font="Arial, 8pt , style=Bold" TextStyle="Shadow">
                            </asp:Title>
                        </Titles>
                        <Annotations>
                        </Annotations>
                    </asp:Chart>
                </td>
                <td>
                    <asp:Chart ID="Chart2" runat="server" BackColor="42, 58, 87" BackGradientStyle="TopBottom"
                        BackSecondaryColor="White" BorderDashStyle="Solid" BorderWidth="2px"
                        BorderlineDashStyle="Solid" BorderColor="26, 59, 105"
                        Visible="False" Width="500px" Height="400px" EnableTheming="True" TextAntiAliasingQuality="Normal">
                        <BorderSkin SkinStyle="Emboss"></BorderSkin>
                        <Series>
                            <asp:Series Name="Series1" Color="DarkMagenta" ChartType="Line"
                                Font="Microsoft Sans Serif, 7pt, style=Bold" IsValueShownAsLabel="True"
                                MarkerSize="7" MarkerBorderColor="#333300" MarkerBorderWidth="1"
                                MarkerColor="#CC0099" MarkerStyle="Circle" CustomProperties="LabelStyle=BottomRight">
                                <EmptyPointStyle CustomProperties="LabelStyle=Left" />
                            </asp:Series>
                        </Series>

                        <Legends>
                            <asp:Legend Alignment="Center" Docking="Bottom" IsTextAutoFit="False" Name="Default"
                                LegendStyle="Row" Font="Trebuchet MS, 8pt, style=Bold" />
                        </Legends>
                        <ChartAreas>
                            <asp:ChartArea Name="ChartArea1" BorderColor="64, 64, 64, 64" BackSecondaryColor="Transparent" BackColor="White" ShadowColor="Transparent" BackGradientStyle="TopBottom" BorderDashStyle="DashDot">
                                <AxisY LineDashStyle="DashDotDot">
                                    <MajorGrid LineDashStyle="Dot" />
                                    <MinorGrid LineDashStyle="Dot" />
                                    <MajorTickMark LineDashStyle="Dot" />
                                    <MinorTickMark LineDashStyle="Dot" />
                                    <LabelStyle Enabled="False" />
                                </AxisY>
                                <AxisX LineDashStyle="Dot">
                                    <MajorGrid LineDashStyle="Dot" />
                                    <MinorGrid LineDashStyle="Dot" />
                                    <LabelStyle Font="Trebuchet MS, 7pt" />
                                </AxisX>
                            </asp:ChartArea>
                        </ChartAreas>
                        <Titles>
                            <asp:Title Name="Title1" Text="" ForeColor="#FFFFFF" Font="Arial, 11pt , style=Bold" TextStyle="Shadow">
                            </asp:Title>
                            <asp:Title Name="Title2" Text="" ForeColor="#FFFFFF" Font="Arial, 8pt , style=Bold" TextStyle="Shadow">
                            </asp:Title>
                        </Titles>
                        <Annotations>
                        </Annotations>
                    </asp:Chart>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnExportar" runat="server" CssClass="btn8" OnClick="btnExportar_Click"
                        OnClientClick="btnExportar_Click" Text="Exportar Gráfica" />
                    <asp:GridView ID="gvDatos" runat="server" AutoGenerateColumns="False" GridLines="Both" HeaderStyle-CssClass="gridHeader" Height="122px" PagerStyle-CssClass="gridPager" RowStyle-CssClass="gridItem" ShowFooter="False" Style="font-size: xx-small" Width="500px">
                        <Columns>
                            <asp:BoundField DataField="fecha_historico" HeaderText="Fecha">
                                <ItemStyle Width="85px " />
                            </asp:BoundField>
                            <asp:BoundField DataField="total" DataFormatString="{0:N}" HeaderText="Total">
                                <ItemStyle Width="85px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="descripcion" HeaderText="Linea">
                                <ItemStyle Width="85px" />
                            </asp:BoundField>

                        </Columns>
                        <HeaderStyle CssClass="gridHeader" />
                        <PagerStyle CssClass="gridPager" />
                        <RowStyle CssClass="gridItem" />
                    </asp:GridView>
                </td>
                
                <td>
                    <asp:Button ID="btnExportarpor" runat="server" CssClass="btn8" OnClick="btnExportarpor_Click"
                        OnClientClick="btnExportarpor_Click"
                        Text="Exportar Gráfica" />
                    <asp:GridView ID="gvCantidad" runat="server" AutoGenerateColumns="False" GridLines="Both" HeaderStyle-CssClass="gridHeader" Height="122px" PagerStyle-CssClass="gridPager" RowStyle-CssClass="gridItem" ShowFooter="False" Style="font-size: xx-small" Width="432px">
                        <Columns>
                            <asp:BoundField DataField="fecha_historico" HeaderText="Fecha">
                                <ItemStyle Width="85px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="numero_cuentas" HeaderText="Cantidad">
                                <ItemStyle Width="85px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="descripcion" HeaderText="Linea">
                                <ItemStyle Width="85px" />
                            </asp:BoundField>
                        </Columns>
                        <HeaderStyle CssClass="gridHeader" />
                        <PagerStyle CssClass="gridPager" />
                        <RowStyle CssClass="gridItem" />
                    </asp:GridView>
                </td>
                
            </tr>
            <tr>
                <td>
                    <asp:Label runat="server" Text="Total General: "></asp:Label>
                    <asp:Label runat="server" ID="lbltotal"></asp:Label>
                </td>
                <td>
                    <asp:Label runat="server" Text="Total Cantidad General: "></asp:Label>
                    <asp:Label runat="server" ID="lbltotalcantidad"></asp:Label>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
