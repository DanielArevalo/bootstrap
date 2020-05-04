<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="CarteraVencidaOficinas.aspx.cs" Inherits="Nuevo" %>

<%@ Register Src="../../../General/Controles/ctlBusquedaRapida.ascx" TagName="BusquedaRapida" TagPrefix="uc1" %>
<%@ Register Src="../../../General/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc1" %>
<%@ Register Src="../../../General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<%@ Register src="../../../General/Controles/ctlColorPicker.ascx" tagname="ctlColorPicker" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:Label ID="lablerror" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="lablerror0" runat="server" Visible="false"></asp:Label>
    <table style="width: 70%">
        <tr>
            <td  style="text-align: left">
                Fecha de Corte<br />
                <asp:DropDownList ID="ddlFechaCorte" runat="server" Height="24px" 
                    Width="165px" AutoPostBack="True" 
                    onselectedindexchanged="ddlFechaCorte_SelectedIndexChanged">                           
                </asp:DropDownList>
                <br />
            </td>
            <td  style="text-align: left">
                Periodo:<br />
                <asp:DropDownList ID="ddlPeriodo" runat="server" Height="24px" 
                    Width="165px" onselectedindexchanged="ddlPeriodo_SelectedIndexChanged" 
                    AutoPostBack="True">
                    <asp:ListItem Value="1">Anual</asp:ListItem>
                    <asp:ListItem Value="2">Semetral</asp:ListItem>
                    <asp:ListItem Value="3">Trimestral</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td style="text-align: left; width: 200px;">      
                Color de Fondo<br />
                <uc1:ctlColorPicker ID="txtColorFondo" runat="server" />
            </td>
            <td style="text-align: left; width: 200px;">      
                <asp:CheckBox ID="ch3D" runat="server" oncheckedchanged="ch3D_CheckedChanged" 
                    Text="Mostrar en 3D" AutoPostBack="True" />                   
            </td>
            <td>
                <asp:Button ID="btnInforme" runat="server" CssClass="btn8" OnClick="btnInforme_Click"
                OnClientClick="btnInforme_Click" Text="Generar Reporte" />
            </td>
        </tr>
    </table>
    <asp:Panel ID="PanelComprobanteImpr" runat="server">
        <table  width="90%">
            <tr>
                <td>
                    <asp:Chart ID="Chart1" runat="server" BackColor="42, 58, 87" BackGradientStyle="TopBottom"                     
                        BackSecondaryColor="White" BorderDashStyle="Solid" BorderWidth="2px" 
                        BorderlineDashStyle="Solid" BorderColor="26, 59, 105"
                        Visible="False" Width="500px" Height="400px">
                        <borderskin SkinStyle="Emboss"></borderskin>
                        <Series>
                            <asp:Series Name="Series1" Color="DarkMagenta" ChartType="Line" 
                                Font="Microsoft Sans Serif, 7pt, style=Bold" IsValueShownAsLabel="True" 
                                MarkerSize="7" MarkerBorderColor="#CC0099" MarkerBorderWidth="1" 
                                MarkerColor="#CC0099" MarkerStyle="Circle" LabelForeColor="MediumOrchid">
                            </asp:Series>
                            <asp:Series Name="Series2" Color="DarkSeaGreen" ChartType="Line" 
                                Font="Microsoft Sans Serif, 7pt" IsValueShownAsLabel="True" MarkerSize="7" MarkerBorderColor="DarkSeaGreen" MarkerBorderWidth="1" MarkerColor="DarkSeaGreen" MarkerStyle="Circle" >
                            </asp:Series>
                            <asp:Series Name="Series3" Color="DodgerBlue" ChartType="Line" 
                                Font="Microsoft Sans Serif, 7pt" IsValueShownAsLabel="True" MarkerSize="7" MarkerBorderColor="DodgerBlue" MarkerBorderWidth="1" MarkerColor="DodgerBlue" MarkerStyle="Circle" >
                            </asp:Series>
                            <asp:Series Name="Series4" Color="Crimson" ChartType="Line" 
                                Font="Microsoft Sans Serif, 7pt" IsValueShownAsLabel="True" MarkerSize="7" MarkerBorderColor="Crimson" MarkerBorderWidth="1" MarkerColor="Crimson" MarkerStyle="Circle" >
                            </asp:Series>
                        </Series>
                        <Legends>
                            <asp:Legend Alignment="Center" Docking="Bottom" IsTextAutoFit="False" Name="Default"
                                LegendStyle="Row" Font="Trebuchet MS, 8pt, style=Bold" />
                        </Legends>
						<chartareas>
							<asp:ChartArea Name="ChartArea1" BorderColor="64, 64, 64, 64" BackSecondaryColor="Transparent" BackColor="White" ShadowColor="Transparent" BackGradientStyle="TopBottom" BorderDashStyle="DashDot">
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
						</chartareas>
                       <Titles>
                            <asp:Title Name="Title1" Text="" ForeColor="#FFFFFF" Font="Arial, 11pt, style=Bold" TextStyle="Shadow">
                            </asp:Title>
                            <asp:Title Name="Title2" Text="" ForeColor="#FFFFFF" Font="Arial, 8pt, style=Bold" TextStyle="Shadow">
                            </asp:Title>
                        </Titles>
					    <annotations>
					    </annotations>
                    </asp:Chart>
                </td>
                <td>
                   <asp:Chart ID="Chart2" runat="server" BackColor="42, 58, 87" BackGradientStyle="TopBottom"                     
                        BackSecondaryColor="White" BorderDashStyle="Solid" BorderWidth="2px" 
                        BorderlineDashStyle="Solid" BorderColor="26, 59, 105"
                        Visible="False" Width="500px" Height="400px">
                        <borderskin SkinStyle="Emboss"></borderskin>
                        <Series>
                            <asp:Series Name="Series1" Color="DarkMagenta" ChartType="Line" 
                                Font="Microsoft Sans Serif, 7pt, style=Bold" IsValueShownAsLabel="True" 
                                MarkerSize="7" MarkerBorderColor="#CC0099" MarkerBorderWidth="1" 
                                MarkerColor="#CC0099" MarkerStyle="Circle" LabelForeColor="MediumOrchid">
                            </asp:Series>
                            <asp:Series Name="Series2" Color="DarkSeaGreen" ChartType="Line" 
                                Font="Microsoft Sans Serif, 7pt" IsValueShownAsLabel="True" MarkerSize="7" MarkerBorderColor="DarkSeaGreen" MarkerBorderWidth="1" MarkerColor="DarkSeaGreen" MarkerStyle="Circle" >
                            </asp:Series>
                            <asp:Series Name="Series3" Color="DodgerBlue" ChartType="Line" 
                                Font="Microsoft Sans Serif, 7pt" IsValueShownAsLabel="True" MarkerSize="7" MarkerBorderColor="DodgerBlue" MarkerBorderWidth="1" MarkerColor="DodgerBlue" MarkerStyle="Circle" >
                            </asp:Series>
                            <asp:Series Name="Series4" Color="Crimson" ChartType="Line" 
                                Font="Microsoft Sans Serif, 7pt" IsValueShownAsLabel="True" MarkerSize="7" MarkerBorderColor="Crimson" MarkerBorderWidth="1" MarkerColor="Crimson" MarkerStyle="Circle" >
                            </asp:Series>
                        </Series>
                        <Legends>
                            <asp:Legend Alignment="Center" Docking="Bottom" IsTextAutoFit="False" Name="Default"
                                LegendStyle="Row" Font="Trebuchet MS, 8pt, style=Bold" />
                        </Legends>
						<chartareas>
							<asp:ChartArea Name="ChartArea1" BorderColor="64, 64, 64, 64" BackSecondaryColor="Transparent" BackColor="White" ShadowColor="Transparent" BackGradientStyle="TopBottom" BorderDashStyle="DashDot">
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
						</chartareas>
                        <Titles>
                            <asp:Title Name="Title1" Text="" ForeColor="#FFFFFF" Font="Arial, 11pt, style=Bold" TextStyle="Shadow">
                            </asp:Title>
                            <asp:Title Name="Title2" Text="" ForeColor="#FFFFFF" Font="Arial, 8pt, style=Bold" TextStyle="Shadow">
                            </asp:Title>
                        </Titles>
					    <annotations>
					    </annotations>
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
        </table>   
    </asp:Panel>

 </asp:Content>
