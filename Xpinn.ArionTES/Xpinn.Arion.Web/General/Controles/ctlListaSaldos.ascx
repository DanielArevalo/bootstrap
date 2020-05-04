<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ctlListaSaldos.ascx.cs" Inherits="General_Controles_ctlListaSaldos" %>

<br/><b><asp:Label ID="lblTitulo" runat="server" Text="Lista Saldos en Devoluciones y CDAT" Visible="false"/></b>
<table>
    <tr>
        <td>
            <asp:GridView ID="gvLista" runat="server" Width="100%" AutoGenerateColumns="False"
                AllowPaging="true"
                Style="font-size: small" DataKeyNames="numero" PagerSettings-Visible="true"
                OnPageIndexChanging="gvLista_PageIndexChanging">
                <Columns>
                    <asp:BoundField DataField="numero" HeaderText="Número Producto">
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="valor_en" HeaderText="Saldo">
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="tipo" HeaderText="Descripción">
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                </Columns>
                <HeaderStyle CssClass="gridHeader" />
                <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                <RowStyle CssClass="gridItem" />
            </asp:GridView>
        </td>
    </tr>
    <tr>
        <td style="text-align: center">
            <br />
            <asp:HiddenField ID="hdf" runat="server" />
            <asp:Label ID="Count" runat="server"></asp:Label></td>
    </tr>
</table>
