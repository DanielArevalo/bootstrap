<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Src="~/General/Controles/direccion.ascx" TagName="direccion" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/ctlSeleccionarPersona.ascx" TagName="ListadoPersonas" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>
<%@ Register Src="~/General/Controles/imprimir.ascx" TagName="imprimir" TagPrefix="ucImprimir" %>
<%@ Register Src="~/General/Controles/ctlTasa.ascx" TagName="ctlTasa" TagPrefix="ctl" %>
<%@ Register Src="~/General/Controles/fechaeditable.ascx" TagName="fecha" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/decimalesGridRow.ascx" TagName="decimalesGridRow" TagPrefix="uc1" %>
<%@ Register src="../../../General/Controles/fecha.ascx" tagname="fecha" tagprefix="uc3" %>


<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:Panel ID="pConsulta" runat="server">
        <table width="100%">
            <tr>
                <td style="text-align: left;">
                    <strong>Datos de la Línea</strong>
                </td>
            </tr>
            <tr>
                <td style="text-align: left;">Línea Ahorro Programado<br />
                    <asp:DropDownList ID="ddlLineaFiltro" runat="server" AutoPostBack="true" Style="margin-bottom: 0px" Width="450px"
                        CssClass="textbox"
                        OnSelectedIndexChanged="ddlLineaFiltro_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:MultiView ID="MvPrincipal" runat="server" ActiveViewIndex="0">
        <asp:View ID="ViewAfilados" runat="server">
            <table>
                <tr>
                    <td>
                        <strong>Datos del Titular</strong>
                    </td>
                </tr>
            </table>
            <uc1:ListadoPersonas ID="ctlBusquedaPersonas" runat="server" />
        </asp:View>
        <asp:View ID="ViewCuenta" runat="server">
            <table style="text-align: center" cellspacing="4" cellpadding="0">
                <tr>
                    <td colspan="6" style="font-size: x-small; text-align: left">
                        <strong>Titular de la Cuenta</strong>
                        <hr style="width: 100%" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left">
                        <asp:TextBox ID="txtCodigoCliente" runat="server" CssClass="textbox" Visible="false" />
                        Identificación<br />
                        <asp:TextBox ID="txtIdentificacion" Enabled="false" runat="server" CssClass="textbox" Width="120px" />
                    </td>
                    <td style="text-align: left">Tipo Identificación<br />
                        <asp:DropDownList ID="ddlTipoIdentifi"  Enabled="false" runat="server" CssClass="textbox" ReadOnly="True"
                            Width="180px" />
                    </td>
                    <td style="text-align: left" colspan="4">Nombre<br />
                        <asp:TextBox ID="txtNombres" runat="server"  Enabled="false" CssClass="textbox" Width="350px" />
                    </td>
                </tr>
                <tr>
                    <td colspan="6" style="font-size: x-small; text-align: left"><strong>Datos de la Cuenta</strong> </td>
                </tr>
                <tr>
                    <td colspan="6">
                        <hr style="width: 100%" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left">Cuenta<br />
                        <asp:TextBox ID="txtCuenta" Enabled="false" runat ="server" CssClass="textbox" Width="120px" />
                        <asp:Label ID="lblNumAuto" runat="server" CssClass="textbox"
                            Text="Autogenerado" Visible="false" />
                        <asp:FilteredTextBoxExtender ID="fte5" runat="server" TargetControlID="txtCuenta"
                            FilterType="Custom, Numbers" ValidChars=",." />
                    </td>
                    <td style="text-align: left">F. Apertura<br />
                        <uc1:fecha ID="txtFechApertura" runat="server"  />
                    </td>
                    <td style="text-align: left" colspan="3">Oficina<br />
                        <asp:DropDownList ID="ddlOficina" runat="server" CssClass="textbox" ReadOnly="True"
                            Width="200px" />
                    </td>
                    <td style="text-align: left">Estado<br />
                        <asp:CheckBox ID="chkEstado" runat="server" Text="Activa" TextAlign="Right" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left;">Motivo Apertura<br />
                        <asp:DropDownList ID="ddlMotivoApertura" runat="server" CssClass="textbox" ReadOnly="True"
                            Width="220px" />
                    </td>
                    <td style="text-align: left;">Moneda<br />
                        <asp:DropDownList ID="ddlTipoMoneda" runat="server" CssClass="textbox"
                            Width="90%" />
                    </td>
                    <td style="text-align: left">Cuota<br />
                        <uc1:decimales ID="txtCuota" runat="server" />
                    </td>
                    <td style="text-align: left;" colspan="2">Plazo<br />
                        <asp:TextBox ID="txtPlazo" AutoPostBack="True" runat="server" CssClass="textbox" Width="70px" Style="text-align: right"
                            MaxLength="4" OnTextChanged="txtPlazo_TextChanged" />
                        <asp:FilteredTextBoxExtender ID="fte20" runat="server" TargetControlID="txtPlazo"
                            FilterType="Custom, Numbers" ValidChars="" />
                    </td>
                    <td style="text-align: left;">Periodicidad<br />
                        <asp:DropDownList ID="ddlPeriodicidad"  AutoPostBack="True"  runat="server" CssClass="textbox" ReadOnly="True"
                            Width="200px" OnSelectedIndexChanged="ddlPeriodicidad_SelectedIndexChanged" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left">F. Primer Pago<br />                      
                        <asp:TextBox ID="txtFechaPrimPago" AutoPostBack="True"  OnTextChanged="txtFechaPrimPago_TextChanged"  CssClass="textbox"  maxlength="10" runat="server"></asp:TextBox>
                        <img id="Image1" alt="Calendario" src="../../../Images/iconCalendario.png" />
                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" 
                            PopupButtonID="Image1" 
                            TargetControlID="txtFechaPrimPago"
                            Format="dd/MM/yyyy" ></asp:CalendarExtender>
                    </td>
                    <td style="text-align: left">Forma de Pago<br />
                        <asp:DropDownList ID="ddlFormaPago" runat="server" AutoPostBack="True" CssClass="textbox"
                            Width="170px" OnSelectedIndexChanged="ddlFormaPago_SelectedIndexChanged">
                            <asp:ListItem Value="1">Caja</asp:ListItem>
                            <asp:ListItem Value="2">Nomina</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td style="text-align: left" colspan="2">
                        <asp:Panel ID="panelEmpresa" runat="server" Width="308px">
                            Empresa de Recuado<br />
                            <asp:DropDownList ID="ddlEmpresa" runat="server" Width="250px" CssClass="textbox" OnSelectedIndexChanged="ddlEmpresa_SelectedIndexChanged" />
                        </asp:Panel>
                    </td>
                    <td style="text-align: left">Fecha de Vencimiento<br />
                        <ucFecha:fecha ID="txtFechaVencimiento" runat="server" Enabled="false" />
                    </td>
                    <td style="text-align: left;">Asesor Comercial:
                                                    <br />
                        <asp:DropDownList ID="ddlAsesor" runat="server" Width="95%" CssClass="textbox" AppendDataBoundItems="true">
                            <asp:ListItem Text="Seleccione un Item" Value=" " />
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td colspan="6" style="text-align: left">
                        <strong>Tasa de Interés</strong>
                        <br />
                        <asp:CheckBox ID="cbInteresCuenta" runat="server"
                            OnCheckedChanged="cbInteresCuenta_CheckedChanged" Text="Interés por Cuenta" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left;" colspan="2">
                        <asp:Panel ID="panelTasa" runat="server">
                            <ctl:ctlTasa ID="ctlTasaInteres" runat="server" Width="400px" />
                        </asp:Panel>
                    </td>
                    <td style="text-align: left;">
                        <asp:Label ID="lblSaldoMinimo" runat="server" Text="Saldo Mínimo Linea"
                            Visible="False"></asp:Label>
                        <br />
                        <uc1:decimales ID="txtSaldoMinimo" runat="server" Visible="False" />
                    </td>
                    <td colspan="2">
                        <asp:Label ID="lblPlazoMinimo" runat="server" Text="Plazo Mínimo Linea"
                            Visible="False"></asp:Label>
                        <br />
                        <uc1:decimales ID="txtPlazoMinimo" runat="server" Visible="False" />
                    </td>
                    <td>&nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="2">&nbsp;</td>
                    <td style="text-align: left;">
                        <asp:Label ID="lblFInteres" runat="server" Text="F. Interes"></asp:Label>
                        <br />
                        <ucFecha:fecha ID="txtFechaInt"  Enabled="false"  runat="server" />
                    </td>
                    <td style="text-align: left;" colspan="2">
                        <asp:Label ID="lbltotalinteres" runat="server"    Text="TotInteres"></asp:Label>
                        <br />
                        <uc1:decimales ID="txtTotInteres" Enabled="false"  runat="server" />
                    </td>
                    <td style="text-align: left;">
                        <asp:Label ID="lbltotalretencion" runat="server" Text="TotRetencion"  Visible="false" ></asp:Label>
                        <br />
                        <uc1:decimales ID="txtTotRetencion" Enabled="false" Visible="false"   runat="server" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left;">
                        <asp:Label ID="LblSaldo" runat="server" Text="Saldo Total"></asp:Label>
                        <br />
                        <uc1:decimales ID="txtSaldoTotal"  Enabled="false"  runat="server" />
                    </td>
                     <td style="text-align: left;">
                        <asp:Label ID="lblInteresPagar" runat="server" Text="Interes_pagar"></asp:Label>
                        <br />
                        <uc1:decimales ID="txtInteresPagar"  Enabled="false"  runat="server" />
                    </td>
                     <td style="text-align: left;">
                        <asp:Label ID="lblInteresCapitalizar" runat="server" Text="Interes_Capitalizar"></asp:Label>
                        <br />
                        <uc1:decimales ID="txtInteresCapitalizar"  Enabled="false"  runat="server" />
                    </td>
                    <td style="text-align: left;">
                        <asp:Label ID="lblproxpago" runat="server" Text="F. Prox Pago"></asp:Label>
                        <br />
                        <ucFecha:fecha ID="txtFechaProxPago" Enabled="false"  runat="server" />
                    </td>
                    <td style="text-align: left;">
                        <asp:Label ID="lblultpago" runat="server" Text="F. Ultimo Pago"></asp:Label>
                        <br />
                        <ucFecha:fecha ID="txtFechaUltPago"  Enabled="false" runat="server" />
                    </td>
                    <td style="text-align: left;" colspan="2">
                        <asp:Label ID="lblfechacierre" runat="server" Text="F. Cierre"></asp:Label>
                        <br />
                        <ucFecha:fecha ID="txtFecCierre" Enabled="false"  runat="server" />
                    </td>
                    <td style="text-align: left;">
                        <asp:Label ID="lblpagadas" runat="server" Text="Pagadas"></asp:Label>
                        <br />
                        <asp:TextBox ID="txtPagadas" runat="server" Enabled="false" CssClass="textbox" Width="70px" />
                        <asp:FilteredTextBoxExtender ID="fte12" runat="server" TargetControlID="txtPagadas"
                            FilterType="Custom, Numbers" ValidChars="" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left;">
                        <ucFecha:fecha ID="txtfechaafili" runat="server" Visible="false" />
                    </td>
                    <td style="text-align: left;">
                        &nbsp;</td>
                    <td style="text-align: left;">&nbsp;</td>
                    <td style="text-align: left;" colspan="2">&nbsp;</td>
                    <td style="text-align: left;">&nbsp;</td>
                </tr>
            </table>
            <table cellpadding="0" cellspacing="0" style="width: 100%">
                <tr>
                    <td style="text-align: left">
                        <asp:CheckBox Text="Desea tener beneficiarios" ID="chkBeneficiario" AutoPostBack="true" OnCheckedChanged="chkBeneficiario_CheckedChanged" runat="server" /><br />
                        <br />
                        <asp:UpdatePanel ID="upBeneficiarios" Visible="false" runat="server">
                            <ContentTemplate>
                                <asp:Button ID="btnAddRowBeneficio" AutoPostBack="true" runat="server" CssClass="btn8" TabIndex="49" OnClick="btnAddRowBeneficio_Click" Text="+ Adicionar Detalle" />
                                <asp:GridView ID="gvBeneficiarios"
                                    runat="server" AllowPaging="True" TabIndex="50" AutoGenerateColumns="false" BackColor="White"
                                    BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="0" DataKeyNames="idbeneficiario"
                                    ForeColor="Black" GridLines="Both" OnRowDataBound="gvBeneficiarios_RowDataBound"
                                    OnRowDeleting="gvBeneficiarios_RowDeleting" PageSize="10" ShowFooter="True" Style="font-size: xx-small"
                                    Width="80%">
                                    <AlternatingRowStyle BackColor="White" />
                                    <Columns>
                                        <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/gr_elim.jpg" ShowDeleteButton="True" />
                                        <asp:TemplateField HeaderText="Identificación" ItemStyle-HorizontalAlign="Left"><ItemTemplate><asp:HiddenField ID="hdIdBeneficiario" runat="server" Value='<%# Bind("idbeneficiario") %>' /><asp:TextBox ID="txtIdentificacion" runat="server" CssClass="textbox" Style="font-size: xx-small; text-align: left"
                                                    Text='<%# Bind("identificacion_ben") %>' Width="100px"> </asp:TextBox></ItemTemplate><ItemStyle HorizontalAlign="Left" Width="100px" /></asp:TemplateField>
                                        <asp:TemplateField HeaderText="Nombres y Apellidos" ItemStyle-HorizontalAlign="Left"><ItemTemplate><asp:TextBox ID="txtNombres" runat="server" CssClass="textbox" Style="font-size: xx-small; text-align: left"
                                                    Text='<%# Bind("nombre_ben") %>' Width="260px"> </asp:TextBox></ItemTemplate><ItemStyle HorizontalAlign="Left" Width="260px" /></asp:TemplateField>
                                        <asp:TemplateField HeaderText="Fecha Nacimiento" ItemStyle-HorizontalAlign="Left"><ItemTemplate><uc1:fecha ID="txtFechaNacimientoBen" runat="server" CssClass="textbox" style="font-size: xx-small; text-align: left"
                                                    Text='<%# Eval("fecha_nacimiento_ben", "{0:" + FormatoFecha() + "}") %>' /></ItemTemplate><ItemStyle HorizontalAlign="Left" Width="100px" /></asp:TemplateField>
                                        <asp:TemplateField HeaderText="Sexo" ItemStyle-HorizontalAlign="Left" ><ItemTemplate><asp:DropDownList runat="server" ID="ddlsexo" CssClass="textbox"><asp:ListItem Value="F" Text="Femenino" /><asp:ListItem Value="M" Text="Masculino" /></asp:DropDownList></ItemTemplate><ItemStyle HorizontalAlign="Left" Width="100px" /></asp:TemplateField>
                                        <asp:TemplateField HeaderText="Parentesco" ItemStyle-HorizontalAlign="Center"><ItemTemplate><asp:DropDownList runat="server" ID="ddlParentezco" DataValueField="CODPARENTESCO" DataTextField="DESCRIPCION" CssClass="textbox"></asp:DropDownList></ItemTemplate><ItemStyle HorizontalAlign="Center" Width="120px" /></asp:TemplateField>
                                        <asp:TemplateField HeaderText="% Ben." ItemStyle-HorizontalAlign="Right"><ItemTemplate><uc1:decimalesGridRow ID="txtPorcentaje" runat="server" AutoPostBack_="True" CssClass="textbox"
                                                    Enabled="True" Habilitado="True" Text='<%# Eval("porcentaje_ben") %>' Width_="80" /></ItemTemplate><ItemStyle HorizontalAlign="Right" Width="100px" /></asp:TemplateField>
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
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
             <table cellpadding="0" cellspacing="0" style="width: 100%">
                <tr>
                    <td style="text-align: left">
                        &nbsp;<asp:Label ID="lblcuotasExtras" runat="server" Text="Cuotas Extras"></asp:Label>
                        <hr style="width: 100%" />
                        <asp:CheckBox Text="Desea Incluir Cuotas Extras" ID="ChkCuotasExtras" AutoPostBack="true" OnCheckedChanged="ChkCuotasExtras_CheckedChanged" runat="server" /><br />
                        <br />
                        <%--<asp:Panel ID="upCuotasExtras" Visible="false" runat="server">--%>
                        <asp:UpdatePanel ID="upCuotasExtras" Visible="false" runat="server">
                            <ContentTemplate>
                                <%--<br />--%>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblValorCuotaExtra" runat="server" Text="Valor Cuota Extra"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtValorCuotaExt" runat="server" CssClass="textbox" Width="136px"></asp:TextBox>
                                        </td>
                                        <td rowspan="4" style="text-align: center">
                                            <asp:Button ID="btnGenerarCuotaext" runat="server" CssClass="btn8" OnClick="btnGenerarCuotaext_Click" Text="Generar Cuotas Extras" />
                                            <br />
                                            <br />
                                            <asp:Button ID="btnLimpiarCuotaext" runat="server" CssClass="btn8" OnClick="btnLimpiarCuotaext_Click" Text="Limpiar Cuotas Extras" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblFechaPrimerCuotaExtra" runat="server" Text="Fecha de primer Cuota Extra"></asp:Label>
                                        </td>
                                        <td><uc3:fecha ID="txtFechaCuotaExt" runat="server" CssClass="textbox" Width="136px" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblPeriodicidadCuotaExtra" runat="server" Text="Periodicidad de Cuota Extra"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlPeriodicidadCuotaExt" runat="server" CssClass="textbox" Width="148px">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
                                        </td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                </table>
                                <br />
                                <asp:GridView ID="gvCuoExt" runat="server" AutoGenerateColumns="False" AutoPostBack="True" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" EmptyDataText="No se encontraron registros." ForeColor="Black" GridLines="Vertical" Height="16px" OnPageIndexChanging="gvCuoExt_PageIndexChanging" OnRowCommand="gvCuoExt_RowCommand"  OnRowDeleting="gvCuoExt_RowDeleting" PageSize="5" ShowFooter="True" ShowHeaderWhenEmpty="True" Style="font-size: x-small" Width="40%">
                                    <AlternatingRowStyle BackColor="White" />
                                    <Columns>
                                        <asp:TemplateField ShowHeader="False">
                                            <FooterTemplate>
                                                <asp:ImageButton ID="btnNuevo" runat="server" CausesValidation="False" CommandName="AddNew" ImageUrl="~/Images/gr_nuevo.jpg" ToolTip="Crear Nuevo" Width="16px" />
                                            </FooterTemplate>
                                            <ItemStyle Width="20px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnEliminar" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="Delete" ImageUrl="~/Images/gr_elim.jpg" ToolTip="Eliminar" Width="16px" />
                                            </ItemTemplate>
                                            <HeaderStyle CssClass="gridIco" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Fecha Pago">
                                            <ItemTemplate>
                                                <asp:Label ID="lblfechapago" runat="server" Text='<%# Bind("fecha_pago","{0:d}") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtfechapago" runat="server" Text='<%# Bind("fecha_pago") %>'></asp:TextBox>
                                                <asp:CalendarExtender ID="calExtfechapago" runat="server" Format="dd/MM/yyyy" TargetControlID="txtfechapago" />
                                            </FooterTemplate>
                                            <ItemStyle Width="50px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Valor">
                                            <ItemTemplate>
                                                <asp:Label ID="lblvalor" runat="server" Text='<%# Bind("valor") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtvalor" runat="server" Text='<%# Bind("valor", "{0:N}") %>'></asp:TextBox>
                                                <asp:FilteredTextBoxExtender ID="txtValor_FilteredTextBoxExtender" runat="server" Enabled="True" FilterType="Numbers, Custom" TargetControlID="txtvalor" ValidChars="." />
                                            </FooterTemplate>
                                            <ItemStyle Width="50px" />
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
                             </ContentTemplate>
                        </asp:UpdatePanel>
                        <%--</asp:Panel>--%>
                    </td>
                </tr>
            </table>

        </asp:View>
        <asp:View ID="vwFinal" runat="server">
            <asp:Panel ID="PanelFinal" runat="server">
                <table style="width: 100%;">
                    <tr>
                        <td style="text-align: center; font-size: large; height: 34px;">
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;">Apertura del Ahorro Programado
                            <asp:Label ID="lblMsj" runat="server"></asp:Label>
                            Correctamente
                            <br />
                            Número de Ahorro&nbsp;<asp:Label ID="lblgenerado" runat="server"></asp:Label>
                            <br />
                            <asp:Button ID="btnImprime" runat="server" CssClass="btn8"
                                OnClick="btnImprime_Click" Text="Desea Imprimir ?" Visible="False" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </asp:View>

    </asp:MultiView>
    <asp:MultiView ID="mvReporte" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwReporte" runat="server">
            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                <tr>
                    <td>
                        <rsweb:ReportViewer ID="rvReporte" runat="server" Enabled="false"
                            Font-Names="Verdana" Font-Size="8pt" Height="500px"
                            InteractiveDeviceInfos="(Colección)" WaitMessageFont-Names="Verdana"
                            WaitMessageFont-Size="10pt" Width="100%" CssClass="aspNetDisabled">
                            <LocalReport ReportPath="Page\Programado\Cuentas\RptAhorroProgramdo.rdlc">
                                <DataSources>
                                    <rsweb:ReportDataSource />
                                </DataSources>
                            </LocalReport>
                        </rsweb:ReportViewer>

                    </td>
                </tr>

            </table>
        </asp:View>
    </asp:MultiView>

    <uc4:mensajegrabar ID="ctlMensaje" runat="server" />

</asp:Content>
