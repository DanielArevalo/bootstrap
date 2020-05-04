<%@ Page Language="C#" ValidateRequest="false" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" Title=".: Xpinn - LineasCredito :." %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>


<%@ Register src="../../../General/Controles/fecha.ascx" tagname="fecha" tagprefix="ucFecha" %>


<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <br />
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <br />
    <asp:MultiView ID="mvLineaCredito" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwDatos" runat="server">
            <asp:UpdatePanel ID="updDatos" runat="server">
                <ContentTemplate>
                    <table border="0" cellpadding="1" cellspacing="0" width="100%" style="text-align: left; margin-right: 0px;">
                        <tr>
                            <td colspan="2" style="width: 133px">Código&nbsp;*&nbsp;<br />
                                <asp:TextBox ID="txtCod_linea_credito" runat="server" CssClass="textbox" MaxLength="128" /><br />
                                <asp:RequiredFieldValidator ID="rfvCOD_LINEA_CREDITO" runat="server" ControlToValidate="txtCod_linea_credito"
                                    ErrorMessage="Campo Requerido" SetFocusOnError="True" ValidationGroup="vgGuardar"
                                    ForeColor="Red" Display="Dynamic" />
                            </td>
                            <td colspan="2">Nombre&nbsp;&nbsp;<br />
                                <asp:TextBox ID="txtNombre" runat="server" CssClass="textbox" MaxLength="128" Width="421px" />
                            </td>
                            <td style="width: 78px; text-align: left">
                                <asp:CheckBox ID="chkEstado" runat="server" Text="Activa" TextAlign="Left" />
                                <br />
                            </td>
                            <td>
                                <asp:Button ID="btnParamContable" runat="server" CssClass="btn8" OnClick="btnParamContable_Click" Width="170px"
                                    OnClientClick="btnParamContable_Click" Text="Parametrización Contable" />
                            </td>
                            <td>&nbsp;
                            </td>
                        </tr>
                    </table>
                    <table border="0" cellpadding="1" cellspacing="0" width="100%" style="text-align: left; margin-right: 0px;">
                        <tr>
                            <td class="tdI">Tipo de Linea&nbsp;<br />
                                <asp:DropDownList ID="ddlTipoLinea" runat="server" CssClass="textbox" Width="273px"
                                    AutoPostBack="True" OnSelectedIndexChanged="ddlTipoLinea_SelectedIndexChanged">
                                    <asp:ListItem Value="1">Crédito</asp:ListItem>
                                    <asp:ListItem Value="2">Rotativo</asp:ListItem>
                                    <asp:ListItem Value="3">Leasing</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td class="tdI">Tipo de Liquidacion&nbsp;*<br />
                                <asp:DropDownList ID="ddlTipoLiquidacion" runat="server" CssClass="textbox" Width="162px">
                                </asp:DropDownList>
                            </td>
                            <td class="tdI">Tipo de Moneda<br />
                                <asp:DropDownList ID="ddlCod_moneda" runat="server" CssClass="textbox" Width="176px">
                                </asp:DropDownList>
                            </td>
                            <td class="tdI">Clasificación<br />
                                <asp:DropDownList ID="ddlCod_clasifica" runat="server" CssClass="textbox" Width="117px">
                                </asp:DropDownList>
                            </td>
                            <td class="tdI">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
                            </td>
                        </tr>
                    </table>
                    <table border="0" cellpadding="1" cellspacing="0" style="text-align: left; margin-right: 0px; width: 100%;">
                        <tr>
                            <td class="tdI" colspan="7">
                                <hr />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdI" colspan="6">
                                <strong>Parametros de la Línea de Crédito</strong>
                            </td>
                            <td class="tdI">&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td class="tdI">
                                <asp:CheckBox ID="chkRecogerSaldos" runat="server" Text="Recoger Saldos" TextAlign="Left"
                                    Style="font-size: x-small" />
                            </td>
                            <td class="tdI">
                                <asp:CheckBox ID="chkCobraMora" runat="server" Text="Cobra Mora" TextAlign="Left"
                                    Style="font-size: x-small" />
                            </td>
                            <td class="tdI">
                                <asp:CheckBox ID="chkModificaDatos" runat="server" Text="Maneja Condiciones Por Cada Crédito"
                                    TextAlign="Left" Style="font-size: x-small" />
                            </td>
                            <td class="tdI">
                                <asp:CheckBox ID="chkModifica_fecha_pago" runat="server" Text="Permite Establecer Fecha de Primer Pago"
                                    TextAlign="Left" Style="font-size: x-small" />
                            </td>
                            <td class="tdI" colspan="2">
                                <asp:CheckBox ID="chkCuotas_extras" runat="server" Text="Maneja Cuotas Extraordinarias"
                                    TextAlign="Left" Style="font-size: x-small" />
                            </td>
                            <td style="width: 170px">
                                <asp:CheckBox ID="chkManejaExcepcion" runat="server" Text="Maneja Excepciones" TextAlign="Left"
                                    Style="font-size: x-small" />
                            </td>
                            <td class="tdI">&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:CheckBox runat="server" ID="chkAporteGarantia" Text="Aportes como garantia admisible" TextAlign="Left" style="font-size:x-small" />
                            </td>
                        </tr>
                    </table>
                    <table border="0" cellpadding="1" cellspacing="0" style="text-align: left; margin-right: 0px; width: 100%;">
                        <tr>
                            <td style="margin-left: 40px">
                                <span style="font-size: x-small">
                                    <asp:CheckBox ID="chkDiferir" runat="server" Style="font-size: x-small" Text="Diferir" TextAlign="Left" AutoPostBack="true"
                                         OnCheckedChanged="chkDiferir_CheckedChanged" />
                                    <asp:Label ID="Lblplazodiferir" runat="server"  Visible="false" Text="Plazo a diferir"></asp:Label>
                                    <asp:TextBox ID="txtplazodiferir" runat="server"   Visible="false"  CssClass="textbox" MaxLength="128" Enabled="false"
                                        Width="98px" />
                                </span>
                            </td>
                            <td colspan="2">
                                <span style="font-size: x-small">
                                <asp:Label ID="lblfechacorte" runat="server" Text="Fecha de Corte" Visible="false"></asp:Label><br />
                                <ucFecha:fecha ID="txtFechaCorte" runat="server" CssClass="textbox" />
                            </td>
                            <td>
                                <asp:CheckBox ID="chkAvancesAprob" runat="server" Style="font-size: x-small" Text="Avances requiere Aprobación" TextAlign="Left" />
                            </td>
                            <td>
                                <asp:CheckBox ID="ChkDeseAhorros" runat="server" Style="font-size: x-small" Text="Desembolsar a Cuenta de Ahorros" TextAlign="Left" />
                            </td>
                            <td><span style="font-size: x-small">
                                <asp:Label ID="Lblcantidadcomision" runat="server" Text="Cobro Comisión" Visible="false"></asp:Label>
                                <asp:TextBox ID="txtcantidadcomision" runat="server" CssClass="textbox"  MaxLength="128" Visible="false" Width="98px" />
                            </td>
                            <td><span style="font-size: x-small">
                                <asp:Label ID="LblValorComision" runat="server" Text="Valor Comisión" Visible="false"></asp:Label>
                                <asp:TextBox ID="TxtValorComision" runat="server" CssClass="textbox" MaxLength="128" Visible="false" Width="98px" />
                               </span>
                            </td>                            
                            <td><span style="font-size: x-small">                                
                                <asp:DropDownList ID="ddlSigno" runat="server" CssClass="textbox" Visible="false">
                                    <asp:ListItem Value="0">$</asp:ListItem>
                                </asp:DropDownList>
                                </span>
                            </td>                            
                            <td></td>
                        </tr>
                    </table>
                    <table border="0" cellpadding="1" cellspacing="0" width="100%" style="text-align: left; margin-right: 0px;">
                        <tr>
                            <td style="text-align: left;">
                                <asp:CheckBox ID="chkCredGenerencial" runat="server" Text="Es Crédito Gerencial"
                                    TextAlign="Left" Style="font-size: x-small" />
                            </td>
                            <td style="text-align: left">
                                <asp:CheckBox ID="chkEducativo" runat="server" Text="Es Crédito Educativo" TextAlign="Left"
                                    Style="font-size: x-small" AutoPostBack="true" OnCheckedChanged="chkEducativo_CheckedChanged" />
                                <br />
                                <asp:CheckBox ID="chkManejaAuxilio" runat="server" Text="Maneja Auxilio" TextAlign="Left"
                                    Style="font-size: x-small" Visible="false" />
                            </td>
                            <td>
                                <span style="font-size: x-small">#Min.Codeudores Requer.<br />
                                    <asp:TextBox ID="txtNumero_codeudores" runat="server" CssClass="textbox" MaxLength="10" Width="90px" />
                                    <asp:MaskedEditExtender ID="mskNumero_codeudores" runat="server" ErrorTooltipEnabled="True"
                                        InputDirection="RightToLeft" Mask="9" MaskType="Number" MessageValidatorTip="true"
                                        OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" TargetControlID="txtNumero_codeudores" />
                                </span>
                            </td>
                            <td>
                                <span style="font-size: x-small">Tipo de Capitalizacion<br />
                                    <asp:DropDownList ID="ddlTipo_capitalizacion" runat="server" CssClass="textbox" Width="150px">
                                        <asp:ListItem Value="0">No Maneja</asp:ListItem>
                                        <asp:ListItem Value="1">Según Reciprocidad</asp:ListItem>
                                    </asp:DropDownList>
                                </span>
                            </td>
                            <td>
                                <span style="font-size: x-small">Tipo de Cupo</span><br />
                                <asp:DropDownList ID="txtTipo_cupo" runat="server" CssClass="textbox" Width="130px" />
                            </td>
                            <td>
                                <span style="font-size: x-small">Tipo de Garantia Requerida</span><br />
                                <asp:DropDownList ID="ddlGarantia_requerida" runat="server" CssClass="textbox" Width="120px">
                                    <asp:ListItem Value="0">Personal</asp:ListItem>
                                    <asp:ListItem Value="1">Real</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <span style="font-size: x-small">Crédito por Línea<br />
                                    <asp:TextBox ID="txtCredXlinea" runat="server" CssClass="textbox" MaxLength="5" Width="70px" />
                                    <asp:FilteredTextBoxExtender ID="ftb12" runat="server" Enabled="True" FilterType="Numbers, Custom"
                                        TargetControlID="txtCredXlinea" ValidChars="" />
                                </span>
                            </td>
                        </tr>
                    </table>
                    <hr />
                    <table border="0" cellpadding="1" cellspacing="0" style="text-align: left; margin-right: 0px; width: 60%;">
                        <tr>
                            <td style="text-align: left;" colspan="5">
                                <strong>Tipo Persona al que aplica</strong>
                            </td>
                            <td style="text-align: left;">&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left;">
                                <asp:CheckBox ID="chktercero" runat="server" Text="Tercero" TextAlign="Left" Style="font-size: x-small" />
                            </td>
                            <td style="text-align: left;">
                                <asp:CheckBox ID="chkasociado" runat="server" Text="Asociado" TextAlign="Left" Style="font-size: x-small" />
                            </td>
                            <td style="text-align: left;">
                                <asp:CheckBox ID="chkempleado" runat="server" Text="Empleado" TextAlign="Left" Style="font-size: x-small" />
                            </td>
                            <td style="text-align: left">Destinaciones Asociadas:                                            
                            </td>
                            <td style="text-align: left">
                                <asp:UpdatePanel ID="upRecoger" runat="server">
                                    <ContentTemplate>
                                        <asp:HiddenField ID="hfValue" runat="server" Visible="false" />
                                        <asp:TextBox ID="txtRecoger" CssClass="textbox" runat="server" Width="145px" ReadOnly="True" Style="text-align: right" TEXTMODE="SingleLine"></asp:TextBox>
                                        <asp:PopupControlExtender ID="txtRecoger_PopupControlExtender" runat="server"
                                            Enabled="True" ExtenderControlID="" TargetControlID="txtRecoger"
                                            PopupControlID="panelLista" OffsetY="22">
                                        </asp:PopupControlExtender>
                                        <asp:Panel ID="panelLista" runat="server" Height="200px" Width="300px"
                                            BorderStyle="Solid" BorderWidth="2px" Direction="LeftToRight"
                                            ScrollBars="Auto" BackColor="#CCCCCC" Style="display: none">
                                            <asp:GridView ID="gvRecoger" runat="server" Width="100%" AutoGenerateColumns="False" 
                                                 HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager" RowStyle-CssClass="gridItem" DataKeyNames="cod_destino">

                                                <Columns>
                                                    <asp:TemplateField HeaderText="Código">
                                                        <itemtemplate>
                                                    <asp:Label ID="lbl_destino" runat="server" Text='<%# Bind("cod_destino") %>'></asp:Label>
                                                    </itemtemplate>
                                                        <ItemStyle HorizontalAlign="Left" Width="50px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Descripción">
                                                          <itemtemplate>
                                                    <asp:Label ID="lbl_descripcion" runat="server" Text='<%# Bind("descripcion") %>'></asp:Label>
                                                    </itemtemplate>                                                        
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-CssClass="gridIco">   
                                                        <ItemTemplate>
                                                            <cc1:CheckBoxGrid ID="cbListado" runat="server" CommandArgument='<%#DataBinder.Eval(Container, "RowIndex") %>'
                                                            AutoPostBack="false" />
                                                        </ItemTemplate>
                                                        <HeaderStyle CssClass="gridIco"></HeaderStyle>
                                                    </asp:TemplateField>
                                                </Columns>  
                                            </asp:GridView>
                                        </asp:Panel>
                                    </ContentTemplate>                                
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                    </table>
                    </td>
                        </tr>
                    </table>
                    <hr />
                    <table border="0" cellpadding="1" cellspacing="0" width="100%" style="text-align: left; margin-right: 0px;">
                        <tr>
                            <td class="tdI" colspan="4">
                                <strong>Parámetros de Refinanciación</strong>
                            </td>
                            <td class="tdI" colspan="3">
                                <strong>Otros Parámetros</strong>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left; width: 250px;">Tipo Refinanciación<br />
                                <asp:DropDownList ID="ddlTipoRefinanciacion" runat="server" CssClass="textbox" Width="215px"
                                    AutoPostBack="True" OnSelectedIndexChanged="ddlTipoRefinanciacion_SelectedIndexChanged">
                                    <asp:ListItem Value="0">Rango Saldo</asp:ListItem>
                                    <asp:ListItem Value="2">% Saldo</asp:ListItem>
                                    <asp:ListItem Value="3">% Cuotas Pagas</asp:ListItem>
                                    <asp:ListItem Value="1">Otro</asp:ListItem>
                                </asp:DropDownList>
                                <br />
                            </td>
                            <td colspan="3">
                                <asp:Panel ID="panelValores" runat="server">
                                    <table>
                                        <tr>
                                            <td style="text-align: left; width: 149px">Valor Mínimo<br />
                                                <asp:TextBox ID="txtMinimo_refinancia" runat="server" CssClass="textbox" MaxLength="128" />
                                                <asp:MaskedEditExtender runat="server" AcceptNegative="Left" DisplayMoney="Left"
                                                    ErrorTooltipEnabled="True" InputDirection="RightToLeft" Mask="999,999,999" MaskType="Number"
                                                    MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError"
                                                    TargetControlID="txtMinimo_refinancia" ID="MaskedEditExtender1" />
                                            </td>
                                            <td style="text-align: left; width: 149px">Valor Máximo<br />
                                                <asp:TextBox ID="txtMaximo_refinancia" runat="server" CssClass="textbox" MaxLength="128" />
                                                <asp:MaskedEditExtender ID="mskMaximo_refinancia" runat="server" AcceptNegative="Left"
                                                    DisplayMoney="Left" ErrorTooltipEnabled="True" InputDirection="RightToLeft" Mask="999,999,999"
                                                    MaskType="Number" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus"
                                                    OnInvalidCssClass="MaskedEditError" TargetControlID="txtMaximo_refinancia" />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <asp:Panel ID="panelPorcentaje" runat="server">
                                    <table>
                                        <tr>
                                            <td style="text-align: left; width: 149px">% Mínimo<br />
                                                <asp:TextBox ID="txtPorcent_Mini" runat="server" CssClass="textbox" MaxLength="6" />
                                                <asp:FilteredTextBoxExtender ID="ftemini" runat="server" Enabled="True" FilterType="Numbers, Custom"
                                                    TargetControlID="txtPorcent_Mini" ValidChars="," />
                                            </td>
                                            <td style="text-align: left; width: 149px">% Máximo<br />
                                                <asp:TextBox ID="txtPorcent_Maxi" runat="server" CssClass="textbox" MaxLength="6" />
                                                <asp:FilteredTextBoxExtender ID="ftemaxi" runat="server" Enabled="True" FilterType="Numbers, Custom"
                                                    TargetControlID="txtPorcent_Maxi" ValidChars="," />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                            <td style="text-align: left;">Cuota Int. Ajuste<br />
                                <asp:TextBox ID="txtCuotaIntAjuste" runat="server" CssClass="textbox" />
                                <asp:FilteredTextBoxExtender ID="fteCuota" runat="server" Enabled="True" FilterType="Numbers, Custom"
                                    TargetControlID="txtCuotaIntAjuste" />
                            </td>
                            <td style="text-align: left;">
                                <br />
                                <asp:CheckBox ID="cbOrdenSErvicio" runat="server" Text="Orden de Servicio" TextAlign="Left" AutoPostBack="true"
                                    Style="font-size: x-small" OnCheckedChanged="cbOrdenSErvicio_CheckedChanged" />
                            </td>
                            <td style="text-align: left;">Prioridad<br />
                                <asp:TextBox ID="txtPrioridad" runat="server" CssClass="textbox" MaxLength="10" Width="60px" />
                                <asp:MaskedEditExtender ID="meePrioridad" runat="server" ErrorTooltipEnabled="True"
                                    InputDirection="RightToLeft" Mask="99" MaskType="Number" MessageValidatorTip="true"
                                    OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" TargetControlID="txtPrioridad" />
                            </td>
                        </tr>
                    </table>
                    <table border="0" cellpadding="1" cellspacing="0" width="100%" style="text-align: left; margin-right: 0px;">
                        <tr>
                            <td class="tdI" colspan="3">
                                <hr />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdI">
                                <table border="0" cellpadding="1" cellspacing="0" width="100%" style="text-align: left; margin-right: 0px;">
                                    <tr>
                                        <td class="tdI" colspan="6">
                                            <strong>Parámetros del Período de Gracía</strong>
                                        </td>
                                        <td class="tdI">&nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdI">
                                            Maneja Periodo Gracia
                                            <br />
                                            <asp:DropDownList ID="ddlTipoGracia" runat="server" CssClass="textbox" Width="140px"
                                                OnSelectedIndexChanged="ddlTipoGracia_SelectedIndexChanged" AutoPostBack="true">
                                                <asp:ListItem Value="0" Text="No Maneja" />
                                                <asp:ListItem Value="1" Text="Fijo" />
                                                <asp:ListItem Value="2" Text="Capital al Final" />
                                                <asp:ListItem Value="3" Text="Por Meses" />
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblMeses" runat="server" Text="Meses Gracia" />
                                            <br />
                                            <asp:UpdatePanel ID="upPeriodicidad" runat="server">
                                                <ContentTemplate>
                                                    <asp:HiddenField ID="hfMeses" runat="server" Visible="false" />
                                                    <asp:TextBox ID="txtMeses" CssClass="textbox" runat="server" Width="80px" ReadOnly="True" Style="text-align: left" TEXTMODE="SingleLine"></asp:TextBox>
                                                    <asp:PopupControlExtender ID="pceMeses" runat="server"
                                                        Enabled="True" ExtenderControlID="" TargetControlID="txtMeses"
                                                        PopupControlID="pMeses" OffsetY="22">
                                                    </asp:PopupControlExtender>
                                                    <asp:Panel ID="pMeses" runat="server" Height="270px" Width="180px"
                                                        BorderStyle="Solid" BorderWidth="2px" Direction="LeftToRight"
                                                        ScrollBars="Auto" BackColor="#CCCCCC" Style="display: none">
                                                        <asp:GridView ID="gvMeses" runat="server" Width="100%" AutoGenerateColumns="False" 
                                                                HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager" RowStyle-CssClass="gridItem" DataKeyNames="mes">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Mes">
                                                                    <itemtemplate>
                                                                        <asp:Label ID="lbl_mes" runat="server" Text='<%# Bind("mes") %>'></asp:Label>
                                                                    </itemtemplate>
                                                                    <ItemStyle HorizontalAlign="Left" Width="50px" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Nombre">
                                                                    <itemtemplate>
                                                                        <asp:Label ID="lbl_nombre" runat="server" Text='<%# Bind("nombre") %>'></asp:Label>
                                                                    </itemtemplate>                                                        
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderStyle-CssClass="gridIco">   
                                                                    <ItemTemplate>
                                                                        <cc1:CheckBoxGrid ID="cbListado" runat="server" CommandArgument='<%#DataBinder.Eval(Container, "RowIndex") %>' AutoPostBack="true" OnCheckedChanged="cbListado_CheckedChanged" />
                                                                    </ItemTemplate>
                                                                    <HeaderStyle CssClass="gridIco"></HeaderStyle>
                                                                </asp:TemplateField>
                                                            </Columns>  
                                                        </asp:GridView>
                                                    </asp:Panel>
                                                </ContentTemplate>                                
                                            </asp:UpdatePanel>
                                        </td>
                                        <td class="tdI" style="width: 149px; text-align: left">Periodo Gracia
                                            <br />
                                            <asp:TextBox ID="txtPeriodo_gracia" runat="server" CssClass="textbox" MaxLength="128" />
                                            <asp:MaskedEditExtender ID="mskPeriodo_gracia" runat="server" ErrorTooltipEnabled="True"
                                                InputDirection="RightToLeft" Mask="99999" MaskType="Number" MessageValidatorTip="true"
                                                OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" TargetControlID="txtPeriodo_gracia" />
                                        </td>
                                        <td class="tdI" colspan="2">Periodicidad Gracia<br />
                                            <asp:DropDownList ID="ddlPeriodicidadGracia" runat="server" CssClass="textbox" Width="152px">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td>
                                <table border="0" cellpadding="1" cellspacing="0" width="100%" style="text-align: left; margin-right: 0px;">
                                    <tr>
                                        <td class="tdI" colspan="6">
                                            <strong>Parámetros de Amortización</strong>&nbsp;
                                        </td>
                                        <td class="tdI">&nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdI">Tipo de Amortización<br />
                                            <asp:DropDownList ID="ddlTipo_amortiza" runat="server" CssClass="textbox" Width="143px">
                                                <asp:ListItem Value="0">Normal</asp:ListItem>
                                                <asp:ListItem Value="1">Corto/Largo Plazo</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td class="tdI" colspan="2">% Amortiza al Corto Plazo<br />
                                            <asp:TextBox ID="txtPorc_corto" runat="server" CssClass="textbox" MaxLength="128" />
                                            <asp:MaskedEditExtender ID="mskPorc_corto" runat="server" ErrorTooltipEnabled="True"
                                                InputDirection="RightToLeft" Mask="999" MaskType="Number" MessageValidatorTip="true"
                                                OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" TargetControlID="txtPorc_corto" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td></td>
                        </tr>
                    </table>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddlTipoLinea" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="ddlTipoRefinanciacion" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="ddlTipoGracia" EventName="SelectedIndexChanged" />
                </Triggers>
            </asp:UpdatePanel>
            <table border="0" cellpadding="1" cellspacing="0" style="text-align: left; margin-right: 0px; width: 100%;">
                <tr>
                    <td class="tdI" colspan="6">
                        <strong>Parámetros de Componentes</strong>&nbsp;
                        <br />
                    </td>
                    <td class="tdI">&nbsp;
                    </td>
                </tr>
            </table>
            <asp:TabContainer ID="tcInfFinNeg" runat="server" ActiveTabIndex="0" Width="95%"
                Style="text-align: left" ScrollBars="Auto" Height="330px"
                CssClass="CustomTabStyle">
                <asp:TabPanel runat="server" HeaderText="Atributos" ID="TabAtributos" ScrollBars="Vertical">
                    <ContentTemplate>
                        <table width="100%">
                            <tr>
                                <td>&nbsp;&nbsp;
                                    <asp:ImageButton ID="btnnuevoatributos" runat="server" ImageUrl="~/Images/btnNuevo.jpg"
                                        OnClick="btnnuevoatributos_Click"></asp:ImageButton>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:GridView ID="gvAtributos" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                        PageSize="8" Width="50%" OnRowEditing="gvAtributos_RowEditing" DataKeyNames="cod_rango_atr"
                                        OnRowDeleting="gvAtributos_RowDeleting" OnPageIndexChanging="gvAtributos_PageIndexChanging">
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="btnEditar" runat="server" CommandName="Edit" ImageUrl="~/Images/gr_edit.jpg"
                                                        ToolTip="Modificar" />
                                                </ItemTemplate>
                                                <HeaderStyle CssClass="gridIco" />
                                                <ItemStyle CssClass="gridIco" />
                                            </asp:TemplateField>
                                            <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/gr_elim.jpg" ShowDeleteButton="True" />
                                            <asp:BoundField DataField="cod_rango_atr" HeaderText="Codigo" />
                                            <asp:BoundField DataField="nombre" HeaderText="Descripción" />
                                        </Columns>
                                        <HeaderStyle CssClass="gridHeader" />
                                        <PagerStyle CssClass="gridPager" />
                                        <RowStyle CssClass="gridItem" />
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:TabPanel>
                <asp:TabPanel ID="TabDeducciones" runat="server" HeaderText="Deducciones" Width="100%">
                    <ContentTemplate>
                        <table width="100%">
                            <tr>
                                <td>&nbsp;&nbsp;
                                    <asp:ImageButton ID="btnnuevodeduccion" runat="server" ImageUrl="~/Images/btnNuevo.jpg"
                                        OnClick="btnnuevodeduccion_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                        <ContentTemplate>
                                            <asp:GridView ID="gvdeducciones" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                                PageSize="10" Width="100%" OnRowEditing="gvdeducciones_RowEditing" Style="font-size: x-small"
                                                DataKeyNames="cod_atr" OnRowDeleting="gvdeducciones_RowDeleting">
                                                <Columns>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="btnEditar0" runat="server" CommandName="Edit" ImageUrl="~/Images/gr_edit.jpg"
                                                                ToolTip="Modificar" />
                                                        </ItemTemplate>
                                                        <HeaderStyle CssClass="gridIco" />
                                                        <ItemStyle CssClass="gridIco" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="btnBorrar0" runat="server" CommandName="Delete" ImageUrl="~/Images/gr_elim.jpg"
                                                                ToolTip="Borrar" />
                                                        </ItemTemplate>
                                                        <HeaderStyle CssClass="gridIco" />
                                                        <ItemStyle CssClass="gI" />
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="cod_atr" HeaderText="Cod Atr" />
                                                    <asp:BoundField DataField="descripcion" HeaderText="Descripcion" />
                                                    <asp:BoundField DataField="tipoliquidacion" HeaderText="Tipo Liquidación" />
                                                    <asp:BoundField DataField="tiposdescuento" HeaderText="Tipos Descuento" />
                                                    <asp:BoundField DataField="Formadescuento" HeaderText="Forma Descuento" />
                                                    <asp:BoundField DataField="valor" HeaderText="Valor" />
                                                    <asp:BoundField DataField="numero_cuotas" HeaderText="Numero Cuotas" />
                                                    <asp:BoundField DataField="tipoimpuesto" HeaderText="Tipo Impuesto" />
                                                    <asp:TemplateField HeaderText="Cobra Mora">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkGenerar" runat="server" Checked='<%#Convert.ToBoolean(Eval("cobra_mora")) %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Modificable">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="ChkModifica" runat="server" Checked='<%#Convert.ToBoolean(Eval("modifica")) %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <HeaderStyle CssClass="gridHeader" />
                                                <PagerStyle CssClass="gridPager" />
                                                <RowStyle CssClass="gridItem" />
                                            </asp:GridView>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:TabPanel>
                <asp:TabPanel runat="server" ID="tblDocumentos" HeaderText="Documentos" Width="100%">
                    <ContentTemplate>
                        <asp:UpdatePanel ID="UdpDocumentos" runat="server">
                            <ContentTemplate>
                                <strong>Documentos Requeridos</strong><br />
                                <asp:GridView ID="gvRequeridoDoc" runat="server" AutoGenerateColumns="False" Width="100%"
                                    Style="font-size: small" DataKeyNames="">
                                    <Columns>
                                        <asp:BoundField DataField="tipo_documento" HeaderText="Tipo de Documento">
                                            <ItemStyle Width="15%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="descripcion" HeaderText="Descripción" />
                                        <asp:TemplateField HeaderText="Aplica Codeudor" HeaderStyle-HorizontalAlign="center">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkAplicaCod" runat="server" Checked='<%#Convert.ToBoolean(Convert.ToInt32(Eval("aplica_codeudor"))) %>' />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" Width="15%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Seleccionar" HeaderStyle-HorizontalAlign="center">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkAplica" runat="server" Checked='<%#Convert.ToBoolean(Eval("checkbox")) %>' />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" Width="15%" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <HeaderStyle CssClass="gridHeader" />
                                    <PagerStyle CssClass="gridPager" />
                                    <RowStyle CssClass="gridItem" />
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </ContentTemplate>
                </asp:TabPanel>
                <asp:TabPanel runat="server" ID="TabRequeridos" HeaderText="Documentos Garantía"
                    Width="100%">
                    <ContentTemplate>
                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                            <ContentTemplate>
                                <strong>Documentos Garantía</strong><br />
                                <asp:Button ID="btnAgregar" runat="server" CssClass="btn8" OnClick="btnAgregar_Click"
                                    OnClientClick="btnAgregar_Click" Text="+ Adicionar Detalle" />
                                <asp:GridView ID="gvGarantiaDoc" runat="server" AutoGenerateColumns="False" Style="font-size: x-small"
                                    DataKeyNames="tipo_documento" OnRowDataBound="gvGarantiaDoc_RowDataBound" OnRowCommand="gvGarantiaDoc_RowCommand">
                                    <Columns>
                                        <%--<asp:TemplateField>
                                         <ItemTemplate>
                                                 <asp:ImageButton ID="btnEliminar" runat="server" 
                                                     CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" 
                                                     CommandName="Delete" ImageUrl="~/Images/gr_elim.jpg" ToolTip="Eliminar" 
                                                     Width="16px" />
                                             </ItemTemplate>
                                             <HeaderStyle CssClass="gridIco" />
                                         </asp:TemplateField>--%>
                                        <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/gr_elim.jpg" ShowDeleteButton="True">
                                            <ItemStyle Width="16px" />
                                        </asp:CommandField>
                                        <asp:TemplateField HeaderText="Tipo de Documento">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTipoDoc" runat="server" Text='<%# Bind("tipo_documento") %>' Visible="false" />
                                                <cc1:DropDownListGrid ID="ddlTipoDoc" runat="server" CssClass="textbox" Width="200px"
                                                    CommandArgument="<%#Container.DataItemIndex %>" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Requerido" HeaderStyle-HorizontalAlign="center">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkRequerido" runat="server" Checked='<%#Convert.ToBoolean(Eval("requerido")) %>' />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Plantilla">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtPlantilla" runat="server" CssClass="textbox" Width="200px" Text='<%# Bind("plantilla") %>' Enabled="false" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Empresa Recaudo(Pagaduria)">
                                            <ItemTemplate>
                                                <asp:Label ID="lbltipoPagaduria" runat="server" Visible="false" />
                                                <cc1:DropDownListGrid ID="ddlpagaduria" runat="server" CssClass="textbox" Width="350px"
                                                    CommandArgument="<%#Container.DataItemIndex %>" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle CssClass="gridHeader" />
                                    <HeaderStyle CssClass="gridHeader" />
                                    <PagerStyle CssClass="gridPager" />
                                    <RowStyle CssClass="gridItem" />
                                </asp:GridView>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="gvGarantiaDoc" EventName="RowDataBound" />
                            </Triggers>
                        </asp:UpdatePanel>

                    </ContentTemplate>
                </asp:TabPanel>
                <asp:TabPanel runat="server" ID="tblProcesos" HeaderText="Procesos">
                    <ContentTemplate>
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <strong>Procesos</strong><br />
                                <asp:GridView ID="gvProcesos" runat="server" AutoGenerateColumns="False" Width="100%"
                                    Style="font-size: small" OnRowDataBound="gvProcesos_RowDataBound">
                                    <Columns>
                                        <asp:BoundField DataField="codtipoproceso" HeaderText="Código">
                                            <ItemStyle Width="15%" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Descripción">
                                            <ItemTemplate>
                                                <asp:Label ID="lbldescripcion" runat="server" Text='<%# Bind("descripcion") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="center" HeaderText="Seleccionar">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkSeleccion" runat="server" Checked='<%#Convert.ToBoolean(Eval("checkbox")) %>' />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" Width="15%" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <HeaderStyle CssClass="gridHeader" />
                                    <PagerStyle CssClass="gridPager" />
                                    <RowStyle CssClass="gridItem" />
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </ContentTemplate>
                </asp:TabPanel>
                <asp:TabPanel runat="server" ID="tblPrioridades" HeaderText="Prioridades">
                    <ContentTemplate>
                        <asp:UpdatePanel ID="updPrioridad" runat="server">
                            <ContentTemplate>
                                <br />
                                <asp:GridView ID="gvPrioridad" runat="server" AllowPaging="false" AutoGenerateColumns="False"
                                    PageSize="5" Width="50%" OnRowEditing="gvAtributos_RowEditing" DataKeyNames="cod_atr"
                                    Style="font-size: small" OnRowDeleting="gvAtributos_RowDeleting">
                                    <Columns>
                                        <asp:BoundField DataField="cod_atr" HeaderText="Codigo" />
                                        <asp:BoundField DataField="nombre" HeaderText="Descripción" />
                                        <asp:TemplateField HeaderText="Prioridad">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtPrioridad" runat="server" Width="80px" CssClass="textbox" Text='<%# Bind("numero") %>' />
                                                <asp:FilteredTextBoxExtender ID="ftemaxi" runat="server" Enabled="True" FilterType="Numbers"
                                                    TargetControlID="txtPrioridad" />
                                            </ItemTemplate>
                                            <ItemStyle CssClass="gridIco" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <HeaderStyle CssClass="gridHeader" />
                                    <PagerStyle CssClass="gridPager" />
                                    <RowStyle CssClass="gridItem" />
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </ContentTemplate>
                </asp:TabPanel>
                 <asp:TabPanel runat="server" ID="TabPanel1" HeaderText="Parametros">
                    <ContentTemplate>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <br />
                                <asp:GridView ID="gvParamtros" runat="server" AllowPaging="false" AutoGenerateColumns="False"
                                    PageSize="5" Width="50%" OnRowEditing="gvAtributos_RowEditing" DataKeyNames="codtipoproceso"
                                    Style="font-size: small" OnRowDeleting="gvAtributos_RowDeleting">
                                    <Columns>
                                        <asp:BoundField DataField="codtipoproceso" HeaderText="Codigo" />
                                        <asp:BoundField DataField="descripcion" HeaderText="Descripción" />
                                        <asp:TemplateField HeaderText="Valor">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtValor" runat="server" Width="80px" CssClass="textbox" Text='<%# Bind("cod_lineacredito") %>' />
                                                <asp:FilteredTextBoxExtender ID="ftemaxi" runat="server" Enabled="True" FilterType="Numbers, UppercaseLetters, LowercaseLetters"
                                                    TargetControlID="txtValor" />
                                            </ItemTemplate>
                                            <ItemStyle CssClass="gridIco" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <HeaderStyle CssClass="gridHeader" />
                                    <PagerStyle CssClass="gridPager" />
                                    <RowStyle CssClass="gridItem" />
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </ContentTemplate>
                </asp:TabPanel>
            </asp:TabContainer>
        </asp:View>
        <asp:View ID="vwFinal" runat="server">
            <asp:Panel ID="PanelFinal" runat="server">
                <table style="width: 100%; text-align: center">
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <asp:Label ID="lblMensajeGrabar" runat="server" Text="Datos Grabados Correctamente"></asp:Label><br />
                            <asp:Button ID="btnAceptar" runat="server" Text="Regresar" OnClick="btnAceptar_Click" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </asp:View>
    </asp:MultiView>
    <uc4:mensajegrabar ID="ctlMensaje" runat="server" />


</asp:Content>

