<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar"
    TagPrefix="uc4" %>
<%@ Register Src="~/General/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:MultiView ID="mvAplicar" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwLista" runat="server">
            <table style="width: 100%">
                <tr>
                    <td colspan="5" style="text-align:left">
                        <strong>Datos del Giro</strong>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left;width:17%">
                        Código:<br />
                        <asp:TextBox ID="txtCodigoG" runat="server" CssClass="textbox" Enabled="false"></asp:TextBox>
                    </td>
                    <td style="text-align: left;width:17%">
                        Fecha Registro:<br />
                        <asp:TextBox ID="txtFecha" runat="server" CssClass="textbox" Enabled="false"></asp:TextBox>
                    </td>
                    <td style="text-align: left;width:17%">
                        Tipo Acto Giro:<br />
                        <asp:TextBox ID="txtTipoActoGiro" runat="server" CssClass="textbox" Enabled="false"></asp:TextBox>
                    </td>
                    <td style="text-align: left;width:17%">
                        Valor:<br />
                        <uc1:decimales ID="txtValor" runat="server" CssClass="textbox" Width="158px" Enabled="false" />
                    </td>
                    <td style="text-align: left;width:32%">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left;width:17%">
                        Cod. Persona:<br />
                        <asp:TextBox ID="txtCodPersona" runat="server" ClientIDMode="Static" CssClass="textbox"
                            Enabled="false" Width="140px" />
                    </td>
                    <td style="text-align: left;width:17%">
                        Identificación:<br />
                        <asp:TextBox ID="txtIdentific" runat="server" ClientIDMode="Static" CssClass="textbox"
                            Enabled="false" Width="140px" />
                    </td>
                    <td style="text-align: left;" colspan="2">
                        Nombre:<br />
                        <asp:TextBox ID="txtNombre" runat="server" ClientIDMode="Static" CssClass="textbox"
                            Enabled="false" Width="90%" />
                    </td>
                    <td style="text-align: left;width:32%">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left;width:17%">
                        Cod. Operación:<br />
                        <asp:TextBox ID="txtCodOperacion" runat="server" ClientIDMode="Static" CssClass="textbox"
                            Enabled="false" Width="140px" />
                    </td>
                    <td style="text-align: left;width:17%">
                        Numero Radicación:<br />
                        <asp:TextBox ID="txtRadicacion" runat="server" ClientIDMode="Static" CssClass="textbox"
                            Enabled="false" Width="140px" />
                    </td>
                    <td style="text-align: left;width:17%">
                        Nro Comprobante:<br />
                        <asp:TextBox ID="txtNumComprobante" runat="server" ClientIDMode="Static" CssClass="textbox"
                            Enabled="false" Width="140px" />
                    </td>
                    <td style="text-align: left;width:17%">
                        Tipo Comprobante:<br />
                        <asp:TextBox ID="txtTipoComprobante" runat="server" ClientIDMode="Static" CssClass="textbox"
                            Enabled="false" Width="140px" />
                    </td>
                    <td style="text-align: left;width:32%">
                        &nbsp;
                    </td>
                </tr>
            </table>
            <hr style="width:100%"/>
            <asp:UpdatePanel runat="server" UpdateMode="Conditional" RenderMode="Inline">
                <ContentTemplate>
                    <table style="width: 100%">
                        <tr>
                            <td style="text-align: left">
                                <asp:Button ID="btnAdicionarFila" runat="server" Text="Adicionar Detalle" CssClass="btn8"
                                    OnClick="btnAdicionarFila_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100%">
                                <div style="overflow: scroll; max-width: 100%; max-height: 400px;">
                                    <asp:GridView ID="gvGiros" runat="server" AutoGenerateColumns="False" HeaderStyle-Height="28px"
                                        BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" ForeColor="Black" ShowFooter="true"
                                        GridLines="Horizontal" Style="font-size: x-small" OnRowDeleting="gvGiros_RowDeleting"
                                        OnRowDataBound="gvGiros_RowDataBound" DataKeyNames="idgiro">
                                        <Columns>
                                            <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/gr_elim.jpg" ShowDeleteButton="True" />
                                            <asp:TemplateField HeaderText="Codigo" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCodigo" runat="server" CssClass="textbox" Text='<%# Bind("idgiro") %>' /></ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Identificacion">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblcod_persona" runat="server" CssClass="textbox" Text='<%# Bind("cod_persona") %>' Visible="false" />
                                                <cc1:TextBoxGrid ID="txtIdentificacion" runat="server" CssClass="textbox" CommandArgument='<%#Container.DataItemIndex %>'
                                                    AutoPostBack="true" Text='<%# Bind("identificacion") %>' Width="100px" OnTextChanged="txtIdentificacion_TextChanged">
                                                </cc1:TextBoxGrid>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="nombre" HeaderText="Nombre">
                                                <ItemStyle HorizontalAlign="Left" Width="180px" />                                                
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="Valor">
                                                <ItemTemplate>
                                                    <uc1:decimales ID="txtValortem" runat="server" CssClass="textbox" Text='<%# Bind("valor") %>'
                                                        Width="100px" />
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtValortotal" runat="server" CssClass="textbox" Enabled="false" Width="100px" 
                                                        Style="font-size: xx-small; text-align: right"></asp:TextBox>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Forma Pago">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblFormaPagotemfie" runat="server" Text='<%# Bind("forma_pago") %>'
                                                        Visible="false"></asp:Label><cc1:DropDownListGrid CssClass="textbox" runat="server"
                                                            ID="ddlFormaPagotemfie" AutoPostBack="true" Width="130px" OnSelectedIndexChanged="ddlFormaPagotemfie_SelectedIndexChanged"
                                                            CommandArgument='<%#Container.DataItemIndex %>'>
                                                        </cc1:DropDownListGrid>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Banco Giro">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblBancoGiroTem" runat="server" Text='<%# Bind("cod_banco") %>' Visible="false" /><cc1:DropDownListGrid
                                                        CssClass="textbox" runat="server" ID="ddlBancoGiroTem" AutoPostBack="true" Width="180px"
                                                        CommandArgument="<%#Container.DataItemIndex %>" OnSelectedIndexChanged="ddlBancoGiroTem_SelectedIndexChanged">
                                                    </cc1:DropDownListGrid>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Cuenta Giro">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCuentaGiro" runat="server" Text='<%# Bind("num_referencia") %>' Visible="false"></asp:Label><cc1:DropDownListGrid
                                                        CssClass="textbox" runat="server" ID="ddlCuentaGiroTemp" AutoPostBack="true"
                                                        Width="140px" CommandArgument="<%#Container.DataItemIndex %>">
                                                    </cc1:DropDownListGrid>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Banco Destino">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblBancoDestino" runat="server" Text='<%# Bind("cod_banco1") %>' Visible="false"></asp:Label><cc1:DropDownListGrid
                                                        CssClass="textbox" runat="server" ID="ddlBancoDestinoTem" AutoPostBack="true"
                                                        Width="180px" CommandArgument="<%#Container.DataItemIndex %>">
                                                    </cc1:DropDownListGrid>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Cuenta Bancaria Destino">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtCuentaDestino" runat="server" CssClass="textbox" Text='<%# Bind("num_referencia1") %>'
                                                        Width="110px" Style="text-align: right" /></ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="TipoCueta">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTipoCuenta" runat="server" Text='<%# Bind("tipo_cuenta") %>' Visible="false" /><cc1:DropDownListGrid
                                                        ID="ddlTipoCuenta" CssClass="textbox" runat="server" CommandArgument="<%#Container.DataItemIndex %>"
                                                        Width="100px" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <FooterStyle CssClass="gridHeader" />
                                        <HeaderStyle CssClass="gridHeader" />
                                        <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                                        <RowStyle CssClass="gridItem" />
                                    </asp:GridView>
                                </div>
                                <asp:Label ID="lblTotalRegs" runat="server" Visible="False" />
                                <asp:Label ID="lblInfo" runat="server" Text="Su consulta no obtuvo ningún resultado."
                                    Visible="False" />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
                <Triggers><asp:AsyncPostBackTrigger ControlID="btnAdicionarFila" EventName="Click" /><asp:AsyncPostBackTrigger ControlID="gvGiros" EventName="RowDeleting" /></Triggers>
            </asp:UpdatePanel>
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
                        <td style="text-align: center; font-size: large;">
                            Distribución del giro&nbsp;<asp:Label ID="lblCodGiro" runat="server"/> &nbsp;generada correctamente
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
    <asp:HiddenField ID="HiddenField1" runat="server" />
    <uc4:mensajegrabar ID="ctlMensaje" runat="server" />
</asp:Content>
