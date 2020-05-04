<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="CarteraBruta.aspx.cs" Inherits="Nuevo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<%@ Register Src="../../../General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>

<%@ Register Src="../../../General/Controles/ctlColorPicker.ascx" TagName="ctlColorPicker" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table style="width: 105%">
        <tr>
            <td style="text-align: left; font-size: small;" colspan="4">
                <strong>Criterios de Generación:</strong></td>
            <td style="text-align: center; width: 578px;">&nbsp;</td>
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
                </asp:DropDownList>
            </td>
            <td style="text-align: left">&nbsp;Tipo de Letra.
                <br />
                <asp:DropDownList ID="ddlFuentes" runat="server" Height="24px"
                    AutoPostBack="true" OnSelectedIndexChanged="ddlFuentes_SelectedIndexChanged">
                </asp:DropDownList>

            </td>


            <td style="text-align: left; width: 355px;">
                <asp:CheckBox ID="ch3D" runat="server" OnCheckedChanged="ch3D_CheckedChanged"
                    Text="Mostrar en 3D" AutoPostBack="True" Width="130px" />
            </td>
            <td style="text-align: left; width: 578px;">
                <asp:Button ID="btnInforme" runat="server" CssClass="btn8" OnClick="btnInforme_Click"
                    OnClientClick="btnInforme_Click" Text="Generar Reporte" />
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
            <td style="text-align: left; width: 355px;">Tipo de Gráfica</td>
            <td style="text-align: left; width: 578px;">
                <asp:DropDownList ID="ddlTipoGrafica2" runat="server" Height="24px"
                    Width="165px" OnSelectedIndexChanged="ddlTipoGrafica2_SelectedIndexChanged"
                    AutoPostBack="True">
                    <asp:ListItem Value="1">Barras</asp:ListItem>
                    <asp:ListItem Value="2">Pie</asp:ListItem>
                    <asp:ListItem Value="3">Lineas</asp:ListItem>
                    <asp:ListItem Value="4">Area</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td colspan="5">
                <center>
                      &nbsp;Color<br />
                <uc1:ctlColorPicker ID="txtColorFondo" runat="server" />
                       </center>
                <br />
                <asp:CheckBox ID="chkFondo" runat="server" Text="Fondo" Checked="true" />
                <asp:CheckBox ID="chkTotal" runat="server" Text="Total" />
                <asp:CheckBox ID="chk1" runat="server" />
                <asp:CheckBox ID="chk2" runat="server" />
                <asp:CheckBox ID="chk3" runat="server" />
                <asp:CheckBox ID="chk4" runat="server" />

            </td>
        </tr>
    </table>
    <asp:Panel ID="PanelComprobanteImpr" runat="server">
        <table width="90%">
            <tr>
                <td>
                    <asp:Chart ID="Chart1" runat="server" BackColor="42, 58, 87" BackGradientStyle="TopBottom"
                        BackSecondaryColor="White" BorderDashStyle="Solid" BorderWidth="2px"
                        BorderlineDashStyle="Solid" BorderColor="26, 59, 105"
                        Visible="False" Width="500px" Height="400px" OnLoad="Chart1_Load">
                        <BorderSkin SkinStyle="Emboss"></BorderSkin>
                        <Series>
                            <asp:Series Name="Series1" Color="DarkMagenta" ChartType="Line"
                                Font="Microsoft Sans Serif, 7pt" IsValueShownAsLabel="True" MarkerSize="7" MarkerBorderColor="#CC0099" MarkerBorderWidth="1"
                                MarkerColor="#CC0099" MarkerStyle="Circle">
                            </asp:Series>
                            <asp:Series Name="Series2" Color="DarkSeaGreen" ChartType="Line"
                                Font="Microsoft Sans Serif, 7pt" IsValueShownAsLabel="True" MarkerSize="7" MarkerBorderColor="DarkSeaGreen" MarkerBorderWidth="1" MarkerColor="DarkSeaGreen" MarkerStyle="Circle">
                            </asp:Series>
                            <asp:Series Name="Series3" Color="DodgerBlue" ChartType="Line"
                                Font="Microsoft Sans Serif, 7pt" IsValueShownAsLabel="True" MarkerSize="7" MarkerBorderColor="DodgerBlue" MarkerBorderWidth="1" MarkerColor="DodgerBlue" MarkerStyle="Circle">
                            </asp:Series>
                            <asp:Series Name="Series4" Color="Crimson" ChartType="Line"
                                Font="Microsoft Sans Serif, 7pt" IsValueShownAsLabel="True" MarkerSize="7" MarkerBorderColor="Crimson" MarkerBorderWidth="1" MarkerColor="Crimson" MarkerStyle="Circle">
                            </asp:Series>
                            <asp:Series Name="Series5" Color="Yellow" ChartType="Line"
                                Font="Microsoft Sans Serif, 7pt" IsValueShownAsLabel="True" MarkerSize="7" MarkerBorderColor="Yellow" MarkerBorderWidth="1" MarkerColor="Yellow" MarkerStyle="Circle">
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

                    <asp:Chart ID="Chart2" runat="server" BackColor="42, 58, 87" BackGradientStyle="TopBottom" BackSecondaryColor="White" BorderColor="26, 59, 105" BorderDashStyle="Solid" BorderlineDashStyle="Solid" BorderWidth="2px" Height="400px" OnLoad="Chart1_Load" Visible="False" Width="500px">
                        <BorderSkin SkinStyle="Emboss" />
                        <Series>
                            <asp:Series ChartType="Line" Color="DarkMagenta" Font="Microsoft Sans Serif, 7pt" IsValueShownAsLabel="True" MarkerBorderColor="#CC0099" MarkerBorderWidth="1" MarkerColor="#CC0099" MarkerSize="7" MarkerStyle="Circle" Name="Series1">
                            </asp:Series>
                            <asp:Series ChartType="Line" Color="DarkSeaGreen" Font="Microsoft Sans Serif, 7pt" IsValueShownAsLabel="True" MarkerBorderColor="DarkSeaGreen" MarkerBorderWidth="1" MarkerColor="DarkSeaGreen" MarkerSize="7" MarkerStyle="Circle" Name="Series2">
                            </asp:Series>
                            <asp:Series ChartType="Line" Color="DodgerBlue" Font="Microsoft Sans Serif, 7pt" IsValueShownAsLabel="True" MarkerBorderColor="DodgerBlue" MarkerBorderWidth="1" MarkerColor="DodgerBlue" MarkerSize="7" MarkerStyle="Circle" Name="Series3">
                            </asp:Series>
                            <asp:Series ChartType="Line" Color="Crimson" Font="Microsoft Sans Serif, 7pt" IsValueShownAsLabel="True" MarkerBorderColor="Crimson" MarkerBorderWidth="1" MarkerColor="Crimson" MarkerSize="7" MarkerStyle="Circle" Name="Series4">
                            </asp:Series>
                            <asp:Series ChartType="Line" Color="Yellow" Font="Microsoft Sans Serif, 7pt" IsValueShownAsLabel="True" MarkerBorderColor="Yellow" MarkerBorderWidth="1" MarkerColor="Yellow" MarkerSize="7" MarkerStyle="Circle" Name="Series5">
                            </asp:Series>
                        </Series>
                        <Legends>
                            <asp:Legend Alignment="Center" Docking="Bottom" Font="Trebuchet MS, 8pt, style=Bold" IsTextAutoFit="False" LegendStyle="Row" Name="Default" />
                        </Legends>
                        <ChartAreas>
                            <asp:ChartArea BackColor="White" BackGradientStyle="TopBottom" BackSecondaryColor="Transparent" BorderColor="64, 64, 64, 64" BorderDashStyle="DashDot" Name="ChartArea1" ShadowColor="Transparent">
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
                            <asp:Title Font="Arial, 11pt , style=Bold" ForeColor="#FFFFFF" Name="Title1" Text="" TextStyle="Shadow">
                            </asp:Title>
                            <asp:Title Font="Arial, 8pt , style=Bold" ForeColor="#FFFFFF" Name="Title2" Text="" TextStyle="Shadow">
                            </asp:Title>
                        </Titles>
                        <Annotations>
                        </Annotations>
                    </asp:Chart>

                </td>
            </tr>

            <tr>
                <td>
                    <br />
                    <asp:Button ID="btnExportar" runat="server" CssClass="btn8" OnClick="btnExportar_Click"
                        OnClientClick="btnExportar_Click" Text="Exportar Gráfica" />
                    <br />
                        <asp:GridView ID="gvDatos" runat="server" AutoGenerateColumns="False"
                            HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager" RowStyle-CssClass="gridItem" ShowFooter="False"
                            Style="font-size: xx-small" Width="500px" GridLines="Both" Height="122px">
                            <Columns>
                                <asp:BoundField DataField="fecha_historico" HeaderText="Fecha">
                                    <ItemStyle Width="85px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="total_cartera" HeaderText="Total Cartera" DataFormatString="{0:C}">
                                    <ItemStyle Width="85px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="total_cartera_microcredito" HeaderText="Total Cartera Microcredito" DataFormatString="{0:C}">
                                    <ItemStyle Width="85px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="total_cartera_consumo" HeaderText="Total Cartera Consumo" DataFormatString="{0:C}">
                                    <ItemStyle Width="85px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="total_cartera_comercial" HeaderText="Total Cartera Comercial" DataFormatString="{0:C}">
                                    <ItemStyle Width="85px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="total_cartera_vivienda" HeaderText="Total Cartera Vivienda" DataFormatString="{0:C}">
                                    <ItemStyle Width="75px" />
                                </asp:BoundField>


                            </Columns>
                            <HeaderStyle CssClass="gridHeader" />
                            <PagerStyle CssClass="gridPager" />
                            <RowStyle CssClass="gridItem" />
                        </asp:GridView>
                    <br />
                </td>
                <td>
                    <asp:Button ID="btnExportarpor" runat="server" CssClass="btn8" OnClick="btnExportarpor_Click"
                        OnClientClick="btnExportarpor_Click"
                        Text="Exportar Gráfica" />
                    <br />
                    <asp:GridView ID="gvCantidad" runat="server" AutoGenerateColumns="False"
                        HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager" RowStyle-CssClass="gridItem" ShowFooter="False"
                        Style="font-size: xx-small" Width="510px" GridLines="Both" Height="122px">
                        <Columns>
                            <asp:BoundField DataField="fecha_historico" HeaderText="Fecha">
                                <ItemStyle Width="85px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="numero_cartera" HeaderText="Cantidad Cartera">
                                <ItemStyle Width="85px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="numero_cartera_microcredito" HeaderText="Cantidad Cartera Microcredito">
                                <ItemStyle Width="85px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="numero_cartera_consumo" HeaderText="Cantidad Cartera Consumo">
                                <ItemStyle Width="85px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="numero_cartera_comercial" HeaderText="Cantidad Cartera Comercial">
                                <ItemStyle Width="85px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="numero_cartera_vivienda" HeaderText="Cantidad Cartera Vivienda">
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
