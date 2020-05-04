<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Detalle.aspx.cs" Inherits="EstadoCuentaGarantiaDetalle" %>

<%@ Register Src="~/General/Controles/imprimir.ascx" TagName="imprimir" TagPrefix="ucImprimir" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <table style="width: 100%">
        <tr>
            <td colspan="3" style="text-align: center">
                &nbsp;<ucImprimir:imprimir ID="ucImprimir" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                Tipo Identificación<br />
                <asp:TextBox ID="txtTipoDoc" runat="server" CssClass="textbox" Enabled="false" 
                    Width="151px"></asp:TextBox>
            </td>
            <td>
                Número Identificación<br />
                <asp:TextBox ID="txtNumeIdentificacion" runat="server" CssClass="textbox" 
                    Enabled="false" Width="151px"></asp:TextBox>
            </td>
            <td>
                Nombre<br />
                <asp:TextBox ID="txtNombres" runat="server" CssClass="textbox" Enabled="false" 
                    Width="265px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                No Crédito<br />
                <asp:TextBox ID="txtNoCredito" runat="server" CssClass="textbox" 
                    Enabled="false" Width="151px"></asp:TextBox>
            </td>
            <td>
                Estado Crédito<br />
                <asp:TextBox ID="txtEstaCredito" runat="server" CssClass="textbox" 
                    Enabled="false" Width="151px"></asp:TextBox>
            </td>
            <td>
                Nombre Línea<br />
                <asp:TextBox ID="txtNombLinea" runat="server" CssClass="textbox" 
                    Enabled="false" Width="265px"></asp:TextBox>
            </td>
        </tr>
        </table>
    <table width="100%">
        <tr>
            <td colspan="2">
                <asp:GridView ID="gvGtias" AllowPaging="true" runat="server" Width="100%" PageSize="20"
                    GridLines="Horizontal" ShowHeaderWhenEmpty="true" AutoGenerateColumns="false" 
                    OnPageIndexChanging="gvGtias_PageIndexChanging" >
                    <Columns>
                        <asp:BoundField DataField="NoGtia" HeaderText="No Garantía" />
                        <asp:BoundField DataField="Tipo" HeaderText="Tipo" />
                        <asp:BoundField DataField="FechaGarantia" HeaderText="Fecha Constitución" />
                        <asp:BoundField DataField="Descripcion" HeaderText="Descripción" />
                        <asp:BoundField DataField="EstadoGtia" HeaderText="Estado Garantía" />
                    </Columns>
                    <HeaderStyle CssClass="gridHeader" />
                    <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                    <RowStyle CssClass="gridItem" />
                </asp:GridView>
                <asp:Label ID="lblTotalReg" runat="server" Visible="False" />
                <asp:Label ID="lblInfo" runat="server" Text="Su consulta no obtuvo ningún resultado." Visible="False" />
            </td>
        </tr>
    </table>
</asp:Content>