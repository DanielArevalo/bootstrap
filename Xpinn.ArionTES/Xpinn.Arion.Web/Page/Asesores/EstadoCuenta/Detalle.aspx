<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" ValidateRequest="false" AutoEventWireup="true" EnableEventValidation="false"
    CodeFile="Detalle.aspx.cs" Inherits="Detalle" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Src="~/General/Controles/imprimir.ascx" TagName="imprimir" TagPrefix="ucImprimir" %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/ctlFormaPago.ascx" TagName="Forma" TagPrefix="uc3" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>
<%@ Register Src="~/General/Controles/ctlMora.ascx" TagName="ctlMora" TagPrefix="ucm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <style type="text/css">
        table.tabs {
            border-collapse: separate;
            border-spacing: 0;
            background-color: green 0px;
            font-size: 0.5em;
            width: 100px;
            margin-right: 2px;
            height: 100px;
        }

        th.tabck {
            border: red 0px;
            border-bottom: 0;
            border-radius: 0.0em 0.0em 0 0;
            background-color: green 0px;
            padding: 0.3em;
            text-align: center;
            cursor: pointer;
        }

        tr.filadiv {
            background-color: rgb(255, 255, 255);
        }

        /* El ancho y alto de los div.tabdiv se configuran en cada aplicación */
        div.tabdiv {
            background-color: rgb(255, 255, 255);
            border: 0;
            padding: 0.5em;
            overflow: auto;
            display: none;
            width: 100%;
            height: auto;
        }

        /* Anchos y altos para varios contenedores en la misma página. Esta parte se particulariza para cada contenedor. (IE8 necesita !important) */
        td#tab-0 > div {
            width: 25em !important;
            height: 25em !important;
        }

        .style1 {
            width: 13px;
        }
    </style>
    <script type="text/javascript">
        var hola = '<%= RpviewEstado.ClientID %>';

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
    <script type="text/javascript" src="../../../Scripts/duplicate.js"></script>

    <table cellpadding="0" cellspacing="0" style="width: 100%">
        <tr>
            <td>
                <table width="100%">
                    <tr>
                        <td style="width: 3%; text-align: left">
                            <asp:Button ID="btnAgregarLink" CssClass="btn8" runat="server" Text=" + " Height="26px"
                                OnClick="btnAgregarLink_Click" />
                        </td>
                        <td style="width: 3%; text-align: left">
                            <asp:Button ID="btnEliminarLink" CssClass="btn8" runat="server" Text=" X " Height="26px"
                                OnClick="btnEliminarLink_Click" />
                        </td>
                        <td style="width: 79%; text-align: left">
                            <asp:PlaceHolder ID="phLinks" runat="server"></asp:PlaceHolder>
                        </td>
                        <td style="width: 15%; text-align: right">
                            <ucm:ctlMora ID="ctlMora" runat="server" width="200px"></ucm:ctlMora>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Panel ID="pConsulta" runat="server">
                    <div style="border-style: none; border-width: medium; background-color: #f5f5f5">
                        <table style="width: 96%; height: 179px;" border="0">
                            <tbody style="text-align: center">
                                <tr>
                                    <td style="width: 153px">Fecha&nbsp; Generación<br />
                                        <asp:TextBox ID="txtFechaGeneracion" runat="server" CssClass="textbox"
                                            Style="text-align: center" Enabled="false"></asp:TextBox>
                                    </td>
                                    <td>Código<br />
                                        <asp:TextBox ID="txtCodigo" runat="server" CssClass="textbox" Enabled="false"
                                            Style="text-align: center"></asp:TextBox>
                                    </td>
                                    <td>Tipo Identificación<br />
                                        <asp:TextBox ID="txtTipoDoc" runat="server" CssClass="textbox" Enabled="false"
                                            Style="text-align: center" Width="130px"></asp:TextBox>
                                    </td>
                                    <td style="text-align: center">Identificación<br />
                                        <asp:TextBox ID="txtNumDoc" runat="server" CssClass="textbox" Enabled="false"
                                            Style="text-align: center"></asp:TextBox>
                                    </td>
                                    <td class="style3">Tipo Cliente<br />
                                        <asp:TextBox ID="txtTipoCliente" runat="server" CssClass="textbox"
                                            Style="text-align: center" Enabled="false" Width="163px"></asp:TextBox>
                                    </td>
                                    <td rowspan="5" style="text-align: center;" width="30%">
                                        <script type="text/javascript">
                                            window.onload = function () {
                                                activarTab(document.getElementById("tabck-0"));
                                            };
                                            function activarTab(unTab) {
                                                try {
                                                    //Los elementos div de todas las pestañas están todos juntos en una
                                                    //única celda de la segunda fila de la tabla de estructura de pestañas.
                                                    //Hemos de buscar la seleccionada, ponerle display block y al resto
                                                    //ponerle display none.
                                                    var id = unTab.id;
                                                    if (id) {
                                                        var tr = unTab.parentNode || unTab.parentElement;
                                                        var tbody = tr.parentNode || tr.parentElement;
                                                        var table = tbody.parentNode || tbody.parentElement;
                                                        //Pestañas en varias filas
                                                        if (table.getAttribute("data-filas") != null) {
                                                            var filas = tbody.getElementsByTagName("tr");
                                                            var filaDiv = filas[filas.length - 1];
                                                            tbody.insertBefore(tr, filaDiv);
                                                        }
                                                        //Para compatibilizar con la versión anterior, si la tabla no tiene los
                                                        //atributos data-min y data-max le ponemos los valores que tenían antes del
                                                        //cambio de versión.
                                                        var desde = table.getAttribute("data-min");
                                                        if (desde == null) desde = 0;
                                                        var hasta = table.getAttribute("data-max");
                                                        if (hasta == null) hasta = MAXTABS;
                                                        var idTab = id.split("tabck-");
                                                        var numTab = parseInt(idTab[1]);
                                                        //Las "tabdiv" son los bloques interiores mientras que los "tabck"
                                                        //son las pestañas.
                                                        var esteTabDiv = document.getElementById("tabdiv-" + numTab);
                                                        for (var i = desde; i <= hasta; i++) {
                                                            var tabdiv = document.getElementById("tabdiv-" + i);
                                                            if (tabdiv) {
                                                                var tabck = document.getElementById("tabck-" + i);
                                                                if (tabdiv.id == esteTabDiv.id) {
                                                                    tabdiv.style.display = "block";
                                                                    tabck.style.color = "white";
                                                                    tabck.style.backgroundColor = "blue";
                                                                    tabck.style.borderBottomColor = "blue";
                                                                } else {
                                                                    tabdiv.style.display = "none";
                                                                    tabck.style.color = "slategrey";
                                                                    tabck.style.backgroundColor = "rgb(235, 235, 225)";
                                                                    tabck.style.borderBottomColor = "rgb(235, 235, 225)";
                                                                }
                                                            }
                                                        }
                                                    }
                                                } catch (e) {
                                                    alert("Error al activar una pestaña. " + e.message);
                                                }
                                            }
                                        </script>
                                        <table class="tabs" data-max="2" data-min="0">
                                            <tr>
                                                <th class="tabcks" style="height: 1px"></th>
                                                <th id="tabck-0" class="tabck" onclick="activarTab(this)"
                                                    style="height: 1px; width: 63px">Foto
                                                </th>
                                                <th class="tabcks" style="height: 1px"></th>
                                                <th id="tabck-1" class="tabck" onclick="activarTab(this)"
                                                    style="height: 1px; width: 77px">Huella
                                                </th>
                                                <th id="tabck-2" class="tabck" onclick="activarTab(this)"
                                                    style="height: 1px; width: 52px"></th>
                                            </tr>
                                            <tr class="filadiv">
                                                <td id="tab-0" class="style1" colspan="5">
                                                    <div id="tabdiv-0" class="tabdiv">
                                                        <h1>
                                                            <asp:HiddenField ID="hdFileName" runat="server" />
                                                            <asp:HiddenField ID="hdFileNameThumb" runat="server" />
                                                            <asp:Image ID="imgFoto" runat="server" Height="160px"
                                                                Width="140px" />
                                                        </h1>
                                                    </div>
                                                    <div id="tabdiv-1" class="tabdiv">
                                                        <p>
                                                            <asp:Image ID="imgHuella" runat="server"
                                                                AlternateText="Image text" ImageAlign="left"
                                                                ImageUrl="Images/huella.jpg" Height="160px"
                                                                Width="140px" />
                                                        </p>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 153px; margin-left: 40px;">Nombres<br />
                                        <asp:TextBox ID="txtPrimerNombre" runat="server" CssClass="textbox"
                                            Enabled="false"></asp:TextBox>
                                    </td>
                                    <td>Apellidos<br />
                                        <asp:TextBox ID="txtPrimerApellido" runat="server" CssClass="textbox"
                                            Enabled="false"></asp:TextBox>
                                        <br />
                                    </td>
                                    <td style="text-align: left">
                                        <table cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td>Estado<br />
                                                    <asp:TextBox ID="txtestado" runat="server" CssClass="textbox"
                                                        Enabled="false" Width="80px"></asp:TextBox>
                                                </td>
                                                <td>Motivo<br />
                                                    <asp:TextBox ID="txtMotivo" Style="text-align: right" runat="server"
                                                        CssClass="textbox" Enabled="false" Width="80px"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td style="text-align: left">
                                        <table cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td>Teléfono</td>
                                                <td>Celular</td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtTelefono" runat="server" CssClass="textbox"
                                                        Enabled="false" Width="70%"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtCelular" runat="server" CssClass="textbox"
                                                        Enabled="false" Width="90%"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td class="style3">Dirección<br />
                                        <asp:TextBox ID="txtDireccion" runat="server" CssClass="textbox" Enabled="false"
                                            Width="163px"></asp:TextBox>
                                        <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 153px">Fecha Ingreso<br />
                                        <asp:TextBox ID="txtFechaAfiliacion" runat="server" CssClass="textbox"
                                            Enabled="false" />
                                    </td>
                                    <td>Zona<br />
                                        <asp:TextBox ID="txtZona" runat="server" CssClass="textbox" Enabled="false" />
                                    </td>
                                    <td style="text-align: center">Ciudad<br />
                                        <asp:TextBox ID="txtCiudad" runat="server" CssClass="textbox" Enabled="false">
                                        </asp:TextBox>
                                    </td>
                                    <td>Ejecutivo<br />
                                        <asp:TextBox ID="txtEjecutivo" runat="server" CssClass="textbox"
                                            Enabled="false"></asp:TextBox>
                                    </td>
                                    <td class="style3">Correo Electrónico<br />
                                        <asp:TextBox ID="txtEmail" runat="server" CssClass="textbox" Enabled="false"
                                            Width="163px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="text-align: center">Pagadurias<br />
                                        <asp:TextBox ID="txtPagadurias" runat="server" CssClass="textbox" Width="90%"
                                            Enabled="false" placeholder="No existen datos..." />
                                    </td>
                                    <td style="text-align: center">Oficina<br />
                                        <asp:TextBox ID="txtOficina" runat="server" CssClass="textbox" Enabled="false">
                                        </asp:TextBox>
                                    </td>
                                    <td colspan="2">
                                        <strong>Alertas</strong><br />
                                        <asp:TextBox Style="overflow: hidden; line-height: 7px; text-align: center"
                                            BorderStyle="None" BackColor="White" Font-Overline="false" Width="100%"
                                            ID="txtAlertas" runat="server" class="facebook" Visible="True"
                                            ForeColor="red" TextMode="MultiLine" ToolTip="Alertas" Height="40px">
                                        </asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3"></td>
                                    <td colspan="2">
                                        <strong>Comentarios</strong><br />
                                        <asp:TextBox
                                            Style="overflow: scroll; resize: none; line-height: 12px; text-align: left"
                                            BorderStyle="None" BackColor="White" Font-Overline="false" Width="100%"
                                            ID="txtComentarios" runat="server" class="facebook" Visible="True"
                                            ForeColor="red" Enabled="false" TextMode="MultiLine" ToolTip="Alertas"
                                            Height="50px"></asp:TextBox>
                                        <%--<asp:Label ID="lblComentarios" Width="100%" runat="server" BackColor="White" ForeColor="Red" Height="40px">--%>
                                        <asp:Label runat="server"></asp:Label>
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
                <asp:Panel ID="pDatosAfiliacion" runat="server">
                    <asp:Panel ID="pEncBusqueda" runat="server" CssClass="collapsePanelHeader" Height="30px">
                        <div style="padding: 5px; cursor: pointer; vertical-align: middle;">
                            <div style="float: left; color: #0066FF; font-size: small">
                                Afiliación
                            </div>
                            <div style="float: left; margin-left: 5px; color: #0066FF; font-size: small">
                                <asp:Label ID="lblMostrarDetalles" runat="server">(Mostrar Detalles...)</asp:Label>
                                <asp:ImageButton ID="imgExpand" runat="server" AlternateText="(Show Details...)"
                                    ImageUrl="~/Images/expand.jpg" />
                            </div>
                            <br />
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="pConsultaAfiliacion" runat="server" Width="989px" Enabled="false">
                        <div style="border-style: none; border-width: medium; background-color: #f5f5f5">
                            <table>
                                <tr>
                                    <td style="text-align: center; width: 160px">Fecha de Afiliación<br />
                                        <asp:TextBox ID="txtcodAfiliacion" runat="server" Width="100px"
                                            CssClass="textbox" Style="text-align: right" Visible="false" />
                                        <uc1:fecha ID="txtFechaAfili" runat="server" Enabled="True"
                                            style="width: 140px" />
                                    </td>

                                    <td style="text-align: center; width: 160px">Fecha de Rétiro<br />
                                        <uc1:fecha ID="txtFechaRetiro" runat="server" Enabled="True"
                                            style="width: 140px" />
                                    </td>
                                    <td style="text-align: center; width: 180px">Estado<br />
                                        <asp:DropDownList ID="ddlEstadoAfi" runat="server" Width="140px"
                                            CssClass="textbox" />
                                    </td>
                                    <td style="text-align: center;">Forma de Pago<br />
                                        <uc3:Forma ID="ddlFormaPago" runat="server" cssclass="textbox" Width="140px" />
                                    </td>
                                    <td style="text-align: center;">
                                        <asp:Label ID="lblEmpresa" runat="server" Text="Empresa" /><br />
                                        <asp:DropDownList ID="ddlEmpresa" runat="server" CssClass="textbox"
                                            Width="160px" />
                                    </td>
                                    <td style="text-align: center; width: 160px">Fecha Actualización Datos<br />
                                        <uc1:fecha ID="txtFechaActualizacion" runat="server" Enabled="True"
                                            style="width: 140px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: center;">Fecha de 1er Pago<br />
                                        <uc1:fecha ID="txtFecha1Pago" runat="server" style="width: 140px" />
                                    </td>
                                    <td>Fecha Prox.Pago<br />
                                        <uc1:fecha ID="txtFechaProxPago" runat="server" style="width: 140px" />
                                    </td>
                                    <td style="text-align: center;">Valor<br />
                                        <uc1:decimales ID="txtValorAfili" runat="server" style="text-align: right;" />
                                    </td>
                                    <td>Saldo<br />
                                        <uc1:decimales ID="txtSaldoAfili" runat="server" style="text-align: right;"
                                            Width="100px" />
                                    </td>
                                    <td style="text-align: center; width: 200px">Periodicidad<br />
                                        <asp:DropDownList ID="ddlPeriodicidad" runat="server" Width="160px"
                                            CssClass="textbox" />
                                    </td>
                                    <td style="text-align: center; width: 160px">Nro Cuotas<br />
                                        <asp:TextBox ID="txtCuotasAfili" runat="server" Width="70px" CssClass="textbox"
                                            Style="text-align: right" />
                                        <asp:FilteredTextBoxExtender ID="txtValor_FilteredTextBoxExtender"
                                            runat="server" Enabled="True" FilterType="Numbers, Custom"
                                            TargetControlID="txtCuotasAfili" ValidChars="" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </asp:Panel>
                    <asp:CollapsiblePanelExtender ID="cpeDemo" runat="Server" CollapseControlID="pEncBusqueda"
                        Collapsed="True" CollapsedImage="~/Images/expand.jpg"
                        CollapsedText="(Click Aqui para Mostrar Detalles...)" ExpandControlID="pEncBusqueda"
                        ExpandedImage="~/Images/collapse.jpg" ExpandedText="(Click Aqui para Ocultar Detalles...)"
                        ImageControlID="lblMostrarDetalles" SkinID="CollapsiblePanelDemo" SuppressPostBack="true"
                        TargetControlID="pConsultaAfiliacion" TextLabelID="imgExpand" />
                </asp:Panel>
                <asp:Panel ID="panelfijo" runat="server" Height="60px" Width="100%">
                    <div style="border-style: none; border-width: medium; background-color: #f5f5f5">
                        <table>
                            <tr>
                                <td>
                                    <asp:Button ID="btnDatosCliente" runat="server" CssClass="btn8"
                                        OnClick="btnDatosCliente_Click" Text="Información de la persona"
                                        Width="158px" />
                                </td>
                                <td style="margin-left: 40px">
                                    <asp:Button ID="btnPreAnalisis" runat="server" CssClass="btn8"
                                        OnClick="btnPreAnalisis_Click" Text="Pre Analisis de crédito" Width="140px" />
                                </td>
                                <td>
                                    <asp:Button ID="btnComentarios" runat="server" CssClass="btn8"
                                        OnClick="btnComentarios_Click" Text="Comentarios " Width="126px" />
                                </td>
                                <td style="margin-left: 80px">
                                    <asp:Button ID="btnDatosNegocio" runat="server" CssClass="btn8"
                                        OnClick="btnDatosNegocio_Click" Text="Datos del Negocio" Width="126px" />
                                </td>
                                <td style="margin-left: 80px">
                                    <asp:Button ID="BtnCobranzas" runat="server" CssClass="btn8"
                                        OnClick="BtnCobranzas_Click" Text="Cobranzas" Width="126px" />
                                </td>
                                <td>
                                    <asp:Button ID="Btnagenda" runat="server" CssClass="btn8" OnClick="Btnagenda_Click"
                                        Text="Agenda" Width="126px" />
                                </td>
                                <td>
                                    <asp:Button ID="btnVerComprobantes" runat="server" CssClass="btn8"
                                        Text="Consulta de Comprobantes" OnClick="btnVerComprobantes_Click" />
                                </td>
                                <td>
                                    <asp:UpdatePanel runat="server">
                                        <ContentTemplate>
                                            <asp:Button ID="btnInfo" runat="server" CssClass="btn8"
                                                Text="Movimiento Afiliación" OnClientClick="return false" />
                                            <asp:BalloonPopupExtender ID="btnInfo_BalloonPopupExtender"
                                                runat="server" DynamicServicePath="" Enabled="True" ExtenderControlID=""
                                                TargetControlID="btnInfo" BalloonPopupControlID="panelMovAfiliacion"
                                                Position="BottomLeft" BalloonSize="Large" UseShadow="true"
                                                ClientIDMode="Static" ScrollBars="Auto" DisplayOnMouseOver="false"
                                                DisplayOnFocus="false" DisplayOnClick="true">
                                            </asp:BalloonPopupExtender>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Button ID="btnConsolidado" runat="server" CssClass="btn8"
                                        Text="Consolidado de Productos" OnClick="btnConsolidado_Click" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </asp:Panel>
            </td>
        </tr>
    </table>
    <asp:Panel ID="pTotales" runat="server" Height="89px" Width="966px">
        <asp:TabContainer ID="Tabs" runat="server" ActiveTabIndex="2" CssClass="CustomTabStyle"
            OnClientActiveTabChanged="ActiveTabChanged" Style="margin-right: 30px" Width="1000px">
            <asp:TabPanel ID="tabAportes" runat="server" HeaderText="Datos">
                <HeaderTemplate>
                    Aportes
                </HeaderTemplate>
                <ContentTemplate>
                    <table width="100%">
                        <tr>
                            <td style="width: 30%; text-align: left;">
                                <asp:CheckBox ID="chkaporterminados" runat="server" Text="Mostrar Aportes Terminados"
                                    AutoPostBack="True" OnCheckedChanged="chkaporterminados_CheckedChanged" />
                            </td>
                        </tr>
                    </table>
                    <asp:GridView ID="gvListaAporte" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                        DataKeyNames="numero_aporte" PageSize="20" ShowHeaderWhenEmpty="True"
                        Width="100%" OnSelectedIndexChanged="gvListaAporte_SelectedIndexChanged"
                        OnPageIndexChanging="gvListaAporte_PageIndexChanging"
                        OnRowDataBound="gvListaAporte_RowDataBound">
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:ImageButton ID="btnInfo" runat="server" CommandName="Select"
                                        ImageUrl="~/Images/gr_general.jpg" ToolTip="Detalle" Width="16px" />
                                </ItemTemplate>
                                <HeaderStyle CssClass="gridIco" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="numero_aporte" HeaderText="Num. Aporte">
                                <ItemStyle HorizontalAlign="Left" Width="50px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="cod_linea_aporte" HeaderText="Linea">
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="nom_linea_aporte" HeaderText="Nombre Linea">
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="nom_oficina" HeaderText="Oficina">
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="fecha_apertura" DataFormatString="{0:d}"
                                HeaderText="Fec. Apertura">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="saldo" HeaderText="Saldo Total" DataFormatString="{0:N0}" />
                            <asp:BoundField DataField="cuota" HeaderText="Valor Cuota" DataFormatString="{0:N0}" />
                            <asp:BoundField DataField="valor_a_pagar" HeaderText="Valor a Pagar"
                                DataFormatString="{0:N0}" />
                            <asp:BoundField DataField="fecha_proximo_pago" DataFormatString="{0:d}"
                                HeaderText="F. Próx. Pago" />
                            <asp:BoundField DataField="estado_Linea" HeaderText="Estado" />
                            <asp:BoundField DataField="nom_forma_pago" HeaderText="Forma Pago" />
                            <asp:BoundField DataField="nom_empresa" HeaderText="Empresa" />
                            <asp:BoundField DataField="valor_acumulado" HeaderText="Rendimientos Causados"
                                DataFormatString="{0:N0}" />
                            <asp:BoundField HeaderText="Valor + Rendim.Causados" DataField="valor_total_acumu"
                                DataFormatString="{0:N0}">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="DiasMora" HeaderText="Dias Mora" />
                        </Columns>
                        <HeaderStyle CssClass="gridHeader" Font-Size="X-Small" />
                        <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                        <RowStyle CssClass="gridItem" Font-Size="X-Small" />
                    </asp:GridView>
                    <asp:Label ID="lblTotalRegsAporte" runat="server" Visible="False" />
                    <asp:Label ID="lblInfoAporte" runat="server" Text="Su consulta no obtuvo ningún resultado."
                        Visible="False" />
                    <table>
                        <tr>
                            <td class="tdI">
                                <asp:Label ID="lbltotalAportes" runat="server" Text="Total Saldos" />
                                <br />
                                <asp:TextBox ID="txtTotalAportes" runat="server" CssClass="textbox" Enabled="False"
                                    DataFormatString="{0:N0}" Style="text-align: center" Width="140px"></asp:TextBox>
                                <asp:MaskedEditExtender ID="MaskedEditExtender4" runat="server"
                                    CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                    CultureDatePlaceholder="" CultureDecimalPlaceholder=""
                                    CultureThousandsPlaceholder="" CultureTimePlaceholder="" DisplayMoney="Right"
                                    Enabled="True" Mask="999,999,999" MaskType="Number"
                                    TargetControlID="txtAportesTotal"></asp:MaskedEditExtender>
                                <asp:TextBox ID="txtAportesTotal" runat="server" CssClass="textbox" Visible="False"
                                    Width="16px"></asp:TextBox>
                            </td>
                            <td class="tdI">
                                <asp:Label ID="lblTotalCuotasAportes" runat="server" Text="Total Cuotas Aportes" />
                                <br />
                                <asp:TextBox ID="txtTotalCuotasAportes" runat="server" CssClass="textbox"
                                    Enabled="False" DataFormatString="{0:N0}" Style="text-align: center" Width="140px">
                                </asp:TextBox>
                                <asp:MaskedEditExtender ID="MaskedEditExtender5" runat="server"
                                    CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                    CultureDatePlaceholder="" CultureDecimalPlaceholder=""
                                    CultureThousandsPlaceholder="" CultureTimePlaceholder="" DisplayMoney="Right"
                                    Enabled="True" Mask="999,999,999" MaskType="Number"
                                    TargetControlID="txtAportesCuotas"></asp:MaskedEditExtender>
                                <asp:TextBox ID="txtAportesCuotas" runat="server" CssClass="textbox" Visible="False"
                                    Width="16px"></asp:TextBox>
                            </td>
                            <td class="tdD">
                                <asp:Label ID="lblAportesPendientesPorPagar" runat="server"
                                    Text="Vlr Pendiente a Pagar" />
                                <br />
                                <asp:TextBox ID="txtAPortesPendientesporPagar" runat="server" CssClass="textbox"
                                    Enabled="False" DataFormatString="{0:N0}" Style="text-align: center" Width="140px">
                                </asp:TextBox>
                                <asp:MaskedEditExtender ID="MaskedEditExtender6" runat="server"
                                    CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                    CultureDatePlaceholder="" CultureDecimalPlaceholder=""
                                    CultureThousandsPlaceholder="" CultureTimePlaceholder="" DisplayMoney="Right"
                                    Enabled="True" Mask="999,999,999" MaskType="Number"
                                    TargetControlID="txtAportesPendientes"></asp:MaskedEditExtender>
                                <asp:TextBox ID="txtAportesPendientes" runat="server" CssClass="textbox" Visible="False"
                                    Width="16px"></asp:TextBox>
                            </td>
                            <td class="tdI">
                                <asp:Label ID="lblTotalCausacionAporte" runat="server" Text="Total Rendim. Causados" />
                                <br />
                                <asp:TextBox ID="txtTotalCausacionAportes" runat="server" CssClass="textbox"
                                    Enabled="False" DataFormatString="{0:N0}" Style="text-align: center" Width="140px">
                                </asp:TextBox>
                                <asp:MaskedEditExtender ID="MaskedEditExtender20" runat="server"
                                    CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                    CultureDatePlaceholder="" CultureDecimalPlaceholder=""
                                    CultureThousandsPlaceholder="" CultureTimePlaceholder="" DisplayMoney="Right"
                                    Enabled="True" Mask="999,999,999" MaskType="Number"
                                    TargetControlID="txtAportesCausacion"></asp:MaskedEditExtender>
                                <asp:TextBox ID="txtAportesCausacion" runat="server" CssClass="textbox" Visible="False"
                                    Width="16px"></asp:TextBox>
                            </td>
                            <td class="tdI">
                                <asp:Label ID="lblTotalCaumassaldoAportes" runat="server"
                                    Text="Total Saldo+ Rend. Causados"></asp:Label>
                                <br />
                                <asp:TextBox ID="txtTotalCausacionmasAportes" runat="server" CssClass="textbox"
                                    DataFormatString="{0:N0}" Enabled="False" Style="text-align: center" Width="140px">
                                </asp:TextBox>
                                <asp:MaskedEditExtender ID="MaskedEditExtender24" runat="server"
                                    CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                    CultureDatePlaceholder="" CultureDecimalPlaceholder=""
                                    CultureThousandsPlaceholder="" CultureTimePlaceholder="" DisplayMoney="Right"
                                    Enabled="True" Mask="999,999,999" MaskType="Number"
                                    TargetControlID="txtTotalCausacionmasAportes"></asp:MaskedEditExtender>
                                <asp:TextBox ID="txtAportesCausacionmassaldo" runat="server" CssClass="textbox"
                                    Visible="False" Width="16px"></asp:TextBox>
                            </td>
                            <td class="tdI">
                                <asp:Label ID="lblClasificaion" runat="server" Text="Clasificación"></asp:Label>
                                <br />
                                <asp:TextBox ID="txtClasificacion" runat="server" CssClass="textbox" Enabled="False"
                                    Style="text-align: center" Width="140px"></asp:TextBox>
                            </td>
                        </tr>
                    </table>

                    <asp:Panel ID="pnlPendientes" runat="server">
                        <asp:Panel ID="panelApoPendiente" runat="server" CssClass="collapsePanelHeader" Height="30px">
                            <div style="padding: 5px; cursor: pointer; vertical-align: middle;">
                                <div style="float: left; color: #0066FF; font-size: small">
                                    Aportes Pendientes
                                </div>
                                <div style="float: left; margin-left: 5px; color: #0066FF; font-size: small">
                                    <asp:Label ID="lblApoPendiente" runat="server">(Mostrar Detalles...)</asp:Label>
                                    <asp:ImageButton ID="imgApoPendientes" runat="server"
                                        AlternateText="(Show Details...)" ImageUrl="~/Images/expand.jpg" />
                                </div>
                                <br />
                            </div>
                        </asp:Panel>

                        <asp:Panel ID="paListaApoPendiente" runat="server" Width="989px">
                            <div style="border-style: none; border-width: medium; background-color: #f5f5f5">
                                <table style="width: 100%">
                                    <tr>
                                        <td style="width: 100%">

                                            <asp:GridView ID="gvApoPendiente" runat="server" Width="100%" PageSize="20"
                                                ShowHeaderWhenEmpty="True" AutoGenerateColumns="False"
                                                Style="font-size: small; margin-bottom: 0px;" DataKeyNames="cod_ope">
                                                <Columns>
                                                    <asp:BoundField DataField="iddetalle" HeaderText="Id." />
                                                    <asp:BoundField DataField="cod_cliente" HeaderText="Cod.Cli." />
                                                    <asp:BoundField DataField="identificacion" HeaderText="Cedula/Nit">
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="nombre" HeaderText="Nombres">
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="tipo_producto"
                                                        HeaderText="Tipo de Producto" />
                                                    <asp:BoundField DataField="numero_producto"
                                                        HeaderText="Número de Producto" />
                                                    <asp:TemplateField HeaderText="Tipo Aplicacion">
                                                        <ItemTemplate>
                                                            <cc1:DropDownListGrid ID="ddlTipoAplicacion" runat="server"
                                                                Style="font-size: xx-small; text-align: center"
                                                                Width="100px" CssClass="dropdown"
                                                                SelectedValue='<%# Bind("tipo_aplicacion") %>'
                                                                CommandArgument='<%#Container.DataItemIndex %>'>
                                                                <asp:ListItem Value="Por Valor">Por Valor</asp:ListItem>
                                                                <asp:ListItem Value="Pago Total">Pago Total
                                                                </asp:ListItem>
                                                                <asp:ListItem Value="Por Valor a Capital">
                                                                </asp:ListItem>
                                                            </cc1:DropDownListGrid>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="num_cuotas" HeaderText="Num.Cuotas" />
                                                    <asp:BoundField DataField="valor" HeaderText="Valor a Aplicar"
                                                        DataFormatString="{0:N}">
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="tipo_tran"
                                                        HeaderText="Tipo de Transacción">
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="cod_ope" HeaderText="Cod Operación"
                                                        Visible="False">
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:BoundField>
                                                </Columns>
                                                <HeaderStyle CssClass="gridHeader" />
                                                <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                                                <RowStyle CssClass="gridItem" />
                                                <SelectedRowStyle Font-Size="XX-Small"></SelectedRowStyle>
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </asp:Panel>
                        <asp:CollapsiblePanelExtender ID="cpPendiente" runat="server"
                            CollapseControlID="panelApoPendiente" Collapsed="True" CollapsedImage="~/Images/expand.jpg"
                            CollapsedText="(Click Aqui para Mostrar Detalles...)" ExpandControlID="panelApoPendiente"
                            ExpandedImage="~/Images/collapse.jpg" ExpandedText="(Click Aqui para Ocultar Detalles...)"
                            ImageControlID="imgApoPendientes" SkinID="CollapsiblePanelDemo" SuppressPostBack="True"
                            TargetControlID="paListaApoPendiente" TextLabelID="lblApoPendiente" Enabled="True" />
                        <br />
                    </asp:Panel>
                </ContentTemplate>
            </asp:TabPanel>

            <asp:TabPanel ID="tabCreditos" runat="server" HeaderText="Datos" ScrollBars="Both">
                <HeaderTemplate>
                    Creditos
                </HeaderTemplate>
                <ContentTemplate>
                    <table width="100%" style="position: relative; z-index: 0">
                        <tr style="">
                            <td style="text-align: left" colspan="3">
                                <asp:CheckBox ID="chkcredterminados" runat="server" Text="Mostrar Créditos Terminados"
                                    AutoPostBack="True" OnCheckedChanged="chkcredterminados_CheckedChanged" />
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:CheckBox ID="chkCredProceso" runat="server" Text="Mostrar Créditos en Proceso"
                                    AutoPostBack="True" OnCheckedChanged="chkCredProceso_CheckedChanged"
                                    Checked="True" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:CheckBox ID="chkCredAnulados" runat="server" Text="Mostrar Créditos Anulados"
                                    AutoPostBack="True" OnCheckedChanged="chkCredAnulados_CheckedChanged" />
                            </td>
                            <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <table style="height: 200px; vertical-align: top" width="100%">
                                    <tr>
                                        <td>


                                            <div id="divCreditos" runat="server"
                                                style="overflow: scroll; width: 62%; height: 100%;">
                                                <asp:GridView ID="gvLista" runat="server" AutoGenerateColumns="False"
                                                    Height="90%" OnPageIndexChanging="gvLista_PageIndexChanging"
                                                    OnRowCommand="gvLista_RowCommand"
                                                    OnRowDataBound="gvLista_RowDataBound"
                                                    OnSelectedIndexChanged="gvLista_SelectedIndexChanged"
                                                    Style="text-align: left;" Width="180% ">
                                                    <Columns>
                                                        <asp:BoundField DataField="IdPersona">
                                                            <HeaderStyle CssClass="gridColNo" />
                                                            <ItemStyle CssClass="gridColNo" />
                                                        </asp:BoundField>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="btnDetalleCredito" runat="server"
                                                                    CommandArgument='<%#Eval("data")%>'
                                                                    CommandName="DetalleCredito"
                                                                    ImageUrl="~/Images/gr_detall.jpg"
                                                                    ToolTip="Detalle Credito" />
                                                            </ItemTemplate>
                                                            <HeaderStyle CssClass="gridIco" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="btnMovGenCredito" runat="server"
                                                                    CommandArgument='<%#Eval("data") %>'
                                                                    CommandName="MovGralCredito"
                                                                    ImageUrl="~/Images/gr_general.jpg"
                                                                    ToolTip="Movimiento General Credito" />
                                                            </ItemTemplate>
                                                            <HeaderStyle CssClass="gridIco" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="DetallePago" runat="server"
                                                                    CommandArgument='<%#Eval("data")%>'
                                                                    CommandName="DetallePago"
                                                                    ImageUrl="~/Images/gr_credit.jpg"
                                                                    ToolTip="DetallePago" />
                                                            </ItemTemplate>
                                                            <HeaderStyle CssClass="gridIco" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Número Radicación">
                                                            <ItemTemplate>
                                                                <div style="position: relative">
                                                                    <asp:HyperLink ID="hplink" runat="server"
                                                                        Text='<%# Eval("NumRadicion")%>'
                                                                        Target="_blank">
                                                                    </asp:HyperLink>
                                                                    <div runat="server" class="notBtn" href="#" visible="False" id="BellNotification">
                                                                        <i class="fa fa-bell" style="font-size: 16px; -webkit-text-fill-color: dodgerblue; -webkit-text-stroke-width: 1px; -webkit-text-stroke-color: black;"></i>
                                                                        <div class="box" runat="server" id="DivGlobal">
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </ItemTemplate>
                                                            <HeaderStyle Width="21pc" />
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="Proceso" HeaderText="Proceso">
                                                            <HeaderStyle Width="80px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Estado" HeaderText="Estado">
                                                            <HeaderStyle Width="60px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Pagare" HeaderText="Pagaré">
                                                            <HeaderStyle Width="50px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Linea" HeaderText="Línea">
                                                            <HeaderStyle Width="80px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="FechaSolicitud" DataFormatString="{0:d}"
                                                            HeaderText="Fecha Aprobación">
                                                            <HeaderStyle Width="50px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="FechaDesembolso" DataFormatString="{0:d}"
                                                            HeaderText="Fecha Desembolso">
                                                            <HeaderStyle Width="50px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="FechaProximoPago" DataFormatString="{0:d}"
                                                            HeaderText="Fecha Próx. Pago">
                                                            <HeaderStyle Width="50px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="garantiacomunitaria" HeaderText="G.Comunitaria">
                                                            <HeaderStyle Width="40px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="saldocapital" HeaderText="Saldo Capital">
                                                            <HeaderStyle Width="70px" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="MontoAprobado" HeaderText="Monto Aprobado">
                                                            <HeaderStyle Width="60px" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="ValorAPagar" HeaderText="Vlr A Pagar">
                                                            <HeaderStyle Width="80px" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="ValorAPagarCE" HeaderText="Vlr.Cuotas Extras">
                                                            <HeaderStyle Width="80px" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="ValorTotalAPagar" HeaderText="Vlr Total A Pagar">
                                                            <HeaderStyle Width="80px" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Atributos" HeaderText="Atributos">
                                                            <HeaderStyle Width="80px" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="FechaVencimiento" DataFormatString="{0:d}"
                                                            HeaderText="Fecha Terminación">
                                                            <HeaderStyle Width="50px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="FechaReestructurado"
                                                            HeaderText="Reestructuracion">
                                                            <HeaderStyle HorizontalAlign="Left" Width="50" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Plazo" HeaderText="Plazo">
                                                            <HeaderStyle Width="40px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Cuota" HeaderText="Cuota">
                                                            <ItemStyle HorizontalAlign="Right" Width="60px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="CuotasPagadas" HeaderText="Cta Pagadas">
                                                            <HeaderStyle Width="40px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="tasainteres" HeaderText="Tasa">
                                                            <HeaderStyle Width="40px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Disponible" HeaderText="Cupo Disponible">
                                                            <HeaderStyle Width="60px" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="NombreOficina" HeaderText="Oficina">
                                                            <HeaderStyle Width="80px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="observaciones" HeaderText="Observaciones">
                                                            <HeaderStyle HorizontalAlign="Left" Width="215px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="vr_ult_dscto" DataFormatString="{0:N}"
                                                            HeaderText="Vr Desc Nonima">
                                                            <HeaderStyle HorizontalAlign="Left" Width="80px" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="pagadurias" HeaderText="Pagaduria">
                                                            <HeaderStyle HorizontalAlign="Left" Width="80px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="TipoLinea" HeaderText="Tipo Linea">
                                                            <HeaderStyle HorizontalAlign="Left" Width="80px" />
                                                        </asp:BoundField>
                                                    </Columns>
                                                    <HeaderStyle CssClass="gridHeader" Font-Size="X-Small" />
                                                    <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                                                    <RowStyle CssClass="gridItem" Font-Size="X-Small" />
                                                </asp:GridView>
                                            </div>

                                        </td>
                                        <td>&nbsp;&nbsp; </td>
                                    </tr>
                                </table>
                                <asp:Panel ID="paTotales" runat="server" CssClass="collapsePanelHeader" Height="70px">
                                    <table align="center" cellpadding="1" cellspacing="0" width="100%">
                                        <tr>
                                            <td class="tdI" style="border-width: 1px">
                                                <asp:Label ID="Label1" runat="server" Text="0" Visible="False">
                                                </asp:Label>
                                                <asp:Label ID="Label2" runat="server" Text="0" Visible="False">
                                                </asp:Label>
                                                <asp:Label ID="Label3" runat="server" Text="0" Visible="False">
                                                </asp:Label>
                                                <asp:Label ID="Label4" runat="server" Text="0" Visible="False">
                                                </asp:Label>
                                                <asp:Label ID="Label5" runat="server" Text="0" Visible="False">
                                                </asp:Label>
                                                <asp:Label ID="lblTotalRegsCodeudores0" runat="server"
                                                    Text="Su consulta no obtuvo ningun resultado." Visible="False">
                                                </asp:Label>
                                                <asp:Label ID="lblTotalRegs" runat="server" Visible="False"></asp:Label>
                                                <asp:Label ID="lblInfo" runat="server"
                                                    Text="Su consulta no obtuvo ningun resultado." Visible="False">
                                                </asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="tdI">
                                                <asp:Label ID="lblTotalSaldos" runat="server" Text="Total Saldos">
                                                </asp:Label>
                                                <br />
                                                <asp:TextBox ID="txtTotalSaldos" runat="server" CssClass="textbox"
                                                    Enabled="False" Style="text-align: center" Width="140px">
                                                </asp:TextBox>
                                                <asp:MaskedEditExtender ID="MaskedEditExtender2" runat="server"
                                                    CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder=""
                                                    CultureDateFormat="" CultureDatePlaceholder=""
                                                    CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                    CultureTimePlaceholder="" DisplayMoney="Right" Enabled="True"
                                                    Mask="999,999,999" MaskType="Number"
                                                    TargetControlID="txtTotalSaldos"></asp:MaskedEditExtender>
                                                <asp:TextBox ID="txtTotalSaldosrp" runat="server" CssClass="textbox"
                                                    Visible="False" Width="16px"></asp:TextBox>
                                            </td>
                                            <td class="tdI">
                                                <asp:Label ID="lblTotalCuotas" runat="server"
                                                    Text="Total Cuotas Créditos"></asp:Label>
                                                <br />
                                                <asp:TextBox ID="txtTotalCoutasCreditos" runat="server"
                                                    CssClass="textbox" Enabled="False" Style="text-align: center"
                                                    Width="140px"></asp:TextBox>
                                                <asp:MaskedEditExtender ID="MaskedEditExtender3" runat="server"
                                                    CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder=""
                                                    CultureDateFormat="" CultureDatePlaceholder=""
                                                    CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                    CultureTimePlaceholder="" DisplayMoney="Right" Enabled="True"
                                                    Mask="999,999,999" MaskType="Number"
                                                    TargetControlID="txtTotalCoutasCreditos"></asp:MaskedEditExtender>
                                                <asp:TextBox ID="txtTotalCoutasCredrp" runat="server" CssClass="textbox"
                                                    Visible="False" Width="16px"></asp:TextBox>
                                            </td>
                                            <td class="tdD">
                                                <asp:Label ID="lblTotalPendientes" runat="server"
                                                    Text="Vlr Pendiente a Pagar"></asp:Label>
                                                <br />
                                                <asp:TextBox ID="txtVlrPendienteApagar" runat="server"
                                                    CssClass="textbox" Enabled="False" Style="text-align: center"
                                                    Width="140px"></asp:TextBox>
                                                <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server"
                                                    CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder=""
                                                    CultureDateFormat="" CultureDatePlaceholder=""
                                                    CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                    CultureTimePlaceholder="" DisplayMoney="Right" Enabled="True"
                                                    Mask="999,999,999" MaskType="Number"
                                                    TargetControlID="txtVlrPendienteApagar"></asp:MaskedEditExtender>
                                                <asp:TextBox ID="txtVlrPendienteAparep" runat="server"
                                                    CssClass="textbox" Visible="False" Width="16px"></asp:TextBox>
                                            </td>
                                            <td class="tdD">
                                                <asp:Label ID="lblTotalPendientesCE" runat="server"
                                                    Text="Vlr Pendiente a Pagar Cuotas Extras"></asp:Label>
                                                <br />
                                                <asp:TextBox ID="txtVlrPendienteApagarCE" runat="server"
                                                    CssClass="textbox" Enabled="False" Style="text-align: center"
                                                    Width="140px"></asp:TextBox>
                                                <asp:MaskedEditExtender ID="MaskedEditExtender25" runat="server"
                                                    CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder=""
                                                    CultureDateFormat="" CultureDatePlaceholder=""
                                                    CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                    CultureTimePlaceholder="" DisplayMoney="Right" Enabled="True"
                                                    Mask="999,999,999" MaskType="Number"
                                                    TargetControlID="txtVlrPendienteApagarCE"></asp:MaskedEditExtender>
                                                <asp:TextBox ID="TextBox2" runat="server" CssClass="textbox"
                                                    Visible="False" Width="16px"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <asp:Panel ID="pacodeudado" runat="server" CssClass="collapsePanelHeader" Height="30px">
                                    <div style="padding: 5px; cursor: pointer; vertical-align: middle;">
                                        <div style="float: left; color: #0066FF; font-size: small">
                                            Creditos Acodeudados
                                        </div>
                                        <div style="float: left; margin-left: 5px; color: #0066FF; font-size: small">
                                            <asp:Label ID="lblCreditosAcodeudados" runat="server">(Mostrar Detalles...)
                                            </asp:Label>
                                            <asp:ImageButton ID="imgCreditosAcodeudados" runat="server"
                                                AlternateText="(Show Details...)" ImageUrl="~/Images/expand.jpg" />
                                        </div>
                                        <br />
                                    </div>
                                </asp:Panel>
                                <asp:Panel ID="paListaCreditosAcodeudados" runat="server" Width="989px">
                                    <div style="border-style: none; border-width: medium; background-color: #f5f5f5">
                                        <table>
                                            <tr>
                                                <asp:GridView ID="gvAcodeudados" runat="server" AllowPaging="True"
                                                    AutoGenerateColumns="False" GridLines="Horizontal"
                                                    OnPageIndexChanging="gvAcodeudados_PageIndexChanging" PageSize="20"
                                                    ShowHeaderWhenEmpty="True" Width="100%">
                                                    <Columns>
                                                        <asp:BoundField DataField="identificacion" HeaderText="Cedula">
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="NumRadicacion"
                                                            HeaderText="Número Crédito">
                                                            <ItemStyle HorizontalAlign="Center" />

                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Nombres" HeaderText="Nombre Deudor">
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Monto" DataFormatString="{0:c}"
                                                            HeaderText="Monto">
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Saldo" DataFormatString="{0:c}"
                                                            HeaderText="Saldo">
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Cuota" DataFormatString="{0:c}"
                                                            HeaderText="Cuota">
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="FechaProxPago"
                                                            HeaderText="Fecha Próx Pago">
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Valor_apagar"
                                                            HeaderText="Valor en Mora">
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Estado_Codeudor" HeaderText="Estado">
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                    </Columns>
                                                    <HeaderStyle CssClass="gridHeader" />
                                                    <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                                                    <RowStyle CssClass="gridItem" Font-Size="X-Small" />
                                                </asp:GridView>
                                                <asp:Label ID="lblInfoss" runat="server" Visible="False"></asp:Label>
                                                <asp:Label ID="lblTotalRegss" runat="server"
                                                    Text="Su consulta no obtuvo ningún resultado." Visible="False">
                                                </asp:Label>
                                            </tr>
                                        </table>
                                    </div>
                                </asp:Panel>
                                <asp:CollapsiblePanelExtender ID="cpacodeudado" runat="server"
                                    CollapseControlID="pacodeudado" Collapsed="True"
                                    CollapsedImage="~/Images/expand.jpg"
                                    CollapsedText="(Click Aqui para Mostrar Detalles...)" Enabled="True"
                                    ExpandControlID="pacodeudado" ExpandedImage="~/Images/collapse.jpg"
                                    ExpandedText="(Click Aqui para Ocultar Detalles...)"
                                    ImageControlID="lblCreditosAcodeudados" SkinID="CollapsiblePanelDemo"
                                    SuppressPostBack="True" TargetControlID="paListaCreditosAcodeudados"
                                    TextLabelID="imgCreditosAcodeudados"></asp:CollapsiblePanelExtender>
                                <br />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:TabPanel>
            <asp:TabPanel ID="tabAhorros" runat="server" HeaderText="Datos">
                <HeaderTemplate>
                    Ahorros Vista
                </HeaderTemplate>
                <ContentTemplate>
                    <table width="100%">
                        <tr>
                            <td style="text-align: left">
                                <div id="div2" runat="server" style="overflow: scroll; width: 100%;">
                                    <asp:CheckBox ID="chkAhorro" runat="server" Text="Mostrar cuentas cerradas"
                                        AutoPostBack="True" OnCheckedChanged="chkAhorro_CheckedChanged" />
                                    <br />
                                    <asp:GridView ID="gvAhorros" runat="server" AllowPaging="True"
                                        AutoGenerateColumns="False" DataKeyNames="numero_cuenta" GridLines="Horizontal"
                                        PageSize="20" ShowHeaderWhenEmpty="True" Width="89%"
                                        OnSelectedIndexChanged="gvAhorros_SelectedIndexChanged"
                                        OnPageIndexChanging="gvAhorros_PageIndexChanging"
                                        OnRowCommand="gvAhorros_RowCommand">
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="btnInfo" runat="server" CommandName="Select"
                                                        ImageUrl="~/Images/gr_general.jpg" ToolTip="Detalle"
                                                        Width="16px" />
                                                </ItemTemplate>
                                                <HeaderStyle CssClass="gridIco" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="numero_cuenta" HeaderText="Num. Cuenta">
                                                <ItemStyle HorizontalAlign="Left" Width="50px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="nom_linea" HeaderText="Nom. Linea">
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="nom_oficina" HeaderText="Oficina">
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="fecha_apertura" DataFormatString="{0:d}"
                                                HeaderText="Fec. Apertura">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="saldo_total" HeaderText="Saldo Total"
                                                DataFormatString="{0:N0}">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="saldo_canje" HeaderText="Saldo en Canje"
                                                DataFormatString="{0:N0}">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="nom_estado" HeaderText="Estado">
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="nom_formapago" HeaderText="Forma Pago">
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="valor_cuota" HeaderText="Valor Cuota"
                                                DataFormatString="{0:N0}">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>

                                            <asp:BoundField DataField="fecha_proximo_pago" DataFormatString="{0:d}"
                                                HeaderText="F. Próx. Pago">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="valor_pagar" HeaderText="Valor Pagar"
                                                DataFormatString="{0:N0}">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="valor_acumulado"
                                                HeaderText="Rendimientos Causados" DataFormatString="{0:N0}">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Valor + Rendim.Causados"
                                                DataField="valor_total_acumu" DataFormatString="{0:N0}">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="Tarjeta de Firmas">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="btnFirmas" runat="server" CommandName="Firmas"
                                                        ImageUrl="~/Images/Lupa.jpg" ToolTip="Tarjeta Firmas"
                                                        Width="16px" CommandArgument='<%#Eval("numero_cuenta")%>' />
                                                    <%--<button onclick=" DisplayFullImage(imgHuella1) " style="background-color:transparent; border-color:transparent;" value="" > 
                                                   <img src="../../../Images/Lupa.jpg"/>
                                              </button> --%>
                                                    <%--<image id="imgHuella1" style="visibility:hidden; width:1px; height:1px;" src='<%# "HandlerFirma.ashx?id=" + Eval("numero_cuenta") + "&Us=" + ((Usuario)Session["Usuario"]).identificacion + "&Pw=" + System.Web.HttpUtility.UrlEncode(((Usuario)Session["Usuario"]).clave_sinencriptar) %>'>
                                                    </image>--%>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <HeaderStyle CssClass="gridHeader" Font-Size="X-Small" />
                                        <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                                        <RowStyle CssClass="gridItem" Font-Size="X-Small" />
                                    </asp:GridView>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: center">
                                <asp:Label ID="lblTotalRegsAhorro" runat="server" Visible="False" />
                                <asp:Label ID="lblInfoAhorro" runat="server"
                                    Text="Su consulta no obtuvo ningún resultado." Visible="False" />
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                        </tr>
                        <tr>
                            <td></td>
                        </tr>
                        <table>
                            <tr>
                                <td class="tdI">
                                    <asp:Label ID="lbltotalAhorros" runat="server" Text="Total Saldos"></asp:Label>
                                    <br />
                                    <asp:TextBox ID="txtTotalAhorros" runat="server" CssClass="textbox"
                                        DataFormatString="{0:N0}" Enabled="False" Style="text-align: center"
                                        Width="140px"></asp:TextBox>
                                    <asp:MaskedEditExtender ID="MaskedEditExtender8" runat="server"
                                        CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder=""
                                        CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder=""
                                        CultureThousandsPlaceholder="" CultureTimePlaceholder="" DisplayMoney="Right"
                                        Enabled="True" Mask="999,999,999" MaskType="Number"
                                        TargetControlID="txtAhorrosTotal"></asp:MaskedEditExtender>
                                    <asp:TextBox ID="txtAhorrosTotal" runat="server" CssClass="textbox" Visible="False"
                                        Width="16px"></asp:TextBox>
                                </td>
                                <td class="tdI">
                                    <asp:Label ID="lblTotalCuotasAhorros" runat="server" Text="Total Cuotas Ahorros">
                                    </asp:Label>
                                    <br />
                                    <asp:TextBox ID="txtTotalCuotasAhorros" runat="server" CssClass="textbox"
                                        DataFormatString="{0:N0}" Enabled="False" Style="text-align: center"
                                        Width="140px"></asp:TextBox>
                                    <asp:MaskedEditExtender ID="MaskedEditExtender9" runat="server"
                                        CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder=""
                                        CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder=""
                                        CultureThousandsPlaceholder="" CultureTimePlaceholder="" DisplayMoney="Right"
                                        Enabled="True" Mask="999,999,999" MaskType="Number"
                                        TargetControlID="txtAhorrosCuotas"></asp:MaskedEditExtender>
                                    <asp:TextBox ID="txtAhorrosCuotas" runat="server" CssClass="textbox" Visible="False"
                                        Width="16px"></asp:TextBox>
                                </td>
                                <td class="tdI">
                                    <asp:Label ID="lblTotalCausacionAhorros" runat="server"
                                        Text="Total Rendim. Causados"></asp:Label>
                                    <br />
                                    <asp:TextBox ID="txtTotalCausacionAhorros" runat="server" CssClass="textbox"
                                        DataFormatString="{0:N0}" Enabled="False" Style="text-align: center"
                                        Width="140px"></asp:TextBox>
                                    <asp:MaskedEditExtender ID="MaskedEditExtender16" runat="server"
                                        CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder=""
                                        CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder=""
                                        CultureThousandsPlaceholder="" CultureTimePlaceholder="" DisplayMoney="Right"
                                        Enabled="True" Mask="999,999,999" MaskType="Number"
                                        TargetControlID="txtAhorrosCausacion"></asp:MaskedEditExtender>
                                    <asp:TextBox ID="txtAhorrosCausacion" runat="server" CssClass="textbox"
                                        Visible="False" Width="16px"></asp:TextBox>
                                </td>
                                <td class="tdI">
                                    <asp:Label ID="lblTotalCanjeAhorros" runat="server" Text="Total Canje Ahorros">
                                    </asp:Label>
                                    <br />
                                    <asp:TextBox ID="txtTotalCanjeAhorros" runat="server" CssClass="textbox"
                                        DataFormatString="{0:N0}" Enabled="False" Style="text-align: center"
                                        Width="140px"></asp:TextBox>
                                    <asp:MaskedEditExtender ID="MaskedEditExtender19" runat="server"
                                        CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder=""
                                        CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder=""
                                        CultureThousandsPlaceholder="" CultureTimePlaceholder="" DisplayMoney="Right"
                                        Enabled="True" Mask="999,999,999" MaskType="Number"
                                        TargetControlID="txtAhorrosCanje"></asp:MaskedEditExtender>
                                    <asp:TextBox ID="txtAhorrosCanje" runat="server" CssClass="textbox" Visible="False"
                                        Width="16px"></asp:TextBox>
                                </td>
                                <td class="tdI">
                                    <asp:Label ID="lblTotalCausacionmassaldo" runat="server"
                                        Text="Total Saldo+ Rend. Causados"></asp:Label>
                                    <br />
                                    <asp:TextBox ID="txtTotalCausacionmassaldo" runat="server" CssClass="textbox"
                                        DataFormatString="{0:N0}" Enabled="False" Style="text-align: center"
                                        Width="140px"></asp:TextBox>
                                    <asp:MaskedEditExtender ID="MaskedEditExtender21" runat="server"
                                        CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder=""
                                        CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder=""
                                        CultureThousandsPlaceholder="" CultureTimePlaceholder="" DisplayMoney="Right"
                                        Enabled="True" Mask="999,999,999" MaskType="Number"
                                        TargetControlID="txtAhorrosCausacionmasahorros"></asp:MaskedEditExtender>
                                    <asp:TextBox ID="txtAhorrosCausacionmasahorros" runat="server" CssClass="textbox"
                                        Visible="False" Width="16px"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </table>
                </ContentTemplate>
            </asp:TabPanel>
            <asp:TabPanel ID="tabAhorroProgramado" runat="server" HeaderText="Datos">
                <HeaderTemplate>
                    Ahorros Programados
                </HeaderTemplate>
                <ContentTemplate>
                    <table width="100%">
                        <tr>
                            <td style="text-align: left">
                                <div id="divprogrmado" runat="server"
                                    style="overflow: scroll; width: 100%; max-height: 600px">
                                    <asp:CheckBox ID="chkAhoProgra" runat="server" Text="Mostrar cuentas cerradas"
                                        AutoPostBack="True" OnCheckedChanged="chkAhoProgra_CheckedChanged" />
                                    <br />
                                    <asp:GridView ID="gvAhoProgra" runat="server" Width="110%"
                                        AutoGenerateColumns="False" AllowPaging="True"
                                        OnPageIndexChanging="gvAhoProgra_PageIndexChanging"
                                        OnSelectedIndexChanged="gvAhoProgra_SelectedIndexChanged" PageSize="20"
                                        DataKeyNames="numero_programado">
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="btnInfo" runat="server" CommandName="Select"
                                                        ImageUrl="~/Images/gr_general.jpg" ToolTip="Detalle"
                                                        Width="16px" />
                                                </ItemTemplate>
                                                <HeaderStyle CssClass="gridIco" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="numero_programado" HeaderText="Num Cuenta">
                                                <HeaderStyle Width="30px" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="nomlinea" HeaderText="Línea">
                                                <HeaderStyle Width="4px" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="plazo" HeaderText="Plazo">
                                                <HeaderStyle Width="10px" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="identificacion" HeaderText="Identificación"
                                                Visible="False">
                                                <ItemStyle HorizontalAlign="Left" />
                                                <HeaderStyle Width="50px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="nombre" HeaderText="Titular" Visible="False">
                                                <ItemStyle HorizontalAlign="Left" />
                                                <HeaderStyle Width="50px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="nomoficina" HeaderText="Oficina">
                                                <HeaderStyle Width="20px" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="fecha_apertura" HeaderText="F. Apertura"
                                                DataFormatString="{0:d}">
                                                <HeaderStyle Width="40px" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="nom_estado" HeaderText="Estado">
                                                <HeaderStyle Width="30px" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="saldo" HeaderText="Saldo Total"
                                                DataFormatString="{0:N0}">
                                                <HeaderStyle Width="80px" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="fecha_ultimo_pago" HeaderText="F. Ult Mov"
                                                DataFormatString="{0:d}">
                                                <HeaderStyle Width="40px" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="fecha_proximo_pago" HeaderText="F. Prox Pago"
                                                DataFormatString="{0:d}">
                                                <HeaderStyle Width="50px" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="nomforma_pago" HeaderText="Forma Pago">
                                                <HeaderStyle Width="50px" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="fecha_vencimiento" HeaderText="F.Vencimie."
                                                DataFormatString="{0:d}">
                                                <HeaderStyle Width="30px" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="valor_cuota" HeaderText="Cuota"
                                                DataFormatString="{0:N0}">
                                                <HeaderStyle Width="40px" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="nom_periodicidad" HeaderText="Periodicidad">
                                                <HeaderStyle Width="40px" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="cuotas_pagadas" HeaderText="Cuotas Pagas">
                                                <HeaderStyle Width="10px" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="tasa_interes" HeaderText="Tasa Interés">
                                                <HeaderStyle Width="40px" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="valor_acumulado"
                                                HeaderText="Rendimientos Causados" DataFormatString="{0:N0}">
                                                <HeaderStyle Width="40px" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Valor + Rendim.Causados"
                                                DataField="valor_total_acumu" DataFormatString="{0:N0}">
                                                <HeaderStyle Width="40px" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                        </Columns>
                                        <HeaderStyle CssClass="gridHeader" Font-Size="X-Small" />
                                        <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                                        <RowStyle CssClass="gridItem" Font-Size="X-Small" />
                                    </asp:GridView>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: center">
                                <asp:Label ID="lblTotalAhoProgra" runat="server" Visible="False" />
                                <asp:Label ID="lblInfoAhoProgra" runat="server"
                                    Text="Su consulta no obtuvó ningún resultado." Visible="False" />
                            </td>
                        </tr>
                        <table>
                            <tr>
                                <td class="tdI">
                                    <asp:Label ID="lbltotalAhoProgramado" runat="server" Text="Total Saldos">
                                    </asp:Label>
                                    <br />
                                    <asp:TextBox ID="txtTotalAhoProgramado" runat="server" CssClass="textbox"
                                        DataFormatString="{0:N0}" Enabled="False" Style="text-align: center"
                                        Width="140px"></asp:TextBox>
                                    <asp:MaskedEditExtender ID="MaskedEditExtender10" runat="server"
                                        CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder=""
                                        CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder=""
                                        CultureThousandsPlaceholder="" CultureTimePlaceholder="" DisplayMoney="Right"
                                        Enabled="True" Mask="999,999,999" MaskType="Number"
                                        TargetControlID="txtAhoProgramadoTotal"></asp:MaskedEditExtender>
                                    <asp:TextBox ID="txtAhoProgramadoTotal" runat="server" CssClass="textbox"
                                        Visible="False" Width="16px"></asp:TextBox>
                                </td>
                                <td class="tdI">
                                    <asp:Label ID="lblTotalCuotasAhoProgra" runat="server" Text="Total Cuotas Ahorros">
                                    </asp:Label>
                                    <br />
                                    <asp:TextBox ID="txtTotalCuotasAhoProgra" runat="server" CssClass="textbox"
                                        DataFormatString="{0:N0}" Enabled="False" Style="text-align: center"
                                        Width="140px"></asp:TextBox>
                                    <asp:MaskedEditExtender ID="MaskedEditExtender11" runat="server"
                                        CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder=""
                                        CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder=""
                                        CultureThousandsPlaceholder="" CultureTimePlaceholder="" DisplayMoney="Right"
                                        Enabled="True" Mask="999,999,999" MaskType="Number"
                                        TargetControlID="txtProgramadoCuotas"></asp:MaskedEditExtender>
                                    <asp:TextBox ID="txtProgramadoCuotas" runat="server" CssClass="textbox"
                                        Visible="False" Width="16px"></asp:TextBox>
                                </td>
                                <td class="tdI">
                                    <asp:Label ID="lblTotalCausacionProgramado" runat="server"
                                        Text="Total Rendimientos Causados"></asp:Label>
                                    <br />
                                    <asp:TextBox ID="txtTotalCausacionProgramado" runat="server" CssClass="textbox"
                                        DataFormatString="{0:N0}" Enabled="False" Style="text-align: center"
                                        Width="140px"></asp:TextBox>
                                    <asp:MaskedEditExtender ID="MaskedEditExtender17" runat="server"
                                        CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder=""
                                        CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder=""
                                        CultureThousandsPlaceholder="" CultureTimePlaceholder="" DisplayMoney="Right"
                                        Enabled="True" Mask="999,999,999" MaskType="Number"
                                        TargetControlID="txtProgramadoCausacion"></asp:MaskedEditExtender>
                                    <asp:TextBox ID="txtProgramadoCausacion" runat="server" CssClass="textbox"
                                        Visible="False" Width="16px"></asp:TextBox>
                                </td>

                                <td class="tdI">
                                    <asp:Label ID="lblTotalCaumassaldoProgramado" runat="server"
                                        Text="Total Saldo + Rend. Causados"></asp:Label>
                                    <br />
                                    <asp:TextBox ID="txtTotalCausacionmasProgramado" runat="server" CssClass="textbox"
                                        DataFormatString="{0:N0}" Enabled="False" Style="text-align: center"
                                        Width="140px"></asp:TextBox>
                                    <asp:MaskedEditExtender ID="MaskedEditExtender22" runat="server"
                                        CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder=""
                                        CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder=""
                                        CultureThousandsPlaceholder="" CultureTimePlaceholder="" DisplayMoney="Right"
                                        Enabled="True" Mask="999,999,999" MaskType="Number"
                                        TargetControlID="txtTotalCausacionmasProgramado"></asp:MaskedEditExtender>
                                    <asp:TextBox ID="txtProgramadoCausacionmassaldo" runat="server" CssClass="textbox"
                                        Visible="False" Width="16px"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </table>
                </ContentTemplate>
            </asp:TabPanel>
            <asp:TabPanel ID="tabCDATS" runat="server" HeaderText="Datos">
                <HeaderTemplate>
                    CDATS
                </HeaderTemplate>
                <ContentTemplate>
                    <table width="100%">
                        <tr>
                            <td style="text-align: left">
                                <div id="div1" runat="server" style="overflow: scroll; width: 100%;">
                                    <asp:CheckBox ID="chkCDATS" runat="server" Text="Mostrar CDATS cerrados"
                                        AutoPostBack="True" OnCheckedChanged="chkCDATS_CheckedChanged" />
                                    <br />
                                    <asp:GridView ID="gvCDATS" runat="server" Width="100%" AutoGenerateColumns="False"
                                        AllowPaging="True" OnPageIndexChanging="gvCDATS_PageIndexChanging"
                                        OnSelectedIndexChanged="gvCDATS_SelectedIndexChanged" PageSize="20"
                                        DataKeyNames="codigo_cdat">
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="btnInfo" runat="server" CommandName="Select"
                                                        ImageUrl="~/Images/gr_general.jpg" ToolTip="Detalle"
                                                        Width="16px" />
                                                </ItemTemplate>
                                                <HeaderStyle CssClass="gridIco" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="numero_cdat" HeaderText="Num Cdat">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="nomlinea" HeaderText="Línea">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="nomoficina" HeaderText="Oficina">
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="fecha_apertura" HeaderText="F. Apertura"
                                                DataFormatString="{0:d}">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="fecha_inicio" HeaderText="F. Inicio"
                                                DataFormatString="{0:d}">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="fecha_vencimiento" HeaderText="F. Vencimiento"
                                                DataFormatString="{0:d}">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="nom_estado" HeaderText="Estado">
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="valor" HeaderText="Valor"
                                                DataFormatString="{0:N0}">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="plazo" HeaderText="Plazo"
                                                DataFormatString="{0:n}">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="nomperiodicidad" HeaderText="Periodicidad">
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="tasa_interes" HeaderText="Tasa Interes">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>

                                            <asp:BoundField DataField="valor_acumulado"
                                                HeaderText="Rendimientos Causados" DataFormatString="{0:N0}" />
                                            <asp:BoundField HeaderText="Valor + Rendim.Causados"
                                                DataField="valor_total_acumu" DataFormatString="{0:N0}">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>
                                        </Columns>
                                        <HeaderStyle CssClass="gridHeader"></HeaderStyle>
                                        <PagerStyle CssClass="gridPager"></PagerStyle>
                                        <RowStyle CssClass="gridItem" Font-Size="X-Small"></RowStyle>
                                    </asp:GridView>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: center">
                                <asp:Label ID="lblTotalCDATS" runat="server" Visible="False" />
                                <asp:Label ID="lblInfoCDATS" runat="server"
                                    Text="Su consulta no obtuvó ningún resultado." Visible="False" />
                            </td>
                        </tr>
                        <table>
                            <tr>
                                <td class="tdI">
                                    <asp:Label ID="lbltotalCdat" runat="server" Text="Total Saldos"></asp:Label>
                                    <br />
                                    <asp:TextBox ID="txtTotalCdat" runat="server" CssClass="textbox"
                                        DataFormatString="{0:N0}" Enabled="False" Style="text-align: center"
                                        Width="140px"></asp:TextBox>
                                    <asp:MaskedEditExtender ID="MaskedEditExtender15" runat="server"
                                        CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder=""
                                        CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder=""
                                        CultureThousandsPlaceholder="" CultureTimePlaceholder="" DisplayMoney="Right"
                                        Enabled="True" Mask="999,999,999" MaskType="Number"
                                        TargetControlID="txtCdatTotal"></asp:MaskedEditExtender>
                                    <asp:TextBox ID="txtCdatTotal" runat="server" CssClass="textbox" Visible="False"
                                        Width="16px"></asp:TextBox>
                                </td>
                                <td class="tdI">
                                    <asp:Label ID="lblTotalCausacionCdat" runat="server"
                                        Text="Total Rendimientos Causados"></asp:Label>
                                    <br />
                                    <asp:TextBox ID="txtTotalCausacionCdat" runat="server" CssClass="textbox"
                                        DataFormatString="{0:N0}" Enabled="False" Style="text-align: center"
                                        Width="140px"></asp:TextBox>
                                    <asp:MaskedEditExtender ID="MaskedEditExtender18" runat="server"
                                        CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder=""
                                        CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder=""
                                        CultureThousandsPlaceholder="" CultureTimePlaceholder="" DisplayMoney="Right"
                                        Enabled="True" Mask="999,999,999" MaskType="Number"
                                        TargetControlID="txtCdatCausacion"></asp:MaskedEditExtender>
                                    <asp:TextBox ID="txtCdatCausacion" runat="server" CssClass="textbox" Visible="False"
                                        Width="16px"></asp:TextBox>


                                    <td class="tdI">
                                        <asp:Label ID="lblTotalCaumassaldoCdat" runat="server"
                                            Text="Total Saldo + Rend. Causados"></asp:Label>
                                        <br />
                                        <asp:TextBox ID="txtTotalCausacionmasCdat" runat="server" CssClass="textbox"
                                            DataFormatString="{0:N0}" Enabled="False" Style="text-align: center"
                                            Width="140px"></asp:TextBox>
                                        <asp:MaskedEditExtender ID="MaskedEditExtender23" runat="server"
                                            CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder=""
                                            CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder=""
                                            CultureThousandsPlaceholder="" CultureTimePlaceholder="" DisplayMoney="Right"
                                            Enabled="True" Mask="999,999,999" MaskType="Number"
                                            TargetControlID="txtTotalCausacionmasCdat"></asp:MaskedEditExtender>
                                        <asp:TextBox ID="txtCdatCausacionmassaldo" runat="server" CssClass="textbox"
                                            Visible="False" Width="16px"></asp:TextBox>
                                    </td>

                                </td>
                            </tr>
                        </table>
                    </table>
                </ContentTemplate>
            </asp:TabPanel>
            <asp:TabPanel ID="tabServicios" runat="server" HeaderText="Servicios">
                <HeaderTemplate>
                    Servicios
                </HeaderTemplate>
                <ContentTemplate>
                    <table width="100%">
                        <tr>
                            <td style="text-align: left">
                                <asp:CheckBox ID="chkBoxServiciosCerrados" runat="server"
                                    Text="Mostrar Servicios cerrados" AutoPostBack="True"
                                    OnCheckedChanged="chkBoxServiciosCerrados_CheckedChanged" />
                                <br />

                                <asp:GridView ID="gridServicios" runat="server" AllowPaging="True"
                                    AutoGenerateColumns="False" DataKeyNames="num_servicio" GridLines="Horizontal"
                                    PageSize="20" ShowHeaderWhenEmpty="True" Width="93%"
                                    OnRowCommand="gridServicios_RowCommand"
                                    OnPageIndexChanging="gridServicios_PageIndexChanging">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnMovGenCredito" runat="server"
                                                    CommandArgument='<%#Eval("num_servicio") %>'
                                                    CommandName="MovGralServicios" ImageUrl="~/Images/gr_general.jpg"
                                                    ToolTip="Movimiento del Servicio" />
                                            </ItemTemplate>
                                            <HeaderStyle CssClass="gridIco" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="num_servicio" HeaderText="Num. Servicio">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="linea" HeaderText="Linea">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="valor" DataFormatString="{0:N0}"
                                            HeaderText="Saldo Inicial" />
                                        <asp:BoundField DataField="fecha_desembolso" DataFormatString="{0:d}"
                                            HeaderText="Fecha Desembolso" />
                                        <asp:BoundField DataField="plan" HeaderText="Plan">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="fecha_proximo_pago" HeaderText="F. Próx. Pago"
                                            DataFormatString="{0:d}"></asp:BoundField>
                                        <asp:BoundField DataField="fecha_final_vigencia" HeaderText="Fec. Vencimiento"
                                            DataFormatString="{0:d}">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="forma_pago" HeaderText="Forma Pago">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="nom_estado" HeaderText="Estado"></asp:BoundField>
                                        <asp:BoundField DataField="cuotas" HeaderText="Plazo">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="saldo" DataFormatString="{0:N0}" HeaderText="Saldo">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="cuota" DataFormatString="{0:N0}" HeaderText="Cuota">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Dias_Mora" HeaderText="Dias Mora">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="valor_pendiente" DataFormatString="{0:N0}" HeaderText="Valor Pendiente">
                                            <ItemStyle HorizontalAlign="center" />
                                        </asp:BoundField>
                                    </Columns>
                                    <HeaderStyle CssClass="gridHeader" Font-Size="X-Small" />
                                    <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                                    <RowStyle CssClass="gridItem" Font-Size="X-Small" />
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: center">
                                <asp:Label ID="lblTotalRegistrosServicios" runat="server" Visible="False" />
                                <asp:Label ID="lblinfoServicios" runat="server"
                                    Text="Su consulta no obtuvo ningún resultado." Visible="False" />
                            </td>
                        </tr>
                        <table>
                            <tr>
                                <td class="tdI">
                                    <asp:Label ID="lbltotalValorInicialServicios" runat="server"
                                        Text="Total Saldo Inicial" /><br />
                                    <asp:TextBox ID="txtTotalValorInicialServicios" runat="server" CssClass="textbox"
                                        Enabled="False" DataFormatString="{0:N0}" Style="text-align: center"
                                        Width="140px"></asp:TextBox>
                                    <asp:MaskedEditExtender ID="MaskedEditExtender14" runat="server"
                                        CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder=""
                                        CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder=""
                                        CultureThousandsPlaceholder="" CultureTimePlaceholder="" DisplayMoney="Right"
                                        Enabled="True" Mask="999,999,999" MaskType="Number"
                                        TargetControlID="txtServiciosvalorInicial"></asp:MaskedEditExtender>
                                    <asp:TextBox ID="txtServiciosvalorInicial" runat="server" CssClass="textbox"
                                        Visible="False" Width="16px"></asp:TextBox>
                                </td>

                                <td class="tdI">
                                    <asp:Label ID="lbltotalServicios" runat="server" Text="Total Saldos" /><br />
                                    <asp:TextBox ID="txtTotalServicios" runat="server" CssClass="textbox"
                                        Enabled="False" DataFormatString="{0:N0}" Style="text-align: center"
                                        Width="140px"></asp:TextBox>
                                    <asp:MaskedEditExtender ID="MaskedEditExtender12" runat="server"
                                        CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder=""
                                        CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder=""
                                        CultureThousandsPlaceholder="" CultureTimePlaceholder="" DisplayMoney="Right"
                                        Enabled="True" Mask="999,999,999" MaskType="Number"
                                        TargetControlID="txtServiciosTotal"></asp:MaskedEditExtender>
                                    <asp:TextBox ID="txtServiciosTotal" runat="server" CssClass="textbox"
                                        Visible="False" Width="16px"></asp:TextBox>
                                </td>
                                <td class="tdI">
                                    <asp:Label ID="lblTotalCuotasServicios" runat="server"
                                        Text="Total Cuotas Servicios" /><br />
                                    <asp:TextBox ID="txtTotalCuotasServicios" runat="server" CssClass="textbox"
                                        Enabled="False" DataFormatString="{0:N0}" Style="text-align: center"
                                        Width="140px"></asp:TextBox>
                                    <asp:MaskedEditExtender ID="MaskedEditExtender13" runat="server"
                                        CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder=""
                                        CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder=""
                                        CultureThousandsPlaceholder="" CultureTimePlaceholder="" DisplayMoney="Right"
                                        Enabled="True" Mask="999,999,999" MaskType="Number"
                                        TargetControlID="txtServiciosCuotas"></asp:MaskedEditExtender>
                                    <asp:TextBox ID="txtServiciosCuotas" runat="server" CssClass="textbox"
                                        Visible="False" Width="16px"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </table>
                </ContentTemplate>
            </asp:TabPanel>
            <asp:TabPanel ID="tabDevoluciones" runat="server" HeaderText="Devoluciones">
                <HeaderTemplate>
                    Devoluciones
                </HeaderTemplate>
                <ContentTemplate>
                    <table>
                        <tr>
                            <td style="width: 100%; text-align: center">
                                <asp:CheckBox ID="cheksaldo" runat="server" Text="Mostrar Saldos en cero"
                                    OnCheckedChanged="chkTabCredito_CheckedChanged" AutoPostBack="True" />
                                &#160;&#160;&#160;&#160;
                                <asp:CheckBox ID="chkImprimirDevolucion" runat="server" Text="Imprimir Devoluciones"
                                    Checked="True" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:GridView ID="gvDevoluciones" runat="server" AllowPaging="True"
                                    AutoGenerateColumns="False" GridLines="Horizontal" PageSize="20"
                                    ShowHeaderWhenEmpty="True" DataKeyNames="num_devolucion, saldo" Width="80%"
                                    OnSelectedIndexChanged="gvDevoluciones_SelectedIndexChanged"
                                    OnPageIndexChanging="gvDevoluciones_PageIndexChanging"
                                    OnRowCommand="gvdetDevol_RowCommand">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnInfo" runat="server" CommandName="DetalleDevol"
                                                    ImageUrl="~/Images/gr_general.jpg"
                                                    CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                                    ToolTip="Detalle" Width="16px" />
                                            </ItemTemplate>
                                            <HeaderStyle CssClass="gridIco" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="num_devolucion" HeaderText="Num. Devolución">
                                            <ItemStyle HorizontalAlign="Left" Width="50px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="concepto" HeaderText="Linea">
                                            <ItemStyle HorizontalAlign="Left" Width="250px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="fecha_devolucion" DataFormatString="{0:d}"
                                            HeaderText="Fec. Apertura">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="valor" HeaderText="Valor" DataFormatString="{0:c}">
                                            <ItemStyle HorizontalAlign="Center" Width="100px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="saldo" HeaderText="Saldo" DataFormatString="{0:c}">
                                            <ItemStyle HorizontalAlign="Center" Width="100px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="estado" HeaderText="Estado" />
                                    </Columns>
                                    <HeaderStyle CssClass="gridHeader" Font-Size="X-Small" />
                                    <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                                    <RowStyle CssClass="gridItem" Font-Size="X-Small" />
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblTotalRegsDev" runat="server" Visible="False" />
                                <asp:Label ID="lblInfoDev" runat="server" Text="Su consulta no obtuvo ningún resultado."
                                    Visible="False" />
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100%; text-align: left">
                                <asp:Label ID="lblDevoluciones" runat="server" Text="Total Devoluciones" /><br />
                                <asp:TextBox ID="txtDevolucionestotal" runat="server" CssClass="textbox" Enabled="False"
                                    Style="text-align: center" Width="140px" />
                                <asp:MaskedEditExtender ID="MaskedEditExtender7" runat="server"
                                    CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                    CultureDatePlaceholder="" CultureDecimalPlaceholder=""
                                    CultureThousandsPlaceholder="" CultureTimePlaceholder="" DisplayMoney="Right"
                                    Enabled="True" Mask="999,999,999" MaskType="Number"
                                    TargetControlID="txtDevolucionestotal"></asp:MaskedEditExtender>
                                <uc1:decimales ID="txtDevolucionestotales" runat="server" CssClass="textbox"
                                    Visible="False" Width="16px" />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:TabPanel>
            <asp:TabPanel ID="tabLineas" runat="server" HeaderText="Lineas" Visible="false">
                <HeaderTemplate>
                    Telefonía
                </HeaderTemplate>
                <ContentTemplate>
                    <table>
                        <tr>
                            <td style="width: 100%; text-align: left">
                                <asp:CheckBox ID="CheckLinesInactivas" runat="server" Text="Mostrar Lineas Inactivas"
                                    OnCheckedChanged="chktabLineas_CheckedChanged" AutoPostBack="True" />

                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:GridView ID="gvLineas" runat="server" AllowPaging="True"
                                    AutoGenerateColumns="False" DataKeyNames="num_linea_telefonica"
                                    GridLines="Horizontal" PageSize="20" ShowHeaderWhenEmpty="True" Width="90%"
                                    OnSelectedIndexChanged="gvLineas_SelectedIndexChanged"
                                    OnPageIndexChanging="gvLineas_PageIndexChanging">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnInfo" runat="server" CommandName="Select"
                                                    ImageUrl="~/Images/gr_general.jpg" Visible="false" ToolTip="Detalle"
                                                    Width="16px" />
                                            </ItemTemplate>
                                            <HeaderStyle CssClass="gridIco" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="num_linea_telefonica" HeaderText="Num. Linea">
                                            <ItemStyle HorizontalAlign="Center" Width="50px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="cod_serv_fijo" HeaderText="Num. Servicio">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="cod_serv_adicional"
                                            HeaderText="Num. Servicio Adicional">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="fecha_activacion" DataFormatString="{0:d}"
                                            HeaderText="Fecha Activación">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="fecha_ult_reposicion" DataFormatString="{0:d}"
                                            HeaderText="Fec. Ult Reposición">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="valor_fijo" HeaderText="Cargo Fijo"
                                            DataFormatString="{0:c}">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="valor_total" HeaderText="Costo Factura"
                                            DataFormatString="{0:c}">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="fecha_vencimiento" DataFormatString="{0:d}"
                                            HeaderText="Fecha Vencimiento">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="nom_plan" HeaderText="Tipo Plan">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="fecha_incativacion" DataFormatString="{0:d}"
                                            HeaderText="Fecha Inactivación">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                    </Columns>
                                    <HeaderStyle CssClass="gridHeader" Font-Size="X-Small" />
                                    <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                                    <RowStyle CssClass="gridItem" Font-Size="X-Small" />
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label6" runat="server" Visible="False" />
                                <asp:Label ID="Label7" runat="server" Text="Su consulta no obtuvo ningún resultado."
                                    Visible="False" />
                            </td>
                        </tr>
                        <%-- <tr>
                            <td style="width: 100%; text-align: left">
                                <asp:Label ID="Label8" runat="server" Text="Total Devoluciones" /><br />
                                <asp:TextBox ID="TextBox1" runat="server" CssClass="textbox" Enabled="False"
                                    Style="text-align: center" Width="140px" /><asp:MaskedEditExtender ID="MaskedEditExtender25"
                                        runat="server" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder=""
                                        CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                        CultureTimePlaceholder="" DisplayMoney="Right" Enabled="True" Mask="999,999,999"
                                        MaskType="Number" TargetControlID="txtDevolucionestotal">
                                    </asp:MaskedEditExtender>
                                <uc1:decimales ID="Decimales1" runat="server" CssClass="textbox" Visible="False"
                                    Width="16px" />
                            </td>
                        </tr>--%>
                    </table>
                </ContentTemplate>
            </asp:TabPanel>
        </asp:TabContainer>

        <table cellpadding="5" cellspacing="0" style="width: 103%">
            <tr>
                <td class="tdI" align="center">
                    <asp:MultiView ID="mvReporte" runat="server">
                        <asp:View ID="vReporte" runat="server">
                            <div style="text-align: left">
                                <asp:Button ID="btnAtras" runat="server" CssClass="btn8" Height="25px" Width="120px"
                                    Text="Cerrar Informe" OnClick="btnAtras_Click" />
                                &#160;&#160;
                                <asp:Button ID="btnImprime" runat="server" CssClass="btn8" Height="25px" Width="120px"
                                    Text="Imprimir" OnClick="btnImprime_Click" />
                            </div>
                            <table width="100%">
                                <tr>
                                    <td>
                                        <rsweb:ReportViewer ID="RpviewEstado" runat="server" Font-Names="Verdana"
                                            Font-Size="8pt" Height="500px" InteractiveDeviceInfos="(Colección)"
                                            WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="96%"
                                            EnableViewState="True">
                                            <LocalReport
                                                ReportPath="Page\Asesores\EstadoCuenta\ReporteEstadodeCuenta.rdlc" />
                                        </rsweb:ReportViewer>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <iframe id="frmPrint" name="IframeName" width="100%"
                                            src="../../Reportes/Reporte.aspx" height="500px" runat="server"
                                            style="border-style: groove; float: left;"></iframe>
                                    </td>
                                </tr>
                            </table>
                        </asp:View>
                    </asp:MultiView>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <br />
    <asp:HiddenField ID="HiddenField1" runat="server" />
    <asp:ModalPopupExtender ID="mpeAgregar" runat="server" PopupControlID="panelAgregarLink"
        TargetControlID="HiddenField1" BackgroundCssClass="modalBackground">
    </asp:ModalPopupExtender>
    <asp:Panel ID="panelAgregarLink" runat="server" BackColor="White" CssClass="pnlBackGround" Width="500px">
        <asp:UpdatePanel ID="updEmergente" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <table style="width: 100%;">
                    <tr>
                        <td colspan="3">&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" style="text-align: center">Nombre
                            <br />
                            <asp:TextBox ID="txtNombreParametro" runat="server" CssClass="textbox" Width="300px"
                                MaxLength="20" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" style="text-align: center">Opción
                            <br />
                            <asp:DropDownList ID="ddlOpcion" runat="server" CssClass="textbox" Width="310px" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:CheckBox ID="chkExtend" Text="Parámetro del reporte" runat="server" Visible="false"
                                AutoPostBack="True" OnCheckedChanged="chkExtend_CheckedChanged" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:TextBox ID="txtExtendQuery" runat="server" CssClass="textbox" Width="300px"
                                Visible="false" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" style="text-align: center">
                            <asp:Label ID="lblError" runat="server" Style="font-size: x-small; color: Red" />
                        </td>
                    </tr>
                </table>
                <table style="width: 100%;">
                    <tr>
                        <td>&nbsp;
                        </td>
                        <td style="text-align: center;">
                            <asp:Button ID="btnContinuar" runat="server" Text="Guardar" CssClass="btn8" Width="182px"
                                Height="27px" OnClick="btnContinuar_Click" />
                            &nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnParar" runat="server" Text="Cancelar" CssClass="btn8" Width="182px"
                                Height="27px" OnClick="btnParar_Click" />
                        </td>
                        <td>&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" style="text-align: center">&nbsp;
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
            <Triggers>
                <%--<asp:PostBackTrigger ControlID="btnContinuar"/>--%>
                <asp:AsyncPostBackTrigger ControlID="btnContinuar" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
    </asp:Panel>
    <asp:ModalPopupExtender ID="mpeEliminar" runat="server" PopupControlID="PanelEliminarLink"
        TargetControlID="HiddenField1" BackgroundCssClass="modalBackground">
    </asp:ModalPopupExtender>
    <asp:Panel ID="PanelEliminarLink" runat="server" BackColor="White" CssClass="pnlBackGround" Width="450px">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <table width="450px">
                    <tr>
                        <td style="text-align: center; width: 100%">
                            <strong>Listado de Links</strong>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; width: 100%">
                            <asp:GridView ID="gvLinks" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                DataKeyNames="idlink" GridLines="Horizontal" PageSize="20" ShowHeaderWhenEmpty="True"
                                OnPageIndexChanging="gvLinks_PageIndexChanging" OnRowDeleting="gvLinks_RowDeleting">
                                <Columns>
                                    <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/gr_elim.jpg"
                                        ShowDeleteButton="True" />
                                    <asp:BoundField DataField="idlink" HeaderText="Código">
                                        <ItemStyle HorizontalAlign="Left" Width="100px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="nombre" HeaderText="Nombre">
                                        <ItemStyle HorizontalAlign="Left" Width="350px" />
                                    </asp:BoundField>
                                </Columns>
                                <HeaderStyle CssClass="gridHeader" Font-Size="X-Small" />
                                <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                                <RowStyle CssClass="gridItem" Font-Size="X-Small" />
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr style="text-align: center; width: 100%">
                        <td style="text-align: center">
                            <asp:Label ID="lblTotalLinks" runat="server" Visible="False" />
                            <asp:Label ID="lblInfoLinks" runat="server" Text="Su consulta no obtuvo ningún resultado."
                                Visible="False" />
                        </td>
                    </tr>
                    <tr style="text-align: center; width: 100%">
                        <td style="text-align: center">
                            <asp:Button ID="btnCerrarLinks" runat="server" Text="Cancelar" CssClass="btn8" Width="182px"
                                Height="27px" OnClick="btnCerrarLinks_Click" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>

    <asp:Panel ID="panelMovAfiliacion" runat="server" Style="margin-bottom: 0px" Height="200px" Width="400px">
        <table style="width: 100%">
            <tr>
                <td style="text-align: center">
                    <span><strong>Movimiento de Afiliación:</strong></span>
                </td>
                <td style="text-align: right">
                    <asp:ImageButton runat="server" ID="ImageButton1" ImageUrl="~/Images/btnCerrar.jpg"
                        OnClientClick="return ControlBallon()" ToolTip="Cancelar" />
                </td>
            </tr>
        </table>
        <br />
        <asp:GridView ID="gvMovAfiliacion" runat="server" AllowPaging="False" AutoGenerateColumns="False"
            DataKeyNames="cod_ope" GridLines="Horizontal" HeaderStyle-CssClass="gridHeader"
            PagerStyle-CssClass="gridPager" PageSize="7" RowStyle-CssClass="gridItem" Width="100%" Height="100%"
            Style="font-size: x-small">
            <Columns>
                <asp:BoundField DataField="fecha_oper" HeaderText="Fecha Pago" DataFormatString="{0:d}" />
                <asp:BoundField DataField="cod_ope" HeaderText="Cod.Ope" />
                <asp:BoundField DataField="tipo_ope" HeaderText="Tip.Ope" />
                <asp:BoundField DataField="nomtipo_ope" HeaderText="Descripción" />
                <asp:BoundField DataField="num_comp" HeaderText="Num Comprobante">
                    <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="tipo_comp" HeaderText="Tipo Comprobante">
                    <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="valor" HeaderText="Valor" DataFormatString="{0:c}">
                    <ItemStyle HorizontalAlign="Right" Width="100" />
                </asp:BoundField>
            </Columns>
        </asp:GridView>
        <asp:Label ID="lblTotalRegsAfi" runat="server" Visible="False" />
    </asp:Panel>

    <asp:HiddenField ID="HiddenField2" runat="server" />
    <asp:ModalPopupExtender ID="MpeDetalleDevol" runat="server" Enabled="True" PopupDragHandleControlID="Panelf"
        PopupControlID="PanelDetalleDevol" TargetControlID="HiddenField1" CancelControlID="btnCloseAct">
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
    <asp:Panel ID="PanelDetalleDevol" runat="server" Width="480px" Style="display: none; border: solid 2px Gray"
        CssClass="modalPopup">
        <asp:UpdatePanel ID="upDetallePago" runat="server">
            <ContentTemplate>
                <table>
                    <tr>
                        <td style="font-size: x-small: color: #FFFFFF; background-color: #0066FF; width: 434px">
                            <asp:Panel ID="Panelf" runat="server" Width="475px" Style="cursor: move">
                                <strong>Detalle Devoluciones</strong>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 475px">
                            <asp:GridView ID="GVDetalleDevoluciones" runat="server" AutoGenerateColumns="False"
                                BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px"
                                CellPadding="4" ForeColor="Black" GridLines="Vertical" PageSize="20" Width="480px"
                                Style="text-align: left; font-size: xx-small">
                                <AlternatingRowStyle BackColor="White" />
                                <Columns>
                                    <asp:BoundField DataField="num_devolucion" HeaderText="No. Devolución">
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="num_comp" HeaderText="No. Comprobante">
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="tipo_comp" HeaderText="Tipo Comprobante">
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="fecha_oper" HeaderText="Fecha Operación"
                                        DataFormatString="{0:d}">
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="valor" HeaderText="Valor" DataFormatString="{0:c}">
                                        <ItemStyle HorizontalAlign="Left" />
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
                            <asp:Button ID="btnCloseAct" runat="server" Text="Cerrar" CssClass="button"
                                OnClick="btnCloseAct_Click" CausesValidation="false" Height="20px" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
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
    </style>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>
    <script type='text/javascript'>
        $(document).ready(function () {
            //PREGUNTO SI LA PAGINA VIENE DE SER DUPLIACADA
            preventDuplicateTab();
        });
        window.onbeforeunload = function (e) {
            var e = e || window.event;
            //e.returnValue = 'sali pext';
            if (window.name == "*ukn*") {
                window.name = "*ukn*";
            }
        }
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
                window.name = "*ukn*";
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

        function Forzar() {
            __doPostBack('', '');
        }

        function ControlBallon() {
            var ctrl = $find('btnInfo_BalloonPopupExtender');

            if (ctrl._popupVisible == true)
                ctrl.hidePopup();
            else
                ctrl.showPopup();

            return false;
        }
    </script>
    <script type="text/javascript">
        function rceFirmaResize(sender, e) {
            var panelFirmas = document.getElementById('<%= panelFirmas.ClientID %>');
            var imgTarjetaFirmas = document.getElementById('<%= imgTarjetaFirmas.ClientID %>');
            imgTarjetaFirmas.width = panelFirmas.width;
            imgTarjetaFirmas.height = panelFirmas.height;

        }

        $(".IconNotification").mouseover(function (e) {
            $("#" + e.delegateTarget.id).parent().find(".notiContent").css("display", "block");
        }).mouseleave(function (e) {
            $("#" + e.delegateTarget.id).parent().find(".notiContent").css("display", "none");
        });

        function Notification(htmlElement) {

            this.htmlElement = htmlElement;
            this.icon = htmlElement.querySelector('.icon > i');
            this.text = htmlElement.querySelector('.text');
            this.close = htmlElement.querySelector('.close');
            this.isRunning = false;
            this.timeout;

            this.bindEvents();
        };

        Notification.prototype.bindEvents = function () {
            var self = this;
            this.close.addEventListener('click', function () {
                window.clearTimeout(self.timeout);
                self.reset();
            });
        }

        Notification.prototype.info = function (message) {
            if (this.isRunning) return false;

            this.text.innerHTML = message;
            this.htmlElement.className = 'notification info';
            this.icon.className = 'fa fa-2x fa-info-circle';

            this.show();
        }

        Notification.prototype.warning = function (message) {
            if (this.isRunning) return false;

            this.text.innerHTML = message;
            this.htmlElement.className = 'notification warning';
            this.icon.className = 'fa fa-2x fa-exclamation-triangle';

            this.show();
        }

        Notification.prototype.error = function (message) {
            if (this.isRunning) return false;

            this.text.innerHTML = message;
            this.htmlElement.className = 'notification error';
            this.icon.className = 'fa fa-2x fa-exclamation-circle';

            this.show();
        }

        Notification.prototype.show = function () {
            if (!this.htmlElement.classList.contains('visible'))
                this.htmlElement.classList.add('visible');

            this.isRunning = true;
            this.autoReset();
        };

        Notification.prototype.autoReset = function () {
            var self = this;
            this.timeout = window.setTimeout(function () {
                self.reset();
            }, 5000);
        }

        Notification.prototype.reset = function () {
            this.htmlElement.className = "notification";
            this.icon.className = "";
            this.isRunning = false;
        };

        document.addEventListener('DOMContentLoaded', init);

        function init() {
            var info = document.getElementById('info');
            var warn = document.getElementById('warn');
            var error = document.getElementById('error');

            var notificator = new Notification(document.querySelector('.notification'));

            info.onclick = function () {
                notificator.info('Esta es una información');
            }

            warn.onclick = function () {
                notificator.warning('Te te te advieeeerto!');
            }

            error.onclick = function () {
                notificator.error('Le causaste derrame al sistema');
            }
        }
    </script>

    <asp:HiddenField ID="hfTarjetaFirmas" runat="server" />
    <asp:ModalPopupExtender ID="mpeTarjetaFirmas" runat="server" Enabled="True"
        BackgroundCssClass="backgroundColor" PopupControlID="panelFirmas" TargetControlID="hfTarjetaFirmas"
        CancelControlID="btnCloseFirmas">
    </asp:ModalPopupExtender>
    <asp:ResizableControlExtender ID="rceFirmas" runat="server" TargetControlID="panelFirmas"
        OnClientResizing="rceFirmaResize" HandleCssClass="handle" ResizableCssClass="resizing" MinimumHeight="580"
        MinimumWidth="420" HandleOffsetY="20"></asp:ResizableControlExtender>
    <asp:DragPanelExtender ID="dpeFirmas" runat="server" Enabled="True" TargetControlID="panelFirmas"
        DragHandleID="panelFirmas"></asp:DragPanelExtender>
    <asp:Panel ID="panelFirmas" runat="server" Height="580px" Width="420px" Style="border: solid 2px Gray"
        BackColor="White">
        <div style="border-style: none; border-width: medium; background-color: #3399FF; cursor: move">
            TARJETA DE FIRMAS
        </div>
        <br />
        <asp:Button ID="btnCloseFirmas" runat="server" Text="Cerrar" CssClass="button" OnClick="btnCloseFirmas_Click"
            CausesValidation="false" Height="20px" />
        &nbsp;&nbsp;&nbsp;
        <br />
        <asp:Image ID="imgTarjetaFirmas" runat="server" Height="90%" Width="100%" />
    </asp:Panel>

    <style type="text/css">
        .handle {
            width: 20px;
            height: 20px;
            background-color: lightseagreen;
            overflow: hidden;
            cursor: se-resize;
            background: url(../../../Images/Lupa.jpg) no-repeat;
        }

        .resizing {
            padding: 0px;
            border-style: solid;
            border-width: 1px;
            border-color: #7391BA;
            top: 400px;
        }

        .dragpanel {
            background-color: #FFC0FF;
            height: 200px;
            width: 400px;
            border-bottom-color: black;
        }
    </style>

</asp:Content>
