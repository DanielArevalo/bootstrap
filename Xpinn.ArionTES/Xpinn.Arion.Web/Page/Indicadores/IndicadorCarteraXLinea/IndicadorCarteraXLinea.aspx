<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="IndicadorCarteraXLinea.aspx.cs" Inherits="Nuevo" %>

<%@ Register Src="../../../General/Controles/ctlBusquedaRapida.ascx" TagName="BusquedaRapida" TagPrefix="uc1" %>
<%@ Register Src="../../../General/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<%@ Register Src="../../../General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>

<%@ Register Src="../../../General/Controles/ctlColorPicker.ascx" TagName="ctlColorPicker" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:Label ID="lablerror" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="lablerror0" runat="server" Visible="false"></asp:Label>
    <br />
    <br />
    <table style="width: 80%">
        <tr>
            <td style="text-align: left; width: 10px">
                <asp:Label ID="lblFechaInicial" runat="server" Text="Fecha Corte"></asp:Label><br />
                <ucFecha:fecha ID="ucFechaCorte" runat="server" style="text-align: center" />
            </td>

            <td style="text-align: left; width: 150px;">Color de Fondo<br />
                <uc1:ctlColorPicker ID="txtColorFondo" runat="server" />
            </td>
            <td style="text-align: left; width: 150px;">
                <asp:CheckBox ID="ch3D" runat="server" OnCheckedChanged="ch3D_CheckedChanged"
                    Text="Mostrar en 3D" AutoPostBack="True" Style="font-size: x-small"
                    Width="110px" />
            </td>
            <td style="text-align: left; width: 150px;">
                <asp:Button ID="btnInforme" runat="server" CssClass="btn8" OnClick="btnInforme_Click"
                    OnClientClick="btnInforme_Click" Text="Generar Gráfica" />
            </td>
        </tr>
        <tr>
            <td style="text-align: left; width: 500px">
                <asp:CheckBox ID="chkMostrarValores" runat="server" OnCheckedChanged="chkMostrarValores_CheckedChanged"
                    Text="Mostrar Gráfica de Valores" AutoPostBack="True" Checked="true" />
            </td>
            <td style="text-align: left; width: 500px">
                <asp:CheckBox ID="chkMostrarPorcentaje" runat="server" OnCheckedChanged="chkMostrarPorcentaje_CheckedChanged"
                    Text="Mostrar Gráfica de Cantidad" AutoPostBack="True" Checked="true" />
            </td>
        </tr>
        <tr>
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
            <td style="text-align: left">&nbsp;</td>
            <td style="text-align: left">&nbsp;</td>


        </tr>

    </table>
    <asp:Panel ID="panelGraficas" runat="server">
        <table>
            <tr>
                <td>
                    <asp:Chart ID="Chart1" runat="server" BackColor="42, 58, 87" BackGradientStyle="TopBottom"
                        BackSecondaryColor="White" BorderDashStyle="Solid" BorderWidth="2px"
                        BorderlineDashStyle="Solid" BorderColor="26, 59, 105"
                        Visible="False" Width="600px" Height="500px">
                        <BorderSkin SkinStyle="Emboss"></BorderSkin>
                        <ChartAreas>
                            <asp:ChartArea Name="ChartArea1" BorderColor="64, 64, 64, 64" BackSecondaryColor="White" BackColor="WhiteSmoke" ShadowColor="Transparent">
                                <Area3DStyle Rotation="20" Perspective="10" Enable3D="False" Inclination="10" IsRightAngleAxes="False" WallWidth="0" IsClustered="False" />
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
                        <Legends>
                            <asp:Legend Alignment="Center" Docking="Bottom" IsTextAutoFit="False" Name="Default"
                                LegendStyle="Row" />
                        </Legends>
                    </asp:Chart>
                </td>
                <td>
                    <asp:Chart ID="Chart2" runat="server" BackColor="42, 58, 87" BackGradientStyle="TopBottom"
                        BackSecondaryColor="White" BorderDashStyle="Solid" BorderWidth="2px"
                        BorderlineDashStyle="Solid" BorderColor="26, 59, 105"
                        Visible="False" Width="600px" Height="500px">
                        <BorderSkin SkinStyle="Emboss"></BorderSkin>
                        <ChartAreas>
                            <asp:ChartArea Name="ChartArea2" BorderColor="64, 64, 64, 64" BackSecondaryColor="White" BackColor="WhiteSmoke" ShadowColor="Transparent">
                                <Area3DStyle Rotation="20" Perspective="10" Enable3D="False" Inclination="10" IsRightAngleAxes="False" WallWidth="0" IsClustered="False" />
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
                        <Legends>
                            <asp:Legend Alignment="Center" Docking="Bottom" IsTextAutoFit="False" Name="Default"
                                LegendStyle="Row" />
                        </Legends>
                    </asp:Chart>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnExportar" runat="server" CssClass="btn8" OnClick="btnExportar_Click"
                        OnClientClick="btnExportar_Click" Text="Exportar Gráfica" /><br />
                </td>
                <td>
                    <asp:Button ID="Button1" runat="server" CssClass="btn8" OnClick="btnExportar_Click"
                        OnClientClick="btnExportar_Click" Text="Exportar Gráfica" /><br />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="gvDatos" runat="server" AutoGenerateColumns="False"
                        HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager" RowStyle-CssClass="gridItem" ShowFooter="False"
                        Style="font-size: xx-small" Width="500px" GridLines="Both" Height="122px">
                        <Columns>
                            <asp:BoundField DataField="Cod_linea_credito" HeaderText="Linea Credito">
                                <ItemStyle Width="85px" />
                            </asp:BoundField>
                              <asp:BoundField DataField="nombre" HeaderText="Nombre Linea Credito">
                                <ItemStyle Width="85px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="total_cartera" HeaderText="Total Cartera Millon" >
                                <ItemStyle Width="85px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="participacion_cartera" HeaderText="Participacion Cartera" >
                                <ItemStyle Width="85px" />
                            </asp:BoundField>

                        </Columns>
                        <HeaderStyle CssClass="gridHeader" />
                        <PagerStyle CssClass="gridPager" />
                        <RowStyle CssClass="gridItem" />
                    </asp:GridView>
                </td>
                 <td>
                  <asp:GridView ID="gvDatos2" runat="server" AutoGenerateColumns="False"
                        HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager" RowStyle-CssClass="gridItem" ShowFooter="False"
                        Style="font-size: xx-small" Width="500px" GridLines="Both" Height="122px">
                        <Columns>
                            <asp:BoundField DataField="Cod_linea_credito" HeaderText="Linea Credito">
                                <ItemStyle Width="85px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="nombre" HeaderText="Nombre Linea Credito">
                                <ItemStyle Width="85px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="numero_cartera" HeaderText="Total Cartera Cantidad" >
                                <ItemStyle Width="85px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="participacion_numero" HeaderText="Participacion Cantidad" >
                                <ItemStyle Width="85px" />
                            </asp:BoundField>

                        </Columns>
                        <HeaderStyle CssClass="gridHeader" />
                        <PagerStyle CssClass="gridPager" />
                        <RowStyle CssClass="gridItem" />
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
