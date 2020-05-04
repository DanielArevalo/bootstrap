<%@ Page Title=".: Xpinn - Flujo de Caja :." Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Nuevo.aspx.cs"
    Inherits="Nuevo" %>

<%@ Register Src="~/General/Controles/ctlPlanCuentas.ascx" TagName="ListadoPlanCtas" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc1" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>


<script runat="server">      
</script>

<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:Panel ID="panelLocal" runat="server">
        <table border="0" cellpadding="0" cellspacing="0" width="80%">
            <tr>
                <td style="text-align: left;"></td>
            </tr>
            <tr>
                <td style="text-align: center; width: 200px">Código Concepto</td>
                <td style="text-align: center; width: 400px">Concepto</td>
                <td style="text-align: center; width: 200px">Tipo de Concepto</td>
            </tr>
            <tr>
                <td style="text-align: center">
                    <asp:TextBox ID="txtCodConcepto" runat="server" CssClass="textbox" MaxLength="20" Enabled="false" />
                    <asp:RequiredFieldValidator ID="rfv1" runat="server" Style="font-size: x-small" ControlToValidate="txtConcepto" ErrorMessage="Campo Requerido" SetFocusOnError="True"
                        ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic" /><br />
                </td>
                <td style="text-align: center">
                    <asp:TextBox ID="txtConcepto" runat="server" CssClass="textbox" Width="350px" />
                    <asp:RequiredFieldValidator ID="rfv2" runat="server" Style="font-size: x-small" ControlToValidate="txtConcepto" ErrorMessage="Campo Requerido" SetFocusOnError="True"
                        ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic" /><br />
                </td>
                <td style="text-align: center">
                    <asp:DropDownList ID="ddlTipoConcepto" runat="server" CssClass="textbox" AutoPostBack="True" Width="150px">
                        <asp:ListItem Text="Seleccione un item" Value="0" Selected="True" />
                        <asp:ListItem Text="Ingreso" Value="1" />
                        <asp:ListItem Text="Egreso" Value="2" />
                        <asp:ListItem Text="Otros Egresos" Value="3" />
                        <asp:ListItem Text="Saldo Caja" Value="4" />
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfv3" runat="server" Style="font-size: x-small" ControlToValidate="ddlTipoConcepto" ErrorMessage="Campo Requerido" SetFocusOnError="True"
                        ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic" />
                    <br />
                </td>
                <td style="text-align: left">&nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="3" style="text-align: center; font-weight: bold">Listado de Cuentas <br/> </td>
            </tr>
            <tr>
                <td colspan="3" style="text-align: center;">
                    <asp:Button Style="margin-top: 10px" ID="btnAgregarFila" runat="server" CssClass="btn8" OnClick="btnAgregarFila_Click"
                        OnClientClick="btnAgregarFila_Click" Text="+ Adicionar Detalle" /><br />
                    <br />
                </td>
            </tr>
            <tr>
                <td colspan="3" style="text-align: left">
                    <asp:GridView ID="gvCuentas" HorizontalAlign="Center" DataKeyNames="cod_cuenta_con"
                        runat="server" AllowPaging="True" AutoGenerateColumns="False" BackColor="White"
                        BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="0"
                        OnRowDeleting="gvCuentas_RowDeleting" PageSize="10" ShowFooter="True"
                        Style="font-size: small" Width="80%">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:ImageButton runat="server" CommandName="Delete" ImageUrl="~/Images/gr_elim.jpg" ToolTip="Delete" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' />
                                </ItemTemplate>
                                <ItemStyle Width="16px" />
                            </asp:TemplateField>
                            <%--<asp:TemplateField>
                                <ItemTemplate>
                                    <asp:ImageButton runat="server" CommandName="Edit" ImageUrl="~/Images/gr_edit.jpg" ToolTip="Edit" />
                                </ItemTemplate>
                                <ItemStyle Width="16px" />
                            </asp:TemplateField>--%>
                            <asp:TemplateField HeaderText="Código Cuenta">
                                <ItemTemplate>
                                    <cc1:TextBoxGrid ID="txtCodCuenta" runat="server" AutoPostBack="True" Style="text-align: left" 
                                        BackColor="#F4F5FF" Width="90px" Text='<%# Bind("cod_cuenta") %>' OnTextChanged="txtCodCuenta_TextChanged"
                                        CommandArgument='<%#((GridViewRow) Container).RowIndex %>'>  
                                    </cc1:TextBoxGrid>
                                    <cc1:ButtonGrid ID="btnListadoPlan" CssClass="btnListado" runat="server"
                                        Text="..." OnClick="btnListadoPlan_Click" CommandArgument='<%#((GridViewRow) Container).RowIndex %>' />
                                    <uc1:ListadoPlanCtas ID="ctlListadoPlan" runat="server" OneventotxtCuenta_TextChanged="txtCodCuenta_TextChanged" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Nombre">
                                <ItemTemplate>
                                    <cc1:TextBoxGrid ID="txtNombreCuenta" runat="server" Text='<%# Bind("nom_cuenta") %>'
                                        Style="padding-left: 15px" Width="350px" Enabled="false" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="T.Mov." HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:UpdatePanel ID="UPDATE1" runat="server" UpdateMode="Always">
                                                    <ContentTemplate>
                                                        <cc1:DropDownListGrid MaxLength="10" ID="ddlTipo" runat="server" Style="text-align: center" Width="40px" CssClass="dropdown" SelectedValue='<%# Bind("tipo_mov") %>'
                                                            AutoPostBack="True" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'>
                                                            <asp:ListItem Value="D">D</asp:ListItem>
                                                            <asp:ListItem Value="C">C</asp:ListItem>
                                                        </cc1:DropDownListGrid>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="40px" />
                                        </asp:TemplateField>
                        </Columns>
                        <FooterStyle CssClass="gridHeader" />
                        <HeaderStyle CssClass="gridHeader" />
                        <RowStyle CssClass="gridItem" />
                        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                        <SortedAscendingCellStyle BackColor="#FBFBF2" />
                        <SortedAscendingHeaderStyle BackColor="#848384" />
                        <SortedDescendingCellStyle BackColor="#EAEAD3" />
                        <SortedDescendingHeaderStyle BackColor="#575357" />
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </asp:Panel>

</asp:Content>


