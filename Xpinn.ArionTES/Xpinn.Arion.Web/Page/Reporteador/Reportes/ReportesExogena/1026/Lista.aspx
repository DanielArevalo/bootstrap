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
            </td>
            <td style="text-align: left">Categoria:<br />
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
                            OnRowEditing="gvLista_RowEditing"
                            OnPageIndexChanging="gvLista_PageIndexChanging" Font-Size="X-Small">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:BoundField DataField="cod_clasifica" HeaderText="Código" />
                                <asp:BoundField DataField="tipo_identificacion" HeaderText="Tipo de documento" />
                                <asp:BoundField DataField="identificacion" HeaderText="Número identificación deudor" />
                                <asp:BoundField DataField="cod_motivo" HeaderText="DV" />
                                <asp:BoundField DataField="primer_apellido" HeaderText="Primer apellido deudor" />
                                <asp:BoundField DataField="segundo_apellido" HeaderText="Segundo apellido deudor" />
                                <asp:BoundField DataField="primer_nombre" HeaderText="Primer nombre deudor" />
                                <asp:BoundField DataField="segundo_nombre" HeaderText="Otros nombres deudor" />                                
                                <asp:BoundField DataField="empresa" HeaderText="Razón Social deudor" />
                                <asp:BoundField DataField="direccion" HeaderText="Direccion" />
                                <asp:BoundField DataField="cod_Departamento" HeaderText="Código dpto" />
                                <asp:BoundField DataField="Cod_MunoCiu" HeaderText="Código mcp" />                               
                                <asp:BoundField DataField="saldo" DataFormatString="{0:N0}" HeaderText="Valor préstamo otorgado al 31-12"/>
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
                        <asp:Label ID="lblTotalRegs" runat="server" Visible="False" />
                        <asp:Label ID="lblInfo" runat="server" Text="Su consulta no obtuvo ningun resultado." Visible="False" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </asp:Panel>



</asp:Content>
