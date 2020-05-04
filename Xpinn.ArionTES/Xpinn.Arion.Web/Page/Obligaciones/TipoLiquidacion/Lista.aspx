<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/General/Master/site.master" CodeFile="Lista.aspx.cs" Inherits="Lista" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
 <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table cellpadding="5" cellspacing="0" style="width: 100%">
        <tr>
            <td>
            <div id="gvDiv">
                <asp:GridView ID="gvTipoLiquidacion" runat="server"
                    AutoGenerateColumns="False" AllowPaging="False" PageSize="20" BackColor="White" 
                    BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
                    ForeColor="Black" GridLines="Vertical" onrowediting="gvTipoLiquidacion_RowEditing" 
                    onrowdeleting="gvTipoLiquidacion_RowDeleting" onrowdatabound="gvTipoLiquidacion_RowDataBound" >
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
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
                         <asp:BoundField DataField="codtipoliquidacion"  HeaderText="Cód Tipo Liq" />
                         <asp:BoundField DataField="descripcion"  HeaderText="Nombre Tipo Liq" />
                         <asp:BoundField DataField="nomtipocuota"  HeaderText="Tipo Cuota" />
                        <asp:BoundField DataField="nomtipopago" HeaderText="Tipo Pago" />
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
        <tr><td>&nbsp;</td></tr>
        </table>
</asp:Content>


