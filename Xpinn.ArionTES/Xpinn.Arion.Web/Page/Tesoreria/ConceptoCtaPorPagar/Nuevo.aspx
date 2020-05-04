<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" Title=".: Xpinn - Tipo Cta por Pagar :." %>

<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>
<%@ Register Src="../../../General/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../../../General/Controles/ctlPlanCuentas.ascx" TagName="ListadoPlanCtas" TagPrefix="uc1" %>
<%@ Register Src="../../../General/Controles/ctlPlanCuentasNif.ascx" TagName="ListadoPlanNif" TagPrefix="uc2" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:MultiView ID="mvImpuesto" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwDatos" runat="server">
            <table border="0" cellpadding="5" cellspacing="0" width="100%">
                <tr>
                    <td class="tdI" style="text-align: left">
                        Código&nbsp;*&nbsp;
                        <asp:RequiredFieldValidator ID="rfvtipopago" runat="server" ControlToValidate="txtConceptoCta"
                            Style="font-size: x-small" ErrorMessage="Campo Requerido" SetFocusOnError="True"
                            ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic" /><br />
                        <asp:TextBox ID="txtConceptoCta" runat="server" CssClass="textbox" MaxLength="128" />
                    </td>
                    <td class="tdD">
                    </td>
                </tr>
                <tr>
                    <td class="tdI" style="text-align: left">
                        Descripción&nbsp;*&nbsp;
                        <asp:RequiredFieldValidator ID="rfvDescripcion" runat="server" ControlToValidate="txtDescripcion"
                            Style="font-size: x-small" ErrorMessage="Campo Requerido" SetFocusOnError="True"
                            ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic" /><br />
                        <asp:TextBox ID="txtDescripcion" runat="server" CssClass="textbox" MaxLength="128"
                            Width="519px" />
                    </td>
                    <td class="tdD">
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <table>
                            <tr>
                                <td style="width: 130px; text-align: left">
                                    Cod Cuenta<br />
                                    <cc1:TextBoxGrid ID="txtCodCuenta" runat="server" AutoPostBack="True" Style="text-align: left"
                                        CssClass="textbox" Width="120px" OnTextChanged="txtCodCuenta_TextChanged">    
                                    </cc1:TextBoxGrid>
                                    <uc1:ListadoPlanCtas ID="ctlListadoPlan" runat="server" />
                                </td>
                                <td style="width: 25px; text-align: center">
                                    <br />
                                    <cc1:ButtonGrid ID="btnListadoPlan" CssClass="btnListado" runat="server" Text="..."
                                        Width="95%" OnClick="btnListadoPlan2_Click" />
                                </td>
                                <td style="width: 230px; text-align: left">
                                    Nombre de la Cuenta<br />
                                    <cc1:TextBoxGrid ID="txtNomCuenta" runat="server" Style="text-align: left" BackColor="#F4F5FF"
                                        Width="200px" CssClass="textbox" Enabled="False">
                                    </cc1:TextBoxGrid>
                                </td>
                                <td style="width: 190px; text-align: left">
                                    Tipo Mov.<br />
                                    <asp:DropDownList ID="ddlTipoMov" runat="server" Height="26px" Width="170px" CssClass="dropdown"
                                        AppendDataBoundItems="True">
                                        <asp:ListItem Value="1">Débito</asp:ListItem>
                                        <asp:ListItem Value="2">Crédito</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <table>
                            <tr>
                                <td style="width: 130px; text-align: left">
                                    Cod Cuenta NIIF
                                    <br />
                                    <cc1:TextBoxGrid ID="txtCodCuentaNIF" runat="server" AutoPostBack="True" Style="text-align: left"
                                        CssClass="textbox" Width="120px" OnTextChanged="txtCodCuentaNIF_TextChanged">    
                                    </cc1:TextBoxGrid>
                                    <uc2:listadoplannif id="ctlListadoPlanNif" runat="server" />
                                </td>
                                <td style="width: 25px; text-align: center">
                                    <br />
                                    <cc1:ButtonGrid ID="btnListadoPlanNIF" CssClass="btnListado" runat="server" Text="..."
                                        Width="95%" OnClick="btnListadoPlanNIF_Click" />
                                </td>
                                <td style="width: 230px; text-align: left">
                                    Nombre de la Cuenta
                                    <br />
                                    <cc1:TextBoxGrid ID="txtNomCuentaNif" runat="server" Style="text-align: left" CssClass="textbox"
                                        Width="200px" Enabled="False">
                                    </cc1:TextBoxGrid>
                                </td>
                                <td style="width: 190px; text-align: left">
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>

                
                <tr>
                    <td colspan="2">
                        <table>
                            <tr>
                                <td style="width: 130px; text-align: left">
                                    Cod Cuenta Descuento<br />
                                    <cc1:TextBoxGrid ID="txtCodCuenta_desc" runat="server" AutoPostBack="True" Style="text-align: left"
                                        CssClass="textbox" Width="120px" OnTextChanged="txtCodCuenta_desc_TextChanged">    
                                    </cc1:TextBoxGrid>
                                    <uc1:ListadoPlanCtas ID="ctlListadoPlanCtasdesc" runat="server" />
                                </td>
                                <td style="width: 25px; text-align: center; vertical-align: middle">
                                    <br />
                                    <cc1:ButtonGrid ID="btnListadoPlandesc" CssClass="btnListado" runat="server" Text="..."
                                        Width="95%" OnClick="btnListadoPlandesc_Click" />
                                </td>
                                <td style="width: 230px; text-align: left; vertical-align:bottom">
                                    Nombre de la Cuenta<br />
                                    <cc1:TextBoxGrid ID="txtNomCuentadesc" runat="server" Style="text-align: left" BackColor="#F4F5FF"
                                        Width="200px" CssClass="textbox" Enabled="False">
                                    </cc1:TextBoxGrid>
                                </td>
                                <td style="width: 190px; text-align: left">
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>


                <tr>
                    <td colspan="2">
                        <table>
                            <tr>
                                <td style="width: 130px; text-align: left">Cod Cuenta Anticipos<br />
                                    <cc1:TextBoxGrid ID="txtCodCuentaAnticipo" runat="server" AutoPostBack="True" CssClass="textbox" OnTextChanged="txtCodCuentaAnticipo_TextChanged" Style="text-align: left" Width="120px">    
                                    </cc1:TextBoxGrid>
                                    <uc1:ListadoPlanCtas ID="ctlListadoPlanAnticipos" runat="server" />
                                </td>
                                <td style="width: 25px; text-align: center">
                                    <br />
                                    <cc1:ButtonGrid ID="btnListadoPlanAnticipos" runat="server" CssClass="btnListado" OnClick="btnListadoPlanAnticipos_Click" Text="..." Width="91%" Height="18px" />
                                </td>
                                <td style="width: 230px; text-align: left">Nombre de la Cuenta<br />
                                    <cc1:TextBoxGrid ID="txtNomCuentaAnticipos" runat="server" BackColor="#F4F5FF" CssClass="textbox" Enabled="False" Style="text-align: left" Width="200px">
                                    </cc1:TextBoxGrid>
                                </td>
                                <td style="width: 190px; text-align: left"><br />
                                    
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>


                <tr>
                    <td style="text-align: left" colspan="2">
                        <asp:UpdatePanel ID="updImpuestos" runat="server">
                            <ContentTemplate>
                                <asp:Button ID="btnDetalle" runat="server" CssClass="btn8" OnClick="btnDetalle_Click"
                                    OnClientClick="btnDetalle_Click" Text="+ Adicionar Impuesto" />
                                <asp:Panel ID="panelGrilla" runat="server">
                                    <asp:GridView ID="gvImpuestos" runat="server" AutoGenerateColumns="false" HeaderStyle-Height="25px"
                                        BorderStyle="None" BorderWidth="0px" CellPadding="0" DataKeyNames="idconceptoimp"
                                        ForeColor="Black" GridLines="None" OnRowDataBound="gvImpuestos_RowDataBound"
                                        OnRowDeleting="gvImpuestos_RowDeleting" ShowFooter="False" Style="font-size: xx-small">
                                        <Columns>
                                            <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/gr_elim.jpg" ShowDeleteButton="True" />
                                            <asp:TemplateField HeaderText="Activ" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCodigo" runat="server" Text='<%# Bind("idconceptoimp") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Tipo de Impuesto" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTipoImpuesto" runat="server" Text='<%# Bind("cod_tipo_impuesto") %>'
                                                        Visible="false"></asp:Label>
                                                    <cc1:DropDownListGrid ID="ddlTipoImpuesto" runat="server" AppendDataBoundItems="True"
                                                        CommandArgument="<%#Container.DataItemIndex %>" CssClass="textbox" Style="font-size: xx-small;
                                                        text-align: left" Width="250px">
                                                    </cc1:DropDownListGrid>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Base Mínima" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <uc1:decimales ID="txtBaseMinima" runat="server" Text='<%# Bind("base_minima") %>'>
                                                    </uc1:decimales>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Porcentaje Impuesto" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtPorcentajeImpuesto" runat="server" CssClass="textbox" Width="100px"
                                                        Style="text-align: right" Text='<%# Bind("porcentaje_impuesto") %>' />
                                                    <asp:FilteredTextBoxExtender ID="fte1" runat="server" Enabled="True" FilterType="Numbers, Custom"
                                                        TargetControlID="txtPorcentajeImpuesto" ValidChars="," />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Cod. Cuenta" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <cc1:TextBoxGrid ID="txtCodCuenta_imp" runat="server" Width="100px" CssClass="textbox"
                                                        Text='<%# Bind("cod_cuenta_imp") %>' CommandArgument='<%#((GridViewRow) Container).RowIndex %>'>  
                                                    </cc1:TextBoxGrid>
                                                    <cc1:ButtonGrid ID="btnListadoPlan" CssClass="btnListado" runat="server" Text="..."
                                                        OnClick="btnListadoPlan_Click" CommandArgument='<%#Container.DataItemIndex %>' />
                                                    <uc1:ListadoPlanCtas ID="ctlListadoPlan" runat="server" />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <HeaderStyle CssClass="gridHeader" />
                                        <RowStyle CssClass="gridItem" />
                                        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                    </asp:GridView>
                                </asp:Panel>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
        </asp:View>
        <asp:View ID="vwFinal" runat="server">
            <asp:Panel ID="PanelFinal" runat="server">
                <table style="width: 100%;">
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <br />
                            <br />
                            <br />
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;color:Red">
                            Concepto de cuenta por pagar &nbsp;<asp:Label ID="lblMsj" runat="server"></asp:Label>&nbsp;Correctamente
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <br />
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;">
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </asp:View>
    </asp:MultiView>

    <uc4:mensajegrabar ID="ctlMensaje" runat="server" />

</asp:Content>
