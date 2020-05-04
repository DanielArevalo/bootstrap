<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" Title=".: Xpinn - ProcesosCobro :." %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
<table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td>
                <asp:Panel ID="pConsulta" runat="server">
                <table id="tbCriterios" border="0" cellpadding="5" cellspacing="0" width="100%">
                   <tr>
                       <td class="tdI">
                           Código proceso cobro&nbsp;<br/>
                       <asp:TextBox ID="txtCodprocesocobro" CssClass="textbox" runat="server" MaxLength="128"/>
                           <br />
                           <asp:CompareValidator ID="cvcodprocesocobro" runat="server" 
                               ControlToValidate="txtcodprocesocobro" Display="Dynamic" 
                               ErrorMessage="Solo se admiten números" ForeColor="Red" Operator="DataTypeCheck" 
                               SetFocusOnError="True" Type="Double" ValidationGroup="vgGuardar" />
                       </td>
                       <td class="tdD">
                           Código proceso precede&nbsp;<br/>
                       <asp:TextBox ID="txtCodprocesoprecede" CssClass="textbox" runat="server"/>
                           <br />
                           <asp:CompareValidator ID="cvcodprocesoprecede" runat="server" 
                               ControlToValidate="txtcodprocesoprecede" Display="Dynamic" 
                               ErrorMessage="Solo se admiten números" ForeColor="Red" Operator="DataTypeCheck" 
                               SetFocusOnError="True" Type="Double" ValidationGroup="vgGuardar" />
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI">
                           Descripción&nbsp;<br/>
                       <asp:TextBox ID="txtDescripcion" CssClass="textbox" runat="server" MaxLength="128"/>
                       </td>
                       <td class="tdD">
                           Rango inicial&nbsp;<br/>
                       <asp:TextBox ID="txtRangoinicial" CssClass="textbox" runat="server"/>
                           <br />
                           <asp:CompareValidator ID="cvrangoinicial" runat="server" 
                               ControlToValidate="txtrangoinicial" Display="Dynamic" 
                               ErrorMessage="Solo se admiten números" ForeColor="Red" Operator="DataTypeCheck" 
                               SetFocusOnError="True" Type="Double" ValidationGroup="vgGuardar" />
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI">
                           Rango Final&nbsp;<br/>
                       <asp:TextBox ID="txtRangofinal" CssClass="textbox" runat="server" MaxLength="128"/>
                           <br />
                           <asp:CompareValidator ID="cvrangofinal" runat="server" 
                               ControlToValidate="txtrangofinal" Display="Dynamic" 
                               ErrorMessage="Solo se admiten números" ForeColor="Red" Operator="DataTypeCheck" 
                               SetFocusOnError="True" Type="Double" ValidationGroup="vgGuardar" />
                       </td>
                       <td class="tdD">
                       &nbsp;
                       </td>
                </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td><hr width="100%" noshade /></td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="gvLista" runat="server" Width="100%" AutoGenerateColumns="False" OnRowDataBound="gvLista_RowDataBound" OnRowDeleting="gvLista_RowDeleting" AllowPaging="True" OnPageIndexChanging="gvLista_PageIndexChanging" onselectedindexchanged="gvLista_SelectedIndexChanged" onrowediting="gvLista_RowEditing" PageSize="20" HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager" RowStyle-CssClass="gridItem"  DataKeyNames="codprocesocobro" >
                    <Columns>
                        <asp:BoundField DataField="codprocesocobro" HeaderStyle-CssClass="gridColNo" 
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
                        <asp:BoundField DataField="codprocesocobro" HeaderText="Código proceso" />
                        <asp:BoundField DataField="codprocesoprecede" HeaderText="Código precede" />
                        <asp:BoundField DataField="descripcion" HeaderText="Descripción" >
                        <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="rangoinicial" HeaderText="Rango inicial" />
                        <asp:BoundField DataField="rangofinal" HeaderText="Rango final" />
                    </Columns>

<HeaderStyle CssClass="gridHeader"></HeaderStyle>

<PagerStyle CssClass="gridPager"></PagerStyle>

<RowStyle CssClass="gridItem"></RowStyle>
                </asp:GridView>
                <asp:Label ID="lblTotalRegs" runat="server" Visible="False"/>
            </td>
        </tr>
    </table>
  <%--  <script type="text/javascript" language="javascript">
        function SetFocus()
        {
            document.getElementById('cphMain_txtcodprocesocobro').focus(); 
        }
        window.onload = SetFocus;
    </script>--%>
</asp:Content>