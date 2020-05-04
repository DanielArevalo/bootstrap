<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"CodeFile="Lista.aspx.cs" Inherits="Nuevo" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<%@ Register assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI.DataVisualization.Charting" tagprefix="asp" %>
<%@ Register src="../../../General/Controles/fecha.ascx" tagname="fecha" tagprefix="ucFecha" %>

<%@ Register src="../../../General/Controles/ctlColorPicker.ascx" tagname="ctlColorPicker" tagprefix="uc1" %>

<%@ Register src="../../../General/Controles/ctlseleccionmultipledropdown.ascx" tagname="dropdownmultiple" tagprefix="ucDrop" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table style="width: 94%">
        <tr>
            <td style="text-align: left; font-size: small;" colspan="4">
                <strong>Criterios de Generación:</strong></td>
            <td style="text-align: center; width: 497px;">                            
                &nbsp;</td>
            <td style="text-align: center; width: 497px;">                            
                &nbsp;</td>
            <td class="logo" style="width: 194px">
                &nbsp;</td>
        </tr>
        <tr>
            <td  style="text-align: left; width: 164px;">
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
            <td style="text-align: left; width: 228px;">      
                Linea Aporte<asp:DropDownList ID="ddllinea" AutoPostBack="true" runat="server" Style="margin-bottom: 0px" Width="150px"
                                                        CssClass="textbox" OnSelectedIndexChanged="ddllinea_SelectedIndexChanged">                                                      
                                                    </asp:DropDownList>
            </td>
            <td style="text-align: left; width: 244px;">      
                Color de Fondo<br />
                <uc1:ctlColorPicker ID="txtColorFondo" runat="server" />
            </td>
            <td style="text-align: left">
                <asp:CheckBox ID="ch3D" runat="server" oncheckedchanged="ch3D_CheckedChanged" 
                    Text="Mostrar en 3D" AutoPostBack="True" />                   
            </td>
            <td style="text-align: left">
                <asp:Button ID="btnInforme" runat="server" CssClass="btn8" OnClick="btnInforme_Click"
                    OnClientClick="btnInforme_Click" Text="Generar Reporte" />                        
            </td>
        </tr>
        <tr>
            <td  style="text-align: left; width: 164px;">
                                    Tipo de Gráfica</td>
            <td  style="text-align: left">
                                    <asp:DropDownList ID="ddlTipoGrafica1" runat="server" Height="24px" Width="150px"
                                        OnSelectedIndexChanged="ddlTipoGrafica1_SelectedIndexChanged" AutoPostBack="True">
                                        <asp:ListItem Value="1">Barras</asp:ListItem>
                                        <asp:ListItem Value="2">Pie</asp:ListItem>
                                        <asp:ListItem Value="3" Selected="True">Lineas</asp:ListItem>
                                        <asp:ListItem Value="4">Area</asp:ListItem>
                                    </asp:DropDownList>
            </td>
            <td style="text-align: left; width: 228px;">      
                &nbsp;</td>
            <td style="text-align: left; width: 244px;">      
                                    Tipo de Gráfica</td>
            <td style="text-align: left">
                                    <asp:DropDownList ID="ddlTipoGrafica2" runat="server" Height="24px" Width="150px"
                                        OnSelectedIndexChanged="ddlTipoGrafica1_SelectedIndexChanged" AutoPostBack="True">
                                        <asp:ListItem Value="1">Barras</asp:ListItem>
                                        <asp:ListItem Value="2">Pie</asp:ListItem>
                                        <asp:ListItem Value="3" Selected="True">Lineas</asp:ListItem>
                                        <asp:ListItem Value="4">Area</asp:ListItem>
                                    </asp:DropDownList>
            </td>
            <td style="text-align: left">
                                    &nbsp;</td>
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
                            <asp:Series Name="Series1" Color="#8bad5b" ChartType="Line" 
                                Font="Microsoft Sans Serif, 6.75pt, style=Bold" IsValueShownAsLabel="True" MarkerSize="7" MarkerBorderColor="#CC0099" MarkerBorderWidth="1" 
                                MarkerColor="#CC0099" MarkerStyle="Circle">
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
                            <asp:Series Name="Series1" Color="#8bad5b" ChartType="Line" 
                                Font="Microsoft Sans Serif, 6.75pt, style=Bold" IsValueShownAsLabel="True" MarkerSize="7" MarkerBorderColor="204, 0, 153" MarkerBorderWidth="1" 
                                MarkerColor="204, 0, 153" MarkerStyle="Circle">
                            </asp:Series>
                        </Series>
                        <Legends>
                            <asp:Legend Alignment="Center" Docking="Bottom" IsTextAutoFit="False" Name="Default"
                                LegendStyle="Row" Font="Trebuchet MS, 8pt, style=Bold" />
                        </Legends>
						<chartareas>
							<asp:ChartArea Name="ChartArea2" BorderColor="64, 64, 64, 64" BackSecondaryColor="Transparent" BackColor="White" ShadowColor="Transparent" BackGradientStyle="TopBottom" BorderDashStyle="DashDot">
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
                        OnClientClick="btnExportar_Click" Text="Exportar Gráfica" />
                                   
                    <asp:GridView ID="gvDatos" runat="server" AutoGenerateColumns="False" GridLines="Both" HeaderStyle-CssClass="gridHeader" Height="122px" PagerStyle-CssClass="gridPager" RowStyle-CssClass="gridItem" ShowFooter="False" style="font-size:xx-small" Width="500px">
                        <Columns>
                            <asp:BoundField DataField="fecha_historico" HeaderText="Fecha">
                            <ItemStyle width="85px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="total" DataFormatString="{0:C}" HeaderText="Total Aportes">
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
                    <br />
                    <asp:GridView ID="gvCantidad" runat="server" AutoGenerateColumns="False" GridLines="Both" HeaderStyle-CssClass="gridHeader" Height="122px" PagerStyle-CssClass="gridPager" RowStyle-CssClass="gridItem" ShowFooter="False" style="font-size:xx-small" Width="432px">
                        <Columns>
                            <asp:BoundField DataField="fecha_historico" HeaderText="Fecha">
                            <ItemStyle width="85px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="numero" DataFormatString="{0:N}" HeaderText="Cantidad Aportes">
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
