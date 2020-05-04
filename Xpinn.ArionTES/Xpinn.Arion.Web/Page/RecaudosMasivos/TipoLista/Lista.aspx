<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" Title=".: Xpinn - Tipo Lista :." %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" width="80%">
        <tr>
            <td>
                <asp:Panel ID="pConsulta" runat="server" Width="80%">
                    <table id="tbCriterios" border="0" cellpadding="5" cellspacing="0" width="100%">
                       <tr>
                           <td>
                               Tipo Lista<br/>
                               <asp:TextBox ID="txtTipoLista" runat="server" CssClass="textbox" Width="30%" />
                           </td>
                           <td>
                               Descripcion<br/>
                               <asp:TextBox ID="txtDescripcion" runat="server" CssClass="textbox" MaxLength="300" Width="70%" />
                           </td>
                       </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td><hr /></td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="gvLista" runat="server" Width="100%" AutoGenerateColumns="False" OnRowDataBound="gvLista_RowDataBound" OnRowDeleting="gvLista_RowDeleting" 
                AllowPaging="True" OnPageIndexChanging="gvLista_PageIndexChanging" onselectedindexchanged="gvLista_SelectedIndexChanged" onrowediting="gvLista_RowEditing" 
                PageSize="20" HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager" RowStyle-CssClass="gridItem"  DataKeyNames="idtipo_lista" >
                    <Columns>
                        <asp:BoundField DataField="idtipo_lista" HeaderStyle-CssClass="gridColNo" ItemStyle-CssClass="gridColNo" >
                            <HeaderStyle CssClass="gridColNo"></HeaderStyle>
                            <ItemStyle CssClass="gridColNo"></ItemStyle>
                        </asp:BoundField>
                        <asp:TemplateField HeaderStyle-CssClass="gridIco"  ItemStyle-CssClass="gridIco">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnEditar" runat="server" CommandName="Edit" ImageUrl="~/Images/gr_edit.jpg" ToolTip="Modificar"/>
                            </ItemTemplate>
                            <HeaderStyle CssClass="gridIco"></HeaderStyle>                  
                            <ItemStyle CssClass="gridIco"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-CssClass="gridIco"  ItemStyle-CssClass="gridIco">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnBorrar" runat="server" CommandName="Delete" ImageUrl="~/Images/gr_elim.jpg" ToolTip="Borrar" />
                            </ItemTemplate>
                            <HeaderStyle CssClass="gridIco"></HeaderStyle>
                            <ItemStyle CssClass="gI" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="idtipo_lista" HeaderText="Tipo Lista" >
                            <ItemStyle HorizontalAlign="Left" Width="100px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="descripcion" HeaderText="Descripción" >
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                    </Columns>
                    <HeaderStyle CssClass="gridHeader"></HeaderStyle>
                    <PagerStyle CssClass="gridPager"></PagerStyle>
                    <RowStyle CssClass="gridItem"></RowStyle>
                </asp:GridView>
                <asp:Label ID="lblTotalRegs" runat="server" Visible="False"/>
            </td>
        </tr>
    </table>
</asp:Content>