<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Lista.aspx.cs" Inherits="Lista" Title=".: Xpinn - Crédito Educativo :." %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Src="~/General/Controles/ctlSeleccionarPersona.ascx" TagName="ListadoPersonas"
    TagPrefix="uc1" %>
<%@ Import Namespace="Xpinn.FabricaCreditos.Entities" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <script src="../../../../Scripts/PCLBryan.js"></script>
    <script type="text/javascript">
        var upload = '<%= avatarUpload.ClientID  %>';
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
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:MultiView ID="mvPrincipal" ActiveViewIndex="0" runat="server">
        <asp:View runat="server">
            <table style="width: 100%; margin-bottom: 0px;">
                <tr>
                    <td>
                        <br />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left;">
                        <b>Tipo de Solicitud de Crédito</b>
                    </td>
                </tr>
                <tr style="text-align: left;">
                    <td align="left" style="display: block; height: 31px;">
                        <asp:RadioButtonList ID="rblTipoCredito" RepeatDirection="Horizontal" runat="server" Style="text-align: left">
                            <asp:ListItem Value="1">Consumo</asp:ListItem>
                            <asp:ListItem Value="4">Microcrédito</asp:ListItem>
                            <asp:ListItem Value="3">Vivienda</asp:ListItem>
                            <asp:ListItem Value="2">Comercial</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left">
                        <strong style="text-align: left">Seleccionar el Solicitante del Crédito</strong>
                        <asp:Panel ID="pBusqueda" runat="server" Height="70px">
                            <table cellpadding="0" cellspacing="0" style="width: 100%">
                                <tr>
                                    <td style="text-align: left;">
                                        <uc1:ListadoPersonas ID="ctlBusquedaPersonas" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
        </asp:View>
        <asp:View ID="vwImportacion" runat="server">
            <br />
            <table cellpadding="2">
                <tr>
                    <td style="text-align: left;" colspan="4">
                        <strong>Criterios de carga</strong>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left">Fecha de Carga<br />
                        <ucFecha:fecha ID="ucFecha" runat="server" Enabled="true" />
                    </td>
                    <td style="text-align: left">Formato de fecha<br />
                        <asp:DropDownList ID="ddlFormatoFecha" runat="server" CssClass="textbox" Width="160px">
                            <asp:ListItem Text="dd/MM/yyyy" Value="dd/MM/yyyy" />
                            <asp:ListItem Text="yyyy/MM/dd" Value="yyyy/MM/dd" />
                            <asp:ListItem Text="MM/dd/yyyy" Value="MM/dd/yyyy" />
                        </asp:DropDownList>
                    </td>
                    <td style="text-align: left">
                        <input id="avatarUpload" type="file" name="file" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td colspan="4" style="text-align: left">
                        <strong>Separador del archivo : </strong>&nbsp;&nbsp;&nbsp;|
                        <table width="100%">
                            <tr>
                                <td style="text-align: left; font-size: x-small">
                                    <strong>Estructura de archivo : SI = OBLIGATORIO , NO = NO OBLIGATORIO (Usar | |) </strong>
                                    <br />
                                    (CREDITO)<br />
                                    NUMERO IDENTIFICACION ASOCIADO, (Alfanumerico - SI), NUMERO RADICACION (Numerico - SI) 
                                    COD OFICINA (Numerico - SI), CODIGO LINEA CREDITO (Numerico - SI), NOMBRE LINEA CREDITO (Alfanumerico - SI),
                                    MONTO SOLICITADO (Numerico - NO), MONTO APROBADO (Numerico - SI), MONTO DESEMBOLSADO (Numerico - SI),
                                    MONEDA (Numerico - SI), FECHA SOLICITUD (Fecha - NO), FECHA APROBACION (Fecha - NO),
                                    FECHA DESEMBOLSO (Fecha - SI), FECHA PRIMERPAGO (Fecha - SI), NUMERO CUOTAS (Numerico - SI),
                                    CUOTAS PAGADAS (Numerico - SI), CUOTAS PENDIENTES (Numerico - SI), PERIODICIDAD (Alfanumerico - SI),
                                    TIPO LIQUIDACION (Alfanumerico - SI), VALOR CUOTA (Numerico - SI), FORMA PAGO (Alfanumerico - SI),
                                    FECHA ULTIMO PAGO (Fecha - SI), FECHA VENCIMIENTO (Fecha - SI), FECHA PROXIMO PAGO (Fecha - SI),
                                    TIPO GRACIA (Alfanumerico - NO), PERIODO GRACIA (Numerico - NO), CLASIFICACION (Numerico - SI), SALDO CAPITAL (Numerico - SI),
                                    OTROS SALDOS (Numerico - NO), ASESOR COMERCIAL (Numerico - NO), TIPO CREDITO (Alfanumerico - SI),
                                    NUMERO CREDITO ORIGEN (Numerico - SI), VALOR REESTRUCTURADO (Numerico - NO), EMPRESA PAGADURIA (Alfanumerico - SI),
                                    COD PAGADURIA (Numerico - SI), GRADIENTE (Numerico - NO), FECHA INICIO (Fecha) - SI), DIAS AJUSTE (Numerico - SI),
                                    ESTADO (Alfanumerico - SI), TIPO DE GARANTIA (Alfanumerico - SI), SALDO CAPITAL VENCIDO A LA FECHA DE MIGRACION (Numerico - SI),
                                    SALDO DE INTERES CORRIENTE VENCIDO A LA FECHA DE MIGRACION (Numerico - SI)
                                    <br />
                                    <br />

                                    (DOCUMENTOS GARANTIAS)  
                                    <br />
                                    NUMERO PAGARE (Alfanumerico - SI)
                                    <br />
                                    <br />

                                    (CODEUDORES)<br />
                                    IDENTIFICACION DEL CODEUDOR 1 (Numerico - NO), IDENTIFICACION DEL CODEUDOR 2 (Numerico - NO), IDENTIFICACION DEL CODEUDOR 3 (Numerico - NO)
                                    <br />
                                    <br />

                                    (ATRIBUTOS CREDITO)<br />
                                    TIPO CALCULO DE TASA (Alfanumerico - SI), TASA (Numerico - SI), TIPO DE TASA (Alfanumerico - SI), TIPO PAGO INTERES (Alfanumerico - SI),
                                    COBRAN SEGURO EN LA CUOTA (Alfanumerico - SI), VALOR SEGUDO DE VIDA MENSUAL (Numerico - SI), VALOR SEGURO DE CARTERA (Numerico - SI),
                                    TIPO PAGO SEGURO CARTERA (Alfanumerico  - SI), VALOR SEGURO CARTERA COBRADO EN CUOTA (Numerico - SI),
                                    VALOR FIJO OTROS CONCEPTOS COBRADOS EN LA CUOTA (Numerico - SI), TASA INTERES MORA (Numerico - SI)
                                    <br />
                                    <br />

                                    (CUOTAS EXTRAS)<br />
                                    FECHA PROXIMO PAGO CUOTA EXTRAORDINARIA (Fecha - NO) , VALOR DE CUOTA EXTRAORDINARIA (Numerico - NO),
                                    NUMERO DE CUOTAS EXTRAORDINARIAS PACTADAS (Numerico - NO), PERIODICIDAD PAGO CUOTAS EXTRAORDINARIAS (Numerico - NO)
                                    NUMERO DE CUOTAS EXTRAORDINARIAS X PAGAR (Alfanumerico - NO)
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left" colspan="4">
                        <asp:Button ID="btnCargarPersonas" runat="server" CssClass="btn8" OnClientClick="return TestInputFileToImportData(upload);" OnClick="btnCargarPersonas_Click"
                            Height="22px" Text="Cargar Personas" Width="150px" />
                    </td>
                </tr>
            </table>
            <hr style="width: 100%" />
            <table cellpadding="2" width="100%">
                <tr>
                    <td>
                        <asp:Panel ID="pErrores" runat="server">
                            <asp:Panel ID="pEncBusqueda1" runat="server" CssClass="collapsePanelHeader" Height="30px">
                                <div style="padding: 5px; cursor: pointer; vertical-align: middle;">
                                    <div style="float: left; margin-left: 5px; color: #0066FF; font-size: small">
                                        <asp:Label ID="lblMostrarDetalles1" runat="server" />
                                        <asp:ImageButton ID="imgExpand1" runat="server" ImageUrl="~/Images/expand.jpg" />
                                    </div>
                                </div>
                            </asp:Panel>
                            <asp:Panel ID="pConsultaAfiliacion" runat="server" Width="100%">
                                <div style="border-style: none; border-width: medium; background-color: #f5f5f5">
                                    <asp:GridView ID="gvErrores" runat="server" Width="100%" ShowHeaderWhenEmpty="True"
                                        AutoGenerateColumns="False" SelectedRowStyle-Font-Size="XX-Small" Style="font-size: x-small; margin-bottom: 0px;">
                                        <Columns>
                                            <asp:BoundField DataField="numero_registro" HeaderText="No." ItemStyle-Width="50"
                                                ItemStyle-HorizontalAlign="Left" />
                                            <asp:BoundField DataField="datos" HeaderText="Datos" ItemStyle-Width="200" ItemStyle-HorizontalAlign="Left" />
                                            <asp:BoundField DataField="error" HeaderText="Error" ItemStyle-Width="200" ItemStyle-HorizontalAlign="Left" />
                                        </Columns>
                                        <HeaderStyle CssClass="gridHeader" />
                                        <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                                        <RowStyle CssClass="gridItem" />
                                        <SelectedRowStyle Font-Size="XX-Small"></SelectedRowStyle>
                                    </asp:GridView>
                                </div>
                            </asp:Panel>
                            <asp:CollapsiblePanelExtender ID="cpeDemo1" runat="Server" CollapseControlID="pEncBusqueda1"
                                Collapsed="True" CollapsedImage="~/Images/expand.jpg" CollapsedText="(Click Aqui para Mostrar Detalles...)"
                                ExpandControlID="pEncBusqueda1" ExpandedImage="~/Images/collapse.jpg" ExpandedText="(Click Aqui para Ocultar Detalles...)"
                                ImageControlID="imgExpand1" SkinID="CollapsiblePanelDemo" SuppressPostBack="true"
                                TargetControlID="pConsultaAfiliacion" TextLabelID="lblMostrarDetalles1" />
                            <br />
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Panel ID="pnlNotificacion" runat="server" Width="100%">
                            <table style="width: 100%;">
                                <tr>
                                    <td style="text-align: center; font-size: large;">
                                        <br />
                                        <br />
                                        <br />
                                        <br />
                                        <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: center; font-size: large;">Importación de datos generada correctamente
                                    <br />
                                        Revisa la tabla de errores en caso de haber registros que no se guardaron correctamente!.
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Panel ID="panelCreditos" runat="server">
                            <div style="overflow: scroll; max-height: 550px; width: 100%">
                                <asp:GridView ID="gvDatos" runat="server" AutoGenerateColumns="False" GridLines="Horizontal"
                                    ShowHeaderWhenEmpty="True" OnRowDeleting="gvDatos_RowDeleting" DataKeyNames="identificacion"
                                    Style="font-size: x-small">
                                    <Columns>
                                        <asp:TemplateField HeaderStyle-CssClass="gridIco">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnEliminar" runat="server" CommandName="Delete" ImageUrl="~/Images/gr_elim.jpg"
                                                    ToolTip="Eliminar" Width="16px" />
                                            </ItemTemplate>
                                            <HeaderStyle CssClass="gridIco"></HeaderStyle>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="identificacion" HeaderText="Identificación">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="cod_deudor" HeaderText="Codigo deudor">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="numero_radicacion" HeaderText="Num.Radicación">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="codigo_oficina" HeaderText="Cod.Oficina">
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="cod_linea_credito" HeaderText="Cod.Linea">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <%--                                        <asp:BoundField DataField="nom_linea_credito" HeaderText="Linea">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>--%>
                                        <asp:BoundField DataField="monto_solicitado" HeaderText="Monto Solicitado" DataFormatString="${0:#,##0.00}">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="monto_aprobado" HeaderText="Monto Aprobado" DataFormatString="${0:#,##0.00}">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="monto_desembolsado" HeaderText="Monto Desembolsado" DataFormatString="${0:#,##0.00}">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="cod_moneda" HeaderText="Cod.Moneda">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="fecha_solicitud" HeaderText="Fecha Solicitud" DataFormatString="{0:d}">
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="fecha_aprobacion" HeaderText="Fecha Aprobación" DataFormatString="{0:d}">
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="fecha_desembolso" HeaderText="Fecha Desembolso" DataFormatString="{0:d}">
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="fecha_prim_pago" HeaderText="Fecha Primer Pago" DataFormatString="{0:d}">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="numero_cuotas" HeaderText="Numero Cuotas">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="cuotas_pagadas" HeaderText="Cuotas Pagadas">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="cuotas_pendientes" HeaderText="Cuotas Pendientes">
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="periodicidad" HeaderText="Periodicidad">
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="tipo_liquidacion" HeaderText="Tipo Liquidación">
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="valor_cuota" HeaderText="Valor Cuota" DataFormatString="${0:#,##0.00}">
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="forma_pago" HeaderText="Forma Pago">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="fecha_ultimo_pago" HeaderText="Fecha Ult. Pago" DataFormatString="{0:d}">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="fecha_vencimiento" HeaderText="Fecha Vencimiento" DataFormatString="{0:d}">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="fecha_prox_pago" HeaderText="Fecha Prox. Pago" DataFormatString="{0:d}">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="tipo_gracia" HeaderText="Tipo Gracia">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="periodo_gracia" HeaderText="Periodo Gracia">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="cod_clasifica" HeaderText="Cod.Clasifi">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="saldo_capital" HeaderText="Saldo Capital" DataFormatString="${0:#,##0.00}">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="otros_saldos" HeaderText="Otros Saldos" DataFormatString="${0:#,##0.00}">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CodigoAsesor" HeaderText="Cod.Asesor">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="tipo_credito" HeaderText="Tipo Credito">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="num_radic_origen" HeaderText="Num.Radi.Origen">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="valor_para_refinanciar" HeaderText="Valor Reestruc.">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <%--                                        <asp:BoundField DataField="nom_pagaduria" HeaderText="Pagaduría">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>--%>
                                        <asp:BoundField DataField="cod_pagaduria" HeaderText="Cod.Pagaduría">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="gradiente" HeaderText="Gradiente">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="fecha_inicio" HeaderText="Fecha Inicio">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="dias_ajuste" HeaderText="Dias Ajuste">
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="estado" HeaderText="Estado">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <%--                                    <asp:BoundField DataField="nom_linea_credito" HeaderText="Linea">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="monto_solicitado" HeaderText="Monto Solicitado" DataFormatString="${0:#,##0.00}">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="monto_aprobado" HeaderText="Monto Aprobado" DataFormatString="${0:#,##0.00}">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>--%>
                                        <asp:TemplateField HeaderText="Numero Pagaré">
                                            <ItemTemplate>
                                                <%# ((Credito)Container.DataItem).Documento_Garantia.referencia %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Iden. Codeudor 1">
                                            <ItemTemplate>
                                                <%# ((Credito)Container.DataItem).Codeudor1.identificacion %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cod. Codeudor 1">
                                            <ItemTemplate>
                                                <%# ((Credito)Container.DataItem).Codeudor1.codpersona %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Iden. Codeudor 2">
                                            <ItemTemplate>
                                                <%# ((Credito)Container.DataItem).Codeudor2.identificacion %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cod. Codeudor 2">
                                            <ItemTemplate>
                                                <%# ((Credito)Container.DataItem).Codeudor2.codpersona %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Iden. Codeudor 3">
                                            <ItemTemplate>
                                                <%# ((Credito)Container.DataItem).Codeudor3.identificacion %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cod. Codeudor 3">
                                            <ItemTemplate>
                                                <%# ((Credito)Container.DataItem).Codeudor3.codpersona %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cod. Atr.1">
                                            <ItemTemplate>
                                                <%# ((Credito)Container.DataItem).AtributosCredito1.cod_atr %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Tasa">
                                            <ItemTemplate>
                                                <%# ((Credito)Container.DataItem).AtributosCredito1.tasa %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cod. Atr.2">
                                            <ItemTemplate>
                                                <%# ((Credito)Container.DataItem).AtributosCredito2.cod_atr %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Valor Seguro de Vida Mensual">
                                            <ItemTemplate>
                                                <%# ((Credito)Container.DataItem).AtributosCredito2.tasa %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cod. Atr.3">
                                            <ItemTemplate>
                                                <%# ((Credito)Container.DataItem).AtributosCredito3.cod_atr %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Valor Seguro de Cartera">
                                            <ItemTemplate>
                                                <%# ((Credito)Container.DataItem).AtributosCredito3.tasa %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cod. Atr.4">
                                            <ItemTemplate>
                                                <%# ((Credito)Container.DataItem).AtributosCredito4.cod_atr %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Tasa Interes Mora">
                                            <ItemTemplate>
                                                <%# ((Credito)Container.DataItem).AtributosCredito4.tasa %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <HeaderStyle CssClass="gridHeader" />
                                    <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                                    <RowStyle CssClass="gridItem" />
                                </asp:GridView>
                            </div>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
        </asp:View>
    </asp:MultiView>

    <uc4:mensajegrabar ID="ctlMensaje" runat="server" />
</asp:Content>
