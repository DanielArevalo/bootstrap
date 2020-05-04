<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" Title=".: Xpinn - Tercero :." EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/fechaeditable.ascx" TagName="Fecha" TagPrefix="ucFecha" %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc2" %>
<%@ Register Src="~/General/Controles/ctlFormaPago.ascx" TagName="Forma" TagPrefix="uc3" %>
<%@ Register Src="~/General/Controles/ctlFormatoDocum.ascx" TagName="FormatoDocu" TagPrefix="uc4" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>
<%@ Register Src="~/General/Controles/ctlBusquedaRapida.ascx" TagName="ListadoPersonas" TagPrefix="uc5" %>
<%@ Register Src="~/General/Controles/ctlDireccion.ascx" TagName="Direccion" TagPrefix="uc6" %>
<%@ Register Src="~/General/Controles/ctlPersonaEd.ascx" TagName="Persona" TagPrefix="ct7" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <script src="../../../Scripts/PCLBryan.js" type="text/javascript"></script>
    <style type="text/css">
        .numeric {
            width: 110px;
            text-align: right;
        }

        .auto-style2 {
            width: 145px;
        }
        .auto-style3 {
            width: 58px;
        }
        .auto-style4 {
            width: 19px;
        }
    </style>

    <script type="text/javascript">
        function ValidNum(e) {
            var tecla = document.all ? tecla = e.keyCode : tecla = e.which;
            return ((tecla > 47 && tecla < 58) || tecla == 46);
        }
        function checkDate(sender, args) {
            if (sender._selectedDate > new Date()) {
                var hoy = new Date();
                alert("Eliga una fecha inferior a la Actual! " + hoy.toDateString());
                sender._selectedDate = new Date();
                sender._textbox.set_Value(sender._selectedDate.format(sender._format))
            }
        }
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
        }

        function mpeSeleccionOnOk() {
        }

        function mpeSeleccionOnCancel() {
        }

        function KeyBackspace(keyStroke) {
            isNetscape = (document.layers);
            eventChooser = (isNetscape) ? keyStroke.which : event.keyCode;
            if (eventChooser == 13) {
                return false;
            }
        }
        document.onkeypress = KeyBackspace;

        document.onkeydown = function () {
            if (window.event && window.event.keyCode == 8) {
                window.event.keyCode = 505;
            }
            if (window.event && window.event.keyCode == 505) {
                return false;
            }
        }

        function MostrarCIIUPrincipal(pDescripcion) {
            document.getElementById('<%= txtActividadCIIU.ClientID %>').value = pDescripcion;
        }

        function InfoUbicacion() {
            var chInfoUbicacion = document.getElementById('<%= chInfoUbicacion.ClientID %>');

            var txtDireccion = document.getElementById('<%= txtDireccion.ClientID %>');
            var ddlCiudad = document.getElementById('<%= ddlCiudad.ClientID %>');
            var txtTelefono = document.getElementById('<%= txtTelefono.ClientID %>');

            var txtDir_Correspondencia = document.getElementById('<%= txtDir_Correspondencia.ClientID %>');
            var ddlCiudad_Corr = document.getElementById('<%= ddlCiudad_Corr.ClientID %>');
            var txtTel_Correspondencia = document.getElementById('<%= txtTel_Correspondencia.ClientID %>');

            if (chInfoUbicacion == null || chInfoUbicacion == null)
                alert('Checkbox no encontrado');
            else {
                if (chInfoUbicacion.checked) {
                    txtDir_Correspondencia.value = txtDireccion.value;
                    ddlCiudad_Corr.value = ddlCiudad.value;
                    txtTel_Correspondencia.value = txtTelefono.value;
                }
            }
        }

    </script>
    <script type="text/javascript">
        function blur7(textbox) {
            var str = textbox.value;
            var formateado = "";
            str = str.replace(/\./g, "");
            if (str > 0) {
                str = parseInt(str);
                str = str.toString();

                if (str.length > 12)
                { str = str.substring(0, 12); }

                var long = str.length;
                var cen = str.substring(long - 3, long);
                var mil = str.substring(long - 6, long - 3);
                var mill = str.substring(long - 9, long - 6);
                var milmill = str.substring(0, long - 9);

                if (long > 0 && long <= 3)
                { formateado = parseInt(cen); }
                else if (long > 3 && long <= 6)
                { formateado = parseInt(mil) + "." + cen; }
                else if (long > 6 && long <= 9)
                { formateado = parseInt(mill) + "." + mil + "." + cen; }
                else if (long > 9 && long <= 12)
                { formateado = parseInt(milmill) + "." + mill + "." + mil + "." + cen; }
                else
                { formateado = "0"; }
            }
            else { formateado = "0"; }
            return formateado;
        }
        function TotalizarIngresosSoli(textbox) {
            var txtingreso_solijq = document.getElementById('<%= txtingreso_mensual.ClientID %>');
            var txtotrosIng_solijq = document.getElementById('<%= txtotrosIng_soli.ClientID %>');

            var txttotalING_solijq = document.getElementById('<%= txttotalING_soli.ClientID %>');

            var A = txtingreso_solijq.value == "" || txtingreso_solijq.value == null ? "0" : replaceAll(".", "", txtingreso_solijq.value);
            var B = txtotrosIng_solijq.value == "" || txtotrosIng_solijq.value == null ? "0" : replaceAll(".", "", txtotrosIng_solijq.value);

            var totalGeneral = parseFloat(A) + parseFloat(B);

            txttotalING_solijq.value = totalGeneral;
            var hdtotalING_soli = document.getElementById('<%= hdtotalING_soli.ClientID %>');
            hdtotalING_soli.value = totalGeneral;
            blur7(textbox);
            blur7(document.getElementById('<%= txttotalING_soli.ClientID %>'));

        }
        function TotalizarEgresosSoli(textbox) {

            var txtegreso_mensual = document.getElementById('<%= txtegreso_mensual.ClientID %>');

            var txttotalEGR_soli = document.getElementById('<%= txttotalEGR_soli.ClientID %>');

            var A = txtegreso_mensual.value == "" || txtegreso_mensual.value == null ? "0" : replaceAll(".", "", txtegreso_mensual.value);

            var totalGeneral = parseFloat(A);

            txttotalEGR_soli.value = totalGeneral;
            var hdtotalEGR_soli = document.getElementById('<%= hdtotalEGR_soli.ClientID %>');
            hdtotalEGR_soli.value = totalGeneral;
            blur7(textbox);
            blur7(document.getElementById('<%= txttotalEGR_soli.ClientID %>'));
        }

        function replaceAll(find, replace, str) {
            while (str.indexOf(find) > -1) {
                str = str.replace(find, replace);
            }
            return str;
        }

    </script>

    <asp:MultiView ID="mvDatos" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwEscoger" runat="server" EnableTheming="True">
            <asp:Panel ID="PanelTipoComprobante" runat="server">
                <table cellpadding="5" cellspacing="0" style="width: 100%">
                    <tr style="height: 20px" colspan="2">
                        <td style="text-align: center; color: #FFFFFF; background-color: #0066FF; height: 20px">TIPO DE PERSONA
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">&nbsp;
                        </td>
                    </tr>
                    <tr style="text-align: center">
                        <td>Escoja el tipo de persona a crear
                        </td>
                        <td>&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center;">
                            <asp:RadioButtonList ID="rbTipoPersona" runat="server" Width="100%">
                                <asp:ListItem Value="J">Juridica</asp:ListItem>
                                <asp:ListItem Selected="True" Value="N">Natural</asp:ListItem>
                                <asp:ListItem Value="M">Menor de Edad</asp:ListItem>
                            </asp:RadioButtonList>
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
        </asp:View>
        <asp:View ID="vwDetalleCliente" runat="server" EnableTheming="True">
            <br />
            <asp:Panel ID="pConsulta" runat="server">                                
                <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                    <ContentTemplate>
                        <table id="tbCriterios" border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td class="logo" style="text-align: left" colspan="4">&nbsp;
                                </td>
                                <td class="tdD">&nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left; width:200px" class="auto-style4">Código<br />
                                    <asp:TextBox ID="txtCodigo" runat="server" CssClass="textbox" Width="120px" />
                                </td>
                                <td style="text-align: left; width: 250px">Nit<br />
                                    <asp:TextBox ID="txtIdentificacion" runat="server" CssClass="textbox" AutoPostBack="True"
                                        OnTextChanged="txtIdentificacion_TextChanged" Width="130px" onkeypress="return ValidNum(event);" />
                                    <asp:Label ID="lblRayita" runat="server" Text="-"></asp:Label>
                                    <asp:TextBox ID="txtDigitoVerificacion" runat="server" CssClass="textbox" Width="30px"
                                        Enabled="false" />
                                    <asp:FilteredTextBoxExtender ID="ftbeIdentificacion" runat="server" Enabled="True"
                                        FilterType="Numbers, Custom" TargetControlID="txtIdentificacion" ValidChars="-+=">
                                    </asp:FilteredTextBoxExtender>
                                </td>
                                <td style="text-align: left; width: 160px">Oficina<br />
                                    <asp:DropDownList ID="ddlOficina" runat="server" AppendDataBoundItems="True"
                                        CssClass="textbox" Width="150px" TabIndex="3">
                                        <asp:ListItem></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td rowspan="7" style="padding-left: 15px; padding-top: 15px; text-align: left; vertical-align: top; width: 350px">
                                    <div style="overflow: scroll; max-height: 260px;">
                                        <asp:Label Visible="false" runat="server" Text="Empresas para Recaudo:" ID="lblempresas"></asp:Label><asp:GridView
                                            ID="gvEmpresaRecaudo" runat="server" AllowPaging="False" TabIndex="34" AutoGenerateColumns="false"
                                            BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px"
                                            CellPadding="0" DataKeyNames="idempresarecaudo" ForeColor="Black" GridLines="Both"
                                            PageSize="10" ShowFooter="True" Style="font-size: xx-small" Width="80%">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Activ" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblidempresarecaudo" runat="server" Text='<%# Bind("idempresarecaudo") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" Width="120px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Activ" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblcodempresa" runat="server" Text='<%# Bind("cod_empresa") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" Width="120px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Empresa" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDescripcion" runat="server" CssClass="textbox" Style="font-size: xx-small; text-align: left"
                                                            Text='<%# Bind("descripcion") %>' Width="170px"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" Width="200px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Seleccionar" ItemStyle-HorizontalAlign="Right">
                                                    <ItemTemplate>
                                                        <asp:UpdatePanel ID="updcheck" runat="server">
                                                            <ContentTemplate>
                                                                <cc1:CheckBoxGrid ID="chkSeleccionar" runat="server" AutoPostBack="true" Checked='<%# Convert.ToBoolean(Eval("seleccionar")) %>'
                                                                    CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' OnCheckedChanged="chkSeleccionar_CheckedChanged" />
                                                            </ContentTemplate>
                                                            <Triggers>
                                                                <asp:AsyncPostBackTrigger ControlID="chkSeleccionar" />
                                                            </Triggers>
                                                        </asp:UpdatePanel>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" Width="100px" />
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
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3" style="text-align: left;">Razón Social<br />
                                    <asp:TextBox ID="txtRazonSocial" runat="server" CssClass="textbox" MaxLength="128"
                                        Style="text-transform: uppercase" Width="350px" />
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left; width:200px" class="auto-style4">Sigla<br />
                                    <asp:TextBox ID="txtSigla" runat="server" CssClass="textbox" MaxLength="128" Style="text-transform: uppercase"
                                        Width="120px" />
                                </td>
                                <td style="text-align: left; width:200px">Tipo de empresa<br />
                                    <asp:DropDownList ID="ddlTipoEmpresa" runat="server" CssClass="textbox" MaxLength="128" Style="text-transform: uppercase"
                                        Width="150px" />
                                </td>
                                <td style="text-align: left;">Cámara de Comercio<br />
                                    <asp:TextBox ID="txtCam_Comercio" runat="server" CssClass="textbox" MaxLength="128" Style="text-transform: uppercase"
                                        Width="150px" />
                                </td>
                            </tr>                            
                            <tr>
                                <td style="text-align: left; width:200px">Ciudad<br />
                                    <asp:DropDownList ID="ddlCiudad" runat="server" Width="180px" CssClass="dropdown" Height="25px"
                                        AppendDataBoundItems="True">
                                        <asp:ListItem Value="">Seleccione un Item</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td style="text-align: left; width:200px">Telefóno<br />
                                    <asp:TextBox ID="txtTelefono" runat="server" CssClass="textbox" MaxLength="12" Width="150px"/>
                                    <asp:FilteredTextBoxExtender ID="ftb12" runat="server" Enabled="True" FilterType="Numbers, Custom"
                                        TargetControlID="txtTelefono" ValidChars="-()" />
                                </td>
                                <td style="text-align: left; width: 160px;">Zona <br />
                                    <asp:DropDownList ID="ddlZona" runat="server" Width="170px" CssClass="textbox required"
                                        AppendDataBoundItems="True" TabIndex="11" required="required">
                                        <asp:ListItem Value="" Text="Seleccione un item"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="text-align: left">E-Mail o Sitio Web<br />
                                    <asp:TextBox ID="txtEmail" runat="server" CssClass="textbox" Width="350px" />
                                    <asp:RegularExpressionValidator ID="revTxtEmail" runat="server" ControlToValidate="txtEmail"
                                        ErrorMessage="E-Mail no valido!" ForeColor="Red" Style="font-size: x-small" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                        ValidationGroup="vgGuardar" Display="Dynamic"></asp:RegularExpressionValidator>
                                </td>
                                <td style="text-align: left">Fecha Creación/Expedic.<br />
                                    <ucFecha:Fecha ID="txtFecha" runat="server" />
                                </td>
                                <%--<td style="text-align: left; height: 55px; width: 397px;">&nbsp;
                                </td>--%>
                            </tr>
                            <tr>
                                <td style="text-align: left;width:200px" >Lugar de registro<br />
                                    <asp:DropDownList ID="ddlLugRegistro" runat="server" Width="180px" CssClass="dropdown"
                                        Height="25px" AppendDataBoundItems="True">
                                    </asp:DropDownList>
                                    <br />
                                </td>
                                <td style="text-align: left;width:150px">Fecha de registro<br />
                                    <ucFecha:Fecha ID="txtFecha_Registro" runat="server"/>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left; width:160px">Actividad<br />
                                    <asp:DropDownList ID="ddlActividad" runat="server" Width="100px" CssClass="dropdown"
                                        Height="25px" AppendDataBoundItems="True">
                                        <asp:ListItem Value="0">Seleccione un Item</asp:ListItem>
                                    </asp:DropDownList>
                                    <br />
                                    <asp:RequiredFieldValidator ID="rfvActividad" runat="server" ErrorMessage="Seleccione Actividad"
                                        ControlToValidate="ddlActividad" Display="Dynamic" ValidationGroup="vgGuardar"
                                        InitialValue="Seleccione un Item" SetFocusOnError="True" ForeColor="#CC3300"
                                        Font-Size="XX-Small"></asp:RequiredFieldValidator>
                                </td>
                                <td style="text-align: left;width:200px">
                                    <asp:CheckBox ID="ChkEnteTerritorial" runat="server" Text="Entidad Territorial" Width="150px"/>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left; width:200px" class="auto-style4">Regimen<br />
                                    <asp:DropDownList ID="ddlRegimen" runat="server" Width="180px" CssClass="dropdown"
                                        Height="25px">
                                        <asp:ListItem Value="">Seleccione un Item</asp:ListItem>
                                        <asp:ListItem Value="C">COMUN</asp:ListItem>
                                        <asp:ListItem Value="S">SIMPLIFICADO</asp:ListItem>
                                        <asp:ListItem Value="E">ESPECIAL</asp:ListItem>
                                        <asp:ListItem Value="GCA">GRAN CONTRIBUYENTE AUTORRETENEDOR</asp:ListItem>
                                        <asp:ListItem Value="GCNA">GRAN CONTRIBUYENTE NO AUTORRETENEDOR</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td style="text-align: left; width: 150px;">Fecha de Constitución<br />
                                    <ucFecha:Fecha ID="txtFecConstitucion" runat="server" />
                                </td>
                                <td style="text-align: left; width: 150px;">Antigüedad<br />
                                    <asp:TextBox ID="txtAntiguedad" runat="server" CssClass="textbox" Width="90%" />
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left;width:200px " class="auto-style4">Tipo de Creación<br />
                                    <asp:DropDownList ID="ddlTipoActoCrea" runat="server" CssClass="textbox" Width="180px" />
                                </td>
                                <td style="text-align: left" colspan="2">
                                    <table>
                                        <tr>
                                            <td style="text-align: left; width: 250px; height: 52px;">Num Acto de Creación<br />
                                                <asp:TextBox ID="txtNumActoCrea" runat="server" CssClass="textbox" Width="90%" />
                                            </td>
                                            <td style="text-align: left; width: 180px; height: 52px;">Celular<br />
                                                <asp:TextBox ID="txtcelular" runat="server" CssClass="textbox" />
                                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" Enabled="True"
                                                    FilterType="Numbers, Custom" TargetControlID="txtcelular" ValidChars="-()" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td style="text-align: left;">Actividad CIIU
                                    <br />
                                    <%--<asp:UpdatePanel ID="upRecoger" runat="server" UpdateMode="Conditional" RenderMode="Inline">
                                        <ContentTemplate>--%>
                                            <asp:TextBox ID="txtActividadCIIU" CssClass="textbox" runat="server" Width="145px" TabIndex="16" />
                                            <asp:PopupControlExtender ID="PopupControlExtenderActividades" runat="server"
                                                TargetControlID="txtActividadCIIU"
                                                PopupControlID="panelLista" OffsetY="22">
                                            </asp:PopupControlExtender>
                                            <asp:Panel ID="panelLista" runat="server" Height="200px" Width="400px"
                                                BorderStyle="Solid" BorderWidth="2px" Direction="LeftToRight"
                                                ScrollBars="Auto" BackColor="#CCCCCC" Style="display: none">
                                                <table style="width:100%">
                                                    <tr>
                                                        <td style="width:30%">
                                                            <asp:TextBox ID="txtBuscarCodigo" onkeypress="return isNumber(event)" CssClass="textbox" runat="server" Width="90%" placeholder="Código"></asp:TextBox>
                                                        </td>
                                                        <td style="width:45%">
                                                            <asp:TextBox ID="txtBuscarDescripcion" CssClass="textbox" runat="server" Width="90%" placeholder="Descripción"></asp:TextBox>
                                                        </td>
                                                        <td style="width:15%">
                                                            <asp:ImageButton ID="imgBuscar" ImageUrl="~/Images/Lupa.jpg" Height="25px" runat="server" OnClick="imgBuscar_Click" CausesValidation="false" />
                                                        </td>
                                                    </tr>
                                                </table>
                                                <asp:GridView ID="gvActividadesCIIU" runat="server" Width="100%" AutoGenerateColumns="False"
                                                    HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager"
                                                    RowStyle-CssClass="gridItem" DataKeyNames="ListaId" OnRowDataBound="gvActividadesCIIU_RowDataBound">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Código">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_codigo" runat="server" Text='<%# Bind("ListaIdStr") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Descripción">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_descripcion" runat="server" Text='<%# Bind("ListaDescripcion") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderStyle-CssClass="gridIco" HeaderText="Principal">
                                                            <ItemTemplate>
                                                                <cc1:CheckBoxGrid ID="chkPrincipal" runat="server" CommandArgument='<%#DataBinder.Eval(Container, "RowIndex") %>'
                                                                    AutoPostBack="false" />
                                                            </ItemTemplate>
                                                            <HeaderStyle CssClass="gridIco"></HeaderStyle>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderStyle-CssClass="gridIco" HeaderText="Sel.">
                                                            <ItemTemplate>
                                                                <cc1:CheckBoxGrid ID="chkSeleccionar" runat="server" CommandArgument='<%#DataBinder.Eval(Container, "RowIndex") %>'
                                                                    AutoPostBack="false" ToolTip="Seleccionar" />
                                                            </ItemTemplate>
                                                            <HeaderStyle CssClass="gridIco"></HeaderStyle>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </asp:Panel>
                                            <%--</ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="gvActividadesCIIU" EventName="RowDataBound" />
                                            <asp:AsyncPostBackTrigger ControlID="imgBuscar" EventName="Click" />
                                        </Triggers>
                                    </asp:UpdatePanel>--%>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel> 
<%-- ------------------------------------------------------------------------------------------- --%>
<%-- ------------------------------------------------------------------------------------------- --%>
<%-- ------------------------------------------------------------------------------------------- --%>
<%-- ------------------------------------------------------------------------------------------- --%>
                <table border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td style="text-align:-webkit-auto;">
                            Dirección<br />
                                    <label id="msgTd" style="color:#FF3333;box-shadow: 0 0 1px 0px #FF3333;">Por favor seleccione un tipo de residencia</label>		                                    
                                    <uc6:Direccion ID="txtDireccion" runat="server" Width="90%" CssClass="textbox required" TabIndex="11" required="required"></uc6:Direccion>
                        </td>
                    </tr>
                </table>
                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                    <ContentTemplate>
                        <table>
                            <table>
                                <tr>
                                    <td colspan="3" style="text-align: left;">
                                        <asp:Label ID="lblCodRepresentante" runat="server" Visible="false" />
                                        <strong>Datos del representante legal</strong>
                                        <br />
                                        <ct7:Persona ID="ctlPersona" runat="server" Width="400px" />
                                    </td>
                                </tr>
                                <%--<tr>
                                    <td style="text-align: left;width:200px" class="auto-style2">Tipo de Identificación<br />
                                        <asp:DropDownList ID="ddlTipoID" runat="server" CssClass="textbox" Width="180px" Enabled="false">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvTipoID" runat="server" ControlToValidate="ddlTipoID" ErrorMessage="Campo Requerido" SetFocusOnError="True" ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic" Font-Size="X-Small" />
                                    </td>
                                    <td style="width: 250px; text-align: left">No. de Identificación<br />
                                        <asp:TextBox ID="txtID" runat="server" CssClass="textbox" Width="90%" Enabled="false"
                                            Style="text-align: left" AutoPostBack="True" />
                                        <asp:RequiredFieldValidator ID="rfvId" runat="server" ControlToValidate="txtID" ErrorMessage="Campo Requerido" SetFocusOnError="True" ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic" Font-Size="X-Small" />
                                        <asp:TextBox ID="txtCodRepresentante" runat="server" CssClass="textbox" Width="90%" Visible="false"
                                            Style="text-align: left" AutoPostBack="True" />
                                    </td>
                                    <td colspan="2" style="text-align: left">Nombres y Apellidos<br />
                                        <asp:TextBox ID="txtNombresR" runat="server" CssClass="textbox" Width="350px" Style="text-align: left"
                                            Enabled="False"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvNomResponsable" runat="server" ControlToValidate="txtNombresR" ErrorMessage="Campo Requerido" SetFocusOnError="True" ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic" Font-Size="X-Small" />
                                    </td>
                                    <td style="text-align: left;">
                                        <br />
                                        <asp:Button ID="btnConsultaRepresentante" CssClass="btn8" runat="server" Text="..." Height="26px"
                                            OnClick="btnConsultaRepresentante_Click" />
                                        <uc5:ListadoPersonas ID="ctlBusquedaPersonas" runat="server" />
                                    </td>
                                </tr>--%>
                            </table>
                            <tr>
                                <td colspan="5">
                                    <br />
                                </td>
                            </tr>
                        </table>
                        <table>
                            <tr>
                                <td colspan="3" style="text-align: left;">
                                    <strong>Información Comercial y de Notificación</strong><br/>
                                    <asp:CheckBox ID="chInfoUbicacion" runat="server" onchange="InfoUbicacion()" TabIndex="44" 
                                    Text="Utilizar los datos de la ubicación principal" TextAlign="Left" />
                                 </td>                                
                            </tr>
                            <tr>
                                <td style="text-align: left; width: 200px">Tipo de Ubicación<br />
                                    <asp:DropDownList ID="ddlTipoUbic" runat="server" AppendDataBoundItems="True"
                                        CssClass="textbox" Width="150px" TabIndex="12" AutoPostBack="False">
                                        <asp:ListItem Value="0" Text="Seleccione un item"></asp:ListItem>
                                        <asp:ListItem Value="1" Text="Urbana"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="Rural"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td style="width: 250px; text-align: left">Dirección<br />
                                    <asp:TextBox ID="txtDir_Correspondencia" runat="server" CssClass="textbox" Width="230px"
                                        Style="text-align: left" AutoPostBack="false" />
                                </td>
                                <td style="text-align: left">Ciudad<br />
                                    <asp:DropDownList ID="ddlCiudad_Corr" runat="server" CssClass="textbox" Width="180px" Style="text-align: left"></asp:DropDownList>
                                </td>
                                <td style="text-align: left;">Barrio<br />
                                    <asp:DropDownList ID="ddlBarrio_Corr" runat="server" CssClass="textbox" Width="180px" Style="text-align: left"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left;">Teléfono<br />
                                    <asp:TextBox ID="txtTel_Correspondencia" runat="server" CssClass="textbox" Width="180px" Style="text-align: left"></asp:TextBox>
                                </td>
                            </tr>

                        </table>

                        <asp:MultiView runat="server" ActiveViewIndex="0">
                            <asp:View ID="vmDetalle" runat="server" EnableTheming="True">
                                <asp:Panel ID="panelDatos" runat="server">
                                    <asp:Accordion ID="acoPersona" runat="server" SelectedIndex="0" HeaderCssClass="accordionHeader"
                                        HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContenido"
                                        FadeTransitions="false" FramesPerSecond="40" TransitionDuration="250" AutoSize="None"
                                        RequireOpenedPane="false" SuppressHeaderPostbacks="true" Width="95%">
                                        <Panes>
                                            <asp:AccordionPane ID="acoInfoEconomica" runat="server" Visible="True">
                                                <Header>
                                            <asp:Image ID="Image1" runat="server" DescriptionUrl="../../../Images/expand.png" />Información Económica</Header>
                                                <Content>
                                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                        <ContentTemplate>
                                                            <table border="0" cellpadding="0" cellspacing="0" style="width: 95%">
                                                                <tr>
                                                                    <td colspan="5">
                                                                        <asp:Panel ID="panel1" runat="server">
                                                                            <table cellpadding="0" style="width: 100%;">
                                                                                <tr>
                                                                                    <td style="text-align: left;">Ingresos mensuales derivados de su actividad principal</td>
                                                                                    <td style="text-align: left;">
                                                                                        <asp:TextBox ID="txtingreso_mensual" runat="server" CssClass="textbox numeric" onkeyup="TotalizarIngresosSoli(this)"></asp:TextBox>
                                                                                        <asp:FilteredTextBoxExtender ID="fte80" runat="server" Enabled="True" FilterType="Numbers, Custom"
                                                                                            TargetControlID="txtingreso_mensual" ValidChars="." />
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="text-align: left;">Otros Ingresos </td>
                                                                                    <td style="text-align: left;">
                                                                                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                                                            <ContentTemplate>
                                                                                                <asp:TextBox ID="txtotrosIng_soli" runat="server" CssClass="textbox numeric" onkeyup="TotalizarIngresosSoli(this)"
                                                                                                    TabIndex="84"></asp:TextBox><asp:FilteredTextBoxExtender ID="fte92" runat="server" Enabled="True" FilterType="Numbers, Custom"
                                                                                                        TargetControlID="txtotrosIng_soli" ValidChars="." />
                                                                                                <asp:PopupControlExtender ID="PopupControlExtender2" runat="server"
                                                                                                    Enabled="True" ExtenderControlID="" TargetControlID="txtotrosIng_soli"
                                                                                                    PopupControlID="panelConceptoOtrosSoli" OffsetY="22">
                                                                                                </asp:PopupControlExtender>
                                                                                                <asp:Panel ID="panelConceptoOtrosSoli" runat="server" Height="70px" Width="250px"
                                                                                                    BorderStyle="Solid" BorderWidth="2px" Direction="LeftToRight"
                                                                                                    ScrollBars="Auto" BackColor="#CCCCCC">
                                                                                                    <table>
                                                                                                        <tr>
                                                                                                            <td style="text-align: left;">Concepto otros</td>
                                                                                                            <td style="text-align: left;">
                                                                                                                <asp:TextBox ID="txtConceptoOtros_soli" runat="server" TabIndex="88" CssClass="textbox" TextMode="MultiLine" Rows="3"></asp:TextBox>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                    </table>
                                                                                                </asp:Panel>
                                                                                            </ContentTemplate>
                                                                                        </asp:UpdatePanel>
                                                                                    </td>
                                                                                    <tr>
                                                                                        <td style="text-align: left;">Egresos mensuales</td>
                                                                                        <td style="text-align: left;">
                                                                                            <asp:TextBox ID="txtegreso_mensual" runat="server" CssClass="textbox numeric" onkeyup="TotalizarEgresosSoli(this)"
                                                                                                ValidChars="."></asp:TextBox>
                                                                                        </td>
                                                                                    </tr>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td colspan="5">
                                                                                        <hr style="width: 100%" />
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="text-align: left;">Total Ingresos </td>
                                                                                    <td style="text-align: left;">
                                                                                        <asp:TextBox ID="txttotalING_soli" runat="server" CssClass="textbox numeric" ReadOnly="true"></asp:TextBox>
                                                                                    </td>
                                                                                    <td style="text-align: left;">Total Egresos </td>
                                                                                    <td style="text-align: left;">
                                                                                        <asp:TextBox ID="txttotalEGR_soli" runat="server" CssClass="textbox numeric" ReadOnly="true"></asp:TextBox>
                                                                                    </td>
                                                                                    <asp:HiddenField ID="hdtotalING_soli" runat="server" ClientIDMode="Static" />
                                                                                    <asp:HiddenField ID="hdtotalEGR_soli" runat="server" ClientIDMode="Static" />
                                                                                </tr>
                                                                                <tr>
                                                                                    <td colspan="5">
                                                                                        <hr style="width: 100%" />
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="text-align: left; height: 23px;">Total Activos </td>
                                                                                    <td style="text-align: left; height: 23px;">
                                                                                        <asp:TextBox ID="txtTotal_activos" runat="server" CssClass="textbox numeric"></asp:TextBox>
                                                                                        <asp:FilteredTextBoxExtender ID="fte86" runat="server" Enabled="True" FilterType="Numbers, Custom"
                                                                                            TargetControlID="txtTotal_activos" ValidChars="." />
                                                                                    </td>
                                                                                    <td style="text-align: left; height: 23px;">Total Pasivos</td>
                                                                                    <td style="text-align: left; height: 23px;">
                                                                                        <asp:TextBox ID="txtTotal_pasivos" runat="server" CssClass="textbox numeric"></asp:TextBox>
                                                                                        <asp:FilteredTextBoxExtender ID="fte87" runat="server" Enabled="True" FilterType="Numbers, Custom"
                                                                                            TargetControlID="txtTotal_activos" ValidChars="." />
                                                                                    </td>
                                                                                    <tr>
                                                                                        <td style="text-align: left;">Total Patrimonio</td>
                                                                                        <td style="text-align: left;">
                                                                                            <asp:TextBox ID="txtTotal_patrimonio" runat="server" CssClass="textbox numeric"></asp:TextBox>
                                                                                            <asp:FilteredTextBoxExtender ID="fte88" runat="server" Enabled="True" FilterType="Numbers, Custom"
                                                                                                TargetControlID="txtTotal_activos" ValidChars="." />
                                                                                        </td>
                                                                                    </tr>
                                                                                </caption>
                                                                            </table>
                                                                        </asp:Panel>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="txtingreso_mensual" EventName="TextChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="txtotrosIng_soli" EventName="TextChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="txtegreso_mensual" EventName="TextChanged" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                </Content>
                                            </asp:AccordionPane>
                                            <asp:AccordionPane ID="acoMonedaExtranjera" runat="server" Visible="True">
                                                <Header>
                                            <asp:Image ID="Image2" runat="server" DescriptionUrl="../../../Images/expand.png" />Información Moneda Extranjera</Header>
                                                <Content>
                                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                        <ContentTemplate>
                                                            <asp:CheckBox ID="chkMonedaExtranjera" runat="server" AutoPostBack="true"
                                                                OnCheckedChanged="chkMonedaExtranjera_CheckedChanged" Text="&lt;strong&gt;¿Maneja moneda extranjera?&lt;/strong&gt;" /><br />
                                                            <br />
                                                            <asp:Panel ID="panelMonedaExtranjera" runat="server" Visible="false">
                                                                <asp:Button ID="btnAgregarFila" runat="server" CssClass="btn8" OnClick="btnAgregarFila_Click"
                                                                    OnClientClick="btnAgregarFila_Click" Text="+ Adicionar Detalle" /><br />
                                                                <br />
                                                                <asp:GridView ID="gvMonedaExtranjera" HorizontalAlign="Center" DataKeyNames="cod_moneda_ext"
                                                                    runat="server" AllowPaging="True" AutoGenerateColumns="False" BackColor="White"
                                                                    BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="0"
                                                                    OnRowDeleting="gvMonedaExtranjera_RowDeleting" PageSize="10" ShowFooter="True"
                                                                    Style="font-size: xx-small" Width="100%">
                                                                    <AlternatingRowStyle BackColor="White" />
                                                                    <Columns>
                                                                        <asp:TemplateField>
                                                                            <ItemTemplate>
                                                                                <asp:ImageButton runat="server" CommandName="Delete" ImageUrl="~/Images/gr_elim.jpg" ToolTip="Delete" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' />
                                                                            </ItemTemplate>
                                                                            <ItemStyle Width="16px" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="CodMoneda" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblCodMoneda" runat="server" Text='<%# Bind("cod_moneda_ext") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <ItemStyle HorizontalAlign="Center" Width="120px" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Número de Cuenta" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txtNumCuentaExt" runat="server" CssClass="textbox" Style="font-size: xx-small; text-align: left" MaxLength="35"
                                                                                    Text='<%# Bind("num_cuenta_ext") %>' Width="140px"></asp:TextBox>
                                                                            </ItemTemplate>
                                                                            <ItemStyle HorizontalAlign="Center" Width="120px" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Nombre del Banco" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txtNomBancoExt" runat="server" CssClass="textbox" Style="font-size: xx-small; text-align: left"
                                                                                    Text='<%# Bind("banco_ext") %>' Width="140px"></asp:TextBox>
                                                                            </ItemTemplate>
                                                                            <ItemStyle HorizontalAlign="Center" Width="120px" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Pais" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txtNomPais" runat="server" CssClass="textbox" Style="font-size: xx-small; text-align: left"
                                                                                    Text='<%# Bind("pais") %>' Width="140px"></asp:TextBox>
                                                                            </ItemTemplate>
                                                                            <ItemStyle HorizontalAlign="Center" Width="120px" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Ciudad" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txtNomCiudad" runat="server" CssClass="textbox" Style="font-size: xx-small; text-align: left"
                                                                                    Text='<%# Bind("ciudad") %>' Width="140px"></asp:TextBox>
                                                                            </ItemTemplate>
                                                                            <ItemStyle HorizontalAlign="Center" Width="120px" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Moneda" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txtNomMoneda" runat="server" CssClass="textbox" Style="font-size: xx-small; text-align: left"
                                                                                    Text='<%# Bind("moneda") %>' Width="140px"></asp:TextBox>
                                                                            </ItemTemplate>
                                                                            <ItemStyle HorizontalAlign="Center" Width="120px" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Operación/Transacción que realiza" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txtOperacion" runat="server" CssClass="textbox" Style="font-size: xx-small; text-align: left"
                                                                                    Text='<%# Bind("desc_operacion") %>' Width="140px"></asp:TextBox>
                                                                            </ItemTemplate>
                                                                            <ItemStyle HorizontalAlign="Center" Width="120px" />
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
                                                            </asp:Panel>

                                                            <asp:CheckBox ID="chkTransaccionExterior" runat="server" OnCheckedChanged="chkTransaccionExterior_CheckedChanged" AutoPostBack="true"
                                                                Text="&lt;strong&gt;¿Posee productos financieros en el exterior?&lt;/strong&gt;" /><br />
                                                            <br />
                                                            <asp:Panel ID="pProductosExt" runat="server" Visible="false">
                                                                <asp:Button ID="btnProductoExt" runat="server" CssClass="btn8" OnClick="btnProductoExt_Click"
                                                                    OnClientClick="btnProductoExt_Click" Text="+ Adicionar Detalle" /><br />
                                                                <br />
                                                                <asp:GridView ID="gvProductosExterior" HorizontalAlign="Center" DataKeyNames="cod_moneda_ext"
                                                                    runat="server" AllowPaging="True" AutoGenerateColumns="False" BackColor="White"
                                                                    BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="0"
                                                                    OnRowDeleting="gvProductosExterior_RowDeleting" PageSize="10" ShowFooter="True"
                                                                    Style="font-size: xx-small" Width="100%">
                                                                    <AlternatingRowStyle BackColor="White" />
                                                                    <Columns>
                                                                        <asp:TemplateField>
                                                                            <ItemTemplate>
                                                                                <asp:ImageButton runat="server" CommandName="Delete" ImageUrl="~/Images/gr_elim.jpg" ToolTip="Delete" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' />
                                                                            </ItemTemplate>
                                                                            <ItemStyle Width="16px" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Cod Producto" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblCodProducto" runat="server" Text='<%# Bind("cod_moneda_ext") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <ItemStyle HorizontalAlign="Center" Width="120px" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Tipo de Producto" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txtTipoProducto" runat="server" CssClass="textbox" Style="font-size: xx-small; text-align: left"
                                                                                    Text='<%# Bind("tipo_producto") %>' Width="140px"></asp:TextBox>
                                                                            </ItemTemplate>
                                                                            <ItemStyle HorizontalAlign="Center" Width="120px" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="No. Producto" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txtNumProducto" runat="server" CssClass="textbox" Style="font-size: xx-small; text-align: left" MaxLength="35"
                                                                                    Text='<%# Bind("num_cuenta_ext") %>' Width="140px"></asp:TextBox>
                                                                            </ItemTemplate>
                                                                            <ItemStyle HorizontalAlign="Center" Width="120px" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Pais" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txtNomPais" runat="server" CssClass="textbox" Style="font-size: xx-small; text-align: left"
                                                                                    Text='<%# Bind("pais") %>' Width="140px"></asp:TextBox>
                                                                            </ItemTemplate>
                                                                            <ItemStyle HorizontalAlign="Center" Width="120px" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Ciudad" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txtNomCiudad" runat="server" CssClass="textbox" Style="font-size: xx-small; text-align: left"
                                                                                    Text='<%# Bind("ciudad") %>' Width="140px"></asp:TextBox>
                                                                            </ItemTemplate>
                                                                            <ItemStyle HorizontalAlign="Center" Width="120px" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Moneda" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txtNomMoneda" runat="server" CssClass="textbox" Style="font-size: xx-small; text-align: left"
                                                                                    Text='<%# Bind("moneda") %>' Width="140px"></asp:TextBox>
                                                                            </ItemTemplate>
                                                                            <ItemStyle HorizontalAlign="Center" Width="120px" />
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
                                                            </asp:Panel>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </Content>
                                            </asp:AccordionPane>
                                            <asp:AccordionPane ID="acoBienesActivos" runat="server" Visible="True">
                                                <Header>
                                                <asp:Image ID="imgBienesActivos" runat="server" DescriptionUrl="../../../Images/expand.png" />Información de Bienes/Activos Fijos</Header>
                                                <Content>
                                                    <asp:UpdatePanel ID="pnlBienesActivos" runat="server">
                                                        <ContentTemplate>
                                                            <asp:Label ID="lblInfoBienesActivos" Text="Para poder agregar activos relacionados con el asociado deberá primero grabar la afiliación" runat="server" Style="color: red" />
                                                            <asp:Button ID="btnBienesActivos" runat="server" CssClass="btn8" TabIndex="90" OnClick="InicializarModal" OnClientClick="javascript: LinkButton1.click()" Text="+ Adicionar Detalle" />
                                                            <asp:GridView ID="gvBienesActivos" runat="server"
                                                                AutoGenerateColumns="False" AllowPaging="False" PageSize="20" BackColor="White"
                                                                BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4"
                                                                ForeColor="Black" GridLines="Vertical" DataKeyNames="IdActivo"
                                                                OnRowEditing="gvBienesActivos_RowEditing" OnRowDeleting="gvBienesActivos_RowDeleting" Width="90%">
                                                                <AlternatingRowStyle BackColor="White" />
                                                                <Columns>
                                                                    <asp:TemplateField>
                                                                        <ItemTemplate>
                                                                            <asp:ImageButton runat="server" CommandName="Delete" ImageUrl="~/Images/gr_elim.jpg" ToolTip="Delete" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' />
                                                                        </ItemTemplate>
                                                                        <ItemStyle Width="16px" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField>
                                                                        <ItemTemplate>
                                                                            <asp:ImageButton runat="server" CommandName="Edit" ImageUrl="~/Images/gr_edit.jpg" ToolTip="Edit" />
                                                                        </ItemTemplate>
                                                                        <ItemStyle Width="16px" />
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField DataField="IdActivo" NullDisplayText=" " HeaderText="Código" />
                                                                    <asp:BoundField DataField="descripcion_activo" NullDisplayText=" " HeaderText="Tipo de Activo" />
                                                                    <asp:BoundField DataField="Descripcion" NullDisplayText=" " HeaderText="Descripción" />
                                                                    <asp:BoundField DataField="Fecha_adquisicionactivo" NullDisplayText=" " DataFormatString="{0:d}" HeaderText="Fecha de Adquisición" />
                                                                    <asp:BoundField DataField="valor_comercial" NullDisplayText=" " HeaderText="Valor Comercial" DataFormatString="{0:N0}" />
                                                                    <asp:BoundField DataField="estado_descripcion" NullDisplayText=" " HeaderText="Estado" />
                                                                </Columns>
                                                                <FooterStyle BackColor="#CCCC99" />
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
                                                </Content>
                                            </asp:AccordionPane>
                                            <asp:AccordionPane ID="acoAfiliacion" runat="server" Visible="True">
                                                <Header>
                                            <asp:Image ID="Image3" runat="server" DescriptionUrl="../../../Images/expand.png" />Afiliación</Header>
                                                <Content>
                                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                        <ContentTemplate>
                                                            <table border="0" cellpadding="0" cellspacing="0" style="width: 95%">
                                                                <tr>
                                                                    <td colspan="5">
                                                                        <asp:Panel ID="panel3" runat="server">
                                                                            <table border="0" cellpadding="0" cellspacing="0" style="width: 95%">
                                                                                <tr>
                                                                                    <td style="text-align: left; width: 185px">
                                                                                        <strong>Tipo Cliente</strong> &nbsp;&nbsp;&nbsp;&nbsp;
                                                                                            <asp:CheckBox ID="chkAsociado" runat="server" Text=" Asociado " AutoPostBack="true" Checked="true"
                                                                                                OnCheckedChanged="chkAsociado_CheckedChanged" /><br />
                                                                                    </td>
                                                                                    <td style="text-align: left;">
                                                                                        <br />
                                                                                        <asp:CheckBox ID="chkAdminRecursos" Text="Administra Recursos Púbicos" runat="server" Width="95%"
                                                                                            AppendDataBoundItems="true"></asp:CheckBox>
                                                                                    </td>
                                                                                    <td style="text-align: left;">
                                                                                        <br />
                                                                                        <asp:CheckBox ID="chkAsociados_Empresa" Text="Tiene asociados con más del 5% del patrimonio social" runat="server" Width="95%"
                                                                                            AppendDataBoundItems="true" OnCheckedChanged="chkAsociados_Empresa_CheckedChanged" AutoPostBack="true"></asp:CheckBox>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td colspan="5">
                                                                                        <asp:Panel ID="panelAfiliacion" runat="server">
                                                                                            <table>
                                                                                                <tr>
                                                                                                    <td style="text-align: left; width: 155px">Fecha de Afiliación<br>
                                                                                                        <asp:TextBox ID="txtcodAfiliacion" runat="server" Width="100px" CssClass="textbox"
                                                                                                            Style="text-align: right" Visible="false" />
                                                                                                        <uc1:fecha ID="txtFechaAfili" runat="server" Enabled="True" style="width: 140px" />
                                                                                                    </td>
                                                                                                    <td style="text-align: left; width: 170px">Estado<br />
                                                                                                        <asp:DropDownList ID="ddlEstadoAfi" runat="server" Width="160px" CssClass="textbox"
                                                                                                            AutoPostBack="true" AppendDataBoundItems="True" OnSelectedIndexChanged="ddlEstadoAfi_SelectedIndexChanged">
                                                                                                        </asp:DropDownList>
                                                                                                    </td>
                                                                                                    <td style="text-align: left; width: 140px">Fecha de Rétiro<br />
                                                                                                        <asp:Panel ID="panelFecha" runat="server">
                                                                                                            <uc1:fecha ID="txtFechaRetiro" runat="server" Enabled="True" style="width: 140px" />
                                                                                                        </asp:Panel>
                                                                                                    </td>
                                                                                                    <td style="text-align: left; width: 170px">Forma de Pago<br />
                                                                                                        <asp:DropDownList ID="ddlFormaPago" runat="server" Width="95%" CssClass="textbox"
                                                                                                            AutoPostBack="True" OnSelectedIndexChanged="ddlFormaPago_SelectedIndexChanged">
                                                                                                            <asp:ListItem Value="1">Caja</asp:ListItem>
                                                                                                            <asp:ListItem Value="2">Nomina</asp:ListItem>
                                                                                                        </asp:DropDownList>
                                                                                                    </td>
                                                                                                    <td style="text-align: left;">
                                                                                                        <asp:Label ID="lblEmpresa" runat="server" Text="Empresa" /><br />
                                                                                                        <asp:DropDownList ID="ddlEmpresa" runat="server" Width="180px" CssClass="textbox"
                                                                                                            OnSelectedIndexChanged="ddlEmpresa_SelectedIndexChanged">
                                                                                                        </asp:DropDownList>
                                                                                                    </td>
                                                                                                    <td style="text-align: left;">Valor<br />
                                                                                                        <uc2:decimales ID="txtValorAfili" runat="server" style="text-align: right;" />
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>                                                                                                    
                                                                                                    <td style="text-align: left;">Fecha de 1er Pago<br />
                                                                                                        <uc1:fecha ID="txtFecha1Pago" runat="server" style="width: 140px" />
                                                                                                    </td>
                                                                                                    <td style="text-align: left;">Nro Cuotas<br />
                                                                                                        <asp:TextBox ID="txtCuotasAfili" runat="server" Width="100px" CssClass="textbox"
                                                                                                            Enabled="false" Style="text-align: right" /><asp:FilteredTextBoxExtender ID="txtValor_FilteredTextBoxExtender"
                                                                                                                runat="server" Enabled="True" FilterType="Numbers, Custom" TargetControlID="txtCuotasAfili"
                                                                                                                ValidChars="" />
                                                                                                    </td>
                                                                                                    <td style="text-align: left;">Periodicidad<br />
                                                                                                        <asp:DropDownList ID="ddlPeriodicidad" runat="server" Width="95%" CssClass="textbox"
                                                                                                            Enabled="false">
                                                                                                        </asp:DropDownList>
                                                                                                    </td>
                                                                                                    <td style="text-align: left;">Asesor Comercial:
                                                                                                            <br />
                                                                                                        <asp:DropDownList ID="ddlAsesor" runat="server" Width="95%" CssClass="textbox" AppendDataBoundItems="true">
                                                                                                            <asp:ListItem Text="Seleccione un Item" Value=" " />
                                                                                                        </asp:DropDownList>
                                                                                                    </td>
                                                                                                    <td style="text-align: left;">Asociados especiales<br />
                                                                                                        <asp:DropDownList ID="ddlAsociadosEspeciales" runat="server" CssClass="textbox" Width="180px" TabIndex="16">
                                                                                                        </asp:DropDownList>
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
                                                            </table>
                                                        </ContentTemplate>
                                                        <%--<Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="ddlEstadoAfi" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlFormaPago" EventName="SelectedIndexChanged" />
                                                    </Triggers --%>
                                                    </asp:UpdatePanel>
                                                    <br />
                                                    <asp:UpdatePanel ID="upAsociados" runat="server" Visible="false">
                                                        <ContentTemplate>
                                                            <asp:Label id="lblAsoaciados" Text="Asociados con más del 5% del patrimonio" runat="server" Font-Bold="true"/>
                                                             <asp:Panel ID="pAsociados" runat="server" Visible="True">
                                                                <asp:Button ID="btnDetalle_Asociado" runat="server" CssClass="btn8" OnClick="btnDetalle_Asociado_Click" OnClientClick="btnDetalle_Asociado_Click"
                                                                     Text="+ Adicionar Detalle" /><br />
                                                                <br />
                                                                <asp:GridView ID="gvAsociados" HorizontalAlign="Center" DataKeyNames="cod_representante"
                                                                    runat="server" AllowPaging="True" AutoGenerateColumns="False" BackColor="White"
                                                                    BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="0" OnRowDataBound="gvAsociados_RowDataBound"
                                                                    OnRowDeleting="gvAsociados_RowDeleting" PageSize="10" ShowFooter="True" Style="font-size: xx-small" Width="100%">
                                                                    <AlternatingRowStyle BackColor="White" />
                                                                    <Columns>
                                                                        <asp:TemplateField>
                                                                            <ItemTemplate>
                                                                                <asp:ImageButton runat="server" CommandName="Delete" ImageUrl="~/Images/gr_elim.jpg" ToolTip="Delete" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' />
                                                                            </ItemTemplate>
                                                                            <ItemStyle Width="16px" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Tipo Identificación" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                                <asp:DropDownList ID="ddlTipo_ID" runat="server" CssClass="textbox" Style="font-size: xx-small; text-align: left"
                                                                                     Width="140px"></asp:DropDownList>
                                                                            </ItemTemplate>
                                                                            <ItemStyle HorizontalAlign="Center" Width="120px" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="No. Identificación" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txtID_Asociado" runat="server" CssClass="textbox" Style="font-size: xx-small; text-align: left"
                                                                                    Text='<%# Bind("identificacion") %>' Width="140px"></asp:TextBox>
                                                                            </ItemTemplate>
                                                                            <ItemStyle HorizontalAlign="Center" Width="120px" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Nombres y Apellidos" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txtNombres" runat="server" CssClass="textbox" Style="font-size: xx-small; text-align: left"
                                                                                    Text='<%# Bind("nombres") %>' Width="140px"></asp:TextBox>
                                                                            </ItemTemplate>
                                                                            <ItemStyle HorizontalAlign="Center" Width="120px" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Fecha Expedición" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                                <uc1:fecha ID="txtFechaExp" runat="server" CssClass="textbox" Style="font-size: xx-small; text-align: left"
                                                                                    Text='<%# Bind("fechaexpedicion") %>' Width="140px"></uc1:fecha>
                                                                            </ItemTemplate>
                                                                            <ItemStyle HorizontalAlign="Center" Width="120px" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Porcentaje del Patrimonio" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txtPorcentaje" runat="server" CssClass="textbox" Style="font-size: xx-small; text-align: left"
                                                                                    Text='<%# Bind("porcentaje_patrimonio") %>' Width="140px"></asp:TextBox>
                                                                            </ItemTemplate>
                                                                            <ItemStyle HorizontalAlign="Center" Width="120px" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Cotiza bolsa" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                                <asp:DropDownList ID="ddlCotiza" runat="server" required CssClass="textbox" Style="font-size: xx-small; text-align: left" Width="140px">
                                                                                    <asp:ListItem Value="" Text="Seleccione" />
                                                                                    <asp:ListItem Value="0" Text="No" />
                                                                                    <asp:ListItem Value="1" Text="Si" />
                                                                                </asp:DropDownList>
                                                                            </ItemTemplate>
                                                                            <ItemStyle HorizontalAlign="Center" Width="120px" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Vinculado PEP" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                                <asp:DropDownList ID="ddlVinculaPEP" runat="server" required CssClass="textbox" Style="font-size: xx-small; text-align: left" Width="140px">
                                                                                    <asp:ListItem Value="" Text="Seleccione" />
                                                                                    <asp:ListItem Value="0" Text="No" />
                                                                                    <asp:ListItem Value="1" Text="Si" />
                                                                                </asp:DropDownList>
                                                                            </ItemTemplate>
                                                                            <ItemStyle HorizontalAlign="Center" Width="120px" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Tributación" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                                <asp:DropDownList ID="ddlTributacion" runat="server" required CssClass="textbox" Style="font-size: xx-small; text-align: left" Width="140px">
                                                                                    <asp:ListItem Value="" Text="Seleccione" />
                                                                                    <asp:ListItem Value="0" Text="No" />
                                                                                    <asp:ListItem Value="1" Text="Si" />
                                                                                </asp:DropDownList>
                                                                            </ItemTemplate>
                                                                            <ItemStyle HorizontalAlign="Center" Width="120px" />
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
                                                            </asp:Panel>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </Content>
                                            </asp:AccordionPane>
                                            <asp:AccordionPane ID="acoInfoAdicional" runat="server" Visible="True">
                                                <Header>
                                             <asp:Image ID="imgExpandGeneral" runat="server" DescriptionUrl="../../../Images/expand.png" />Información Adicional</Header>
                                                <Content>
                                                    <asp:UpdatePanel ID="upTipoVivienda" runat="server">
                                                        <ContentTemplate>
                                                            <table width="95%">
                                                                <%--                                                <tr>
                                                                <td style="text-align: center; color: #FFFFFF; background-color: #0066FF; height: 20px">Información Adicional
                                                                </td>
                                                            </tr>--%>
                                                                <tr>
                                                                    <td>
                                                                        <%--OnRowDataBound="gvInfoAdicional_RowDataBound"--%>
                                                                        <asp:GridView ID="gvInfoAdicional" runat="server" AllowPaging="True" OnRowDataBound="gvInfoAdicional_RowDataBound"
                                                                            AutoGenerateColumns="false" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None"
                                                                            BorderWidth="0px" CellPadding="0" DataKeyNames="" ForeColor="Black" GridLines="Both"
                                                                            PageSize="10" ShowFooter="False" ShowHeader="False" ShowHeaderWhenEmpty="False"
                                                                            Width="80%">
                                                                            <AlternatingRowStyle BackColor="White" />
                                                                            <Columns>
                                                                                <asp:TemplateField HeaderText="ID" ItemStyle-HorizontalAlign="Center">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblidinfadicional" runat="server" Text='<%# Bind("idinfadicional") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle HorizontalAlign="Center" Width="70px" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="codigo" ItemStyle-HorizontalAlign="Center">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblcod_infadicional" runat="server" Text='<%# Bind("cod_infadicional") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle HorizontalAlign="Center" Width="120px" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Descripcion" ItemStyle-HorizontalAlign="Center">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lbldescripcion" runat="server" Text='<%# Bind("descripcion") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle HorizontalAlign="left" Width="160px" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Control" ItemStyle-HorizontalAlign="Left">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblopcionaActivar" runat="server" Text='<%# Bind("tipo") %>' Visible="false"></asp:Label><asp:TextBox ID="txtCadena" runat="server" CssClass="textbox" Style="font-size: xx-small; text-align: left"
                                                                                            Text='<%# Bind("valor") %>' Visible="false" Width="280px"></asp:TextBox><asp:TextBox ID="txtNumero" runat="server" CssClass="textbox" Style="font-size: xx-small; text-align: left"
                                                                                                Text='<%# Bind("valor") %>' Visible="false" Width="150px">
                                &#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; &#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;
                                &#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;
                                &#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;
                                                                                            </asp:TextBox><asp:FilteredTextBoxExtender ID="ftb1" runat="server" Enabled="True" FilterType="Numbers, Custom"
                                                                                                TargetControlID="txtNumero" ValidChars="" />
                                                                                        <uc1:fecha ID="txtctlfecha" runat="server" cssclass="textbox" Enabled="True" habilitado="True"
                                                                                            style="font-size: xx-small; text-align: left" Text='<%# Eval("valor", "{0:d}") %>'
                                                                                            tipoletra="xx-Small" Visible="false" />
                                                                                        <asp:Label ID="lblValorDropdown" runat="server" Text='<%# Bind("valor") %>' Visible="false"></asp:Label><asp:Label ID="lblDropdown" runat="server" Text='<%# Bind("items_lista") %>' Visible="false"></asp:Label><cc1:DropDownListGrid ID="ddlDropdown" runat="server" AppendDataBoundItems="True"
                                                                                            CommandArgument="<%#Container.DataItemIndex %>" CssClass="textbox" Style="font-size: xx-small; text-align: left"
                                                                                            Visible="false" Width="160px">
                                                                                        </cc1:DropDownListGrid>
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle HorizontalAlign="Left" Width="120px" />
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
                                                                        <br />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>                                                    
                                                </Content>
                                            </asp:AccordionPane>
                                        </Panes>
                                    </asp:Accordion>
                                </asp:Panel>
                            </asp:View>
                        </asp:MultiView>
                    </ContentTemplate>
                </asp:UpdatePanel>                
                <div style="visibility: hidden">
                    <asp:LinkButton ID="LinkButton1" runat="server" ClientIDMode="Static" Text="Click here to change the paragraph style"  />
                </div>
                <asp:ModalPopupExtender ID="mpeNuevoActividad" runat="server" PopupControlID="panelMostrarModal" 
                    TargetControlID="LinkButton1" BackgroundCssClass="backgroundColor" CancelControlID="btnCancelarModal">
                </asp:ModalPopupExtender>
                <asp:Panel ID="panelMostrarModal" runat="server" BackColor="White" Style="overflow-y: scroll; text-align: left; max-height: 500px; padding: 20px; border: medium groove #0000FF; background-color: #FFFFFF;"
                    Width="700px">
                    <asp:UpdatePanel ID="upReclasificacion" runat="server">
                        <ContentTemplate>
                            <center><strong>ACTIVOS FIJOS</strong></center>
                            <table style="width: 100%">
                                <tr style="text-align: right">
                                    <td></td>
                                    <td style="width: 120px">
                                        <asp:ImageButton runat="server" ID="btnCancelarModal" ImageUrl="~/Images/btnCancelar.jpg" ToolTip="Cancelar" OnClick="btnCancelarModal_click" />
                                    </td>
                                    <td style="width: 120px">
                                        <asp:ImageButton runat="server" ID="btnGuardarModal" ImageUrl="~/Images/btnGuardar.jpg" ToolTip="Guardar" OnClick="btnGuardarModal_click" />
                                    </td>
                                </tr>
                            </table>
                            <table style="width: 100%">
                                <tr>
                                    <td colspan="3">
                                        <asp:Label ID="lblErrorModal" runat="server" Style="text-align: center" Width="100%" ForeColor="Red"></asp:Label><br />
                                        <asp:Label ID="lblTipoProceso" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: left;" colspan="3">&nbsp;
                                        <strong>Datos del Activo:  </strong>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 25%;">Identificación<br />
                                        <asp:TextBox ID="txtModalIdentificacion" Enabled="false" runat="server" CssClass="textbox"
                                            MaxLength="128" Width="92%" />
                                    </td>
                                    <td style="width: 25%;">Tipo Identificación<br />
                                        <asp:DropDownList ID="ddlModalIdentificacion" Enabled="false" runat="server"
                                            CssClass="textbox" Width="95%">
                                        </asp:DropDownList>
                                    </td>
                                    <td style="width: 50%;">Nombres y Apellidos<br />
                                        <asp:TextBox ID="txtModalNombres" Enabled="false" runat="server" CssClass="textbox" Style="text-transform: uppercase"
                                            MaxLength="128" Width="95%" /></td>
                                </tr>
                            </table>
                            <table style="width: 100%" cellpadding="5" cellspacing="0">
                                <tr>
                                    <td colspan="2" style="width: 148px;">Tipo de Activo<br />
                                        <asp:DropDownList ID="ddlModalTipoActivo" runat="server"
                                            CssClass="textbox" Width="199px" AutoPostBack="true" OnSelectedIndexChanged="ddlModalTipoActivo_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                    <td style="text-align: left">Estado<br />
                                        <asp:DropDownList ID="ddlEstadoModal" runat="server" CssClass="textbox" Width="95%">
                                            <asp:ListItem Value="0" Text="Inactivo" />
                                            <asp:ListItem Value="1" Text="Activo" />
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: left">Descripción</td>
                                    <td colspan="2" style="text-align: left">
                                        <asp:TextBox ID="txtModalDescripcion" runat="server" CssClass="textbox" Style="text-transform: uppercase"
                                            MaxLength="128" Width="350px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 30%">
                                        <asp:Label ID="LabelFecha_gara" runat="server" Text="Fecha Adquisición"></asp:Label>
                                        <uc1:fecha ID="txtModalFechaIni" runat="server" />
                                    </td>
                                    <td style="width: 35%; text-align: left">Valor Comercial:<br />
                                        <asp:TextBox ID="txtModalValorComercial" runat="server" onkeypress="return isNumber(event)" CssClass="textbox" MaxLength="128" Width="196px" />
                                    </td>
                                    <td style="width: 35%; text-align: left">Valor Comprometido:<br />
                                        <asp:TextBox ID="txtModalValorComprometido" runat="server" onkeypress="return isNumber(event)" CssClass="textbox" MaxLength="128" Width="196px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3" style="text-align: left; width: 684px;">
                                        <hr style="width: 99%" />
                                    </td>
                                </tr>
                            </table>
                            <asp:Panel ID="panelTipoActivoInmueble" Visible="false" runat="server">
                                <table>
                                    <tr>
                                        <td class="tdD" style="height: 36px; width: 148px;">Dirección
                                            <asp:TextBox ID="txtModalDireccion" runat="server" CssClass="textbox"
                                                MaxLength="128" Width="196px" />
                                        </td>
                                        <td class="tdD" style="height: 36px; width: 148px;">Localización
                                            <asp:TextBox ID="txtModalLocalizacion" runat="server" CssClass="textbox"
                                                MaxLength="128" Width="196px" />
                                        </td>
                                        <td class="tdD" style="width: 148px;">VIS<br />
                                            <asp:DropDownList ID="ddlModalVIS" Width="180px" AutoPostBack="true" class="textbox" runat="server" OnSelectedIndexChanged="ddlModalVIS_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Sin VIS
                                                </asp:ListItem>
                                                <asp:ListItem Value="1">Con VIS
                                                </asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 148px;">Nro. Matricula
                                            <asp:TextBox ID="txtModalNoMatricula" runat="server" CssClass="textbox"
                                                MaxLength="128" Width="196px" />
                                        </td>
                                        <td style="width: 148px;">Escritura
                                            <asp:TextBox ID="txtModalEscritura" runat="server" CssClass="textbox"
                                                MaxLength="128" Width="196px" />
                                        </td>
                                        <td style="width: 148px;">Notaria
                                            <asp:TextBox ID="txtModalNotaria" runat="server" CssClass="textbox"
                                                MaxLength="128" Width="196px" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 148px;">Entidad Redescuento<br />
                                            <asp:DropDownList ID="ddlModalEntidadReDesc" runat="server"
                                                CssClass="textbox" Width="199px">
                                                <asp:ListItem Value="0">Ninguna</asp:ListItem>
                                                <asp:ListItem Value="1">FINDETER</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 148px;">Margen Redescuento<br />
                                            <asp:TextBox ID="txtModalmargenReDesc" runat="server" CssClass="textbox"
                                                MaxLength="128" Width="196px" />
                                            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" Enabled="True" FilterType="Numbers, Custom"
                                                TargetControlID="txtModalmargenReDesc" ValidChars=".," />
                                        </td>
                                        <td style="width: 148px;">Tipo Vivienda<br />
                                            <asp:DropDownList ID="ddlModalTipoVivienda" runat="server"
                                                CssClass="textbox" Width="199px">
                                                <asp:ListItem Value="1">Nueva
                                                </asp:ListItem>
                                                <asp:ListItem Value="2">Usada
                                                </asp:ListItem>
                                                <asp:ListItem Value="3">Mejoramiento
                                                </asp:ListItem>
                                                <asp:ListItem Value="4">Lote con servicios
                                                </asp:ListItem>
                                                <asp:ListItem Value="5">Construccion en sitio propio
                                                </asp:ListItem>
                                            </asp:DropDownList>
                                        </td>

                                    </tr>
                                    <tr>
                                        <td style="width: 148px;">Desembolso
                                            <asp:DropDownList ID="ddlModalDesembolso" runat="server"
                                                CssClass="textbox" Width="199px">
                                                <asp:ListItem Value="1">Desembolso Directo</asp:ListItem>
                                                <asp:ListItem Value="2">Desembolso a Constructor</asp:ListItem>
                                                <asp:ListItem Value="3">Subrogración</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 148px;">Desembolso Directo
                                            <asp:TextBox ID="txtModalDesembolsoDirecto" runat="server" CssClass="textbox"
                                                MaxLength="128" Width="196px" />
                                        </td>
                                        <td style="width: 148px;">Rango Vivienda<br />
                                            <asp:DropDownList ID="ddlModalRangoVivienda" runat="server"
                                                CssClass="textbox" Width="199px">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 148px;">
                                            <asp:CheckBox ID="chkHipoteca" runat="server" Text="Hipoteca" Width="199px">
                                            </asp:CheckBox>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <asp:Panel ID="pnlTipoActivoMaquinaria" Visible="false" runat="server">
                                <table>
                                    <tr>
                                        <td style="width: 148px;">Marca
                                            <asp:TextBox ID="txtModalMarca" runat="server" CssClass="textbox"
                                                MaxLength="128" Width="196px" />
                                        </td>
                                        <td style="width: 148px;">Referencia
                                            <asp:TextBox ID="txtModalReferencia" runat="server" CssClass="textbox"
                                                MaxLength="128" Width="196px" />
                                        </td>
                                        <td style="width: 148px;">Modelo
                                            <asp:TextBox ID="txtModalModelo" runat="server" CssClass="textbox"
                                                MaxLength="128" Width="196px" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 148px;">Uso<br />
                                            <asp:DropDownList ID="ddlModalUso" Width="180px" class="textbox" runat="server">
                                                <asp:ListItem Value="1">Particular
                                                </asp:ListItem>
                                                <asp:ListItem Value="2">Publico
                                                </asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 148px;">No.Chasis
                                            <asp:TextBox ID="txtModalNoChasis" runat="server" CssClass="textbox"
                                                MaxLength="128" Width="196px" />
                                        </td>
                                        <td style="width: 148px;">Capacidad
                                            <asp:TextBox ID="txtModalCapacidad" runat="server" CssClass="textbox"
                                                MaxLength="128" Width="196px" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 148px;">No.Serie Motor
                                            <asp:TextBox ID="txtModalNoSerieMotor" runat="server"
                                                CssClass="textbox" Width="199px" />
                                        </td>
                                        <td style="width: 148px;">Placa
                                            <asp:TextBox ID="txtModalPlaca" runat="server" CssClass="textbox"
                                                MaxLength="128" Width="196px" />
                                        </td>
                                        <td style="width: 148px;">Color
                                            <asp:TextBox ID="txtModalColor" runat="server" CssClass="textbox"
                                                MaxLength="128" Width="196px" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 148px;">Doc.Importación
                                            <asp:TextBox ID="txtModalDocImportacion" runat="server"
                                                CssClass="textbox" Width="199px" />
                                        </td>
                                        <td>
                                            <asp:Label ID="Label1" runat="server" Text="Fecha Importación"></asp:Label>
                                            <uc1:fecha ID="txtModalFechaImportacion" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 148px;">
                                            <asp:CheckBox ID="chkPignorado" runat="server" Text="Pignorado" Width="199px" OnCheckedChanged="chkPignorado_CheckedChanged" />
                                        </td>
                                        <td>
                                            <asp:Label ID="lblPorcPignorado" runat="server" Text="Porcentaje" Visible="false"></asp:Label>
                                            <asp:TextBox ID="txtPorcPignorado" runat="server" CssClass="textbox" Visible="false"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:Panel>
            </asp:Panel>
        </asp:View>
    </asp:MultiView>
    <uc4:FormatoDocu ID="ctlFormatos" runat="server" />
    <asp:Panel ID="panelFinal" runat="server" Visible="false" Height="600px">
        <div style="text-align: left">
            <asp:Button ID="btnVerData" runat="server" CssClass="btn8" Text="Cerrar Informe"
                OnClick="btnVerData_Click" Width="280px" Height="30px" />
        </div>
        <iframe id="frmPrint" name="IframeName" width="100%" src="../../Reportes/Reporte.aspx"
            height="100%" runat="server" style="border-style: groove; float: left;"></iframe>
    </asp:Panel>
    <script type="text/javascript">
        function mensaje() {
            var contenidoRadio01 = getRadio("ctl00$cphMain$txtDireccion$rbtnDetalleZonaGeo");
            console.log(contenidoRadio01);
            if (contenidoRadio01 == "R" || contenidoRadio01 == "U") {
                $("#msgTd").hide();
            }
            var contenidoRadio02 = getRadio("ctl00$cphMain$txtDirCorrespondencia$rbtnDetalleZonaGeo");
            if (contenidoRadio02 == "R" || contenidoRadio02 == "U") {
                $("#msgTd02").hide();
            }
            function getRadio(name) {
                var radioObjs = document.getElementsByName(name);
                var radioLength = radioObjs.length;
                var result = null;
                for (var i = 0; i < radioLength; i++) {
                    if (radioObjs[i].checked) {
                        result = radioObjs[i].value;
                    }
                }
                return result;
            }
            function setRadio(name, newValue) {
                var radioObjs = document.getElementsByName(name);
                var radioLength = radioObjs.length;
                for (var i = 0; i < radioLength; i++) {
                    radioObjs[i].checked = false;
                    if (radioObjs[i].value == newValue) {
                        radioObjs[i].checked = true;
                    }
                }
            }
        }
     </script>
</asp:Content>
