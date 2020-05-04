<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"CodeFile="Lista.aspx.cs" Inherits="Nuevo" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<%@ Register assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI.DataVisualization.Charting" tagprefix="asp" %>
<%@ Register src="../../../General/Controles/fecha.ascx" tagname="fecha" tagprefix="ucFecha" %>

<%@ Register src="../../../General/Controles/ctlColorPicker.ascx" tagname="ctlColorPicker" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table style="width: 80%">
        <tr>
            <td style="text-align: left; font-size: small;" colspan="5">
                <strong>Criterios de Generación:</strong></td>
            <td style="text-align: left; font-size: small;">
                &nbsp;</td>
            <td style="text-align: center; width: 497px;">                            
                &nbsp;</td>
            <td class="logo" style="width: 194px">
                &nbsp;</td>
        </tr>
        <tr>
            <td  style="text-align: left">
                Año<br />
                <asp:DropDownList ID="ddlFechaCorte" runat="server" Height="24px" 
                    Width="165px" AutoPostBack="True" 
                    onselectedindexchanged="ddlFechaCorte_SelectedIndexChanged">                           
                </asp:DropDownList>
                <br />
            </td>
            <td  style="text-align: left">
                <br />
                <asp:DropDownList ID="ddlPeriodo" runat="server" Height="24px" 
                    Width="165px" onselectedindexchanged="ddlPeriodo_SelectedIndexChanged" Visible="false"
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
                &nbsp;</td>
            <td style="text-align: left; width: 200px;">      
                <asp:CheckBox ID="ch3D" runat="server" oncheckedchanged="ch3D_CheckedChanged" 
                    Text="Mostrar en 3D" AutoPostBack="True" />                   
            </td>
            <td style="text-align: left; width: 200px;">      
                <asp:DropDownList ID="ddlTipoGrafica" runat="server" Height="24px" 
                        Width="165px" 
                        AutoPostBack="True">
                        <asp:ListItem Value="1">Barras</asp:ListItem>
                        <asp:ListItem Value="2">Pie</asp:ListItem>
                        <asp:ListItem Value="3">Lineas</asp:ListItem>
                        <asp:ListItem Value="4">Area</asp:ListItem>
                    </asp:DropDownList>
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
                            <asp:Series Name="Series1" Color="#009999">
                            </asp:Series>
                        </Series>
                        <Legends>
                            <asp:Legend Alignment="Center" Docking="Bottom" IsTextAutoFit="False" Name="Default"
                                LegendStyle="Row" Font="Trebuchet MS, 8.25pt, style=Bold" />
                        </Legends>
						<chartareas>
							<asp:ChartArea Name="ChartArea1" BorderColor="64, 64, 64, 64" BackSecondaryColor="White" BackColor="64, 165, 191, 228" ShadowColor="Transparent" BackGradientStyle="TopBottom">
							    <area3dstyle Enable3D="True" Inclination="10" IsClustered="False" IsRightAngleAxes="False" LightStyle="Realistic" Perspective="10" Rotation="10" WallWidth="0" />
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
                    <asp:Chart ID="Chart2" runat="server" BackColor="42, 58, 87" BackGradientStyle="TopBottom" BackSecondaryColor="White" BorderColor="26, 59, 105" BorderDashStyle="Solid" BorderlineDashStyle="Solid" BorderWidth="2px" Height="400px" Visible="False" Width="500px">
                        <borderskin SkinStyle="Emboss" />
                        <Series>
                            <asp:Series Color="#009999" Name="Series1">
                            </asp:Series>
                        </Series>
                        <Legends>
                            <asp:Legend Alignment="Center" Docking="Bottom" Font="Trebuchet MS, 8.25pt, style=Bold" IsTextAutoFit="False" LegendStyle="Row" Name="Default" />
                        </Legends>
                        <chartareas>
                            <asp:ChartArea BackColor="64, 165, 191, 228" BackGradientStyle="TopBottom" BackSecondaryColor="White" BorderColor="64, 64, 64, 64" Name="ChartArea1" ShadowColor="Transparent">
                                <area3dstyle Enable3D="True" Inclination="10" IsClustered="False" IsRightAngleAxes="False" LightStyle="Realistic" Perspective="10" Rotation="10" WallWidth="0" />
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
                            <asp:Title Font="Arial, 11pt , style=Bold" ForeColor="#FFFFFF" Name="Title1" Text="" TextStyle="Shadow">
                            </asp:Title>
                            <asp:Title Font="Arial, 8pt , style=Bold" ForeColor="#FFFFFF" Name="Title2" Text="" TextStyle="Shadow">
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
                        OnClientClick="btnExportar_Click" Text="Exportar Gráfica" />
                    <asp:GridView ID="gvDatosAfiliaciones" runat="server" AutoGenerateColumns="False" GridLines="Both" HeaderStyle-CssClass="gridHeader" Height="122px" PagerStyle-CssClass="gridPager" RowStyle-CssClass="gridItem" ShowFooter="False" style="font-size:xx-small" Width="500px">
                        <Columns>
                            <asp:BoundField DataField="Año" HeaderText="Año">
                            <ItemStyle width="85px" />

                            </asp:BoundField>
                               <asp:BoundField DataField="mesgrafica" HeaderText="Mes">
                            <ItemStyle width="85px" />

                            </asp:BoundField>
                            <asp:BoundField DataField="numero" DataFormatString="{0:n}" HeaderText="Cantidad">
                            <ItemStyle width="85px" />
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
                    <asp:GridView ID="gvDatosRetiros" runat="server" AutoGenerateColumns="False" GridLines="Both" HeaderStyle-CssClass="gridHeader" Height="122px" PagerStyle-CssClass="gridPager" RowStyle-CssClass="gridItem" ShowFooter="False" style="font-size:xx-small" Width="500px">
                        <Columns>
                            <asp:BoundField DataField="Año" HeaderText="Año">
                            <ItemStyle width="85px" />

                            </asp:BoundField>
                               <asp:BoundField DataField="mesgrafica" HeaderText="Mes">
                            <ItemStyle width="85px" />

                            </asp:BoundField>
                            <asp:BoundField DataField="numero" DataFormatString="{0:n}" HeaderText="Cantidad">
                            <ItemStyle width="85px" />
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
