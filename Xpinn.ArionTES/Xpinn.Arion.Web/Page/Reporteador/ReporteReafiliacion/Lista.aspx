<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Lista.aspx.cs" Inherits="Lista" %>



<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Src="~/General/Controles/ctlseleccionmultipledropdown.ascx" TagName="dropdownmultiple" TagPrefix="ucDrop" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table style="width: 100%">
        <tr>
            <td style="text-align: left; font-size: small;" colspan="3">
                <strong>Criterios de Generación:</strong></td>
            <td style="text-align: center; width: 467px;">&nbsp;</td>
            <td class="logo" style="width: 194px">&nbsp;</td>
        </tr>
        <tr>
            <td style="text-align: left">
                <asp:Label ID="lblFechaInicial" runat="server" Text="Fecha Inicial:"></asp:Label><br />
                <ucFecha:fecha ID="ucFechaInicial" runat="server" style="text-align: center" />
            </td>
            <td style="text-align: left">
                <asp:Label ID="lblFechaFinal" runat="server" Text="Fecha Final:"></asp:Label><br />
                <ucFecha:fecha ID="ucFechaFinal" runat="server" style="text-align: center" />
            </td>

<%--            <td style="text-align: left">Oficina:
                <ucDrop:dropdownmultiple ID="ddloficina" runat="server" Height="24px" Width="200px"></ucDrop:dropdownmultiple>
            </td>--%>
            <%--<td style="text-align: left">Categoria:<br />
                <ucDrop:dropdownmultiple ID="ddlCategoria" runat="server" Height="24px" Width="165px"></ucDrop:dropdownmultiple>
            </td>
            <td style="text-align: left">Linea de Crédito:<br />
                <ucDrop:dropdownmultiple ID="ddlLinea" runat="server" Height="24px" Width="200px"></ucDrop:dropdownmultiple>
            </td>--%>
           
        </tr>
    </table>


    <asp:Panel ID="Principal" runat="server">
        <asp:Panel ID="Listado" runat="server">
            <table style="width: 100%;">
                <tr>
                    <td>
                        <asp:GridView ID="gvLista" runat="server" AllowPaging="True" 
                            AutoGenerateColumns="False" BackColor="White" BorderColor="#DEDFDE" 
                            BorderStyle="None" BorderWidth="1px" CellPadding="4"  
                            ForeColor="Black" GridLines="Vertical" PageSize="20" Width="100%"
                            OnSelectedIndexChanged="gvLista_SelectedIndexChanged" 
                            onrowediting="gvLista_RowEditing" 
                            onpageindexchanging="gvLista_PageIndexChanging" Font-Size="X-Small">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                            
                                <asp:BoundField DataField="identificacion" HeaderText="Identificación" />
                                <asp:BoundField DataField="nombre" HeaderText="Apellidos y Nombres" />
                                <asp:BoundField DataField="nombre_empresa_pagaduria" HeaderText="Pagaduría" />
                                <asp:BoundField DataField="fecha_apertura" DataFormatString="{0:d}" HeaderText="Fecha Afiliación"/>
                                <asp:BoundField DataField="fecha_retiro" DataFormatString="{0:d}" HeaderText="Fecha Retiro"/> 
                                <asp:BoundField DataField="fecha_ultima_afiliacion" DataFormatString="{0:d}" HeaderText="Fecha Ult. Reafiliación"/>
                                <asp:BoundField DataField="Num_Reafiliaciones" DataFormatString="{0:N0}" HeaderText="N° Reafiliaciones"> <ItemStyle HorizontalAlign="Right" /></asp:BoundField> 
                                   
                            </Columns>
                            <FooterStyle BackColor="#CCCC99" />
                            <HeaderStyle CssClass="gridHeader" />
                            <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                            <RowStyle CssClass="gridItem" />
                            <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                            <SortedAscendingCellStyle BackColor="#FBFBF2" />
                            <SortedAscendingHeaderStyle BackColor="#848384" />
                            <SortedDescendingCellStyle BackColor="#EAEAD3" />
                            <SortedDescendingHeaderStyle BackColor="#575357" />
                        </asp:GridView>
                        <asp:Label ID="lblTotalRegs" runat="server" Visible="False"/>
                        <asp:Label ID="lblInfo" runat="server" Text="Su consulta no obtuvo ningun resultado." Visible="False"/>                    
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </asp:Panel>



</asp:Content>
