<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" Title=".: Xpinn - Parámetros ACH :." %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="Fecha" TagPrefix="ucFecha" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:HiddenField ID="idObjetoReg" runat="server" />
    <asp:HiddenField ID="idObjetoCam" runat="server" />
    <asp:MultiView ID="mvACH" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwPlantilla" runat="server">
            <table border="0" cellpadding="0" cellspacing="0" width="50%">
                <tr>
                    <td class="tdI" style="text-align: left">
                        Código&nbsp;*&nbsp;<asp:RequiredFieldValidator ID="rfvtipopago" runat="server" ControlToValidate="txtCodigo"
                            ErrorMessage="Campo Requerido" SetFocusOnError="True" ValidationGroup="vgGuardar"
                            ForeColor="Red" Display="Dynamic" style="font-size:x-small"/><br />
                        <asp:TextBox ID="txtCodigo" runat="server" CssClass="textbox" MaxLength="128" />
                         <asp:FilteredTextBoxExtender ID="fte1" 
                             runat="server" Enabled="True" FilterType="Numbers, Custom"
                             TargetControlID="txtCodigo" ValidChars="" />
                    </td>
                    <td class="tdD" style="text-align: left">
                        Fecha&nbsp;*&nbsp;<br />
                        <ucFecha:Fecha ID="txtFecha" runat="server" Enabled="true" />
                    </td>
                    <td class="tdD" style="text-align: left">
                        <asp:CheckBox ID="cbEstado" runat="server" Text="Activa"></asp:CheckBox>
                    </td>
                </tr>
                <tr>
                    <td class="tdI" style="text-align: left" colspan="3">
                        Nombre&nbsp;*&nbsp;<asp:RequiredFieldValidator ID="rfvNombre" runat="server" ControlToValidate="txtNombre"
                            ErrorMessage="Campo Requerido" SetFocusOnError="True" ValidationGroup="vgGuardar"
                            ForeColor="Red" Display="Dynamic" style="font-size:x-small"/><br />
                        <asp:TextBox ID="txtNombre" runat="server" CssClass="textbox" MaxLength="128" Width="519px" />
                    </td>
                    <td class="tdD">
                    </td>
                </tr>
                <tr>
                    <td class="tdI" style="text-align: left" colspan="3">
                        Descripción&nbsp;*&nbsp;<asp:RequiredFieldValidator ID="rfvDescripcion" runat="server"
                            ControlToValidate="txtDescripcion" ErrorMessage="Campo Requerido" SetFocusOnError="True"
                            ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic" style="font-size:x-small"/><br />
                        <asp:TextBox ID="txtDescripcion" runat="server" CssClass="textbox" MaxLength="128"
                            Width="519px" />
                    </td>
                    <td class="tdD">
                    </td>
                </tr>
                <tr>
                    <td class="tdI" style="text-align: left" colspan="3">
                        Banco&nbsp;*&nbsp;<br />
                        <asp:DropDownList ID="ddlEntidad" runat="server" CssClass="textbox" 
                            Width="520px" />
                    </td>
                    <td class="tdD">
                    </td>
                </tr>
            </table>
            <asp:Panel ID="pLista" runat="server" Width="45%">
                <strong style="text-align: left">Registros de la Plantilla</strong><br />
                <asp:Button ID="btnDetalle" runat="server" CssClass="btn8" OnClick="btnDetalle_Click"
                    OnClientClick="btnDetalle_Click" Text="+ Adicionar Detalle" />
                <asp:Button ID="btnCrearRegistro" runat="server" CssClass="btn8" OnClick="btnCrearRegistro_Click"
                    OnClientClick="btnCrearRegistro_Click" Text="+ Crear Registro" />
                <br />
                <asp:GridView ID="gvLista" runat="server" Width="100%" AutoGenerateColumns="False"
                    AllowPaging="True" PageSize="20" GridLines="Horizontal" ShowHeaderWhenEmpty="True"
                    DataKeyNames="codigo" OnRowEditing="gvLista_RowEditing" Style="font-size: x-small"
                    OnRowDeleting="gvLista_RowDeleting">
                    <Columns>
                        <asp:TemplateField HeaderStyle-CssClass="gridIco">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnEditar" runat="server" CommandName="Edit" ImageUrl="~/Images/gr_edit.jpg"
                                    ToolTip="Editar" Width="16px" />
                            </ItemTemplate>
                            <HeaderStyle CssClass="gridIco"></HeaderStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-CssClass="gridIco">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnEliminar" runat="server" CommandName="Delete" ImageUrl="~/Images/gr_elim.jpg"
                                    ToolTip="Eliminar" Width="16px" />
                            </ItemTemplate>
                            <HeaderStyle CssClass="gridIco"></HeaderStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Registro" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <cc1:DropDownListGrid ID="ddlRegistro" runat="server" Style="font-size: x-small;
                                    text-align: left" AutoPostBack="True" CssClass="dropdown" Width="500px" DataSource="<%# ListaRegistros() %>"
                                    DataTextField="nombre" DataValueField="codigo" SelectedValue='<%# Bind("codigo") %>'
                                    CommandArgument='<%#Container.DataItemIndex %>' AppendDataBoundItems="True">
                                    <asp:ListItem Value="-1"></asp:ListItem>
                                </cc1:DropDownListGrid>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                    </Columns>
                    <HeaderStyle CssClass="gridHeader" />
                    <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                    <RowStyle CssClass="gridItem" />
                </asp:GridView>
                <br />
            </asp:Panel>
        </asp:View>
        <asp:View ID="vwRegistro" runat="server">
            <table border="0" cellpadding="0" cellspacing="0" width="50%">
                <tr>
                    <td class="tdI" style="text-align: left">
                        Código&nbsp;*&nbsp;<asp:RequiredFieldValidator ID="rfvCodigoReg" runat="server" ControlToValidate="txtCodigoReg"
                            ErrorMessage="Campo Requerido" SetFocusOnError="True" ValidationGroup="vgGuardarReg"
                            ForeColor="Red" Display="Dynamic" Style="font-size: x-small" /><br />
                        <asp:TextBox ID="txtCodigoReg" runat="server" CssClass="textbox" MaxLength="8" Width="100px" />
                         <asp:FilteredTextBoxExtender ID="fte2" runat="server" Enabled="True" FilterType="Numbers, Custom"
                                    TargetControlID="txtCodigoReg" ValidChars="" />
                    </td>
                    <td class="tdI" colspan="2" style="text-align: left">
                        Tipo&nbsp;*&nbsp;<asp:RequiredFieldValidator ID="rfvTipoReg" runat="server" ControlToValidate="ddlTipoReg"
                            ErrorMessage="Campo Requerido" SetFocusOnError="True" ValidationGroup="vgGuardarReg"
                            ForeColor="Red" Display="Dynamic" Style="font-size: x-small" /><br />
                        <asp:DropDownList ID="ddlTipoReg" runat="server" Style="font-size: x-small; text-align: left"
                            AutoPostBack="True" CssClass="textbox" Width="150px" AppendDataBoundItems="True"
                            Visible="True">
                            <asp:ListItem Value="">Seleccione un Item</asp:ListItem>
                            <asp:ListItem Value="1">Encabezado</asp:ListItem>
                            <asp:ListItem Value="6">Control</asp:ListItem>
                            <asp:ListItem Value="2">Detalle Transacción</asp:ListItem>
                            <asp:ListItem Value="3">Detalle Prenotificación</asp:ListItem>
                            <asp:ListItem Value="4">Addenda</asp:ListItem>
                            <asp:ListItem Value="5">Fin Archivo</asp:ListItem>                       
                        </asp:DropDownList>
                    </td>
                    <td class="tdI" style="text-align: left">
                        Separador&nbsp;*&nbsp;<asp:RequiredFieldValidator ID="rfvSeparadorReg" runat="server"
                            ControlToValidate="txtSeparadorReg" ErrorMessage="Campo Requerido" SetFocusOnError="True"
                            ValidationGroup="vgGuardarReg" ForeColor="Red" Display="Dynamic" Style="font-size: x-small" /><br />
                        <asp:TextBox ID="txtSeparadorReg" runat="server" CssClass="textbox" MaxLength="1"
                            Width="50px" />
                    </td>
                </tr>
                <tr>
                    <td class="tdI" style="text-align: left" colspan="4">
                        Nombre&nbsp;*&nbsp;<asp:RequiredFieldValidator ID="rfvNombreReg" runat="server" ControlToValidate="txtNombreReg"
                            ErrorMessage="Campo Requerido" SetFocusOnError="True" ValidationGroup="vgGuardarReg"
                            ForeColor="Red" Display="Dynamic" Style="font-size: x-small" /><br />
                        <asp:TextBox ID="txtNombreReg" runat="server" CssClass="textbox" MaxLength="128"
                            Width="500px" />
                    </td>
                </tr>
            </table>
            <asp:Panel ID="pCamposRegistro" runat="server" Width="70%">
                <strong style="text-align: left">Campos del Registro</strong><br />
                <asp:Button ID="btnDetalleReg" runat="server" CssClass="btn8" OnClick="btnDetalleReg_Click"
                    OnClientClick="btnDetalleReg_Click" Text="+ Adicionar Campo" />
                <asp:Button ID="btnCrearCampo" runat="server" CssClass="btn8" OnClick="btnCrearCampo_Click"
                    OnClientClick="btnCrearCampo_Click" Text="+ Crear Campo" />
                <br />
                <div id="divRegistro" style="overflow: scroll;">
                    <asp:GridView ID="gvListaReg" runat="server" Width="100%" AutoGenerateColumns="False"
                        AllowPaging="False" PageSize="20" GridLines="Horizontal" ShowHeaderWhenEmpty="True"
                        DataKeyNames="codigo" OnRowEditing="gvListaReg_RowEditing" Style="font-size: x-small"
                        OnRowDeleting="gvListaReg_RowDeleting">
                        <Columns>
                            <asp:TemplateField HeaderStyle-CssClass="gridIco">
                                <ItemTemplate>
                                    <asp:ImageButton ID="btnEditar" runat="server" CommandName="Edit" ImageUrl="~/Images/gr_edit.jpg"
                                        ToolTip="Editar" Width="16px" />
                                </ItemTemplate>
                                <HeaderStyle CssClass="gridIco"></HeaderStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-CssClass="gridIco">
                                <ItemTemplate>
                                    <asp:ImageButton ID="btnEliminar" runat="server" CommandName="Delete" ImageUrl="~/Images/gr_elim.jpg"
                                        ToolTip="Eliminar" Width="16px" />
                                </ItemTemplate>
                                <HeaderStyle CssClass="gridIco"></HeaderStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Campo" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <cc1:DropDownListGrid ID="ddlCampo" runat="server" Style="font-size: x-small; text-align: left"
                                        AutoPostBack="True" CssClass="dropdown" Width="500px" DataSource="<%# ListaCampos() %>"
                                        DataTextField="nombre" DataValueField="codigo" SelectedValue='<%# Bind("codigo") %>'
                                        CommandArgument='<%#Container.DataItemIndex %>' AppendDataBoundItems="True">
                                        <asp:ListItem Value="-1"></asp:ListItem>
                                    </cc1:DropDownListGrid>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Orden" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtOrden" runat="server" CssClass="dropdown" MaxLength="8" Width="50px"
                                        Text='<%# Bind("orden") %>' />
                                        <asp:FilteredTextBoxExtender ID="fte3" runat="server" Enabled="True" FilterType="Numbers, Custom"
                                    TargetControlID="txtOrden" ValidChars="" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                        </Columns>
                        <HeaderStyle CssClass="gridHeader" />
                        <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                        <RowStyle CssClass="gridItem" />
                    </asp:GridView>
                </div>
                &nbsp;<br />
            </asp:Panel>
        </asp:View>
        <asp:View ID="vwCampo" runat="server">
            <asp:UpdatePanel runat="server">
                <ContentTemplate>
                    <table border="0" cellpadding="2" cellspacing="0" width="70%">
                        <tr>
                            <td class="tdI" style="text-align: left" colspan="3">
                                Código&nbsp;*&nbsp;<asp:RequiredFieldValidator ID="rfvCodigoCam" runat="server" ControlToValidate="txtCodigoCam"
                                    ErrorMessage="Campo Requerido" SetFocusOnError="True" ValidationGroup="vgGuardarCam"
                                    ForeColor="Red" Display="Dynamic" Style="font-size: x-small" /><br />
                                <asp:TextBox ID="txtCodigoCam" runat="server" CssClass="textbox" MaxLength="8" Width="100px" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdI" style="text-align: left" colspan="3">
                                Nombre&nbsp;*&nbsp;<asp:RequiredFieldValidator ID="rfvNombreCam" runat="server" ControlToValidate="txtNombreCam"
                                    ErrorMessage="Campo Requerido" SetFocusOnError="True" ValidationGroup="vgGuardarCam"
                                    ForeColor="Red" Display="Dynamic" Style="font-size: x-small" /><br />
                                <asp:TextBox ID="txtNombreCam" runat="server" CssClass="textbox" MaxLength="128"
                                    Width="500px" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdI" style="text-align: left" colspan="3">
                                <div style="text-align: left; font-size:xx-small">
                                    Campos Fijos <br />
                                    <strong>
                                    :NUM_GIRO :FEC_REG :COD_PERSONA :IDENTIFICACION :NOMBRE :COD_OPE :NUM_COMP
                                    :TIPO_COMP :FORMA_PAGO :BANCO_GIRO :CTA_BANCO_GIRO :BANCO_DESTINO :CTA_BANCO_DESTINO
                                    :VALOR_GIRO :ESTADO :NUM_REGISTROS :SUM_CREDITOS :TIPO_CUENTA_ORIGEN :TIPO_CUENTA_DESTINO 
                                    :FECHA_APLICACION :FECHA_TRANSACCION :TIPO_TRANSACCION :CONCEPTO :RELLENO :COD_OFICINA_CTA_ORIGEN
                                    :TIPO_IDENTIFICACION_DESTINO :TIPO_IDENTIFICACION_ORIGEN :CODIGO_COMPENSACION_BANCO_DESTINO
                                    :CONTADOR
                                    </strong>
                                </div>
                                <asp:RadioButtonList ID="rbTipoCam" runat="server" RepeatDirection="Horizontal" AutoPostBack="True"
                                    ValidationGroup="vgGuardarCam" OnSelectedIndexChanged="rbTipoCam_SelectedIndexChanged">
                                    <asp:ListItem Value="1">Constante</asp:ListItem>
                                    <asp:ListItem Value="2">Sentencia SQL</asp:ListItem>
                                </asp:RadioButtonList>
                                <asp:RequiredFieldValidator ID="rfvTipoCam" runat="server" ControlToValidate="rbTipoCam"
                                    ErrorMessage="Campo Requerido" SetFocusOnError="True" ValidationGroup="vgGuardarCam"
                                    ForeColor="Red" Display="Dynamic" Style="font-size: x-small" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdI" style="text-align: left" colspan="3">
                                <asp:Label ID="lblTitValorCam" runat="server" Text="Valor"></asp:Label>
                                &nbsp;*&nbsp;
                                <asp:RequiredFieldValidator ID="rfvValorCam" runat="server" ControlToValidate="txtSeparadorReg"
                                    ErrorMessage="Campo Requerido" SetFocusOnError="True" ValidationGroup="vgGuardarCam"
                                    ForeColor="Red" Display="Dynamic" Style="font-size: x-small" /><br />
                                <asp:TextBox ID="txtValorCam" runat="server" CssClass="textbox" MaxLength="5000"
                                    Width="500px" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdI" style="text-align: left">
                                Tipo de Dato&nbsp;*&nbsp;<asp:RequiredFieldValidator ID="rfvTipoDatoCam" runat="server"
                                    ControlToValidate="ddlTipoDatoCam" ErrorMessage="Campo Requerido" SetFocusOnError="True"
                                    ValidationGroup="vgGuardarCam" ForeColor="Red" Display="Dynamic" Style="font-size: x-small" /><br />
                                <asp:DropDownList ID="ddlTipoDatoCam" runat="server" Style="font-size: x-small; text-align: left"
                                    AutoPostBack="True" CssClass="textbox" Width="150px" AppendDataBoundItems="True"
                                    Visible="True" onselectedindexchanged="ddlTipoDatoCam_SelectedIndexChanged">
                                    <asp:ListItem Value="">Seleccione un Item</asp:ListItem>
                                    <asp:ListItem Value="1">Numérico</asp:ListItem>
                                    <asp:ListItem Value="2">Caracter</asp:ListItem>
                                    <asp:ListItem Value="3">Fecha</asp:ListItem>
                                    <asp:ListItem Value="4">Hora</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td class="tdI" style="text-align: left; width: 170px;">
                                Alineación&nbsp;*&nbsp;<asp:RequiredFieldValidator ID="rfvAlineacionCam" runat="server"
                                    ControlToValidate="ddlAlineacionCam" ErrorMessage="Campo Requerido" SetFocusOnError="True"
                                    ValidationGroup="vgGuardarCam" ForeColor="Red" Display="Dynamic" Style="font-size: x-small" /><br />
                                <asp:DropDownList ID="ddlAlineacionCam" runat="server" Style="font-size: x-small;
                                    text-align: left" AutoPostBack="True" CssClass="textbox" Width="150px" AppendDataBoundItems="True"
                                    Visible="True">
                                    <asp:ListItem Value="">Seleccione un Item</asp:ListItem>
                                    <asp:ListItem Value="1">Izquierda</asp:ListItem>
                                    <asp:ListItem Value="2">Derecha</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td class="tdI" style="text-align: left">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td class="logo" style="text-align: left">
                                Longitud&nbsp;*&nbsp;<asp:RequiredFieldValidator ID="rfvLongitudCam" runat="server"
                                    ControlToValidate="txtLongitudCam" ErrorMessage="Campo Requerido" SetFocusOnError="True"
                                    ValidationGroup="vgGuardarCam" ForeColor="Red" Display="Dynamic" Style="font-size: x-small" /><br />
                                <asp:TextBox ID="txtLongitudCam" runat="server" CssClass="textbox" MaxLength="3"
                                    Width="60px" />
                            </td>
                            <td class="tdI" style="text-align: left; width: 170px;">
                                Carácter de Llenado&nbsp;*&nbsp;<asp:RequiredFieldValidator ID="rfvCaracterCam" runat="server"
                                    ControlToValidate="txtCaracterCam" ErrorMessage="Campo Requerido" SetFocusOnError="True"
                                    ValidationGroup="vgGuardarCam" ForeColor="Red" Display="Dynamic" Style="font-size: x-small" /><br />
                                <asp:TextBox ID="txtCaracterCam" runat="server" CssClass="textbox" MaxLength="1"
                                    Width="60px" />
                            </td>
                            <td class="tdI" style="text-align: left">
                                <asp:Label ID="lblFormatoCam" runat="server" Text="Formato * " /><br />
                                <asp:TextBox ID="txtFormatoCam" runat="server" CssClass="textbox" MaxLength="10" 
                                    Width="60px" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdI" style="text-align: left">
                                <asp:Label ID="lblPuntoCam" runat="server" Text="Separador Décimal * " /><br />
                                <asp:DropDownList ID="ddlPuntoCam" runat="server" Style="font-size: x-small; text-align: left"
                                    AutoPostBack="True" CssClass="textbox" Width="150px" AppendDataBoundItems="True"
                                    Visible="True">
                                    <asp:ListItem Value="">Seleccione un Item</asp:ListItem>
                                    <asp:ListItem Value="0">(Ninguno)</asp:ListItem>
                                    <asp:ListItem Value="1">Punto (.)</asp:ListItem>
                                    <asp:ListItem Value="2">Coma (,)</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td class="tdI" style="text-align: left; width: 170px;">
                                <asp:Label ID="lblDecimalesCam" runat="server" Text="Posiciones Décimales * " /><br />
                                <asp:TextBox ID="txtDecimalesCam" runat="server" CssClass="textbox" MaxLength="1"
                                    Width="60px" />
                            </td>
                            <td class="tdI" style="text-align: left">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
                <Triggers>
                <asp:AsyncPostBackTrigger ControlID="ddlTipoDatoCam" EventName="SelectedIndexChanged" />
                </Triggers>
            </asp:UpdatePanel>
        </asp:View>
    </asp:MultiView>
</asp:Content>
