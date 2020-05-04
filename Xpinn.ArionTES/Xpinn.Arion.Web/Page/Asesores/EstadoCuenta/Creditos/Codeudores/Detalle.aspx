<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Detalle.aspx.cs" Inherits="EstadoCuentaCodeudoresDetalle" %>

<%@ Register Src="~/General/Controles/imprimir.ascx" TagName="imprimir" TagPrefix="ucImprimir" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <table style="width: 100%">
        <tr>
            <td colspan="4" style="text-align: center">
                &nbsp;<ucImprimir:imprimir ID="ucImprimir" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                No Crédito<br />
                <asp:TextBox ID="txtNoCredito" runat="server" CssClass="textbox" Enabled="false"></asp:TextBox>
            </td>
            <td>
                Estado Crédito<br />
                <asp:TextBox ID="txtEstaCredito" runat="server" CssClass="textbox" Enabled="false"></asp:TextBox>
            </td>            
            <td>
                Nombre Línea<br />
                <asp:TextBox ID="txtNombLinea" runat="server" CssClass="textbox" Enabled="false"></asp:TextBox>
            </td>
            <td>
                Nombres<br />
                <asp:TextBox ID="txtNombres" runat="server" CssClass="textbox" Enabled="false"></asp:TextBox>
            </td>
        </tr>
        <tr>            
            <td>
                Monto<br />
                <asp:TextBox ID="txtMonto" runat="server" CssClass="textbox" Enabled="false"></asp:TextBox>
            </td>
            <td>
                Saldo<br />
                <asp:TextBox ID="txtSaldo" runat="server" CssClass="textbox" Enabled="false"></asp:TextBox>
            </td>
            <td>
                Couta<br />
                <asp:TextBox ID="txtCouta" runat="server" CssClass="textbox" Enabled="false"></asp:TextBox>
            </td> 
            <td>
                Próx. Pago<br />
                <asp:TextBox ID="txtProPago" runat="server" CssClass="textbox" Enabled="false"></asp:TextBox>
            </td>
        </tr>
    </table>

    <table width="100%">
        <tr>
            <td colspan="2">
                <asp:GridView ID="gvCodeudores" AllowPaging="true" runat="server" Width="100%" PageSize="20"
                    GridLines="Horizontal" ShowHeaderWhenEmpty="true" AutoGenerateColumns="false"
                    OnPageIndexChanging="gvCodeudores_PageIndexChanging">
                    <Columns>
                        <asp:BoundField DataField="NumeroDocumento" HeaderText="NumeroDocumento" />
                        <asp:BoundField DataField="Nombres" HeaderText="Nombres" />
                        <asp:BoundField DataField="Apellidos" HeaderText="Apellidos" />
                        <asp:BoundField DataField="Direccion" HeaderText="Dirección" />
                        <asp:BoundField DataField="Telefono" HeaderText="Teléfono" />
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