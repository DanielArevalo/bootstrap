<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphmain" Runat="Server">
    <table cellpadding="0" cellspacing="0" style="width: 100%">
        <tr>
            <td >
                <hr  width="100%" noshade >
            </td>
        </tr>
         <tr>
            <td> Oficina
             <asp:DropDownList ID="ddlOficinas" Width="250" class="dropdown" runat="server" 
                    onselectedindexchanged="ddlOficinas_SelectedIndexChanged" Enabled="False"
                    AutoPostBack="True">
        </asp:DropDownList>
       
            </td>
        </tr>
        <tr><td></td></tr>
        <tr>
            <td>
                <asp:GridView ID="gvCajeros" runat="server" Width="100%" 
                    AutoGenerateColumns="False" AllowPaging="True" PageSize="20" BackColor="White" 
                    BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
                    ForeColor="Black" GridLines="Vertical" 
                    onselectedindexchanged="gvCajeros_SelectedIndexChanged" 
                    onrowdeleting="gvCajeros_RowDeleting" onrowediting="gvCajeros_RowEditing" 
                    onrowdatabound="gvCajeros_RowDataBound">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:TemplateField HeaderStyle-CssClass="gridIco">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnInfo" runat="server" CommandName="Select" ImageUrl="~/Images/gr_info.jpg"
                                    ToolTip="Detalle" Width="16px" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:ImageButton ID="btnEditar" runat="server" CommandName="Edit" ImageUrl="~/Images/gr_edit.jpg"
                                    ToolTip="Editar" Width="16px" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-CssClass="gridIco">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnEliminar" runat="server" CommandName="Delete" ImageUrl="~/Images/gr_elim.jpg"
                                    ToolTip="Eliminar" Width="16px" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="cod_cajero" HeaderText="Cód. Cajero" />
                        <asp:BoundField DataField="nom_cajero" HeaderText="Nombre" />
                        <asp:BoundField DataField="nom_caja" HeaderText="Caja" />
                        <asp:BoundField DataField="fecha_ingreso" DataFormatString="{0:d}"  HeaderText="Fecha Ingreso" />
                        <asp:BoundField DataField="fecharetiro" DataFormatString="{0:g}"  HtmlEncode="false" HeaderText="Fecha Retiro" />
                        <asp:BoundField DataField="nomestado" HeaderText="Estado" />
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
                <asp:Label ID="lblTotalRegs" runat="server" Visible="False"/>
                <asp:Label ID="lblInfo" runat="server" Text="Su consulta no obtuvo ningun resultado." Visible="False"/>
            </td>
        </tr>
    </table>
</asp:Content>
