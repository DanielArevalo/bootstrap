<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ctlCuotasExtras.ascx.cs" Inherits="General_Controles_ctlCuotasExtras" %>
<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>
<%@ Register TagPrefix="uc3" TagName="fecha" Src="~/General/Controles/fecha.ascx" %>

<asp:UpdatePanel ID="UpdatePanelCuoExt" runat="server">
    <ContentTemplate>
        <asp:Panel runat="server" ID="panelCuotasExtras" Visible="True">
            <table style="width: 100%">
                <tr>
                    <td style="text-align: left; color: #FFFFFF; background-color: #0066FF; height: 20px;" colspan="3">
                        <strong>Cuotas Extras</strong>
                    </td>
                </tr>
                <tr>
                    <td>
                        <%-->Variables para validacion del monto al agregar una cuota extra Inicio<--%>
                        <asp:TextBox runat="server" ID="txtMonto" Style="display: none"></asp:TextBox>
                        <asp:TextBox runat="server" ID="txtPlazoTxt" Style="display: none"></asp:TextBox>
                        <asp:TextBox runat="server" ID="txtDiasPeriodicidad" Style="display: none"></asp:TextBox>
                        <asp:TextBox runat="server" ID="txtCodPeriodicidad" Style="display: none"></asp:TextBox>
                        <%-->Variables para validacion del monto al agregar una cuota extra fin  <--%>

                        <asp:GridView ID="gvCuoExt" AutoPostBack="True" runat="server" Width="40%" ShowHeaderWhenEmpty="True"
                            EmptyDataText="No se encontraron registros." AutoGenerateColumns="False" PageSize="5"
                            BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px"
                            CellPadding="4" ForeColor="Black" GridLines="Vertical" Height="16px" ShowFooter="True"
                            Style="font-size: x-small" OnRowDataBound="gvCuoExt_RowDataBound"
                            OnRowDeleting="gvCuoExt_RowDeleting" OnRowCommand="gvCuoExt_RowCommand"
                            OnPageIndexChanging="gvCuoExt_PageIndexChanging">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:TemplateField ShowHeader="False">
                                    <FooterTemplate>
                                        <asp:ImageButton ID="btnNuevo" runat="server" CausesValidation="False" CommandName="AddNew"
                                            ImageUrl="~/Images/gr_nuevo.jpg" ToolTip="Crear Nuevo" Width="16px" />
                                    </FooterTemplate>
                                    <ItemStyle Width="20px" />
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnEliminar" runat="server" CommandName="Delete" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                            ImageUrl="~/Images/gr_elim.jpg" ToolTip="Eliminar" Width="16px" />
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="gridIco" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Fecha Pago">
                                    <ItemTemplate>
                                        <asp:Label ID="lblfechapago" runat="server" Text='<%# Bind("fecha_pago","{0:d}") %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtfechapago" runat="server" Text='<%# Bind("fecha_pago") %>'></asp:TextBox>
                                        <asp:CalendarExtender ID="calExtfechapago" Format="dd/MM/yyyy" runat="server" TargetControlID="txtfechapago" />
                                    </FooterTemplate>
                                    <ItemStyle Width="50px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Forma Pago">
                                    <ItemTemplate>
                                        <asp:Label ID="lblformapago" runat="server" Text='<%# Bind("des_forma_pago") %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:DropDownList ID="ddlformapago" runat="server" Width="164px" CssClass="dropdown">
                                            <asp:ListItem Value="1">Caja</asp:ListItem>
                                            <asp:ListItem Value="2">Nomina</asp:ListItem>
                                        </asp:DropDownList>
                                    </FooterTemplate>
                                    <ItemStyle Width="50px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Valor">
                                    <ItemTemplate>
                                        <asp:Label ID="lblvalor" runat="server" Text='<%# Bind("valor", "{0:N}") %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtvalor" runat="server" Text='<%# Bind("valor", "{0:N}") %>'></asp:TextBox>
                                        <asp:FilteredTextBoxExtender ID="txtValor_FilteredTextBoxExtender" runat="server"
                                            Enabled="True" FilterType="Numbers, Custom" TargetControlID="txtvalor" ValidChars="." />
                                    </FooterTemplate>
                                    <ItemStyle Width="50px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Tipo Cuota Extra">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcodtipocuota" runat="server" Text='<%# Bind("tipo_cuota") %>'></asp:Label>
                                        <asp:Label ID="lbltipocuota" runat="server" Text='<%# Bind("des_tipo_cuota") %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:DropDownList ID="ddltipocuotagv" runat="server" Width="164px" CssClass="dropdown">
                                        </asp:DropDownList>
                                    </FooterTemplate>
                                    <ItemStyle Width="50px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cod.Forma Pago" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcodformapago" runat="server" Text='<%# Bind("forma_pago") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle CssClass="gridHeader" />
                            <HeaderStyle CssClass="gridHeader" />
                            <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                            <RowStyle CssClass="gridItem" />
                            <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                            <SortedAscendingCellStyle BackColor="#FBFBF2" />
                            <SortedAscendingHeaderStyle BackColor="#848384" />
                            <SortedDescendingCellStyle BackColor="#EAEAD3" />
                            <SortedDescendingHeaderStyle BackColor="#575357" />
                        </asp:GridView>
                        <br />
                        <div id="divGenerar" runat="server" visible="true">
                            <table>
                                <tr>
                                    <td colspan="2">Generar Cuotas Extras:
                                    </td>
                                    <td rowspan="7" style="text-align: center">
                                        <asp:Button ID="btnGenerarCuotaext" runat="server" CssClass="btn8" Text="Generar Cuota Ext." OnClick="btnGenerarCuotaext_Click" />
                                        <br />
                                        <br />
                                        <asp:Button ID="btnLimpiarCuotaext" runat="server" CssClass="btn8" Text="Limpiar" OnClick="btnLimpiarCuotaext_Click" />

                                    </td>
                                </tr>
                                <tr>
                                    <td>Porcentaje del Credito en Cuotas Extras</td>
                                    <td>
                                        <asp:TextBox ID="txtPorcentaje" runat="server" CssClass="textbox"
                                            Width="136px"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td>Numero de Cuotas Extras</td>
                                    <td>
                                        <asp:TextBox ID="txtNumeroCuotaExt" runat="server" CssClass="textbox"
                                            Width="136px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Fecha de primera Cuota Extra</td>
                                    <td>
                                        <uc3:fecha ID="txtFechaCuotaExt" Width="136px" CssClass="textbox" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>Valor Cuota Extra</td>
                                    <td>
                                        <asp:TextBox ID="txtValorCuotaExt" runat="server" CssClass="textbox"
                                            Width="136px"></asp:TextBox></td>

                                </tr>
                                <tr>
                                    <td>Forma de Pago</td>
                                    <td>
                                        <asp:DropDownList ID="ddlFormaPagoCuotaExt" runat="server" Width="148px" CssClass="dropdown">
                                            <asp:ListItem Value="1">Caja</asp:ListItem>
                                            <asp:ListItem Value="2">Nomina</asp:ListItem>
                                        </asp:DropDownList>

                                    </td>
                                </tr>
                                <tr>
                                    <td>Periodicidad de Cuota Extra</td>
                                    <td>
                                        <asp:DropDownList ID="ddlPeriodicidadCuotaExt" runat="server" CssClass="textbox" Width="148px">
                                        </asp:DropDownList></td>

                                </tr>
                                <tr>
                                    <td>Tipo Cuota Extra</td>
                                    <td>
                                        <asp:DropDownList ID="ddlCuotaExtTipo" runat="server" CssClass="textbox" Width="148px">
                                        </asp:DropDownList></td>

                                </tr>
                            </table>
                        </div>
                        <asp:Label runat="server" ID="lblError" ForeColor="Red"></asp:Label>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel runat="server" Visible="False" ID="panelCuotasExtrasEdit">
            <asp:GridView ID="gvCuoExtEdit" AutoPostBack="True" runat="server" Width="40%" ShowHeaderWhenEmpty="True"
                EmptyDataText="No se encontraron registros." AutoGenerateColumns="False" PageSize="5"
                BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px"
                CellPadding="4" ForeColor="Black" GridLines="Vertical" Height="16px" ShowFooter="True"
                Style="font-size: x-small" OnRowDataBound="gvCuoExt_RowDataBound"
                OnRowDeleting="gvCuoExt_RowDeleting" OnRowCommand="gvCuoExt_RowCommand" OnPageIndexChanging="gvCuoExt_PageIndexChanging">
                <Columns>
                    <asp:TemplateField>
                        <FooterTemplate>
                            <asp:ImageButton ID="btnNuevo" runat="server" CausesValidation="False" CommandName="AddNew"
                                ImageUrl="~/Images/gr_nuevo.jpg" ToolTip="Crear Nuevo" Width="16px" />
                        </FooterTemplate>
                        <ItemStyle Width="20px" />
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:ImageButton ID="btnEliminar" runat="server" CommandName="Delete" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                ImageUrl="~/Images/gr_elim.jpg" ToolTip="Eliminar" Width="16px" />
                        </ItemTemplate>
                        <HeaderStyle CssClass="gridIco" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Fecha Pago">
                        <ItemTemplate>
                            <asp:TextBox runat="server" ID="CodCuota" Text='<%# Bind("cod_cuota")%>' Style="display: none"></asp:TextBox>
                            <asp:TextBox runat="server" ID="txtSaldoCapital" Text='<%# Bind("saldo_capital")%>' Style="display: none"></asp:TextBox>
                            <asp:TextBox ID="lblfechapago" runat="server" Text='<%# Bind("fecha_pago","{0:d}") %>'></asp:TextBox>
                            <asp:CalendarExtender ID="calExtfechapago" Format="dd/MM/yyyy" runat="server" TargetControlID="lblfechapago" />
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtfechapago" runat="server" Text='<%# Bind("fecha_pago") %>'></asp:TextBox>
                            <asp:CalendarExtender ID="calExtfechapago" Format="dd/MM/yyyy" runat="server" TargetControlID="txtfechapago" />
                        </FooterTemplate>
                        <ItemStyle Width="50px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Forma Pago">
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlformapago" runat="server" Width="164px" CssClass="dropdown">
                                <asp:ListItem Value="1">Caja</asp:ListItem>
                                <asp:ListItem Value="2">Nomina</asp:ListItem>
                            </asp:DropDownList>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:DropDownList ID="ddlformapago" runat="server" Width="164px" CssClass="dropdown">
                                <asp:ListItem Value="1">Caja</asp:ListItem>
                                <asp:ListItem Value="2">Nomina</asp:ListItem>
                            </asp:DropDownList>
                        </FooterTemplate>
                        <ItemStyle Width="50px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Valor">
                        <ItemTemplate>
                            <asp:TextBox ID="lblvalor" runat="server" />
                            <asp:FilteredTextBoxExtender ID="txtValor_FilteredTextBoxExtender" runat="server"
                                Enabled="True" FilterType="Numbers, Custom" TargetControlID="lblvalor" ValidChars="." />
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtvalor" runat="server"></asp:TextBox>
                            <asp:FilteredTextBoxExtender ID="txtValor_FilteredTextBoxExtender" runat="server"
                                Enabled="True" FilterType="Numbers, Custom" TargetControlID="txtvalor" ValidChars="." />
                        </FooterTemplate>
                        <ItemStyle Width="50px" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Tipo Cuota Extra">
                        <ItemTemplate>
                            <asp:DropDownList ID="ddltipocuotagvs" runat="server" Width="164px" CssClass="dropdown">
                            </asp:DropDownList>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:DropDownList ID="ddltipocuotagv" runat="server" Width="164px" CssClass="dropdown">
                            </asp:DropDownList>
                        </FooterTemplate>
                        <ItemStyle Width="50px" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Cod.Forma Pago" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblcodformapago" runat="server" Text='<%# Bind("forma_pago") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <FooterStyle CssClass="gridHeader" />
                <HeaderStyle CssClass="gridHeader" />
                <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                <RowStyle CssClass="gridItem" />
                <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#FBFBF2" />
                <SortedAscendingHeaderStyle BackColor="#848384" />
                <SortedDescendingCellStyle BackColor="#EAEAD3" />
                <SortedDescendingHeaderStyle BackColor="#575357" />
            </asp:GridView>
        </asp:Panel>
    </ContentTemplate>
</asp:UpdatePanel>
<script>
    function Recargar() {
        alert("Se recargara la pagina para agregar el nuevo codeudor.")
    }
</script>
