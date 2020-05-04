<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" Title=".: Xpinn - Usuario :." %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
<table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td style="text-align:left">
                Ingresar criterios de Búsqueda:
                <asp:Panel ID="pConsulta" runat="server">
                <table id="tbCriterios" border="0" cellpadding="5" cellspacing="0" width="80%">
                   <tr>
                       <td class="tdI" style="text-align:left">
                           Identificacion<br/>
                           <asp:TextBox ID="txtIdentificacion" runat="server" CssClass="textbox" Width="100px" />
                       </td>
                       <td class="tdD" style="text-align:left">
                           Nombre<br/>
                           <asp:TextBox ID="txtNombre" runat="server" CssClass="textbox" MaxLength="128" Width="200px" />
                       </td>
                       <td class="tdI" style="text-align:left">
                           Estado<br/>
                           <asp:DropDownList ID="txtEstado" runat="server" CssClass="textbox" 
                               Width="120px">
                           <asp:ListItem Text="Todos" Value="" Selected="True"/>
                           <asp:ListItem Text="0 - Inactivos" Value="0"/>
                           <asp:ListItem Text="1 - Activos" Value="1"/>
                           <asp:ListItem Text="2 - Bloqueados" Value="2"/>
                           </asp:DropDownList>
                       </td>
                       <td class="tdD" style="text-align:left">
                           Perfil<br/>
                           <asp:DropDownList ID="txtCodperfil" runat="server" CssClass="textbox" Width="200px" />
                       </td>
                       <td class="tdD" style="text-align:left">
                           &nbsp;</td>
                   </tr>
                </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td><hr width="100%" /></td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="gvLista" runat="server" Width="100%" AutoGenerateColumns="False" OnRowDataBound="gvLista_RowDataBound" OnRowDeleting="gvLista_RowDeleting" AllowPaging="True" 
                    OnPageIndexChanging="gvLista_PageIndexChanging" onselectedindexchanged="gvLista_SelectedIndexChanged" onrowediting="gvLista_RowEditing" PageSize="20" HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager" RowStyle-CssClass="gridItem"  DataKeyNames="codusuario" >
                    <Columns>
                        <asp:BoundField DataField="codusuario" HeaderStyle-CssClass="gridColNo" 
                            ItemStyle-CssClass="gridColNo" >
                            <HeaderStyle CssClass="gridColNo"></HeaderStyle>
                            <ItemStyle CssClass="gridColNo"></ItemStyle>
                        </asp:BoundField>
                        <asp:TemplateField HeaderStyle-CssClass="gridIco"  ItemStyle-CssClass="gridIco">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnInfo" runat="server" CommandName="Select" ImageUrl="~/Images/gr_info.jpg" ToolTip="Detalle"/>
                            </ItemTemplate>
                            <HeaderStyle CssClass="gridIco"></HeaderStyle>
                            <ItemStyle CssClass="gridIco"></ItemStyle>
                        </asp:TemplateField>
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
                        <asp:BoundField DataField="identificacion" HeaderText="Identificacion" >
                        <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>                        
                        <asp:BoundField DataField="nombre" HeaderText="Nombre" >
                        <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="cod_persona" HeaderText="Código" >
                            <ItemStyle HorizontalAlign="center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="telefono" HeaderText="Telefono" />
                        <asp:BoundField DataField="estado" HeaderText="Estado" />
                        <asp:BoundField DataField="codperfil" HeaderText="Codperfil" />                        
                        <%--<asp:BoundField DataField="clave_sinencriptar" HeaderText="..." />--%>
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