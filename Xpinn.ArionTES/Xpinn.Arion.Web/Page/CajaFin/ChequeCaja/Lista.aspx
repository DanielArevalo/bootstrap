<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
<br/><br/>
<div id="gvDiv"> 
<table cellpadding="0" cellspacing="0" style="width: 100%">
        <tr>
            <td >
                <hr  width="100%" noshade >
            </td>
        </tr>
         <tr>
            <td> Fecha 
                <asp:TextBox ID="txtFecha" enabled="false" CssClass="textbox" runat="server"></asp:TextBox>
            </td>
        </tr>
         <tr>
            <td> Cajero 
            <asp:TextBox ID="txtCajero" enabled="false" CssClass="textbox" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr><td></td></tr>
        <tr>
            <td>
                <div id="DivButtons">
                    <asp:Button ID="btnVerCheques" runat="server" Text="Ver Cheques" 
                    onclick="btnVerCheques_Click" />&#160;
                     <asp:Button ID="btnImprimir" runat="server" Text="Imprimir" />&#160;
                    <asp:Button ID="btnExportarExcel" runat="server" Text="Exportar" 
                        onclick="btnExportarExcel_Click" />
                </div>
             </td>

        </tr>
        <tr>
            <td  style="text-align: center; color: #FFFFFF; background-color: #0066FF">
                Cheques Pendientes
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="gvChequePendiente" runat="server" Width="100%" 
                    AutoGenerateColumns="False" AllowPaging="False" PageSize="20" BackColor="White" 
                    BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
                    ForeColor="Black" GridLines="Vertical">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                       <asp:BoundField DataField="cod_movimiento" HeaderText="Código Operación" />
                       <asp:BoundField DataField="num_documento" HeaderText="Número Cheque" />
                       <asp:BoundField DataField="nom_banco" HeaderText="Banco" />
                       <asp:BoundField DataField="titular" HeaderText="Títular" />
                       <asp:BoundField DataField="valor" HeaderText="Valor" DataFormatString="{0:N0}">
                         <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
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
        <tr>
            <td  style="text-align: center; color: #FFFFFF; background-color: #0066FF">        
                Cheques Asignados 
                </td>
            </tr>
        <tr>
            <td>
                  <asp:GridView ID="gvChequeAsignado" runat="server" Width="100%" 
                    AutoGenerateColumns="False" AllowPaging="False" PageSize="20" BackColor="White" 
                    BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
                    ForeColor="Black" GridLines="Vertical">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                       <asp:BoundField DataField="cod_movimiento" HeaderText="Código Giro" />
                       <asp:BoundField DataField="num_documento" HeaderText="Número Cheque" />
                       <asp:BoundField DataField="nom_banco" HeaderText="Banco" />
                       <asp:BoundField DataField="titular" HeaderText="Títular" />
                         <asp:BoundField DataField="valor" HeaderText="Valor" DataFormatString="{0:N0}">
                         <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                       <asp:BoundField DataField="estado" HeaderText="Estado" />
                       <asp:BoundField DataField="fec_ope" DataFormatString="{0:d}" HeaderText="Fecha Asignación" /> 
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

