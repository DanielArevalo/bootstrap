<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Nuevo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<%@ Register Src="../../../General/Controles/ctlBusquedaRapida.ascx" TagName="BusquedaRapida" TagPrefix="uc1" %>
<%@ Register Src="../../../General/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc1" %>
<%@ Register Src="../../../General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Src="../../../General/Controles/ctlseleccionmultipledropdown.ascx" TagName="dropdownmultiple" TagPrefix="ucDrop" %>
<%@ Register Src="../../../General/Controles/ctlColorPicker.ascx" TagName="ctlColorPicker" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:Label ID="lablerror" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="lablerror0" runat="server" Visible="false"></asp:Label>
    <table style="width: 100%">
        <tr>
            <td  style="text-align: left">
                Fecha de Corte<br />
                <asp:DropDownList ID="ddlFechaCorte" runat="server" Height="24px" 
                    Width="165px" AutoPostBack="True" 
                    onselectedindexchanged="ddlFechaCorte_SelectedIndexChanged">                           
                </asp:DropDownList>
                <br />
            </td>
            <td style="text-align: left">
                Linea de Crédito:<br />
                <ucdrop:dropdownmultiple id="ddlLinea" runat="server" height="24px" width="200px">
                </ucdrop:dropdownmultiple>
            </td>
            <td style="text-align: left;">      
                <asp:CheckBox ID="ch3D" runat="server" oncheckedchanged="ch3D_CheckedChanged" 
                    Text="Mostrar en 3D" AutoPostBack="True" />                   
            </td>
            <td style="text-align: left;">
                Color de Fondo<br />
                <uc1:ctlColorPicker ID="txtColorFondo" runat="server" />
            </td>
            <td>
                <asp:Button ID="btnInforme" runat="server" CssClass="btn8" OnClick="btnInforme_Click"
                OnClientClick="btnInforme_Click" Text="Generar Reporte" />
            </td>
        </tr>
        <tr>
            <td  style="text-align: left">
                                    Tipo de Gráfica</td>
            <td  style="text-align: left">
                                    <asp:DropDownList ID="ddlTipoGrafica1" runat="server" Height="24px" Width="150px"
                                        OnSelectedIndexChanged="ddlTipoGrafica1_SelectedIndexChanged" AutoPostBack="True">
                                        <asp:ListItem Value="1" Selected="True">Pie</asp:ListItem>
                                        <asp:ListItem Value="2">Barras</asp:ListItem>
                                        <asp:ListItem Value="3">Lineas</asp:ListItem>
                                        <asp:ListItem Value="4">Area</asp:ListItem>
                                    </asp:DropDownList>
            </td>
            <td style="text-align: left" colspan="2">
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    Tipo de Gráfica</td>
            <td style="text-align: left;">
                                    <asp:DropDownList ID="ddlTipoGrafica2" runat="server" Height="24px" Width="150px"
                                        OnSelectedIndexChanged="ddlTipoGrafica1_SelectedIndexChanged" AutoPostBack="True">
                                        <asp:ListItem Value="1" Selected="True">Pie</asp:ListItem>
                                        <asp:ListItem Value="2">Barras</asp:ListItem>
                                        <asp:ListItem Value="3">Lineas</asp:ListItem>
                                        <asp:ListItem Value="4">Area</asp:ListItem>
                                    </asp:DropDownList>
            </td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
    <hr style="width:100%"/>
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
                            <asp:Series Name="Series1" Color="White" ChartType="Pie" 
                                Font="Microsoft Sans Serif, 7pt, style=Bold" IsValueShownAsLabel="True" 
                                MarkerSize="7" MarkerBorderColor="#CC0099" MarkerBorderWidth="1" 
                                MarkerColor="#CC0099" MarkerStyle="Circle" LabelForeColor="White">
                            </asp:Series>
                        </Series>
                        <Legends>
                            <asp:Legend Alignment="Center" Docking="Bottom" IsTextAutoFit="False" Name="Default"
                                LegendStyle="Table" Font="Trebuchet MS, 8pt, style=Bold" />
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
                            <asp:Series Name="Series1" Color="DarkMagenta" ChartType="Pie" 
                                Font="Microsoft Sans Serif, 7pt, style=Bold" IsValueShownAsLabel="True" 
                                MarkerSize="7" MarkerBorderColor="#CC0099" MarkerBorderWidth="1" 
                                MarkerColor="#CC0099" MarkerStyle="Circle" LabelForeColor="White">
                            </asp:Series>
                        </Series>
                        <Legends>
                            <asp:Legend Alignment="Center" Docking="Bottom" IsTextAutoFit="False" Name="Default"
                                LegendStyle="Table" Font="Trebuchet MS, 8pt, style=Bold" />
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
                            <asp:BoundField DataField="segmento" HeaderText="Segmento">
                                <ItemStyle width="85px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="numero" HeaderText="Cantidad">
                                <ItemStyle width="85px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="saldo" DataFormatString="{0:C}" HeaderText="Monto">
                                <ItemStyle width="85px" />
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
                    <asp:GridView ID="gvCantidad" runat="server" AutoGenerateColumns="False" GridLines="Both" HeaderStyle-CssClass="gridHeader" Height="122px" PagerStyle-CssClass="gridPager" RowStyle-CssClass="gridItem" ShowFooter="False" style="font-size:xx-small" Width="510px">
                        <Columns>
                            <asp:BoundField DataField="fecha_historico" HeaderText="Fecha">
                                <ItemStyle width="85px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="segmento" HeaderText="Segmento">
                                <ItemStyle width="85px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="cod_categoria" HeaderText="Categoria">
                                <ItemStyle width="85px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="numero" HeaderText="Cantidad">
                                <ItemStyle width="85px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="saldo" DataFormatString="{0:C}" HeaderText="Monto">
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
