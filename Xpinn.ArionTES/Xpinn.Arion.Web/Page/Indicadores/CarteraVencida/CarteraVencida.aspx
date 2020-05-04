<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="CarteraVencida.aspx.cs" Inherits="Nuevo" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<%@ Register Src="../../../General/Controles/ctlBusquedaRapida.ascx" TagName="BusquedaRapida" TagPrefix="uc1" %>
<%@ Register Src="../../../General/Controles/ctlseleccionmultipledropdown.ascx" TagName="dropdownmultiple" TagPrefix="ucDrop" %>
<%@ Register Src="../../../General/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc1" %>
<%@ Register Src="../../../General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Src="../../../General/Controles/ctlColorPicker.ascx" TagName="ctlColorPicker" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <table style="width: 100%">
        <tr>
            <td colspan="5" style="text-align: left; font-size: x-small">
                <strong>Criterios de búsqueda</strong>
            </td>
        </tr>
        <tr>
            <td style="text-align: left">
                <asp:Label ID="lblFechaInicial" runat="server" Text="Fecha Inicial"></asp:Label><br />
                <ucFecha:fecha ID="ucFechaInicial" runat="server" style="text-align: center" />
            </td>
            <td style="text-align: left">
                <asp:Label ID="lblFechaFinal" runat="server" Text="Fecha Final"></asp:Label><br />
                <ucFecha:fecha ID="ucFechaFinal" runat="server" style="text-align: center" />
            </td>
            <td style="text-align: left">
                <asp:RadioButtonList ID="rblFiltroCategoria" runat="server" AutoPostBack="true" RepeatDirection="Horizontal"
                    OnSelectedIndexChanged="rblFiltro2_SelectedIndexChanged" Style="font-size: x-small" class="Listar">
                    <asp:ListItem Value="V">Vencimiento</asp:ListItem>
                    <asp:ListItem Value="C" Selected="True">Categoria</asp:ListItem>
                    <asp:ListItem Value="L">Linea Credito</asp:ListItem>
                </asp:RadioButtonList>
                <asp:DropDownList ID="ddlVencCateg" runat="server" Height="24px" Width="140px" AutoPostBack="True"
                    OnSelectedIndexChanged="ddlVencCateg_SelectedIndexChanged">
                </asp:DropDownList>
                <ucDrop:dropdownmultiple ID="dllineas" Visible="False" runat="server" Height="24px" Width="165px"></ucDrop:dropdownmultiple>
            </td>
            <td style="text-align: left">
                <asp:Label ID="lbl_mora" Visible="False" runat="server" Text="Vencimiento"></asp:Label><br />
                <asp:DropDownList ID="drop_periocidadmora" Visible="False" runat="server" Height="24px" Width="150px"
                    OnSelectedIndexChanged="ddlTipoGrafica1_SelectedIndexChanged" AutoPostBack="True">
                </asp:DropDownList>
            </td>
            <td style="text-align: left">Oficina:<br />
                <ucDrop:dropdownmultiple ID="ddloficina" runat="server" Height="24px" Width="165px"></ucDrop:dropdownmultiple>
            </td>
            <td style="text-align: left">
                <asp:Button ID="btnInforme" runat="server" CssClass="btn8" OnClick="btnInforme_Click"
                    OnClientClick="btnInforme_Click" Text="Generar Gráfica" />
            </td>
        </tr>
    </table>
    <table style="width: 80%">
        <tr>
            <td style="text-align: left">
                <asp:CheckBox ID="chkMostrarValores" runat="server" OnCheckedChanged="chkMostrarValores_CheckedChanged"
                    Text="Mostrar Gráfica de Valores" AutoPostBack="True" Checked="true" />
            </td>
            <td style="text-align: left">
                <asp:CheckBox ID="chkMostrarPorcentaje" runat="server" OnCheckedChanged="chkMostrarPorcentaje_CheckedChanged"
                    Text="Mostrar Gráfica de Porcentaje" AutoPostBack="True" Checked="true" />
            </td>
            <td style="text-align: left">
                <asp:CheckBox ID="ch3D" runat="server" OnCheckedChanged="ch3D_CheckedChanged" Width="120px"
                    Text="Mostrar en 3D" AutoPostBack="True" />
            </td>
            <td style="text-align: left">Color de Fondo<br />
                <uc1:ctlColorPicker ID="txtColorFondo" runat="server" />
            </td>
            <td style="text-align: left"></td>
        </tr>
    </table>
    <hr style="width: 100%" />
    <asp:Panel ID="panelGraficas" runat="server">
        <table>
            <tr>
                <td style="text-align: left">
                    <asp:Panel ID="panelValores" runat="server">
                        <table>
                            <tr>
                                <td>Tipo de Gráfica
                                    <asp:DropDownList ID="ddlTipoGrafica1" runat="server" Height="24px" Width="150px"
                                        OnSelectedIndexChanged="ddlTipoGrafica1_SelectedIndexChanged" AutoPostBack="True">
                                        <asp:ListItem Value="1">Barras</asp:ListItem>
                                        <asp:ListItem Value="2">Pie</asp:ListItem>
                                        <asp:ListItem Value="3">Lineas</asp:ListItem>
                                        <asp:ListItem Value="4">Area</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Chart ID="Chart1" runat="server" BackColor="42, 58, 87" BackGradientStyle="TopBottom"
                                        BackSecondaryColor="White" BorderDashStyle="Solid" BorderWidth="2px" BorderlineDashStyle="Solid"
                                        BorderColor="26, 59, 105" Visible="False" Width="600px" Height="400px">
                                        <BorderSkin SkinStyle="Emboss"></BorderSkin>
                                        <Series>
                                            <asp:Series Name="Series1" ChartType="Line" Color="#990000" Font="Microsoft Sans Serif, 7pt"
                                                MarkerSize="7" MarkerBorderColor="Crimson" MarkerBorderWidth="1" MarkerColor="Crimson"
                                                MarkerStyle="Circle">
                                            </asp:Series>
                                            <asp:Series Name="Series2" ChartType="Line" Color="#000099" Font="Microsoft Sans Serif, 7pt"
                                                MarkerSize="7" MarkerBorderColor="Crimson" MarkerBorderWidth="1" MarkerColor="Crimson"
                                                MarkerStyle="Circle">
                                            </asp:Series>
                                        </Series>
                                        <ChartAreas>
                                            <asp:ChartArea Name="ChartArea1" BorderColor="64, 64, 64, 64" BackSecondaryColor="White"
                                                BackColor="WhiteSmoke" ShadowColor="Transparent">
                                                <Area3DStyle Rotation="20" Perspective="10" Enable3D="True" Inclination="10" IsRightAngleAxes="False"
                                                    WallWidth="0" IsClustered="False" />
                                                <AxisY LineDashStyle="DashDotDot" LineColor="Silver">
                                                    <MajorGrid LineDashStyle="Dot" />
                                                    <MinorGrid LineDashStyle="Dot" />
                                                    <MajorTickMark LineDashStyle="Dot" />
                                                    <MinorTickMark LineDashStyle="Dot" />
                                                    <LabelStyle Enabled="False" />
                                                </AxisY>
                                                <AxisX LineDashStyle="Dot" LineColor="LightGray">
                                                    <MajorGrid LineDashStyle="Dot" />
                                                    <MinorGrid LineDashStyle="Dot" />
                                                    <LabelStyle Font="Trebuchet MS, 7pt" />
                                                </AxisX>
                                            </asp:ChartArea>
                                        </ChartAreas>
                                        <Titles>
                                            <asp:Title Name="Title1" Text="" ForeColor="#FFFFFF" Font="Arial, 11pt, style=Bold"
                                                TextStyle="Shadow">
                                            </asp:Title>
                                            <asp:Title Name="Title2" Text="" ForeColor="#FFFFFF" Font="Arial, 8pt, style=Bold"
                                                TextStyle="Shadow">
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
                                    <center>
                                        <asp:Button ID="btnExportar" runat="server" CssClass="btn8" OnClick="btnExportar_Click"
                                            OnClientClick="btnExportar_Click" Text="Exportar Gráfica" />
                                    </center>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
                <td style="text-align: left">
                    <asp:Panel ID="panelPorcentaje" runat="server">
                        <table>
                            <tr>
                                <td>Tipo de Gráfica&nbsp;&nbsp;&nbsp;
                                    <asp:DropDownList ID="ddlTipoGrafica2" runat="server"
                                        Height="24px" Width="150px" OnSelectedIndexChanged="ddlTipoGrafica2_SelectedIndexChanged"
                                        AutoPostBack="True">
                                        <asp:ListItem Value="1">Barras</asp:ListItem>
                                        <asp:ListItem Value="2">Pie</asp:ListItem>
                                        <asp:ListItem Value="3">Lineas</asp:ListItem>
                                        <asp:ListItem Value="4">Area</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Chart ID="Chart2" runat="server" BackColor="42, 58, 87" BackGradientStyle="TopBottom"
                                        BackSecondaryColor="White" BorderDashStyle="Solid" BorderWidth="2px" BorderlineDashStyle="Solid"
                                        BorderColor="26, 59, 105" Visible="False" Width="500px" Height="400px">
                                        <BorderSkin SkinStyle="Emboss"></BorderSkin>
                                        <Series>
                                            <asp:Series Name="Series1" ChartType="Line" Color="#990000" Font="Microsoft Sans Serif, 7pt"
                                                MarkerSize="7" MarkerBorderColor="Crimson" MarkerBorderWidth="1" MarkerColor="Crimson"
                                                MarkerStyle="Circle">
                                            </asp:Series>
                                            <asp:Series Name="Series2" ChartType="Line" Color="#000099" Font="Microsoft Sans Serif, 7pt"
                                                MarkerSize="7" MarkerBorderColor="Crimson" MarkerBorderWidth="1" MarkerColor="Crimson"
                                                MarkerStyle="Circle">
                                            </asp:Series>
                                        </Series>
                                        <ChartAreas>
                                            <asp:ChartArea Name="ChartArea1" BorderColor="64, 64, 64, 64" BackSecondaryColor="White"
                                                BackColor="WhiteSmoke" ShadowColor="Transparent">
                                                <Area3DStyle Rotation="20" Perspective="10" Enable3D="True" Inclination="10" IsRightAngleAxes="False"
                                                    WallWidth="0" IsClustered="False" />
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
                                            <asp:Title Name="Title1" Text="" ForeColor="#FFFFFF" Font="Arial, 11pt, style=Bold"
                                                TextStyle="Shadow">
                                            </asp:Title>
                                            <asp:Title Name="Title2" Text="" ForeColor="#FFFFFF" Font="Arial, 8pt, style=Bold"
                                                TextStyle="Shadow">
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
                                    <center>
                                        <asp:Button ID="btnExportarpor" runat="server" CssClass="btn8" OnClick="btnExportarpor_Click"
                                            OnClientClick="btnExportarpor_Click" Text="Exportar Gráfica" />
                                    </center>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td colspan="1">
                    <div style="overflow: auto; width: 90%;">
                        <asp:GridView ID="gvDatos" runat="server" AutoGenerateColumns="False" HeaderStyle-CssClass="gridHeader"
                            PagerStyle-CssClass="gridPager" RowStyle-CssClass="gridItem" ShowFooter="False"
                            Style="font-size: xx-small" Width="90%" GridLines="Both">
                            <Columns>
                            </Columns>
                            <HeaderStyle CssClass="gridHeader" />
                            <PagerStyle CssClass="gridPager" />
                            <RowStyle CssClass="gridItem" />
                        </asp:GridView>
                    </div>
                </td>
                <td colspan="1">
                    <div style="overflow: auto; width: 90%;">
                        <asp:GridView ID="gvPorcentaje" runat="server" AutoGenerateColumns="False" HeaderStyle-CssClass="gridHeader"
                            PagerStyle-CssClass="gridPager" RowStyle-CssClass="gridItem" ShowFooter="False"
                            Style="font-size: xx-small" Width="90%" GridLines="Both">
                            <Columns>
                            </Columns>
                            <HeaderStyle CssClass="gridHeader" />
                            <PagerStyle CssClass="gridPager" />
                            <RowStyle CssClass="gridItem" />
                        </asp:GridView>
                    </div>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
