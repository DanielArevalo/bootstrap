<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/ctlBusquedaRapida.ascx" TagName="ListadoPersonas" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc2" %>
<%@ Register Src="~/General/Controles/decimalesGridRow.ascx" TagName="decimalesGridRow" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/ctlProcesoContable.ascx" TagName="procesoContable" TagPrefix="uc2" %>

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
    <asp:Panel ID="panelGeneral" runat="server">
        <asp:Panel ID="panelEncabezado" runat="server">
            <table style="width: 85%;" cellpadding="0" cellspacing="0">
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
                            Width="100px" ></asp:TextBox>
                        <asp:CalendarExtender ID="ceFechaCont" runat="server"
                            DaysModeTitleFormat="dd/MM/yyyy" Enabled="True" Format="dd/MM/yyyy"
                            TargetControlID="txtFechaCont" TodaysDateFormat="dd/MM/yyyy">
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
                    <td style="text-align: left">Nombre</td>
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
                    <td style="text-align: right"></td>
                    <td style="text-align: left"></td>
                    <td style="text-align: left">&nbsp;</td>
                </tr>
            </table>
            <table width="80%">
                <tr>
                    <td>
                        <div id="divDatos" runat="server" style="overflow: scroll; height: 120px">
                            <br />
                            <asp:GridView ID="gvCdat" runat="server" Width="99%" AutoGenerateColumns="False"
                                AllowPaging="false" PageSize="5" GridLines="Horizontal" ShowHeaderWhenEmpty="True"
                                Style="font-size: x-small" DataKeyNames="codigo_cdat" OnRowEditing="gvCdat_RowEditing">
                                 <Columns>
                                            <asp:CommandField ButtonType="Image" EditImageUrl="~/Images/gr_info.jpg" ShowEditButton="True" />
                                    <asp:BoundField DataField="codigo_cdat" HeaderText="Código Cdat">
                                        <ItemStyle HorizontalAlign="center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="numero_cdat" HeaderText="Número cdat">
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
                                        </Columns>  <HeaderStyle CssClass="gridHeader"></HeaderStyle>
                                <PagerStyle CssClass="gridPager"></PagerStyle>
                                <RowStyle CssClass="gridItem"></RowStyle>
                            </asp:GridView>
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
                                                ToolTip="Estado Cuenta" CommandName='<%#Eval("numero_radicacion")%>' />
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
                <table style="width: 100%;" cellpadding="0" cellspacing="0">
                    <tr>
                        <td style="font-size: small; text-align: left" colspan="7">
                            <strong>Datos de la Transacción</strong></td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 25px; font-size: x-small;">&nbsp;</td>
                        <td style="width: 140px; font-size: x-small;">Número de Producto</td>
                        <td style="width: 140px; font-size: x-small;">Tipo de Pago</td>
                        <td style="width: 160px; font-size: x-small;">
                            <table>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblNumeroCuotas" runat="server" Text="#Cuo" Visible="false" /></td>
                                    <td>
                                        <asp:Label ID="lblTipoValorTransaccion" runat="server" Text="Valor de la Transacción"></asp:Label>
                                </tr>
                            </table>
                        </td>
                        <td style="width: 160px; font-size: x-small;">Moneda</td>
                        <td style="width: 15px">&nbsp;</td>
                        <td style="width: 35px">&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 25px">&nbsp;</td>
                        <td style="width: 144px">
                            <asp:TextBox ID="txtNumProducto" runat="server" CssClass="textbox"
                                Width="134px" MaxLength="12" AutoPostBack="True"
                                OnTextChanged="txtNumProducto_TextChanged"></asp:TextBox>
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
                                        <uc1:decimales ID="txtValTransac" runat="server" CssClass="textbox" Width="150px"
                                            MaxLength="17" style="text-align: right"></uc1:decimales>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="width: 163px">
                            <asp:DropDownList ID="ddlMonedas" runat="server" CssClass="textbox"
                                Width="138px">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 12px">&nbsp;</td>
                        <td style="width: 35px">
                            <asp:Button ID="btnGoTran" runat="server" OnClick="btnGoTran_Click"
                                Text="&gt;&gt;" />
                        </td>
                        <td>&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblMsgNroProducto" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                        <td style="width: 12px">&nbsp;</td>
                        <td style="width: 35px">&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                </table>
                <%--<asp:UpdatePanel ID="upDevoluciones" runat="server">
                    <ContentTemplate>--%>
                <table style="width: 100%;">
                    <tr>
                        <td colspan="2" style="text-align: left">Observaciones&nbsp;
                                    <asp:TextBox ID="txtObservacion" runat="server" CssClass="textbox" Width="600px" MaxLength="600"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: left">
                            <strong>Transacciones a Aplicar</strong></td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:GridView ID="gvTransacciones" runat="server" AllowPaging="False" AutoGenerateColumns="False"
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
                                            <asp:ImageButton ID="btnDistPagos" runat="server"
                                                CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                                CommandName="DetallePago" ImageUrl="~/Images/gr_info.jpg"
                                                ToolTip="Dist Pagos" />
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="gridIco"></HeaderStyle>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="moneda" HeaderStyle-CssClass="gridColNo"
                                        ItemStyle-CssClass="gridColNo">
                                        <HeaderStyle CssClass="gridColNo" />
                                        <ItemStyle CssClass="gridColNo" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="tipo" HeaderStyle-CssClass="gridColNo"
                                        ItemStyle-CssClass="gridColNo">
                                        <HeaderStyle CssClass="gridColNo" />
                                        <ItemStyle CssClass="gridColNo" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="tproducto" HeaderStyle-CssClass="gridColNo"
                                        ItemStyle-CssClass="gridColNo">
                                        <HeaderStyle CssClass="gridColNo" />
                                        <ItemStyle CssClass="gridColNo" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="tipomov" HeaderStyle-CssClass="gridColNo"
                                        ItemStyle-CssClass="gridColNo">
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
                        <td style="text-align: left; width: 150px">Valor Total
                        </td>
                        <td style="text-align: left;">
                            <asp:TextBox ID="txtValorTran" runat="server" CssClass="textbox"
                                Enabled="false" Width="120px" Style="text-align: right"></asp:TextBox>
                            <asp:MaskedEditExtender ID="MaskedEditExtender2" runat="server" TargetControlID="txtValorTran" Mask="999,999,999,999" MessageValidatorTip="true"
                                OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="Number" InputDirection="RightToLeft"
                                AcceptNegative="Left" DisplayMoney="Left" ErrorTooltipEnabled="True">
                            </asp:MaskedEditExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left;" colspan="2">
                            <asp:Panel ID="pnlFormaPago" runat="server">
                                <strong style="text-align: left"><span>Número Cuenta<br />
                                    <asp:TextBox ID="txtNumCuenta" runat="server" CssClass="textbox" Width="100px"
                                        OnTextChanged="txtNumCuenta_TextChanged" AutoPostBack="true"></asp:TextBox>
                                </span></strong>
                                &nbsp;<asp:GridView ID="gvAhorros" runat="server" Width="80%"
                                    GridLines="Horizontal" AutoGenerateColumns="False"
                                    AllowPaging="True" PageSize="20" DataKeyNames="numero_cuenta"
                                    Font-Size="Small">
                                    <Columns>
                                        <%--<asp:CommandField ButtonType="Image" EditImageUrl="~/Images/gr_edit.jpg" ShowEditButton="True" />--%>
                                        <asp:BoundField DataField="num_devolucion" HeaderText="Código" Visible="False">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="numero_cuenta" HeaderText="Cuenta">
                                            <ItemStyle HorizontalAlign="Left" Width="120px" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="saldo_total" HeaderText="Saldo"
                                            DataFormatString="{0:N2}">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" Width="120px" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Valor a Aplicar">
                                            <ItemTemplate>
                                                <uc1:decimalesGridRow ID="txtValorAplicar" runat="server" CssClass="textbox" Width_="80"
                                                    MaxLength="12" AutoPostBack_="True" TipoLetra="X-Small"></uc1:decimalesGridRow>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" Width="120px" />
                                            <HeaderStyle HorizontalAlign="Center" CssClass="gridIco" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <HeaderStyle CssClass="gridHeader"></HeaderStyle>
                                    <PagerStyle CssClass="gridPager"></PagerStyle>
                                    <RowStyle CssClass="gridItem"></RowStyle>
                                </asp:GridView>
                                <table style="width: 80%;" cellpadding="2" cellspacing="0">
                                    <tr>
                                        <td style="text-align: left; width: 150px">Valor Aplicar
                                        </td>
                                        <td style="text-align: left; width: 120px">
                                            <asp:TextBox ID="txtValTotalAplicar" runat="server" CssClass="textbox" Enabled="false" Width="120px" Style="text-align: right"></asp:TextBox>
                                            <asp:MaskedEditExtender ID="meeTotalDevol" runat="server" TargetControlID="txtValTotalAplicar" Mask="999,999,999,999,999" MessageValidatorTip="true"
                                                OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="Number" InputDirection="RightToLeft"
                                                AcceptNegative="Left" DisplayMoney="Left" ErrorTooltipEnabled="True">
                                            </asp:MaskedEditExtender>
                                        </td>
                                        <td style="text-align: left;"></td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>

                <%-- </ContentTemplate>
                  </asp:UpdatePanel>--%>
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
        <asp:HiddenField ID="HiddenField1" runat="server" />
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
        <asp:Panel ID="PanelDetallePago" runat="server" Width="480px" Style="display: none; border: solid 2px Gray;" CssClass="modalPopup">
            <asp:UpdatePanel ID="upDetallePago" runat="server">
                <ContentTemplate>
                    <table>
                        <tr>
                            <td style="font-size: x-small: color: #FFFFFF; background-color: #0066FF; width: 434px">
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
        <asp:Panel ID="PanelDetallePagoAportes" runat="server" Width="480px" Style="display: none; border: solid 2px Gray;" CssClass="modalPopup">
            <asp:UpdatePanel ID="UpDetallePagoApo" runat="server">
                <ContentTemplate>
                    <table>
                        <tr>
                            <td style="font-size: x-small: color: #FFFFFF; background-color: #0066FF; width: 434px">
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
    </asp:Panel>
    <asp:Panel ID="panelProceso" runat="server" Width="100%">
        <uc2:procesoContable ID="ctlproceso" runat="server" />
    </asp:Panel>

</asp:Content>
