<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" Title=".: Xpinn - CalificacionReferencias :." %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
<table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td>
                <asp:Panel ID="pConsulta" runat="server">
                <table id="tbCriterios" border="0" cellpadding="5" cellspacing="0" width="100%">
                   <tr>
                       <td class="tdI">
                           Tipo calificacion referencia&nbsp;<br/>
                       <asp:TextBox ID="txtTipocalificacionref" CssClass="textbox" runat="server" MaxLength="128"/>
                           <br />
                           <asp:CompareValidator ID="cvTipoCalificacionRef" runat="server" 
                               ControlToValidate="txtTipoCalificacionRef" Display="Dynamic" 
                               ErrorMessage="Solo se admiten n�meros" ForeColor="Red" Operator="DataTypeCheck" 
                               SetFocusOnError="True" Type="Double" ValidationGroup="vgGuardar" />
                       </td>
                       <td class="tdD">
                       Nombre&nbsp;<br/>
                       <asp:TextBox ID="txtNombre" CssClass="textbox" runat="server"/>
                       </td>
                   </tr>
                </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td><hr width="100%" noshade></td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="gvLista" runat="server" Width="100%" AutoGenerateColumns="False" OnRowDataBound="gvLista_RowDataBound" OnRowDeleting="gvLista_RowDeleting" AllowPaging="True" OnPageIndexChanging="gvLista_PageIndexChanging" onselectedindexchanged="gvLista_SelectedIndexChanged" onrowediting="gvLista_RowEditing" PageSize="20" HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager" RowStyle-CssClass="gridItem"  DataKeyNames="TipoCalificacionRef" >
                    <Columns>
                        <asp:BoundField DataField="TipoCalificacionRef" HeaderStyle-CssClass="gridColNo" ItemStyle-CssClass="gridColNo" />
                        <asp:TemplateField HeaderStyle-CssClass="gridIco"  ItemStyle-CssClass="gridIco">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnInfo" runat="server" CommandName="Select" ImageUrl="~/Images/gr_info.jpg" ToolTip="Detalle"/>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-CssClass="gridIco"  ItemStyle-CssClass="gridIco">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnEditar" runat="server" CommandName="Edit" ImageUrl="~/Images/gr_edit.jpg" ToolTip="Modificar"/>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-CssClass="gridIco"  ItemStyle-CssClass="gridIco">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnBorrar" runat="server" CommandName="Delete" ImageUrl="~/Images/gr_elim.jpg" ToolTip="Borrar" />
                            </ItemTemplate>
                            <ItemStyle CssClass="gI" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="Tipocalificacionref" HeaderText="Tipo calificaci�n referencia" />
                        <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                    </Columns>
                </asp:GridView>
                <asp:Label ID="lblTotalRegs" runat="server" Visible="False"/>
            </td>
        </tr>
    </table>
    <script type="text/javascript" language="javascript">
        function SetFocus()
        {
            document.getElementById('cphMain_txtTipoCalificacionRef').focus(); 
        }
        window.onload = SetFocus;
    </script>
</asp:Content>