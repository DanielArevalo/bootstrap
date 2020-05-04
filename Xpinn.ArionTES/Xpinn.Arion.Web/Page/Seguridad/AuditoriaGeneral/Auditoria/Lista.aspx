<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" Title=".: Xpinn - Auditoria :." %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
<table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td>
                <asp:Panel ID="pConsulta" runat="server">
                <table id="tbCriterios" border="0" cellpadding="5" cellspacing="0" width="100%">
                   <tr>
                       <td class="tdI">
                       Usuario&nbsp;<asp:CompareValidator ID="cvcodusuario" runat="server" ControlToValidate="txtcodusuario" ErrorMessage="Solo se admiten n&uacute;meros" Operator="DataTypeCheck" SetFocusOnError="True" Type="Double" ValidationGroup="vgGuardar" Display="Dynamic" ForeColor="Red" /><br/>
                       <asp:DropDownList ID="txtCodusuario" runat="server" CssClass="dropdown"/>
                       </td>
                       <td class="tdD">
                       Opción&nbsp;<asp:CompareValidator ID="cvcodopcion" runat="server" ControlToValidate="txtcodopcion" ErrorMessage="Solo se admiten n&uacute;meros" Operator="DataTypeCheck" SetFocusOnError="True" Type="Double" ValidationGroup="vgGuardar" Display="Dynamic" ForeColor="Red" /><br/>
                       <asp:DropDownList ID="txtCodopcion" runat="server" CssClass="dropdown"/>
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI">
                       Fecha&nbsp;<asp:CompareValidator ID="cvfecha" runat="server" ControlToValidate="txtfecha" ErrorMessage="Formato de Fecha (dd/mm/aaaa)" Operator="DataTypeCheck" SetFocusOnError="True" ToolTip="Formato fecha" Type="Date" ValidationGroup="vgGuardar" Display="Dynamic" ForeColor="Red" /><br/>
                       <asp:TextBox ID="txtFecha" CssClass="textbox" runat="server"/>
                       </td>
                       <td class="tdD">
                       Acción&nbsp;<br/>
                       <asp:DropDownList ID="txtAccion" runat="server" CssClass="dropdown">
                           <asp:ListItem>Todos</asp:ListItem>
                           <asp:ListItem Value="1">Crear</asp:ListItem>
                           <asp:ListItem Value="4">Modificar</asp:ListItem>
                           <asp:ListItem Value="5">Eliminar</asp:ListItem>
                           </asp:DropDownList>
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
                <asp:GridView ID="gvLista" runat="server" Width="100%" AutoGenerateColumns="False" AllowPaging="True" OnPageIndexChanging="gvLista_PageIndexChanging" onselectedindexchanged="gvLista_SelectedIndexChanged" PageSize="20" HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager" RowStyle-CssClass="gridItem"  DataKeyNames="cod_auditoria" >
                    <Columns>
                        <asp:BoundField DataField="cod_auditoria" HeaderStyle-CssClass="gridColNo" ItemStyle-CssClass="gridColNo" />
                        <asp:TemplateField HeaderStyle-CssClass="gridIco"  ItemStyle-CssClass="gridIco">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnInfo" runat="server" CommandName="Select" ImageUrl="~/Images/gr_info.jpg" ToolTip="Detalle"/>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="codusuario" HeaderText="Usuario" />
                        <asp:BoundField DataField="codopcion" HeaderText="Opcion" />
                        <asp:BoundField DataField="fecha" HeaderText="Fecha" />
                        <asp:BoundField DataField="IP" HeaderText="IP" />
                        <asp:BoundField DataField="navegador" HeaderText="Navegador" />
                        <asp:BoundField DataField="accion" HeaderText="Accion" />
                        <asp:BoundField DataField="tabla" HeaderText="Tabla" />
                    </Columns>
                </asp:GridView>
                <asp:Label ID="lblTotalRegs" runat="server" Visible="False"/>
            </td>
        </tr>
    </table>
    <script type="text/javascript" language="javascript">
        function SetFocus()
        {
            document.getElementById('cphMain_txtcod_auditoria').focus(); 
        }
        window.onload = SetFocus;
    </script>
</asp:Content>