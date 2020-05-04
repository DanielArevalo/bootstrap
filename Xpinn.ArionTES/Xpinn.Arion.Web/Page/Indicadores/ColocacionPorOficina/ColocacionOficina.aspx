<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="ColocacionOficina.aspx.cs" Inherits="Nuevo" %>

<%@ Register Src="../../../General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Src="../../../General/Controles/ctlseleccionmultipledropdown.ascx" TagName="dropdownmultiple" TagPrefix="ucDrop" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<%@ Register Src="../../../General/Controles/ctlColorPicker.ascx" TagName="ctlColorPicker" TagPrefix="uc1" %>

<%@ Register src="../../../General/Controles/fecha.ascx" tagname="fecha" tagprefix="uc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table style="width: 85%">
        <tr>
            <td style="text-align: left; font-size: small;" colspan="3">
                <strong>Criterios de Generación:</strong></td>
            <td style="text-align: center; width: 497px;">                            
                &nbsp;</td>
            <td style="text-align: center; width: 497px;">                            
                &nbsp;</td>
            <td style="text-align: left">
                &nbsp;</td>
            <td class="logo" style="width: 194px">
                &nbsp;</td>
        </tr>
        <tr>
            <td  style="text-align: left">
                Fecha de Corte<br />
                <asp:DropDownList ID="ddlVencimmiento" runat="server" Height="24px" 
                    Width="120px" AutoPostBack="True" 
                    onselectedindexchanged="ddlVencimmiento_SelectedIndexChanged">                           
                </asp:DropDownList>
                <br />
            </td>
            <td  style="text-align: left">
                Comparar Con:<br />
                <asp:DropDownList ID="ddlPeriodo" runat="server" Height="24px" Width="120px" AutoPostBack="True" 
                    onselectedindexchanged="ddlVencimmiento_SelectedIndexChanged" AppendDataBoundItems="true">                           
                    <asp:ListItem></asp:ListItem>
                </asp:DropDownList>
                <br />
            </td>
            <td  style="text-align: left">
                <br />
                Oficina:
                <ucDrop:dropdownmultiple ID="ddloficina" runat="server" Height="24px" 
                    Width="165px">                           
                </ucDrop:dropdownmultiple>
                <br />
            </td>
            <td style="text-align: left">
                Linea de Crédito:<br />
                <ucDrop:dropdownmultiple ID="ddlLinea" runat="server" Height="24px" Width="200px">
                </ucDrop:dropdownmultiple>
            </td>
            <td style="text-align: left" colspan="3">
                Color de Fondo<br />
                <uc1:ctlColorPicker ID="txtColorFondo" runat="server" />
            </td>
            <td style="text-align: left; width: 797px;">      
                <asp:CheckBox ID="ch3D" runat="server" oncheckedchanged="ch3D_CheckedChanged" 
                    Text="Mostrar en 3D" AutoPostBack="True" />                   
            </td>            
            <td style="text-align: left; width: 797px;">      
                <asp:CheckBox ID="cbNumero" runat="server" oncheckedchanged="cbNumero_CheckedChanged" 
                    Text="Mostrar Número" AutoPostBack="True" />                   
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
                    <table>
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
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Chart ID="Chart1" runat="server" BackGradientStyle="TopBottom"                     
                                    BackSecondaryColor="White" BorderDashStyle="Solid" BorderWidth="2px" 
                                    BorderlineDashStyle="Solid" BorderColor="26, 59, 105"
                                    Visible="False" Width="500px" Height="400px">
                                    <borderskin SkinStyle="Emboss"></borderskin>
                                    <Series>
                                        <asp:Series Name="Series1" Color="#009999" 
                                            Font="Microsoft Sans Serif, 7pt" IsValueShownAsLabel="True" 
                                            LabelBorderDashStyle="NotSet" >
                                            <SmartLabelStyle AllowOutsidePlotArea="Yes" />
                                        </asp:Series>
                                        <asp:Series Name="Series2" Color="#009900" >
                                        </asp:Series>                      
                                    </Series>
                                    <Legends>
                                        <asp:Legend Alignment="Center" Docking="Bottom" IsTextAutoFit="False" 
                                            Name="Default" Font="Trebuchet MS, 6pt, style=Bold" TableStyle="Wide" />
                                    </Legends>
						            <chartareas>
							            <asp:ChartArea Name="ChartArea1" BorderColor="64, 64, 64, 64" BackSecondaryColor="Transparent" BackColor="64, 165, 191, 228" ShadowColor="Transparent" BackGradientStyle="TopBottom">
								            <area3dstyle Rotation="10" Perspective="10" Enable3D="True" Inclination="10" IsRightAngleAxes="False" WallWidth="0" IsClustered="False" />
								            <axisy LineColor="64, 64, 64, 64">
									            <LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" />
									            <MajorGrid LineColor="64, 64, 64, 64" />
								            </axisy>
								            <axisx LineColor="64, 64, 64, 64" IsLabelAutoFit="False" 
                                                LabelAutoFitMaxFontSize="8" MaximumAutoSize="90">
									            <LabelStyle Font="Trebuchet MS, 8pt, style=Bold" />
									            <MajorGrid LineColor="64, 64, 64, 64" />
								                <ScaleBreakStyle StartFromZero="No" />
								            </axisx>
							            </asp:ChartArea>
						            </chartareas>
                                    <Titles>
                                        <asp:Title Name="Title1" Text="" ForeColor="#FFFFFF" Font="Arial, 11pt, style=Bold"  TextStyle="Shadow">
                                        </asp:Title>
                                        <asp:Title Name="Title2" Text="" ForeColor="#FFFFFF" TextStyle="Shadow">
                                        </asp:Title>
                                    </Titles>
					                <annotations>
					                </annotations>
                                </asp:Chart>
                            </td>
                        </tr>
                    </table>
                </td>
                <td>
                    <asp:Label ID="lblbTipoGrafica2" runat="server" Text="Tipo de Gráfica" />
                    &nbsp;&nbsp;&nbsp;
                    <asp:DropDownList ID="ddlTipoGrafica2" runat="server" Height="24px" 
                        Width="165px" onselectedindexchanged="ddlTipoGrafica2_SelectedIndexChanged" 
                        AutoPostBack="True">
                        <asp:ListItem Value="1">Barras</asp:ListItem>
                        <asp:ListItem Value="2">Pie</asp:ListItem>
                        <asp:ListItem Value="3">Lineas</asp:ListItem>
                        <asp:ListItem Value="4">Area</asp:ListItem>
                    </asp:DropDownList>
                    <ucFecha:Fecha Visible="false" ID="txtfecha" runat="server"></ucFecha:Fecha>
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
                            <asp:Title Name="Title1" Text="" ForeColor="#FFFFFF" Font="Arial, 11pt, style=Bold"  TextStyle="Shadow">
                            </asp:Title>
                            <asp:Title Name="Title2" Text="" ForeColor="#FFFFFF" TextStyle="Shadow">
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
                    <div style="overflow: auto; width: 90%; height: 100px">
                        <asp:GridView ID="gvDatos" runat="server" AutoGenerateColumns="False" 
                            HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager" RowStyle-CssClass="gridItem"
                            style="font-size:xx-small" Width="90%" GridLines="Both"   >
                            <Columns>
                                <asp:BoundField DataField="nombre" HeaderText="Linea">
                                    <ItemStyle HorizontalAlign="left" />
                                    <FooterStyle Font-Bold="true" Font-Size="X-Small" HorizontalAlign="center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="valor" HeaderText="Valor Corte" DataFormatString="{0:n2}">
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="true" Font-Size="X-Small" HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="fecha_historico" HeaderText="Periodo">
                                    <ItemStyle HorizontalAlign="center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="valor_comp" HeaderText="Valor Comparacion" DataFormatString="{0:n2}">
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="fecha_historico_comp" HeaderText="Periodo Comparación">
                                    <ItemStyle HorizontalAlign="center" />
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
