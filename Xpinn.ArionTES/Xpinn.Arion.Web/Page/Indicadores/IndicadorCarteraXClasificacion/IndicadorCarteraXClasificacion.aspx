<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="IndicadorCarteraXClasificacion.aspx.cs" Inherits="Nuevo" %>

<%@ Register Src="../../../General/Controles/ctlBusquedaRapida.ascx" TagName="BusquedaRapida" TagPrefix="uc1" %>
<%@ Register Src="../../../General/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<%@ Register Src="../../../General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>

<%@ Register src="../../../General/Controles/ctlColorPicker.ascx" tagname="ctlColorPicker" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:Label ID="lablerror" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="lablerror0" runat="server" Visible="false"></asp:Label>
    <table style="width: 60%">
        <tr>
            <td style="text-align: center">
                <asp:Label ID="lblFechaInicial" runat="server" Text="Fecha Inicial"></asp:Label><br />
                <ucFecha:fecha ID="ucFechaInicial" runat="server" style="text-align: center" />
            </td>
            <td style="text-align: center">
                <asp:Label ID="lblFechaFinal" runat="server" Text="Fecha Final"></asp:Label><br />
                <ucFecha:fecha ID="ucFechaFinal" runat="server" style="text-align: center" />
            </td>
            <td style="text-align: left; width: 397px;">      
                Color de Fondo<br />
                <uc1:ctlColorPicker ID="txtColorFondo" runat="server" />
            </td>
            <td style="text-align: left; width: 397px;">      
                <asp:CheckBox ID="ch3D" runat="server" oncheckedchanged="ch3D_CheckedChanged" 
                    Text="Mostrar en 3D" AutoPostBack="True" style="font-size: x-small" 
                    Width="110px" />                   
            </td>
            <td style="text-align: center">
                <asp:Button ID="btnInforme" runat="server" CssClass="btn8" OnClick="btnInforme_Click"
                    OnClientClick="btnInforme_Click" Text="Generar Gráfica" />
                        
            </td>
        </tr>              
    </table>
    <asp:Panel ID="panelGraficas" runat="server">
        <table>
            <tr>
                <td>
                    <asp:Chart ID="Chart1"  runat="server" BackColor="42, 58, 87" BackGradientStyle="TopBottom"
                        BackSecondaryColor="White" BorderDashStyle="Solid" BorderWidth="2px"
                        BorderlineDashStyle="Solid" BorderColor="26, 59, 105"
                        Visible="False" Width="600px" Height="400px" >
                        <borderskin SkinStyle="Emboss"></borderskin>
                        <Series>
                            <asp:Series Name="Series1" ChartType="Line" Color="#990000" 
                                Font="Microsoft Sans Serif, 6pt"  MarkerSize="7" MarkerBorderColor="#990000" MarkerBorderWidth="1" MarkerColor="#990000" MarkerStyle="Circle" >
                            </asp:Series>
                            <asp:Series Name="Series2" ChartType="Line" Color="#000099" 
                                Font="Microsoft Sans Serif, 6pt"  MarkerSize="7" MarkerBorderColor="#000099" MarkerBorderWidth="1" MarkerColor="#000099" MarkerStyle="Circle" >
                            </asp:Series>
                            <asp:Series Name="Series3" ChartType="Line" Color="Yellow" 
                                Font="Microsoft Sans Serif, 6pt"  MarkerSize="7" 
                                MarkerBorderColor="Yellow" MarkerBorderWidth="1" MarkerColor="Yellow" 
                                MarkerStyle="Circle" >
                            </asp:Series>
                            <asp:Series Name="Series4" ChartType="Line" Color="0, 192, 0" 
                                Font="Microsoft Sans Serif, 6pt"  MarkerSize="7" 
                                MarkerBorderColor="0, 192, 0" MarkerBorderWidth="1" MarkerColor="0, 192, 0" 
                                MarkerStyle="Circle" >
                            </asp:Series>
                        </Series>
                        <ChartAreas>
							<asp:ChartArea Name="ChartArea1" BorderColor="64, 64, 64, 64" BackSecondaryColor="White" BackColor="WhiteSmoke" ShadowColor="Transparent">
								<area3dstyle Rotation="20" Perspective="10" Enable3D="True" Inclination="10" IsRightAngleAxes="False" WallWidth="0" IsClustered="False" />							
							    <axisy LineDashStyle="DashDotDot">
                                    <MajorGrid LineDashStyle="Dot" />
                                    <MinorGrid LineDashStyle="Dot" />
                                    <MajorTickMark LineDashStyle="Dot" />
                                    <MinorTickMark LineDashStyle="Dot" />
                                    <LabelStyle Enabled="False" />
                                </axisy>
                                <axisx LineDashStyle="Dot">
                                    <MajorGrid LineDashStyle="Dot" />
                                    <MinorGrid LineDashStyle="Dot" />
                                    <LabelStyle Font="Trebuchet MS, 7pt" />
                                </axisx>
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
                        Visible="False" Width="500px" Height="400px">
                        <borderskin SkinStyle="Emboss"></borderskin>
                        <Series>
                            <asp:Series Name="Series1" ChartType="Line" Color="#990000" 
                                Font="Microsoft Sans Serif, 6pt"  MarkerSize="7" MarkerBorderColor="#990000" MarkerBorderWidth="1" MarkerColor="#990000" MarkerStyle="Circle" >
                            </asp:Series>
                            <asp:Series Name="Series2" ChartType="Line" Color="#000099" 
                                Font="Microsoft Sans Serif, 6pt"  MarkerSize="7" MarkerBorderColor="#000099" MarkerBorderWidth="1" MarkerColor="#000099" MarkerStyle="Circle" >
                            </asp:Series>
                            <asp:Series Name="Series3" ChartType="Line" Color="Yellow" 
                                Font="Microsoft Sans Serif, 6pt"  MarkerSize="7" 
                                MarkerBorderColor="Yellow" MarkerBorderWidth="1" MarkerColor="Yellow" 
                                MarkerStyle="Circle" >
                            </asp:Series>
                            <asp:Series Name="Series4" ChartType="Line" Color="0, 192, 0" 
                                Font="Microsoft Sans Serif, 6pt"  MarkerSize="7" 
                                MarkerBorderColor="0, 192, 0" MarkerBorderWidth="1" MarkerColor="0, 192, 0" 
                                MarkerStyle="Circle" >
                            </asp:Series>
                        </Series>
                        <ChartAreas>
							<asp:ChartArea Name="ChartArea1" BorderColor="64, 64, 64, 64" BackSecondaryColor="White" BackColor="WhiteSmoke" ShadowColor="Transparent">
								<area3dstyle Rotation="20" Perspective="10" Enable3D="True" Inclination="10" IsRightAngleAxes="False" WallWidth="0" IsClustered="False" />							
							    <axisy LineDashStyle="DashDotDot">
                                    <MajorGrid LineDashStyle="Dot" />
                                    <MinorGrid LineDashStyle="Dot" />
                                    <MajorTickMark LineDashStyle="Dot" />
                                    <MinorTickMark LineDashStyle="Dot" />
                                    <LabelStyle Enabled="False" />
                                </axisy>
                                <axisx LineDashStyle="Dot">
                                    <MajorGrid LineDashStyle="Dot" />
                                    <MinorGrid LineDashStyle="Dot" />
                                    <LabelStyle Font="Trebuchet MS, 7pt" />
                                </axisx>
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
                    <asp:Button ID="btnExportarpor" runat="server" CssClass="btn8" OnClick="btnExportarpor_Click"
                        OnClientClick="btnExportarpor_Click" 
                         Text="Exportar Gráfica" />
                </td>
            </tr>            
            <tr>
                <td colspan="2">
                    <div style="overflow: auto; width: 90%;">
                        <asp:GridView ID="gvDatos" runat="server" AutoGenerateColumns="False" 
                            HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager" RowStyle-CssClass="gridItem" ShowFooter="False" 
                            style="font-size:xx-small" Width="90%" GridLines="Both"   >
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
