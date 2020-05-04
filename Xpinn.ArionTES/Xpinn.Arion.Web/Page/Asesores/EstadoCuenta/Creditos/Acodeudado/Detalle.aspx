<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Detalle.aspx.cs" Inherits="EstadoCuentaAcodeudadoDetalle" %>

<%@ Register Src="~/General/Controles/imprimir.ascx" TagName="imprimir" TagPrefix="ucImprimir" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <table width="100%">
        <tr>
            <td style="text-align: center">
                &nbsp;<ucImprimir:imprimir ID="ucImprimir" runat="server" />
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:GridView ID="gvAcodeudados" AllowPaging="True" runat="server" Width="100%" PageSize="20"
                    GridLines="Horizontal" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" RowStyle-Font-Size="X-Small"
                    OnPageIndexChanging="gvAcodeudados_PageIndexChanging">
                    <Columns>
                        <asp:BoundField DataField="Estado" HeaderText="Estado" ><ItemStyle HorizontalAlign="Left" /></asp:BoundField>
                        <asp:BoundField DataField="NumRadicacion" HeaderText="Número Crédito" ><ItemStyle HorizontalAlign="center" /></asp:BoundField>
                        <asp:BoundField DataField="Linea" HeaderText="Línea" ><ItemStyle HorizontalAlign="Left" /></asp:BoundField>
                        <asp:BoundField DataField="Nombres" HeaderText="Nombre Deudor" ><ItemStyle HorizontalAlign="Left" /></asp:BoundField>
                        <asp:BoundField DataField="Monto" HeaderText="Monto" DataFormatString="{0:c}" ><ItemStyle HorizontalAlign="Right" /></asp:BoundField>
                        <asp:BoundField DataField="Saldo" HeaderText="Saldo" DataFormatString="{0:c}" ><ItemStyle HorizontalAlign="Right" /></asp:BoundField>
                        <asp:BoundField DataField="Cuota" HeaderText="Cuota" DataFormatString="{0:c}" ><ItemStyle HorizontalAlign="Right" /></asp:BoundField>
                        <asp:BoundField DataField="FechaProxPago" HeaderText="Fecha Próx Pago" ><ItemStyle HorizontalAlign="Center" /></asp:BoundField>
                    </Columns>
                    <HeaderStyle CssClass="gridHeader" />
                    <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                    <RowStyle CssClass="gridItem" />
                </asp:GridView>
                <asp:Label ID="lblTotalReg" runat="server" Visible="False" />
                <asp:Label ID="lblInfo" runat="server" Text="Su consulta no obtuvo ningún resultado."
                    Visible="False" />
            </td>
        </tr>
    </table>
</asp:Content>