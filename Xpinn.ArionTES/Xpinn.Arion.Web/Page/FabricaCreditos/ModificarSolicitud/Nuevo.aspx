<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/solicitud.master" AutoEventWireup="true" CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/direccion.ascx" TagName="direccion" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc2" %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="uc3" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>
<%@ Register Src="~/General/Controles/ctlBusquedaRapida.ascx" TagName="ListadoPersonas" TagPrefix="uc1" %>
<%@ Register
    Src="~/General/Controles/ctlValidarBiometria.ascx" TagName="validarBiometria" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <asp:MultiView ID="mvDatosSolicitud" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwSolicitudCredito" runat="server">
            <%--            <asp:UpdatePanel ID="updSolicitud" runat="server" UpdateMode="Always">
            <ContentTemplate>    --%>
            <table cellpadding="5" cellspacing="0" width="85%">
                <tr>
                    <td class="tdI" style="text-align: center">
                        <asp:Panel ID="Panel4" runat="server">
                            <table style="width: 100%;">
                                <tr>
                                    <td style="text-align: center; color: #FFFFFF; background-color: #0066FF; height: 20px;"
                                        colspan="5">&nbsp;<strong style="text-align: center">Modificacion De La Solicitud De Credito</strong></td>
                                </tr>
                                <tr>
                                    <td>Número de Solicitud<br />
                                        <asp:TextBox ID="lblNumero" runat="server" CssClass="textbox" Enabled="False"
                                            Width="153px"></asp:TextBox>
                                        <asp:TextBox ID="txtCliente" runat="server" CssClass="textbox" Visible="False"
                                            Width="153px"></asp:TextBox>
                                    </td>
                                    <td>Número de Radicación<br />
                                        <asp:TextBox ID="lblradicacion" runat="server" CssClass="textbox" Enabled="False"
                                            Width="153px"></asp:TextBox>
                                        <asp:TextBox ID="txtradicacion" runat="server" CssClass="textbox" Visible="False"
                                            Width="153px"></asp:TextBox>
                                    </td>
                                    <td>Fecha de Solicitud *<br />
                                        <asp:TextBox ID="lblFecha" runat="server" CssClass="textbox" Enabled="False"
                                            Width="153px"></asp:TextBox>
                                    </td>
                                    <td>Oficina<br />
                                        <asp:TextBox ID="lblOficina" runat="server" CssClass="textbox" Enabled="False"
                                            Width="153px"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td class="tdI" style="text-align: right">
                        <asp:Panel ID="Panel5" runat="server" Height="263px">
                            <table style="width: 100%; text-align: left; height: 500px; margin-right: 9px;">
                                <tr>
                                    <td align="left" colspan="3">
                                        <asp:Label ID="lblMensajeValidacion" runat="server"
                                            Style="font-size: xx-small; color: #FF0000; text-align: left"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" colspan="5" style="color: #FFFFFF; background-color: #0066FF">
                                        <strong>Condiciones Solicitadas Del Crédito</strong>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>Tipo de Crédito<br />
                                        <asp:DropDownList ID="ddlTipoCredito" runat="server" AutoPostBack="True"
                                            OnSelectedIndexChanged="ddlTipoCredito_SelectedIndexChanged" Width="250px"
                                            CssClass="textbox">
                                        </asp:DropDownList>
                                    </td>
                                    <td>Monto Máximo<br />
                                        <asp:TextBox ID="txtMontoMaximo" runat="server" CssClass="textbox"
                                            Enabled="False" Width="153px"></asp:TextBox>
                                    </td>
                                    <td>Plazo Máximo<br />
                                        <asp:TextBox ID="txtPlazoMaximo" runat="server" CssClass="textbox"
                                            Enabled="False" Width="136px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Monto Solicitado<br />
                                        <uc2:decimales ID="txtMonto" runat="server" />
                                    </td>
                                    <td>Plazo<br />
                                        <uc2:decimales ID="txtPlazo" runat="server" AutoPostBack_="false" />
                                        <br />
                                    </td>
                                    <td>Periodicidad<br />
                                        <asp:DropDownList ID="ddlPeriodicidad" runat="server" CssClass="textbox"
                                            Width="148px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td># Periodo de Gracia<br />
                                        <uc2:decimales ID="txtPeriodo" runat="server" />
                                    </td>
                                    <td>Cuota Deseada<br />
                                        <uc2:decimales ID="txtCuota" runat="server" />
                                    </td>
                                    <td>Medio por el Cual se Entero de la Entidad<br />
                                        <asp:DropDownList ID="ddlMedio" runat="server" AutoPostBack="True" CssClass="textbox"
                                            OnSelectedIndexChanged="ddlMedio_SelectedIndexChanged"
                                            Width="163px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">Destino del Crédito<br />
                                        <asp:TextBox ID="txtConcepto" runat="server" CssClass="textbox" MaxLength="50"
                                            Width="500px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblCual" runat="server" Text="¿Cual?"></asp:Label>
                                        <br />
                                        <asp:TextBox ID="txtOtro" runat="server" CssClass="textbox" MaxLength="20"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:CheckBox ID="Checkgarantia_real" runat="server" />
                                        <asp:Label ID="Label1" runat="server" Text=" ¿Requiere Garantía Real?"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="Checkgarantia_comunitaria" runat="server" Enabled="False" />
                                        <asp:Label ID="Label2" runat="server" Text=" ¿Fondo de Garantías Comunitarias?"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="Checkpoliza" runat="server" />
                                        <asp:Label ID="Label3" runat="server" Text=" ¿Desea tomar Poliza Microseguros?"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Asesor Comercial<br />
                                        <asp:DropDownList ID="Ddlusuarios" runat="server" CssClass="textbox"
                                            Width="260px">
                                        </asp:DropDownList>
                                        <span style="font-size: xx-small">
                                            <asp:RequiredFieldValidator
                                                ID="rfvAsesor" runat="server"
                                                ControlToValidate="Ddlusuarios" Display="Dynamic"
                                                ErrorMessage="Campo Requerido" ForeColor="Red" SetFocusOnError="True"
                                                Style="font-size: x-small" ValidationGroup="vgGuardar"
                                                InitialValue="&lt;Seleccione un Item&gt;" />
                                        </span>
                                    </td>
                                    <td>Valor Fondo Garantías<br />
                                        <asp:TextBox ID="txt_ValorGaran" runat="server" CssClass="textbox" Visible="false" Width="153px"></asp:TextBox>
                                    </td>
                                    <td>Forma de Pago<br />
                                        <asp:DropDownList ID="ddlFormaPago" runat="server" Width="225px"
                                            CssClass="textbox" AutoPostBack="True"
                                            OnSelectedIndexChanged="ddlFormaPago_SelectedIndexChanged">
                                            <asp:ListItem Value="1">Caja</asp:ListItem>
                                            <asp:ListItem Value="2">Nomina</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>

                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblEmpresa" runat="server" Text="Empresa"></asp:Label><br />
                                        <asp:DropDownList ID="ddlEmpresa" runat="server" CssClass="textbox"
                                            Width="222px">
                                        </asp:DropDownList>
                                    </td>
                                    <td style="text-align: left;">
                                        <asp:Label ID="LblTipoLiquidacion" Visible="False" runat="server" Text="Tipo de Liquidacion"></asp:Label>
                                        <br />
                                        <asp:DropDownList ID="ddlTipoLiquidacion" runat="server" CssClass="dropdown"
                                            Width="260px" Visible="False">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator
                                            ID="rfvTipoLiquidacion" runat="server"
                                            ControlToValidate="ddlTipoLiquidacion" Display="Dynamic"
                                            ErrorMessage="Campo Requerido" ForeColor="Red" SetFocusOnError="True"
                                            Style="font-size: x-small" ValidationGroup="vgGuardar"
                                            InitialValue="&lt;Seleccione un Item&gt;" />
                                    </td>
                                    <td colspan="2" style="text-align: left;"></td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="text-align: left;">
                                        <span style="font-weight: bold">
                                            <asp:Label ID="lblTitOrden" runat="server" Text="Proveedor para La Orden de Servicio:" /></span>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: left;">
                                        <asp:Label ID="lblTitIdenProveedor" runat="server" Text="Identificación" />
                                        <br />
                                        <asp:TextBox ID="txtIdentificacionprov" runat="server" CssClass="textbox" AutoPostBack="true"
                                            Width="181px" MaxLength="20" OnTextChanged="txtIdentificacionprov_TextChanged"></asp:TextBox>
                                        <cc1:ButtonGrid ID="btnListadoPersona" CssClass="btnListado" runat="server" Text="..."
                                            OnClick="btnListadoPersona_Click" />
                                        <uc1:ListadoPersonas ID="ctlBusquedaPersonas" runat="server" />
                                    </td>
                                    <td colspan="2" style="text-align: left;">
                                        <asp:Label ID="lblTitNomProveedor" runat="server" Text="Nombre " />
                                        <br />
                                        <asp:TextBox ID="txtNombreProveedor" runat="server" CssClass="textbox" Enabled="false"
                                            Width="455px" MaxLength="200"></asp:TextBox>
                                    </td>
                                    <tr>
                                        <td>
                                            <asp:Panel ID="panelCodeudor" runat="server">
                                                <table width="166%">
                                                    <tr>
                                                        <td style="text-align: left; color: #FFFFFF; background-color: #0066FF; height: 20px;" colspan="3">
                                                            <strong>Codeudores</strong>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: left">
                                                            <br />
                                                            <asp:GridView ID="gvListaCodeudores" runat="server" Width="100%" AutoGenerateColumns="False"
                                                                BackColor="White" BorderColor="#DEDFDE" BorderStyle="None"
                                                                BorderWidth="1px" ForeColor="Black" OnRowDeleting="gvListaCodeudores_RowDeleting"
                                                                OnRowEditing="gvListaCodeudores_RowEditing" OnRowCancelingEdit="gvListaCodeudores_RowCancelingEdit"
                                                                OnRowUpdating="gvListaCodeudores_RowUpdating" OnRowCommand="gvListaCodeudores_RowCommand"
                                                                PageSize="5" DataKeyNames="cod_persona" ShowFooter="True" Style="font-size: x-small">
                                                                <Columns>
                                                                    <asp:TemplateField ShowHeader="False">
                                                                        <ItemTemplate>
                                                                            <asp:ImageButton ID="btnEdit" runat="server" CausesValidation="False" CommandName="Edit"
                                                                                ImageUrl="~/Images/gr_edit.jpg" ToolTip="Editar" Width="16px" Height="16px" />
                                                                        </ItemTemplate>
                                                                        <EditItemTemplate>
                                                                            <asp:ImageButton ID="btnSave" runat="server" CausesValidation="False" CommandName="Update"
                                                                                ImageUrl="~/Images/gr_guardar.jpg" ToolTip="Guardar" Width="16px" Height="16px" />
                                                                        </EditItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:ImageButton ID="btnNuevo" runat="server" CausesValidation="False" CommandName="AddNew"
                                                                                ImageUrl="~/Images/gr_nuevo.jpg" ToolTip="Crear Nuevo" Width="16px" Height="16px" />
                                                                        </FooterTemplate>
                                                                        <ItemStyle Width="20px" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField ShowHeader="False">
                                                                        <ItemTemplate>
                                                                            <asp:ImageButton ID="btnDelete" runat="server" CausesValidation="False" CommandName="Delete"
                                                                                ImageUrl="~/Images/gr_elim.jpg" ToolTip="Eliminar" Width="16px" Height="16px" />
                                                                        </ItemTemplate>
                                                                        <EditItemTemplate>
                                                                            <asp:ImageButton ID="btnCancel" runat="server" CausesValidation="False" CommandName="Cancel"
                                                                                ImageUrl="~/Images/gr_cancelar.jpg" ToolTip="Cancelar" Width="16px" Height="16px" />
                                                                        </EditItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <%--<asp:BoundField DataField="cod_persona" HeaderText="Codpersona" />--%>
                                                                    <asp:TemplateField HeaderText="Cod Persona">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblCodPersona" runat="server" Text='<%# Bind("cod_persona") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:Label ID="lblOrden" runat="server" Text='# Orden'></asp:Label>
                                                                            <asp:TextBox ID="txtOdenFooter" runat="server" Style="font-size: x-small; text-align: right" Width="55px" Enabled="false" />
                                                                            <asp:FilteredTextBoxExtender ID="fteOrdenFooter" runat="server" Enabled="True" FilterType="Numbers" TargetControlID="txtOdenFooter" />
                                                                            &nbsp;&nbsp;&nbsp;<asp:Label ID="lblCodPersonaFooter" runat="server"></asp:Label>
                                                                        </FooterTemplate>
                                                                        <ItemStyle Width="170px" />
                                                                        <FooterStyle HorizontalAlign="Center" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Identificación">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblidentificacion" runat="server" Text='<%# Bind("IDENTIFICACION") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:TextBox ID="txtidentificacion" runat="server" Text='<%# Bind("IDENTIFICACION") %>'
                                                                                OnTextChanged="txtidentificacion_TextChanged" Style="font-size: x-small" AutoPostBack="True"></asp:TextBox>
                                                                            <asp:Button ID="btnConsultaPersonas" runat="server" Text="..."
                                                                                Height="26px" OnClick="btnConsultaPersonas_Click" />
                                                                            <uc1:ListadoPersonas ID="ctlBusquedaPersonas" runat="server" />
                                                                        </FooterTemplate>
                                                                        <ItemStyle Width="170px" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Primer Nombre">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblPrimerNombre" runat="server" Text='<%# Bind("PRIMER_NOMBRE") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Segundo Nombre">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblSegundoNombre" runat="server" Text='<%# Bind("SEGUNDO_NOMBRE") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Primer Apellido">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblPrimerApellido" runat="server" Text='<%# Bind("PRIMER_APELLIDO") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Segundo Apellido">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblSegundoApellido" runat="server" Text='<%# Bind("SEGUNDO_APELLIDO") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Dirección">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblDireccionRow" runat="server" Text='<%# Bind("DIRECCION") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Teléfono">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblTelefonoRow" runat="server" Text='<%# Bind("TELEFONO") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Orden">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblOrdenRow" runat="server" Text='<%# Bind("ORDEN") %>' />
                                                                        </ItemTemplate>
                                                                        <EditItemTemplate>
                                                                            <asp:TextBox ID="txtOrdenRow" runat="server" Text='<%# Bind("ORDEN") %>' Width="50px" Style="text-align: right" />
                                                                            <asp:FilteredTextBoxExtender ID="fteOrden" runat="server" Enabled="True" FilterType="Numbers" TargetControlID="txtOrdenRow" />
                                                                        </EditItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Center" />
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                                <FooterStyle CssClass="gridHeader" />
                                                                <HeaderStyle CssClass="gridHeader" />
                                                                <PagerStyle CssClass="gridPager" />
                                                                <RowStyle CssClass="gridItem" />
                                                            </asp:GridView>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: center">
                                                            <asp:Label ID="lblTotReg" runat="server" Visible="False" />
                                                            <asp:Label ID="lblTotalRegsCodeudores" runat="server" Text="No hay codeudores para este crédito."
                                                                Visible="False" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <br />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                <tr>
                                    <td>
                                        <br />
                                        <table style="width: 100%; display: none" >
                                            <tr>
                                                <td style="text-align: left; color: #FFFFFF; background-color: #0066FF; height: 20px;" colspan="3">
                                                    <strong>Referencias</strong>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: left;">
                                                    <asp:UpdatePanel ID="UpdatePanelReferencias" runat="server">
                                                        <ContentTemplate>
                                                            <asp:Button ID="btnAgregarReferencia" runat="server" CssClass="btn8" Text="+ Adicionar Referencia" OnClick="btnAgregarReferencia_Click" />
                                                            <div style="overflow: auto; max-height: 200px;">
                                                                <asp:GridView ID="gvReferencias" runat="server" Width="100%" AutoGenerateColumns="False"
                                                                    AllowPaging="False" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None"
                                                                    OnRowCommand="gvReferencia_OnRowCommand"
                                                                    OnRowDeleting="gvReferencias_RowDeleting"
                                                                    BorderWidth="1px" ForeColor="Black"
                                                                    Style="font-size: x-small; overflow: auto;">
                                                                    <Columns>
                                                                        <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/gr_elim.jpg" ShowDeleteButton="True">
                                                                            <ItemStyle Width="16px" />
                                                                        </asp:CommandField>
                                                                        <asp:TemplateField HeaderText="Quien Referencia" ItemStyle-Width="10%">
                                                                            <ItemTemplate>
                                                                                <cc1:DropDownListGrid ID="ddlQuienReferencia" runat="server" CssClass="textbox" Width="95%">
                                                                                </cc1:DropDownListGrid>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Tipo Referencia">
                                                                            <ItemTemplate>
                                                                                <cc1:DropDownListGrid ID="ddlTipoReferencia" runat="server" CommandArgument='<%#((GridViewRow) Container).RowIndex %>' CssClass="textbox" AppendDataBoundItems="true" AutoPostBack="true"
                                                                                    SelectedValue='<%# Bind("tiporeferencia") %>' OnSelectedIndexChanged="ddlTipoReferencia_SelectedIndexChanged" Width="95%">
                                                                                    <asp:ListItem Value="2" Text="Personal" />
                                                                                    <asp:ListItem Value="1" Text="Familiar" />
                                                                                    <asp:ListItem Value="3" Text="Comercial" />
                                                                                </cc1:DropDownListGrid>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Nombres">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txtNombres" Text='<%# Bind("nombres") %>' runat="server" Style="font-size: x-small" CssClass="textbox" Width="95%" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Parentesco" ItemStyle-Width="10%">
                                                                            <ItemTemplate>
                                                                                <cc1:DropDownListGrid ID="ddlParentesco" runat="server" CssClass="textbox" AppendDataBoundItems="true" Width="95%"
                                                                                    DataSource="<%#ListarParentesco() %>" DataTextField="ListaDescripcion" DataValueField="ListaId"
                                                                                    SelectedValue='<%# Bind("codparentesco") %>'>
                                                                                    <asp:ListItem Value="0" Text="" />
                                                                                </cc1:DropDownListGrid>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Dirección" ItemStyle-Width="16%">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txtDireccion" Text='<%# Bind("direccion") %>' runat="server" CssClass="textbox" Style="font-size: x-small" Width="95%" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Teléfono" ItemStyle-Width="8%">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txtTelefono" Text='<%# Bind("telefono") %>' onkeypress="return isNumber(event)" runat="server" CssClass="textbox" Style="font-size: x-small" Width="95%" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Tel.Oficina" ItemStyle-Width="8%">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txtTelOficina" Text='<%# Bind("teloficina") %>' onkeypress="return isNumber(event)" runat="server" CssClass="textbox" Style="font-size: x-small" Width="95%" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Celular" ItemStyle-Width="8%">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txtCelular" Text='<%# Bind("celular") %>' onkeypress="return isNumber(event)" runat="server" CssClass="textbox" Style="font-size: x-small" Width="90%" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                    <FooterStyle CssClass="gridHeader" />
                                                                    <HeaderStyle CssClass="gridHeader" />
                                                                    <PagerStyle CssClass="gridPager" />
                                                                    <RowStyle CssClass="gridItem" />
                                                                </asp:GridView>
                                                            </div>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="6" style="text-align: left">
                                        <asp:UpdatePanel ID="UpdatePanelCuoExt" runat="server">
                                            <ContentTemplate>
                                                <asp:Panel runat="server" ID="panelCuotasExtras">
                                                    <table style="width: 100%">
                                                        <tr>
                                                            <td style="text-align: left; color: #FFFFFF; background-color: #0066FF; height: 20px;" colspan="3">
                                                                <strong>Cuotas Extras</strong>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="text-align: left;">
                                                                <asp:GridView ID="gvCuoExt" AutoPostBack="True" runat="server" Width="40%" ShowHeaderWhenEmpty="True"
                                                                    EmptyDataText="No se encontraron registros." AutoGenerateColumns="False" PageSize="5"
                                                                    BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px"
                                                                    CellPadding="4" ForeColor="Black" GridLines="Vertical" Height="16px" ShowFooter="True"
                                                                    Style="font-size: x-small" OnRowDataBound="gvCuoExt_RowDataBound"
                                                                    OnRowDeleting="gvCuoExt_RowDeleting" OnRowCommand="gvCuoExt_RowCommand" OnPageIndexChanging="gvCuoExt_PageIndexChanging">
                                                                    <AlternatingRowStyle BackColor="White" />
                                                                    <Columns>

                                                                        <asp:TemplateField>
                                                                            <FooterTemplate>
                                                                                <asp:ImageButton ID="btnNuevo" runat="server" CausesValidation="False" CommandName="AddNew"
                                                                                    ImageUrl="~/Images/gr_nuevo.jpg" ToolTip="Crear Nuevo" Width="16px" />
                                                                            </FooterTemplate>
                                                                            <ItemStyle Width="20px" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField>
                                                                            <ItemTemplate>
                                                                                <asp:ImageButton ID="btnEliminar" runat="server" CommandName="Delete" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                                                                    ImageUrl="~/Images/gr_elim.jpg" ToolTip="Eliminar" Width="16px" />
                                                                            </ItemTemplate>
                                                                            <HeaderStyle CssClass="gridIco" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Fecha Pago">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="lblfechapago" runat="server" Text='<%# Bind("fecha_pago","{0:d}") %>'></asp:TextBox>
                                                                                <asp:CalendarExtender ID="calExtfechapago" Format="dd/MM/yyyy" runat="server" TargetControlID="lblfechapago" />
                                                                            </ItemTemplate>
                                                                            <FooterTemplate>
                                                                                <asp:TextBox ID="txtfechapago" runat="server" Text='<%# Bind("fecha_pago") %>'></asp:TextBox>
                                                                                <asp:CalendarExtender ID="calExtfechapago" Format="dd/MM/yyyy" runat="server" TargetControlID="txtfechapago" />
                                                                            </FooterTemplate>
                                                                            <ItemStyle Width="50px" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Forma Pago">
                                                                            <ItemTemplate>
                                                                                <asp:DropDownList ID="ddlformapago" runat="server" Width="164px" CssClass="dropdown">
                                                                                    <asp:ListItem Value="1">Caja</asp:ListItem>
                                                                                    <asp:ListItem Value="2">Nomina</asp:ListItem>
                                                                                </asp:DropDownList>
                                                                            </ItemTemplate>
                                                                            <FooterTemplate>
                                                                                <asp:DropDownList ID="ddlformapago" runat="server" Width="164px" CssClass="dropdown">
                                                                                    <asp:ListItem Value="1">Caja</asp:ListItem>
                                                                                    <asp:ListItem Value="2">Nomina</asp:ListItem>
                                                                                </asp:DropDownList>
                                                                            </FooterTemplate>
                                                                            <ItemStyle Width="50px" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Valor">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="lblvalor" runat="server" Text='<%# Bind("valor") %>'></asp:TextBox>
                                                                                <asp:FilteredTextBoxExtender ID="txtValor_FilteredTextBoxExtender" runat="server"
                                                                                    Enabled="True" FilterType="Numbers, Custom" TargetControlID="lblvalor" ValidChars="." />
                                                                            </ItemTemplate>
                                                                            <FooterTemplate>
                                                                                <asp:TextBox ID="txtvalor" runat="server" Text='<%# Bind("valor") %>'></asp:TextBox>
                                                                                <asp:FilteredTextBoxExtender ID="txtValor_FilteredTextBoxExtender" runat="server"
                                                                                    Enabled="True" FilterType="Numbers, Custom" TargetControlID="txtvalor" ValidChars="." />
                                                                            </FooterTemplate>
                                                                            <ItemStyle Width="50px" />
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Tipo Cuota Extra">
                                                                            <ItemTemplate>
                                                                                <asp:DropDownList ID="ddltipocuotagv" runat="server" Width="164px" CssClass="dropdown">
                                                                                </asp:DropDownList>
                                                                            </ItemTemplate>
                                                                            <FooterTemplate>
                                                                                <asp:DropDownList ID="ddltipocuotagv" runat="server" Width="164px" CssClass="dropdown">
                                                                                </asp:DropDownList>
                                                                            </FooterTemplate>
                                                                            <ItemStyle Width="50px" />
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Cod.Forma Pago" Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblcodformapago" runat="server" Text='<%# Bind("forma_pago") %>'></asp:Label>
                                                                            </ItemTemplate>
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
                                                                <br />
                                                                <asp:Label runat="server" ID="lblError" ForeColor="Red"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>

                                <tr>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>

                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>



            </table>

            <%--            </ContentTemplate>
            <Triggers>
            <asp:AsyncPostBackTrigger ControlID="ddlFormaPago" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlMedio" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlTipoCredito" EventName="TextChanged" />
            </Triggers>
            </asp:UpdatePanel>  --%>
        </asp:View>
        <asp:View ID="vwCreditosRecoger" runat="server">
            <table style="width: 100%" style="display: none">
                <tr>
                    <td style="text-align: center">
                        <strong>Creditos A Recoger</strong><hr />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center;">
                        <asp:GridView ID="gvListaSolicitudCreditosRecogidos" runat="server"
                            AllowPaging="True" AutoGenerateColumns="False" DataKeyNames="numero_credito"
                            GridLines="Horizontal"
                            OnPageIndexChanging="gvListaSolicitudCreditosRecogidos_PageIndexChanging"
                            OnRowCommand="gvListaSolicitudCreditosRecogidos_RowCommand"
                            OnRowDataBound="gvListaSolicitudCreditosRecogidos_RowDataBound"
                            OnRowDeleting="gvListaSolicitudCreditosRecogidos_RowDeleting"
                            OnRowEditing="gvListaSolicitudCreditosRecogidos_RowEditing"
                            OnSelectedIndexChanged="gvListaSolicitudCreditosRecogidos_SelectedIndexChanged"
                            PageSize="5" Width="100%">
                            <Columns>
                                <asp:BoundField DataField="numero_credito">
                                    <HeaderStyle CssClass="gridColNo" />
                                    <ItemStyle CssClass="gridColNo" />
                                </asp:BoundField>
                                <asp:BoundField DataField="numero_credito" HeaderText="Número de crédito" />
                                <asp:BoundField DataField="linea_credito" HeaderText="Línea">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="monto" DataFormatString="{0:n}" HeaderText="Monto">
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="saldo_capital" DataFormatString="{0:N0}"
                                    HeaderText="Saldo capital">
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="interes_corriente" DataFormatString="{0:N0}"
                                    HeaderText="Interés corriente">
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="interes_mora" DataFormatString="{0:N0}"
                                    HeaderText="Interés mora">
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="otros" DataFormatString="{0:n0}" HeaderText="Otros">
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="leymipyme" HeaderText="LeymiPyme" />
                                <asp:BoundField DataField="iva_leymipyme" HeaderText="Iva LeyMiPyme" />
                                <asp:BoundField DataField="valor_total" DataFormatString="{0:n}"
                                    HeaderText="Valor Total">
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="cuotas_pagadas" HeaderText="Cuo.Pag">
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Recoger">
                                    <ItemTemplate>
                                        <cc1:CheckBoxGrid ID="chkRecoger" runat="server" Checked='<%# Eval("Recoger") %>' AutoPostBack="true"
                                            OnCheckedChanged="chkRecoger_CheckedChanged" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <HeaderStyle CssClass="gridHeader" />
                            <PagerStyle CssClass="gridPager" />
                            <RowStyle CssClass="gridItem" />
                        </asp:GridView>
                        <hr />
                        <br />
                        ¿Requiere Codeudores?<br />
                        <asp:Button ID="btnSi0" runat="server" Text="Si" />
                        &nbsp;
                        <asp:Button ID="btnNo0" runat="server" Text="No" />
                        <br />
                        <asp:Label ID="lblMensajeCredRecogidos" runat="server"></asp:Label>
                        <asp:Label ID="lblTotalRegsSolicitudCreditosRecogidos" runat="server"
                            Text="Su consulta no obtuvo ningún resultado." Visible="False"></asp:Label>
                        <br />
                    </td>
                </tr>
            </table>
        </asp:View>
        <asp:View ID="vwMensaje" runat="server">
            <table style="width: 100%">
                <tr>
                    <td style="text-align: center">
                        <hr />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center;">
                        <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                        <br />
                        <br />
                        Datos Modificados Correctamente
                    </td>
                </tr>

            </table>
        </asp:View>
    </asp:MultiView>

    <uc1:validarBiometria ID="ctlValidarBiometria" runat="server" />

</asp:Content>
