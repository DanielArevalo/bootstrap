<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" Title=".: Xpinn - Credito :." %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc2" %>
<%@ Register Src="~/General/Controles/decimales.ascx" TagName="Decimal" TagPrefix="ucDecimal" %>
<%@ Register Src="~/General/Controles/PlanPagos.ascx" TagName="planpagos" TagPrefix="uc3" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>
<%@ Register Src="~/General/Controles/ctlCuotasExtras.ascx" TagName="ctrCuotasExtras" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/ctlCodeudores.ascx" TagName="ctlCodeudores" TagPrefix="uc1" %>



<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <script src="../../../Scripts/PCLBryan.js" type="text/javascript"></script>
    <script type="text/javascript">
        function PanelClick(sender, e) {
        }

        function isNumber(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;
        }

        function ActiveTabChanged(sender, e) {
        }

        var HighlightAnimations = {};

        function Highlight(el) {
            if (HighlightAnimations[el.uniqueID] == null) {
                HighlightAnimations[el.uniqueID] = Sys.Extended.UI.Animation.createAnimation({
                    AnimationName: "color",
                    duration: 0.5,
                    property: "style",
                    propertyKey: "backgroundColor",
                    startValue: "#FFFF90",
                    endValue: "#FFFFFF"
                }, el);
            }
            HighlightAnimations[el.uniqueID].stop();
            HighlightAnimations[el.uniqueID].play();
        }

        function mpeSeleccionOnOk() {
        }

        function mpeSeleccionOnCancel() {
        }
    </script>

    <asp:MultiView ID="mvCredito" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwDatos" runat="server">
            <asp:Panel ID="panelEncabezado" runat="server">
                <table border="0" cellpadding="5" cellspacing="0" width="100%">
                    <tr>
                        <td class="tdI">
                            <asp:Panel ID="Panel2" runat="server">
                                <table style="width: 100%;">
                                    <tr>
                                        <td class="logo" colspan="3" style="text-align: left">
                                            <strong>DATOS DEL DEUDOR</strong>
                                        </td>
                                        <td class="logo" style="text-align: left">&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="logo" style="width: 150px; text-align: left">
                                            <asp:Label runat="server" Text="Identificación" ID="lblIdentificacion"></asp:Label>
                                        </td>
                                        <td style="text-align: left">
                                            <asp:Label runat="server" ID="lblTipo_Identificacion" Text="Tipo Identificación"></asp:Label>
                                        </td>
                                        <td style="text-align: left">
                                            <asp:Label runat="server" ID="lblNombre" Text="Nombre"></asp:Label>
                                        </td>
                                        <td style="text-align: left">&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="logo" style="width: 150px; text-align: left">
                                            <asp:TextBox ID="txtIdentificacion" runat="server" CssClass="textbox"
                                                 />
                                        </td>
                                        <td style="text-align: left">
                                            <asp:TextBox ID="txtTipo_identificacion" runat="server" CssClass="textbox"  />
                                        </td>
                                        <td style="text-align: left">
                                            <asp:TextBox ID="txtNombre" runat="server" CssClass="textbox"  Width="377px" />
                                        </td>
                                        <td style="text-align: left">&nbsp;</td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>

                <table border="0" cellpadding="5" cellspacing="0" width="100%">
                    <tr>
                        <td colspan="4" style="text-align: left">
                            <strong>DATOS DEL CRÉDITO</strong>
                        </td>
                        <td style="text-align: left">&nbsp;</td>
                        <td style="text-align: left">&nbsp;</td>
                        <td style="text-align: left">&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="text-align: left; width: 151px;">
                            <asp:Label runat="server" ID="lblNumero_Radicacion" Text="Número Radicación"></asp:Label>
                            <br />
                            <asp:TextBox ID="txtNumero_radicacion" runat="server" CssClass="textbox"  />
                        </td>
                        <td colspan="3" style="text-align: left">
                            <asp:Label runat="server" ID="lblLinea_credito" Text="Línea de crédito"></asp:Label>
                            <br />
                            <asp:TextBox ID="txtLinea_credito" runat="server" CssClass="textbox"
                                 Width="347px" />

                            <asp:HyperLink ID="mpePopUp" Click="Avances_Click" runat="server" Text='Modificar Avances' NavigateUrl="#" Visible="False"></asp:HyperLink>

                        </td>

                    </tr>
                    <tr>
                        <td style="text-align: left; width: 151px;">
                            <asp:Label runat="server" ID="lblMonto" Text="Monto"></asp:Label>
                            <br />
                            <uc2:decimales ID="txtMonto" runat="server"  />
                        </td>
                        <td style="text-align: left">
                            <asp:Label runat="server" ID="lblPlazo" Text="Plazo"></asp:Label><br />
                            <asp:TextBox ID="txtPlazo" runat="server" CssClass="textbox"  />
                        </td>
                        <td style="text-align: left">
                            <asp:Label runat="server" ID="lblPeriodicidad" Text="Periodicidad"></asp:Label><br />
                            <asp:TextBox ID="txtPeriodicidad" runat="server" CssClass="textbox periocidad"  />
                        </td>
                        <td style="text-align: left">
                            <asp:Label runat="server" ID="lblValor_cuota" Text="Valor de la Cuota"></asp:Label><br />
                            <uc2:decimales ID="txtValor_cuota" runat="server"  />
                        </td>
                        <td style="text-align: left">&nbsp;
                        </td>
                        <td style="text-align: left">&nbsp;
                        </td>
                        <td style="text-align: left">&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; width: 151px;">
                            <asp:Label runat="server" ID="lblForma_pago" Text="Forma de Pago"></asp:Label><br />
                            <asp:TextBox ID="txtForma_pago" runat="server" CssClass="textbox"  />
                        </td>
                        <td style="text-align: left">
                            <asp:Label runat="server" ID="lblMoneda" Text="Moneda"></asp:Label><br />
                            <asp:TextBox ID="txtMoneda" runat="server" CssClass="textbox"  />
                        </td>
                        <td style="text-align: left">
                            <asp:Label runat="server" ID="lblFechaAprobacion" Text="F.Aprobación"></asp:Label><br />
                            <asp:TextBox ID="txtFechaAprobacion" runat="server" CssClass="textbox"  />
                        </td>
                        <td style="text-align: left">
                            <asp:Label runat="server" ID="lblEstado" Text="Estado"></asp:Label><br />
                            <asp:TextBox ID="txtEstado" runat="server" CssClass="textbox" ReadOnly="True"></asp:TextBox>
                        </td>
                        <td style="text-align: left">&nbsp;</td>
                        <td style="text-align: left">&nbsp;</td>
                        <td style="text-align: left">&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="text-align: left; width: 151px;">
                            <asp:Label runat="server" ID="lblSaldoCapital" Text="Saldo Capital"></asp:Label><br />
                            <uc2:decimales ID="txtSaldoCapital" runat="server"  />
                        </td>
                        <td style="text-align: left">
                            <asp:Label runat="server" ID="lblFechaUltimoPago" Text="F. Ult Pago"></asp:Label><br />
                            <uc1:fecha ID="txtFechaUltimoPago" runat="server" />
                        </td>
                        <td style="text-align: left">&nbsp;                                         
                        </td>
                        <td style="text-align: left"></td>
                        <td style="text-align: left">&nbsp;</td>
                        <td style="text-align: left">&nbsp;</td>
                        <td style="text-align: left">&nbsp;</td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:UpdatePanel ID="UpdReliquidacion" runat="server">
                <ContentTemplate>
                    <table border="0" cellpadding="5" cellspacing="0" width="80%">
                        <tr>
                            <td colspan="6" style="text-align: left">
                                <strong><span style="font-size: small">MODIFICACIÓN DE CRÉDITO</strong>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left">
                                <asp:Label runat="server" ID="lblFechaProximoPago" Text="F.Proximo Pago"></asp:Label><br />
                                <uc1:fecha ID="txtFechaProximoPago" runat="server" />
                            </td>
                            <td style="text-align: left">
                                <asp:Label runat="server" ID="lblCuotasPagas" Text="Cuotas Pagas"></asp:Label><br />
                                <asp:TextBox ID="txtCuotasPagas" runat="server" CssClass="textbox" />
                                <asp:FilteredTextBoxExtender ID="txtValor_FilteredTextBoxExtender" runat="server"
                                     FilterType="Numbers, Custom" TargetControlID="txtCuotasPagas" ValidChars=",." />
                            </td>
                            <td style="text-align: left">
                                <asp:Label runat="server" ID="lblFecVencimiento" Text="F.Terminación"></asp:Label>
                                <br />
                                <uc1:fecha ID="txtFecVencimiento" runat="server"  />
                            </td>
                            <td style="text-align: left">
                                <asp:Label runat="server" ID="lblDiasAjusteNuevo" Text="Dias de Ajuste"></asp:Label><br />
                                <asp:TextBox ID="txtDiasAjusteNuevo" runat="server" CssClass="textbox"  Width="90px" />
                                <asp:FilteredTextBoxExtender ID="fteDiasAjuste" runat="server"
                                     FilterType="Numbers, Custom" TargetControlID="txtDiasAjusteNuevo" ValidChars="-,." />
                            </td>
                            <td style="text-align: left">
                                <asp:Label runat="server" ID="lblValorCuotaNuevo" Text="Vr.Cuota"></asp:Label><br />
                                <uc2:decimales ID="txtValorCuotaNuevo" runat="server"  />
                            </td>
                            <td style="text-align: left; font-size: small;">
                                <asp:Label runat="server" ID="lblSaldoCapitalNuevo" Text="Saldo Capital"></asp:Label>
                                <br />
                                <uc2:decimales ID="txtSaldoCapitalNuevo" runat="server"  />
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left;">
                                <asp:Label runat="server" ID="lblFechaSolicitudNuevo" Text="F.Solicitud"></asp:Label><br />
                                <uc1:fecha ID="txtFechaSolicitudNuevo" runat="server"  />
                            </td>
                            <td style="text-align: left">
                                <asp:Label runat="server" ID="lblFechaAprobacionNuevo" Text="F.Aprobación"></asp:Label><br />
                                <uc1:fecha ID="txtFechaAprobacionNuevo" runat="server"  />
                            </td>
                            <td style="text-align: left">
                                <asp:Label runat="server" ID="lblFechaDesembolsoNuevo" Text="F.Desembolso"></asp:Label><br />
                                <uc1:fecha ID="txtFechaDesembolsoNuevo" runat="server"  />
                            </td>
                            <td style="text-align: left">
                                <asp:Label runat="server" ID="lblfechaInicio" Text="F.Inicio"></asp:Label><br />
                                <uc1:fecha ID="txtfechaInicio" runat="server"  />
                            </td>
                            <td style="text-align: left">
                                <asp:Label ID="lblFecPrimerPago" runat="server" Text="F. Primer Pago" Visible="false" /><br />
                                <uc1:fecha ID="txtFecPrimerPago" runat="server"  Visible="false" />
                            </td>
                            <td style="text-align: left">&nbsp;</td>
                        </tr>
                        <tr>
                            <td colspan="6" style="text-align: left">
                                <strong>
                                    <asp:Label runat="server" ID="txtTittleAtr" Text="Atributos Financiados" Style="font-size: small"></asp:Label>
                                </strong>
                                <br />
                                <asp:GridView ID="gvAtributos" runat="server" AutoGenerateColumns="False" BackColor="White" 
                                    BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="0" ForeColor="Black"
                                    OnRowDataBound="gvAtributos_RowDataBound"
                                    ShowFooter="True" Style="font-size: xx-small; margin-right: 0px;" Width="80%"
                                    DataKeyNames="cod_atr">
                                    <AlternatingRowStyle BackColor="White" />
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <span>
                                                    <asp:Label ID="lblcodatributo" runat="server" Text='<%# Bind("cod_atr") %>' Visible="False"></asp:Label>
                                                </span>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="DescripcionAtributo" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="lbldescripcionatributo" runat="server" Text='<%# Bind("descripcion") %>'
                                                    Visible="False"></asp:Label>
                                                <cc1:DropDownListGrid ID="ddlAtributos" runat="server" AppendDataBoundItems="True"
                                                    CommandArgument="<%#Container.DataItemIndex %>" CssClass="textbox" Style="font-size: xx-small; text-align: left"
                                                    Width="120px">
                                                </cc1:DropDownListGrid>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" Width="120px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Forma Calculo" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <span>
                                                    <asp:Label ID="lblformacalculo" runat="server" Text='<%# Bind("formacalculo") %>'
                                                        Visible="False"></asp:Label>
                                                    <cc1:DropDownListGrid ID="ddlFormaCalculo" runat="server" AppendDataBoundItems="True"
                                                        CommandArgument="<%#Container.DataItemIndex %>" CssClass="textbox" Style="font-size: xx-small; text-align: left"
                                                        Width="120px" AutoPostBack="True" OnSelectedIndexChanged="ddlFormaCalculo_SelectedIndexChanged">
                                                    </cc1:DropDownListGrid>
                                                </span>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" Width="100px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Tasa" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <span>
                                                    <asp:TextBox ID="txttasa" onkeypress="return isDecimalNumber(event);" runat="server" CssClass="textbox" Style="font-size: xx-small; text-align: left"
                                                        Text='<%# Bind("tasa") %>' Width="100px"></asp:TextBox>
                                                </span>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="TipoTasa">
                                            <ItemTemplate>
                                                <span>
                                                    <asp:Label ID="lbltipotasa" runat="server" Text='<%# Bind("tipotasa") %>' Visible="False"></asp:Label>
                                                    <cc1:DropDownListGrid ID="ddltipotasa" runat="server" AppendDataBoundItems="True"
                                                        CommandArgument="<%#Container.DataItemIndex %>" CssClass="textbox" Style="font-size: xx-small; text-align: left"
                                                        Width="120px">
                                                    </cc1:DropDownListGrid>
                                                </span>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Desviación">
                                            <ItemTemplate>
                                                <span>
                                                    <asp:TextBox ID="txtDesviacion" onkeypress="return isDecimalNumber(event);" runat="server" CssClass="textbox" Style="font-size: xx-small; text-align: left"
                                                        Text='<%# Bind("desviacion") %>' Width="100px"></asp:TextBox>
                                                </span>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="TipoHistorico">
                                            <ItemTemplate>
                                                <span>
                                                    <asp:Label ID="lbltipohistorico" runat="server" Text='<%# Bind("tipo_historico") %>'
                                                        Visible="False"></asp:Label>
                                                    <cc1:DropDownListGrid ID="ddlTipoHistorico" runat="server" AppendDataBoundItems="True"
                                                        CommandArgument="<%#Container.DataItemIndex %>" CssClass="textbox" Style="font-size: xx-small; text-align: left"
                                                        Width="120px">
                                                    </cc1:DropDownListGrid>
                                                </span>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cobra Mora">
                                            <ItemTemplate>
                                                <span>
                                                    <asp:CheckBox ID="chkCobramora" runat="server" AutoPostBack="True" Checked='<%#Convert.ToBoolean(Eval("cobra_mora"))%>' />
                                                </span>
                                            </ItemTemplate>
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
                        <tr>
                            <td colspan="6" style="text-align: left">
                                <strong><span style="font-size: small">
                                    <asp:Label ID="lblAtrDesc" runat="server" Text="Atributos Descontados/Financiados" /></strong><br />
                                <asp:GridView ID="gvDeducciones" runat="server" Width="100%" AutoGenerateColumns="False" 
                                    AllowPaging="False" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None"
                                    BorderWidth="1px" ForeColor="Black" DataKeyNames="cod_atr"
                                    ShowFooter="True" Style="font-size: x-small">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Cod.Atr">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCodAtr" runat="server" CssClass="textbox" Text='<%# Bind("cod_atr") %>'  Width="30px" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="nom_atr" HeaderText="Descripción" HeaderStyle-Width="120px" />
                                        <asp:TemplateField HeaderText="Tipo de Descuento">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="ddlTipoDescuento" runat="server" CssClass="textbox"
                                                    DataSource="<%#ListarTiposdeDecuento() %>" DataTextField="descripcion" DataValueField="codigo"
                                                    SelectedValue='<%# Bind("tipo_descuento") %>'  />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Tipo de Liquidación">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="ddlTipoLiquidacion" runat="server" CssClass="textbox"
                                                    DataSource="<%#ListaCreditoTipoDeLiquidacion() %>" DataTextField="descripcion" DataValueField="codigo"
                                                    SelectedValue='<%# Bind("tipo_liquidacion") %>'  Width="100px" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Forma de Descuento">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="ddlFormaDescuento" runat="server" CssClass="textbox"
                                                    DataSource="<%#ListaCreditoFormadeDescuento() %>" DataTextField="descripcion" DataValueField="codigo"
                                                    SelectedValue='<%# Bind("forma_descuento") %>'  Width="100px" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Valor">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtvalor"  runat="server" Text='<%# Bind("val_atr") %>' Style="font-size: x-small" Width="60px" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Num.Cuotas">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtnumerocuotas" runat="server" Text='<%# Bind("numero_cuotas") %>' Style="font-size: x-small"  Width="60px" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cobra Mora">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="cbCobraMora" runat="server" Text=" " Checked='<%# Convert.ToBoolean(Eval("cobra_mora")) %>'  />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Tipo de Impuestos">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="ddlimpuestos" runat="server" CssClass="textbox" AppendDataBoundItems="true"
                                                    DataSource="<%#ListaImpuestos() %>" DataTextField="nombre" DataValueField="cod_atr"
                                                    SelectedValue='<%# Bind("tipo_impuesto") %>'  Width="100px">
                                                    <asp:ListItem Value="" Text="" />
                                                    <asp:ListItem Value="0" Text="" />
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle CssClass="gridHeader" />
                                    <HeaderStyle CssClass="gridHeader" />
                                    <PagerStyle CssClass="gridPager" />
                                    <RowStyle CssClass="gridItem" />
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6" style="text-align: left">
                                <uc1:ctlCodeudores runat="server" ID="gvCodeudores" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6" style="text-align: left">
                                <asp:UpdatePanel ID="UpdatePanelCuoExt" runat="server">
                                    <ContentTemplate>
                                        <asp:Panel runat="server" ID="panelCuotasExtras" Visible="False">
                                            <table style="width: 100%">
                                                <tr>
                                                    <td style="text-align: left; color: #FFFFFF; background-color: #359af2; height: 20px;" colspan="3">
                                                        <strong>Cuotas Extras</strong>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="6" style="text-align: left">
                                                        <strong><span style="font-size: small">Cuotas Extras</strong><br />
                                                        <div runat="server" id="Div1">
                                                            <uc1:ctrCuotasExtras runat="server" ID="gvCuotasExtras" />
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6" style="text-align: left">
                                <strong><span style="font-size: small">Pagos Pendientes</strong><br />
                                <asp:UpdatePanel ID="UpdatePanelPagosPendientes" runat="server">
                                    <ContentTemplate>
                                        <asp:Button ID="btnDetalle" runat="server" CssClass="btn8" OnClick="btnAgregarFilaPagoPendiente_Click"
                                            Text="+ Adicionar Detalle" />
                                        <div style="overflow: auto; max-height: 200px;">
                                            <asp:GridView ID="gvPagosPendientes" runat="server" Width="100%" AutoGenerateColumns="False"
                                                AllowPaging="False" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None"
                                                OnRowCommand="OnRowCommandDeleting"
                                                BorderWidth="1px" ForeColor="Black" DataKeyNames="idamortiza"
                                                ShowFooter="True"
                                                Style="font-size: x-small; overflow: auto; width: 800px; max-height: 200px;"
                                                OnRowDeleting="gvPagosPendientes_RowDeleting">
                                                <Columns>
                                                    <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/gr_elim.jpg" ShowDeleteButton="True">
                                                        <ItemStyle Width="16px" />
                                                    </asp:CommandField>
                                                    <asp:TemplateField HeaderText="Cod. Amort.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblidamortiza" runat="server" Text='<%# Bind("idamortiza") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Fecha Cuota">
                                                        <ItemTemplate>
                                                            <uc1:fecha ID="txtFechaPagoPendiente" Text='<%# Bind("fecha_cuota") %>' runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Cod.Atr">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="ddlCodigoAtributo" runat="server" CssClass="textbox" AppendDataBoundItems="true"
                                                                DataSource="<%#ListaAtributos() %>" DataTextField="nom_atr" DataValueField="cod_atr"
                                                                SelectedValue='<%# Bind("cod_atr") %>'>
                                                                <asp:ListItem Value="" Text="" />
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Valor">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtValorPago" onkeypress="return isNumber(event)" runat="server" Text='<%# Bind("valor") %>' Style="font-size: x-small" Width="60px" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Saldo">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtSaldoPago" onkeypress="return isNumber(event)" runat="server" Text='<%# Bind("saldo") %>' Style="font-size: x-small" Width="60px" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <FooterStyle CssClass="gridHeader" />
                                                <HeaderStyle CssClass="gridHeader" />
                                                <PagerStyle CssClass="gridPager" />
                                                <RowStyle CssClass="gridItem" />
                                            </asp:GridView>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <asp:Label ID="LabelInformacion" runat="server" />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
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
                            <asp:Label ID="lblMensaje" runat="server" Text="Modificación Realizada Correctamente"
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
                        <td style="text-align: center; font-size: large;"></td>
                    </tr>
                </table>
            </asp:Panel>
        </asp:View>
    </asp:MultiView>



    <asp:ModalPopupExtender ID="MpeDetalleAvances" runat="server"  PopupDragHandleControlID="Avances"
        PopupControlID="Panl1" TargetControlID="mpePopUp" CancelControlID="btnCloseAct2">
    </asp:ModalPopupExtender>
    <asp:Panel ID="Panl1" runat="server" higtStyle="display: none; border: solid 5px black" CssClass="modalPopup" Visible="False">
        <asp:UpdatePanel ID="UpDetalleAvances" runat="server">
            <ContentTemplate>
                <table style="background: #0066FF" id="Avances">
                    <tr>
                        <td colspan="6" style="text-align: left">
                            <strong><span style="font-size: small">Datos Avances</strong><br />
                            <asp:GridView ID="gvAvances" runat="server" AutoGenerateColumns="False"
                                BackColor="White" BorderColor="black" AllowPaging="True"
                                BorderWidth="1px" ForeColor="Black" DataKeyNames="NumAvance" GridLines="Both"
                                CellPadding="0" ShowFooter="True" Style="font-size: x-small; background: #CCCC99"
                                AutoPostBack="false" PageSize="10" OnPageIndexChanging="gvLista_PageIndexChanging">
                                <Columns>
                                    <asp:BoundField HeaderText="Codigo Avance" DataField="NumAvance" />
                                    <asp:BoundField HeaderText="Fe.Solicitud" DataField="Fecha_Solicitud" DataFormatString="{0:MM/dd/yyyy}" />
                                    <asp:BoundField HeaderText="Fe.Aprovacion" DataField="fecha_Aprobacion" DataFormatString="{0:MM/dd/yyyy}" />
                                    <asp:BoundField HeaderText="Fe.Desembolso" DataField="FechaUltiPago" DataFormatString="{0:MM/dd/yyyy}" />
                                    <asp:BoundField HeaderText="Valor Desembolsado" DataField="ValDesembolso" />
                                    <asp:BoundField HeaderText="Saldo Avance" DataField="SaldoAvance" />

                                    <asp:TemplateField HeaderText="Estado">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="stEstado" runat="server" Style="background: #CCCC99; border: none;"
                                                AppendDataBoundItems="true" Width="51px" Height="29px" DataTextField='<%# Eval("Estado") %>' CssClass="stEstado" SelectedValue='<%# Eval("Estado") %>' >
                                                <asp:ListItem Text="Terminado" Value="Terminado" />
                                                <asp:ListItem Text="Desembolsado" Value="Desembolsado" />
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="Valor Cuota" DataField="ValorCuota" />

                                    <asp:TemplateField HeaderText="Plazo">
                                        <ItemTemplate>
                                            <asp:TextBox ID="Plazo" runat="server" CssClass="textbox Plazo" Text='<%# Bind("Plazo") %>' Style="background: #CCCC99; border: none" Width="30px" Height="21px" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Tasa">
                                        <ItemTemplate>
                                            <asp:TextBox ID="Tasa" Style="background: #CCCC99; border: none" runat="server" CssClass="textbox tasa" Text='<%# Eval("Tasa")%>' Width="30px" Height="21px" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Fe.Prox. De Pago">
                                        <ItemTemplate>
                                            <asp:TextBox ID="FeProxpago" runat="server" Style="background: #CCCC99; border: none" CssClass="textbox FeProxpago" Text='<%# Eval("FechaProxPago","{0:MM/dd/yyyy}")%>' Width="103px" Height="1.3pc" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Cuotas Pendientes">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtCuPendientes" runat="server" Style="background: #CCCC99; border: none" CssClass="textbox CuPendientes" Text='<%# Eval("CuotasPendi")%>' Width="30px" Height="21px" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Cuotas Pagadas">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtCuPagadas" runat="server" Style="background: #CCCC99; border: none" CssClass="textbox CuPagadas" Text='<%# Eval("CuotasPagadas")%>' Width="30px" Height="21px" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Fecha Ultimo De Pago">
                                        <ItemTemplate>
                                            <asp:TextBox ID="FeUlPago" runat="server" Style="background: #CCCC99; border: none" CssClass="textbox FeUlPago" Text='<%# Eval("FechaUltiPago","{0:MM/dd/yyyy}")%>' Width="103px" Height="1.3pc" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Modificar">
                                        <ItemTemplate>
                                            <cc1:CheckBoxGrid ID="modifique" runat="server" CssClass="checkbox" Style="text-align: center"
                                                Width="30px" onClick="activar(this)" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                </Columns>

                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 475px; background-color: #0066FF">
                            <asp:Button ID="btnCloseAct2" runat="server" Text="Cancelar" CssClass="button" CausesValidation="False" Height="20px" OnClick="btnCancelar_Click" />
                            <asp:Button ID="BtnGuardarDatos" runat="server" Text="Guardar" CssClass="button" Height="20px" OnClick="BtnGuardarDatos_Click" />

                        </td>
                    </tr>
                </table>
                <uc4:mensajegrabar ID="Mensajegrabar1" runat="server" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
    <uc4:mensajegrabar ID="ctlMensaje" runat="server" />
    <script type="text/javascript">

        $(document).ready(function () {

            $(".Plazo").attr("readonly", "readonly");
            $(".tasa").attr("readonly", "readonly");
            $(".FeProxpago").attr("readonly", "readonly");
            $(".FeUlPago").css("background", "#CCCC99");
            $(".CuPendientes").attr("readonly", "readonly");
            $(".CuPagadas").attr("readonly", "readonly");
            $(".stEstado").attr("disabled", "disabled");
        });
        function activar(e) {
            debugger;
            var s = ($(e)[0].id);
            var index = $(e)[0].id.split("_");
            var pla = $(e).parent().parent().parent().parent().parent().find(".Plazo")[index[3]].id;
            var ta = $(e).parent().parent().parent().parent().parent().find(".tasa")[index[3]].id;
            var fepro = $(e).parent().parent().parent().parent().parent().find(".FeProxpago")[index[3]].id;
            var feul = $(e).parent().parent().parent().parent().parent().find(".FeUlPago")[index[3]].id;
            var pen = $(e).parent().parent().parent().parent().parent().find(".CuPendientes")[index[3]].id;
            var pagfa = $(e).parent().parent().parent().parent().parent().find(".CuPagadas")[index[3]].id;
            var esta = $(e).parent().parent().parent().parent().parent().find(".stEstado")[index[3]].id;
            var estsa = $(e).parent().parent().parent().parent().parent().find(".stEstado")[index[3]].value;
            var fepros = $(e).parent().parent().parent().parent().parent().find(".FeProxpago")[index[3]].value;
            var feule = $(e).parent().parent().parent().parent().parent().find(".FeUlPago")[index[3]].value;
            var uls = feule.split("/");
            var fecha = fepros.split("/");

            console.log(estsa)

            if ($("#" + s).is(":checked")) {
                $("#" + pla).attr("readonly", false);
                $("#" + pla).css("background", "white");
                $("#" + ta).attr("readonly", false);
                $("#" + ta).css("background", "white");
                $("#" + fepro).attr("readonly", false);
                $("#" + fepro).css("background", "white");
                $("#" + fepro).prop("type", "date");
                $("#" + fepro).val(fecha[2] + '-' + fecha[0] + '-' + fecha[1]);
                $("#" + feul).attr("readonly", false);
                $("#" + feul).css("background", "white");
                $("#" + feul).prop("type", "date");
                $("#" + feul).val(uls[2] + '-' + uls[0] + '-' + uls[1]);
                $("#" + pen).attr("readonly", false);
                $("#" + pen).css("background", "white");
                $("#" + pagfa).attr("readonly", false);
                $("#" + pagfa).css("background", "white");
                if (estsa !== "Desembolsado") {
                    $("#" + esta).attr("disabled", false);
                    $("#" + esta).css("background", "white");
                }


            } else {

                $("#" + pla).attr("readonly", "readonly");
                $("#" + pla).css("background", "#CCCC99");
                $("#" + ta).attr("readonly", "readonly");
                $("#" + ta).css("background", "#CCCC99");
                $("#" + fepro).attr("readonly", "readonly");
                $("#" + fepro).css("background", "#CCCC99");
                $("#" + fepro).prop("type", "text");
                $("#" + fepro).attr("readonly", "readonly");
                $("#" + feul).css("background", "#CCCC99");
                $("#" + feul).prop("type", "text");
                $("#" + pen).attr("readonly", "readonly");
                $("#" + pen).css("background", "#CCCC99");
                $("#" + pagfa).attr("readonly", "readonly");
                $("#" + pagfa).css("background", "#CCCC99");
                $("#" + esta).attr("disabled", "disabled");
                $("#" + esta).css("background", "#CCCC99");

            }
        }


        function click() {
            debugger;
            $("#BtnGuardarDatos").click(function () {
                $("#Panl1").modal('hide');
                localizacion.reload();
            })
        }
    </script>

</asp:Content>

