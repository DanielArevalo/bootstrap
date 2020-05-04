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
                <asp:DropDownList ID="ddlFechaCorte" runat="server" Height="24px" 
                    Width="165px" AutoPostBack="True" 
                    onselectedindexchanged="ddlFechaCorte_SelectedIndexChanged">                           
                </asp:DropDownList>
                <br />
            </td>
            <td  style="text-align: left">
                &nbsp;</td>
            <td style="text-align: left; width: 200px;">      
                Descripción<asp:DropDownList ID="ddlDescripcion" runat="server" AutoPostBack="True" CssClass="textbox"
                                Width="182px">
                    <asp:ListItem Value="1">FONDO LIQUIDEZ</asp:ListItem>
                    <asp:ListItem Value="2">DISPONIBLE</asp:ListItem>
                            </asp:DropDownList>
            </td>
            <td style="text-align: left; width: 200px;">      
                <asp:TextBox ID="txttotal" Visible="false" runat="server"></asp:TextBox>
            </td>
            <td style="text-align: left">
                <asp:Button ID="btnInforme" runat="server" CssClass="btn8" OnClick="btnInforme_Click"
                    OnClientClick="btnInforme_Click" Text="Generar Reporte" />                        
            </td>
        </tr>
    </table>
    <asp:Panel ID="PanelComprobanteImpr" runat="server">
        <table>
            <tr>
                <td colspan="2">
                    <asp:GridView ID="gvDatos" runat="server" AutoGenerateColumns="False" AutoSizeColumnsMode="false" GridLines="Horizontal"  
                        PageSize="6"  OnRowDataBound="gvDatos_RowDataBound" ShowFooter="True" Width="120%" HorizontalAlign="Center">
                        <Columns>
                            <asp:BoundField DataField="descripcion" HeaderText="Tipo Deposito">
                                   <ItemStyle Width="74%" HorizontalAlign="left"/> </asp:BoundField>
                             <asp:BoundField DataField="cod_cuenta" HeaderText="Cuenta">                          
                               <ItemStyle Width="20%" HorizontalAlign="left" /> </asp:BoundField>
                            <asp:BoundField DataField="total" HeaderText="Saldo" DataFormatString="{0:n0}">
                               <ItemStyle Width="20%" HorizontalAlign="Right" /> </asp:BoundField>
                        </Columns>
                        <HeaderStyle CssClass="gridHeader" />
                        <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                        <RowStyle CssClass="gridItem" />
                    </asp:GridView>
                    <br />
                    <asp:GridView ID="gvFondo" runat="server" AutoGenerateColumns="False"  AutoSizeColumnsMode="false" GridLines="Horizontal" PageSize="1"
                         ShowFooter="True"  OnRowDataBound="gvFondo_RowDataBound" Width="120%" ShowHeader="False" HorizontalAlign="Center">
                        <Columns>
                            <asp:BoundField DataField="descripcion" HeaderText="Tipo Deposito">
                                   <ItemStyle Width="100%" HorizontalAlign="left" /> </asp:BoundField>
                             <asp:BoundField DataField="cod_cuenta" HeaderText="Cuenta">                          
                               <ItemStyle Width="40%" HorizontalAlign="left" /> </asp:BoundField>
                            <asp:BoundField DataField="total" HeaderText="Saldo" DataFormatString="{0:n0}">
                               <ItemStyle Width="40%" HorizontalAlign="Right"  /> </asp:BoundField>
                        </Columns>
                        <HeaderStyle CssClass="gridHeader" />
                        <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                        <RowStyle CssClass="gridItem" />
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td style="width: 385px">
                    <asp:Button ID="btnExportar" runat="server" CssClass="btn8" OnClick="btnExportar_Click"
                        OnClientClick="btnExportar_Click" Text="Exportar Gráfica" Visible="False" /><br />
                </td>
                <td>
                    &nbsp;</td>
            </tr>                    
        </table>   
    </asp:Panel>
</asp:Content>
