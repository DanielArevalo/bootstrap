<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" ValidateRequest="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Src="~/General/Controles/ctlPlanCuentas.ascx" TagName="ListadoPlanCtas" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/ctlPlanCuentasNif.ascx" TagName="ListadoPlanNif" TagPrefix="uc2" %>
<%@ Register Src="~/General/Controles/ctlBusquedaRapida.ascx" TagName="ListadoPersonas" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/decimalesGridRow.ascx" TagName="decimalesGridRow" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="uc1" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
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
    <script type="text/javascript" src="../../../Scripts/duplicate.js"></script>

    <script language="javascript" type="text/javascript">
        function mpeSeleccionOnOk() {
        }
        function mpeSeleccionOnCancel() {
        }
    </script>
    <script type="text/javascript">
        function Page_Load(textbox) {
            var str = textbox.value;
            var posDec = 0;
            var strDec = "";
            var formateado = "";
            str = str.replace(/\./g, "");
            posDec = str.indexOf(",");
            if (posDec > 0) {
                strDec = str.substring(posDec + 1, str.length);
                str = str.substring(0, posDec);
            }

            if (str != 0) {
                str = parseFloat(str);
                str = str.toString();

                if (str.length > 13) { str = str.substring(0, 13); }

                var long = str.length;
                var cen = str.substring(long - 3, long);
                var mil = str.substring(long - 6, long - 3);
                var mill = str.substring(long - 9, long - 6);
                var supmill = str.substring(0, long - 9);

                if (long > 0 && long <= 3) { formateado = parseInt(cen); }
                else if (long > 3 && long <= 6) { formateado = parseInt(mil) + "." + cen; }
                else if (long > 6 && long <= 9) { formateado = parseInt(mill) + "." + mil + "." + cen; }
                else if (long > 9 && long <= 13) { formateado = parseInt(supmill) + "." + mill + "." + mil + "." + cen; }
                else { formateado = ""; }

                if (posDec > 0 && formateado != "") {
                    formateado = formateado + "," + strDec;
                }
            }
            else {
                if (strDec != 0) { formateado = "0," + strDec; }
                else { formateado = ""; }
            }
            document.getElementById(textbox.id).value = formateado;
        }

    </script>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:HiddenField ID="hfgvDetMovsSV" runat="server" Visible="false" />
    <asp:HiddenField ID="hfgvDetMovsSH" runat="server" Visible="false" />
    <asp:Label ID="lablerror" runat="server" Visible="false" Style="font-size: x-small; text-align: left"></asp:Label>
    <asp:Label ID="lablerror0" runat="server" Visible="false" Style="font-size: x-small; text-align: left"></asp:Label>
    <asp:Panel ID="vwPanel0" runat="server">
        <table cellpadding="5" cellspacing="0" style="width: 100%">
            <tr>
                <td style="text-align: center; color: #FFFFFF; background-color: #0066FF">TIPO DE COMPROBANTE
                </td>
            </tr>
            <tr>
                <td>&nbsp;
                </td>
            </tr>
            <tr style="text-align: center">
                <td>Escoja el tipo de comprobante a generar
                </td>
                <td>&nbsp;
                </td>
            </tr>
            <tr>
                <td style="text-align: center">
                    <asp:RadioButton ID="rbIngreso" runat="server" Text="Comprobante de Ingreso" AutoPostBack="True"
                        OnCheckedChanged="rbIngreso_CheckedChanged" />
                </td>
                <td>&nbsp;
                </td>
            </tr>
            <tr>
                <td style="text-align: center">
                    <asp:RadioButton ID="rbEgreso" runat="server" Text="Comprobante de Egreso" AutoPostBack="True"
                        OnCheckedChanged="rbEgreso_CheckedChanged" />
                </td>
                <td>&nbsp;
                </td>
            </tr>
            <tr>
                <td style="text-align: center">
                    <asp:RadioButton ID="rbContable" runat="server" Text="Comprobante Contable" AutoPostBack="True"
                        OnCheckedChanged="rbContable_CheckedChanged" />
                </td>
                <td>&nbsp;
                </td>
            </tr>
            <tr>
                <td style="text-align: center">&nbsp;
                </td>
                <td>&nbsp;
                </td>
            </tr>
            <tr>
                <td style="text-align: center">
                    <asp:ImageButton ID="imgAceptar" runat="server" ImageUrl="~/Images/btnAceptar.jpg"
                        OnClick="imgAceptar_Click" />
                </td>
                <td>&nbsp;
                </td>
            </tr>
            <tr>
                <td style="text-align: center">&nbsp;
                </td>
                <td>&nbsp;
                </td>
            </tr>
        </table>
    </asp:Panel>

    <asp:Panel ID="vwPanel1" runat="server">
        <asp:UpdatePanel runat="server">
            <ContentTemplate>
                <table cellpadding="0" cellspacing="0" style="width: 100%">
                    <tr>
                        <td class="align-rt" colspan="8" style="font-size: medium; height: 21px; text-align: left;">
                            <strong>
                                <asp:Label ID="Lblerror" runat="server" ForeColor="Red" CssClass="align-rt"></asp:Label></strong>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Panel ID="Panel2" runat="server" Width="100%">
                                <table>
                                    <tr>
                                        <td style="font-size: medium; text-align: left;">
                                            <b>
                                                <asp:Label ID="lblComprobante" runat="server" Text="Comprobante No." Width="140px"></asp:Label></b>
                                        </td>
                                        <td style="text-align: left">
                                            <asp:TextBox ID="txtNumComp" runat="server" CssClass="textbox" Enabled="false" Width="120px"
                                                Font-Size="Small"></asp:TextBox>
                                        </td>
                                        <td style="text-align: right;">
                                            <asp:Label ID="lblTipoComp" runat="server" Style="text-align: left" Text="Tipo Comprob."></asp:Label>
                                            <asp:Label ID="lblSoporte" runat="server" Style="text-align: left" Text="Soporte"></asp:Label>
                                        </td>
                                        <td style="text-align: left;">
                                            <table cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td>
                                                        <asp:DropDownList ID="ddlTipoComprobante" runat="server" AutoPostBack="True" CssClass="textbox"
                                                            OnSelectedIndexChanged="ddlTipoComprobante_SelectedIndexChanged" Style="text-align: left"
                                                            Width="170px">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td>
                                                        <asp:UpdatePanel ID="upSoporte" runat="server">
                                                            <ContentTemplate>
                                                                <asp:TextBox ID="txtNumSop" runat="server" CssClass="textbox" Width="90px"></asp:TextBox>
                                                                <asp:FilteredTextBoxExtender ID="fte55" runat="server" Enabled="True" FilterType="Numbers, Custom"
                                                                    TargetControlID="txtNumSop" ValidChars="()-.," />
                                                            </ContentTemplate>
                                                            <Triggers>
                                                                <asp:PostBackTrigger ControlID="ddlCuenta" />
                                                            </Triggers>
                                                        </asp:UpdatePanel>
                                                    </td>
                                                    <td>
                                                        <asp:CheckBox ID="cbLocal" runat="server" Text="Local" Font-Size="XX-Small" Enabled="False" />&nbsp;
                                                <asp:CheckBox ID="cbNif" runat="server" Text="NIF" Font-Size="XX-Small" Enabled="False" />&nbsp;
                                                <asp:CheckBox ID="cbAmbos" runat="server" Text="Ambos" Font-Size="XX-Small" Enabled="False" />&nbsp;
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td style="text-align: right">Fecha
                                        </td>
                                        <td style="text-align: left">
                                            <asp:TextBox ID="txtFecha" runat="server" CssClass="textbox" Enabled="false" Width="70px"></asp:TextBox>
                                            <asp:MaskedEditExtender ID="mskFecha" runat="server" TargetControlID="txtFecha" Mask="99/99/9999"
                                                MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError"
                                                MaskType="Date" DisplayMoney="Left" AcceptNegative="Left" ErrorTooltipEnabled="True" />
                                            <asp:MaskedEditValidator ID="mevFecha" runat="server" ControlExtender="mskFecha"
                                                ControlToValidate="txtFecha" EmptyValueMessage="Fecha Requerida" InvalidValueMessage="Fecha No Valida"
                                                Display="Dynamic" TooltipMessage="Seleccione una Fecha" EmptyValueBlurredText="Fecha No Valida"
                                                InvalidValueBlurredMessage="Fecha No Valida" ValidationGroup="vgGuardar" ForeColor="Red" />
                                            <asp:CalendarExtender ID="txtFecha_CalendarExtender" runat="server" DaysModeTitleFormat="dd/MM/yyyy"
                                                Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtFecha" TodaysDateFormat="dd/MM/yyyy"></asp:CalendarExtender>
                                        </td>
                                        <td style="width: 4px">&nbsp;
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="cbImpresion" runat="server" Text="Impresión Resumida" Font-Size="XX-Small" />
                                        </td>
                                        <caption>
                                            &nbsp;
                                            <tr>
                                                <td>&nbsp; </td>
                                            </tr>
                                        </caption>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <hr />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Panel ID="Panel3" runat="server" Width="80%">
                                <table>
                                    <tr>
                                        <td style="text-align: left;">
                                            <asp:Label ID="lblCiudad" runat="server" Style="text-align: left" Text="Ciudad" Width="80px"></asp:Label>
                                        </td>
                                        <td style="text-align: left">
                                            <asp:DropDownList ID="ddlCiudad" runat="server" CssClass="textbox" Style="text-align: left"
                                                Width="320px">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="text-align: left" colspan="2">&nbsp;
                                        </td>
                                        <td style="text-align: left" colspan="2">Oficina
                                        </td>
                                        <td style="text-align: left">
                                            <asp:TextBox ID="tbxOficina" runat="server" CssClass="textbox" Enabled="false" Width="308px"
                                                Style="text-align: left"></asp:TextBox>
                                        </td>
                                        <td style="text-align: left">&nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: left">Concepto
                                        </td>
                                        <td style="text-align: left" colspan="6">
                                            <asp:DropDownList ID="ddlConcepto" runat="server" CssClass="textbox" Style="text-align: left"
                                                Width="320px">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="text-align: left">&nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: left;">
                                            <asp:Label ID="lblFormaPago" runat="server" Style="text-align: left" Text="Forma Pago"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlFormaPago" runat="server" AutoPostBack="True" CssClass="textbox"
                                                OnSelectedIndexChanged="ddlFormaPago_SelectedIndexChanged" Style="text-align: left"
                                                Width="321px">
                                            </asp:DropDownList>
                                            <asp:Label ID="lblIdGiro" runat="server" Visible="false" />
                                        </td>
                                        <td style="text-align: left" colspan="2">&nbsp;
                                        </td>
                                        <td style="text-align: left" colspan="2">
                                            <asp:TextBox ID="tbxTelefono" runat="server" CssClass="textbox" Enabled="false" Style="text-align: left" Visible="false" Width="18px"></asp:TextBox>
                                        </td>
                                        <td style="text-align: left">
                                            <asp:TextBox ID="tbxDireccion" runat="server" CssClass="textbox" Enabled="false" Visible="false" Style="text-align: left" Width="28px"></asp:TextBox>
                                        </td>
                                        <td style="text-align: left">&nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: left">
                                            <asp:Label ID="labelNumeroFactura" runat="server" Visible="false" Style="text-align: left" Text="Numero Factura"></asp:Label>
                                        </td>
                                        <td style="text-align: left" colspan="6">
                                            <asp:TextBox runat="server" ID="txtNumeroFactura" Visible="false" CssClass="textbox" Style="text-align: left" Width="310px" />
                                            <asp:DropDownList ID="ddlNumeroFacturas" runat="server" Visible="false" CssClass="textbox" Style="text-align: left" Width="310px">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="text-align: left">&nbsp;
                                        </td>
                                    </tr>

                                </table>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Panel ID="mvBamco" runat="server">
                                                <table>
                                                    <tr>
                                                        <td style="text-align: left; width: 110px">
                                                            <asp:Label ID="lblEntidad" runat="server" Style="text-align: left" Text="Entidad de Giro"></asp:Label>
                                                        </td>
                                                        <td style="text-align: left; width: 250px">
                                                            <asp:UpdatePanel ID="upEntidad" runat="server">
                                                                <ContentTemplate>
                                                                    <asp:DropDownList ID="ddlEntidadOrigen" runat="server" AutoPostBack="True" CssClass="textbox"
                                                                        OnSelectedIndexChanged="ddlEntidadOrigen_SelectedIndexChanged" Style="text-align: left"
                                                                        Width="93%">
                                                                    </asp:DropDownList>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </td>
                                                        <td style="text-align: left; width: 70px">
                                                            <asp:Label ID="lblCuenta" runat="server" Text="Cuenta" Style="text-align: left"></asp:Label>
                                                        </td>
                                                        <td style="text-align: left; width: 250px">
                                                            <asp:UpdatePanel ID="upCuentaBancaria" runat="server">
                                                                <ContentTemplate>
                                                                    <asp:DropDownList ID="ddlCuenta" runat="server" CssClass="textbox" Style="text-align: left"
                                                                        Width="95%" OnSelectedIndexChanged="ddlCuenta_SelectedIndexChanged" AutoPostBack="True">
                                                                    </asp:DropDownList>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </td>
                                                        <td style="text-align: left; width: 110px">&nbsp;
                                                        </td>
                                                        <td style="text-align: left; width: 180px">&nbsp;
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Panel ID="panelTransferencia" runat="server">
                                                <table>
                                                    <tr>
                                                        <td style="text-align: left; width: 110px">Num de Cuenta<br />
                                                        </td>
                                                        <td style="text-align: left; width: 250px">
                                                            <asp:TextBox ID="txtNum_cuenta" runat="server" CssClass="textbox" Width="90%" />
                                                        </td>
                                                        <td style="text-align: left; width: 70px">Entidad<br />
                                                        </td>
                                                        <td style="text-align: left; width: 250px">
                                                            <asp:DropDownList ID="ddlEntidad" runat="server" CssClass="textbox" Style="text-align: left"
                                                                Width="95%" />
                                                        </td>
                                                        <td style="text-align: left; width: 110px">Tipo de Cuenta<br />
                                                        </td>
                                                        <td style="text-align: left; width: 180px">
                                                            <asp:DropDownList ID="ddlTipo_cuenta" runat="server" CssClass="textbox" Style="text-align: left"
                                                                Width="90%" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <hr />
                            <asp:Panel ID="panBeneficiario" runat="server" Width="815px">
                                <table style="width: 100%;">
                                    <tr>
                                        <td colspan="4" style="text-align: left">
                                            <strong>Beneficiario</strong>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 108px; text-align: left">Identificación
                                        </td>
                                        <td style="width: 237px; text-align: left">Tipo Identificación
                                        </td>
                                        <td colspan="2" style="text-align: left">Nombres y Apellidos
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 108px; text-align: left">
                                            <asp:TextBox ID="txtCodigo" runat="server" Visible="false" />
                                            <asp:TextBox ID="txtIdentificacion" runat="server" CssClass="textbox" Width="125px"
                                                Style="text-align: left" AutoPostBack="True" OnTextChanged="txtIdentificacion_TextChanged" />
                                        </td>
                                        <td style="width: 237px; text-align: left">
                                            <asp:DropDownList ID="ddlTipoIdentificacion" runat="server" CssClass="textbox" Width="156px"
                                                Style="text-align: left">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="text-align: left">
                                            <asp:TextBox ID="txtNombres" runat="server" CssClass="textbox" Width="474px" Style="text-align: left"
                                                Enabled="False"></asp:TextBox>
                                        </td>
                                        <td style="text-align: left">
                                            <asp:Button ID="btnConsultaPersonas" CssClass="btn8" runat="server" Text="..." Height="26px"
                                                OnClick="btnConsultaPersonas_Click" />
                                        </td>
                                    </tr>
                                </table>
                                <uc1:ListadoPersonas ID="ctlBusquedaPersonas" runat="server" />
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
        <hr />
        <asp:UpdatePanel runat="server" RenderMode="Inline" UpdateMode="Conditional">
            <Triggers>
                <asp:PostBackTrigger ControlID="btnExportarDetalle" />
            </Triggers>
            <ContentTemplate>
                <table cellpadding="0" cellspacing="0" style="width: 100%">
                    <tr>
                        <td style="text-align: left">
                            <strong style="text-align: left">Detalle</strong><br />
                            <asp:Button ID="btnDetalle" runat="server" CssClass="btn8" OnClick="btnDetalle_Click"
                                OnClientClick="btnDetalle_Click" Text="+ Adicionar Detalle" />
                            <asp:Button ID="btnExportarDetalle" runat="server" CssClass="btn8" OnClick="btnExportarDet_Click"
                                Text="Exportar a csv" />
                            <br />
                        </td>
                    </tr>
                </table>
                <table cellpadding="0" cellspacing="0" style="width: 100%">
                    <tr>
                        <td style="text-align: left">
                            <div style="overflow: scroll; height: 330px; width: 100%; margin-right: 0px;">
                                <asp:GridView ID="gvDetMovs" runat="server" AutoGenerateColumns="false" BackColor="White"
                                    BorderColor="#DEDFDE" BorderStyle="None" PageSize="10" AllowPaging="True" BorderWidth="1px"
                                    GridLines="Both" CellPadding="0" ForeColor="Black" ShowFooter="True" Style="font-size: xx-small"
                                    Width="100%" DataKeyNames="codigo" OnRowDeleting="gvDetMovs_RowDeleting" OnPageIndexChanging="gvDetMovs_PageIndexChanging"
                                    OnRowDataBound="gvDetMovs_RowDataBound">
                                    <AlternatingRowStyle BackColor="White" />
                                    <Columns>
                                        <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/gr_elim.jpg" ShowDeleteButton="True"
                                            ItemStyle-Width="20px" />
                                        <asp:TemplateField HeaderText="Cod. Cuenta" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <table cellpadding="2" cellspacing="0">
                                                    <tr>
                                                        <td>
                                                            <cc1:TextBoxGrid ID="txtCodCuenta" runat="server" AutoPostBack="True" Style="font-size: xx-small; text-align: left"
                                                                BackColor="#F4F5FF" Width="80px" Text='<%# Bind("cod_cuenta") %>'
                                                                OnTextChanged="txtCodCuenta_TextChanged" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'>  
                                                            </cc1:TextBoxGrid><cc1:ButtonGrid ID="btnListadoPlan" CssClass="btnListado" runat="server"
                                                                Text="..." OnClick="btnListadoPlan_Click" CommandArgument='<%#((GridViewRow) Container).RowIndex %>' />
                                                            <uc1:ListadoPlanCtas ID="ctlListadoPlan" runat="server" OneventotxtCuenta_TextChanged="txtCodCuenta_TextChanged" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <cc1:TextBoxGrid ID="ddlNomCuenta" runat="server" AutoPostBack="true" Style="font-size: xx-small; text-align: left"
                                                                BackColor="#F4F5FF" Width="180px" Text='<%# Bind("nombre_cuenta") %>' CommandArgument='<%#Container.DataItemIndex %>'
                                                                Enabled="False">
                                                            </cc1:TextBoxGrid><asp:Label ID="lblManejaTer" runat="server" Style="font-size: xx-small; text-align: left"
                                                                Text='<%# Bind("maneja_ter") %>' Width="5" Visible="False"></asp:Label>
                                                            <asp:Label ID="lblImpuesto" runat="server" Style="font-size: xx-small; text-align: left"
                                                                Text='<%# Bind("impuesto") %>' Width="5" Visible="False"></asp:Label>

                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="180px" />

                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cod.Cuenta NIF" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <table cellpadding="2" cellspacing="0">
                                                    <tr>
                                                        <td>
                                                            <cc1:TextBoxGrid ID="txtCodCuentaNIF" runat="server" AutoPostBack="True" Style="font-size: xx-small; text-align: left"
                                                                BackColor="#F4F5FF" Width="80px" Text='<%# Bind("cod_cuenta_niif") %>'
                                                                OnTextChanged="txtCodCuentaNIF_TextChanged" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'>    
                                                            </cc1:TextBoxGrid><cc1:ButtonGrid ID="btnListadoPlanNIF" CssClass="btnListado" runat="server"
                                                                Text="..." OnClick="btnListadoPlanNIF_Click" CommandArgument='<%#((GridViewRow) Container).RowIndex %>' />
                                                            <uc2:ListadoPlanNif ID="ctlListadoPlanNif" runat="server" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <cc1:TextBoxGrid ID="txtNomCuentaNif" runat="server" Style="font-size: xx-small; text-align: left"
                                                                BackColor="#F4F5FF" Width="180px" Text='<%# Bind("nombre_cuenta_nif") %>'
                                                                CommandArgument='<%#Container.DataItemIndex %>' Enabled="False">
                                                            </cc1:TextBoxGrid>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="180px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="C/C" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <table cellpadding="2" cellspacing="0">
                                                    <tr>
                                                        <td>C/C:
                                                        </td>
                                                        <td>
                                                            <cc1:DropDownListGrid ID="ddlCentroCosto" runat="server" Style="font-size: xx-small; text-align: center"
                                                                CssClass="dropdown" Width="40px" DataSource="<%# ListaCentrosCosto() %>"
                                                                DataTextField="centro_costo" DataValueField="centro_costo" SelectedValue='<%# Bind("centro_costo") %>'
                                                                CommandArgument='<%#Container.DataItemIndex %>' AppendDataBoundItems="True">
                                                                <asp:ListItem Value=""></asp:ListItem>
                                                            </cc1:DropDownListGrid>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>C/G:
                                                        </td>
                                                        <td>
                                                            <cc1:DropDownListGrid ID="ddlCentroGestion" runat="server" Style="font-size: xx-small; text-align: center"
                                                                CssClass="dropdown" Width="40px" DataSource="<%# ListaCentroGestion() %>"
                                                                DataTextField="centro_gestion" DataValueField="centro_gestion" SelectedValue='<%# Bind("centro_gestion") %>'
                                                                CommandArgument='<%#Container.DataItemIndex %>' AppendDataBoundItems="True">
                                                                <asp:ListItem Value="0"></asp:ListItem>
                                                                <asp:ListItem Value=""></asp:ListItem>
                                                            </cc1:DropDownListGrid>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="60px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Detalle" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtDetalle" runat="server" Style="font-size: xx-small; text-align: left"
                                                    Width="200px" Text='<%# Bind("detalle") %>' TextMode="MultiLine" Rows="2" Height="28"></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="200px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="T.Mov." HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:UpdatePanel ID="UPDATE1" runat="server" UpdateMode="Always">
                                                    <ContentTemplate>
                                                        <cc1:DropDownListGrid MaxLength="10" ID="ddlTipo" runat="server" Style="font-size: xx-small; text-align: center"
                                                            Width="40px" CssClass="dropdown" SelectedValue='<%# Bind("tipo") %>'
                                                            AutoPostBack="True" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'
                                                            OnSelectedIndexChanged="ddlTipo_SelectedIndexChanged">
                                                            <asp:ListItem Value="D">D</asp:ListItem>
                                                            <asp:ListItem Value="C">C</asp:ListItem>
                                                        </cc1:DropDownListGrid>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="40px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Valor" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <table cellpadding="2" cellspacing="0">
                                                    <tr>
                                                        <td>
                                                            <cc1:TextBoxGrid ID="txtValor" runat="server" Text='<%# Eval("valor") %>' Style="font-size: xx-small; text-align: right"
                                                                TipoLetra="XX-Small" Habilitado="True" AutoPostBack="True"
                                                                onblur="Page_Load(this)" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'
                                                                OnPreRender="txtValor_PreRender" Width="80px" OnTextChanged="txtValor_TextChangeds"
                                                                Enabled="True" Width_="80" />
                                                            <asp:FilteredTextBoxExtender ID="txtValor_FilteredTextBoxExtender" runat="server"
                                                                Enabled="True" FilterType="Custom" TargetControlID="txtValor" ValidChars="0123456789.,"></asp:FilteredTextBoxExtender>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <cc1:DropDownListGrid ID="ddlMoneda" runat="server" Style="font-size: xx-small; text-align: left"
                                                                CssClass="dropdown" Width="83px" DataSource="<%# ListaMonedas() %>"
                                                                DataTextField="descripcion" DataValueField="cod_moneda" SelectedValue='<%# Bind("moneda") %>'
                                                                CommandArgument='<%#Container.DataItemIndex %>' AppendDataBoundItems="True">
                                                                <asp:ListItem Value=""></asp:ListItem>
                                                            </cc1:DropDownListGrid>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" Width="80px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Tercero" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <table cellpadding="2" cellspacing="0">
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblTercero" runat="server" Style="font-size: xx-small; text-align: left"
                                                                Text='<%# Bind("tercero") %>' Width="40" Visible="false" />
                                                        </td>
                                                        <td>
                                                            <cc1:TextBoxGrid ID="txtIdentificD" runat="server" AutoPostBack="True" Width="60px"
                                                                Style="font-size: xx-small; text-align: left" Text='<%# Bind("identificacion") %>'
                                                                OnTextChanged="txtIdentificD_TextChanged" CommandArgument='<%#Container.DataItemIndex %>' />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <asp:Label ID="lblNombreTercero" runat="server" Style="font-size: xx-small; text-align: left"
                                                                Text='<%# Bind("nom_tercero") %>' />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="180px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Impuestos">
                                            <ItemTemplate>
                                                <table cellpadding="2" cellspacing="0">
                                                    <tr>
                                                        <td style="text-align: left">
                                                            <asp:Label ID="lblBase" runat="server" Style="font-size: xx-small; text-align: left"
                                                                Text="Base" Visible="false" />
                                                        </td>
                                                        <td style="text-align: left">
                                                            <asp:TextBox ID="txtBaseGrid" runat="server" Width="70px" CssClass="textbox" Text='<%# Bind("base_comp") %>'
                                                                Height="9px" Style="text-align: right; font-size: xx-small" Visible='<%# Bind("isVisible_base_comp") %>'></asp:TextBox>
                                                            <asp:FilteredTextBoxExtender ID="fte1" runat="server" Enabled="True" FilterType="Numbers, Custom"
                                                                TargetControlID="txtBaseGrid" ValidChars=",." />
                                                        </td>
                                                        <td rowspan="2" style="text-align: right">
                                                            <cc1:ButtonGrid ID="btnGenerarImpuesto" runat="server" Height="20px" Style="font-size: xx-small; text-align: left"
                                                                Text="Generar" Visible="true" AutoPostback="true" OnClick="btnGenerarImpuesto_Click"
                                                                CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: left">
                                                            <asp:Label ID="lblPorcentaje" runat="server" Style="font-size: xx-small; text-align: left"
                                                                Text="Porcent." Visible="false" />
                                                        </td>
                                                        <td style="text-align: left">
                                                            <asp:TextBox ID="txtPorcentGrid" runat="server" Width="50px" CssClass="textbox" Enabled="false"
                                                                Text='<%# Bind("porcentaje") %>' Height="9px" Style="text-align: right; font-size: xx-small"
                                                                Visible='true'></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="180px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Producto" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCodTipoProducto" runat="server" Text='<%# Bind("cod_tipo_producto") %>' Visible="true"></asp:Label><br />
                                                <asp:Label ID="lblNumeroTransaccion" runat="server" Text='<%# Bind("numero_transaccion") %>' Visible="true" Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <PagerStyle CssClass="pagerstyle" />
                                    <PagerTemplate>
                                        <asp:Label ID="lblFilas" runat="server" Text="Mostrar filas:" />
                                        <asp:DropDownList CssClass="letranormal" ID="RegsPag" runat="server" AutoPostBack="true"
                                            OnSelectedIndexChanged="RegsPag_SelectedIndexChanged">
                                            <asp:ListItem Value="10" />
                                            <asp:ListItem Value="15" />
                                            <asp:ListItem Value="20" />
                                        </asp:DropDownList>
                                        &nbsp; Ir a
                                        <asp:TextBox ID="IraPag" runat="server" AutoPostBack="true" OnTextChanged="IraPag"
                                            CssClass="gopag" />
                                        de
                                        <asp:Label ID="lblTotalNumberOfPages" runat="server" />
                                        &nbsp;
                                        <asp:ImageButton runat="server" ID="btnTxt" ImageUrl="~/Images/btnCancelar.jpg" OnClick="btnTxt" Visible="false" />
                                        <asp:Button ID="btnPrimero" runat="server" CommandName="Page" ToolTip="Prim. Pag"
                                            CommandArgument="First" CssClass="pagfirst" />
                                        <asp:Button ID="btnAnterior" runat="server" CommandName="Page" ToolTip="Pág. anterior"
                                            CommandArgument="Prev" CssClass="pagprev" />
                                        <asp:Button ID="btnSiguiente" runat="server" CommandName="Page" ToolTip="Sig. página"
                                            CommandArgument="Next" CssClass="pagnext" />
                                        <asp:Button ID="btnUltimo" runat="server" CommandName="Page" ToolTip="Últ. Pag" CommandArgument="Last"
                                            CssClass="paglast" />
                                    </PagerTemplate>
                                    <AlternatingRowStyle CssClass="altrow" />
                                    <FooterStyle CssClass="gridHeader" />
                                    <HeaderStyle CssClass="gridHeader" />
                                    <RowStyle CssClass="gridItem" />
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
                <asp:HiddenField ID="hfImpuestos" runat="server" />
                <asp:ModalPopupExtender ID="mpeImpuestos" runat="server" DropShadow="True" Drag="True"
                    PopupControlID="panelImpuestos" TargetControlID="hfImpuestos" BackgroundCssClass="backgroundColor">
                </asp:ModalPopupExtender>
                <asp:Panel ID="panelImpuestos" runat="server" BackColor="White" BorderColor="Black"
                    Style="text-align: right" BorderWidth="1px">
                    <%--<asp:UpdatePanel ID="updImpusto" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>--%>
                    <div id="popupcontainer" style="border-style: outset; text-align: center;">
                        <table>
                            <tr>
                                <td style="text-align: left">
                                    <asp:GridView ID="gvImpuestos" runat="server" AutoGenerateColumns="false" HeaderStyle-Height="25px"
                                        BorderStyle="None" BorderWidth="0px" CellPadding="0" DataKeyNames="idimpuesto"
                                        ForeColor="Black" GridLines="None" OnRowDataBound="gvImpuestos_RowDataBound"
                                        OnRowDeleting="gvImpuestos_RowDeleting" ShowFooter="False" Style="font-size: xx-small">
                                        <Columns>
                                            <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/gr_elim.jpg" ShowDeleteButton="True" />
                                            <asp:TemplateField HeaderText="Activ" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCodigo" runat="server" Text='<%# Bind("idimpuesto") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Tipo de Impuesto" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTipoImpuesto" runat="server" Text='<%# Bind("cod_tipo_impuesto") %>'
                                                        Visible="false"></asp:Label>
                                                    <cc1:DropDownListGrid ID="ddlTipoImpuesto" runat="server" Enabled="false" AppendDataBoundItems="True"
                                                        CommandArgument="<%#Container.DataItemIndex %>" CssClass="textbox" Style="font-size: xx-small; text-align: left"
                                                        Width="160px">
                                                    </cc1:DropDownListGrid>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Base" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:UpdatePanel ID="updBas3" runat="server">
                                                        <ContentTemplate>
                                                            <cc1:TextBoxGrid ID="txtBase" runat="server" AutoPostBack="true" OnTextChanged="txtBase_TextChanged"
                                                                CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' Style="text-align: right"
                                                                Width="100px" />
                                                            <asp:FilteredTextBoxExtender ID="fteBase" runat="server" Enabled="True" FilterType="Numbers, Custom"
                                                                TargetControlID="txtBase" ValidChars="," />
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="% Impuesto" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtPorcentajeImpuesto" runat="server" CssClass="textbox" Width="60px"
                                                        Enabled="false" Style="text-align: right" Text='<%# Bind("porcentaje_impuesto") %>' />
                                                    <asp:FilteredTextBoxExtender ID="fte1" runat="server" Enabled="True" FilterType="Numbers, Custom"
                                                        TargetControlID="txtPorcentajeImpuesto" ValidChars="," />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Asumido" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <cc1:CheckBoxGrid ID="cbAsumido" runat="server" Enabled="false" AutoPostBack="false"
                                                        CommandArgument='<%#Container.DataItemIndex %>' Checked='<%#Convert.ToBoolean(Eval("asumido"))%>' />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Valor" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:UpdatePanel ID="updVal" runat="server">
                                                        <ContentTemplate>
                                                            <uc1:decimales ID="txtValorImp" runat="server" Enabled="false" Width="100px"></uc1:decimales>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="txtBase" EventName="TextChanged" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Cod. Cuenta" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <cc1:TextBoxGrid ID="txtCodCuenta_imp" runat="server" Width="90px" CssClass="textbox"
                                                        Enabled="false" Text='<%# Bind("cod_cuenta_imp") %>' CommandArgument='<%#((GridViewRow) Container).RowIndex %>'>  
                                                    </cc1:TextBoxGrid>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Cod. Cuenta Asumido" ItemStyle-HorizontalAlign="center"
                                                HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <cc1:TextBoxGrid ID="txtCodCuenta_imps" runat="server" Width="100px" CssClass="textbox"
                                                        Enabled="false" Text='<%# Bind("cod_cuenta_asumido") %>' CommandArgument='<%#((GridViewRow) Container).RowIndex %>'>  
                                                    </cc1:TextBoxGrid>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <HeaderStyle CssClass="gridHeader" />
                                        <RowStyle CssClass="gridItem" />
                                        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblInfo" runat="server" Text="Su consulta no obtuvo ningun resultado."
                                        Visible="False" />
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: center">
                                    <asp:Button ID="btnGenerarDa" runat="server" CssClass="btn8" Text="Generar" Height="30px"
                                        Width="90px" OnClick="btnGenerarDa_Click" />
                                    &#160;<asp:Button ID="btnCerrarVent" runat="server" CssClass="btn8" Text="Cerrar"
                                        Height="30px" Width="90px" OnClick="btnCerrarVent_Click" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </asp:Panel>
                <asp:Panel ID="PanelObservaciones" runat="server" Width="80%">
                    <table>
                        <tr>
                            <td valign="top" rowspan="2" style="text-align: left;">
                                <strong>Observaciones</strong><br />
                                <asp:TextBox ID="tbxObservaciones" runat="server" Height="40px" TextMode="MultiLine"
                                    Width="400px"></asp:TextBox>
                            </td>
                            <td style="text-align: left">Diferencia
                            </td>
                            <td style="text-align: left">Total Debitos
                            </td>
                            <td style="text-align: left">Total Creditos
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left">
                                <asp:TextBox ID="tbxDiferencia" runat="server" Enabled="false" Style="text-align: right"
                                    Width="120px" CssClass="textbox" />
                            </td>
                            <td style="text-align: left">
                                <asp:TextBox ID="tbxTotalDebitos" runat="server" Enabled="false" Style="text-align: right"
                                    Width="130px" CssClass="textbox" />
                            </td>
                            <td style="text-align: left">
                                <asp:TextBox ID="tbxTotalCreditos" runat="server" Enabled="false" Style="text-align: right"
                                    Width="130px" CssClass="textbox" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>

            </ContentTemplate>

            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnGenerarDa" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="gvImpuestos" EventName="RowDeleting" />
            </Triggers>

        </asp:UpdatePanel>
        <hr />
        <asp:Panel ID="PanelElaborado" runat="server" Width="80%">
            <table>
                <tr>
                    <td style="text-align: left">Elaborado Por
                    </td>
                    <td style="text-align: left">
                        <asp:TextBox ID="txtCodElabora" runat="server" CssClass="textbox" Visible="False"
                            Width="23px"></asp:TextBox>
                        <asp:TextBox ID="txtElaboradoPor" runat="server" CssClass="textbox" Width="477px"
                            Style="text-align: left" Enabled="False"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txtEstado" runat="server" CssClass="textbox" Visible="False" Width="23px"
                            Enabled="False" Style="text-align: left"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txtCodAprobo" runat="server" CssClass="textbox" Visible="False"
                            Width="23px" Enabled="False" Style="text-align: left"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <hr />
        <asp:Panel ID="PanelFooter" runat="server" Width="70%">
            <table>
                <tr>
                    <td style="text-align: left" colspan="2">
                        <span style="font-weight: bold">Beneficiario del Cheque</span>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td style="text-align: left">Identificación
                    </td>
                    <td colspan="2" style="text-align: left">
                        <asp:TextBox ID="txtidenti" runat="server" CssClass="textbox" Style="text-align: left"
                            Width="150px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left; width: 120px">Tipo Identificaciòn
                    </td>
                    <td colspan="2" style="text-align: left">
                        <asp:DropDownList ID="ddlTipoIdentificacion0" runat="server" CssClass="textbox" Style="text-align: left"
                            Width="156px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left">Nombre Completo
                    </td>
                    <td colspan="2" style="text-align: left">
                        <asp:TextBox ID="txtnom" runat="server" CssClass="textbox" Style="text-align: left"
                            Width="477px"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <hr />
    </asp:Panel>

    <asp:Panel ID="vwPanel2" runat="server">
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
                <td style="text-align: center; font-size: large; height: 29px;">
                    <asp:Label ID="lblMensajeGrabar" runat="server" Text="Comprobante Grabado Correctamente"></asp:Label>
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
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnImprimir" runat="server" Text="Imprimir" OnClick="btnInforme_Click" />
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnImpriOrden" runat="server" Text="Imprimir Orden" OnClick="btnImpriOrden_Click"
                        Visible="false" />
                    <asp:Button ID="btnRecibo" runat="server" Text="Imprimir Recibo" OnClick="btnImpriReci_Click" Visible="true" />
                </td>
            </tr>
        </table>
    </asp:Panel>

    <asp:Panel ID="vwPanel3" runat="server">
        <table style="width: 100%;">
            <tr>
                <td>&nbsp;
                </td>
                <td style="text-align: center">
                    <br />
                    <br />
                    <br />
                    <br />
                </td>
                <td>&nbsp;
                </td>
            </tr>
            <tr>
                <td>&nbsp;
                </td>
                <td style="text-align: center">&nbsp;
                </td>
                <td>&nbsp;
                </td>
            </tr>
            <tr>
                <td>&nbsp;
                </td>
                <td style="text-align: center">Por favor seleccione el tipo de comprobante y concepto deseado para el proceso
                </td>
                <td>&nbsp;
                </td>
            </tr>
            <tr>
                <td>&nbsp;
                </td>
                <td style="text-align: center">
                    <asp:Label ID="lblProceso" runat="server" Text=""></asp:Label>
                </td>
                <td>&nbsp;
                </td>
            </tr>
            <tr>
                <td>&nbsp;
                </td>
                <td style="text-align: center">&nbsp;
                </td>
                <td>&nbsp;
                </td>
            </tr>
            <tr>
                <td>&nbsp;
                </td>
                <td style="text-align: center">
                    <asp:ListBox ID="lstProcesos" runat="server" Width="396px" Height="143px"></asp:ListBox>
                    <br />
                </td>
                <td>&nbsp;
                </td>
            </tr>
            <tr>
                <td>&nbsp;
                </td>
                <td style="text-align: center; font-weight: 700;">
                    <asp:ImageButton ID="imgAceptarProceso" runat="server" ImageUrl="~/Images/btnAceptar.jpg"
                        OnClick="imgAceptarProceso_Click" />
                    &nbsp;&nbsp;&nbsp;
                </td>
                <td>&nbsp;
                </td>
            </tr>
        </table>
    </asp:Panel>

    <asp:Panel ID="vwPanel4" runat="server">
        <br />
        <br />
        <br />
        <table width="100%">
            <tr>
                <td style="text-align: left">
                    <asp:Button ID="btnRegresarComp" runat="server" CssClass="btn8" OnClick="btnRegresarComp_Click"
                        Text="Regresar al Comprobante" Height="25px" />
                    &#160;&#160;
                    <asp:Button ID="btnImprime" runat="server" CssClass="btn8" Height="25px" Width="120px"
                        Text="Imprimir" OnClick="btnImprime_Click" />
                </td>
            </tr>
            <tr>
                <td>
                    <iframe id="frmPrint" name="IframeName" width="100%" src="../../Reportes/Reporte.aspx"
                        height="500px" runat="server" style="border-style: dotted; float: left;"></iframe>
                </td>
            </tr>
            <tr>
                <td>
                    <rsweb:ReportViewer ID="RpviewComprobante" runat="server" Font-Names="Verdana" Font-Size="8pt"
                        Height="450px" InteractiveDeviceInfos="(Colección)" WaitMessageFont-Names="Verdana"
                        WaitMessageFont-Size="14pt" Width="100%" OnLoad="RpviewComprobante_OnLoad">
                        <LocalReport ReportPath="Page\Contabilidad\Comprobante\ReportComprobante.rdlc">
                        </LocalReport>
                    </rsweb:ReportViewer>
                </td>
            </tr>
        </table>
    </asp:Panel>

    <asp:Panel ID="vwPanel5" runat="server">
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
                    <asp:Label ID="lblErrorGenerar" runat="server" Text="Se presento error al generar el comprobante"
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
                    <asp:Button ID="btnRegresar" runat="server" Text="Regresar" OnClick="btnRegresar_Click" />
                </td>
            </tr>
        </table>
    </asp:Panel>

    <asp:Panel ID="vwPanel6" runat="server">
        <table style="width: 50%;">
            <tr>
                <td colspan="4" style="text-align: left">Período de Vigencia
                </td>
                <td></td>
            </tr>
            <tr>
                <td style="text-align: left">
                    <uc1:fecha ID="txtFechaIni" runat="server"></uc1:fecha>
                </td>
                <td style="text-align: left">&nbsp;&nbsp;a&nbsp;&nbsp;
                </td>
                <td style="text-align: left">
                    <uc1:fecha ID="txtFechaFin" runat="server"></uc1:fecha>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                </td>
                <td style="text-align: left"></td>
                <td></td>
                <td></td>
            </tr>
        </table>
        <table style="width: 70%;">
            <tr>
                <td style="text-align: left;">Tipo de Operación&nbsp;&nbsp;
                </td>
                <td style="text-align: left;">
                    <asp:DropDownList ID="ddlTipoOperacion" runat="server" CssClass="dropdown" Width="240px">
                    </asp:DropDownList>
                </td>
                <td></td>
                <td></td>
            </tr>
            <tr>
                <td style="text-align: left;">Tipo de Comprobante&nbsp;&nbsp;
                </td>
                <td style="text-align: left;">
                    <asp:DropDownList ID="ddlTipoComp" runat="server" Width="240px" CssClass="dropdown">
                    </asp:DropDownList>
                </td>
                <td></td>
                <td></td>
            </tr>
            <tr>
                <td style="text-align: left;">Concepto&nbsp;&nbsp;
                </td>
                <td style="text-align: left;">
                    <asp:DropDownList ID="ddlConcep" runat="server" Width="240px" CssClass="dropdown">
                    </asp:DropDownList>
                </td>
                <td></td>
                <td></td>
            </tr>
            <tr>
                <td style="text-align: left;">Cuenta Contable&nbsp;&nbsp;
                </td>
                <td colspan="3" style="text-align: left;">
                    <table>
                        <tr>
                            <td style="text-align: left">
                                <cc1:TextBoxGrid ID="txtCodCuentaPC" runat="server" AutoPostBack="True" Style="text-align: left"
                                    CssClass="textbox" Width="120px" OnTextChanged="txtCodCuentaPC_TextChanged">    
                                </cc1:TextBoxGrid>
                                <uc1:ListadoPlanCtas ID="ctlListadoPlanPC" runat="server" />
                            </td>
                            <td style="text-align: left; width: 30px">
                                <cc1:ButtonGrid ID="btnListadoPlanPC" CssClass="btnListado" runat="server" Text="..."
                                    Width="95%" OnClick="btnListadoPlanPC_Click" />
                            </td>
                            <td style="text-align: left; width: 30px">
                                <cc1:TextBoxGrid ID="txtNomCuentaPC" runat="server" Style="text-align: left" BackColor="#F4F5FF"
                                    Width="200px" CssClass="textbox" Enabled="False">
                                </cc1:TextBoxGrid>
                            </td>
                        </tr>
                    </table>


                </td>
            </tr>
            <tr>
                <td style="text-align: left;">Estructura de Detalle&nbsp;&nbsp;
                </td>
                <td style="text-align: left;">
                    <asp:DropDownList ID="ddlEstructura" runat="server" Width="240px" CssClass="dropdown">
                    </asp:DropDownList>
                </td>
                <td></td>
                <td></td>
            </tr>
            <tr>
                <td style="text-align: left;">Tipo de Movimiento&nbsp;&nbsp;
                </td>
                <td style="text-align: left;">
                    <asp:DropDownList ID="ddlTipoMov" runat="server" Width="240px" CssClass="dropdown">
                        <asp:ListItem Value=""></asp:ListItem>
                        <asp:ListItem Value="1">Débito</asp:ListItem>
                        <asp:ListItem Value="2">Crédito</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td></td>
                <td></td>
            </tr>
            <tr>
                <td style="text-align: center;" colspan="2">
                    <asp:Button ID="btnAceptarProceso" runat="server" Text="Aceptar" OnClick="btnAceptarProceso_Click" />
                </td>
            </tr>
        </table>
    </asp:Panel>

    <script type='text/javascript'>
        function paginado() {
            var btnTxt = document.getElementById('btnTxt');
            btnTxt.click();
        }
        function Forzar() {
            __doPostBack('', '');
        }

    
        function reCargar() {
            var currentUrl = window.location.href;
            var url = new URL(currentUrl);
            var desP = url.pathname;
            var p = url.search;
            var dirt = desP + p;
            // window.location.href = dirt;
        }


    </script>


</asp:Content>
