<%@ Page MaintainScrollPositionOnPostback="true" Title="" Language="C#" MasterPageFile="~/General/Master/solicitud.master" AutoEventWireup="true" CodeFile="SolicitudCredito.aspx.cs" Inherits="Detalle" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/direccion.ascx" TagName="direccion" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc2" %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="uc3" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>
<%@ Register Src="~/General/Controles/ctlProveedor.ascx" TagName="BuscarProveedor" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/ctlValidarBiometria.ascx" TagName="validarBiometria" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/ctlBusquedaRapida.ascx" TagName="ListadoPersonas" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/ctlCuotasExtras.ascx" TagName="ctrCuotasExtras" TagPrefix="uc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <script src="../../../../Scripts/PCLBryan.js"></script>
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td>
                <asp:UpdatePanel ID="updSolicitud" runat="server">
                    <ContentTemplate>
                        <table cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td class="tdI" style="text-align: center">
                                    <table style="width: 100%;">
                                        <tr>
                                            <td style="text-align: center; color: #FFFFFF; background-color: #0066FF; height: 20px;"
                                                colspan="3">&nbsp;<strong style="text-align: center">SOLICITUD DE CREDITO</strong>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="margin-left: 120px">Número de Solicitud *<br />
                                                <asp:TextBox ID="lblNumero" runat="server" CssClass="textbox" Enabled="False" Width="153px"></asp:TextBox>
                                                <asp:TextBox ID="txtCliente" runat="server" CssClass="textbox" Visible="False" Width="153px"></asp:TextBox>
                                            </td>
                                            <td>Fecha de Solicitud *<br />
                                                <asp:TextBox ID="lblFecha" runat="server" CssClass="textbox" Enabled="False" Width="153px"></asp:TextBox>
                                            </td>
                                            <td>Oficina<br />
                                                <asp:TextBox ID="lblOficina" runat="server" CssClass="textbox" Enabled="False" Width="153px"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td class="tdI" style="text-align: right">
                                    <table cellpadding="0" cellspacing="0" style="width: 100%; text-align: left;">
                                        <tr>
                                            <td align="left" colspan="3">
                                                <asp:Label ID="lblMensajeValidacion" runat="server" Style="font-size: xx-small; color: #FF0000; text-align: left"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" colspan="3" style="color: #FFFFFF; background-color: #0066FF; height: 20px;">
                                                <strong>Condiciones Solicitadas Del Crédito</strong>&nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Tipo de Crédito<br />
                                                <asp:DropDownList ID="ddlTipoCredito" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlTipoCredito_TextChanged"
                                                    OnTextChanged="ddlTipoCredito_TextChanged" Width="250px" CssClass="textbox">
                                                </asp:DropDownList>
                                            </td>
                                            <td>Monto Máximo<br />
                                                <asp:TextBox ID="txtMontoMaximoMostrar" runat="server" CssClass="textbox" Enabled="False"
                                                    Width="153px"></asp:TextBox>
                                                <asp:TextBox ID="txtMontoMaximo" runat="server" CssClass="textbox" Enabled="False" Visible="false"
                                                    Width="153px"></asp:TextBox>
                                            </td>
                                            <td>Plazo Máximo<br />
                                                <asp:TextBox ID="txtPlazoMaximo" runat="server" CssClass="textbox" Enabled="False"
                                                    Width="136px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Monto Solicitado<br />
                                                <uc2:decimales ID="txtMonto" runat="server" AutoPostBack_="true" OneventoCambiar="txtMonto_eventoCambiar" Width="153px" />
                                            </td>
                                            <td>Plazo<br />
                                                <uc2:decimales ID="txtPlazo" runat="server" AutoPostBack_="false" Width="153px" OneventoCambiar="txtMonto_eventoCambiar"/>
                                                <br />
                                            </td>
                                            <td>
                                                <asp:Label ID="LblPerio" runat="server" Text="Periodicidad" />&nbsp;&nbsp;
                                                <asp:DropDownList ID="ddlPeriodicidad" runat="server" CssClass="textbox" Width="148px" OnSelectedIndexChanged="ddlPeriodicidad_OnSelectedIndexChanged" AutoPostBack="True">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td># Periodo de Gracia<br />
                                                <uc2:decimales ID="txtPeriodo" runat="server" AutoPostBack_="false" Width="153px" />
                                            </td>
                                            <td>Cuota Deseada<br />
                                                <uc2:decimales ID="txtCuota" runat="server" AutoPostBack_="false" Width="153px" />
                                            </td>
                                            <td>Medio por el Cual se Entero de la Entidad<br />
                                                <asp:DropDownList ID="ddlMedio" runat="server" AutoPostBack="true" CssClass="textbox"
                                                    OnSelectedIndexChanged="ddlMedio_SelectedIndexChanged" Width="100px">
                                                </asp:DropDownList>
                                                <asp:Label ID="lblCual" runat="server" Text="¿Cual?"></asp:Label>
                                                <asp:TextBox ID="txtOtro" runat="server" CssClass="textbox" MaxLength="20" Width="80px"></asp:TextBox>
                                            </td>

                                        </tr>
                                        <tr>
                                            <td colspan="2">Observaciones<br />
                                                <asp:TextBox ID="txtConcepto" runat="server" CssClass="textbox" MaxLength="4000" Width="500px" AutoPostBack_="false"></asp:TextBox>
                                            </td>
                                            <td>Destino del Crédito
                                                <br />
                                                <asp:DropDownList ID="ddlDestino" runat="server" CssClass="textbox" Width="225px" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:CheckBox ID="Checkgarantia_real" runat="server" />
                                                <asp:Label ID="Label1" runat="server" Text=" ¿Requiere Garantía Real?"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="Checkgarantia_comunitaria" runat="server" Enabled="False" />
                                                <asp:Label ID="Label2" runat="server" Text=" ¿Fondo de Garantías?"></asp:Label>
                                            </td>

                                            <td>
                                                <asp:CheckBox ID="Checkpoliza" runat="server" />
                                                <asp:Label ID="Label3" runat="server" Text=" ¿Desea tomar Poliza Microseguros?"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:CheckBox ID="CkcAfiancol" runat="server" Enabled="True" OnCheckedChanged="CkcAfiancol_OnCheckedChanged" AutoPostBack="True" />
                                                <asp:Label ID="Label5" runat="server" Text="Afiancol "></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Asesor Comercial<br />
                                                <asp:DropDownList ID="Ddlusuarios" runat="server" CssClass="textbox" Width="260px">
                                                </asp:DropDownList>
                                                <span style="font-size: xx-small">
                                                    <asp:RequiredFieldValidator ID="rfvAsesor" runat="server" ControlToValidate="Ddlusuarios"
                                                        Display="Dynamic" ErrorMessage="Campo Requerido" ForeColor="Red" SetFocusOnError="True"
                                                        Style="font-size: x-small" ValidationGroup="vgGuardar" InitialValue="&lt;Seleccione un Item&gt;" />
                                                </span>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbvalorfondo" runat="server" Text="Valor Fondo Garantías" Visible="false"></asp:Label><br />
                                                <asp:TextBox ID="txt_ValorGaran" runat="server" CssClass="textbox" Visible="false" Width="153px"></asp:TextBox>
                                            </td>

                                            <td>Forma de Pago<br />
                                                <asp:DropDownList ID="ddlFormaPago" runat="server" Width="225px" CssClass="textbox"
                                                    AutoPostBack="True" OnSelectedIndexChanged="ddlFormaPago_SelectedIndexChanged">
                                                    <asp:ListItem Value="1">Caja</asp:ListItem>
                                                    <asp:ListItem Value="2">Nomina</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblEmpresa" runat="server" Text="Empresa"></asp:Label><br />
                                                <asp:DropDownList ID="ddlEmpresa" runat="server" CssClass="textbox" Width="222px">
                                                </asp:DropDownList>
                                            </td>
                                            <td style="text-align: left;">
                                                <asp:Label ID="LblTipoLiquidacion" Visible="False" runat="server" Text="Tipo de Liquidacion"></asp:Label>
                                                <br />
                                                <asp:DropDownList ID="ddlTipoLiquidacion" runat="server" CssClass="dropdown" Width="260px"
                                                    Visible="False">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvTipoLiquidacion" runat="server" ControlToValidate="ddlTipoLiquidacion"
                                                    Display="Dynamic" ErrorMessage="Campo Requerido" ForeColor="Red" SetFocusOnError="True"
                                                    Style="font-size: x-small" ValidationGroup="vgGuardar" InitialValue="&lt;Seleccione un Item&gt;" />
                                            </td>
                                            <td colspan="2" style="text-align: left;">
                                                <asp:Label ID="lblFechaPrimerPago" runat="server" Text="Fecha Primer Pago" /><br />
                                                <asp:TextBox ID="txtfechapripago" CssClass="textbox" runat="server"></asp:TextBox>
                                                <asp:CalendarExtender ID="calExtfechapago" Format="dd/MM/yyyy" runat="server" TargetControlID="txtfechapripago" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left;" colspan="3">
                                                <uc1:BuscarProveedor ID="ctlBusquedaProveedor" runat="server" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddlFormaPago" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="ddlMedio" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="ddlTipoCredito" EventName="TextChanged" />
                        <asp:AsyncPostBackTrigger ControlID="ddlTipoCredito" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
    <asp:Panel ID="Panel5" runat="server" Width="100%">
        <br />
        <asp:UpdatePanel ID="upFormaDesembolso" runat="server">
            <ContentTemplate>
                <table style="width: 583px;">
                    <tr>
                        <td style="width: 179px; text-align: left">
                            <strong>Forma de Desembolso</strong>
                        </td>
                        <td colspan="1" style="width: 404px">
                            <asp:DropDownList ID="DropDownFormaDesembolso" runat="server" Style="margin-left: 0px; text-align: left"
                                Width="84%" Height="28px" CssClass="textbox" AutoPostBack="True"
                                OnSelectedIndexChanged="DropDownFormaDesembolso_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>

                <asp:Panel runat="server" ID="pnlCuentaAhorroVista">
                    <table style="width: 48%">
                        <tr>
                            <td style="text-align: left; width: 110px">
                                <asp:Label ID="lblCuentaAhorroVista" runat="server" Text="Numero Cuenta" Style="text-align: left"></asp:Label>
                            </td>
                            <td style="width: 151px; text-align: left;">
                                <asp:DropDownList ID="ddlCuentaAhorroVista" runat="server" Style="margin-left: 0px; text-align: left"
                                    Width="102%" CssClass="textbox">
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel runat="server" ID="pnlCuentasBancarias">
                    <table style="width: 100%;">
                        <tr>
                            <td style="width: 179px; text-align: left">
                                <asp:Label ID="lblEntidad" runat="server" Text="Entidad" Style="text-align: left"></asp:Label>
                            </td>
                            <td colspan="3">
                                <%--<asp:DropDownList ID="DropDownEntidad" AutoPostBack="true" runat="server" Style="margin-left: 0px; text-align: left"
                                        Width="84%" CssClass="textbox" OnSelectedIndexChanged="DropDownEntidad_SelectedIndexChanged" >
                                    </asp:DropDownList>--%>
                                <asp:DropDownList ID="DropDownEntidad" runat="server" Style="margin-left: 0px; text-align: left"
                                    Width="84%" CssClass="textbox">
                                </asp:DropDownList>
                            </td>
                            <td>&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 179px; text-align: left">
                                <asp:Label ID="lblNumCuenta" runat="server" Text="Numero de Cuenta" Style="text-align: left"></asp:Label>
                            </td>
                            <td style="width: 151px">
                                <%--<asp:DropDownList ID="ddlNumeroCuenta"  runat="server" Style="margin-left: 0px; text-align: left"
                                        Width="102%" CssClass="textbox" OnSelectedIndexChanged="ddlNumeroCuenta_SelectedIndexChanged">
                                    </asp:DropDownList>--%>
                                <asp:TextBox runat="server" ID="txtNumeroCuentaBanco" CssClass="textbox" />
                            </td>
                            <td style="width: 110px; text-align: left">
                                <asp:Label ID="lblTipoCuenta" runat="server" Text="Tipo Cuenta" Style="text-align: left"></asp:Label>
                            </td>
                            <td style="width: 151px">
                                <asp:DropDownList ID="ddlTipo_cuenta" runat="server" Style="margin-left: 0px; text-align: left"
                                    Width="102%" CssClass="textbox">
                                </asp:DropDownList>
                            </td>
                            <td>&nbsp;
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
    <table style="width: 100%">
        <tr>
            <td style="text-align: left; font-size: x-small">
                <strong>
                    <asp:Label ID="lblMensaje" runat="server"></asp:Label></strong>
            </td>
        </tr>
    </table>
    <asp:Panel ID="panelCodeudor" runat="server">
        <table width="100%">
            <tr>
                <td style="text-align: left; color: #FFFFFF; background-color: #0066FF; height: 20px;" colspan="3">
                    <strong>Codeudores</strong>
                </td>
            </tr>
            <tr>
                <td style="text-align: left">
                    <br />

                    <asp:GridView ID="gvListaCodeudores" runat="server" Width="100%" AutoGenerateColumns="False"
                        BackColor="White" BorderColor="#DEDFDE" BorderStyle="None"
                        BorderWidth="1px" ForeColor="Black" OnRowDeleting="gvListaCodeudores_RowDeleting"
                        OnRowEditing="gvListaCodeudores_RowEditing" OnRowCancelingEdit="gvListaCodeudores_RowCancelingEdit"
                        OnRowUpdating="gvListaCodeudores_RowUpdating" OnRowCommand="gvListaCodeudores_RowCommand"
                        PageSize="5" DataKeyNames="cod_persona" ShowFooter="True" Style="font-size: x-small">
                        <Columns>
                            <asp:TemplateField ShowHeader="False">
                                <ItemTemplate>
                                    <asp:ImageButton ID="btnEdit" runat="server" CausesValidation="False" CommandName="Edit"
                                        ImageUrl="~/Images/gr_edit.jpg" ToolTip="Editar" Width="16px" Height="16px" />
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:ImageButton ID="btnSave" runat="server" CausesValidation="False" CommandName="Update"
                                        ImageUrl="~/Images/gr_guardar.jpg" ToolTip="Guardar" Width="16px" Height="16px" />
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:ImageButton ID="btnNuevo" runat="server" CausesValidation="False" CommandName="AddNew"
                                        ImageUrl="~/Images/gr_nuevo.jpg" ToolTip="Crear Nuevo" Width="16px" Height="16px" />
                                </FooterTemplate>
                                <ItemStyle Width="20px" />
                            </asp:TemplateField>
                            <asp:TemplateField ShowHeader="False">
                                <ItemTemplate>
                                    <asp:ImageButton ID="btnDelete" runat="server" CausesValidation="False" CommandName="Delete"
                                        ImageUrl="~/Images/gr_elim.jpg" ToolTip="Eliminar" Width="16px" Height="16px" />
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:ImageButton ID="btnCancel" runat="server" CausesValidation="False" CommandName="Cancel"
                                        ImageUrl="~/Images/gr_cancelar.jpg" ToolTip="Cancelar" Width="16px" Height="16px" />
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <%--<asp:BoundField DataField="cod_persona" HeaderText="Codpersona" />--%>
                            <asp:TemplateField HeaderText="Cod Persona">
                                <ItemTemplate>
                                    <asp:Label ID="lblCodPersona" runat="server" Text='<%# Bind("cod_persona") %>'></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lblOrden" runat="server" Text='# Orden'></asp:Label>
                                    <asp:TextBox ID="txtOdenFooter" runat="server" Style="font-size: x-small; text-align: right" Width="55px" Enabled="false" />
                                    <asp:FilteredTextBoxExtender ID="fteOrdenFooter" runat="server" Enabled="True" FilterType="Numbers" TargetControlID="txtOdenFooter" />
                                    &nbsp;&nbsp;&nbsp;<asp:Label ID="lblCodPersonaFooter" runat="server"></asp:Label>
                                </FooterTemplate>
                                <ItemStyle Width="170px" />
                                <FooterStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Identificación">
                                <ItemTemplate>
                                    <asp:Label ID="lblidentificacion" runat="server" Text='<%# Bind("IDENTIFICACION") %>'></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtidentificacion" runat="server" Text='<%# Bind("IDENTIFICACION") %>'
                                        OnTextChanged="txtidentificacion_TextChanged" Style="font-size: x-small" AutoPostBack="True"></asp:TextBox>
                                    <asp:Button ID="btnConsultaPersonas" runat="server" Text="..."
                                        Height="26px" OnClick="btnConsultaPersonas_Click" />
                                    <uc1:ListadoPersonas ID="ctlBusquedaPersonas" runat="server" />
                                </FooterTemplate>
                                <ItemStyle Width="170px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Primer Nombre">
                                <ItemTemplate>
                                    <asp:Label ID="lblPrimerNombre" runat="server" Text='<%# Bind("PRIMER_NOMBRE") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Segundo Nombre">
                                <ItemTemplate>
                                    <asp:Label ID="lblSegundoNombre" runat="server" Text='<%# Bind("SEGUNDO_NOMBRE") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Primer Apellido">
                                <ItemTemplate>
                                    <asp:Label ID="lblPrimerApellido" runat="server" Text='<%# Bind("PRIMER_APELLIDO") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Segundo Apellido">
                                <ItemTemplate>
                                    <asp:Label ID="lblSegundoApellido" runat="server" Text='<%# Bind("SEGUNDO_APELLIDO") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Dirección">
                                <ItemTemplate>
                                    <asp:Label ID="lblDireccionRow" runat="server" Text='<%# Bind("DIRECCION") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Teléfono">
                                <ItemTemplate>
                                    <asp:Label ID="lblTelefonoRow" runat="server" Text='<%# Bind("TELEFONO") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Orden">
                                <ItemTemplate>
                                    <asp:Label ID="lblOrdenRow" runat="server" Text='<%# Bind("ORDEN") %>' />
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtOrdenRow" runat="server" Text='<%# Bind("ORDEN") %>' Width="50px" Style="text-align: right" />
                                    <asp:FilteredTextBoxExtender ID="fteOrden" runat="server" Enabled="True" FilterType="Numbers" TargetControlID="txtOrdenRow" />
                                </EditItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
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
                <td style="text-align: center">
                    <asp:Label ID="lblTotReg" runat="server" Visible="False" />
                    <asp:Label ID="lblTotalRegsCodeudores" runat="server" Text="No hay codeudores para este crédito."
                        Visible="False" />
                </td>
            </tr>
            <tr>
                <td>
                    <br />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <table style="width: 100%">
        <tr>
            <td style="text-align: left; color: #FFFFFF; background-color: #0066FF; height: 20px;" colspan="3">
                <strong>Creditos A Recoger</strong>

            </td>
        </tr>
        <tr>
            <td style="height: 1px;">
                <asp:TextBox ID="txtorden" Height="10px" Visible="false" runat="server"></asp:TextBox>
            </td>
        </tr>
    </table>
    <asp:UpdatePanel ID="updatePanelRecoger" runat="server">
        <ContentTemplate>
            <asp:Panel ID="panelRecoger" runat="server">
                <table style="width: 100%">
                    <tr>
                        <td style="text-align: center;">
                            <div style="overflow: auto; max-height: 190px;">
                                <asp:GridView ID="gvListaSolicitudCreditosRecogidos" runat="server"
                                    AutoGenerateColumns="False" DataKeyNames="numero_credito" Style="font-size: x-small"
                                    OnRowCommand="gvListaSolicitudCreditosRecogidos_RowCommand"
                                    OnRowDataBound="gvListaSolicitudCreditosRecogidos_RowDataBound" OnRowDeleting="gvListaSolicitudCreditosRecogidos_RowDeleting"
                                    OnRowEditing="gvListaSolicitudCreditosRecogidos_RowEditing" OnSelectedIndexChanged="gvListaSolicitudCreditosRecogidos_SelectedIndexChanged"
                                    PageSize="8" Width="99%">
                                    <Columns>
                                        <asp:BoundField DataField="numero_credito">
                                            <HeaderStyle CssClass="gridColNo" />
                                            <ItemStyle CssClass="gridColNo" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="numero_credito" HeaderText="Número de crédito" />
                                        <asp:BoundField DataField="linea_credito" HeaderText="Línea">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="monto" DataFormatString="{0:n}" HeaderText="Monto">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="saldo_capital" DataFormatString="{0:N0}" HeaderText="Saldo capital">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="interes_corriente" DataFormatString="{0:N0}" HeaderText="Interés corriente">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="interes_mora" DataFormatString="{0:N0}" HeaderText="Interés mora">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="otros" DataFormatString="{0:n0}" HeaderText="Otros">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="leymipyme" HeaderText="LeymiPyme" />
                                        <asp:BoundField DataField="iva_leymipyme" HeaderText="Iva LeyMiPyme" />
                                        <asp:BoundField DataField="valor_total" DataFormatString="{0:N0}" HeaderText="Valor Total">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="cuotas_pagadas" HeaderText="Cuo.Pag">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Recoger">
                                            <ItemTemplate>
                                                <cc1:CheckBoxGrid ID="chkRecoger" runat="server" Checked='<%# Eval("recoger") %>'
                                                    AutoPostBack="true" OnCheckedChanged="chkRecoger_CheckedChanged" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="cantidad_nominas" HeaderText="No.Nom.">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="valor_nominas" HeaderText="Val.Nominas">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                    </Columns>
                                    <HeaderStyle CssClass="gridHeader" />
                                    <PagerStyle CssClass="gridPager" />
                                    <RowStyle CssClass="gridItem" />
                                </asp:GridView>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblMensajeCredRecogidos" runat="server" Visible="false" Font-Bold="true" />
                        </td>
                    </tr>
                </table>
                <table style="width: 100%">
                    <tr>
                        <td style="text-align: left; color: #FFFFFF; background-color: #0066FF; height: 20px;" colspan="3">
                            <strong>Servicios A Recoger</strong>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="gvServicios" runat="server" Width="100%" GridLines="Horizontal" AutoGenerateColumns="False"
                                HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager"
                                RowStyle-CssClass="gridItem" DataKeyNames="numero_servicio" Style="font-size: x-small">
                                <Columns>
                                    <asp:BoundField DataField="numero_servicio" HeaderText="Num. Servicio">
                                        <ItemStyle HorizontalAlign="center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="fecha_solicitud" HeaderText="Fec. Solicitud" DataFormatString="{0:d}">
                                        <ItemStyle HorizontalAlign="center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="nom_linea" HeaderText="Linea">
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="#Cuota" DataField="numero_cuotas" />
                                    <asp:BoundField HeaderText="Periodicidad" DataField="nom_periodicidad" />
                                    <asp:BoundField HeaderText="Forma de Pago" DataField="forma_pago" />
                                    <asp:BoundField HeaderText="Vr.Cuota" DataField="valor_cuota" DataFormatString="{0:n0}" />
                                    <asp:BoundField DataField="saldo" HeaderText="Saldo" DataFormatString="{0:n0}" />
                                    <asp:BoundField DataField="total_interes_calculado" HeaderText="Interes" DataFormatString="{0:n0}" />
                                    <asp:BoundField DataField="total_calculado" HeaderText="Valor a Pagar" DataFormatString="{0:n0}" />
                                    <asp:TemplateField HeaderText="Recoger">
                                        <ItemTemplate>
                                            <cc1:CheckBoxGrid ID="chkRecogerServicios" runat="server"
                                                AutoPostBack="true" OnCheckedChanged="chkRecogerServicios_CheckedChanged" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                </Columns>
                                <HeaderStyle CssClass="gridHeader"></HeaderStyle>
                                <PagerStyle CssClass="gridPager"></PagerStyle>
                                <RowStyle CssClass="gridItem"></RowStyle>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="lblTotRec" runat="server" Text="Total por Recoger :" Style="font-size: x-small" />
                            &#160;&#160;
                    <uc2:decimales ID="txtVrTotRecoger" runat="server" Width="120px" Enabled="false" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="lblMontoDesembolso" runat="server" Text="Valor estimado a Desembolsar :" Style="font-size: x-small" />
                            &#160;&#160;
                    <uc2:decimales ID="txtVrDesembolsar" runat="server" Width="120px" Enabled="false" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; vertical-align: top">
                            <asp:Label ID="lblTotalRegsSolicitudCreditosRecogidos" Font-Bold="true" runat="server" Text="Su consulta no obtuvo ningún resultado." Visible="False" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="txtMonto" EventName="eventoCambiar" />
        </Triggers>
    </asp:UpdatePanel>
    <br />
    <br />
    <table style="width: 100%">
        <tr>
            <td style="text-align: left; color: #FFFFFF; background-color: #0066FF; height: 20px;" colspan="3">
                <strong>Referencias</strong>
            </td>
        </tr>
        <tr>
            <td style="text-align: left;">
                <asp:UpdatePanel ID="UpdatePanelReferencias" runat="server">
                    <ContentTemplate>
                        <asp:Button ID="btnAgregarReferencia" runat="server" CssClass="btn8" Text="+ Adicionar Referencia" OnClick="btnAgregarReferencia_Click" />
                        <div style="overflow: auto; max-height: 200px;">
                            <asp:GridView ID="gvReferencias" runat="server" Width="100%" AutoGenerateColumns="False"
                                AllowPaging="False" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None"
                                OnRowCommand="gvReferencia_OnRowCommand"
                                OnRowDeleting="gvReferencias_RowDeleting"
                                BorderWidth="1px" ForeColor="Black"
                                Style="font-size: x-small; overflow: auto;">
                                <Columns>
                                    <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/gr_elim.jpg" ShowDeleteButton="True">
                                        <ItemStyle Width="16px" />
                                    </asp:CommandField>
                                    <asp:TemplateField HeaderText="Quien Referencia" ItemStyle-Width="10%">
                                        <ItemTemplate>
                                            <cc1:DropDownListGrid ID="ddlQuienReferencia" runat="server" CssClass="textbox" Width="95%">
                                            </cc1:DropDownListGrid>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Tipo Referencia">
                                        <ItemTemplate>
                                            <cc1:DropDownListGrid ID="ddlTipoReferencia" runat="server" CommandArgument='<%#((GridViewRow) Container).RowIndex %>' CssClass="textbox" AppendDataBoundItems="true" AutoPostBack="true"
                                                SelectedValue='<%# Bind("tiporeferencia") %>' OnSelectedIndexChanged="ddlTipoReferencia_SelectedIndexChanged" Width="95%">
                                                <asp:ListItem Value="2" Text="Personal" />
                                                <asp:ListItem Value="1" Text="Familiar" />
                                                <asp:ListItem Value="3" Text="Comercial" />
                                            </cc1:DropDownListGrid>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Nombres">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtNombres" Text='<%# Bind("nombres") %>' runat="server" Style="font-size: x-small" CssClass="textbox" Width="95%" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Parentesco" ItemStyle-Width="10%">
                                        <ItemTemplate>
                                            <cc1:DropDownListGrid ID="ddlParentesco" runat="server" CssClass="textbox" AppendDataBoundItems="true" Width="95%"
                                                DataSource="<%#ListarParentesco() %>" DataTextField="ListaDescripcion" DataValueField="ListaId"
                                                SelectedValue='<%# Bind("codparentesco") %>'>
                                                <asp:ListItem Value="0" Text="" />
                                            </cc1:DropDownListGrid>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Dirección" ItemStyle-Width="16%">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtDireccion" Text='<%# Bind("direccion") %>' runat="server" CssClass="textbox" Style="font-size: x-small" Width="95%" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Teléfono" ItemStyle-Width="8%">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtTelefono" Text='<%# Bind("telefono") %>' onkeypress="return isNumber(event)" runat="server" CssClass="textbox" Style="font-size: x-small" Width="95%" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Tel.Oficina" ItemStyle-Width="8%">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtTelOficina" Text='<%# Bind("teloficina") %>' onkeypress="return isNumber(event)" runat="server" CssClass="textbox" Style="font-size: x-small" Width="95%" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Celular" ItemStyle-Width="8%">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtCelular" Text='<%# Bind("celular") %>' onkeypress="return isNumber(event)" runat="server" CssClass="textbox" Style="font-size: x-small" Width="90%" />
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
            </td>
        </tr>
    </table>
    <div>
        <uc1:ctrCuotasExtras runat="server" ID="CuotasExtras" />
    </div>
    <asp:ModalPopupExtender ID="MpeDetalleAvances" runat="server" Enabled="True" PopupDragHandleControlID="Panel3"
        PopupControlID="PanelDetalleAvance" TargetControlID="HiddenField1" CancelControlID="btnCloseAct2">
        <Animations>
            <OnHiding>
                <Sequence>                            
                    <StyleAction AnimationTarget="btnCloseAct2" Attribute="display" Value="none" />
                    <Parallel>
                        <FadeOut />
                        <Scale ScaleFactor="5" />
                    </Parallel>
                </Sequence>
            </OnHiding>            
        </Animations>
    </asp:ModalPopupExtender>
    <asp:HiddenField ID="HiddenField1" runat="server" />
    <asp:Panel ID="PanelDetalleAvance" runat="server" Width="480px" Style="display: none; border: solid 2px Gray" CssClass="modalPopup">
        <asp:UpdatePanel ID="UpDetalleAvances" runat="server">
            <ContentTemplate>
                <table>
                    <tr>
                        <td style="font-size: x-small; color: #FFFFFF; background-color: #0066FF; width: 434px">
                            <asp:Panel ID="Panel3" runat="server" Width="475px" Style="cursor: move">
                                <strong>Detalle Avances </strong>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr style="background-color: #0066FF">
                        <td style="background-color: #0066FF">
                            <table style="background-color: #0066FF">
                                <tr style="background-color: #0066FF">
                                    <td style="width: 120px; background-color: #0066FF">
                                        <strong>Total Capital: </strong>
                                        <asp:TextBox ID="TxtTotalCap" runat="server" CssClass="textbox"
                                            Width="56px"></asp:TextBox>
                                    </td>
                                    <td style="width: 120px; background-color: #0066FF">
                                        <strong>Total Intereses: </strong>
                                        <asp:TextBox ID="TxtTotalInt" runat="server" CssClass="textbox"
                                            Width="56px"></asp:TextBox>
                                    </td>
                                    <td style="width: 120px; background-color: #0066FF">
                                        <strong>Total Avances: </strong>
                                        <asp:TextBox ID="TxtTotalAvances" runat="server" CssClass="textbox"
                                            Width="56px"></asp:TextBox>
                                    </td>

                                </tr>
                            </table>
                        </td>
                        <tr>
                            <td style="width: 475px; margin-left: 120px; background-color: #0066FF">
                                <div class="scrolling-table-container" style="height: 378px; overflow-y: scroll; overflow-x: hidden;">
                                    <asp:CheckBox ID="chkAvances" runat="server" AutoPostBack="true" OnCheckedChanged="chkAvances_CheckedChanged" />
                                    <asp:Label ID="Label4" runat="server" Text=" ¿Seleccionar Todos los Avances?"></asp:Label>
                                    <br />
                                    <asp:GridView ID="gvAvances" runat="server" Width="99%" AutoGenerateColumns="False"
                                        AllowPaging="false" OnPageIndexChanging="gvAvances_PageIndexChanging" PageSize="20" GridLines="Horizontal" ShowHeaderWhenEmpty="True"
                                        Style="font-size: x-small" DataKeyNames="NumAvance">
                                        <Columns>
                                            <%-- IDAVANCE,FECHA_DESEMBOLSO,VALOR_DESEMBOLSADO,VALOR_CUOTA,PLAZO,SALDO_AVANCE--%>
                                            <asp:TemplateField HeaderText="Sel">
                                                <ItemTemplate>
                                                    <cc1:CheckBoxGrid ID="chkAvance" runat="server" Checked="true"
                                                        AutoPostBack="true" OnCheckedChanged="chkAvance_CheckedChanged" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="NumAvance" HeaderText="Id Avance">
                                                <ItemStyle HorizontalAlign="center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="FechaDesembolsi" HeaderText="Fecha Desembolso" DataFormatString="{0:d}">
                                                <ItemStyle HorizontalAlign="left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ValDesembolso" HeaderText="Valor Desembolso">
                                                <ItemStyle HorizontalAlign="left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ValorCuota" HeaderText="Valor Cuota">
                                                <ItemStyle HorizontalAlign="center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Plazo" HeaderText="Plazo">
                                                <ItemStyle HorizontalAlign="center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="SaldoAvance" HeaderText="Saldo Avance">
                                                <ItemStyle HorizontalAlign="center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Intereses" HeaderText="Intereses">
                                                <ItemStyle HorizontalAlign="center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ValorTotal" HeaderText="Total Pagar">
                                                <ItemStyle HorizontalAlign="center" />
                                            </asp:BoundField>
                                        </Columns>
                                        <FooterStyle BackColor="#CCCC99" />
                                        <HeaderStyle CssClass="gridHeader" />
                                        <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                                        <RowStyle CssClass="gridItem" BackColor="#CCCC99" />
                                        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                        <SortedAscendingCellStyle BackColor="#FBFBF2" />
                                        <SortedAscendingHeaderStyle BackColor="#848384" />
                                        <SortedDescendingCellStyle BackColor="#EAEAD3" />
                                        <SortedDescendingHeaderStyle BackColor="#575357" />
                                    </asp:GridView>
                                </div>
                            </td>
                        </tr>


                </table>
                </tr>
                    <tr>
                        <td style="width: 475px; background-color: #0066FF">
                            <asp:Button ID="btnCloseAct2" runat="server" Text="Cerrar" CssClass="button" OnClick="btnCloseAct2_Click" CausesValidation="False" Height="20px" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
    <uc1:validarBiometria ID="ctlValidarBiometria" runat="server" />

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

    <script src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.5.2.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        function validar() {
            var statusConfirm = confirm("¿Desea Crear Codeudor no existe en el Sistema?");
            if (statusConfirm == true) {
                window.open("../../../Aportes/Personas/Tab/Persona.aspx", 'Codeudores', "resizable=yes ,width=500, height=450 align=center");
            }
        }



    </script>
</asp:Content>
