<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="nuevo.aspx.cs" Inherits="Nuevo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/ctlBusquedaRapida.ascx" TagName="ListadoPersonas" TagPrefix="uc5" %>
<%@ Register Src="~/General/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc2" %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="uc3" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <script type="text/javascript">
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
    <br />

    <asp:Panel ID="panelEncabezado" runat="server">
        <table style="width: 85%;">
            <tr>
                <td style="text-align: left;" colspan="8">
                    <asp:Label ID="Lblerror" runat="server" Style="color: #FF0000; font-weight: 700"
                        Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 60px; text-align: left;">
                    <span style="font-size: x-small">Oficina</span>
                </td>
                <td style="width: 180px; text-align: left;">
                    <asp:TextBox ID="txtOficina" runat="server" CssClass="textbox" Enabled="False" Width="151px"></asp:TextBox>
                </td>
                <td style="font-size: x-small; width: 140px; text-align: left">
                    Fecha y Hora de Transacción
                </td>
                <td style="font-size: x-small; width: 120px; text-align: left">
                    <asp:TextBox ID="txtFechaReal" runat="server" CssClass="textbox" Enabled="false"
                        MaxLength="10" Width="120px"></asp:TextBox>
                </td>
                <td style="font-size: x-small; width: 100px; text-align: right">
                    Fecha Nota
                </td>
                <td style="font-size: x-small; width: 100px; text-align: left">
                    <uc3:fecha ID="txtFechaNota" runat="server" />
                </td>
            </tr>
        </table>
        <hr />
        <table style="width: 80%;">
            <tr>
                <td style="text-align: left" colspan="2">
                    <strong>Datos del Cliente</strong>&nbsp;
                </td>
                <td style="text-align: right">
                    &nbsp;
                </td>
                <td style="text-align: left">
                    &nbsp;
                </td>
                <td style="text-align: left">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td style="text-align: left">
                    Tipo Identificación
                </td>
                <td style="text-align: left">
                    <asp:DropDownList ID="ddlTipoIdentificacion" runat="server" CssClass="textbox" Width="180px">
                    </asp:DropDownList>
                </td>
                <td style="text-align: right">
                    Identificación
                </td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtIdentificacion" runat="server" CssClass="textbox" MaxLength="12"
                        Width="124px"></asp:TextBox>
                    <asp:Button ID="btnConsultaPersonas" CssClass="btn8" runat="server" Text="..." Height="26px"
                        OnClick="btnConsultaPersonas_Click" />
                    <uc5:ListadoPersonas ID="ctlBusquedaPersonas" runat="server" />
                </td>
                <td style="text-align: left">
                    <asp:Button ID="btnConsultar" runat="server" OnClick="btnConsultar_Click" Text="Consultar"
                        ValidationGroup="vgGuardar" />
                </td>
            </tr>
            <tr>
                <td style="text-align: left">
                    Nombres y Apellidos
                </td>
                <td colspan="4" style="text-align: left">
                    <asp:TextBox ID="txtNombreCliente" runat="server" CssClass="textbox" Enabled="false"
                        Width="582px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="text-align: left">
                    Tipo de Producto
                </td>
                <td style="text-align: left">
                    <asp:DropDownList ID="ddlTipoProducto" runat="server" AutoPostBack="True" CssClass="textbox"
                        OnSelectedIndexChanged="ddlTipoTipoProducto_SelectedIndexChanged" Width="180px">
                    </asp:DropDownList>
                </td>
                <td style="text-align: right">
                </td>
                <td style="text-align: left">
                </td>
                <td style="text-align: left">
                    &nbsp;
                </td>
            </tr>
        </table>
        <table width="80%">
            <tr>
                <td>
                    <div id="divDatos" runat="server" style="overflow: scroll; height: 200px">
                        <asp:GridView ID="gvConsultaDatos" runat="server" Width="99%" AutoGenerateColumns="False"
                            AllowPaging="false" PageSize="5" GridLines="Horizontal" ShowHeaderWhenEmpty="True"
                             DataKeyNames="numero_radicacion,valor_cuota" OnRowEditing="gvGridViews_RowEditing" Style="font-size: x-small">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:CommandField ButtonType="Image" EditImageUrl="~/Images/gr_info.jpg" ShowEditButton="True" />
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
                                <asp:BoundField DataField="fecha_proxima_pago" HeaderText="Fec. Próx.Pago" DataFormatString="{0:dd/MM/yyyy}" />
                                <asp:BoundField DataField="valor_a_pagar" HeaderText="Valor a Pagar" DataFormatString="{0:n0}">
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="total_a_pagar" HeaderText="Valor Total a Pagar" DataFormatString="{0:n0}">
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
                        <asp:GridView ID="gvDatosAfiliacion" runat="server" Width="99%" AutoGenerateColumns="False"
                            PageSize="5" GridLines="Horizontal" ShowHeaderWhenEmpty="True" DataKeyNames="idafiliacion"
                            Style="font-size: x-small" OnRowEditing="gvGridViews_RowEditing">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:CommandField ButtonType="Image" EditImageUrl="~/Images/gr_info.jpg" ShowEditButton="True" />
                                <asp:BoundField DataField="idafiliacion" HeaderText="Código" />
                                <asp:BoundField DataField="fecha_afiliacion" HeaderText="Fecha Afiliación" DataFormatString="{0:d}" />
                                <asp:BoundField DataField="valor" HeaderText="Valor Afiliación" DataFormatString="{0:n}">
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="descripcion" HeaderText="Periodicidad" />
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
                    style="font-size: x-small" DataKeyNames="numero_cuenta,valor_cuota" OnRowEditing="gvGridViews_RowEditing" >  
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
                            <asp:BoundField DataField="saldo_total" HeaderText="Saldo Total"  DataFormatString="{0:n0}">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="saldo_canje" HeaderText="Saldo Canje"  DataFormatString="{0:n0}">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="valor_cuota" HeaderText="Vr. Cuota"  DataFormatString="{0:n0}">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                        </Columns>
                        <HeaderStyle CssClass="gridHeader"></HeaderStyle>
                        <PagerStyle CssClass="gridPager"></PagerStyle>
                        <RowStyle CssClass="gridItem"></RowStyle>
                    </asp:GridView>

                    <%--SERVICIOS--%>
                    <asp:GridView ID="gvServicios" runat="server" Width="100%" GridLines="Horizontal" AutoGenerateColumns="False"
                        AllowPaging="false" OnRowEditing="gvGridViews_RowEditing"
                        PageSize="20" HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager"
                        RowStyle-CssClass="gridItem" DataKeyNames="numero_servicio" style="font-size: x-small">
                        <Columns>
                            <asp:CommandField ButtonType="Image" EditImageUrl="~/Images/gr_edit.jpg" ShowEditButton="True" />                           
                            <asp:BoundField DataField="numero_servicio" HeaderText="Num. Servicio">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="fecha_solicitud" HeaderText="Fec. Solicitud" DataFormatString="{0:d}">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="nom_linea" HeaderText="Linea">
                                <ItemStyle HorizontalAlign="Right" />
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
                            <asp:BoundField DataField="valor_total" HeaderText="Valor" DataFormatString="{0:n0}" />
                        </Columns>
                        <HeaderStyle CssClass="gridHeader"></HeaderStyle>
                        <PagerStyle CssClass="gridPager"></PagerStyle>
                        <RowStyle CssClass="gridItem"></RowStyle>
                    </asp:GridView> 

                <%--AHORRO PROGRAMADO--%>
                <asp:GridView ID="gvProgramado" runat="server" Width="99%" AutoGenerateColumns="False"
                    AllowPaging="false" PageSize="5" GridLines="Horizontal" ShowHeaderWhenEmpty="True"
                    style="font-size: x-small" DataKeyNames="numero_programado,valor_cuota" OnRowEditing="gvGridViews_RowEditing" > 
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
                            <asp:BoundField DataField="saldo" HeaderText="Saldo Total"  DataFormatString="{0:n0}">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="valor_cuota" HeaderText="Cuota"  DataFormatString="{0:n0}">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                             <asp:BoundField DataField="valor_total" HeaderText="Valor Total a Pagar"  DataFormatString="{0:n0}">
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
                        Style="font-size: x-small" DataKeyNames="codigo_cdat">
                        <Columns>
                            <asp:BoundField DataField="codigo_cdat" HeaderText="Num Producto">
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
                        </Columns>
                       <HeaderStyle CssClass="gridHeader"></HeaderStyle>
                        <PagerStyle CssClass="gridPager"></PagerStyle>
                        <RowStyle CssClass="gridItem"></RowStyle>
                    </asp:GridView>
                    </div>
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
        </table>
        <hr />
    </asp:Panel>
    <asp:MultiView ID="mvOperacion" runat="server">
        <asp:View ID="vwOperacion" runat="server">
            <asp:UpdatePanel ID="updOperacion" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <table cellpadding="0" cellspacing="2">
                        <tr>
                            <td style="font-size: small; text-align: left" colspan="3">
                                <strong>Datos del Cambio</strong>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 140px; text-align: left">
                                Número Producto
                                <br />
                                <asp:TextBox ID="txtNumProd" runat="server" CssClass="textbox" Width="90%" MaxLength="15" AutoPostBack="true" OnTextChanged="txtNumProd_TextChanged"></asp:TextBox>
                            </td>
                            <td style="width: 240px; text-align: left">
                                Forma de Pago<br />
                                <asp:DropDownList ID="ddlFormaPago" runat="server" AutoPostBack="True" CssClass="textbox"
                                    OnSelectedIndexChanged="ddlFormaPago_SelectedIndexChanged" Width="95%">
                                    <asp:ListItem Value="1">Caja</asp:ListItem>
                                    <asp:ListItem Value="2">Nomina</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td style="width: 240px; text-align: left" runat="server" id="tdValorCuota">
                                Vr. Cuota<br />
                                <asp:TextBox ID="txtValorCuota" runat="server" CssClass="textbox" Width="90%" MaxLength="18" onkeypress="return isNumber(event)" AutoPostBack="true" OnTextChanged="txtNumProd_TextChanged"></asp:TextBox>
                                <asp:HiddenField runat="server" ID="hfAnteriorCuota"/>
                            </td>
                            <td style="width: 240px; text-align: left" runat="server" id="tdFechaDeseada">
                                Fecha deseada de modificación<br />
                                <uc3:fecha ID="txtFechaDeseada" runat="server" />
                            </td>
                            <td style="width: 300px; vertical-align: sub;text-align:left">
                                Observaciones<br />
                                <asp:TextBox ID="txtObservacion" runat="server" CssClass="textbox" TextMode="MultiLine"
                                    Width="95%" MaxLength="300" Height="50px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="text-align: left">
                                &nbsp;<asp:Label ID="lbltituloEmpresa" runat="server" Text="Empresa Recaudadora" />
                                <asp:Panel ID="PanelCredito" runat="server">
                                    <asp:GridView ID="gvEmpresaRecaudora" runat="server" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False"
                                        PageSize="15" HeaderStyle-Height="25px" BackColor="White" BorderColor="#DEDFDE"
                                        BorderStyle="None" BorderWidth="1px" CellPadding="2" ForeColor="Black" GridLines="Horizontal"
                                        Style="font-size: x-small">
                                        <AlternatingRowStyle BackColor="White" />
                                        <Columns>
                                           <asp:TemplateField HeaderText="Id">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblidcrerecaudo" runat="server" Text='<%# Bind("idempresarecaudo") %>' />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Cód.Empresa">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCodigo" runat="server" Text='<%# Bind("cod_empresa") %>' />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Nombre">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNombre" runat="server" Text='<%# Bind("nom_empresa") %>' />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" Width="200px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="%">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtPorcentaje" runat="server" Width="50px" Style="text-align: right"
                                                        CssClass="textbox" MaxLength="3" Text='<%# Bind("porcentaje") %>'/>%
                                                    <asp:FilteredTextBoxExtender ID="ftb12" runat="server" Enabled="True" FilterType="Numbers, Custom"
                                                        TargetControlID="txtPorcentaje" ValidChars="," />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="center" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <FooterStyle CssClass="gridHeader" />
                                        <HeaderStyle CssClass="gridHeader" />
                                        <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                                        <RowStyle CssClass="gridItem" />
                                    </asp:GridView>
                                    <asp:Label ID="lblInfo" runat="server" Visible="false" Text="No existe ningún dato para el producto seleccionado"/>
                                </asp:Panel>
                                <asp:Panel ID="panelAporte" runat="server">
                                    <asp:DropDownList ID="ddlEmpresaRecaudo" runat="server" CssClass="textbox" Width="230px" />
                                </asp:Panel>
                            </td>
                </ContentTemplate>
                <Triggers>
                </Triggers>
            </asp:UpdatePanel>
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
                            <asp:Button ID="btnContinuar" runat="server" Text="Continuar" OnClick="btnContinuar_Click" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </asp:View>
    </asp:MultiView>

</asp:Content>
