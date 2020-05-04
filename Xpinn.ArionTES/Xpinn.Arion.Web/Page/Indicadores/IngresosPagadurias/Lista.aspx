<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Lista.aspx.cs" Inherits="Lista" %>

<%@ Register Src="../../../General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Src="../../../General/Controles/ctlseleccionmultipledropdown.ascx" TagName="dropdownmultiple" TagPrefix="ucDrop" %>
<%@ Register Src="../../../General/Controles/Ctlmultiempresas.ascx" TagName="ddlpagadurias" TagPrefix="ucPagaduria" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<%@ Register Src="../../../General/Controles/ctlColorPicker.ascx" TagName="ctlColorPicker" TagPrefix="uc1" %>

<%@ Register src="../../../General/Controles/fecha.ascx" tagname="fecha" tagprefix="uc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table style="width: 100%">
        <tr>
            <td style="text-align: left; font-size: small;" colspan="6">
                <strong>Criterios de Generación:</strong>
            </td>
        </tr>
        <tr>
            <td style="text-align: left">
                Fecha Inicial<br />
                <uc:fecha ID="txtFecIni" runat="server" CssClass="textbox" Width="100px" />
                <br />
            </td>
            <td style="text-align: left">
                Fecha Final<br />
                <uc:fecha ID="txtFecFin" runat="server" CssClass="textbox" Width="100px" />
            </td>
            <td style="text-align: left">
                &nbsp;Color de Fondo<br />
                <uc1:ctlColorPicker ID="txtColorFondo" runat="server" />
                <ucDrop:dropdownmultiple ID="ddloficina" runat="server" Height="24px" 
                    Width="200px" Visible="False">
                </ucDrop:dropdownmultiple>
            </td>
            <td style="text-align: left">
                Tipo de Gráfica <br />
                <asp:DropDownList ID="ddlTipoGrafica" runat="server" Height="24px" Width="165px">
                    <asp:ListItem Value="1">Barras</asp:ListItem>
                    <asp:ListItem Value="2">Pie</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td style="text-align: left">
                <asp:CheckBox ID="ch3D" runat="server" OnCheckedChanged="ch3D_CheckedChanged" Width="120px"
                    Text="Mostrar en 3D" AutoPostBack="True" />
                &nbsp;&nbsp;&nbsp;
                <asp:CheckBox ID="chkmostrarlegendas" runat="server" Text="Mostrar Legendas" width="197px" Checked="true"/>
                <br />
            </td>
            <td style="text-align: left">
                <asp:Button ID="btnInforme" runat="server" CssClass="btn8" OnClick="btnInforme_Click"
                    OnClientClick="btnInforme_Click" Text="Generar Reporte" />
            </td>
        </tr>
    </table>
    <asp:Panel ID="PanelComprobanteImpr" runat="server">
        <table  width="95%">
            <tr>
                <td colspan="2" style="text-align: left; width: 100%">
                    <asp:Chart ID="Chart1" runat="server" BackColor="42, 58, 87" BackGradientStyle="TopBottom"                     
                        BackSecondaryColor="White" BorderDashStyle="Solid" BorderWidth="2px" 
                        BorderlineDashStyle="Solid" BorderColor="26, 59, 105"
                        Visible="False" Height="500px" Width="800px">
                        <borderskin SkinStyle="Emboss"></borderskin>
                        <Series>
                            <asp:Series Color="#99FFCC" Name="Series1">
                            </asp:Series>
                        </Series>
                        <Legends>
                            <asp:Legend Alignment="Center" Docking="Right" IsTextAutoFit="False" Name="Default"
                                LegendStyle="Column" />
                        </Legends>
						<chartareas>
							<asp:ChartArea Name="ChartArea1" BorderColor="64, 64, 64, 64" BackSecondaryColor="White" BackColor="64, 165, 191, 228" ShadowColor="Transparent" BackGradientStyle="TopBottom">
								<area3dstyle Rotation="10" Perspective="10" Enable3D="True" Inclination="10" IsRightAngleAxes="False" WallWidth="0" IsClustered="False" LightStyle="Realistic" />
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
            </tr>
            <tr>
                <td colspan="2" style="text-align: left">
                    <asp:Button ID="btnExportar" runat="server" CssClass="btn8" OnClick="btnExportar_Click"
                        OnClientClick="btnExportar_Click" Text="Exportar Gráfica" /> &#160;&#160;&#160;
                    <asp:Button ID="Button1" runat="server" CssClass="btn8" OnClick="btnExportarData_Click"
                        OnClientClick="btnExportarData_Click" Text="Exportar Datos" />
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>            
            <tr>
                <td colspan="2" style="text-align: left">
                    <div style="overflow: auto; width: 100%;">
                        <asp:GridView ID="gvDatos" runat="server" AutoGenerateColumns="False" 
                            HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager" RowStyle-CssClass="gridItem" ShowFooter="False" 
                            style="font-size:xx-small" Width="95%" GridLines="Both"   >
                            <Columns>
                                 <asp:BoundField DataField="nombre" HeaderText="Oficina">
                                    <ItemStyle HorizontalAlign="left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="valor" HeaderText="Monto" DataFormatString="{0:n2}">
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
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
