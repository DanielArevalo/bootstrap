<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" Title=".: Xpinn - Cuentas por Pagar :." %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>
<%@ Register Src="~/General/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="uc2" %>
<%@ Register Src="~/General/Controles/ctlBusquedaRapida.ascx" TagName="ListadoPersonas" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>
<%@ Register Src="~/General/Controles/decimalesGridRow.ascx" TagName="decimalesGridRow" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/ctlProcesoContable.ascx" TagName="procesoContable" TagPrefix="uc2" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<%@ Register Src="../../../General/Controles/ctlGiro.ascx" TagName="giro" TagPrefix="uc3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">

    <script type="text/javascript">
        function Page_Load(textbox) {
            var str = textbox.value;
            var formateado = "";
            str = str.replace(/\./g, "");
            if (str > 0) {
                str = parseInt(str);
                str = str.toString();

                if (str.length > 12) { str = str.substring(0, 12); }

                var long = str.length;
                var cen = str.substring(long - 3, long);
                var mil = str.substring(long - 6, long - 3);
                var mill = str.substring(long - 9, long - 6);
                var milmill = str.substring(0, long - 9);

                if (long > 0 && long <= 3) { formateado = parseInt(cen); }
                else if (long > 3 && long <= 6) { formateado = parseInt(mil) + "." + cen; }
                else if (long > 6 && long <= 9) { formateado = parseInt(mill) + "." + mil + "." + cen; }
                else if (long > 9 && long <= 12) { formateado = parseInt(milmill) + "." + mill + "." + mil + "." + cen; }
                else { formateado = "0"; }
            }
            else { formateado = "0"; }
            document.getElementById(textbox.id).value = formateado;
        }
    </script>

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:Panel ID="panelGeneral" runat="server">
        <asp:MultiView ID="mvCuentasxPagar" runat="server" ActiveViewIndex="0">
            <asp:View ID="vwDatos" runat="server">
                <br />
                <%--<asp:UpdatePanel ID="Panelgrilla" runat="server">
                    <ContentTemplate>--%>
                <table border="0" cellpadding="0" cellspacing="0" width="700px">
                    <tr>
                        <td style="text-align: left; width: 130px;">Código<br />
                            <asp:TextBox ID="txtCodigo" runat="server" CssClass="textbox" Width="90%" />
                            <asp:Label ID="lblCod_Ope" runat="server" Visible="false" />
                        </td>
                        <td style="text-align: left; width: 140px">
                            <asp:Label ID="lblNumFactura" runat="server" Text="Num. Factura" /><br />
                            <asp:TextBox ID="txtNumFactura" runat="server" CssClass="textbox" Width="90%" Style="text-align: right" />
                        </td>
                        <td style="text-align: left; width: 140px">Fecha Ingreso<br />
                            <uc2:fecha ID="txtFechaIngreso" runat="server" CssClass="textbox" />
                        </td>
                        <td style="text-align: left; width: 140px">
                            <asp:Label ID="lblFechaFact" runat="server" Text="Fecha Factura" /><br />
                            <uc2:fecha ID="txtFechaFact" runat="server" CssClass="textbox" />
                        </td>
                        <td style="text-align: left; width: 140px">
                            <asp:Label ID="lblFechaRadia" runat="server" Text="Fecha Radicación" /><br />
                            <uc2:fecha ID="txtFechaRadia" runat="server" CssClass="textbox" />
                        </td>
                        <td style="text-align: left; width: 140px">
                            <asp:Label ID="lblFechaVenci" runat="server" Text="F. Vencimiento" /><br />
                            <uc2:fecha ID="txtFechaVenci" runat="server" CssClass="textbox" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left" colspan="2">Tipo Cta por Pagar<br />
                            <asp:DropDownList ID="ddlTipoCuenta" runat="server" CssClass="textbox"
                                Width="230px" AutoPostBack="true"
                                OnSelectedIndexChanged="ddlTipoCuenta_SelectedIndexChanged" />
                        </td>
                        <td style="text-align: left">Doc. Equivalente<br />
                            <asp:TextBox ID="txtDocEquiva" runat="server" CssClass="textbox" Width="90%" Style="text-align: right" />
                            <asp:FilteredTextBoxExtender ID="fte1" runat="server" Enabled="True" FilterType="Numbers, Custom"
                                TargetControlID="txtDocEquiva" ValidChars="" />
                        </td>
                        <td style="text-align: left">
                            <asp:Label ID="lblNroContra" runat="server" Text="Nro. Contrato" /><br />
                            <asp:TextBox ID="txtNroContra" runat="server" CssClass="textbox" Width="90%" Style="text-align: right" />
                        </td>
                        <td style="text-align: left">
                            <asp:Label ID="lblPoliza" runat="server" Text="Poliza" /><br />
                            <asp:TextBox ID="txtPoliza" runat="server" CssClass="textbox" Width="90%" Style="text-align: right" />
                        </td>
                        <td style="text-align: left">
                            <asp:Label ID="lblVence" runat="server" Text="Vence en" /><br />
                            <uc2:fecha ID="txtVence" runat="server" CssClass="textbox" />
                        </td>
                    </tr>
                </table>
                <table border="0" cellpadding="0" cellspacing="0" width="700px">
                    <tr>
                        <td style="text-align: left; width: 130px">Proveedor
                        </td>
                        <td style="text-align: left; width: 200px">Nombre
                        </td>
                        <td style="text-align: left; width: 100px">&nbsp;
                        </td>
                        <td style="text-align: left; width: 100px">
                            <asp:Label ID="lblEstado" runat="server" Text="Estado" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtCodProveedor" runat="server" CssClass="textbox" ReadOnly="True" AutoPostBack="true"
                                Width="50px" Visible="false" OnTextChanged="txtCodProveedor_TextChanged" />
                            <asp:TextBox ID="txtIdProveedor" runat="server" CssClass="textbox" AutoPostBack="true"
                                Width="100px" OnTextChanged="txtIdProveedor_TextChanged" />
                            <asp:Button ID="btnConsultaPersonas" runat="server" CssClass="btn8" Height="26px"
                                OnClick="btnConsultaPersonas_Click" Text="..." />
                        </td>
                        <td style="text-align: left">
                            <uc1:ListadoPersonas ID="ctlBusquedaPersonas" runat="server" />
                            <asp:TextBox ID="txtNomProveedor" runat="server" CssClass="textbox" ReadOnly="True"
                                Width="250px" />
                            <asp:RequiredFieldValidator ID="rfvEncargado" runat="server" ControlToValidate="txtNomProveedor"
                                Display="Dynamic" ErrorMessage="Seleccione encargado" ForeColor="Red" InitialValue="0"
                                Style="font-size: xx-small" ValidationGroup="vgGuardar" />
                        </td>
                        <td style="text-align: left">
                            <asp:CheckBox ID="cbDescuentos" runat="server" Text="Maneja Descuentos" AutoPostBack="True"
                                OnCheckedChanged="cbDescuentos_CheckedChanged" />
                        </td>
                        <td style="text-align: left">
                            <br />
                            <asp:TextBox ID="txtEstado" runat="server" CssClass="textbox" Style="text-align: right" Width="90%" />
                        </td>
                    </tr>
                </table>
                <hr style="width: 100%" />
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td style="text-align: left; width: 100%" colspan="2">
                            <strong>Detalle:</strong><br />
                            <asp:Button ID="btnAddRow" runat="server" CssClass="btn8" OnClick="btnAddRow_Click"
                                OnClientClick="btnAddRow_Click" Text="+ Adicionar Detalle" />
                            <br />
                            <div style="overflow: scroll; width: 100%; max-height: 450px">
                                <asp:GridView ID="gvDetalle" runat="server" AutoGenerateColumns="False"
                                    HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager"
                                    RowStyle-CssClass="gridItem" Style="font-size: x-small" OnRowDataBound="gvDetalle_RowDataBound"
                                    ShowFooter="True" GridLines="Horizontal" DataKeyNames="coddetallefac" OnRowDeleting="gvDetalle_RowDeleting" PageSize="50">
                                    <Columns>
                                        <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/gr_elim.jpg" ShowDeleteButton="True" />
                                        <asp:TemplateField HeaderText="Codigo" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblcodigo" runat="server" Text='<%# Bind("coddetallefac") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Concepto">
                                            <ItemTemplate>
                                                <asp:Label ID="lblConcepto" runat="server" Text='<%# Bind("cod_concepto_fac") %>' Visible="false"></asp:Label>
                                                <cc1:DropDownListGrid ID="ddlConcepto" runat="server"
                                                    AppendDataBoundItems="True" CommandArgument="<%#Container.DataItemIndex %>" CssClass="textbox"
                                                    Style="font-size: xx-small; text-align: left" Width="120px" AutoPostBack="true"
                                                    OnSelectedIndexChanged="ddlConcepto_SelectedIndexChanged">
                                                </cc1:DropDownListGrid>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Detalle">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtDetalle" runat="server" Text='<%# Bind("detalle") %>' CssClass="textbox"></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="C/C">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCentroCosto" runat="server" Text='<%# Bind("centro_costo") %>'
                                                    Visible="false"></asp:Label><cc1:DropDownListGrid ID="ddlCentroCosto" runat="server"
                                                        AppendDataBoundItems="True" CommandArgument="<%#Container.DataItemIndex %>" CssClass="textbox"
                                                        Style="font-size: xx-small; text-align: left" Width="120px">
                                                    </cc1:DropDownListGrid>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cantidad">
                                            <ItemTemplate>
                                                <cc1:TextBoxGrid ID="txtCantidad" runat="server" Text='<%# Eval("cantidad") %>' Width="60px"
                                                    AutoPostBack="true" OnTextChanged="txtCantidad_TextChanged" CommandArgument='<%#Container.DataItemIndex %>'
                                                    Style="text-align: right; border: 1px solid #d7e6e9;" /><asp:FilteredTextBoxExtender
                                                        ID="fte90" runat="server" Enabled="True" FilterType="Numbers, Custom" TargetControlID="txtCantidad"
                                                        ValidChars="" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Vr. Unitario">
                                            <ItemTemplate>
                                                <cc1:TextBoxGrid ID="txtVrUnitario" runat="server" Text='<%# Eval("valor_unitario") %>' onblur="Page_Load(this)" OnPreRender="txtVrUnitario_PreRender"
                                                    Style="text-align: right;" AutoPostBack="true" OnTextChanged="txtVrUnitario_TextChanged"
                                                    Width="100px" CommandArgument='<%#Container.DataItemIndex %>' /><asp:FilteredTextBoxExtender
                                                        ID="fte123" runat="server" Enabled="True" FilterType="Numbers, Custom" TargetControlID="txtVrUnitario"
                                                        ValidChars=".," />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Vr. Total">
                                            <ItemTemplate>
                                                <cc1:TextBoxGrid ID="txtVrTotal" runat="server" Text='<%# Eval("valor_total","{0:n0}")  %>'
                                                    Style="text-align: right" Enabled="false" CssClass="textbox" Width="90px" AutoPostBack="True"
                                                    OnTextChanged="txtVrTotal_TextChanged" CommandArgument='<%#Container.DataItemIndex %>' /><asp:FilteredTextBoxExtender
                                                        ID="fte11" runat="server" Enabled="True" FilterType="Numbers, Custom" TargetControlID="txtVrTotal"
                                                        ValidChars=",." />
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblVrTotal" runat="server"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="%Descuento">
                                            <ItemTemplate>
                                                <cc1:TextBoxGrid ID="txtPorDesc" runat="server" Text='<%# Eval("porc_descuento")  %>'
                                                    Style="text-align: right" Width="50px" AutoPostBack="True" OnTextChanged="txtPorDesc_TextChanged"
                                                    CommandArgument='<%#Container.DataItemIndex %>' Enabled="false" MaxLength="7" /><asp:FilteredTextBoxExtender
                                                        ID="fte12" runat="server" Enabled="True" FilterType="Numbers, Custom" TargetControlID="txtPorDesc"
                                                        ValidChars="," />
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblPorDesc" runat="server"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Impuestos" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:DataList ID="dtImpuestos" runat="server" RepeatDirection="Horizontal" OnItemDataBound="dtImpuestos_ItemDataBound">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblID" runat="server" Text='<%# Eval("coddetalleimp") %>' Visible="false" />
                                                        <asp:Label ID="lblCodImp" runat="server" Text='<%# Eval("cod_tipo_impuesto") %>' Visible="false" />
                                                        <asp:Label ID="lblTipo" runat="server" Text='<%# Eval("naturaleza") %>' Visible="false" />
                                                        <asp:Label ID="lblBase" runat="server" Text='<%# Eval("base_minima") %>' Visible="false" />

                                                        <asp:Label ID="lblTitulo" runat="server" Style="font-size: xx-small" Text='<%# Eval("nom_tipo_impuesto") %>' Visible="false"  />&nbsp;<br />

                                                        <asp:Label ID="lblCuenta1" runat="server" Style="font-size: xx-small" Text='<%# Eval("cod_cuenta_imp") %>' Visible="false"  />

                                                        <cc1:DropDownListGrid ID="ddlPorcentaje" runat="server" AppendDataBoundItems="True" CommandArgument="<%#Container.ItemIndex%>" CssClass="dropdown" AutoPostBack="true" Visible="false" 
                                                            Style="font-size: xx-small; text-align: left" Width="60px" Height="22px" OnSelectedIndexChanged="ddlPorcentaje_SelectedIndexChanged">
                                                        </cc1:DropDownListGrid>

                                                    </ItemTemplate>
                                                    <ItemStyle Width="130px" />
                                                </asp:DataList>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Vr.Neto">
                                            <ItemTemplate>
                                                <asp:Label ID="txtVrNeto" runat="server" Text='<%# Eval("valor_neto") %>' Width="100px"
                                                    Style="text-align: right" CssClass="textbox" />
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblVrNeto" runat="server"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <HeaderStyle CssClass="gridHeader"></HeaderStyle>
                                    <PagerStyle CssClass="gridPager"></PagerStyle>
                                    <RowStyle CssClass="gridItem"></RowStyle>
                                </asp:GridView>
                            </div>
                            <asp:Label ID="lblTotalRegs" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; width: 80%">
                            <strong>Forma de Pago:</strong><br />
                            <asp:Button ID="btnAgregar" runat="server" CssClass="btn8" OnClick="btnAgregar_Click"
                                OnClientClick="btnAgregar_Click" Text="+ Adicionar Detalle" />
                            <br />
                            <div style="overflow: scroll; width: 100%; max-height: 350px">
                                <asp:GridView ID="gvFormaPago" runat="server" AutoGenerateColumns="False"
                                    HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager"
                                    RowStyle-CssClass="gridItem" Style="font-size: x-small" DataKeyNames="codpagofac"
                                    GridLines="Horizontal" OnRowDeleting="gvFormaPago_RowDeleting">
                                    <Columns>
                                        <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/gr_elim.jpg" ShowDeleteButton="True" />
                                        <asp:TemplateField HeaderText="codigo" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCodPago" runat="server" Text='<%# Bind("codpagofac") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Nro. Pago">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtnumero" runat="server" Text='<%# Bind("numero") %>' Width="60px"
                                                    CssClass="textbox"></asp:TextBox><asp:FilteredTextBoxExtender ID="txtValor_FilteredTextBoxExtender"
                                                        runat="server" Enabled="True" FilterType="Numbers, Custom" TargetControlID="txtnumero"
                                                        ValidChars="" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Fecha">
                                            <ItemTemplate>
                                                <uc2:fecha ID="txtfecha" runat="server" CssClass="textbox" Enabled="True" Habilitado="True"
                                                    style="font-size: xx-small; text-align: left" Text='<%# Eval("fecha", "{0:d}") %>'
                                                    TipoLetra="XX-Small" Width_="80" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Porcentaje">
                                            <ItemTemplate>
                                                <cc1:TextBoxGrid ID="txtPorcentaje" runat="server" Text='<%# Bind("porcentaje") %>' Width="80px" MaxLength="6"
                                                    AutoPostBack="true" OnTextChanged="txtPorcentaje_TextChanged" CommandArgument='<%#Container.DataItemIndex %>' Style="text-align: right" BackColor="#F4F5FF" />
                                                <asp:FilteredTextBoxExtender ID="fte99" runat="server" Enabled="True" FilterType="Numbers, Custom"
                                                    TargetControlID="txtPorcentaje" ValidChars="," />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Valor a Pagar">
                                            <ItemTemplate>
                                                <cc1:TextBoxGrid ID="txtValor" runat="server" Text='<%# Eval("valor") %>' Width="110px" onblur="Page_Load(this)" OnPreRender="txtValor_PreRender"
                                                    AutoPostBack="true" OnTextChanged="txtValor_TextChanged" CommandArgument='<%#Container.DataItemIndex %>'
                                                    Style="text-align: right;" /><asp:FilteredTextBoxExtender ID="fte20" runat="server"
                                                        Enabled="True" FilterType="Numbers, Custom" TargetControlID="txtValor" ValidChars="." />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="%Dto pronto Pago">
                                            <ItemTemplate>
                                                <cc1:TextBoxGrid ID="txtPorDsctoPago" runat="server" Text='<%# Eval("porc_descuento") %>'
                                                    Width="80px" AutoPostBack="true" OnTextChanged="txtPorDsctoPago_TextChanged"
                                                    CommandArgument='<%#Container.DataItemIndex %>' Style="text-align: right;" /><asp:FilteredTextBoxExtender
                                                        ID="fte21" runat="server" Enabled="True" FilterType="Numbers, Custom" TargetControlID="txtPorDsctoPago"
                                                        ValidChars="," />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Fecha Pago con Dscto">
                                            <ItemTemplate>
                                                <uc2:fecha ID="txtfechaDscto" runat="server" CssClass="textbox" Enabled="True" Habilitado="True"
                                                    style="font-size: xx-small; text-align: left" Text='<%# Eval("fecha_descuento", "{0:d}") %>'
                                                    TipoLetra="XX-Small" Width_="80" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Vr. Pagar con Dscto">
                                            <ItemTemplate>
                                                <asp:Label ID="txtValorDscto" runat="server" Text='<%# Eval("vr_ConDescuento") %>'
                                                    Width="100px" Style="text-align: right" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <HeaderStyle CssClass="gridHeader"></HeaderStyle>
                                    <PagerStyle CssClass="gridPager"></PagerStyle>
                                    <RowStyle CssClass="gridItem"></RowStyle>
                                </asp:GridView>
                            </div>
                            <asp:Label ID="lblTotalRegs1" runat="server" />
                        </td>
                        <td style="text-align: center; width: 20%">
                            <strong>Anticipos</strong><br />
                            <asp:CheckBox ID="cbManejaAnti" runat="server" Text="Maneja Anticipos" AutoPostBack="True"
                                OnCheckedChanged="cbManejaAnti_CheckedChanged" />
                            <br />
                            Valor Anticipo<br />


                            <asp:TextBox ID="txtValorAnti" runat="server" AutoPostBack="true" CssClass="textbox" Width="120px" Style="text-align: right" OnTextChanged="txtValorAnti_TextChanged" /><br />
                            <asp:FilteredTextBoxExtender ID="txtValor_FilteredTextBoxExtender" runat="server"
                                Enabled="True" FilterType="Numbers, Custom" TargetControlID="txtValorAnti" ValidChars="" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: left">Observaciones<br />
                            <asp:TextBox ID="txtObservacion" runat="server" CssClass="textbox" Width="600px"
                                Style="text-align: left" TextMode="MultiLine" />
                        </td>
                    </tr>
                </table>
                <asp:Panel ID="panelGiro" runat="server" Height="70px">
                    <table style="width: 100%">
                        <tr>
                            <td colspan="3" style="text-align: left">
                                <uc3:giro ID="ctlGiro" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">&nbsp;</td>
                        </tr>
                    </table>
                </asp:Panel>
                <%--</ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="cbManejaAnti" EventName="CheckedChanged" />
                        <asp:AsyncPostBackTrigger ControlID="ddlTipoCuenta" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>--%>
            </asp:View>
            <asp:View ID="vwReporte" runat="server">
                <br />
                <br />
                <table width="100%">
                    <tr>
                        <td style="text-align: left">
                            <asp:Button ID="btnVerData" runat="server" CssClass="btn8" OnClick="btnVerData_Click"
                                Text="Visualizar los datos" Height="25px" Width="130px" />
                            &#160;&#160;
                            <asp:Button ID="btnImprime" runat="server" CssClass="btn8" Height="25px" Width="130px"
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
                            <rsweb:ReportViewer ID="rvCuentaXpagar" runat="server" Font-Names="Verdana" Font-Size="8pt"
                                Height="450px" InteractiveDeviceInfos="(Colección)" WaitMessageFont-Names="Verdana"
                                WaitMessageFont-Size="14pt" Width="100%">
                                <LocalReport ReportPath="Page\Tesoreria\CuentasPorPagar\rptCuentasXpagar.rdlc"></LocalReport>
                            </rsweb:ReportViewer>
                        </td>
                    </tr>
                </table>
            </asp:View>
            <asp:View ID="vwFinal" runat="server">
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
                            <td style="text-align: center; font-size: large;">Cuenta
                                <asp:Label ID="lblMsj" runat="server"></asp:Label>
                                Correctamente
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: center; font-size: large;">
                                <br />
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: center; font-size: large;"></td>
                        </tr>
                    </table>
                </asp:Panel>
            </asp:View>
        </asp:MultiView>
    </asp:Panel>
    <asp:Panel ID="panelProceso" runat="server" Width="100%">
        <uc2:procesoContable ID="ctlproceso" runat="server" />
    </asp:Panel>
    <asp:HiddenField ID="HiddenField1" runat="server" />
    <uc4:mensajegrabar ID="ctlMensaje" runat="server" />
</asp:Content>
