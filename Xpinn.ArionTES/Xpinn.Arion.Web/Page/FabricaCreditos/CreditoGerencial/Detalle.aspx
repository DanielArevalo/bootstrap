<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Detalle.aspx.cs" Inherits="Detalle" %>

<%@ Register Src="~/General/Controles/decimales.ascx" TagName="Decimal" TagPrefix="ucDecimal" %>
<%@ Register Src="~/General/Controles/fechaeditable.ascx" TagName="Fecha" TagPrefix="ucFecha" %>
<%@ Register Src="~/General/Controles/ctlValidarBiometria.ascx" TagName="validarBiometria" TagPrefix="uc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:MultiView ID="mvReestructuras" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwDatos" runat="server">
            <table cellpadding="0" cellspacing="0" style="width: 80%">
                <tr>
                    <td style="text-align: left">
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Panel ID="pConsulta" runat="server">
                            <div style="border-style: none; border-width: medium;">
                                <table style="width: 100%">
                                    <tbody style="text-align: left">
                                        <tr>
                                            <td style="width: 176px; margin-left: 40px;">
                                                &nbsp;
                                                <asp:TextBox ID="txtCodigo" runat="server" Enabled="False" Visible="False"></asp:TextBox>
                                                <br />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="margin-left: 40px;" colspan="3">
                                                <strong>Datos del Deudor:</strong>
                                            </td>
                                            <td style="margin-left: 40px;">
                                                &nbsp;
                                            </td>
                                            <td style="margin-left: 40px;">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 176px; margin-left: 40px; text-align: left">
                                                Identificación<br />
                                                <asp:TextBox ID="txtNumDoc" runat="server" CssClass="textbox" Enabled="false" Style="text-align: left"></asp:TextBox>
                                            </td>
                                            <td style="text-align: left">
                                                Apellidos
                                                <br />
                                                <asp:TextBox ID="txtPrimerApellido" runat="server" CssClass="textbox" Enabled="false"
                                                    Width="231px"></asp:TextBox>
                                                <br />
                                            </td>
                                            <td style="text-align: left">
                                                Nombres<br />
                                                <asp:TextBox ID="txtPrimerNombre" runat="server" CssClass="textbox" Enabled="false"
                                                    Width="284px"></asp:TextBox>
                                                <br />
                                            </td>
                                            <td style="text-align: left">
                                                &nbsp;
                                            </td>
                                            <td style="text-align: left">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                Dirección<br />
                                                <asp:TextBox ID="txtDireccion" runat="server" CssClass="textbox" Enabled="false"
                                                    Width="408px"></asp:TextBox>
                                            </td>
                                            <td style="text-align: left">
                                                Teléfono<br />
                                                <asp:TextBox ID="txtTelefono" runat="server" CssClass="textbox" Enabled="false" Width="191px"></asp:TextBox>
                                            </td>
                                            <td style="text-align: left">
                                                &nbsp;
                                            </td>
                                            <td style="text-align: left">
                                                &nbsp;
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                </tr>
            </table>
            <table cellpadding="0" cellspacing="0" style="width: 100%">
                <tr>
                    <td style="text-align: left">
                        <asp:Panel ID="pTotales" runat="server" Height="135px">
                            <strong>Datos de Créditos :</strong> <span style="font-size: x-small; text-align: left">
                                (Seleccione el(los) crédito(s) a re-estructurar)</span><br />
                            <table cellpadding="5" cellspacing="0" style="width: 100%">
                                <tr>
                                    <td class="tdI" align="center" colspan="3" style="text-align: left">
                                        <asp:GridView ID="gvLista" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                            OnPageIndexChanging="gvLista_PageIndexChanging" Width="94%" DataKeyNames="numero_credito"
                                            HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager" RowStyle-CssClass="gridItem"
                                            Style="font-size: x-small">
                                            <Columns>
                                                <asp:BoundField DataField="numero_credito" HeaderStyle-CssClass="gridColNo" ItemStyle-CssClass="gridColNo">
                                                    <HeaderStyle CssClass="gridColNo" />
                                                    <ItemStyle CssClass="gridColNo" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="numero_credito" HeaderText="Número de crédito" />
                                                <asp:BoundField DataField="linea_credito" HeaderText="Línea" />
                                                <asp:BoundField DataField="monto" HeaderText="Monto" DataFormatString="{0:c0}">
                                                    <FooterStyle HorizontalAlign="Center" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="saldo_capital" HeaderText="Saldo Capital" DataFormatString="{0:c0}">
                                                    <FooterStyle HorizontalAlign="Center" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="interes_corriente" HeaderText="Interes Corriente" DataFormatString="{0:c0}">
                                                    <FooterStyle HorizontalAlign="Center" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="interes_mora" HeaderText="Interes Mora" DataFormatString="{0:c0}">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="otros" HeaderText="Otros" DataFormatString="{0:c0}">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="valor_total" HeaderText="Total" DataFormatString="{0:c0}">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="Seleccionar">
                                                    <ItemTemplate>
                                                        <asp:UpdatePanel ID="UpdatePanelchkRecoger" runat="server" UpdateMode="Conditional"
                                                            RenderMode="Inline">
                                                            <ContentTemplate>
                                                                <asp:CheckBox ID="chkRecoger" runat="server" Checked='<%# Eval("Recoger") %>' AutoPostBack="True"
                                                                    OnCheckedChanged="chkRecoger_CheckedChanged" /></ContentTemplate>
                                                        </asp:UpdatePanel>
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
                            </table>
                            <br />

                            <table style="width: 95%">
                                <tr>
                                    <td style="text-align: left" colspan="3">
                                        <strong>Datos del Crédito</strong>
                                    </td>
                                    <td style="width: 191px">
                                        &nbsp;
                                    </td>
                                    <td style="width: 88px">
                                        &nbsp;
                                    </td>
                                    <td style="width: 165px">
                                        &nbsp;
                                    </td>
                                    <td style="width: 165px">
                                        &nbsp;
                                    </td>
                                    <td style="width: 177px">
                                        &nbsp;
                                    </td>
                                    <td style="width: 165px">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="text-align: left">
                                        Fecha Crédito
                                    </td>
                                    <td style="text-align: left">
                                        Valor Crédito
                                    </td>
                                    <td style="text-align: left; width: 191px;">
                                        &nbsp;
                                    </td>
                                    <td style="width: 88px; text-align: left">
                                        &nbsp;
                                    </td>
                                    <td style="width: 165px; text-align: left">
                                        &nbsp;
                                    </td>
                                    <td style="width: 165px; text-align: left">
                                        &nbsp;
                                    </td>
                                    <td style="width: 177px; text-align: left">
                                        &nbsp;
                                    </td>
                                    <td style="width: 165px">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="text-align: left">
                                        <ucFecha:Fecha ID="txtFechaRes" runat="server"></ucFecha:Fecha>
                                    </td>
                                    <td style="width: 165px; text-align: left">
                                        <ucDecimal:Decimal ID="txtvalortotal" runat="server" Enabled="False"></ucDecimal:Decimal>
                                    </td>
                                    <td style="text-align: center; width: 191px;">
                                        &nbsp;
                                    </td>
                                    <td style="text-align: left; width: 88px;">
                                        &nbsp;
                                    </td>
                                    <td style="text-align: left">
                                        &nbsp;
                                    </td>
                                    <td style="text-align: left">
                                        &nbsp;
                                    </td>
                                    <td style="width: 177px">
                                        &nbsp;
                                    </td>
                                    <td style="width: 165px">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: left" colspan="3">
                                        Linea de Crédito<br />
                                        <asp:UpdatePanel ID="updLinea" runat="server">
                                            <ContentTemplate>
                                                <asp:DropDownList ID="ddllineas" runat="server" Width="318px" AutoPostBack="true"
                                                    CssClass="dropdown" OnSelectedIndexChanged="ddllineas_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblAsesor" runat="server" Text="Asesor "></asp:Label>
                                        <br />
                                        <asp:DropDownList ID="Ddlusuarios" runat="server" Width="225px" CssClass="dropdown"
                                            ValidationGroup="vgGuardar" Height="23px">
                                        </asp:DropDownList>
                                    </td>
                                    <td colspan="4" style="text-align: left">
                                        Fecha Primer Pago<br />
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                            <ContentTemplate>
                                                <ucFecha:Fecha ID="txtfechaproxpago" runat="server"></ucFecha:Fecha>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="ddlFormaPago" EventName="SelectedIndexChanged" />
                                                <asp:AsyncPostBackTrigger ControlID="ddlEmpresa" EventName="SelectedIndexChanged" />                                                
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </td>
                                    <td style="width: 165px">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: left; height: 47px;">
                                        Plazo&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <br />
                                        <asp:TextBox ID="txtplazo" runat="server" Width="50px" MaxLength="4" CssClass="textbox"></asp:TextBox>
                                        <asp:FilteredTextBoxExtender ID="txtplazoExtender" runat="server" Enabled="True"
                                            FilterType="Numbers, Custom" TargetControlID="txtplazo" ValidChars="." />
                                    </td>
                                    <td style="text-align: left; height: 47px;" colspan="2">
                                        &nbsp;Periodicidad&nbsp;
                                        <br />
                                        <asp:DropDownList ID="ddlperiodicidad" runat="server" Width="225px" CssClass="dropdown">
                                        </asp:DropDownList>
                                    </td>
                                    <td style="width: 191px; height: 47px;">
                                        <asp:UpdatePanel ID="updFormaPago" runat="server">
                                            <ContentTemplate>
                                                Forma de Pago<br />
                                                <asp:DropDownList ID="ddlFormaPago" runat="server" Width="225px" CssClass="dropdown"
                                                    AutoPostBack="True" OnSelectedIndexChanged="ddlFormaPago_SelectedIndexChanged">
                                                    <asp:ListItem Value="C">Caja</asp:ListItem>
                                                    <asp:ListItem Value="N">Nomina</asp:ListItem>
                                                </asp:DropDownList>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                    <td style="text-align: left; height: 47px;" colspan="4">
                                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                            <ContentTemplate>
                                                <asp:Label ID="lblEmpresa" runat="server" Text="Empresa"></asp:Label><br />
                                                <asp:DropDownList ID="ddlEmpresa" runat="server" CssClass="dropdown" Width="222px"
                                                    AutoPostBack="True" OnSelectedIndexChanged="ddlEmpresa_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="ddlFormaPago" EventName="SelectedIndexChanged" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </td>
                                    <td style="width: 165px; height: 47px;">
                                    </td>
                                </tr>
                            </table>
                            <asp:TabContainer runat="server" ID="Tabs" ActiveTabIndex="0" Width="860">
                                <asp:TabPanel runat="server" ID="TabPanelAtributos" HeaderText="Atributos">
                                    <HeaderTemplate>
                                        Atributos</HeaderTemplate>
                                    <ContentTemplate>
                                        <table style="width: 80%">
                                            <tr>
                                                <td style="text-align: left">
                                                    Atributo
                                                    <asp:UpdatePanel ID="upFormaDesembolso" runat="server">
                                                        <ContentTemplate>
                                                            <asp:DropDownList ID="ddlAtributo" runat="server" Width="224px" AutoPostBack="true"
                                                                OnSelectedIndexChanged="ddlAtributo_SelectedIndexChanged" />
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="ddllineas" EventName="SelectedIndexChanged" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                </td>
                                                <td style="width: 223px">
                                                    <br />
                                                    <asp:CheckBox ID="chkCobraMora" runat="server" Style="font-size: small" Text="Cobrar Mora" />
                                                </td>
                                                <td style="width: 165px">
                                                    &#160;
                                                </td>
                                                <td style="width: 165px">
                                                    &#160;
                                                </td>
                                                <td style="width: 165px">
                                                    &#160;
                                                </td>
                                                <td style="width: 165px">
                                                    &#160;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: left" colspan="3">
                                                    <asp:UpdatePanel ID="updTipoTasa" runat="server">
                                                        <ContentTemplate>
                                                            <asp:RadioButtonList ID="rbCalculoTasa" runat="server" RepeatDirection="Horizontal"
                                                                Style="font-size: small" AutoPostBack="True" OnSelectedIndexChanged="rbCalculoTasa_SelectedIndexChanged">
                                                                <asp:ListItem Value="1">Tasa Fija</asp:ListItem>
                                                                <asp:ListItem Value="3">Histórico Fijo</asp:ListItem>
                                                                <asp:ListItem Value="5">Histórico Variable</asp:ListItem>
                                                            </asp:RadioButtonList>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </td>
                                                <td style="width: 165px">
                                                </td>
                                                <td style="width: 165px">
                                                    &#160;&#160;
                                                </td>
                                                <td style="width: 165px">
                                                    &#160;&#160;
                                                </td>
                                            </tr>
                                        </table>
                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                            <ContentTemplate>
                                                <asp:MultiView ID="mvAtributo" runat="server" ActiveViewIndex="0">
                                                    <asp:View ID="vwHistorico" runat="server">
                                                        <table style="width: 80%">
                                                            <tr>
                                                                <td style="text-align: left">
                                                                    Tipo Histórico<br />
                                                                    <asp:DropDownList ID="ddlHistorico" runat="server" Width="224px" AppendDataBoundItems="true">
                                                                        <asp:ListItem Value="" Text="" />
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td colspan="2">
                                                                    Spread<br />
                                                                    <ucDecimal:Decimal ID="txtDesviacion" runat="server" />
                                                                </td>
                                                                <td style="width: 165px">
                                                                    &#160;&#160;
                                                                </td>
                                                                <td style="width: 165px">
                                                                    &#160;&#160;
                                                                </td>
                                                                <td style="width: 165px">
                                                                    &#160;&#160;
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </asp:View>
                                                    <asp:View ID="vwFija" runat="server">
                                                        <table style="width: 80%">
                                                            <tr>
                                                                <td style="text-align: left">
                                                                    Tasa<br />
                                                                    <asp:TextBox ID="txtTasa" runat="server" CssClass="textbox" Width="130px" MaxLength="8" /><asp:FilteredTextBoxExtender
                                                                        ID="fte1" runat="server" Enabled="True" FilterType="Custom, Numbers" TargetControlID="txtTasa"
                                                                        ValidChars=",">
                                                                    </asp:FilteredTextBoxExtender>
                                                                </td>
                                                                <td colspan="2">
                                                                    Tipo de Tasa<br />
                                                                    <asp:DropDownList ID="ddlTipoTasa" runat="server" Width="224px" />
                                                                </td>
                                                                <td style="width: 165px">
                                                                    &#160;&#160;
                                                                </td>
                                                                <td style="width: 165px">
                                                                    &#160;&#160;
                                                                </td>
                                                                <td style="width: 165px">
                                                                    &#160;&#160;
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </asp:View>
                                                </asp:MultiView></ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="rbCalculoTasa" EventName="SelectedIndexChanged" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </ContentTemplate>
                                </asp:TabPanel>
                                <asp:TabPanel runat="server" ID="TabPanelCodeudores" HeaderText="Codeudores">
                                    <HeaderTemplate>
                                        Codeudores</HeaderTemplate>
                                    <ContentTemplate>
                                        <asp:UpdatePanel ID="UpdatePanelCodeudores" runat="server">
                                            <ContentTemplate>
                                                <asp:GridView ID="gvCodeudores" runat="server" Width="100%" ShowHeaderWhenEmpty="True"
                                                    EmptyDataText="No se encontraron registros." AutoGenerateColumns="False" PageSize="5"
                                                    BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px"
                                                    CellPadding="4" ForeColor="Black" GridLines="Vertical" Height="16px" ShowFooter="True"
                                                    OnRowDataBound="gvCodeudores_RowDataBound" OnRowCommand="gvCodeudores_RowCommand"
                                                    OnRowDeleting="gvCodeudores_RowDeleting" Style="font-size: x-small">
                                                    <AlternatingRowStyle BackColor="White" />
                                                    <Columns>
                                                        <asp:TemplateField ShowHeader="False">
                                                            <FooterTemplate>
                                                                <asp:ImageButton ID="btnNuevo0" runat="server" CausesValidation="False" CommandName="AddNew"
                                                                    ImageUrl="~/Images/gr_nuevo.jpg" ToolTip="Crear Nuevo" Width="16px" /></FooterTemplate>
                                                            <ItemStyle Width="20px" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="btnEliminar" runat="server" CommandName="Delete" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                                                    ImageUrl="~/Images/gr_elim.jpg" ToolTip="Eliminar" Width="16px" /></ItemTemplate>
                                                            <HeaderStyle CssClass="gridIco" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Código">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblcodpersona" runat="server" Text='<%# Bind("codpersona") %>'></asp:Label></ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:Label ID="txtcodpersona" runat="server" Text='<%# Bind("codpersona") %>'></asp:Label></FooterTemplate>
                                                            <ItemStyle Width="30px" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Identificación">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblidentificacion" runat="server" Text='<%# Bind("identificacion") %>'></asp:Label></ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:TextBox ID="txtidentificacion" runat="server" Text='<%# Bind("identificacion") %>'
                                                                    OnTextChanged="txtidentificacion_TextChanged" Style="font-size: x-small" AutoPostBack="True"></asp:TextBox></FooterTemplate>
                                                            <ItemStyle Width="50px" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Nombres y Apellidos">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblnombres" runat="server" Text='<%# Bind("nombres") %>'></asp:Label></ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:Label ID="txtnombres" runat="server" Text='<%# Bind("nombres") %>'></asp:Label></FooterTemplate>
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
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </ContentTemplate>
                                </asp:TabPanel>
                            </asp:TabContainer>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
        </asp:View>
        <asp:View ID="mvFinal" runat="server">
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
                            <asp:Label ID="lblMensaje" runat="server" Text="Solicitud Realizada Correctamente"
                                Style="color: #FF3300"></asp:Label>
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
                            <asp:Button ID="btnFinal" runat="server" Text="Finalizar" OnClick="btnFinalClick" />&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnPlanPagos" runat="server" Text="Ir a Plan de Pagos" OnClick="btnPlanPagosClick" />&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnAprobacion" runat="server" Text="ir a Aprobación" OnClick="btnAprobacionClick" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </asp:View>
    </asp:MultiView>
    <asp:HiddenField ID="HiddenField1" runat="server" />
    <asp:ModalPopupExtender ID="mpeNuevo" runat="server" PopupControlID="panelActividadReg"
        TargetControlID="HiddenField1" BackgroundCssClass="backgroundColor">
    </asp:ModalPopupExtender>
    <asp:Panel ID="panelActividadReg" runat="server" BackColor="White" Style="text-align: right"
        BorderWidth="1px" Width="500px">
        <div id="popupcontainer" style="width: 500px">
            <table style="width: 100%;">
                <tr>
                    <td colspan="3">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="3" style="text-align: center">
                        Esta Seguro de Realizar la solicitud del crédito gerencial ?
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                    <td style="text-align: center">
                        <asp:Button ID="btnContinuar" runat="server" Text="Continuar" CssClass="btn8" Width="182px"
                            OnClick="btnContinuar_Click" />
                        &nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnParar" runat="server" Text="Cancelar" CssClass="btn8" Width="182px"
                            OnClick="btnParar_Click" />
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
            </table>
        </div>
    </asp:Panel>

    <uc2:validarBiometria ID="ctlValidarBiometria" runat="server" />
     <script type='text/javascript'>
         function Forzar() {
             __doPostBack('', '');
         }
    </script>
</asp:Content>
