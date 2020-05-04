<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="nuevo.aspx.cs" Inherits="Nuevo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/ctlBusquedaRapida.ascx" TagName="ListadoPersonas" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>
<%@ Register Src="~/General/Controles/ctlProcesoContable.ascx" TagName="procesoContable" TagPrefix="uc2" %>
<%@ Register Src="~/General/Controles/ctlNumeroConDecimales.ascx" TagName="numero" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/ctlAvances.ascx" TagName="avances" TagPrefix="uc3" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <style>
        #WindowLoad {
            position: fixed;
            top: 0px;
            left: 0px;
            z-index: 3200;
            filter: alpha(opacity=65);
            -moz-opacity: 65;
            opacity: 0.65;
            background: #999;
        }

        .enlace {
            display: inline;
            border: 0;
            padding: 0;
            margin: 0;
            text-decoration: underline;
            background: none;
            color: #000088;
            font-family: arial, sans-serif;
            font-size: 1em;
            line-height: 1em;
        }

            .enlace:hover {
                text-decoration: none;
                color: #0000cc;
                cursor: pointer;
            }
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            //PREGUNTO SI LA PAGINA VIENE DE SER DUPLIACADA
            preventDuplicateTab();
        });
        function preventDuplicateTab() {
            if (sessionStorage.createTS) {
                if (!window.name) {
                    window.name = "*uknd*";
                    sessionStorage.createTS = Date.now();
                    reCargar();
                } else {
                    if (window.name == "*uknd*") {
                        bloquearPantalla();
                    }
                }
            } else {
                sessionStorage.createTS = Date.now();
                reCargar();
            }
        }
        function reCargar() {
            var currentUrl = window.location.href;
            var url = new URL(currentUrl);
            var desP = url.pathname;
            var p = url.search;
            var dirt = desP + p;
            window.location.href = dirt;
        }
        function bloquearPantalla() {
            var mensaje = "";
            mensaje = "Ya tiene una pagina abierta, para evitar el cruce de información por favor cierre esta pagina";

            //centrar el titulo
            height = 20;//El div del titulo, para que se vea mas arriba (H)
            var ancho = 0;
            var alto = 0;

            //obtenemos el ancho y alto de la ventana de nuestro navegador, compatible con todos los navegadores
            if (window.innerWidth == undefined) ancho = window.screen.width;
            else ancho = window.innerWidth;
            if (window.innerHeight == undefined) alto = window.screen.height;
            else alto = window.innerHeight;

            //operación necesaria para centrar el div que muestra el mensaje
            var heightdivsito = alto / 2 - parseInt(height) / 2;//Se utiliza en el margen superior, para centrar
            imgCentro = "<div style='text-align:center;height:" + alto + "px;'><div  style='color:#000;margin-top:" + heightdivsito + "px; font-size:27px;font-weight:bold'>" + mensaje + "</div></div>";

            //creamos el div que bloquea grande-
            div = document.createElement("div");
            div.id = "WindowLoad"
            div.style.width = ancho + "px";
            div.style.height = alto + "px";
            $("body").append(div);

            //creamos un input text para que el foco se plasme en este y el usuario no pueda escribir en nada de atras
            input = document.createElement("input");
            input.id = "focusInput";
            input.type = "text"

            //asignamos el div que bloquea
            $("#WindowLoad").append(input);

            //asignamos el foco y ocultamos el input text
            $("#focusInput").focus();
            $("#focusInput").hide();

            //centramos el div del texto
            $("#WindowLoad").html(imgCentro);
        }
        function deshabilitar() {
            //Esta es una variable. Su valor es true cuando todos los campos 
            //son válidos y false cuando hay algún error.
            var camposValidos = false;

            //En esta variable guardamos el objeto que representa al elemento
            //que tenga como ID = boton dentro de nuestro HTML. Asumimos que ese fue
            //el ID que se le dio al botón de enviar. 
            var botonEnviar = document.getElementById('btnGuardar2');

            if (camposValidos == false) {
                botonEnviar.disabled = true;
            }
            else {
                botonEnviar.disabled = false;
            }
        }

        function deshabilitar(boton) {
            document.getElementById(boton).style.visibility = 'hidden';
        }
    </script>
    <script type="text/javascript">
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
    </script>
    <asp:Panel ID="panelEncabezado" runat="server">
        <br />
        <asp:ImageButton runat="server" ID="btnGuardar"
            ImageUrl="~/Images/btnGuardar.jpg" ValidationGroup="vgGuardar"
            OnClick="btnGuardar_Click" />
        <asp:ImageButton runat="server" ID="btnGuardar2"
            ImageUrl="~/Images/btnGuardar.jpg" ValidationGroup="vgGuardar"
            OnClick="btnGuardar2_Click" Visible="False" />
        <asp:ImageButton runat="server" ID="btnCancelar" ImageUrl="~/Images/btnCancelar.jpg" OnClick="btnCancelar_Click" />

        <br />
        <table style="width: 85%;">
            <tr>
                <td style="text-align: left;" colspan="8">
                    <asp:Label ID="Lblerror" runat="server"
                        Style="color: #FF0000; font-weight: 700" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 60px; text-align: left;">
                    <span style="font-size: x-small">Oficina</span>
                </td>
                <td style="width: 180px; text-align: left;">
                    <asp:TextBox ID="txtOficina" runat="server" CssClass="textbox" Enabled="False"
                        Width="151px"></asp:TextBox>
                </td>
                <td style="font-size: x-small; width: 140px; text-align: left">Fecha y Hora de Transacción</td>
                <td style="font-size: x-small; width: 120px; text-align: left">
                    <asp:TextBox ID="txtFechaReal" runat="server" CssClass="textbox"
                        Enabled="false" MaxLength="10" Width="120px"></asp:TextBox>
                </td>
                <td style="font-size: x-small; width: 80px; text-align: right">Fecha Pago</td>
                <td style="font-size: x-small; width: 100px; text-align: left">
                    <asp:TextBox ID="txtFechaTransaccion" runat="server" CssClass="textbox"
                        MaxLength="10" Width="100px" AutoPostBack="True"
                        OnDataBinding="txtFechaTransaccion_DataBinding"
                        OnTextChanged="txtFechaTransaccion_TextChanged"></asp:TextBox>
                    <asp:CalendarExtender ID="ceFechaPago" runat="server"
                        DaysModeTitleFormat="dd/MM/yyyy" Enabled="True" Format="dd/MM/yyyy"
                        TargetControlID="txtFechaTransaccion" TodaysDateFormat="dd/MM/yyyy">
                    </asp:CalendarExtender>
                    <asp:RequiredFieldValidator ID="rfvFechaPago" runat="server"
                        ControlToValidate="txtFechaTransaccion" Display="Dynamic"
                        ErrorMessage="Debe ingresar la fecha" ValidationGroup="vgGuardar"></asp:RequiredFieldValidator>
                </td>
                <td style="font-size: x-small; width: 100px; text-align: right">Fecha Contabilización</td>
                <td style="font-size: x-small; width: 100px; text-align: left">
                    <asp:TextBox ID="txtFechaCont" runat="server" CssClass="textbox" MaxLength="10"
                        Width="100px" Enabled="True"></asp:TextBox>
                    <asp:CalendarExtender ID="ceFechaCont" runat="server"
                        DaysModeTitleFormat="dd/MM/yyyy" Enabled="True" Format="dd/MM/yyyy"
                        TargetControlID="txtFechaCont" TodaysDateFormat="dd/MM/yyyy">
                    </asp:CalendarExtender>
                </td>
                <td style="font-size: x-small; width: 100px; text-align: right">Fecha Corte</td>
                <td style="font-size: x-small; width: 100px; text-align: left">
                    <asp:TextBox ID="txtfechacorte" runat="server" CssClass="textbox" MaxLength="10"
                        Width="100px" Enabled="True" OnTextChanged="txtFechaCorte_TextChanged"></asp:TextBox>
                    <asp:CalendarExtender ID="CalendarExtender1" runat="server"
                        DaysModeTitleFormat="dd/MM/yyyy" Enabled="True" Format="dd/MM/yyyy"
                        TargetControlID="txtfechacorte" TodaysDateFormat="dd/MM/yyyy">
                    </asp:CalendarExtender>
                </td>
            </tr>
        </table>
        <hr />

        <table style="width: 80%;">
            <tr>
                <td style="text-align: left" colspan="2">
                    <strong>Datos del Cliente</strong>&nbsp;</td>
                <td style="text-align: right">&nbsp;</td>
                <td style="text-align: left">&nbsp;</td>
                <td style="text-align: left">&nbsp;</td>
            </tr>
            <tr>
                <td style="text-align: left">Tipo Identificación
                </td>
                <td style="text-align: left">
                    <asp:DropDownList ID="ddlTipoIdentificacion" runat="server" CssClass="textbox" Width="180px">
                    </asp:DropDownList>
                </td>
                <td style="text-align: right">Identificación</td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtIdentificacion" runat="server" CssClass="textbox"
                        MaxLength="12" Width="124px"></asp:TextBox>
                    <asp:Button ID="btnConsultaPersonas" CssClass="btn8" runat="server" Text="..."
                        Height="26px" OnClick="btnConsultaPersonas_Click" />
                    <uc1:ListadoPersonas ID="ctlBusquedaPersonas" runat="server" />
                </td>
                <td style="text-align: left">
                    <asp:Button ID="btnConsultar" runat="server" OnClick="btnConsultar_Click"
                        Text="Consultar" ValidationGroup="vgGuardar" />
                </td>
            </tr>
            <tr>
                <td style="text-align: left">Nombres y Apellidos</td>
                <td colspan="4" style="text-align: left">
                    <asp:TextBox ID="txtNombreCliente" runat="server" CssClass="textbox"
                        Enabled="false" Width="582px"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td style="text-align: left">Tipo de Producto
                </td>
                <td style="text-align: left">
                    <asp:DropDownList ID="ddlTipoProducto" runat="server" AutoPostBack="True" CssClass="textbox"
                        OnSelectedIndexChanged="ddlTipoTipoProducto_SelectedIndexChanged" Width="180px">
                    </asp:DropDownList>
                </td>
                <td style="text-align: right">Tipo Movimiento</td>
                <td style="text-align: left">
                    <asp:DropDownList ID="ddlTipoMovimiento" runat="server" AutoPostBack="True"
                        CssClass="textbox"
                        OnSelectedIndexChanged="ddlTipoTipoProducto_SelectedIndexChanged" Width="160px">
                        <asp:ListItem Value="1">INGRESO</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td style="text-align: left">&nbsp;</td>
            </tr>
        </table>
        <table width="80%">
            <tr>
                <td>
                    <div id="divDatos" runat="server" style="overflow: scroll; height: 200px">
                        <asp:GridView ID="gvConsultaDatos" runat="server" Width="99%" AutoGenerateColumns="False"
                            AllowPaging="false" PageSize="5" GridLines="Horizontal" ShowHeaderWhenEmpty="True"
                            OnPageIndexChanging="gvLista_PageIndexChanging"
                            OnRowCommand="gvLista_RowCommand" Style="font-size: x-small"
                            OnSelectedIndexChanged="gvConsultaDatos_SelectedIndexChanged">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:TemplateField HeaderStyle-CssClass="gridIco">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnIdCliente" runat="server" ImageUrl="~/Images/gr_info.jpg"
                                            ToolTip="Estado Cuenta" CommandName='<%#Eval("numero_radicacion")%>' CommandArgument='<%#Eval("valor_a_pagar")%>' />
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="gridIco"></HeaderStyle>
                                </asp:TemplateField>
                                <asp:BoundField DataField="cod_persona" HeaderStyle-CssClass="gridColNo" ItemStyle-CssClass="gridColNo">
                                    <HeaderStyle CssClass="gridColNo" />
                                    <ItemStyle CssClass="gridColNo" />
                                </asp:BoundField>
                                <asp:BoundField DataField="numero_radicacion" HeaderText="Radicado" />
                                <asp:BoundField DataField="linea_credito" HeaderText="Línea Crédito" />
                                <asp:BoundField DataField="Dias_mora" HeaderText="Dias de Mora" />
                                <asp:BoundField DataField="monto_aprobado" HeaderText="Monto Aprobado" DataFormatString="{0:n0}">
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="fecha_aprobacion" HeaderText="Fecha Aprobación" DataFormatString="{0:dd/MM/yyyy}" />
                                <asp:BoundField DataField="valor_cuota" HeaderText="Valor Cuota" DataFormatString="{0:n0}">
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="saldo_capital" HeaderText="Saldo Capital" DataFormatString="{0:n0}">
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="garantias_comunitarias" HeaderText="G.Comunitaria" DataFormatString="{0:n0}">
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="fecha_proxima_pago" HeaderText="Fec. Próx.Pago"
                                    DataFormatString="{0:dd/MM/yyyy}" />
                                <asp:BoundField DataField="valor_a_pagar" HeaderText="Valor a Pagar"
                                    DataFormatString="{0:n0}">
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="valor_CE" HeaderText="Valor C.Extras"
                                    DataFormatString="{0:n0}">
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="total_a_pagar" HeaderText="Valor Total a Pagar"
                                    DataFormatString="{0:n0}">
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
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

                        <%--AFILIACION--%>
                        <asp:GridView ID="gvDatosAfiliacion" runat="server" Width="99%"
                            AutoGenerateColumns="False" PageSize="5" GridLines="Horizontal" ShowHeaderWhenEmpty="True"
                            OnPageIndexChanging="gvLista_PageIndexChanging" DataKeyNames="idafiliacion"
                            Style="font-size: x-small" OnRowEditing="gvDatosAfiliacion_RowEditing">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:CommandField ButtonType="Image" EditImageUrl="~/Images/gr_info.jpg" ShowEditButton="True" />
                                <asp:BoundField DataField="idafiliacion" HeaderText="Código" />
                                <asp:BoundField DataField="fecha_afiliacion" HeaderText="Fecha Afiliación" DataFormatString="{0:d}" HeaderStyle-Width="100px" />
                                <asp:BoundField DataField="valor" HeaderText="Valor Afiliación" DataFormatString="{0:n0}" HeaderStyle-Width="100px">
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="descripcion" HeaderText="Periodicidad" />
                                <asp:BoundField DataField="saldo" HeaderText="Saldo Afiliación" DataFormatString="{0:n0}" HeaderStyle-Width="100px">
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
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

                        <%--AHORRO VISTA--%>
                        <asp:GridView ID="gvAhorroVista" runat="server" Width="99%" AutoGenerateColumns="False"
                            AllowPaging="false" PageSize="5" GridLines="Horizontal" ShowHeaderWhenEmpty="True"
                            Style="font-size: x-small" DataKeyNames="numero_cuenta" OnRowEditing="gvAhorroVista_RowEditing">
                            <Columns>
                                <asp:CommandField ButtonType="Image" EditImageUrl="~/Images/gr_info.jpg" ShowEditButton="True" />
                                <asp:BoundField DataField="numero_cuenta" HeaderText="Num Producto">
                                    <ItemStyle HorizontalAlign="center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="nom_linea" HeaderText="Línea">
                                    <ItemStyle HorizontalAlign="center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="nom_oficina" HeaderText="Oficina">
                                    <ItemStyle HorizontalAlign="left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="fecha_apertura" HeaderText="Fecha Apertura" DataFormatString="{0:d}">
                                    <ItemStyle HorizontalAlign="center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="fecha_proximo_pago" HeaderText="Fec Próx Pago" DataFormatString="{0:d}">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="nom_formapago" HeaderText="Forma Pago">
                                    <ItemStyle HorizontalAlign="left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="nom_periodicidad" HeaderText="Periodicidad">
                                    <ItemStyle HorizontalAlign="left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="saldo_total" HeaderText="Saldo Total" DataFormatString="{0:n0}">
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="saldo_canje" HeaderText="Saldo Canje" DataFormatString="{0:n0}">
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="valor_cuota" HeaderText="Vr. Cuota" DataFormatString="{0:n0}">
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                            </Columns>
                            <HeaderStyle CssClass="gridHeader"></HeaderStyle>
                            <PagerStyle CssClass="gridPager"></PagerStyle>
                            <RowStyle CssClass="gridItem"></RowStyle>
                        </asp:GridView>

                        <%--SERVICIOS--%>
                        <asp:GridView ID="gvServicios" runat="server" Width="100%" GridLines="Horizontal" AutoGenerateColumns="False"
                            AllowPaging="false" OnRowEditing="gvServicios_RowEditing"
                            PageSize="20" HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager"
                            RowStyle-CssClass="gridItem" DataKeyNames="numero_servicio" Style="font-size: x-small">
                            <Columns>
                                <asp:CommandField ButtonType="Image" EditImageUrl="~/Images/gr_edit.jpg" ShowEditButton="True" />
                                <asp:BoundField DataField="numero_servicio" HeaderText="Num. Servicio">
                                    <ItemStyle HorizontalAlign="center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="fecha_solicitud" HeaderText="Fec. Solicitud" DataFormatString="{0:d}">
                                    <ItemStyle HorizontalAlign="center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="nom_linea" HeaderText="Linea">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="nom_plan" HeaderText="Plan">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="num_poliza" HeaderText="Poliza">
                                    <ItemStyle HorizontalAlign="center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="fecha_inicio_vigencia" HeaderText="Fec. Inicial" DataFormatString="{0:d}">
                                    <ItemStyle HorizontalAlign="center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="fecha_final_vigencia" HeaderText="Fec. Final" DataFormatString="{0:d}">
                                    <ItemStyle HorizontalAlign="center" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="F. 1erCuota" DataField="fecha_primera_cuota" DataFormatString="{0:d}" />
                                <asp:BoundField HeaderText="#Cuota" DataField="numero_cuotas" />
                                <asp:BoundField HeaderText="Periodicidad" DataField="nom_periodicidad" />
                                <asp:BoundField HeaderText="Forma de Pago" DataField="forma_pago" />
                                <asp:BoundField HeaderText="Vr.Cuota" DataField="valor_cuota" DataFormatString="{0:n0}" />
                                <asp:BoundField DataField="saldo" HeaderText="Saldo" DataFormatString="{0:n0}" />
                                <asp:BoundField DataField="interes_corriente" HeaderText="Valor a Pagar" DataFormatString="{0:n0}" />
                            </Columns>
                            <HeaderStyle CssClass="gridHeader"></HeaderStyle>
                            <PagerStyle CssClass="gridPager"></PagerStyle>
                            <RowStyle CssClass="gridItem"></RowStyle>
                        </asp:GridView>

                        <%--AHORRO PROGRAMADO--%>
                        <asp:GridView ID="gvProgramado" runat="server" Width="99%" AutoGenerateColumns="False"
                            AllowPaging="false" PageSize="5" GridLines="Horizontal" ShowHeaderWhenEmpty="True"
                            Style="font-size: x-small" DataKeyNames="numero_programado" OnRowEditing="gvProgramado_RowEditing">
                            <Columns>
                                <asp:CommandField ButtonType="Image" EditImageUrl="~/Images/gr_info.jpg" ShowEditButton="True" />
                                <asp:BoundField DataField="numero_programado" HeaderText="Num Producto">
                                    <ItemStyle HorizontalAlign="center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="nomlinea" HeaderText="Línea">
                                    <ItemStyle HorizontalAlign="center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="nomoficina" HeaderText="Oficina">
                                    <ItemStyle HorizontalAlign="left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="fecha_apertura" HeaderText="F. Apertura" DataFormatString="{0:d}">
                                    <ItemStyle HorizontalAlign="center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="fecha_ultimo_pago" HeaderText="F. Ult Mov" DataFormatString="{0:d}">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="fecha_proximo_pago" HeaderText="F. Prox Pago" DataFormatString="{0:d}">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="nomforma_pago" HeaderText="Forma Pago">
                                    <ItemStyle HorizontalAlign="left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="nom_periodicidad" HeaderText="Periodicidad">
                                    <ItemStyle HorizontalAlign="left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="saldo" HeaderText="Saldo Total" DataFormatString="{0:n0}">
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="valor_cuota" HeaderText="Cuota" DataFormatString="{0:n0}">
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="valor_total" HeaderText="Valor Total a Pagar" DataFormatString="{0:n0}">
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                            </Columns>
                            <HeaderStyle CssClass="gridHeader"></HeaderStyle>
                            <PagerStyle CssClass="gridPager"></PagerStyle>
                            <RowStyle CssClass="gridItem"></RowStyle>
                        </asp:GridView>

                        <%--CDAT--%>
                        <asp:GridView ID="gvCdat" runat="server" Width="99%" AutoGenerateColumns="False"
                            AllowPaging="false" PageSize="5" GridLines="Horizontal" ShowHeaderWhenEmpty="True"
                            Style="font-size: x-small" DataKeyNames="codigo_cdat" OnRowEditing="gvCdat_RowEditing">
                            <Columns>
                                <asp:CommandField ButtonType="Image" EditImageUrl="~/Images/gr_info.jpg" ShowEditButton="True" />
                                <asp:BoundField DataField="codigo_cdat" HeaderText="Num Producto">
                                    <ItemStyle HorizontalAlign="center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="numero_cdat" HeaderText="Num Cdat">
                                    <ItemStyle HorizontalAlign="center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="nomlinea" HeaderText="Línea">
                                    <ItemStyle HorizontalAlign="left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="nomoficina" HeaderText="Oficina">
                                    <ItemStyle HorizontalAlign="left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="fecha_apertura" HeaderText="F. Apertura" DataFormatString="{0:d}">
                                    <ItemStyle HorizontalAlign="center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="fecha_inicio" HeaderText="Fecha de Emisión" DataFormatString="{0:d}">
                                    <ItemStyle HorizontalAlign="center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="modalidad" HeaderText="Modalidad">
                                    <ItemStyle HorizontalAlign="center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="nomperiodicidad" HeaderText="Periodicidad">
                                    <ItemStyle HorizontalAlign="center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="plazo" HeaderText="Plazo">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="retencion" HeaderText="Cobra Interés"></asp:BoundField>
                                <asp:BoundField DataField="valor" HeaderText="Valor" DataFormatString="{0:n0}">
                                    <ItemStyle HorizontalAlign="center" />
                                </asp:BoundField>

                                <asp:BoundField DataField="valor_parcial" HeaderText="Valor_Parcial" DataFormatString="{0:n0}">
                                    <ItemStyle HorizontalAlign="center" />
                                </asp:BoundField>
                            </Columns>
                            <HeaderStyle CssClass="gridHeader"></HeaderStyle>
                            <PagerStyle CssClass="gridPager"></PagerStyle>
                            <RowStyle CssClass="gridItem"></RowStyle>
                        </asp:GridView>

                        <%--TOTAL ADEUDADO--%>
                        <asp:GridView ID="gvtotal" runat="server" Width="99%" AutoGenerateColumns="False"
                            AllowPaging="false" PageSize="5" GridLines="Horizontal" ShowHeaderWhenEmpty="True"
                            OnPageIndexChanging="gvLista_PageIndexChanging"
                            OnRowCommand="gvLista_RowCommand" Style="font-size: x-small"
                            OnSelectedIndexChanged="gvConsultaDatos_SelectedIndexChanged">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="CheckBoxgv" runat="server" AutoPostBack="true" OnCheckedChanged="Check_Clicked" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="tipo_producto" HeaderText="Tipo Producto" />
                                <asp:BoundField DataField="numero_radicacion" HeaderText="Numero Producto" />
                                <asp:BoundField DataField="linea_credito" HeaderText="Línea Producto" />
                                <asp:BoundField DataField="Dias_mora" HeaderText="Días de Mora" />
                                <asp:BoundField DataField="fecha_aprobacion" HeaderText="Fecha Apertura" DataFormatString="{0:dd/MM/yyyy}" />
                                <asp:BoundField DataField="valor_cuota" HeaderText="Valor Cuota" DataFormatString="{0:n0}">
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="saldo_capital" HeaderText="Saldo" DataFormatString="{0:n0}">
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="fecha_proxima_pago" HeaderText="Fec. Próx. Pago" DataFormatString="{0:dd/MM/yyyy}" />
                                <asp:BoundField DataField="total_a_pagar" HeaderText="Total a Pagar a la fecha"
                                    DataFormatString="{0:n0}">
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="valor_CE" HeaderText="Vr.Cuotas Extras"
                                    DataFormatString="{0:n0}">
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
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

                    </div>
                </td>
                <td>&nbsp;
                </td>
            </tr>
        </table>
        <hr />
    </asp:Panel>
    <asp:MultiView ID="mvOperacion" runat="server">
        <asp:View ID="vwOperacion" runat="server">
            <table style="width: 100%;">
                <tr>
                    <td style="font-size: small; text-align: left" colspan="7">
                        <strong>Datos de la Transacción</strong></td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td style="width: 25px; font-size: x-small;">&nbsp;</td>
                    <td style="width: 140px; font-size: x-small;">Número del Producto</td>
                    <td style="width: 140px; font-size: x-small;">Tipo de Pago</td>
                    <td style="width: 160px; font-size: x-small;">
                        <table>
                            <tr>
                                <td>
                                    <asp:Label ID="lblNumeroCuotas" runat="server" Text="#Cuo" Visible="false" /></td>
                                <td>
                                    <asp:Label ID="lblTipoValorTransaccion" runat="server" Text="Valor de la Transacción" /></td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 160px; font-size: x-small;">Moneda</td>
                    <td style="width: 15px">
                        <asp:Label ID="LblReferencia" runat="server" Text="Referencia" Visible="False" /></td>
                    <td style="width: 140px; font-size: x-small;">
                        <asp:Label ID="LblIdAvances" runat="server" Text="Id.Avances" Visible="False" /></td>
                    <td style="width: 35px">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td style="width: 25px">&nbsp;</td>
                    <td style="width: 144px">
                        <asp:TextBox ID="txtNumProducto" Enabled="false" runat="server" AutoPostBack="True"
                            CssClass="textbox" MaxLength="12" OnTextChanged="txtNumProducto_TextChanged"
                            Width="134px"></asp:TextBox>
                    </td>
                    <td style="width: 144px">
                        <asp:DropDownList ID="ddlTipoPago" runat="server" CssClass="textbox" Width="114px" AutoPostBack="True"
                            OnSelectedIndexChanged="ddlTipoPago_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                    <td style="width: 160px">
                        <table>
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtNumCuotas" runat="server" AutoPostBack="True" Visible="false"
                                        CssClass="textbox" MaxLength="3" OnTextChanged="txtNumCuotas_TextChanged" Width="20px" />
                                    <asp:FilteredTextBoxExtender ID="txtNumCuotas_FilteredTextBoxExtender"
                                        runat="server" Enabled="True" FilterType="Custom" TargetControlID="txtNumCuotas"
                                        ValidChars="0123456789" />
                                </td>
                                <td>
                                    <uc1:numero ID="txtValTransac" runat="server" CssClass="textbox" Width="150px"
                                        MaxLength="17" style="text-align: right"></uc1:numero>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 163px">
                        <asp:DropDownList ID="ddlMonedas" runat="server" CssClass="textbox"
                            Width="138px">
                        </asp:DropDownList>
                    </td>
                    <td style="width: 12px">
                        <asp:TextBox ID="txtReferencia" runat="server" AutoPostBack="True"
                            CssClass="textbox" MaxLength="12" Visible="False" Width="134px"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Panel ID="upAvances" runat="server" Visible="false">
                            <table>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txtAvances" CssClass="textbox" runat="server" Width="100%" ReadOnly="True" Style="text-align: left" />
                                    </td>
                                    <td>
                                        <asp:Button ID="btnAvances" CssClass="btn8" runat="server" Text="..." Height="26px" Width="100%" OnClick="btnConsultaAvances_Click" />
                                        <uc3:avances ID="listadoavances" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                    <td style="width: 35px">
                        <asp:Button ID="btnGoTran" runat="server" OnClick="btnGoTran_Click"
                            Text="&gt;&gt;" Width="30px" />
                    </td>
                    <td style="width: 35px">
                        <asp:Button ID="btnTotalTran" runat="server" OnClick="Cargar_trans" Visible="false"
                            Text="Pago Total &gt;&gt;" Width="99px" />
                    </td>

                    <td>
                        <asp:CheckBox ID="chkMora" runat="server" Visible="false"
                            Text="Aplicar por Mora" Width="99px" /></td>
                </tr>
                <tr>
                    <td colspan="5">Estado Crédito&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtEstado" runat="server" CssClass="textbox"
                        Enabled="false" ForeColor="Red" Width="673px"></asp:TextBox>
                    </td>
                    <td style="width: 12px">&nbsp;</td>
                    <td style="width: 35px">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="5">
                        <asp:Label ID="lblMsgNroProducto" runat="server" ForeColor="Red"></asp:Label>
                    </td>
                    <td style="width: 12px">&nbsp;</td>
                    <td style="width: 35px">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
            </table>
            <table style="width: 100%;">
                <tr>
                    <td colspan="5" style="text-align: left">
                        <p>
                             <b>Datos de la persona que consigna</b>
                             <asp:CheckBox ID="chkTitular" runat="server" Text="Es el titular" onchange="titular()"></asp:CheckBox>
                        </p>
                    </td>
                </tr>
                <tr id="PersonaOperacion">
                    <td>
                        Tipo Documento<br />
                        <asp:DropDownList ID="ddlTipoDocPersona" runat="server" CssClass="textbox" Width="180px">
                        </asp:DropDownList>
                    </td>
                    <td style="width:19%;">Documento<br />
                        <asp:TextBox ID="txtDocPersona" runat="server" CssClass="textbox" MaxLength="600"></asp:TextBox>
                    </td>
                    <td style="text-align:left;">Primer Nombre<br />
                        <asp:TextBox ID="txtPrimerNombrePersona" runat="server" CssClass="textbox" MaxLength="600"></asp:TextBox>
                    </td>
                    <td style="width:19%;">Segundo Nombre<br />
                        <asp:TextBox ID="txtSegundoNombrePersona" runat="server" CssClass="textbox" MaxLength="600"></asp:TextBox>
                    </td>
                    <td style="text-align:left;">Primer Apellido<br />
                        <asp:TextBox ID="txtPrimerApellidoPersona" runat="server" CssClass="textbox" MaxLength="600"></asp:TextBox>
                    </td>
                    <td style="width:19%;">Segundo Apellido<br />
                        <asp:TextBox ID="txtSegundoApellidoPersona" runat="server" CssClass="textbox" MaxLength="600"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="5" style="text-align: left"><strong>Observaciones</strong> <br />
                        <asp:TextBox ID="txtObservacion" runat="server" TextMode="multiline" CssClass="textbox" style="height: 38px;width: 254%;" MaxLength="600"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="5" style="text-align: left">
                        <strong>Transacciones a Aplicar</strong></td>
                </tr>
                <tr>
                    <td colspan="5">
                        <asp:GridView ID="gvTransacciones" runat="server" AutoGenerateColumns="False"
                            BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px"
                            CellPadding="4" ForeColor="Black" GridLines="Vertical" PageSize="20"
                            Width="60%" OnRowDeleting="gvTransacciones_RowDeleting" OnRowCommand="gvTransacciones_RowCommand"
                            Style="font-size: x-small">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:TemplateField HeaderStyle-CssClass="gridIco">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnEliminar" runat="server" CommandName="Delete" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                            ImageUrl="~/Images/gr_elim.jpg" ToolTip="Eliminar" Width="16px" />
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="gridIco" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-CssClass="gridIco">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnDistPagos" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                            CommandName="DetallePago" ImageUrl="~/Images/gr_info.jpg" ToolTip="Dist Pagos" />
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="gridIco"></HeaderStyle>
                                </asp:TemplateField>
                                <asp:BoundField DataField="moneda" HeaderStyle-CssClass="gridColNo" ItemStyle-CssClass="gridColNo">
                                    <HeaderStyle CssClass="gridColNo" />
                                    <ItemStyle CssClass="gridColNo" />
                                </asp:BoundField>
                                <asp:BoundField DataField="tipo" HeaderStyle-CssClass="gridColNo" ItemStyle-CssClass="gridColNo">
                                    <HeaderStyle CssClass="gridColNo" />
                                    <ItemStyle CssClass="gridColNo" />
                                </asp:BoundField>
                                <asp:BoundField DataField="tproducto" HeaderStyle-CssClass="gridColNo" ItemStyle-CssClass="gridColNo">
                                    <HeaderStyle CssClass="gridColNo" />
                                    <ItemStyle CssClass="gridColNo" />
                                </asp:BoundField>
                                <asp:BoundField DataField="tipomov" HeaderStyle-CssClass="gridColNo" ItemStyle-CssClass="gridColNo">
                                    <HeaderStyle CssClass="gridColNo" />
                                    <ItemStyle CssClass="gridColNo" />
                                </asp:BoundField>
                                <asp:BoundField DataField="nomtipo" HeaderText="Tipo" />
                                <asp:BoundField DataField="nomtproducto" HeaderText="T.Producto" />
                                <asp:BoundField DataField="nroRef" HeaderText="# Ref" />
                                <asp:BoundField DataField="valor" DataFormatString="{0:N}" HeaderText="Valor">
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="nommoneda" HeaderText="Moneda" />
                                <asp:BoundField DataField="tipopago" HeaderText="Tipo Pago" />
                                <asp:BoundField DataField="codtipopago" HeaderStyle-CssClass="gridColNo" ItemStyle-CssClass="gridColNo">
                                    <HeaderStyle CssClass="gridColNo" />
                                    <ItemStyle CssClass="gridColNo" />
                                </asp:BoundField>
                                <asp:BoundField DataField="referencia" HeaderText="Referencia" />
                                <asp:BoundField DataField="idavance" HeaderText="Id.Avance" />
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
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left; width: 70px">Valor Total
                    </td>
                    <td style="text-align: left;">
                        <asp:TextBox ID="txtValorTran" runat="server" CssClass="textbox"
                            Enabled="false" Width="170px" Style="text-align: right"></asp:TextBox>
                        <%--<asp:MaskedEditExtender ID="MaskedEditExtender2" runat="server" TargetControlID="txtValorTran" Mask="999,999,999,999.99" MessageValidatorTip="true" 
                            OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="Number" InputDirection="RightToLeft" 
                            AcceptNegative="Left" DisplayMoney="Left" ErrorTooltipEnabled="True"></asp:MaskedEditExtender>--%>
                        
                
                    </td>
                </tr>
            </table>
        </asp:View>
        <asp:View ID="vwFormaPago" runat="server">
            <asp:UpdatePanel ID="upFormaPago" runat="server">
                <ContentTemplate>
                    <table style="width: 95%;" cellpadding="2" cellspacing="0">
                        <tr>
                            <td style="vertical-align: top; text-align: left">
                                <table style="width: 100%;" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td colspan="5">
                                            <strong>Forma de Pago</strong>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Moneda</td>
                                        <td>Forma de Pago</td>
                                        <td>Valor</td>
                                        <td>
                                            <asp:Label ID="lblBancoConsignacion" runat="server"
                                                Style="color: #FF0000; font-size: xx-small;" Visible="False"></asp:Label>
                                            <asp:Label ID="lblProcesoContable" runat="server" Text="Cta Contable"
                                                Style="color: #FF0000; font-size: xx-small;" Visible="False"></asp:Label>
                                            <asp:Label ID="lblboucher" runat="server"
                                                Style="color: #FF0000; font-size: xx-small;" Text="N. Baucher" Visible="False"></asp:Label>
                                        </td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:DropDownList ID="ddlMoneda" runat="server" CssClass="textbox" Width="90px" />
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlFormaPago" runat="server" CssClass="textbox"
                                                Width="90px" AutoPostBack="True"
                                                OnSelectedIndexChanged="ddlFormaPago_SelectedIndexChanged" />
                                        </td>
                                        <td>
                                            <uc1:numero ID="txtValor" runat="server" CssClass="textbox" Width="80px"></uc1:numero>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlBancoConsignacion" runat="server" CssClass="textbox" Width="80px" Visible="False"></asp:DropDownList>
                                            <asp:DropDownList ID="ddlProcesoContable" runat="server" CssClass="textbox" Width="80px" Visible="False"></asp:DropDownList>
                                            <asp:TextBox ID="txtBaucher" runat="server" CssClass="textbox" MaxLength="20"
                                                Width="50px" Visible="False"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:Button ID="btnGoFormaPago" runat="server" OnClick="btnGoFormaPago_Click"
                                                Text="&gt;&gt;" Width="25px" Height="26px" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="5">
                                            <asp:GridView ID="gvFormaPago" runat="server"
                                                AutoGenerateColumns="False" BackColor="White" BorderColor="#DEDFDE"
                                                BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black"
                                                GridLines="Vertical" PageSize="20" Width="100%"
                                                Style="text-align: left; font-size: x-small">
                                                <AlternatingRowStyle BackColor="White" />
                                                <Columns>
                                                    <asp:BoundField DataField="moneda">
                                                        <HeaderStyle CssClass="gridColNo" />
                                                        <ItemStyle CssClass="gridColNo" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="fpago">
                                                        <HeaderStyle CssClass="gridColNo" HorizontalAlign="Center" />
                                                        <ItemStyle CssClass="gridColNo" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="nommoneda" HeaderText="Moneda" />
                                                    <asp:BoundField DataField="nomfpago" HeaderText="F.Pago">
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="valor" HeaderText="Valor" DataFormatString="{0:N0}">
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="tipomov">
                                                        <HeaderStyle CssClass="gridColNo" />
                                                        <ItemStyle CssClass="gridColNo" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="cod_banco" HeaderText="Cta Bancaria o Baucher">
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:BoundField>
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
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right;" colspan="5">&nbsp;&nbsp;&nbsp;
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td>&nbsp;&nbsp;
                            </td>
                            <td style="vertical-align: top; text-align: left">
                                <table style="width: 100%;" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td>
                                            <strong>Cheques</strong></td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td style="font-size: x-small;">Núm. Cheque</td>
                                        <td style="font-size: x-small">Entidad Bancaria</td>
                                        <td style="font-size: x-small">Valor Cheque</td>
                                        <td style="font-size: x-small">Moneda</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="txtNumCheque" runat="server" CssClass="textbox" Width="60px" MaxLength="20"></asp:TextBox>
                                            <asp:Label ID="numchequevacio" runat="server" Style="color: #FF0000; font-size: xx-small;"></asp:Label>
                                        </td>

                                        <td>
                                            <asp:DropDownList ID="ddlBancos" runat="server" CssClass="textbox" Width="120px"></asp:DropDownList>
                                            <asp:Label ID="bancochquevacio" runat="server" Style="color: #FF0000; font-size: xx-small;"></asp:Label>
                                        </td>
                                        <td>
                                            <uc1:decimales ID="txtValCheque" runat="server" CssClass="textbox" Width="90px"></uc1:decimales>
                                            <asp:Label ID="valorchequevacio" runat="server" Style="color: #FF0000; font-size: xx-small;"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlMonCheque" runat="server" CssClass="textbox" Width="80px"></asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:Button ID="btnGoCheque" runat="server" OnClick="btnGoCheque_Click" Text="&gt;&gt;" Width="25px" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="5">
                                            <asp:GridView ID="gvCheques" runat="server"
                                                AutoGenerateColumns="False" BackColor="White" BorderColor="#DEDFDE"
                                                BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black"
                                                GridLines="Vertical" PageSize="20" Style="font-size: x-small" Width="100%"
                                                ShowHeaderWhenEmpty="True" OnRowDeleting="gvCheques_RowDeleting">
                                                <AlternatingRowStyle BackColor="White" />
                                                <Columns>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="btnEliminar" runat="server" CommandName="Delete"
                                                                ImageUrl="~/Images/gr_elim.jpg" ToolTip="Eliminar" Width="16px" />
                                                        </ItemTemplate>
                                                        <HeaderStyle CssClass="gridIco" />
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="entidad">
                                                        <HeaderStyle CssClass="gridColNo" />
                                                        <ItemStyle CssClass="gridColNo" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="moneda">
                                                        <HeaderStyle CssClass="gridColNo" />
                                                        <ItemStyle CssClass="gridColNo" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="numcheque" HeaderText="Núm. Cheque" />
                                                    <asp:BoundField DataField="nomentidad" HeaderText="Entidad" />
                                                    <asp:BoundField DataField="valor" DataFormatString="{0:N0}" HeaderText="Valor">
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="nommoneda" HeaderText="Moneda" />
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
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" style="text-align: right">
                                            <asp:Label ID="lblValTotalCheque" runat="server" Text="Valor Total Cheques" />
                                            &nbsp;&nbsp;</td>
                                        <td colspan="2">
                                            <asp:TextBox ID="txtValTotalCheque" runat="server" CssClass="textbox"
                                                Enabled="False" Width="171px" Style="text-align: right"></asp:TextBox>
                                            <asp:MaskedEditExtender ID="MaskedEditExtender5" runat="server"
                                                TargetControlID="txtValTotalCheque" Mask="999,999,999,999,999"
                                                MaskType="Number" InputDirection="RightToLeft"
                                                AcceptNegative="Left" DisplayMoney="Left" ErrorTooltipEnabled="True"
                                                CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder=""
                                                CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder=""
                                                CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True">
                                            </asp:MaskedEditExtender>
                                        </td>
                                        <td>&nbsp;</td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <table style="width: 80%;" cellpadding="2" cellspacing="0">
                        <tr>
                            <td style="text-align: right; width: 100px">Valor Total
                            </td>
                            <td style="text-align: right; width: 120px">
                                <asp:TextBox ID="txtValTotalFormaPago" runat="server" CssClass="textbox" Enabled="false" Width="120px" Style="text-align: right"></asp:TextBox>
                                <asp:MaskedEditExtender ID="MaskedEditExtender6" runat="server" TargetControlID="txtValTotalFormaPago" Mask="999,999,999,999,999.99" MessageValidatorTip="true"
                                    OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="Number" InputDirection="RightToLeft"
                                    AcceptNegative="Left" DisplayMoney="Left" ErrorTooltipEnabled="True">
                                </asp:MaskedEditExtender>

                            </td>
                            <td style="text-align: right;">
                                <asp:Button runat="server" ID="mpePopUp" class="enlace" role="link" Text="Ver Documento" OnClick="mpePopUp_OnClick" Visible="False" />
                            </td>
                            <td style="text-align: right; width: 100px"></td>
                            <td style="text-align: right; width: 120px"></td>
                            <td style="text-align: right;"></td>
                            <td style="text-align: right; width: 140px"></td>
                            <td style="text-align: right; width: 120px"></td>
                            <td style="text-align: right;"></td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:Panel runat="server" ClientIDMode="Static" ID="Panel1">
                <table style="width: 100%">
                    <tr>
                        <td>
                            <rsweb:ReportViewer ID="RpviewInfo" runat="server" Font-Names="Verdana" Font-Size="8pt"
                                Height="300px" InteractiveDeviceInfos="(Colección)" WaitMessageFont-Names="Verdana"
                                WaitMessageFont-Size="14pt" Width="100%" ShowBackButton="False"
                                ShowCredentialPrompts="False" ShowDocumentMapButton="False"
                                ShowFindControls="False" ShowPageNavigationControls="False"
                                ShowParameterPrompts="False" ShowPromptAreaButton="False"
                                ShowRefreshButton="False" ShowToolBar="False" ShowWaitControlCancelLink="False"
                                ShowZoomControl="False" SizeToReportContent="True">
                                <LocalReport ReportPath="Page\Tesoreria\PagosVentanilla\rptDeclaracion.rdlc">
                                </LocalReport>
                            </rsweb:ReportViewer>



                            <asp:UpdatePanel runat="server">
                                <ContentTemplate>
                                    <%--<asp:Button ID="Button1" runat="server" Text="Imprimir Informe" Visible="true"
                                        Style="width: 115px; text-align: left;" OnClientClick="javascript: PrintReportBrowserDirectlyWithoutShowPDF('Panel1');" />--%>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <%--<input id="Button1" onclick="javascript: imprimir();" type="button" value="Imprimir Factura." style="width: 130px;" />--%>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
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
                            <asp:Label ID="lblMensajeGrabar" runat="server" Text="Pago Aplicado Correctamente"></asp:Label>
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
                            <asp:Button ID="btnContinuar" runat="server" Text="Continuar"
                                OnClick="btnContinuar_Click" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </asp:View>
    </asp:MultiView>
    <asp:ModalPopupExtender ID="MpeDetallePagoAportes" runat="server" Enabled="True" PopupDragHandleControlID="Panelf"
        PopupControlID="PanelDetallePagoAportes" TargetControlID="HiddenField1" CancelControlID="btnCloseAct2">
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
    <asp:ModalPopupExtender ID="MpeDetalleAvances" runat="server" Enabled="True" PopupDragHandleControlID="Panelf"
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
    <asp:ModalPopupExtender ID="GeneradorDocumento" runat="server" Enabled="True" PopupDragHandleControlID="Panelsw"
        PopupControlID="PanelDocumentos" TargetControlID="HiddenField1" CancelControlID="btnCloseAct2">
    </asp:ModalPopupExtender>
    
    <asp:ModalPopupExtender ID="MpeDetallePago" runat="server" Enabled="True" PopupDragHandleControlID="Panelf"
        PopupControlID="PanelDetallePago" TargetControlID="HiddenField1" CancelControlID="btnCloseAct">
        <Animations>
            <OnHiding>
                <Sequence>                            
                    <StyleAction AnimationTarget="btnCloseAct" Attribute="display" Value="none" />
                    <Parallel>
                        <FadeOut />
                        <Scale ScaleFactor="5" />
                    </Parallel>
                </Sequence>
            </OnHiding>            
        </Animations>
    </asp:ModalPopupExtender>
    <asp:HiddenField ID="HiddenField1" runat="server" />
    <asp:Panel ID="PanelDetallePago" runat="server" Width="480px" Style="display: none; border: solid 2px Gray" CssClass="modalPopup">
        <asp:UpdatePanel ID="upDetallePago" runat="server">
            <ContentTemplate>
                <table>
                    <tr>
                        <td style="font-size: x-small; color: #FFFFFF; background-color: #0066FF; width: 434px">
                            <asp:Panel ID="Panelf" runat="server" Width="475px" Style="cursor: move">
                                <strong>Detalle del Pago</strong>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 475px">
                            <asp:GridView ID="GVDetallePago" runat="server"
                                AutoGenerateColumns="False" BackColor="White" BorderColor="#DEDFDE"
                                BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black"
                                GridLines="Vertical" PageSize="20" Width="300px"
                                Style="text-align: left; font-size: xx-small">
                                <AlternatingRowStyle BackColor="White" />
                                <Columns>
                                    <asp:BoundField DataField="NumCuota" HeaderText="No." DataFormatString="{0:N0}">
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="FechaCuota" HeaderText="F.Pago"
                                        DataFormatString="{0:d}">
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Capital" HeaderText="Capital" DataFormatString="{0:N0}">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="IntCte" HeaderText="Int.Cte" DataFormatString="{0:N0}">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="IntMora" HeaderText="Int.Mora" DataFormatString="{0:N0}">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="LeyMiPyme" HeaderText="Ley MiPyme" DataFormatString="{0:N0}">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ivaLeyMiPyme" HeaderText="Iva Ley MiPyme" DataFormatString="{0:N0}">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Otros" HeaderText="Otros" DataFormatString="{0:N0}">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Poliza" HeaderText="Póliza" DataFormatString="{0:N0}">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Total" HeaderText="Total" DataFormatString="{0:N0}">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Saldo" HeaderText="Saldo" DataFormatString="{0:N0}">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
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
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 475px; background-color: #0066FF">
                            <asp:Button ID="btnCloseAct" runat="server" Text="Cerrar" CssClass="button" OnClick="btnCloseAct_Click" CausesValidation="false" Height="20px" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
    <asp:Panel ID="PanelDetallePagoAportes" runat="server" Width="480px" Style="display: none; border: solid 2px Gray" CssClass="modalPopup">
        <asp:UpdatePanel ID="UpDetallePagoApo" runat="server">
            <ContentTemplate>
                <table>
                    <tr>
                        <td style="font-size: x-small; color: #FFFFFF; background-color: #0066FF; width: 434px">
                            <asp:Panel ID="Panel9" runat="server" Width="475px" Style="cursor: move">
                                <strong>Detalle del Pago</strong>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 475px; margin-left: 120px;">
                            <asp:GridView ID="GvPagosAPortes" runat="server"
                                AutoGenerateColumns="False" BackColor="White" BorderColor="#DEDFDE"
                                BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black"
                                GridLines="Vertical" PageSize="20" Width="475px"
                                Style="text-align: left; font-size: xx-small">
                                <AlternatingRowStyle BackColor="White" />
                                <Columns>

                                    <asp:BoundField DataField="FechaCuota" HeaderText="F.Pago"
                                        DataFormatString="{0:d}">
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Capital" HeaderText="ValorPago" DataFormatString="{0:N0}">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Total" HeaderText="Total" DataFormatString="{0:N0}">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Saldo" HeaderText="Saldo" DataFormatString="{0:N0}">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
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
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 475px; background-color: #0066FF">
                            <asp:Button ID="btnCloseAct2" runat="server" Text="Cerrar" CssClass="button" OnClick="btnCloseAct2_Click" CausesValidation="false" Height="20px" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
    <asp:Panel ID="PanelDetalleAvance" runat="server" Width="480px" Style="display: none; border: solid 2px Gray" CssClass="modalPopup">
        <asp:UpdatePanel ID="UpDetalleAvances" runat="server">
            <ContentTemplate>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
    <asp:Panel ID="PanelDocumentos" runat="server" Width="47pc" Style="display: none; border: solid 2px Gray" CssClass="modalPopup">
        <asp:UpdatePanel ID="PanelDocumento" runat="server">
            <ContentTemplate>
                <table>
                    <tr>
                        <td style="font-size: x-small; color: #FFFFFF; background-color: #0066FF; width: 434px">
                            <asp:Panel ID="Panelsw" runat="server" Width="748px" Style="cursor: move">
                                <strong>Documento Declaracion</strong>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Literal ID="LiteralDcl" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="Button1" runat="server" Text="cerrar" CssClass="button" CausesValidation="False" Height="20px" OnClick="btneCancelar_Click" Style="margin: 0 auto;" />
                        </td>
                    </tr>

                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>

    <uc4:mensajegrabar ID="ctlMensaje" runat="server" />
    <asp:Panel ID="panelProceso" runat="server" Width="100%">
        <uc2:procesoContable ID="ctlproceso" runat="server" />
    </asp:Panel>
    <script type="text/javascript">
        function titular() {
            if ($("#cphMain_chkTitular").is(':checked')) {
                $("#PersonaOperacion").hide();
                $("#cphMain_txtObservacion").css("width", "100%");
            } else {
                $("#PersonaOperacion").show();
                $("#cphMain_txtObservacion").css("width", "254%");
            }
        }
    </script>
</asp:Content>
