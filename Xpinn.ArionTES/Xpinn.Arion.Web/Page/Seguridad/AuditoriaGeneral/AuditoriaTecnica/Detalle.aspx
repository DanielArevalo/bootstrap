<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Detalle.aspx.cs" Inherits="Detalle" Title=".: Xpinn - Auditoria :." %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">

    <table border="0" cellpadding="5" cellspacing="0" width="100%">
        <tr>
            <td class="tdI">Codigo Auditoria&nbsp;<br />
                <asp:TextBox ID="txtCodigoAuditoria" runat="server" CssClass="textbox" Enabled="false" />
            </td>
            <td class="tdD">Fecha Ejecucion&nbsp;<br />
                <asp:TextBox ID="txtFecha" runat="server" CssClass="textbox" Enabled="false" />
            </td>
        </tr>
        <tr>
            <td class="tdI">Codigo Usuario&nbsp;<br />
                <asp:TextBox ID="txtCodigoUsuario" runat="server" CssClass="textbox" Enabled="false" />
            </td>
            <td class="tdI">Usuario&nbsp;<br />
                <asp:TextBox ID="txtUsuario" runat="server" CssClass="textbox" Enabled="false" Width="90%" />
            </td>
        </tr>
        <tr>
            <td class="tdI">Codigo Opcion<br />
                <asp:TextBox ID="txtCodigoOpcion" runat="server" CssClass="textbox" Enabled="false" />
            </td>
            <td class="tdD">Opcion<br />
                <asp:TextBox ID="txtOpcion" runat="server" CssClass="textbox" Enabled="false" Width="90%" />
            </td>
        </tr>
        <tr>
            <td class="tdI">Exitoso<br />
                <asp:TextBox ID="txtExitoso" runat="server" CssClass="textbox" Enabled="false" />
            </td>
            <td class="tdD">Nombre SP<br />
                <asp:TextBox ID="txtNombreSp" runat="server" CssClass="textbox" Enabled="false" Width="90%" />
            </td>
        </tr>
        <tr>
            <td class="tdI" colspan="2">Mensaje Error<br />
                <asp:TextBox runat="server" ID="txtMensajeError" TextMode="MultiLine" Width="80%" Height="100px" />
            </td>
        </tr>
        <tr>
            <td class="tdI" colspan="2">Informacion Enviada<br />
                <asp:GridView runat="server" ID="gvParametros" Width="80%" HorizontalAlign="Center" AutoGenerateColumns="false"
                    HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager" RowStyle-CssClass="gridItem">
                    <Columns>
                        <asp:BoundField DataField="NombreParametro" HeaderText="Nombre Parametro" />
                        <asp:BoundField DataField="ValorParametro" HeaderText="Valor Parametro" />
                    </Columns>
                </asp:GridView>
            </td>
    </table>

</asp:Content>
