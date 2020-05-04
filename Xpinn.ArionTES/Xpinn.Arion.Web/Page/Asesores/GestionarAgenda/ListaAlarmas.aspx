<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ListaAlarmas.aspx.cs" Inherits="ListaAlarmas" MasterPageFile="~/General/Master/site.master"%>
<asp:Content ID="Content1" runat="server" contentplaceholderid="cphMain">
<table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td style="text-align: left">
                <asp:Panel ID="pConsulta" runat="server">
                <table id="tbCriterios" border="0" cellpadding="5" cellspacing="0" width="100%">
                   <tr>
                       <td class="tdI">
                           Fecha&nbsp;<br/>
                       <asp:TextBox ID="txtFecha" CssClass="textbox" runat="server"/>
                           <br />
                           <asp:CompareValidator ID="cvfecha" runat="server" ControlToValidate="txtfecha" 
                               Display="Dynamic" ErrorMessage="Formato de Fecha (dd/mm/aaaa)" ForeColor="Red" 
                               Operator="DataTypeCheck" SetFocusOnError="True" ToolTip="Formato fecha" 
                               Type="Date" ValidationGroup="vgGuardar" />
                       </td>
                       <td class="tdD">
                           Hora&nbsp;<br/>
                           <asp:TextBox ID="txtHora" runat="server" CssClass="textbox" MaxLength="128" />
                           <br />
                           <asp:CompareValidator ID="cvhora" runat="server" ControlToValidate="txthora" 
                               Display="Dynamic" ErrorMessage="Solo se admiten números" ForeColor="Red" 
                               Operator="DataTypeCheck" SetFocusOnError="True" Type="Double" 
                               ValidationGroup="vgGuardar" />
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI">
                           Cédula del cliente&nbsp;<br/>
                           <asp:TextBox ID="txtIdcliente" runat="server" CssClass="textbox" 
                               MaxLength="128" />
                           <br />
                           <asp:CompareValidator ID="cvidcliente" runat="server" 
                               ControlToValidate="txtidcliente" Display="Dynamic" 
                               ErrorMessage="Solo se admiten números" ForeColor="Red" Operator="DataTypeCheck" 
                               SetFocusOnError="True" Type="Double" ValidationGroup="vgGuardar" />
                       </td>
                       <td class="tdD">
                           Tipo alarma&nbsp;<br/>
                       <asp:TextBox ID="txtTipoalarma" CssClass="textbox" runat="server"/>
                           <br />
                           <asp:CompareValidator ID="cvtipoalarma" runat="server" 
                               ControlToValidate="txttipoalarma" Display="Dynamic" 
                               ErrorMessage="Solo se admiten números" ForeColor="Red" Operator="DataTypeCheck" 
                               SetFocusOnError="True" Type="Double" ValidationGroup="vgGuardar" />
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI">
                           Estado&nbsp;<br/>
                           <asp:DropDownList ID="ddlEstado" runat="server" CssClass="dropdown">
                               <asp:ListItem Value="0">&lt;Seleccione un Item&gt;</asp:ListItem>
                               <asp:ListItem Value="1">Programada</asp:ListItem>
                               <asp:ListItem Value="2">En proceso</asp:ListItem>
                               <asp:ListItem Value="3">Realizada</asp:ListItem>
                           </asp:DropDownList>
                           <br />
                       </td>
                       <td class="tdD">
                           Tipo actividad<br />
                           <asp:DropDownList ID="ddlTipoActividad" runat="server" CssClass="dropdown">
                           </asp:DropDownList>
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI" colspan="2">
                           <hr width="100%"/>
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
                <asp:GridView ID="gvLista" runat="server" Width="100%" 
                    AutoGenerateColumns="False" OnRowDataBound="gvLista_RowDataBound" 
                    OnRowDeleting="gvLista_RowDeleting" AllowPaging="True" 
                    OnPageIndexChanging="gvLista_PageIndexChanging" 
                    onselectedindexchanged="gvLista_SelectedIndexChanged" 
                    onrowediting="gvLista_RowEditing" HeaderStyle-CssClass="gridHeader" 
                    PagerStyle-CssClass="gridPager" RowStyle-CssClass="gridItem"  
                    DataKeyNames="idalarma" >
                    <Columns>
                        <asp:BoundField DataField="idalarma" HeaderStyle-CssClass="gridColNo" ItemStyle-CssClass="gridColNo" />
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
                        <asp:BoundField DataField="idalarma" HeaderText="Id alarma" />
                        <asp:BoundField DataField="fecha" HeaderText="Fecha" />
                        <asp:BoundField DataField="hora" HeaderText="Hora" />
                        <asp:BoundField DataField="tipoalarma" HeaderText="Tipo alarma" />
                        <asp:BoundField DataField="idcliente" HeaderText="Cliente" />
                        <asp:BoundField DataField="tipoactividad" HeaderText="Tipo" />
                        <asp:BoundField DataField="descripcion" HeaderText="Descripción" />
                        <asp:BoundField DataField="repeticiones" HeaderText="Repeticiones" />
                        <asp:BoundField DataField="estado" HeaderText="Estado" />
                    </Columns>
                </asp:GridView>
                <div style="text-align: left">
                <asp:Label ID="lblTotalRegs" runat="server" Visible="False"/>
                </div>
            </td>
        </tr>
    </table>
    <script type="text/javascript" language="javascript">
        function SetFocus() {
            document.getElementById('cphMain_txtidalarma').focus();
        }
        window.onload = SetFocus;
    </script>
</asp:Content>
