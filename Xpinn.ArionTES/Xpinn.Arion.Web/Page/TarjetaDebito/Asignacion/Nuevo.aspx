<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Src="~/General/Controles/direccion.ascx" TagName="direccion" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/ctlSeleccionarPersona.ascx" TagName="ListadoPersonas"
    TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar"
    TagPrefix="uc4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:MultiView ID="MvPrincipal" runat="server" ActiveViewIndex="0">
        <asp:View ID="ViewAfilados" runat="server">
            <table style="width: 1237px">
                <tr>
                    <td style="height: 24px">
                        <strong>Datos del Titular</strong>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Panel ID="pEncBusqueda" runat="server" CssClass="collapsePanelHeader"
                            Height="30px">
                            <div style="padding: 5px; cursor: pointer; vertical-align: middle;">
                                <div style="float: left; color: #0066FF; font-size: small">
                                    Búsqueda Avanzada
                                </div>
                                <div style="float: left; margin-left: 5px; color: #0066FF; font-size: small">
                                    <asp:Label ID="lblMostrarDetalles" runat="server">(Mostrar Detalles...)</asp:Label>
                                    <asp:ImageButton ID="imgExpand" runat="server"
                                        AlternateText="(Show Details...)" ImageUrl="~/Images/expand.jpg" />
                                </div>
                                <br />
                            </div>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td style="height: 23px">
                        <asp:Panel ID="pBusqueda" runat="server" Height="70px">
                            <table cellpadding="0" cellspacing="0" style="width: 100%">
                                <tr>
                                    <td colspan="4" style="height: 15px; text-align: left; font-size: x-small;">
                                        <strong>Criterios de Búsqueda:</strong>
                                        <asp:Label ID="LblMensaje" runat="server" Font-Size="Larger" Style="color: #FF0000; font-weight: 700; font-size: x-small"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: left;">Tipo de Persona<br />
                                        <asp:DropDownList ID="ddlTipoPersona" runat="server"
                                            AppendDataBoundItems="True" CssClass="textbox" Width="120px">
                                            <asp:ListItem Value=""></asp:ListItem>
                                            <asp:ListItem Value="N">Natural</asp:ListItem>
                                            <asp:ListItem Value="J">Juridica</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td style="text-align: left;">Código<br />
                                        <asp:TextBox ID="txtCodigo" runat="server" CssClass="textbox" Width="100px"></asp:TextBox>
                                    </td>
                                    <td style="text-align: left;">Identificación<br />
                                        <asp:TextBox ID="txtNumeIdentificacion2" runat="server" CssClass="textbox"
                                            OnTextChanged="txtNumeIdentificacion2_TextChanged" Width="120px"></asp:TextBox>
                                    </td>
                                    <td style="text-align: left;">Nombres<br />
                                        <asp:TextBox ID="txtNombres0" runat="server" CssClass="textbox" Enabled="true"
                                            Width="160px"></asp:TextBox>
                                    </td>
                                    <td style="text-align: left;">Apellidos<br />
                                        <asp:TextBox ID="txtApellidos" runat="server" CssClass="textbox" Enabled="true"
                                            Width="160px"></asp:TextBox>
                                    </td>
                                    <td style="text-align: left;">Razón Social<br />
                                        <asp:TextBox ID="txtRazonSocial" runat="server" CssClass="textbox"
                                            Enabled="true" Width="160px"></asp:TextBox>
                                    </td>
                                    <td style="text-align: left;">Ciudad<br />
                                        <asp:DropDownList ID="ddlCiudad" runat="server" AppendDataBoundItems="True"
                                            CssClass="textbox" Width="120px">
                                            <asp:ListItem Value=""></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="7" style="text-align: left;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                        <asp:Repeater ID="rptAlphabets" runat="server"
                                            OnItemCommand="rptAlphabets_ItemCommand">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtAlphabet" runat="server" OnClick="Alphabet_Click"
                                                    OnClientClick="Alphabet_Click" Text='<%#Eval("Value")%>'
                                                    Visible='<%# !Convert.ToBoolean(Eval("Selected"))%>' />
                                                <asp:Label ID="lblAlphabet0" runat="server" Text='<%#Eval("Value")%>'
                                                    Visible='<%# Convert.ToBoolean(Eval("Selected"))%>' />
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="7" style="text-align: left;">
                                        <div class="AlphabetPager" style="text-align: left; width: 1200px;">
                                            &nbsp;&nbsp;
                                            <asp:Label ID="lblTotalRegs" runat="server" Visible="False" />
                                            <asp:Label ID="lblInfo" runat="server"
                                                Text="Su consulta no obtuvo ningún resultado." Visible="False" />
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="7" style="text-align: left;">&nbsp;</td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <asp:CollapsiblePanelExtender ID="cpeDemo" runat="Server"
                            CollapseControlID="pEncBusqueda" Collapsed="False"
                            CollapsedImage="~/Images/expand.jpg"
                            CollapsedText="(Click Aqui para Mostrar Detalles...)"
                            ExpandControlID="pEncBusqueda" ExpandedImage="~/Images/collapse.jpg"
                            ExpandedText="(Click Aqui para Ocultar Detalles...)" ImageControlID="imgExpand"
                            SkinID="CollapsiblePanelDemo" SuppressPostBack="true"
                            TargetControlID="pBusqueda" TextLabelID="lblMostrarDetalles" />
                    </td>
                </tr>
                <tr>
                    <td style="height: 23px">
                        <asp:MultiView ID="MvAfiliados" runat="server">
                            <asp:View ID="ViewAfilaidoss" runat="server">
                                <asp:GridView ID="gvListaAFiliados" runat="server" AllowPaging="True"
                                    AutoGenerateColumns="False" DataKeyNames="identificacion"
                                    GridLines="Horizontal" OnPageIndexChanging="gvListaAFiliados_PageIndexChanging"
                                    OnRowEditing="gvListaAFiliados_RowEditing" PageSize="5"
                                    ShowHeaderWhenEmpty="True" Style="font-size: x-small" Width="100%">
                                    <Columns>
                                        <asp:TemplateField HeaderStyle-CssClass="gridIco">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnEditar" runat="server" CommandName="Edit"
                                                    ImageUrl="~/Images/gr_edit.jpg" ToolTip="Editar" Width="16px" />
                                            </ItemTemplate>
                                            <HeaderStyle CssClass="gridIco" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="cod_persona" HeaderText="Código">
                                            <ItemStyle HorizontalAlign="Left" Width="50px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="nom_tipo_persona" HeaderText="Tipo Persona">
                                            <ItemStyle HorizontalAlign="Left" Width="50px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="identificacion" HeaderText="Identificación">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="valor_afiliacion" HeaderText="Valor Afiliacion" />
                                        <asp:BoundField DataField="digito_verificacion" HeaderText="D.V.">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="primer_nombre" HeaderText="Primer Nombre">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="segundo_nombre" HeaderText="Segundo Nombre">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="primer_apellido" HeaderText="Primer Apellido">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="segundo_apellido" HeaderText="Segundo Apellido">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="razon_social" HeaderText="Razón Social">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="codciudadexpedicion" HeaderText="Ciudad">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="direccion" HeaderText="Dirección">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="telefono" HeaderText="Telefóno">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="email" HeaderText="E-Mail">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                    </Columns>
                                    <HeaderStyle CssClass="gridHeader" />
                                    <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                                    <RowStyle CssClass="gridItem" />
                                </asp:GridView>
                            </asp:View>

                        </asp:MultiView>
                    </td>
                </tr>
            </table>
        </asp:View>

        <br />

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
                        <td style="text-align: center; font-size: large; color: Red">La cuenta fue
                            <asp:Label ID="lblmsj" runat="server"></asp:Label>
                            &nbsp;correctamente.
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <br />
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;">&nbsp;
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </asp:View>
        <asp:View ID="ViewCuenta" runat="server">
            <table style="text-align: center" cellspacing="4" cellpadding="0">
                <tr>
                    <td colspan="6" style="font-size: x-small; text-align: left">
                        <strong>Titular de la Cuenta</strong>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left; width: 194px;">
                        <asp:TextBox ID="txtCodigoCliente" runat="server" CssClass="textbox"
                            Visible="False" />
                        Identificación<br />
                        <asp:TextBox ID="txtIdentificacion" runat="server" CssClass="textbox"
                            Width="120px" Enabled="False" />
                    </td>
                    <td style="text-align: left;" colspan="3">Tipo Identificación<br />
                        <asp:DropDownList ID="ddlTipoIdentifi" runat="server" CssClass="textbox" ReadOnly="True"
                            Width="180px" />
                    </td>
                    <td style="text-align: left" colspan="2">Nombre<br />
                        <asp:TextBox ID="txtNombres" runat="server" CssClass="textbox" Width="350px"
                            Enabled="False" />
                    </td>
                </tr>
                <tr>
                    <td colspan="6">
                        <hr style="width: 100%" />
                    </td>
                </tr>
                <tr>
                    <td colspan="6" style="font-size: x-small; text-align: left">
                        <strong>Datos de la Cuenta</strong>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left; height: 73px;" colspan="2">Tipo de Cuenta<br />
                        <asp:DropDownList ID="ddlTipoCuenta" runat="server" CssClass="textbox"
                            ReadOnly="True" Width="180px" AutoPostBack="True"
                            OnSelectedIndexChanged="ddlTipoCuenta_SelectedIndexChanged1" />

                        <br />
                    </td>
                    <td colspan="2" style="text-align: left; height: 73px;">Cuenta<br />
                        <asp:DropDownList ID="ddlCuenta" runat="server" CssClass="textbox"
                            ReadOnly="True" Width="180px" AutoPostBack="True"
                            OnSelectedIndexChanged="ddlCuenta_SelectedIndexChanged" />
                        <asp:TextBox ID="txtCuentas" runat="server" CssClass="textbox" Enabled="False"
                            Width="120px" />
                    </td>
                    <td style="text-align: left; height: 4px;" colspan="2">&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align: left; height: 4px;">Saldo Total<br />
                        <uc1:decimales ID="txtSaldoTotal" runat="server" Enabled="False" />
                    </td>
                    <td colspan="2" style="text-align: left; height: 4px;">Saldo Disponible<br />
                        <uc1:decimales ID="txtSaldoDisponible" runat="server" ClientIDMode="Inherit"
                            Enabled="False" />
                    </td>
                    <td colspan="2" style="text-align: left; height: 4px;">Asesor Comercial:
                                                    <br />
                        <asp:DropDownList ID="ddlAsesor" runat="server" Width="95%" CssClass="textbox" AppendDataBoundItems="true">
                            <asp:ListItem Text="Seleccione un Item" Value=" " />
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td colspan="6" style="font-size: x-small; text-align: left">
                        <strong>Datos de la Tarjeta</strong>
                        <hr style="width: 100%" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align: left;">Fecha de Asignación<br />
                        <ucFecha:fecha ID="TxtFechaAsignacion" runat="server" />
                    </td>
                    <td style="text-align: left;" colspan="2">Convenio<br />
                        <asp:DropDownList ID="ddlConvenio" runat="server" CssClass="textbox"
                            Width="250px" AutoPostBack="True"
                            OnSelectedIndexChanged="ddlConvenio_SelectedIndexChanged" />
                    </td>
                    <td colspan="2" style="text-align: left;">Oficina<br />
                        <asp:DropDownList ID="ddlOficina" runat="server" CssClass="textbox"
                            ReadOnly="True" Width="200px" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align: left;">Num. Tarjeta<br />
                        <asp:DropDownList ID="ddlNumtarjeta" runat="server" CssClass="textbox"
                            ReadOnly="True" Width="180px" />
                        <asp:TextBox ID="txtTarjeta" runat="server" CssClass="textbox" Enabled="False"
                            Width="120px" />
                    </td>
                    <td colspan="2" style="text-align: left;">
                        <asp:CheckBox ID="chkCobraCuota" runat="server" Text="Cobra Cuota Manejo" />
                        &nbsp; Valor<br />
                        <uc1:decimales ID="txtCuota" runat="server" />
                    </td>
                    <td style="text-align: left;" colspan="2">
                        <br />
                        Estado de la Tarjeta<br />
                        <asp:DropDownList ID="ddlEstado" runat="server" CssClass="textbox"
                            Height="25px" Width="138px" />
                    </td>
                </tr>
                <tr>
                    <td colspan="5" style="font-size: x-small; text-align: left">
                        <strong>Parámetros del Convenio</strong>
                        <hr style="width: 100%" />
                    </td>
                    <td style="font-size: x-small; text-align: left;">&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="5" style="font-size: x-small; text-align: left">
                        <strong>CAJERO AUTOMÁTICO </strong>
                    </td>
                    <td style="font-size: x-small; text-align: left;">&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="3" style="text-align: left">Cupo Máximo<br />
                        <uc1:decimales ID="txtCupoMaximo" runat="server" Enabled="False" />
                    </td>
                    <td colspan="2" style="text-align: left;"># Max Transacciones<br />
                        <asp:TextBox ID="txtMaxTransacciones" runat="server" CssClass="textbox"
                            MaxLength="5" Style="text-align: right" Width="120px" />
                        <asp:FilteredTextBoxExtender ID="txtMaxTransacciones_FilteredTextBoxExtender"
                            runat="server" FilterType="Custom, Numbers"
                            TargetControlID="txtMaxTransacciones" ValidChars="" />
                    </td>
                    <td style="text-align: left;">&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="3" style="text-align: left">
                        <strong>DATAFONOS </strong>
                    </td>
                    <td colspan="2" style="text-align: left;">&nbsp;</td>
                    <td style="text-align: left;">&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align: left;">Cupo Máximo<uc1:decimales ID="TxtCupoMaxDatafono" runat="server"
                        Enabled="False" />
                    </td>
                    <td style="text-align: left;">&nbsp;</td>
                    <td colspan="2" style="text-align: left;"># Max Transacciones<br />
                        <asp:TextBox ID="txtnummaxtransaDataf" runat="server" CssClass="textbox"
                            MaxLength="5" Style="text-align: right" Width="120px" Enabled="False" />
                        <asp:FilteredTextBoxExtender ID="txtnummaxtransaDataf_FilteredTextBoxExtender"
                            runat="server" FilterType="Custom, Numbers"
                            TargetControlID="txtnummaxtransaDataf" ValidChars="" />
                    </td>
                    <td style="text-align: left;">&nbsp;</td>
                </tr>
            </table>
        </asp:View>
    </asp:MultiView>

    <uc4:mensajegrabar ID="ctlMensaje" runat="server" />

</asp:Content>
