<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/General/Master/site.master" CodeFile="Lista.aspx.cs" Inherits="Lista" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
<table cellpadding="0" cellspacing="0" style="width: 100%">
        <tr>
            <td >
                <hr  width="100%" />
            </td>
        </tr>
         <tr>
            <td> Fecha Anulación
                <asp:TextBox ID="txtFechaAnula" enabled="false" CssClass="textbox" runat="server" Width="70"></asp:TextBox>
            </td>
        </tr>
        <tr><td></td></tr>
        <tr>
            <td>
            <div id="gvDiv">
                <asp:GridView ID="gvOperacion" runat="server" Width="100%" 
                    AutoGenerateColumns="False" PageSize="20" BackColor="White" 
                    BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
                    ForeColor="Black" GridLines="Vertical" 
                    onrowediting="gvOperacion_RowEditing">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:BoundField DataField="COD_OPE" HeaderStyle-CssClass="gridColNo" 
                            ItemStyle-CssClass="gridColNo" > <HeaderStyle CssClass="gridColNo"></HeaderStyle>
                            <ItemStyle CssClass="gridColNo" HorizontalAlign="Left"></ItemStyle>
                        </asp:BoundField>             
                        <asp:TemplateField HeaderStyle-CssClass="gridIco"  ItemStyle-CssClass="gridIco">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnEditar" runat="server" CommandName="Edit" ImageUrl="~/Images/gr_edit.jpg" ToolTip="Modificar"/>
                            </ItemTemplate>
                            <HeaderStyle CssClass="gridIco"></HeaderStyle>
                            <ItemStyle CssClass="gridIco"></ItemStyle>
                        </asp:TemplateField>   
                        <asp:BoundField DataField="cod_ope" HeaderText="Cod Operación" />
                        <asp:BoundField DataField="codobligacion" HeaderText="Nro Obligación" />
                        <asp:BoundField DataField="entidad" HeaderText="Entidad" />
                        <asp:BoundField DataField="nrocuota" HeaderText="Nro Cuota" />
                        <asp:BoundField DataField="fechacuota" DataFormatString="{0:d}" HeaderText="Fecha Cuota" />
                        <asp:BoundField DataField="fechareal" DataFormatString="{0:d}" HeaderText="Fecha Real" />
                        <asp:BoundField DataField="tipo_ope" HeaderText="Tipo Operación" />
                        <asp:BoundField DataField="amort_cap" HeaderText="Capital" DataFormatString="{0:N0}">
                          <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="interes_corriente" HeaderText="Int Corriente" DataFormatString="{0:N0}">
                          <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="interes_mora" HeaderText="Int Mora" DataFormatString="{0:N0}">
                          <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="seguro" HeaderText="Seguro" DataFormatString="{0:N0}">
                          <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="cuotatotal" HeaderText="Total" DataFormatString="{0:N0}">
                          <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                          <asp:TemplateField HeaderText="Anular" Visible="False">
                            <ItemTemplate>
                            <asp:CheckBox ID="chkAnula" runat="server" />
                            </ItemTemplate>
                         </asp:TemplateField>
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
                </div>
            </td>
        </tr>
        <tr><td>&#160;</td></tr>
    </table>


</asp:Content>

