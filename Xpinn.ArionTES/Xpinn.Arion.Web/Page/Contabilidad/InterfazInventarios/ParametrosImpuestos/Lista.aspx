<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Nuevo" Title=".: Xpinn - Inventarios Impuestos :." %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/ctlNumero.ascx" TagName="numero" TagPrefix="uc2" %>
<%@ Register Src="~/General/Controles/ctlPlanCuentas.ascx" TagName="ListadoPlanCtas" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/ctlPlanCuentasNif.ascx" TagName="ListadoPlanNif" TagPrefix="uc2" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>
<%@ Register Src="~/General/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc1" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">

    <br />

    <script type="text/javascript" src="../../../Scripts/gridviewScroll.min.js"></script>

    <asp:ScriptManager ID="ScriptManager1" runat="server">
        <Scripts>
            <asp:ScriptReference Path="../../../Scripts/PopUp.js" />
        </Scripts>
    </asp:ScriptManager>

    <asp:Button ID="btnLoading" runat="server" Style="display: none" />

    <asp:MultiView ID="mvPresupuesto" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwEncabezado" runat="server">
            <table id="tbEncabezado" border="0" cellpadding="5" cellspacing="0" style="text-align: left; width: 100%">
                <tr>
                    <td colspan="3" style="text-align: left">
                        <strong>Inventarios - Cuentas Contables Impuestos</strong>
                        <hr style="width: 100%" /> 
                    </td>
                </tr>                
                <tr>
                    <td colspan="3" style="text-align: left">
                        <br />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left" colspan="3">
                        <strong>Conceptos:</strong>
                        <asp:GridView ID="gvImpuestos" runat="server"  AutoGenerateColumns="False" HeaderStyle-Height="25px"
                            PageSize="20" Style="font-size: x-small" DataKeyNames="id_concepto" OnRowDataBound="gvImpuestos_RowDataBound">
                            <Columns>
                                <asp:TemplateField HeaderText="Activ" ItemStyle-HorizontalAlign="Center" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCodigo" runat="server" Text='<%# Bind("id_concepto") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Tipo de Impuesto" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblNombre" runat="server" Text='<%# Bind("nombre") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Base Mínima" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblBaseMinima" runat="server" Text='<%# Bind("base_minima") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Porcentaje" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPorcentaje" runat="server" Text='<%# Bind("porcentaje_calculo") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cod. Cuenta" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <cc1:TextBoxGrid ID="txtCodCuentaCon" runat="server" AutoPostBack="True" BackColor="#F4F5FF" Width="100px"  CssClass="textbox"
                                            Text='<%# Bind("cod_cuenta") %>' OnTextChanged="txtCodCuentaCon_TextChanged" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'>  
                                        </cc1:TextBoxGrid>
                                        <cc1:TextBoxGrid ID="ddlNomCuentaCon" runat="server" AutoPostBack="true" BackColor="#F4F5FF" Width="180px" CssClass="textbox"
                                            Text='<%# Bind("nombre_cuenta") %>' CommandArgument='<%#Container.DataItemIndex %>' Enabled="False" />
                                        <cc1:ButtonGrid ID="btnListadoPlanCon" CssClass="btnListado" runat="server" Text="..."
                                            OnClick="btnListadoPlanCon_Click" CommandArgument='<%#Container.DataItemIndex %>' />
                                        <uc1:ListadoPlanCtas ID="ctlListadoPlanCon" runat="server" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                            </Columns>
                            <HeaderStyle CssClass="gridHeader" />
                            <PagerStyle CssClass="gridPager" />
                            <RowStyle CssClass="gridItem" />
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td colspan="3" style="text-align: left">
                        <br />
                    </td>
                </tr>
                <tr>
                    <td colspan="3" style="text-align: left">
                        <strong>Impuestos:</strong>
                        <asp:GridView ID="gvLista" runat="server"  AutoGenerateColumns="False" Width="75%"  HeaderStyle-Height="25px"
                            PageSize="20" Style="font-size: x-small" DataKeyNames="idparametro" OnRowDataBound="gvLista_RowDataBound">
                            <Columns>
                                <asp:TemplateField HeaderText="Id." HeaderStyle-HorizontalAlign="Center" Visible="true">
                                    <ItemTemplate>
                                        <asp:Label ID="lblparametro" runat="server" Text='<%# Bind("idparametro") %>' Visible="true"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="5%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Impuesto" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblIdimpuesto" runat="server" Text='<%# Bind("id_impuesto") %>' Visible="true"></asp:Label>&nbsp;&nbsp;
                                        <asp:Label ID="lblImpuesto" runat="server" Text='<%# Bind("descripcion") %>' Visible="true"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" Width="20%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cod.Cuenta" HeaderStyle-HorizontalAlign="Center" Visible="true">
                                    <ItemTemplate>
                                        <cc1:TextBoxGrid ID="txtCodCuenta" runat="server" AutoPostBack="True" BackColor="#F4F5FF" Width="80px" CssClass="textbox"
                                            Text='<%# Bind("cod_cuenta") %>' OnTextChanged="txtCodCuenta_TextChanged" CommandArgument='<%#((GridViewRow) Container).RowIndex %>' />
                                        <cc1:TextBoxGrid ID="ddlNomCuenta" runat="server" AutoPostBack="true" BackColor="#F4F5FF" Width="180px" CssClass="textbox"
                                            Text='<%# Bind("nombre_cuenta") %>' CommandArgument='<%#Container.DataItemIndex %>' Enabled="False" />
                                        <cc1:ButtonGrid ID="btnListadoPlan" CssClass="btnListado" runat="server"
                                            Text="..." OnClick="btnListadoPlan_Click" CommandArgument='<%#((GridViewRow) Container).RowIndex %>' />
                                        <uc1:ListadoPlanCtas ID="ctlListadoPlan" runat="server" OneventotxtCuenta_TextChanged="txtCodCuenta_TextChanged" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" Width="35%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cod.Cuenta Niif" HeaderStyle-HorizontalAlign="Center" Visible="true">
                                    <ItemTemplate>
                                        <cc1:TextBoxGrid ID="txtCodCuenta_niif" runat="server" AutoPostBack="True" BackColor="#F4F5FF" Width="80px" CssClass="textbox"
                                            Text='<%# Bind("cod_cuenta_niif") %>' OnTextChanged="txtCodCuenta_niif_TextChanged" CommandArgument='<%#((GridViewRow) Container).RowIndex %>' />
                                        <cc1:TextBoxGrid ID="ddlNomCuenta_niif" runat="server" AutoPostBack="true" BackColor="#F4F5FF" Width="180px" CssClass="textbox"
                                            Text='<%# Bind("nombre_cuenta_niif") %>' CommandArgument='<%#Container.DataItemIndex %>' Enabled="False" />
                                        <cc1:ButtonGrid ID="btnListadoPlan_niif" CssClass="btnListado" runat="server"
                                            Text="..." OnClick="btnListadoPlan_niif_Click" CommandArgument='<%#((GridViewRow) Container).RowIndex %>' />
                                        <uc1:ListadoPlanCtas ID="ctlListadoPlan_niif" runat="server" OneventotxtCuenta_TextChanged="txtCodCuenta_niif_TextChanged" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" Width="35%" />
                                </asp:TemplateField>
                            </Columns>
                            <HeaderStyle CssClass="gridHeader" />
                            <PagerStyle CssClass="gridPager" />
                            <RowStyle CssClass="gridItem" />
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </asp:View>

    </asp:MultiView>
    <uc4:mensajegrabar ID="ctlMensaje" runat="server" />

</asp:Content>
