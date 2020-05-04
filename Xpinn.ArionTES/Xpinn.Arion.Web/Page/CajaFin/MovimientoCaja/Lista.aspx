<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<br/><br/>
<div id="gvDiv">
 <table cellpadding="0" cellspacing="0" style="width: 100%">
        <tr>
            <td >
                <hr  width="100%"></hr>
            </td>
        </tr>
         <tr>
            <td>Fecha
                <asp:TextBox ID="txtFecha" enabled="false" CssClass="textbox" runat="server" Width="70"></asp:TextBox>
            </td>
        </tr>
         <tr>
            <td> Oficina
                <asp:TextBox ID="txtOficina" enabled="false" CssClass="textbox" runat="server" Width="260px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>Caja
                <asp:TextBox ID="txtCaja" enabled="false" CssClass="textbox" runat="server" Width="260px"></asp:TextBox>
            </td>
        </tr>
          <tr>
            <td>Cajero
                <asp:TextBox ID="txtCajero" enabled="false" CssClass="textbox" runat="server" Width="260px"></asp:TextBox>
            </td>
        </tr>
        <tr><td></td></tr>
        <tr><td>
            <div id="DivButtons">
                <asp:Button ID="btnImprimir" runat="server" Text="Imprimir" />&#160;
                <asp:Button ID="btnExportarExcel" runat="server" Text="Exportar" 
                    onclick="btnExportarExcel_Click" />
            </div>
            </td></tr>
        <tr>
            <td>
                <asp:GridView ID="gvMovimiento" runat="server" Width="100%" 
                    AutoGenerateColumns="False" AllowPaging="False" PageSize="20" BackColor="White" 
                    BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
                    ForeColor="Black" GridLines="Vertical" 
                    onpageindexchanging="gvMovimiento_PageIndexChanging" 
                    style="font-size: x-small">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:BoundField DataField="fecha_movimiento" DataFormatString="{0:d}" HeaderText="Fecha" />
                        <asp:BoundField DataField="cod_ope" HeaderText="Cod Operación" />
                        <asp:BoundField DataField="nom_tipo_producto" HeaderText="Tipo Producto" />
                        <asp:BoundField DataField="tipo_ope" HeaderText="Cod Tipo Oper" />
                        <asp:BoundField DataField="nom_tipo_ope" HeaderText="Tipo Oper" />
                        <asp:BoundField DataField="tipo_movimiento" HeaderText="Tipo Movimiento" />
                        <asp:BoundField DataField="iden_cliente" HeaderText="Identificación" />
                        <asp:BoundField DataField="nom_cliente" HeaderText="Nombre Cliente" />
                        <asp:BoundField DataField="num_producto" HeaderText="Producto" />
                        <asp:BoundField DataField="valor_pago" HeaderText="Valor" DataFormatString="{0:N0}">
                          <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="nom_cajero" HeaderText="Cajero" />
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
                
            </td>
        </tr>

    </table>
  </div>
</asp:Content>

