<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ctlCierreHistoricoError.ascx.cs" Inherits="ctlCierreHistoricoError" %>

<br/><asp:Label ID="lblTitulo" runat="server" Text="Errores en el cierre historico" Visible="false"/>
<table>
    <tr>
        <td>
            <asp:GridView ID="gvLista" runat="server" Width="100%" AutoGenerateColumns="False"
                AllowPaging="true"
                Style="font-size: small" DataKeyNames="codproducto" PagerSettings-Visible="true"
                OnPageIndexChanging="gvLista_PageIndexChanging">
                <Columns>
                    <asp:BoundField DataField="codproducto" HeaderText="Número Producto">
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="saldo" HeaderText="Saldo">
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="descripcionerror" HeaderText="Descripción">
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
