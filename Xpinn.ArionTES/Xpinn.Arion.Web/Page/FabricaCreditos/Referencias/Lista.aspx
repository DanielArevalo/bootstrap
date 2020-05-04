<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" Title=".: Xpinn - Referencia :." %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td>
                <asp:Panel ID="pConsulta" runat="server">
                    <table id="tbCriterios" border="0" cellpadding="5" cellspacing="0" width="100%">
                        <tr>
                            <td class="tdI" style="width: 351px">&nbsp;</td>
                            <td class="tdD">&nbsp;</td>
                            <td class="tdD">&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="tdI" style="width: 351px">Número radicación<br />
                                <asp:TextBox ID="txtNumero_radicacion" runat="server" CssClass="textbox"
                                    MaxLength="128" />
                                <br />
                                <asp:CompareValidator ID="cvNumero_radicacion" runat="server"
                                    ControlToValidate="txtnumero_radicacion" Display="Dynamic"
                                    ErrorMessage="Solo se admiten números" ForeColor="Red" Operator="DataTypeCheck"
                                    SetFocusOnError="True" Type="Double" ValidationGroup="vgGuardar" />
                            </td>
                            <td class="tdD">Identificación&nbsp;<br />
                                <asp:TextBox ID="txtIdentificacion" runat="server" CssClass="textbox" />
                                <br />
                                <asp:CompareValidator ID="cvIdentificacion" runat="server"
                                    ControlToValidate="txtIdentificacion" Display="Dynamic"
                                    ErrorMessage="Solo se admiten números" ForeColor="Red" Operator="DataTypeCheck"
                                    SetFocusOnError="True" Type="Double" ValidationGroup="vgGuardar" />
                            </td>
                            <td class="tdD">Oficina<br />
                                <asp:DropDownList ID="ddlOficinas" runat="server" CssClass="dropdown"
                                    Width="174px">
                                </asp:DropDownList>
                                <br />
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdI" style="width: 351px">Primer apellido&nbsp;<br />
                                <asp:TextBox ID="txtPrimer_apellido" CssClass="textbox" runat="server" MaxLength="128" />
                            </td>
                            <td class="tdD">Segundo apellido&nbsp;<br />
                                <asp:TextBox ID="txtSegundo_apellido" CssClass="textbox" runat="server" />
                            </td>
                            <td class="tdD">Nombre<br />
                                <asp:TextBox ID="txtNombres" runat="server" CssClass="textbox"
                                    MaxLength="128" />
                            </td>
                            <td class="tdD">Código de nómina<br />
                                <asp:TextBox ID="txtCodigoNomina" runat="server" CssClass="textbox" Width="150px" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdI" style="width: 351px">Línea de crédito&nbsp;<br />
                                <asp:TextBox ID="txtLinea_credito" runat="server" CssClass="textbox"
                                    MaxLength="128" />
                            </td>
                            <td class="tdD">&nbsp;</td>
                            <td class="tdD">&nbsp;</td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td>
                <hr width="100%" noshade>
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="gvLista" runat="server" Width="100%" GridLines="Horizontal" AutoGenerateColumns="False" OnRowDataBound="gvLista_RowDataBound" OnRowDeleting="gvLista_RowDeleting" AllowPaging="True" OnPageIndexChanging="gvLista_PageIndexChanging" OnSelectedIndexChanged="gvLista_SelectedIndexChanged" OnRowEditing="gvLista_RowEditing" PageSize="20" HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager" RowStyle-CssClass="gridItem" DataKeyNames="cod_referencia">
                    <Columns>
                        <asp:BoundField DataField="cod_referencia" HeaderStyle-CssClass="gridColNo" ItemStyle-CssClass="gridColNo" />
                        <asp:TemplateField HeaderStyle-CssClass="gridIco" ItemStyle-CssClass="gridIco">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnInfo" runat="server" CommandName="Select" ImageUrl="~/Images/gr_info.jpg" ToolTip="Detalle" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-CssClass="gridIco" ItemStyle-CssClass="gridIco">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnEditar" runat="server" CommandName="Edit" ImageUrl="~/Images/gr_edit.jpg" ToolTip="Modificar" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <%--<asp:TemplateField HeaderStyle-CssClass="gridIco"  ItemStyle-CssClass="gridIco">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnBorrar" runat="server" CommandName="Delete" ImageUrl="~/Images/gr_elim.jpg" ToolTip="Borrar" />
                            </ItemTemplate>
                            <ItemStyle CssClass="gI" />
                        </asp:TemplateField>--%>
                        <asp:BoundField DataField="numero_radicacion" HeaderText="Número solicitud" />
                        <asp:BoundField DataField="identificacion" HeaderText="Identificación" />
                        <asp:BoundField DataField="nombres" HeaderText="Nombre completo" />
                        <asp:BoundField DataField="cod_nomina" HeaderText="Código de nómina" />
                        <asp:BoundField DataField="referenciado" HeaderText="Referenciado" />
                        <asp:BoundField DataField="oficina" HeaderText="Oficina" />
                        <asp:BoundField DataField="tipo_referencia" HeaderText="Tipo referencia" />
                        <asp:BoundField DataField="vinculo" HeaderText="Vínculo" />
                        <asp:BoundField DataField="nombre_referenciado" HeaderText="Referencia" />
                        <asp:BoundField DataField="estado" HeaderText="Estado" />
                    </Columns>
                </asp:GridView>
                <asp:Label ID="lblTotalRegs" runat="server" Visible="False" />
            </td>
        </tr>
    </table>
    <%--<script type="text/javascript" language="javascript">
        function SetFocus()
        {
            document.getElementById('ctl00_cphMain_txtNumero_radicacion').focus(); 
        }
        window.onload = SetFocus;
    </script>--%>
</asp:Content>
