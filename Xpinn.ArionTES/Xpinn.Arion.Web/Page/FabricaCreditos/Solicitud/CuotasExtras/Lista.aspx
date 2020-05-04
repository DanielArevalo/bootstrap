<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Lista.aspx.cs" Inherits="Lista" Title=".: Xpinn - CuotasExtras :." %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td>
                <asp:Panel ID="pConsulta" runat="server" Visible="false">
                    <table id="tbCriterios" border="0" cellpadding="5" cellspacing="0" width="100%">
                        <tr>
                            <td class="tdI">
                                Cod_cuota&nbsp;<asp:CompareValidator ID="cvCOD_CUOTA" runat="server" ControlToValidate="txtCOD_CUOTA"
                                    ErrorMessage="Solo se admiten n&uacute;meros" Operator="DataTypeCheck" SetFocusOnError="True"
                                    Type="Double" ValidationGroup="vgGuardar" Display="Dynamic" ForeColor="Red" /><br />
                                <asp:TextBox ID="txtCod_cuota" CssClass="textbox" runat="server" MaxLength="128" />
                            </td>
                            <td class="tdD">
                                Numero_radicacion&nbsp;<asp:CompareValidator ID="cvNUMERO_RADICACION" runat="server"
                                    ControlToValidate="txtNUMERO_RADICACION" ErrorMessage="Solo se admiten n&uacute;meros"
                                    Operator="DataTypeCheck" SetFocusOnError="True" Type="Double" ValidationGroup="vgGuardar"
                                    Display="Dynamic" ForeColor="Red" /><br />
                                <asp:TextBox ID="txtNumero_radicacion" CssClass="textbox" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdI">
                                Fecha_pago&nbsp;<asp:CompareValidator ID="cvFECHA_PAGO" runat="server" ControlToValidate="txtFECHA_PAGO"
                                    ErrorMessage="Formato de Fecha (dd/mm/aaaa)" Operator="DataTypeCheck" SetFocusOnError="True"
                                    ToolTip="Formato fecha" Type="Date" ValidationGroup="vgGuardar" Display="Dynamic"
                                    ForeColor="Red" /><br />
                                <asp:TextBox ID="txtFecha_pago" CssClass="textbox" runat="server" MaxLength="128" />
                            </td>
                            <td class="tdD">
                                Valor&nbsp;<asp:CompareValidator ID="cvVALOR" runat="server" ControlToValidate="txtVALOR"
                                    ErrorMessage="Solo se admiten n&uacute;meros" Operator="DataTypeCheck" SetFocusOnError="True"
                                    Type="Double" ValidationGroup="vgGuardar" Display="Dynamic" ForeColor="Red" /><br />
                                <asp:TextBox ID="txtValor" CssClass="textbox" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdI">
                                Valor_capital&nbsp;<asp:CompareValidator ID="cvVALOR_CAPITAL" runat="server" ControlToValidate="txtVALOR_CAPITAL"
                                    ErrorMessage="Solo se admiten n&uacute;meros" Operator="DataTypeCheck" SetFocusOnError="True"
                                    Type="Double" ValidationGroup="vgGuardar" Display="Dynamic" ForeColor="Red" /><br />
                                <asp:TextBox ID="txtValor_capital" CssClass="textbox" runat="server" MaxLength="128" />
                            </td>
                            <td class="tdD">
                                Valor_interes&nbsp;<asp:CompareValidator ID="cvVALOR_INTERES" runat="server" ControlToValidate="txtVALOR_INTERES"
                                    ErrorMessage="Solo se admiten n&uacute;meros" Operator="DataTypeCheck" SetFocusOnError="True"
                                    Type="Double" ValidationGroup="vgGuardar" Display="Dynamic" ForeColor="Red" /><br />
                                <asp:TextBox ID="txtValor_interes" CssClass="textbox" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdI">
                                Saldo_capital&nbsp;<asp:CompareValidator ID="cvSALDO_CAPITAL" runat="server" ControlToValidate="txtSALDO_CAPITAL"
                                    ErrorMessage="Solo se admiten n&uacute;meros" Operator="DataTypeCheck" SetFocusOnError="True"
                                    Type="Double" ValidationGroup="vgGuardar" Display="Dynamic" ForeColor="Red" /><br />
                                <asp:TextBox ID="txtSaldo_capital" CssClass="textbox" runat="server" MaxLength="128" />
                            </td>
                            <td class="tdD">
                                Saldo_interes&nbsp;<asp:CompareValidator ID="cvSALDO_INTERES" runat="server" ControlToValidate="txtSALDO_INTERES"
                                    ErrorMessage="Solo se admiten n&uacute;meros" Operator="DataTypeCheck" SetFocusOnError="True"
                                    Type="Double" ValidationGroup="vgGuardar" Display="Dynamic" ForeColor="Red" /><br />
                                <asp:TextBox ID="txtSaldo_interes" CssClass="textbox" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdI">
                                Forma_pago&nbsp;<br />
                                <asp:TextBox ID="txtForma_pago" CssClass="textbox" runat="server" MaxLength="128" />
                            </td>
                            <td class="tdD">
                                &nbsp;
                            </td>
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
                <asp:GridView ID="gvLista" runat="server" Width="100%" AutoGenerateColumns="False"
                    OnRowDataBound="gvLista_RowDataBound" OnRowDeleting="gvLista_RowDeleting" AllowPaging="True"
                    OnPageIndexChanging="gvLista_PageIndexChanging" OnSelectedIndexChanged="gvLista_SelectedIndexChanged"
                    OnRowEditing="gvLista_RowEditing" PageSize="20" HeaderStyle-CssClass="gridHeader"
                    PagerStyle-CssClass="gridPager" RowStyle-CssClass="gridItem" DataKeyNames="COD_CUOTA">
                    <Columns>
                        <%--<asp:BoundField DataField="COD_CUOTA" HeaderStyle-CssClass="gridColNo" ItemStyle-CssClass="gridColNo" />--%>
                        <asp:BoundField DataField="COD_CUOTA" HeaderText="Cod_cuota" />
                        <asp:TemplateField HeaderStyle-CssClass="gridIco" ItemStyle-CssClass="gridIco">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnInfo" runat="server" CommandName="Select" ImageUrl="~/Images/gr_info.jpg"
                                    ToolTip="Detalle" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-CssClass="gridIco" ItemStyle-CssClass="gridIco">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnEditar" runat="server" CommandName="Edit" ImageUrl="~/Images/gr_edit.jpg"
                                    ToolTip="Modificar" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-CssClass="gridIco" ItemStyle-CssClass="gridIco">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnBorrar" runat="server" CommandName="Delete" ImageUrl="~/Images/gr_elim.jpg"
                                    ToolTip="Borrar" />
                            </ItemTemplate>
                            <ItemStyle CssClass="gI" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="NUMERO_RADICACION" HeaderText="Numero_radicacion" />
                        <asp:BoundField DataField="FECHA_PAGO" HeaderText="Fecha_pago" />
                        <asp:BoundField DataField="VALOR" HeaderText="Valor" />
                        <asp:BoundField DataField="VALOR_CAPITAL" HeaderText="Valor_capital" />
                        <asp:BoundField DataField="VALOR_INTERES" HeaderText="Valor_interes" />
                        <asp:BoundField DataField="SALDO_CAPITAL" HeaderText="Saldo_capital" />
                        <asp:BoundField DataField="SALDO_INTERES" HeaderText="Saldo_interes" />
                        <asp:BoundField DataField="FORMA_PAGO" HeaderText="Forma_pago" />
                    </Columns>
                </asp:GridView>
                <asp:Label ID="lblTotalRegs" runat="server" Visible="False" />
            </td>
        </tr>
    </table>
 <%--   <script type="text/javascript" language="javascript">
        function SetFocus() {
            document.getElementById('cphMain_txtCOD_CUOTA').focus();
        }
        window.onload = SetFocus;
    </script>--%>
</asp:Content>
