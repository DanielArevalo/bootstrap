<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Detalle.aspx.cs" Inherits="Detalle"
    EnableEventValidation="true" MasterPageFile="~/General/Master/site.master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc2" %>
<%@ Register Src="~/General/Controles/decimalesGridRow.ascx" TagName="decimalesGridRow" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Src="~/General/Controles/ctlProveedor.ascx" TagName="BuscarProveedor" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/ctlFormatoDocum.ascx" TagName="FormatoDocu" TagPrefix="uc4" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>
<%@ Register Src="~/General/Controles/ctlNumeroConDecimales.ascx" TagName="Decimal" TagPrefix="ucNumeroConDecimales" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Src="~/General/Controles/ctlCuotasExtras.ascx" TagName="ctrCuotasExtras" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/ctlDocumentosAnexo.ascx" TagName="ctlDocumentosAnexo" TagPrefix="uc1" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphMain">
    <style>
        #cphMain_Tabs_body {
            overflow: scroll;
        }
    </style>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
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
        function DisplayFullImage(ctrlimg) {
            txtCode = "<HTML><HEAD>"
                + "</HEAD><BODY TOPMARGIN=0 LEFTMARGIN=0 MARGINHEIGHT=0 MARGINWIDTH=0><CENTER>"
                + "<IMG src='" + ctrlimg.src + "' BORDER=0 NAME=FullImage "
                + "onload='window.resizeTo(document.FullImage.width,document.FullImage.height)'>"
                + "</CENTER>"
                + "</BODY></HTML>";
            mywindow = window.open('', 'image', 'toolbar=0,location=0,menuBar=0,scrollbars=0,resizable=0,width=1,height=1');
            mywindow.document.open();
            mywindow.document.write(txtCode);
            mywindow.document.close();
        }

    </script>
    <span class="badge badge-warning" runat="server" Visible="True" id="labelWarning" style="font-size: 9pt;"></span>
    <asp:MultiView ID="mvAprobacion" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwDatos" runat="server">
            <table style="width: 90%;">
                <tr style="text-align: left">
                    <td colspan="6">
                        <strong>Datos del Deudor&nbsp; </strong>
                    </td>
                </tr>
                <tr style="text-align: left">
                    <td style="width: 140px">Código del cliente:
                        <br />
                        <asp:TextBox ID="txtCodigo" runat="server" CssClass="textbox" Enabled="False" Width="140px"></asp:TextBox>
                    </td>
                    <td style="width: 120px">Identificación:
                        <br />
                        <asp:TextBox ID="txtId" runat="server" CssClass="textbox" Enabled="False" Width="120px"></asp:TextBox>
                    </td>
                    <td style="width: 399px">Nombres:
                        <br />
                        <asp:TextBox ID="txtNombres" runat="server" CssClass="textbox" Enabled="False" Width="387px"></asp:TextBox>
                    </td>
                    <td style="width: 80px">
                        <asp:Label ID="lbledad" runat="server" Text="Edad"></asp:Label>
                        <asp:RangeValidator ID="rvEdad" runat="server" ControlToValidate="txtEdad" EnableClientScript="false"
                            ErrorMessage="Edad no valida" ForeColor="Red" MaximumValue="75" MinimumValue="18"
                            Type="Integer" ValidationGroup="vgEdad"></asp:RangeValidator>
                        <br />
                        <asp:TextBox ID="txtEdad" runat="server" CssClass="textbox" Enabled="False" Width="80px"></asp:TextBox>
                    </td>
                    <td style="width: 80px">Calificación:
                        <br />
                        <asp:TextBox ID="txtCalificacion" runat="server" CssClass="textbox" Enabled="False"
                            Width="80px"></asp:TextBox>
                    </td>
                    <td>
                        <strong>
                            <asp:DropDownList ID="ddlProceso" runat="server" CssClass="dropdown" Enabled="False"
                                Height="25px" Width="186px" Visible="False">
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
                    <td>&nbsp;
                    </td>
                </tr>
                <tr style="text-align: left">
                    <td style="width: 104px">No. Crédito:
                        <br />
                        <asp:TextBox ID="txtCredito" runat="server" CssClass="textbox" Enabled="False" Width="140px"></asp:TextBox>
                    </td>
                    <td style="width: 258px">Línea de crédito:
                        <br />
                        <asp:TextBox ID="txtLinea" runat="server" CssClass="textbox" Enabled="False" Width="260px"></asp:TextBox>
                    </td>
                    <td style="width: 144px" class="logo">Monto Solicitado:
                        <br />
                        <uc2:decimales ID="txtMonto" runat="server" CssClass="textbox" Enabled="False" Width="132px"></uc2:decimales>
                    </td>
                    <td style="width: 131px">Plazo:
                        <br />
                        <asp:TextBox ID="txtPlazo" runat="server" CssClass="textbox" Enabled="False" Width="85px"></asp:TextBox>
                    </td>
                    <td style="width: 245px">Cuota Crédito:<br />
                        <asp:TextBox ID="txtCuota" runat="server" CssClass="textbox" Enabled="False" Width="131px"></asp:TextBox>
                        <asp:MaskedEditExtender ID="mskCuota" runat="server" AcceptNegative="Left" DisplayMoney="Left"
                            ErrorTooltipEnabled="True" InputDirection="RightToLeft" Mask="999,999,999" MaskType="Number"
                            MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError"
                            TargetControlID="txtCuota" />
                    </td>
                    <td style="width: 131px">Forma Pago:
                        <br />
                        <asp:TextBox ID="txtFormaPago" runat="server" CssClass="textbox" Enabled="False"
                            Width="94px"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <table style="width: 90%;">
                <tr style="text-align: left">
                    <td style="text-align: left; width: 444px;">Asesor:<br />
                        <asp:TextBox ID="txtAsesor" runat="server" CssClass="textbox" Enabled="False" Width="433px"></asp:TextBox>
                    </td>
                    <td style="text-align: left; width: 138px;">Periodicidad:
                        <br />
                        <asp:TextBox ID="txtPeriodicidad" runat="server" CssClass="textbox" Enabled="False"
                            Width="130px"></asp:TextBox>
                    </td>
                    <td style="text-align: left; width: 153px;">Disponible:<br />
                        <asp:TextBox ID="txtDisp" runat="server" CssClass="textbox" Enabled="False" Width="148px"></asp:TextBox>
                    </td>
                    <td>Destino del Crédito
                       <br />
                        <asp:DropDownList ID="ddlDestino" runat="server" CssClass="textbox" Width="225px" />
                    </td>
                </tr>
                <tr>
                    <td colspan="4">Observaciones Solicitud
                       <br />
                        <asp:TextBox ID="txtObsSoli" runat="server" CssClass="textbox" Enabled="False" Width="95%" MaxLength="4000"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <table style="width: 90%;">
                <tr style="text-align: left">
                    <td colspan="3">
                        <strong>Conceptos y Propuesta del Asesor</strong>
                    </td>

                    <td>&nbsp;
                    </td>
                    <td>&nbsp;
                    </td>
                </tr>
                <tr style="text-align: left">
                    <td colspan="3">
                        <asp:TextBox ID="txtPropuesta" runat="server" CssClass="textbox" Enabled="False"
                            TextMode="MultiLine" Width="497px"></asp:TextBox>
                    </td>
                    <td>
                        <asp:CheckBox ID="CkcAfiancol" runat="server" Enabled="True" OnCheckedChanged="CkcAfiancol_OnCheckedChanged" AutoPostBack="True" />
                        <asp:Label ID="Label5" runat="server" Text="Afiancol "></asp:Label>
                    </td>
                    <td style="text-align: left">
                        <asp:Button ID="registro" runat="server" CssClass="btn8" OnClick="registro_Click" Visible="false"
                            Text="Registrar Conceptos" Width="126px" />
                    </td>
                    <td>
                        <span>
                            <asp:TextBox ID="TxtValorTasaCred" runat="server" CssClass="textbox" MaxLength="250"
                                Width="88px" Visible="false"></asp:TextBox>
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
            <asp:TabContainer runat="server" ID="Tabs" CssClass="CustomTabStyle" OnClientActiveTabChanged="ActiveTabChanged"
                ActiveTabIndex="1" Width="100%" Height="250px" Style="margin-right: 30px;">
                <asp:TabPanel runat="server" ID="tabCodeudores" HeaderText="Datos">
                    <HeaderTemplate>
                        Codeudores
                    </HeaderTemplate>
                    <ContentTemplate>
                        <asp:UpdatePanel ID="UpdatePanelCodeudores" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:Label ID="lblErrorCod" runat="server" Style="font-size: x-small; color: red; font-weight: bold" /><br />
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
                                            </FooterTemplate>
                                            <ItemStyle Width="50px" />
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
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="gvListaCodeudores" EventName="RowDeleting" />
                                <asp:AsyncPostBackTrigger ControlID="gvListaCodeudores" EventName="RowEditing" />
                                <asp:AsyncPostBackTrigger ControlID="gvListaCodeudores" EventName="RowCancelingEdit" />
                                <asp:AsyncPostBackTrigger ControlID="gvListaCodeudores" EventName="RowUpdating" />
                                <asp:AsyncPostBackTrigger ControlID="gvListaCodeudores" EventName="RowCommand" />
                            </Triggers>
                        </asp:UpdatePanel>
                        <asp:Label ID="lblTotalRegsCodeudores" runat="server" Text="Su consulta no obtuvo ningun resultado."
                            Visible="False" />
                    </ContentTemplate>
                </asp:TabPanel>
                <asp:TabPanel runat="server" ID="tabRecogidos" HeaderText="Datos">
                    <HeaderTemplate>
                        Productos Recogidos
                    </HeaderTemplate>
                    <ContentTemplate>
                        <asp:UpdatePanel ID="updatePanelRecoger" runat="server">
                            <ContentTemplate>
                                <table style="width: 100%;">
                                    <tr>
                                        <td>
                                            <strong>Creditos Recogidos</strong>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:GridView ID="gvListaSolicitudCreditosRecogidos" runat="server" Width="100%"
                                                AutoGenerateColumns="False" OnRowDataBound="gvListaSolicitudCreditosRecogidos_RowDataBound"
                                                AllowPaging="True" OnPageIndexChanging="gvListaSolicitudCreditosRecogidos_PageIndexChanging"
                                                DataKeyNames="numero_credito" Style="font-size: x-small">
                                                <Columns>
                                                    <asp:BoundField DataField="numero_credito">
                                                        <HeaderStyle CssClass="gridColNo" />
                                                        <ItemStyle CssClass="gridColNo" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="numero_credito" HeaderText="Número de crédito" />
                                                    <asp:BoundField DataField="linea_credito" HeaderText="Línea">
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="monto" HeaderText="Monto" DataFormatString="{0:n}">
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="saldo_capital" HeaderText="Saldo capital" DataFormatString="{0:N0}">
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="interes_corriente" HeaderText="Interés corriente" DataFormatString="{0:N0}">
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="interes_mora" HeaderText="Interés mora" DataFormatString="{0:N0}">
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="otros" HeaderText="Otros" DataFormatString="{0:n0}">
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="valor_total" HeaderText="Valor Total" DataFormatString="{0:n0}">
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="cuotas_pagadas" HeaderText="Cuo.Pag">
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField HeaderText="Recoger">
                                                        <ItemTemplate>
                                                            <cc1:CheckBoxGrid ID="chkRecoger" runat="server" Checked='<%# Eval("Recoger") %>'
                                                                AutoPostBack="true" OnCheckedChanged="chkRecoger_CheckedChanged" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="cantidad_nominas" HeaderText="Cant.Nominas">
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
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <strong>Servicios Recogidos</strong>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:GridView ID="gvServiciosRecogidos" runat="server" Width="100%"
                                                GridLines="Horizontal" AutoGenerateColumns="False"
                                                AllowPaging="True" OnPageIndexChanging="gvServiciosRecogidos_PageIndexChanging"
                                                DataKeyNames="consecutivo" Style="font-size: x-small">
                                                <Columns>
                                                    <asp:BoundField DataField="numeroservicio" HeaderText="Num. Servicio">
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="fecha_solicitud" HeaderText="Fec. Solicitud" DataFormatString="{0:d}">
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="nom_linea" HeaderText="Linea">
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField HeaderText="#Cuota" DataField="numero_cuotas" />
                                                    <asp:BoundField HeaderText="Periodicidad" DataField="nom_periodicidad" />
                                                    <asp:BoundField HeaderText="Forma de Pago" DataField="forma_pago" />
                                                    <asp:BoundField HeaderText="Vr.Cuota" DataField="valor_cuota" DataFormatString="{0:n0}" />
                                                    <asp:BoundField DataField="saldoservicio" HeaderText="Saldo" DataFormatString="{0:n0}" />
                                                    <asp:BoundField DataField="interessevicio" HeaderText="Interes" DataFormatString="{0:n0}" />
                                                    <asp:BoundField DataField="valorrecoger" HeaderText="Valor a Pagar" DataFormatString="{0:n0}" />
                                                </Columns>
                                                <HeaderStyle CssClass="gridHeader" />
                                                <PagerStyle CssClass="gridPager" />
                                                <RowStyle CssClass="gridItem" />
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right">
                                            <asp:Label ID="lblTotRec" runat="server" Text="Total por Recoger :" Style="font-size: x-small" />
                                            &#160;&#160;
                                            <uc2:decimales ID="txtVrTotRecoger" runat="server" CssClass="textbox" Width="120px"
                                                Enabled="False" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right">
                                            <asp:Label ID="lblMontoDesembolso" runat="server" Text="Valor estimado a Desembolsar :"
                                                Style="font-size: x-small" />
                                            &#160;&#160;
                                            <uc2:decimales ID="txtVrDesembolsar" runat="server" CssClass="textbox" Width="120px"
                                                Enabled="False" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: center">
                                            <asp:Label ID="lblTotalRegsSolicitudCreditosRecogidos" runat="server" Text="Su consulta no obtuvo ningún resultado."
                                                Visible="False" />
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </ContentTemplate>
                </asp:TabPanel>
                <asp:TabPanel runat="server" ID="tabAnexos" HeaderText="Datos">
                    <HeaderTemplate>
                        Modalidad Tasas
                    </HeaderTemplate>
                    <ContentTemplate>
                        <strong>
                            <div id="divModalidadTasas" runat="server" style="overflow: scroll">
                                <asp:GridView ID="gvListaModalidadTasas" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                    DataKeyNames="COD_MODALIDAD" GridLines="Horizontal" OnPageIndexChanging="gvListaModalidadTasas_PageIndexChanging"
                                    OnRowDataBound="gvListaModalidadTasas_RowDataBound" Width="99%" PageSize="4">
                                    <Columns>
                                        <asp:BoundField DataField="cod_linea_credito">
                                            <HeaderStyle CssClass="gridColNo" />
                                            <ItemStyle CssClass="gridColNo" HorizontalAlign="Center" VerticalAlign="Bottom" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="descripcion" HeaderText="Descripcion">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="valor" DataFormatString="{0:d}" HeaderText="Valor">
                                            <FooterStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Seleccionar">
                                            <ItemTemplate>
                                                <cc1:CheckBoxGrid ID="chkSeleccionarTasa" runat="server" CommandArgument='<%#Container.DataItemIndex %>'
                                                    Checked='<%# Eval("Seleccionar") %>' AutoPostBack="True" OnCheckedChanged="chkSeleccionarTasa_CheckedChanged" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <HeaderStyle CssClass="gridHeader" />
                                    <PagerStyle CssClass="gridPager" />
                                    <RowStyle CssClass="gridItem" />
                                </asp:GridView>
                            </div>
                        </strong>
                        <asp:Label ID="lblTotalRegsModalidadTasa" runat="server" Visible="False" />
                        <asp:Label ID="lblInfo2" runat="server" Text="Su consulta no obtuvo ningún resultado."
                            Visible="False" />
                    </ContentTemplate>
                </asp:TabPanel>
                <asp:TabPanel runat="server" ID="tabCentrales" HeaderText="Datos">
                    <HeaderTemplate>
                        Consulta Centrales de Riesgo
                    </HeaderTemplate>
                    <ContentTemplate>
                        <asp:GridView ID="gvLista" runat="server" Width="100%" AutoGenerateColumns="False"
                            AllowPaging="True" OnPageIndexChanging="gvLista_PageIndexChanging" OnSelectedIndexChanged="gvLista_SelectedIndexChanged"
                            PageSize="20" DataKeyNames="NUMEROFACTURA">
                            <Columns>
                                <asp:BoundField DataField="NUMEROFACTURA">
                                    <HeaderStyle CssClass="gridColNo" />
                                    <ItemStyle CssClass="gridColNo" />
                                </asp:BoundField>
                                <asp:TemplateField Visible="False">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnInfo" runat="server" CommandName="Select" ImageUrl="~/Images/gr_info.jpg"
                                            ToolTip="Detalle" />
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="gridIco" />
                                    <ItemStyle CssClass="gridIco" />
                                </asp:TemplateField>
                                <asp:TemplateField Visible="False">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnEditar" runat="server" CommandName="Edit" ImageUrl="~/Images/gr_edit.jpg"
                                            ToolTip="Modificar" />
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="gridIco" />
                                    <ItemStyle CssClass="gridIco" />
                                </asp:TemplateField>
                                <asp:TemplateField Visible="False">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnBorrar" runat="server" CommandName="Delete" ImageUrl="~/Images/gr_elim.jpg"
                                            ToolTip="Borrar" />
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="gridIco" />
                                    <ItemStyle CssClass="gI" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="NUMEROFACTURA" HeaderText="Número Factura" />
                                <asp:BoundField DataField="FECHACONSULTA" HeaderText="Fecha Consulta" />
                                <asp:BoundField DataField="CEDULACLIENTE" HeaderText="Cédula Cliente" />
                                <asp:BoundField DataField="USUARIO" HeaderText="Usuario" />
                                <asp:BoundField DataField="IP" HeaderText="Ip" />
                                <asp:BoundField DataField="OFICINA" HeaderText="Oficina" />
                                <asp:BoundField DataField="VALORCONSULTA" HeaderText="Valor Consulta" DataFormatString="{0:n}">
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                            </Columns>
                            <HeaderStyle CssClass="gridHeader" />
                            <PagerStyle CssClass="gridPager" />
                            <RowStyle CssClass="gridItem" />
                        </asp:GridView>
                        <asp:Label ID="lblTotalRegsConsultasCentrales" runat="server" Text="Su consulta no obtuvó ningún resultado."
                            Visible="False" />
                    </ContentTemplate>
                </asp:TabPanel>
                <asp:TabPanel runat="server" ID="tabReferenciador" HeaderText="Datos">
                    <HeaderTemplate>
                        Observaciones Referenciador
                    </HeaderTemplate>
                    <ContentTemplate>
                        <asp:GridView ID="gvObservacionesRef" runat="server" Width="100%" AutoGenerateColumns="False"
                            AllowPaging="True" PageSize="20" AllowSorting="True" OnPageIndexChanging="gvObservacionesRef_PageIndexChanging">
                            <Columns>
                                <asp:BoundField DataField="referenciado" HeaderText="Tipo Referencia" />
                                <asp:BoundField DataField="nombre_referenciado" HeaderText="Nombre" />
                                <asp:BoundField DataField="detalle" HeaderText="Pregunta" />
                                <asp:BoundField DataField="resultado" HeaderText="Alerta">
                                    <ItemStyle ForeColor="Red" />
                                </asp:BoundField>
                                <asp:BoundField DataField="observaciones" HeaderText="Observaciones" />
                            </Columns>
                            <HeaderStyle CssClass="gridHeader"></HeaderStyle>
                            <PagerStyle CssClass="gridPager"></PagerStyle>
                            <RowStyle CssClass="gridItem"></RowStyle>
                        </asp:GridView>
                        <asp:Label ID="lblTotalRegsObservacionesRef" runat="server"></asp:Label>
                    </ContentTemplate>
                </asp:TabPanel>
                <asp:TabPanel runat="server" ID="TabPanel1" HeaderText="Datos">
                    <HeaderTemplate>
                        Conceptos de Aprobadores
                    </HeaderTemplate>
                    <ContentTemplate>
                        <asp:GridView ID="gvAprobacion" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                            GridLines="Horizontal" PageSize="3" ShowHeaderWhenEmpty="True" Width="100%" OnPageIndexChanging="gvAprobacion_PageIndexChanging">
                            <Columns>
                                <asp:BoundField DataField="nivel" HeaderText="Nivel" />
                                <asp:BoundField DataField="nombres" HeaderText="Aprobador" />
                                <asp:BoundField DataField="observaciones" HeaderText="Observaciones cómite de crédito" />
                            </Columns>
                            <HeaderStyle CssClass="gridHeader" />
                            <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                            <RowStyle CssClass="gridItem" />
                        </asp:GridView>
                        <asp:Label ID="lblTotalRegs" runat="server" Visible="False" />
                        <asp:Label ID="lblInfo" runat="server" Text="Su consulta no obtuvo ningún resultado."
                            Visible="False" />
                    </ContentTemplate>
                </asp:TabPanel>
                <asp:TabPanel runat="server" ID="tabCuoExt" HeaderText="Cuotas Extras">
                    <HeaderTemplate>
                        Cuotas Extras
                    </HeaderTemplate>
                    <ContentTemplate>
                        <uc1:ctrCuotasExtras runat="server" ID="CuotasExtras" />
                    </ContentTemplate>
                </asp:TabPanel>
                <asp:TabPanel runat="server" ID="TabPanel2" HeaderText="Documentos Anexos">
                    <HeaderTemplate>
                        DOCUMENTOS ANEXOS
                    </HeaderTemplate>
                    <ContentTemplate>
                        <uc1:ctlDocumentosAnexo runat="server" ID="DocumentosAnexos" />
                    </ContentTemplate>
                </asp:TabPanel>
            </asp:TabContainer>

            <hr style="border-color: #0000FF; border-width: 3px;" width="100%" />
            <table width="100%">
                <tr>
                    <td>
                        <asp:RadioButtonList ID="rbTipoAprobacion" runat="server"
                            RepeatDirection="Horizontal"
                            OnSelectedIndexChanged="rbTipoAprobacion_SelectedIndexChanged"
                            AutoPostBack="True">
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
                                    <td>No. Crédito:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtCredito2" runat="server" CssClass="textbox" Width="145px" Enabled="false"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvCredito2" runat="server" ControlToValidate="txtCredito2"
                                            Style="font-size: xx-small" Display="Dynamic" ErrorMessage="Campo Requerido"
                                            ForeColor="Red" SetFocusOnError="True" ValidationGroup="vgAcpAprobar" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>Observaciones de aprobación:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtObsApr" runat="server" CssClass="textbox" Width="283px" MaxLength="2500"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: center">
                                        <asp:Button ID="btnAcpAprobar" runat="server" CssClass="btn8" Text="Aceptar" OnClick="btnAcpAprobar_Click"
                                            ValidationGroup="vgAcpAprobar" />
                                    </td>
                                    <td style="text-align: center">
                                        <asp:Button ID="btnCncAprobar" runat="server" CssClass="btn8" Text="Cancelar" OnClick="btnCncAprobar_Click" />
                                    </td>
                                </tr>
                            </table>

                        </asp:Panel>
                        <asp:Panel ID="panelAprobacionCom" runat="server">
                            <table style="width: 100%" cellpadding="1" cellspacing="1">
                                <tr>
                                    <td colspan="3" style="text-align: center">
                                        <strong>
                                            <asp:Label ID="Lblaprob" runat="server"></asp:Label></strong>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: left">No. Crédito:
                                    </td>
                                    <td style="text-align: left">
                                        <asp:TextBox ID="txtCredito3" runat="server" CssClass="textbox" Width="145px" Enabled="false"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvCredito3" runat="server" ControlToValidate="txtCredito3"
                                            Style="font-size: xx-small" Display="Dynamic" ErrorMessage="Campo Requerido"
                                            ForeColor="Red" SetFocusOnError="True" ValidationGroup="vgAcpAproModif" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: left; width: 20%">Observaciones:
                                    </td>
                                    <td style="text-align: left">
                                        <asp:TextBox ID="txtObs3" runat="server" CssClass="textbox" Width="300px" MaxLength="4000"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: left">Monto:
                                    </td>
                                    <td style="text-align: left">
                                        <uc2:decimales ID="txtMonto2" runat="server" CssClass="textbox" Width="145px"></uc2:decimales>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: left">Línea de Crédito:
                                    </td>
                                    <td style="text-align: left">
                                        <asp:DropDownList ID="ddlLineaCredito" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlLineaCredito_TextChanged"
                                            OnTextChanged="ddlLineaCredito_TextChanged" Width="305px" CssClass="textbox" Enabled="False">
                                        </asp:DropDownList>
                                    </td>
                                    <td style="text-align: left; width: 150px">
                                        <asp:CheckBox ID="Checkpoliza" runat="server" />
                                        <asp:Label ID="Label3" runat="server" Text=" ¿Desea tomar Poliza Microseguros?"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: left">Destinación de Crédito:
                                    </td>
                                    <td style="text-align: left">
                                        <asp:DropDownList ID="ddldestino2" runat="server" CssClass="textbox" Width="225px" />
                                    </td>
                                    <td style="text-align: left">
                                        <asp:CheckBox ID="chekpago" runat="server" />
                                        <asp:Label ID="lbl4" runat="server" Text=" Condicion especial de pago ?"></asp:Label>
                                    </td>

                                </tr>
                                <tr>
                                    <td style="text-align: left">Plazo crédito:
                                    </td>
                                    <td style="text-align: left">
                                        <asp:TextBox ID="txtPlazo2" runat="server" CssClass="textbox" Width="86px" MaxLength="5"></asp:TextBox>
                                        &nbsp;
                                        <asp:RequiredFieldValidator ID="rfvPlazo2" runat="server" ControlToValidate="txtPlazo2"
                                            Style="font-size: xx-small" Display="Dynamic" ErrorMessage="Campo Requerido"
                                            ForeColor="Red" SetFocusOnError="True" ValidationGroup="vgAcpAproModif" />
                                        <asp:DropDownList ID="ddlPlazo" runat="server" Width="150px" CssClass="textbox">
                                        </asp:DropDownList>
                                        &nbsp;<asp:CompareValidator ID="cvPlazo" runat="server" ControlToValidate="ddlPlazo"
                                            Display="Dynamic" ErrorMessage="Seleccione una periodicidad" Text="<strong>*</strong>"
                                            SetFocusOnError="true" Type="Integer" Operator="GreaterThan" ValueToCompare="0"
                                            ValidationGroup="vgAcpAproModif" ForeColor="Red">
                                        </asp:CompareValidator>
                                    </td>
                                    <td style="text-align: left; width: 150px">&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: left;">Fecha Primer Pago
                                    </td>
                                    <td style="text-align: left">
                                        <ucFecha:fecha ID="ucFechaPrimerPago" runat="server" style="text-align: left" />
                                    </td>
                                    <td style="text-align: left"></td>
                                </tr>
                                <tr>
                                    <td style="text-align: left">Forma de Pago:
                                    </td>
                                    <td style="text-align: left" colspan="2">
                                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                            <ContentTemplate>
                                                <asp:DropDownList ID="ddlFormaPago" runat="server" Width="225px" AutoPostBack="true"
                                                    OnSelectedIndexChanged="ddlFormaPago_SelectedIndexChanged" CssClass="textbox">
                                                    <asp:ListItem Value="C">Caja</asp:ListItem>
                                                    <asp:ListItem Value="N">Nomina</asp:ListItem>
                                                </asp:DropDownList>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3" style="text-align: left">Debito Automatico:
                                                 <asp:CheckBox ID="chkDebito" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3" style="text-align: left">
                                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                            <ContentTemplate>
                                                <asp:Panel ID="panelEmpresaRec" runat="server">
                                                    <strong>Empresa Recaudadora</strong><br />
                                                    <asp:GridView ID="gvEmpresaRecaudora" runat="server" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False"
                                                        PageSize="15" HeaderStyle-Height="25px" BackColor="White" BorderColor="#DEDFDE"
                                                        BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal"
                                                        Style="font-size: x-small">
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
                                                            <asp:TemplateField HeaderText="%" HeaderStyle-Width="80px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <cc1:TextBoxGrid ID="txtPorcentaje" runat="server" Width="50px" Style="text-align: right"
                                                                        OnTextChanged="txtPorcentaje_OnTextChanged" AutoPostBack="true" CssClass="textbox"
                                                                        MaxLength="5" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' />%
                                                                    <asp:FilteredTextBoxExtender ID="ftb12" runat="server" Enabled="True" FilterType="Numbers, Custom"
                                                                        TargetControlID="txtPorcentaje" ValidChars="," />
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Valor">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtValor" runat="server" Width="50px" Style="text-align: right; text-align: right"
                                                                        CssClass="textbox" MaxLength="12" Enabled="false" />
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
                                                <asp:AsyncPostBackTrigger ControlID="ddlFormaPago" EventName="SelectedIndexChanged" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: left" colspan="3">
                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                            <ContentTemplate>
                                                <asp:CheckBox ID="chkTasa" runat="server" Text="Actualizar Tasa" AutoPostBack="true"
                                                    OnCheckedChanged="chkTasa_CheckedChanged" />
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="chkTasa" EventName="CheckedChanged" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: left" colspan="3">
                                        <asp:UpdatePanel ID="updtipoTasa" runat="server">
                                            <ContentTemplate>
                                                <asp:RadioButtonList ID="rbCalculoTasa" runat="server" RepeatDirection="Horizontal"
                                                    Style="font-size: small" AutoPostBack="True" OnSelectedIndexChanged="rbCalculoTasa_SelectedIndexChanged">
                                                    <asp:ListItem Value="1">Tasa Fija</asp:ListItem>
                                                    <asp:ListItem Value="3" Enabled="false">Histórico Fijo</asp:ListItem>
                                                    <asp:ListItem Value="5" Enabled="false">Histórico Variable</asp:ListItem>
                                                </asp:RadioButtonList>
                                                <asp:Label ID="lblCalculoTasa" runat="server" Text="<strong>*</strong>" Style="color: Red"
                                                    Visible="false"></asp:Label>
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
                                                            <td style="text-align: left; width: 40%">Tipo Histórico<br />
                                                                <asp:DropDownList ID="ddlHistorico" runat="server" Width="224px" CssClass="textbox">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td style="text-align: left; width: 40%">Spread<br />
                                                                <asp:TextBox ID="txtDesviacion" runat="server" CssClass="textbox" Width="100px" />
                                                                <asp:FilteredTextBoxExtender ID="fte16" runat="server" Enabled="True" FilterType="Numbers, Custom"
                                                                    TargetControlID="txtDesviacion" ValidChars="," />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                                <asp:Panel ID="PanelFija" runat="server">
                                                    <table style="width: 80%">
                                                        <tr>
                                                            <td>
                                                                <asp:GridView ID="gvAtributos" runat="server" AutoGenerateColumns="False" BackColor="White"
                                                                    BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="0" ForeColor="Black" OnRowDataBound="gvAtributos_RowDataBound"
                                                                    ShowFooter="True" Style="font-size: xx-small; margin-right: 0px;" Width="80%"
                                                                    PageSize="2" DataKeyNames="cod_atr">
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
                                                                                    Visible="True"></asp:Label>

                                                                            </ItemTemplate>
                                                                            <ItemStyle HorizontalAlign="Center" Width="120px" />
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Tasa" ItemStyle-HorizontalAlign="Right">
                                                                            <ItemTemplate>
                                                                                <span>
                                                                                    <ucNumeroConDecimales:Decimal ID="txttasa" runat="server" CssClass="textbox" Style="font-size: xx-small; text-align: left"
                                                                                        Text='<%# Bind("tasa") %>' Width="100px"></ucNumeroConDecimales:Decimal>
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
                                                                                    <asp:Label ID="lbltipotasa" runat="server" Text='<%# Bind("tipotasaNom") %>' Visible="true" Width="120px"></asp:Label>
                                                                                    <cc1:DropDownListGrid ID="ddltipotasa" runat="server" AppendDataBoundItems="True"
                                                                                        CommandArgument="<%#Container.DataItemIndex %>" CssClass="textbox" Style="font-size: xx-small; text-align: left"
                                                                                        Width="120px" Visible="false">
                                                                                    </cc1:DropDownListGrid>
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
                                                            <td style="text-align: left; width: 40%"><%--Tasa<br />--%>
                                                                <%--   <ucNumeroConDecimales:Decimal ID="txtTasa" runat="server" Width="100px" />--%>
                                                                <%--<asp:TextBox ID="txtTasa" runat="server" CssClass="textbox" Width="100px" />
                                                                <asp:FilteredTextBoxExtender ID="ftb15" runat="server" Enabled="True" FilterType="Numbers, Custom"
                                                                    TargetControlID="txtTasa" ValidChars="." />--%>
                                                            </td>
                                                            <td style="text-align: left; width: 40%"><%--Tipo de Tasa<br />--%>
                                                                <%--      <asp:DropDownList ID="ddlTipoTasa" runat="server" Width="224px" CssClass="textbox" />--%>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="rbCalculoTasa" EventName="SelectedIndexChanged" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
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
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%; text-align: left" colspan="3">
                                        <asp:UpdatePanel ID="updDistribucion" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <strong>
                                                    <asp:CheckBox ID="chkDistribuir" runat="server" Text="Distribución de Giros" AutoPostBack="true"
                                                        OnCheckedChanged="chkDistribuir_CheckedChanged" />
                                                </strong>
                                                <asp:Panel ID="panelDistribucion" runat="server">
                                                    <div style="text-align: left">
                                                        <asp:Button ID="btnAdicionarFila" runat="server" CssClass="btn8" OnClick="btnAdicionarFila_Click"
                                                            OnClientClick="btnAdicionarFila_Click" Text="+ Adicionar Detalle" Height="25px" /><br />
                                                    </div>
                                                    <asp:GridView ID="gvDistribucion" runat="server" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False"
                                                        HeaderStyle-Height="30px" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None"
                                                        ShowFooter="True" DataKeyNames="idgiro" ForeColor="Black" GridLines="Horizontal"
                                                        Style="font-size: x-small" OnRowDeleting="gvDistribucion_RowDeleting" OnRowDataBound="gvDistribucion_RowDataBound">
                                                        <Columns>
                                                            <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/gr_elim.jpg" ShowDeleteButton="True">
                                                                <ItemStyle HorizontalAlign="left" />
                                                            </asp:CommandField>
                                                            <asp:TemplateField HeaderText="Código" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblCodigo" runat="server" Text='<%# Bind("idgiro") %>' />
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Identificación" HeaderStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblCod_persona" runat="server" Text='<%# Bind("cod_persona") %>' Visible="false" />
                                                                    <cc1:TextBoxGrid ID="txtIdentificacionD" runat="server" Width="120px" CssClass="textbox"
                                                                        CommandArgument='<%#Container.DataItemIndex %>' AutoPostBack="true" Text='<%# Bind("identificacion") %>'
                                                                        OnTextChanged="txtIdentificacionD_TextChanged" />
                                                                    <asp:FilteredTextBoxExtender ID="ftb120" runat="server" Enabled="True" FilterType="Numbers, Custom"
                                                                        TargetControlID="txtIdentificacionD" ValidChars="-" />
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Nombre" HeaderStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <cc1:TextBoxGrid ID="txtNombre" runat="server" Width="300px" CssClass="textbox" Text='<%# Bind("nombre") %>' />
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Tipo" HeaderStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblTipo" runat="server" Visible="false" Text='<%# Bind("tipo") %>' />
                                                                    <cc1:DropDownListGrid ID="ddlTipo" runat="server" CssClass="textbox" Width="130px" />
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    Total :
                                                                </FooterTemplate>
                                                                <ItemStyle HorizontalAlign="center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Valor" HeaderStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <uc1:decimalesGridRow ID="txtValor" runat="server" Text='<%# Eval("valor") %>' style="text-align: right"
                                                                        Habilitado="True" AutoPostBack_="True" Enabled="True" Width_="100" />
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="lblTotalVr" runat="server" />
                                                                </FooterTemplate>
                                                                <ItemStyle HorizontalAlign="Left" />
                                                                <FooterStyle HorizontalAlign="Right" />
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <FooterStyle CssClass="gridHeader" />
                                                        <HeaderStyle CssClass="gridHeader" />
                                                        <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                                                        <RowStyle CssClass="gridItem" />
                                                    </asp:GridView>
                                                    <asp:Label ID="lblErrorDist" runat="server" Style="font-size: x-small; color: Red" />
                                                </asp:Panel>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="chkDistribuir" EventName="CheckedChanged" />
                                                <asp:AsyncPostBackTrigger ControlID="btnAdicionarFila" EventName="Click" />
                                                <asp:AsyncPostBackTrigger ControlID="gvDistribucion" EventName="RowDeleting" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </td>

                                </tr>
                                <tr>
                                    <td style="width: 100%; text-align: left" colspan="3">
                                        <uc1:BuscarProveedor ID="ctlBusquedaProveedor" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3" style="text-align: left">
                                        <b>
                                            <asp:Label ID="lblError" runat="server" Style="color: Red;" Visible="false" /></b>
                                        <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="BulletList"
                                            ForeColor="Red" HeaderText="Errores:" ShowMessageBox="false" ShowSummary="true"
                                            ValidationGroup="vgAprModif" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: center" colspan="3">
                                        <asp:Button ID="btnAcpAproModif" runat="server" CssClass="btn8" Text="Aceptar" OnClick="btnAcpAproModif_Click"
                                            ValidationGroup="vgAcpAproModif" />
                                        &nbsp;&nbsp;&nbsp;
                                        <asp:HiddenField ID="hiddenOperacionCredito" runat="server" />
                                        <asp:Button ID="btnCncAproModif" runat="server" CssClass="btn8" Text="Cancelar" OnClick="btnCncAproModif_Click" />
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
                                    <td>No. Crédito:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtCredito4" runat="server" CssClass="textbox" Width="145px" Enabled="false"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvCredito4" runat="server" ControlToValidate="txtCredito4"
                                            Display="Dynamic" ErrorMessage="Campo Requerido" ForeColor="Red" SetFocusOnError="True"
                                            ValidationGroup="vgAplazar" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>Motivo de aplazamiento:
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlAplazar" runat="server" CssClass="dropdown" Width="150px">
                                        </asp:DropDownList>
                                        &nbsp;<asp:CompareValidator ID="cvAplaz" runat="server" ControlToValidate="ddlAplazar"
                                            Display="Dynamic" ErrorMessage="Seleccione un motivo de aplazamiento" Text="<strong>*</strong>"
                                            SetFocusOnError="true" Type="Integer" Operator="GreaterThan" ValueToCompare="0"
                                            ValidationGroup="vgAplazar" ForeColor="Red">
                                        </asp:CompareValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Observación:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtObs4" runat="server" CssClass="textbox" Width="145px" MaxLength="250"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:ValidationSummary ID="ValidationSummary3" runat="server" DisplayMode="BulletList"
                                            ForeColor="Red" HeaderText="Errores:" ShowMessageBox="false" ShowSummary="true"
                                            ValidationGroup="vgAplazar" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: center">
                                        <asp:Button ID="btnAcpAlazar" runat="server" CssClass="btn8" Text="Aceptar" OnClick="btnAcpAplazar_Click"
                                            ValidationGroup="vgAplazar" />
                                    </td>
                                    <td style="text-align: center">
                                        <asp:Button ID="btnAcpAplazar" runat="server" CssClass="btn8" Text="Cancelar" OnClick="btnCncAplaz_Click" />
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
                                    <td style="text-align: left">No. Crédito:
                                    </td>
                                    <td style="text-align: left">
                                        <asp:TextBox ID="txtCredito5" runat="server" CssClass="textbox" Width="145px" Enabled="false"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvCredito5" runat="server" ControlToValidate="txtCredito5"
                                            Display="Dynamic" ErrorMessage="Campo Requerido" ForeColor="Red" SetFocusOnError="True"
                                            ValidationGroup="vgNegar" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: left">Motivo de negación:
                                    </td>
                                    <td style="text-align: left">
                                        <asp:DropDownList ID="ddlNegar" runat="server" CssClass="dropdown" Width="150px">
                                        </asp:DropDownList>
                                        &nbsp;<asp:CompareValidator ID="cvNegar" runat="server" ControlToValidate="ddlNegar"
                                            Display="Dynamic" ErrorMessage="Seleccione un motivo de negacion" Text="<strong>*</strong>"
                                            SetFocusOnError="true" Type="Integer" Operator="GreaterThan" ValueToCompare="0"
                                            ValidationGroup="vgNegar" ForeColor="Red">
                                        </asp:CompareValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: left">Observación:
                                    </td>
                                    <td style="text-align: left">
                                        <asp:TextBox ID="txtObs5" runat="server" CssClass="textbox" Width="145px" MaxLength="250"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="text-align: center">
                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="BulletList"
                                            ForeColor="Red" HeaderText="Errores:" ShowMessageBox="false" ShowSummary="true"
                                            ValidationGroup="vgNegar" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: center">
                                        <asp:Button ID="btnAcpNegar" runat="server" CssClass="btn8" Text="Aceptar" OnClick="btnAcpNegar_Click"
                                            ValidationGroup="vgNegar" />
                                    </td>
                                    <td style="text-align: center">
                                        <asp:Button ID="btnCncNegar" runat="server" CssClass="btn8" Text="Cancelar" OnClick="btnCncNegar_Click" />
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

                <asp:Panel runat="server" Visible="false">
                    <table width="100%">
                        <tr>
                            <td>
                                <rsweb:ReportViewer ID="RptAprobacion" runat="server" Font-Names="Verdana" Font-Size="8pt"
                                    Height="500px" InteractiveDeviceInfos="(Colección)" WaitMessageFont-Names="Verdana"
                                    WaitMessageFont-Size="14pt" Width="96%" EnableViewState="True">
                                    <LocalReport ReportPath="Page\FabricaCreditos\CreditosPorAprobar\rptCreditoAprobacion.rdlc" />
                                </rsweb:ReportViewer>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
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
                            <asp:Button ID="btnContinuar" runat="server" Text="Continuar" OnClick="btnContinuar_Click" />
                            &#160;&#160;&#160;&#160;
                            <asp:Button ID="btnGenerarCarta" runat="server" Text="Desea Generar Cartas?" OnClick="btnGenerarCarta_Click" />
                            &#160;&#160;&#160;&#160;
                            <asp:Button ID="btnCorreo" runat="server" Text="Enviar Correo al Asociado" OnClick="btnCorreo_Click" />
                            &#160;&#160;&#160;&#160;
                            <asp:Button ID="btnImprimirAprobacion" runat="server" OnClick="btnImprimirAprobacion_Click"
                                Text="Imprimir Aprobación" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <asp:Panel ID="panelReporte" runat="server" Visible="false" Height="600px">
                                <div style="text-align: left">
                                    <asp:Button ID="btnVerData" runat="server" CssClass="btn8" Text="Cerrar Informe"
                                        OnClick="btnVerData_Click" Width="280px" Height="30px" />
                                </div>
                                <iframe id="frmPrint" name="IframeName" width="100%" src="../../Reportes/Reporte.aspx"
                                    height="100%" runat="server" style="border-style: groove; float: left;"></iframe>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </asp:View>
    </asp:MultiView>

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
                        <td style="font-size: x-small: color: #FFFFFF; background-color: #0066FF; width: 434px">
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
                                    <td style="width: 120px; background-color: #0066FF">
                                        <strong>Seleccionar Todo </strong>
                                        <input type="checkbox" id="selectAll" onchange="selectAl()" />
                                    </td>

                                </tr>
                            </table>
                        </td>
                        <tr>
                            <td style="width: 475px; margin-left: 120px; background-color: #0066FF">
                                <div class="scrolling-table-container" style="height: 378px; overflow-y: scroll; overflow-x: hidden;">
                                    <asp:GridView ID="gvAvances" runat="server" Width="99%" AutoGenerateColumns="False"
                                        AllowPaging="false" PageSize="20" GridLines="Horizontal" ShowHeaderWhenEmpty="True"
                                        Style="font-size: x-small" DataKeyNames="NumAvance">
                                        <Columns>
                                            <%-- IDAVANCE,FECHA_DESEMBOLSO,VALOR_DESEMBOLSADO,VALOR_CUOTA,PLAZO,SALDO_AVANCE--%>

                                            <asp:TemplateField HeaderText="Sel">
                                                <ItemTemplate>
                                                    <cc1:CheckBoxGrid ID="chkAvance" runat="server"
                                                        AutoPostBack="true" OnCheckedChanged="chkAvance_CheckedChanged" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' CssClass="ckavance" />
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

    <uc4:FormatoDocu ID="ctlFormatos" runat="server" />
    <script type='text/javascript'>
        function Forzar() {
            __doPostBack('', '');
        }
    </script>

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

        function selectAl() {
            var number = 0;
            if ($("#selectAll").is(":checked")) {
                $(".ckavance").children().attr("checked", true);
                console.log($("#cphMain_gvAvances tbody tr"));
                debugger;
                var s = $("#cphMain_gvAvances tbody tr");
                s.each(function () {
                    var d = s[number].innerText.split('	');
                    if (d[8] !== "Total Pagar") {
                        var total = Number($("#cphMain_TxtTotalAvances").val()) + Number(d[8]);
                        var totals = Number($("#cphMain_TxtTotalInt").val()) + Number(d[7]);
                        var totalq = Number($("#cphMain_TxtTotalCap").val()) + Number(d[6]);
                        $("#cphMain_TxtTotalAvances").val(total);
                        $("#cphMain_TxtTotalInt").val(totals);
                        $("#cphMain_TxtTotalCap").val(totalq);
                    }
                    number++;
                    console.log(d);
                });
            } else {
                $(".ckavance").children().attr("checked", false);
                $("#cphMain_TxtTotalAvances").val('');
                $("#cphMain_TxtTotalInt").val('');
                $("#cphMain_TxtTotalCap").val('');
            }
        }
    </script>


    <script src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.5.2.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        function validar() {


            var statusConfirm = confirm("¿Desea Crear Codeudor no existe en el Sistema?");
            if (statusConfirm == true) {
                window.open("../../Aportes/Personas/Nuevo.aspx", 'Codeudores', "resizable=yes ,width=500, height=450 align=center");
            }
        }
    </script>
</asp:Content>
