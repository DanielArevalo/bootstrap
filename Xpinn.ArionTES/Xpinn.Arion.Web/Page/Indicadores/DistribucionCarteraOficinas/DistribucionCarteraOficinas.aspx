<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="DistribucionCarteraOficinas.aspx.cs" Inherits="Nuevo" %>

<%@ Register Src="../../../General/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc1" %>
<%@ Register Src="../../../General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<%@ Register src="../../../General/Controles/ctlColorPicker.ascx" tagname="ctlColorPicker" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table style="width: 80%">
        <tr>
            <td style="text-align: left; font-size: small;" colspan="4">
                <strong>Criterios de Generación:</strong></td>
            <td style="text-align: center; width: 497px;">                            
                &nbsp;</td>
            <td class="logo" style="width: 194px">
                &nbsp;</td>
        </tr>
        <tr>
            <td  style="text-align: left">
                Fecha de Corte<br />
                <asp:DropDownList ID="ddlVencimmiento" runat="server" Height="24px" 
                    Width="165px" AutoPostBack="True" 
                    onselectedindexchanged="ddlVencimmiento_SelectedIndexChanged">                           
                </asp:DropDownList>
                <br />
            </td>
            <td style="text-align: left; width: 200px;">      
                Color de Fondo<br />
                <uc1:ctlColorPicker ID="txtColorFondo" runat="server" />
            </td>
            <td style="text-align: left; width: 200px;">      
                <asp:CheckBox ID="ch3D" runat="server" oncheckedchanged="ch3D_CheckedChanged" 
                    Text="Mostrar en 3D" AutoPostBack="True" />                   
            </td>
            <td style="text-align: left">
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
                            <asp:Series Name="Series1" Color="Blue" ChartType="StackedColumn100" Font="Calibri, 7pt"  >
                            </asp:Series>
                            <asp:Series Name="Series2" Color="255, 128, 128" ChartType="StackedColumn100" Font="Calibri, 7pt" >
                            </asp:Series>
                            <asp:Series Name="Series3" Color="128, 255, 128" ChartType="StackedColumn100" Font="Calibri, 7pt" >
                            </asp:Series>
                            <asp:Series Name="Series4" Color="255, 128, 255" ChartType="StackedColumn100" Font="Calibri, 7pt" >
                            </asp:Series>
                            <asp:Series Name="Series5" Color="Aqua" ChartType="StackedColumn100" Font="Calibri, 7pt" >
                            </asp:Series>
                            <asp:Series Name="Series6" Color="255, 128, 0" ChartType="StackedColumn100" Font="Calibri, 7pt" >
                            </asp:Series>
                            <asp:Series Name="Series7" Color="255, 255, 128" ChartType="StackedColumn100" Font="Calibri, 7pt" >
                            </asp:Series>
                        </Series>
                        <Legends>
                            <asp:Legend Alignment="Center" Docking="Bottom" IsTextAutoFit="False" 
                                Name="Default" Font="Calibri, 7pt" />
                        </Legends>
						<chartareas>
							<asp:ChartArea Name="ChartArea1" BorderColor="64, 64, 64, 64" BorderDashStyle="Solid" BackSecondaryColor="Transparent" BackColor="64, 165, 191, 228" ShadowColor="Transparent" BackGradientStyle="TopBottom">
									<InnerPlotPosition Height="66.49457" Width="90.86936" X="7.98935" 
                                        Y="16.28261" />
									<area3dstyle Rotation="10" Inclination="15" WallWidth="0" />
									<position Y="3" Height="92" Width="92" X="2"></position>
									<axisy LineColor="64, 64, 64, 64"  LabelAutoFitMaxFontSize="8">
										<LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" />
										<MajorGrid LineColor="64, 64, 64, 64" />
									</axisy>
									<axisx LineColor="64, 64, 64, 64"  LabelAutoFitMaxFontSize="6">
										<LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" />
										<MajorGrid LineColor="64, 64, 64, 64" />
									</axisx>
								</asp:ChartArea>
						</chartareas>
                       <Titles>
                            <asp:Title Name="Title1" Text="" ForeColor="#FFFFFF" Font="Arial, 11pt , style=Bold" TextStyle="Shadow">
                            </asp:Title>
                            <asp:Title Name="Title2" Text="" ForeColor="#FFFFFF" Font="Arial, 8pt , style=Bold" TextStyle="Shadow">
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
                            <asp:Series Name="Series1" Color="Blue" ChartType="StackedColumn100" Font="Calibri, 7pt"  >
                            </asp:Series>
                            <asp:Series Name="Series2" Color="255, 128, 128" ChartType="StackedColumn100" Font="Calibri, 7pt" >
                            </asp:Series>
                            <asp:Series Name="Series3" Color="128, 255, 128" ChartType="StackedColumn100" Font="Calibri, 7pt" >
                            </asp:Series>
                            <asp:Series Name="Series4" Color="255, 128, 255" ChartType="StackedColumn100" Font="Calibri, 7pt" >
                            </asp:Series>
                            <asp:Series Name="Series5" Color="Aqua" ChartType="StackedColumn100" Font="Calibri, 7pt" >
                            </asp:Series>
                            <asp:Series Name="Series6" Color="255, 128, 0" ChartType="StackedColumn100" Font="Calibri, 7pt" >
                            </asp:Series>
                            <asp:Series Name="Series7" Color="255, 255, 128" ChartType="StackedColumn100" Font="Calibri, 7pt" >
                            </asp:Series>
                        </Series>
                        <Legends>
                            <asp:Legend Alignment="Center" Docking="Bottom" IsTextAutoFit="False" 
                                Name="Default" Font="Calibri, 7pt" />
                        </Legends>
						<chartareas>
							<asp:ChartArea Name="ChartArea1" BorderColor="64, 64, 64, 64" BorderDashStyle="Solid" BackSecondaryColor="Transparent" BackColor="64, 165, 191, 228" ShadowColor="Transparent" BackGradientStyle="TopBottom">
									<InnerPlotPosition Height="66.49457" Width="90.86936" X="7.98935" 
                                        Y="16.28261" />
									<area3dstyle Rotation="10" Inclination="15" WallWidth="0" />
									<position Y="3" Height="92" Width="92" X="2"></position>
									<axisy LineColor="64, 64, 64, 64"  LabelAutoFitMaxFontSize="8">
										<LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" />
										<MajorGrid LineColor="64, 64, 64, 64" />
									</axisy>
									<axisx LineColor="64, 64, 64, 64"  LabelAutoFitMaxFontSize="6">
										<LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" />
										<MajorGrid LineColor="64, 64, 64, 64" />
									</axisx>
								</asp:ChartArea>
						</chartareas>
                        <Titles>
                            <asp:Title Name="Title1" Text="" ForeColor="#FFFFFF" Font="Arial, 11pt , style=Bold" TextStyle="Shadow">
                            </asp:Title>
                            <asp:Title Name="Title2" Text="" ForeColor="#FFFFFF" Font="Arial, 8pt , style=Bold" TextStyle="Shadow">
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
