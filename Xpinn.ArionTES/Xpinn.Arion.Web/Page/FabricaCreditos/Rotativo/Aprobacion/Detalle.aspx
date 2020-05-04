<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Detalle.aspx.cs" Inherits="Detalle"
    EnableEventValidation="true" MasterPageFile="~/General/Master/site.master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc2" %>
<%@ Register Src="~/General/Controles/decimalesGridRow.ascx" TagName="decimalesGridRow" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>
<%@ Register Src="~/General/Controles/ctlProveedor.ascx" TagName="BuscarProveedor" TagPrefix="uc1" %>

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

       
        function mpeSeleccionOnOk() {
        }

        function mpeSeleccionOnCancel() {
        }  
    </script>
    <asp:MultiView ID="mvAprobacion" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwDatos" runat="server">
            <table style="width: 90%;">
                <tr style="text-align: left">
                    <td colspan="6">
                        <strong>Datos del Deudor&nbsp; </strong>
                    </td>
                </tr>
                <tr style="text-align: left">
                    <td style="width: 140px">
                        Código del cliente:
                        <br />
                        <asp:TextBox ID="txtCodigo" runat="server" CssClass="textbox" Enabled="False" 
                            Width="140px"></asp:TextBox>
                    </td>
                    <td style="width: 120px">
                        Identificación:
                        <br />
                        <asp:TextBox ID="txtId" runat="server" CssClass="textbox" Enabled="False" 
                            Width="120px"></asp:TextBox>
                    </td>
                    <td style="width: 399px">
                        Nombres:
                        <br />
                        <asp:TextBox ID="txtNombres" runat="server" CssClass="textbox" Enabled="False" 
                            Width="387px"></asp:TextBox>
                    </td>
                    <td style="width: 80px">
                        <br />
                    </td>
                    <td style="width: 80px">
                        Calificación:
                        <br />
                        <asp:TextBox ID="txtCalificacion" runat="server" CssClass="textbox" 
                            Enabled="False" Width="80px"></asp:TextBox>
                    </td>
                    <td>
                        <strong>
                        <asp:DropDownList ID="ddlProceso" runat="server" CssClass="dropdown" 
                            Enabled="False" Height="25px" Visible="False" Width="186px">
                        </asp:DropDownList>
                        </strong>
                    </td>
                </tr>
            </table>
            <table style="width: 90%;">
                <tr style="text-align: left">
                    <td colspan="5" style="text-align: left">
                        <strong>Datos del Crédito</strong>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr style="text-align: left">
                    <td style="width: 104px">
                        No. Crédito:
                        <br />
                        <asp:TextBox ID="txtCredito" runat="server" CssClass="textbox" Enabled="False" 
                            Width="140px"></asp:TextBox>
                    </td>
                    <td style="width: 258px">
                        Línea de crédito:
                        <br />
                        <asp:TextBox ID="txtLinea" runat="server" CssClass="textbox" Enabled="False" 
                            Width="260px"></asp:TextBox>
                    </td>
                    <td class="logo" style="width: 144px">
                        Monto Solicitado:
                        <br />
                        <asp:TextBox ID="txtMonto" runat="server" CssClass="textbox" Enabled="False" 
                            Width="132px"></asp:TextBox>
                        <asp:MaskedEditExtender ID="mskMonto" runat="server" AcceptNegative="Left" 
                            DisplayMoney="Left" ErrorTooltipEnabled="True" InputDirection="RightToLeft" 
                            Mask="999,999,999" MaskType="Number" MessageValidatorTip="true" 
                            OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" 
                            TargetControlID="txtMonto" />
                    </td>
                    <td style="width: 131px">
                        Plazo:
                        <br />
                        <asp:TextBox ID="txtPlazo" runat="server" CssClass="textbox" Enabled="False" 
                            Width="85px"></asp:TextBox>
                    </td>
                    <td style="width: 245px">
                        Cuota Crédito:<br />
                        <asp:TextBox ID="txtCuota" runat="server" CssClass="textbox" Enabled="False" 
                            Width="131px"></asp:TextBox>
                        <asp:MaskedEditExtender ID="mskCuota" runat="server" AcceptNegative="Left" 
                            DisplayMoney="Left" ErrorTooltipEnabled="True" InputDirection="RightToLeft" 
                            Mask="999,999,999" MaskType="Number" MessageValidatorTip="true" 
                            OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" 
                            TargetControlID="txtCuota" />
                    </td>
                    <td style="width: 131px">
                        Forma Pago:
                        <br />
                        <asp:TextBox ID="txtFormaPago" runat="server" CssClass="textbox" 
                            Enabled="False" Width="94px"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <table style="width: 90%;">
                <tr style="text-align: left">
                    <td style="text-align: left; width: 444px;">
                        Asesor:<br />
                        <asp:TextBox ID="txtAsesor" runat="server" CssClass="textbox" Enabled="False" 
                            Width="433px"></asp:TextBox>
                    </td>
                    <td style="text-align: left; width: 138px;">
                        Periodicidad:
                        <br />
                        <asp:TextBox ID="txtPeriodicidad" runat="server" CssClass="textbox" 
                            Enabled="False" Width="130px"></asp:TextBox>
                    </td>
                    <td style="text-align: left; width: 153px;">
                        Disponible:<br />
                        <asp:TextBox ID="txtDisp" runat="server" CssClass="textbox" Enabled="False" 
                            Width="148px"></asp:TextBox>
                    </td>
                    <td>
                        <span>
                        <asp:TextBox ID="TxtValorTasaCred" runat="server" CssClass="textbox" 
                            MaxLength="250" Visible="false" Width="88px"></asp:TextBox>
                        </span>
                    </td>
                </tr>
            </table>
            <table style="width: 90%;">
                <tr style="text-align: left">
                    <td colspan="3">
                        <strong>Información del Crédito</strong>
                    </td>
                </tr>
            </table>
            <asp:TabContainer ID="Tabs" runat="server" ActiveTabIndex="0" 
                CssClass="CustomTabStyle" Height="120px" 
                OnClientActiveTabChanged="ActiveTabChanged" Style="margin-right: 30px" 
                Width="900px">
                <asp:TabPanel ID="tabCodeudores" runat="server" HeaderText="Datos">
                    <HeaderTemplate>
                        Codeudores
                    </HeaderTemplate>
                    <ContentTemplate>
                        <asp:UpdatePanel ID="UpdatePanelCodeudores" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="gvListaCodeudores" runat="server" AllowPaging="True" 
                                    AutoGenerateColumns="False" BackColor="White" BorderColor="#DEDFDE" 
                                    BorderStyle="None" BorderWidth="1px" DataKeyNames="cod_persona" 
                                    ForeColor="Black" OnPageIndexChanging="gvListaCodeudores_PageIndexChanging" 
                                    OnRowCommand="gvListaCodeudores_RowCommand" 
                                    OnRowDeleting="gvListaCodeudores_RowDeleting" PageSize="5" ShowFooter="True" 
                                    Style="font-size: x-small" Width="100%">
                                    <Columns>
                                        <asp:TemplateField ShowHeader="False">
                                            <FooterTemplate>
                                                <asp:ImageButton ID="btnNuevo" runat="server" CausesValidation="False" 
                                                    CommandName="AddNew" Height="16px" ImageUrl="~/Images/gr_nuevo.jpg" 
                                                    ToolTip="Crear Nuevo" Width="16px" />
                                            </FooterTemplate>
                                            <ItemStyle Width="20px" />
                                        </asp:TemplateField>
                                        <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/gr_elim.jpg" 
                                            ShowDeleteButton="True">
                                        <ItemStyle Width="16px" />
                                        </asp:CommandField>
                                        <asp:BoundField DataField="cod_persona" HeaderText="Codpersona" />
                                        <asp:TemplateField HeaderText="Identificación">
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtidentificacion" runat="server" AutoPostBack="True" 
                                                    OnTextChanged="txtidentificacion_TextChanged" Style="font-size: x-small" 
                                                    Text='<%# Bind("IDENTIFICACION") %>'></asp:TextBox>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblidentificacion" runat="server" 
                                                    Text='<%# Bind("IDENTIFICACION") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="50px" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="PRIMER_NOMBRE" HeaderText="Primer Nombre" />
                                        <asp:BoundField DataField="SEGUNDO_NOMBRE" HeaderText="Segundo Nombre" />
                                        <asp:BoundField DataField="PRIMER_APELLIDO" HeaderText="Primer Apellido" />
                                        <asp:BoundField DataField="SEGUNDO_APELLIDO" HeaderText="Segundo Apellido" />
                                        <asp:BoundField DataField="DIRECCION" HeaderText="Dirección" />
                                        <asp:BoundField DataField="TELEFONO" HeaderText="Teléfono" />
                                    </Columns>
                                    <FooterStyle CssClass="gridHeader" />
                                    <HeaderStyle CssClass="gridHeader" />
                                    <PagerStyle CssClass="gridPager" />
                                    <RowStyle CssClass="gridItem" />
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <asp:Label ID="lblTotalRegsCodeudores" runat="server" 
                            Text="Su consulta no obtuvo ningun resultado." Visible="False" />
                    </ContentTemplate>
                </asp:TabPanel>
            </asp:TabContainer>
            <hr style="border-color: #0000FF; border-width: 3px;" width="100%" />
            <table width="100%">
                <tr>
                    <td>
                        <asp:RadioButtonList ID="rbTipoAprobacion" runat="server" AutoPostBack="True" 
                            onselectedindexchanged="rbTipoAprobacion_SelectedIndexChanged" 
                            RepeatDirection="Horizontal">
                            <asp:ListItem Text="Aprobación" Value="1" />
                            <asp:ListItem Text="Aplazamiento" Value="2" />
                            <asp:ListItem Text="Negar Crédito" Value="3" />
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Panel ID="panelAprobacion" runat="server" Visible="false">
                            <table style="width: 100%;">
                                <tr>
                                    <td colspan="2" style="text-align: center">
                                        <strong>APROBACIÓN DE CRÉDITOS:</strong>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        No. Crédito:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtCredito2" runat="server" CssClass="textbox" Enabled="false" 
                                            Width="145px"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvCredito2" runat="server" 
                                            ControlToValidate="txtCredito2" Display="Dynamic" 
                                            ErrorMessage="Campo Requerido" ForeColor="Red" SetFocusOnError="True" 
                                            Style="font-size: xx-small" ValidationGroup="vgAcpAprobar" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Observaciones de aprobación:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtObsApr" runat="server" CssClass="textbox" MaxLength="250" 
                                            Width="283px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: center">
                                        <asp:Button ID="btnAcpAprobar" runat="server" CssClass="btn8" 
                                            OnClick="btnAcpAprobar_Click" Text="Aceptar" ValidationGroup="vgAcpAprobar" />
                                    </td>
                                    <td style="text-align: center">
                                        <asp:Button ID="btnCncAprobar" runat="server" CssClass="btn8" 
                                            OnClick="btnCncAprobar_Click" Text="Cancelar" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <asp:Panel ID="panelAprobacionCom" runat="server">
                            <table cellpadding="1" cellspacing="1" style="width: 100%">
                                <tr>
                                    <td colspan="3" style="text-align: center">
                                        <strong>
                                        <asp:Label ID="Lblaprob" runat="server"></asp:Label>
                                        </strong>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: left">
                                        No. Crédito:
                                    </td>
                                    <td style="text-align: left">
                                        <asp:TextBox ID="txtCredito3" runat="server" CssClass="textbox" Enabled="false" 
                                            Width="145px"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvCredito3" runat="server" 
                                            ControlToValidate="txtCredito3" Display="Dynamic" 
                                            ErrorMessage="Campo Requerido" ForeColor="Red" SetFocusOnError="True" 
                                            Style="font-size: xx-small" ValidationGroup="vgAcpAproModif" />
                                        <asp:TextBox ID="txtFecha" runat="server" CssClass="textbox" 
                                            Style="text-align: left" Visible="False"></asp:TextBox>
                                        <asp:CalendarExtender ID="txtFecha_CalendarExtender" runat="server" 
                                            Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtFecha">
                                        </asp:CalendarExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: left; width: 20%">
                                        Observaciones:
                                    </td>
                                    <td style="text-align: left">
                                        <asp:TextBox ID="txtObs3" runat="server" CssClass="textbox" MaxLength="250" 
                                            Width="300px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: left">
                                        Monto:
                                    </td>
                                    <td style="text-align: left">
                                        <asp:TextBox ID="txtMonto2" runat="server" CssClass="textbox" Width="145px"></asp:TextBox>
                                        <asp:MaskedEditExtender ID="mskMonto2" runat="server" AcceptNegative="Left" 
                                            DisplayMoney="Left" ErrorTooltipEnabled="True" InputDirection="RightToLeft" 
                                            Mask="999,999,999" MaskType="Number" MessageValidatorTip="true" 
                                            OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" 
                                            TargetControlID="txtMonto2" />
                                        &nbsp;
                                        <asp:RequiredFieldValidator ID="rfvMonto2" runat="server" 
                                            ControlToValidate="txtMonto2" Display="Dynamic" ErrorMessage="Campo Requerido" 
                                            ForeColor="Red" SetFocusOnError="True" Style="font-size: xx-small" 
                                            ValidationGroup="vgAcpAproModif" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: left">
                                        Línea de Crédito:
                                    </td>
                                    <td style="text-align: left">
                                        <asp:DropDownList ID="ddlLineaCredito" runat="server" AutoPostBack="True" 
                                            CssClass="textbox" OnSelectedIndexChanged="ddlLineaCredito_TextChanged" 
                                            OnTextChanged="ddlLineaCredito_TextChanged" Width="305px">
                                        </asp:DropDownList>
                                    </td>
                                    <td style="text-align: left; width: 150px">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="text-align: left">
                                        Plazo crédito:
                                    </td>
                                    <td style="text-align: left">
                                        <asp:TextBox ID="txtPlazo2" runat="server" CssClass="textbox" MaxLength="5" 
                                            Width="86px"></asp:TextBox>
                                        &nbsp;
                                        <asp:RequiredFieldValidator ID="rfvPlazo2" runat="server" 
                                            ControlToValidate="txtPlazo2" Display="Dynamic" ErrorMessage="Campo Requerido" 
                                            ForeColor="Red" SetFocusOnError="True" Style="font-size: xx-small" 
                                            ValidationGroup="vgAcpAproModif" />
                                        <asp:DropDownList ID="ddlPlazo" runat="server" CssClass="textbox" Width="150px">
                                        </asp:DropDownList>
                                        &nbsp;<asp:CompareValidator ID="cvPlazo" runat="server" ControlToValidate="ddlPlazo" 
                                            Display="Dynamic" ErrorMessage="Seleccione una periodicidad" ForeColor="Red" 
                                            Operator="GreaterThan" SetFocusOnError="true" 
                                            Text="&lt;strong&gt;*&lt;/strong&gt;" Type="Integer" 
                                            ValidationGroup="vgAcpAproModif" ValueToCompare="0">
                                        </asp:CompareValidator>
                                    </td>
                                    <td style="text-align: left">
                                        <asp:CheckBox ID="chekpago" runat="server" visible="false" />
                                        <asp:Label ID="lbl4" runat="server" Text=" Condicion especial de pago ?" visible="false"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: left;">
                                        Fecha Primer Pago
                                    </td>
                                    <td style="text-align: left">
                                        <ucFecha:fecha ID="ucFechaPrimerPago" runat="server" style="text-align: left" />
                                    </td>
                                    <td style="text-align: left">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: left">
                                        Forma de Pago:
                                    </td>
                                    <td colspan="2" style="text-align: left">
                                        <asp:DropDownList ID="ddlFormaPago" runat="server" AutoPostBack="true" 
                                            CssClass="textbox" OnSelectedIndexChanged="ddlFormaPago_SelectedIndexChanged" 
                                            Width="225px">
                                            <asp:ListItem Value="C">Caja</asp:ListItem>
                                            <asp:ListItem Value="N">Nomina</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: left" colspan="3">
                                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                            <ContentTemplate>
                                                <span style="font-size: small">
                                                <asp:GridView ID="gvDeducciones" runat="server" AllowPaging="False" 
                                                    AutoGenerateColumns="False" BackColor="White" BorderColor="#DEDFDE" 
                                                    BorderStyle="None" BorderWidth="1px" DataKeyNames="cod_atr" ForeColor="Black" 
                                                    ShowFooter="True" Style="font-size: x-small" Width="100%">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Cod.Atr">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCodAtr" runat="server" CssClass="textbox" Enabled="false" 
                                                                    Text='<%# Bind("cod_atr") %>' Width="30px" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="nom_atr" HeaderStyle-Width="120px" 
                                                            HeaderText="Descripción" />
                                                        <asp:TemplateField HeaderText="Tipo de Descuento">
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="ddlTipoDescuento" runat="server" CssClass="textbox" 
                                                                    DataSource="<%#ListarTiposdeDecuento() %>" DataTextField="descripcion" 
                                                                    DataValueField="codigo" Enabled="false" 
                                                                    SelectedValue='<%# Bind("tipo_descuento") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Tipo de Liquidación">
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="ddlTipoLiquidacion" runat="server" CssClass="textbox" 
                                                                    DataSource="<%#ListaCreditoTipoDeLiquidacion() %>" DataTextField="descripcion" 
                                                                    DataValueField="codigo" Enabled="false" 
                                                                    SelectedValue='<%# Bind("tipo_liquidacion") %>' Width="100px" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Forma de Descuento">
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="ddlFormaDescuento" runat="server" CssClass="textbox" 
                                                                    DataSource="<%#ListaCreditoFormadeDescuento() %>" DataTextField="descripcion" 
                                                                    DataValueField="codigo" Enabled="false" 
                                                                    SelectedValue='<%# Bind("forma_descuento") %>' Width="100px" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Valor">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtvalor" runat="server" Style="font-size: x-small" 
                                                                    Text='<%# Bind("val_atr") %>' Width="60px" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Num.Cuotas">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtnumerocuotas" runat="server" Enabled="false" 
                                                                    Style="font-size: x-small" Text='<%# Bind("numero_cuotas") %>' Width="60px" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Cobra Mora">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="cbCobraMora" runat="server" 
                                                                    Checked='<%# Convert.ToBoolean(Eval("cobra_mora")) %>' Enabled="false" 
                                                                    Text=" " />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Tipo de Impuestos">
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="ddlimpuestos" runat="server" CssClass="textbox" 
                                                                    DataSource="<%#ListaImpuestos() %>" DataTextField="descripcion" 
                                                                    DataValueField="codigo" Enabled="false" 
                                                                    SelectedValue='<%# Bind("tipo_impuesto") %>' Width="100px">
                                                                </asp:DropDownList>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <FooterStyle CssClass="gridHeader" />
                                                    <HeaderStyle CssClass="gridHeader" />
                                                    <PagerStyle CssClass="gridPager" />
                                                    <RowStyle CssClass="gridItem" />
                                                </asp:GridView>
                                                </span>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3" style="text-align: left">
                                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                            <ContentTemplate>
                                                <asp:Panel ID="panelEmpresaRec" runat="server">
                                                    <strong>Empresa Recaudadora</strong><br />
                                                    <asp:GridView ID="gvEmpresaRecaudora" runat="server" 
                                                        AutoGenerateColumns="False" BackColor="White" BorderColor="#DEDFDE" 
                                                        BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" 
                                                        GridLines="Horizontal" HeaderStyle-Height="25px" PageSize="15" 
                                                        ShowHeaderWhenEmpty="True" Style="font-size: x-small">
                                                        <AlternatingRowStyle BackColor="White" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Código">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblCodigo" runat="server" Text='<%# Bind("cod_empresa") %>' />
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Nombre">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblNombre" runat="server" Text='<%# Bind("nom_empresa") %>' />
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="250px" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" 
                                                                HeaderStyle-Width="80px" HeaderText="%" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <cc1:TextBoxGrid ID="txtPorcentaje" runat="server" AutoPostBack="true" 
                                                                        CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CssClass="textbox" 
                                                                        MaxLength="5" OnTextChanged="txtPorcentaje_OnTextChanged" 
                                                                        Style="text-align: right" Width="50px" />
                                                                    %
                                                                    <asp:FilteredTextBoxExtender ID="ftb12" runat="server" Enabled="True" 
                                                                        FilterType="Numbers, Custom" TargetControlID="txtPorcentaje" ValidChars="," />
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Valor">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtValor" runat="server" CssClass="textbox" Enabled="false" 
                                                                        MaxLength="12" Style="text-align: right; text-align: right" Width="50px" />
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="center" />
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
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="ddlFormaPago" 
                                                    EventName="SelectedIndexChanged" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3" style="text-align: left">
                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                            <ContentTemplate>
                                                <asp:CheckBox ID="chkTasa" runat="server" AutoPostBack="true" 
                                                    OnCheckedChanged="chkTasa_CheckedChanged" Text="Actualizar Tasa" />
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="chkTasa" EventName="CheckedChanged" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3" style="text-align: left">
                                        <asp:UpdatePanel ID="updtipoTasa" runat="server">
                                            <ContentTemplate>
                                                <asp:RadioButtonList ID="rbCalculoTasa" runat="server" AutoPostBack="True" 
                                                    OnSelectedIndexChanged="rbCalculoTasa_SelectedIndexChanged" 
                                                    RepeatDirection="Horizontal" Style="font-size: small">
                                                    <asp:ListItem Value="1">Tasa Fija</asp:ListItem>
                                                    <asp:ListItem Value="3">Histórico Fijo</asp:ListItem>
                                                    <asp:ListItem Value="5">Histórico Variable</asp:ListItem>
                                                </asp:RadioButtonList>
                                                <asp:Label ID="lblCalculoTasa" runat="server" Style="color: Red" 
                                                    Text="&lt;strong&gt;*&lt;/strong&gt;" Visible="false"></asp:Label>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                            <ContentTemplate>
                                                <asp:Panel ID="PanelHistorico" runat="server">
                                                    <table style="width: 80%">
                                                        <tr>
                                                            <td style="text-align: left; width: 40%">
                                                                Tipo Histórico<br />
                                                                <asp:DropDownList ID="ddlHistorico" runat="server" CssClass="textbox" 
                                                                    Width="224px">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td style="text-align: left; width: 40%">
                                                                Spread<br />
                                                                <asp:TextBox ID="txtDesviacion" runat="server" CssClass="textbox" 
                                                                    Width="100px" />
                                                                <asp:FilteredTextBoxExtender ID="fte16" runat="server" Enabled="True" 
                                                                    FilterType="Numbers, Custom" TargetControlID="txtDesviacion" ValidChars="," />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                                <asp:Panel ID="PanelFija" runat="server">
                                                    <table style="width: 80%">
                                                        <tr>
                                                            <td style="text-align: left; width: 40%">
                                                                Tasa<br />
                                                                <asp:TextBox ID="txtTasa" runat="server" CssClass="textbox" Width="100px" />
                                                                <asp:FilteredTextBoxExtender ID="ftb15" runat="server" Enabled="True" 
                                                                    FilterType="Numbers, Custom" TargetControlID="txtTasa" ValidChars="," />
                                                            </td>
                                                            <td style="text-align: left; width: 40%">
                                                                Tipo de Tasa<br />
                                                                <asp:DropDownList ID="ddlTipoTasa" runat="server" CssClass="textbox" 
                                                                    Width="224px" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="rbCalculoTasa" 
                                                    EventName="SelectedIndexChanged" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3" style="text-align: left">
                                        <b>
                                        <asp:Label ID="lblError" runat="server" Style="color: Red;" Visible="false" />
                                        </b>
                                        <asp:ValidationSummary ID="ValidationSummary2" runat="server" 
                                            DisplayMode="BulletList" ForeColor="Red" HeaderText="Errores:" 
                                            ShowMessageBox="false" ShowSummary="true" ValidationGroup="vgAprModif" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3" style="text-align: center">
                                        <asp:Button ID="btnAcpAproModif" runat="server" CssClass="btn8" 
                                            OnClick="btnAcpAproModif_Click" Text="Aceptar" 
                                            ValidationGroup="vgAcpAproModif" />
                                        &nbsp;&nbsp;&nbsp;
                                        <asp:Button ID="btnCncAproModif" runat="server" CssClass="btn8" 
                                            OnClick="btnCncAproModif_Click" Text="Cancelar" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <asp:Panel ID="panelAplazamiento" runat="server" Visible="false">
                            <table style="width: 100%;">
                                <tr>
                                    <td colspan="3" style="text-align: center">
                                        <strong>APLAZAMIENTO DE CRÉDITOS:</strong>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        No. Crédito:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtCredito4" runat="server" CssClass="textbox" Enabled="false" 
                                            Width="145px"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvCredito4" runat="server" 
                                            ControlToValidate="txtCredito4" Display="Dynamic" 
                                            ErrorMessage="Campo Requerido" ForeColor="Red" SetFocusOnError="True" 
                                            ValidationGroup="vgAplazar" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Motivo de aplazamiento:
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlAplazar" runat="server" CssClass="dropdown" 
                                            Width="150px">
                                        </asp:DropDownList>
                                        &nbsp;<asp:CompareValidator ID="cvAplaz" runat="server" 
                                            ControlToValidate="ddlAplazar" Display="Dynamic" 
                                            ErrorMessage="Seleccione un motivo de aplazamiento" ForeColor="Red" 
                                            Operator="GreaterThan" SetFocusOnError="true" 
                                            Text="&lt;strong&gt;*&lt;/strong&gt;" Type="Integer" 
                                            ValidationGroup="vgAplazar" ValueToCompare="0">
                                        </asp:CompareValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Observación:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtObs4" runat="server" CssClass="textbox" MaxLength="250" 
                                            Width="145px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:ValidationSummary ID="ValidationSummary3" runat="server" 
                                            DisplayMode="BulletList" ForeColor="Red" HeaderText="Errores:" 
                                            ShowMessageBox="false" ShowSummary="true" ValidationGroup="vgAplazar" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: center">
                                        <asp:Button ID="btnAcpAlazar" runat="server" CssClass="btn8" 
                                            OnClick="btnAcpAplazar_Click" Text="Aceptar" ValidationGroup="vgAplazar" />
                                    </td>
                                    <td style="text-align: center">
                                        <asp:Button ID="btnAcpAplazar" runat="server" CssClass="btn8" 
                                            OnClick="btnCncAplaz_Click" Text="Cancelar" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <asp:Panel ID="panelNegar" runat="server" Visible="false">
                            <table style="width: 100%; text-align: left">
                                <tr>
                                    <td colspan="3" style="text-align: center">
                                        <strong>NEGAR CRÈDITO</strong>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: left">
                                        No. Crédito:
                                    </td>
                                    <td style="text-align: left">
                                        <asp:TextBox ID="txtCredito5" runat="server" CssClass="textbox" Enabled="false" 
                                            Width="145px"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvCredito5" runat="server" 
                                            ControlToValidate="txtCredito5" Display="Dynamic" 
                                            ErrorMessage="Campo Requerido" ForeColor="Red" SetFocusOnError="True" 
                                            ValidationGroup="vgNegar" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: left">
                                        Motivo de negación:
                                    </td>
                                    <td style="text-align: left">
                                        <asp:DropDownList ID="ddlNegar" runat="server" CssClass="dropdown" 
                                            Width="150px">
                                        </asp:DropDownList>
                                        &nbsp;<asp:CompareValidator ID="cvNegar" runat="server" ControlToValidate="ddlNegar" 
                                            Display="Dynamic" ErrorMessage="Seleccione un motivo de negacion" 
                                            ForeColor="Red" Operator="GreaterThan" SetFocusOnError="true" 
                                            Text="&lt;strong&gt;*&lt;/strong&gt;" Type="Integer" ValidationGroup="vgNegar" 
                                            ValueToCompare="0">
                                        </asp:CompareValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: left">
                                        Observación:
                                    </td>
                                    <td style="text-align: left">
                                        <asp:TextBox ID="txtObs5" runat="server" CssClass="textbox" MaxLength="250" 
                                            Width="145px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="text-align: center">
                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
                                            DisplayMode="BulletList" ForeColor="Red" HeaderText="Errores:" 
                                            ShowMessageBox="false" ShowSummary="true" ValidationGroup="vgNegar" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: center">
                                        <asp:Button ID="btnAcpNegar" runat="server" CssClass="btn8" 
                                            OnClick="btnAcpNegar_Click" Text="Aceptar" ValidationGroup="vgNegar" />
                                    </td>
                                    <td style="text-align: center">
                                        <asp:Button ID="btnCncNegar" runat="server" CssClass="btn8" 
                                            OnClick="btnCncNegar_Click" Text="Cancelar" />
                                    </td>
                                </tr>
                            </table>
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
                            <asp:Label ID="lblMensajeGrabar" runat="server" Text="Crèdito Aprobado/Negado Correctamente"></asp:Label>
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
                            <asp:Button ID="btnContinuar" runat="server" Text="Continuar" OnClick="btnContinuar_Click" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </asp:View>
    </asp:MultiView>
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
