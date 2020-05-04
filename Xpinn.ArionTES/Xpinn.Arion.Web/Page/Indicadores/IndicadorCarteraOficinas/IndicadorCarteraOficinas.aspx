<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="IndicadorCarteraOficinas.aspx.cs" Inherits="Nuevo" %>

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
            <td  style="text-align: left">
                Comparar con Período:<br />
                <asp:DropDownList ID="ddlPeriodo" runat="server" Height="24px" 
                    Width="165px" AppendDataBoundItems="True" AutoPostBack="True" 
                    onselectedindexchanged="ddlPeriodo_SelectedIndexChanged">
                    <asp:ListItem Value=" "> </asp:ListItem>
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
                    Tipo de Gráfica&nbsp;&nbsp;&nbsp;<asp:DropDownList ID="ddlTipoGrafica1" runat="server" Height="24px" 
                        Width="165px" onselectedindexchanged="ddlTipoGrafica1_SelectedIndexChanged" 
                        AutoPostBack="True">
                        <asp:ListItem Value="1">Barras</asp:ListItem>
                        <asp:ListItem Value="2">Pie</asp:ListItem>
                        <asp:ListItem Value="3">Lineas</asp:ListItem>
                        <asp:ListItem Value="4">Area</asp:ListItem>
                    </asp:DropDownList>
                    <asp:Chart ID="Chart1" runat="server" BackColor="42, 58, 87" BackGradientStyle="TopBottom"                     
                        BackSecondaryColor="White" BorderDashStyle="Solid" BorderWidth="2px" 
                        BorderlineDashStyle="Solid" BorderColor="26, 59, 105"
                        Visible="False" Width="500px" Height="400px">
                        <borderskin SkinStyle="Emboss"></borderskin>
                        <Series>
                            <asp:Series Name="Series1" Color="#009999" >
                            </asp:Series>
                            <asp:Series Name="Series2" Color="#009900" >
                            </asp:Series>                      
                        </Series>
                        <Legends>
                            <asp:Legend Alignment="Center" Docking="Bottom" IsTextAutoFit="False" Name="Default"
                                LegendStyle="Row" Font="Trebuchet MS, 8.25pt, style=Bold" />
                        </Legends>
						<chartareas>
							<asp:ChartArea Name="ChartArea1" BorderColor="64, 64, 64, 64" BackSecondaryColor="Transparent" BackColor="64, 165, 191, 228" ShadowColor="Transparent" BackGradientStyle="TopBottom">
								<area3dstyle Rotation="10" Perspective="10" Enable3D="True" Inclination="10" IsRightAngleAxes="False" WallWidth="0" IsClustered="False" />
								<axisy LineColor="64, 64, 64, 64">
									<LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" />
									<MajorGrid LineColor="64, 64, 64, 64" />
								</axisy>
								<axisx LineColor="64, 64, 64, 64">
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
                    Tipo de Gráfica&nbsp;&nbsp;&nbsp;<asp:DropDownList ID="ddlTipoGrafica2" runat="server" Height="24px" 
                        Width="165px" onselectedindexchanged="ddlTipoGrafica2_SelectedIndexChanged" 
                        AutoPostBack="True">
                        <asp:ListItem Value="1">Barras</asp:ListItem>
                        <asp:ListItem Value="2">Pie</asp:ListItem>
                        <asp:ListItem Value="3">Lineas</asp:ListItem>
                        <asp:ListItem Value="4">Area</asp:ListItem>
                    </asp:DropDownList>
                    <asp:Chart ID="Chart2" runat="server" BackColor="42, 58, 87" BackGradientStyle="TopBottom"
                        BackSecondaryColor="White" BorderDashStyle="Solid" BorderWidth="2px"
                        BorderlineDashStyle="Solid" BorderColor="26, 59, 105"
                        Visible="False" Width="500px" Height="400px">
                        <borderskin SkinStyle="Emboss"></borderskin>
                        <Series>
                            <asp:Series Color="#000099" Name="Series1">
                            </asp:Series>
                            <asp:Series Color="#999900" Name="Series2">
                            </asp:Series>                                               
                        </Series>
                        <Legends>
                            <asp:Legend Alignment="Center" Docking="Bottom" IsTextAutoFit="False" Name="Default"
                                LegendStyle="Row" />
                        </Legends>
                        <ChartAreas>
							<asp:ChartArea Name="ChartArea1" BorderColor="64, 64, 64, 64" BackSecondaryColor="Transparent" BackColor="64, 165, 191, 228" ShadowColor="Transparent" BackGradientStyle="TopBottom">
								<area3dstyle Rotation="10" Perspective="10" Enable3D="True" Inclination="10" IsRightAngleAxes="False" WallWidth="0" IsClustered="False" />
								<axisy LineColor="64, 64, 64, 64">
									<LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" />
									<MajorGrid LineColor="64, 64, 64, 64" />
								</axisy>
								<axisx LineColor="64, 64, 64, 64">
									<LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" />
									<MajorGrid LineColor="64, 64, 64, 64" />
								</axisx>
							</asp:ChartArea>
                        </ChartAreas>
                        <Titles>
                            <asp:Title Name="Title1" Text="" ForeColor="#FFFFFF" Font="Arial, 11pt , style=Bold" TextStyle="Shadow">
                            </asp:Title>
                        </Titles>
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
