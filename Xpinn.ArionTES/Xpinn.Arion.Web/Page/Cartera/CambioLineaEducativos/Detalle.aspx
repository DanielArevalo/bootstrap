<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Detalle.aspx.cs" Inherits="Detalle"
    EnableEventValidation="true" MasterPageFile="~/General/Master/site.master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc2" %>
<%@ Register Src="~/General/Controles/decimalesGridRow.ascx" TagName="decimalesGridRow" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>
<%@ Register Src="~/General/Controles/ctlProveedor.ascx" TagName="BuscarProveedor" TagPrefix="uc1" %>

<%@ Register src="../../../General/Controles/decimales.ascx" tagname="Decimal" tagprefix="ucDecimal" %>

<%@ Register src="../../../General/Controles/ctlProcesoContable.ascx" tagname="procesocontable" tagprefix="uc2" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphMain">
    &nbsp;<asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <script type="text/javascript">
        function PanelClick(sender, e) {
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

        function ToggleHidden(value) {
            $find('<%=Tabs.ClientID%>').get_tabs()[2].set_enabled(value);
        }

        function mpeSeleccionOnOk() {
        }

        function mpeSeleccionOnCancel() {
        }  
    </script>
     <asp:Panel ID="panelGeneral" runat="server">
    <asp:MultiView ID="mvAprobacion" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwDatos" runat="server">
            <table style="width: 90%;">
                <tr style="text-align: left">
                    <td colspan="4">
                        <strong>DATOS DEL DEUDOR&nbsp; </strong>
                    </td>
                </tr>
                <tr style="text-align: left">
                    <td style="width: 140px">
                        Código Deudor:
                        <br />
                        <asp:TextBox ID="txtCodigo" runat="server" CssClass="textbox" Enabled="False" Width="140px"></asp:TextBox>
                    </td>
                    <td style="width: 120px">
                        Identificación:
                        <br />
                        <asp:TextBox ID="txtId" runat="server" CssClass="textbox" Enabled="False" Width="120px"></asp:TextBox>
                    </td>
                    <td style="width: 399px">
                        Tipo Identificación<br />
                        <asp:TextBox ID="txtTipoIdentificacion" runat="server" CssClass="textbox" 
                            Enabled="False" Width="154px"></asp:TextBox>
                    </td>
                    <td>
                        Nombres:
                        <br />
                        <asp:TextBox ID="txtNombres" runat="server" CssClass="textbox" Enabled="False" 
                            Width="387px"></asp:TextBox>
                        <br />
                    </td>
                </tr>
            </table>
            <table style="width: 90%;">
                <tr style="text-align: left">
                    <td colspan="6" style="text-align: left">
                        <strong>DATOS DEL CRÉDITO</strong>
                    </td>
                </tr>
                <tr style="text-align: left">
                    <td style="width: 104px">
                        No. Crédito:
                        <br />
                        <asp:TextBox ID="txtCredito" runat="server" CssClass="textbox" Enabled="False" Width="140px"></asp:TextBox>
                    </td>
                    <td colspan="2">
                        Línea de crédito:
                        <br />
                        <asp:TextBox ID="txtLinea" runat="server" CssClass="textbox" Enabled="False" Width="260px"></asp:TextBox>
                    </td>
                    <td colspan="2">
                        Monto Aprobado:
                        <br />
                        <asp:TextBox ID="txtMonto" runat="server" CssClass="textbox" Enabled="False" 
                            Width="132px"></asp:TextBox>
                        <asp:MaskedEditExtender ID="mskMonto" runat="server" AcceptNegative="Left" 
                            DisplayMoney="Left" ErrorTooltipEnabled="True" InputDirection="RightToLeft" 
                            Mask="999,999,999" MaskType="Number" MessageValidatorTip="true" 
                            OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" 
                            TargetControlID="txtMonto" />
                    </td>
                    <td class="logo" style="width: 196px">
                        &nbsp;</td>
                </tr>
                <tr style="text-align: left">
                    <td style="width: 104px">
                        Plazo:
                        <br />
                        <asp:TextBox ID="txtPlazoold" runat="server" CssClass="textbox" Enabled="False" 
                            Width="85px"></asp:TextBox>
                    </td>
                    <td style="width: 258px">
                        Periodicidad:
                        <br />
                        <asp:TextBox ID="txtPeriodicidad" runat="server" CssClass="textbox" 
                            Enabled="False" Width="130px"></asp:TextBox>
                    </td>
                    <td style="width: 258px">
                        Valor Cuota :<br />
                        <asp:TextBox ID="txtCuota" runat="server" CssClass="textbox" Enabled="False" 
                            Width="131px"></asp:TextBox>
                        <asp:MaskedEditExtender ID="txtCuota_MaskedEditExtender" runat="server" 
                            AcceptNegative="Left" DisplayMoney="Left" ErrorTooltipEnabled="True" 
                            InputDirection="RightToLeft" Mask="999,999,999" MaskType="Number" 
                            MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" 
                            OnInvalidCssClass="MaskedEditError" TargetControlID="txtCuota" />
                    </td>
                    <td style="width: 258px">
                        Forma Pago:
                        <br />
                        <asp:TextBox ID="txtFormaPago" runat="server" CssClass="textbox" 
                            Enabled="False" Width="94px"></asp:TextBox>
                    </td>
                    <td colspan="2">
                        F. Aprobación<br />
                        <asp:TextBox ID="txtFechaAprobacion" runat="server" CssClass="textbox" 
                            Enabled="False" Width="154px"></asp:TextBox>
                    </td>
                </tr>
                <tr style="text-align: left">
                    <td style="width: 104px">
                        F.Prox. Pago<br />
                        <asp:TextBox ID="txtFechaProxPago_old" runat="server" CssClass="textbox" 
                            Enabled="False" Width="154px"></asp:TextBox>
                    </td>
                    <td style="width: 258px">
                        F. Ult. Pago<br />
                        <asp:TextBox ID="txtFechaUltPago" runat="server" CssClass="textbox" 
                            Enabled="False" Width="154px"></asp:TextBox>
                    </td>
                    <td style="width: 258px">
                        Saldo Capital<br />
                        <asp:TextBox ID="txtSaldoCapital" runat="server" CssClass="textbox" 
                            Enabled="False" Width="132px"></asp:TextBox>
                        <asp:MaskedEditExtender ID="txtSaldoCapital_MaskedEditExtender" runat="server" 
                            AcceptNegative="Left" DisplayMoney="Left" ErrorTooltipEnabled="True" 
                            InputDirection="RightToLeft" Mask="999,999,999" MaskType="Number" 
                            MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" 
                            OnInvalidCssClass="MaskedEditError" TargetControlID="txtSaldoCapital" />
                    </td>
                    <td style="width: 258px">
                        Vr. Total
                        <br />
                        <asp:TextBox ID="txtValorTotal" runat="server" CssClass="textbox" 
                            Enabled="False" Width="132px"></asp:TextBox>
                        <asp:MaskedEditExtender ID="txtValorTotal_MaskedEditExtender" runat="server" 
                            AcceptNegative="Left" DisplayMoney="Left" ErrorTooltipEnabled="True" 
                            InputDirection="RightToLeft" Mask="999,999,999" MaskType="Number" 
                            MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" 
                            OnInvalidCssClass="MaskedEditError" TargetControlID="txtValorTotal" />
                    </td>
                    <td style="width: 71px">
                        &nbsp;</td>
                    <td class="logo" style="width: 196px">
                        &nbsp;</td>
                </tr>
            </table>
            <table style="width: 90%;">
                <tr style="text-align: left">
                    <td colspan="3">
                        <table style="width: 95%">
                            <tr>
                                <td colspan="4" style="text-align: left">
                                    <strong>DATOS DEL NUEVO CRÉDITO</strong>
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
                                    <br />
                                    <ucFecha:fecha ID="txtFechaRes" runat="server" />
                                </td>
                                <td colspan="2" style="text-align: left">
                                    Linea de Crédito<br />
                                    <asp:UpdatePanel ID="updLinea0" runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="ddllineas" runat="server" AutoPostBack="true" 
                                                CssClass="dropdown" OnSelectedIndexChanged="ddllineas_SelectedIndexChanged" 
                                                Width="318px">
                                            </asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td colspan="4" style="text-align: left">
                                    Fecha Primer Pago<br />
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <ucFecha:fecha ID="txtfechaproxpago" runat="server" />
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="ddlFormaPago" 
                                                EventName="SelectedIndexChanged" />
                                            <asp:AsyncPostBackTrigger ControlID="ddlEmpresa" 
                                                EventName="SelectedIndexChanged" />
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
                                    <asp:TextBox ID="txtplazo" runat="server" CssClass="textbox" MaxLength="4" 
                                        Width="50px"></asp:TextBox>
                                    <asp:FilteredTextBoxExtender ID="txtplazo_FilteredTextBoxExtender" 
                                        runat="server" Enabled="True" FilterType="Numbers, Custom" 
                                        TargetControlID="txtplazo" ValidChars="." />
                                    <asp:RequiredFieldValidator ID="rfvPlazo" runat="server" 
                                        ControlToValidate="txtplazo" Display="Dynamic" ErrorMessage="Campo Requerido" 
                                        ForeColor="Red" InitialValue="&lt;Seleccione un Item&gt;" 
                                        SetFocusOnError="True" Style="font-size: x-small" ValidationGroup="vgGuardar" />
                                </td>
                                <td colspan="2" style="text-align: left; height: 47px;">
                                    &nbsp;Periodicidad&nbsp;
                                    <br />
                                    <asp:DropDownList ID="ddlperiodicidad" runat="server" CssClass="dropdown" 
                                        Width="225px">
                                    </asp:DropDownList>
                                </td>
                                <td style="text-align: left; height: 47px;">
                                    Forma de Pago<br />
                                    <asp:DropDownList ID="ddlFormaPago" runat="server" AutoPostBack="True" 
                                        CssClass="dropdown" OnSelectedIndexChanged="ddlFormaPago_SelectedIndexChanged" 
                                        Width="225px">
                                        <asp:ListItem Value="C">Caja</asp:ListItem>
                                        <asp:ListItem Value="N">Nomina</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td colspan="4" style="text-align: left; height: 47px;">
                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                        <ContentTemplate>
                                            <asp:Label ID="lblEmpresa" runat="server" Text="Empresa"></asp:Label>
                                            <br />
                                            <asp:DropDownList ID="ddlEmpresa" runat="server" AutoPostBack="True" 
                                                CssClass="dropdown" OnSelectedIndexChanged="ddlEmpresa_SelectedIndexChanged" 
                                                Width="222px" Visible="False">
                                            </asp:DropDownList>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="ddlFormaPago" 
                                                EventName="SelectedIndexChanged" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="width: 165px; height: 47px;">
                                </td>
                            </tr>
                        </table>
                        <asp:TabContainer ID="Tabs" runat="server" ActiveTabIndex="0" 
                            CssClass="CustomTabStyle" Height="120px" 
                            OnClientActiveTabChanged="ActiveTabChanged" Style="margin-right: 30px" 
                            Width="900px">
                            <asp:TabPanel ID="tabCodeudores" runat="server" HeaderText="Datos"><HeaderTemplate>Codeudores
                            </HeaderTemplate>
                            <ContentTemplate>
                                <asp:UpdatePanel ID="UpdatePanelCodeudores" runat="server"><ContentTemplate><asp:GridView 
                                        ID="gvCodeudores" runat="server" AutoGenerateColumns="False" BackColor="White" 
                                        BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
                                        EmptyDataText="No se encontraron registros." ForeColor="Black" 
                                        GridLines="Vertical" Height="16px" OnRowCommand="gvCodeudores_RowCommand" 
                                        OnRowDataBound="gvCodeudores_RowDataBound" 
                                        OnRowDeleting="gvCodeudores_RowDeleting" PageSize="5" ShowFooter="True" 
                                        ShowHeaderWhenEmpty="True" Style="font-size: x-small" Width="100%"><AlternatingRowStyle 
                                        BackColor="White" /><Columns><asp:TemplateField ShowHeader="False"><FooterTemplate><asp:ImageButton 
                                            ID="btnNuevo" runat="server" CausesValidation="False" CommandName="AddNew" 
                                            ImageUrl="~/Images/gr_nuevo.jpg" ToolTip="Crear Nuevo" Width="16px" />
                                        </FooterTemplate>
                                        <ItemStyle Width="20px" />
                                        </asp:TemplateField><asp:TemplateField><ItemTemplate><asp:ImageButton 
                                                ID="btnEliminar" runat="server" 
                                                CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" 
                                                CommandName="Delete" ImageUrl="~/Images/gr_elim.jpg" ToolTip="Eliminar" 
                                                Width="16px" />
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="gridIco" />
                                        </asp:TemplateField><asp:TemplateField HeaderText="Código"><ItemTemplate><asp:Label 
                                                ID="lblcodpersona" runat="server" Text='<%# Bind("codpersona") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="txtcodpersona" runat="server" Text='<%# Bind("codpersona") %>'></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle Width="30px" />
                                        </asp:TemplateField><asp:TemplateField HeaderText="Identificación"><ItemTemplate><asp:Label 
                                                ID="lblidentificacion" runat="server" Text='<%# Bind("identificacion") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtidentificacion" runat="server" AutoPostBack="True" 
                                                OnTextChanged="txtidentificacion_TextChanged" Style="font-size: x-small" 
                                                Text='<%# Bind("identificacion") %>'></asp:TextBox>
                                        </FooterTemplate>
                                        <ItemStyle Width="50px" />
                                        </asp:TemplateField><asp:TemplateField HeaderText="Nombres y Apellidos"><ItemTemplate><asp:Label 
                                                ID="lblnombres" runat="server" Text='<%# Bind("nombres") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="txtnombres" runat="server" Text='<%# Bind("nombres") %>'></asp:Label>
                                        </FooterTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle CssClass="gridHeader" />
                                    <HeaderStyle CssClass="gridHeader" />
                                    <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                                    <RowStyle CssClass="gridItem" />
                                    <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                    <sortedascendingcellstyle backcolor="#FBFBF2" />
                                    <sortedascendingheaderstyle backcolor="#848384" />
                                    <sorteddescendingcellstyle backcolor="#EAEAD3" />
                                    <sorteddescendingheaderstyle backcolor="#575357" />
                                    </asp:GridView>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <asp:Label ID="lblTotalRegsCodeudores" runat="server" 
                                    Text="Su consulta no obtuvo ningun resultado." Visible="False" />
                            </ContentTemplate>
                            </asp:TabPanel>
                        </asp:TabContainer>
                            <asp:HiddenField ID="HiddenField1" runat="server" />
    <asp:ModalPopupExtender ID="mpeNuevo" runat="server" PopupControlID="panelActividadReg"
        TargetControlID="HiddenField1" BackgroundCssClass="backgroundColor">
    </asp:ModalPopupExtender>
                        <asp:Panel ID="panelActividadReg" runat="server" BackColor="White" 
                            BorderWidth="1px" Style="text-align: right" Width="500px">
                            <div id="popupcontainer" style="width: 500px">
                                <table style="width: 100%;">
                                    <tr>
                                        <td colspan="3">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" style="text-align: center">
                                            Esta Seguro de Realizar la solicitud del crédito educativo ?
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td style="text-align: center">
                                            <asp:Button ID="btnContinuar" runat="server" CssClass="btn8" 
                                                OnClick="btnContinuar_Click" Text="Continuar" Width="182px" 
                                                ValidationGroup="vgGuardar" />
                                            &nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:Button ID="btnParar" runat="server" CssClass="btn8" 
                                                OnClick="btnParar_Click" Text="Cancelar" Width="182px" />
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </asp:Panel>
                        <br />
                    </td>
                </tr>
            </table>
           
            <hr style="border-color: #0000FF; border-width: 3px;" width="100%" />
        </asp:View>
        <asp:View ID="mvFinal" runat="server">
            <asp:Panel ID="PanelFinal" runat="server" Height="84px">
                <table style="width: 100%;">
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <asp:Label ID="lblMensaje" runat="server" Style="color: #FF3300" 
                                Text="Credito Realizado Correctamente"></asp:Label>
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <asp:Button ID="btnContinua" runat="server" Text="Continuar" 
                                OnClick="btnContinua_Click" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
               
        </asp:View>
    </asp:MultiView>
         <asp:GridView ID="gvDocumentos" runat="server" AllowPaging="True" 
                AutoGenerateColumns="False" GridLines="Horizontal" PageSize="20" 
                ShowHeaderWhenEmpty="True" Style="text-align: left; font-size: x-small;" 
                Width="18%">
                <Columns>
                    <asp:BoundField DataField="numero_radicacion" HeaderText="Número Radicación" />
                    <asp:BoundField DataField="tipo_documento" HeaderText="Tipo Documento" />
                 
                </Columns>
                <HeaderStyle CssClass="gridHeader" />
                <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                <RowStyle CssClass="gridItem" />
            </asp:GridView>
        
    </asp:Panel>
    <script type="text/javascript">
        window.onload = function () {
            if (typeof window.event == 'undefined') {
                document.onkeypress = function (e) {
                    var test_var = e.target.nodeName.toUpperCase();
                    if (e.target.type) var test_type = e.target.type.toUpperCase();
                    if ((test_var == 'INPUT' && test_type == 'TEXT') || test_var == 'TEXTAREA') {
                        return e.keyCode;
                    } else if (e.keyCode == 8) {
                        e.preventDefault();
                    }
                }
            } else {
                document.onkeydown = function () {
                    var test_var = event.srcElement.tagName.toUpperCase();
                    if (event.srcElement.type) var test_type = event.srcElement.type.toUpperCase();
                    if ((test_var == 'INPUT' && test_type == 'TEXT') || test_var == 'TEXTAREA') {
                        return event.keyCode;
                    } else if (event.keyCode == 8) {
                        event.returnValue = false;
                    }
                }
            }
        }    
    </script>
</asp:Content>
