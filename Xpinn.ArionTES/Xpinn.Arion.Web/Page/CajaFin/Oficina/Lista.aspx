<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" 
CodeFile="Lista.aspx.cs" Inherits="Lista" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphmain" Runat="Server">
    <table cellpadding="0" cellspacing="0" style="width: 100%">
        <tr>
            <td>
                <asp:Panel ID="pConsulta" runat="server">
                <table cellpadding="5" cellspacing="0" style="width: 100%">
                    <tr>
                        <td class="tdI">
                            Código<br />
                            <asp:TextBox ID="txtCodigo" runat="server" CssClass="textbox"></asp:TextBox>
                        </td>
                        <td class="tdD">
                            Oficina<br />
                            <asp:TextBox ID="txtOficina" runat="server" CssClass="textbox"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td >
                <hr  width="100%" noshade >
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="gvLista" runat="server" Width="100%" 
                    AutoGenerateColumns="False" AllowPaging="True" PageSize="20" BackColor="White" 
                    BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
                    ForeColor="Black" GridLines="Vertical" onrowediting="gvLista_RowEditing" 
                    onrowdeleting="gvLista_RowDeleting" OnSelectedIndexChanged="gvLista_SelectedIndexChanged" onrowdatabound="gvLista_RowDataBound">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                     <asp:TemplateField HeaderStyle-CssClass="gridIco">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnInfo" runat="server" CommandName="Select" ImageUrl="~/Images/gr_info.jpg"
                                    ToolTip="Detalle" Width="16px" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-CssClass="gridIco">
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
                        <asp:BoundField DataField="cod_oficina" HeaderText="Código" />
                        <asp:BoundField DataField="nombre" HeaderText="Oficina" />
                        <asp:BoundField DataField="fecha_creacion" DataFormatString="{0:d}"  HeaderText="Fecha Creación" />
                        <asp:BoundField DataField="nom_ciudad" HeaderText="Ciudad" />
                        <asp:BoundField DataField="direccion" HeaderText="Dirección" />
                        <asp:BoundField DataField="nom_persona" HeaderText="Encargado" />
                        <asp:BoundField DataField="nom_centro" HeaderText="Centro de Costo" />
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
    <script type="text/javascript" language="javascript">
        function SetFocus() {
            document.getElementById('cphMain_txtCodigo').focus();
        }
        window.onload = SetFocus;
     </script>
</asp:Content>


