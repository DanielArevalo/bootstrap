<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Lista.aspx.cs" Inherits="Lista" Title=".: Xpinn - Tesoreria FusionGiro :." %>

<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar"
    TagPrefix="uc4" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/tnumero.ascx" TagName="numero" TagPrefix="ucnumero" %>
<%@ Register Src="../../../General/Controles/ctlBusquedaRapida.ascx" TagName="ListadoPersonas"
    TagPrefix="uc1" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:MultiView ID="mvPrincipal" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwData" runat="server">
            <asp:Panel ID="pConsulta" runat="server">
                <br />
                <br />
                <asp:Panel ID="pEncBusqueda" runat="server" CssClass="collapsePanelHeader" Height="30px">
                    <div style="padding: 5px; cursor: pointer; vertical-align: middle;">
                        <div style="float: left; color: #0066FF; font-size: small">
                            Criterios de Selección</div>
                        <div style="float: left; margin-left: 5px; color: #0066FF; font-size: small">
                            <asp:Label ID="lblMostrarDetalles" runat="server">(Mostrar Detalles...)</asp:Label>
                        </div>
                        <div style="float: right; vertical-align: middle;">
                            <asp:ImageButton ID="imgExpand" runat="server" ImageUrl="~/Images/expand.jpg" AlternateText="(Show Details...)" />
                        </div>
                    </div>
                </asp:Panel>
                <div id="divBusqueda" runat="server" style="overflow: scroll; width: 100%;">
                    <asp:Panel ID="pBusqueda" runat="server" Height="100px">
                        <table width="100%">
                            <tr>
                                <td style="text-align: left">
                                    Cod. Giro<br />
                                    <asp:TextBox ID="txtCodGiro" runat="server" CssClass="textbox" Width="100px" />
                                </td>
                                <td style="text-align: left">
                                    Fecha Giro<br />
                                    <ucFecha:fecha ID="txtFechaGiro" runat="server" CssClass="textbox" />
                                </td>
                                <td style="text-align: left">
                                    Identificación<br />
                                    <asp:TextBox ID="txtIdentificacion" runat="server" CssClass="textbox" Width="100px" />
                                </td>
                                <td style="text-align: left" colspan="2">
                                    Nombres<br />
                                    <asp:TextBox ID="txtNombres" runat="server" CssClass="textbox" Width="90%" />
                                </td>
                                <td style="text-align: left">
                                    Num. Comp<br />
                                    <asp:TextBox ID="txtNumComp" runat="server" CssClass="textbox" Width="100px" />
                                </td>
                                <td style="text-align: left">
                                    Tipo. Comp<br />
                                    <asp:DropDownList ID="ddlTipoComp" runat="server" CssClass="textbox" Width="200px"
                                        AppendDataBoundItems="true">
                                        <asp:ListItem Value="0" Text="Seleccione un item"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left">
                                    Generado en:<br />
                                    <asp:DropDownList ID="ddlGenerado" runat="server" CssClass="textbox" Width="200px" />
                                </td>
                                <td style="text-align: left">
                                    Forma Pago<br />
                                    <asp:DropDownList ID="ddlFormaPago" runat="server" CssClass="textbox" Width="200px" />
                                </td>
                                <td style="text-align: left">
                                    Banco Giro<br />
                                    <asp:DropDownList ID="ddlEntidad" runat="server" CssClass="textbox" Width="200px" />
                                </td>
                                <td style="text-align: left">
                                    Cuenta Giro<br />
                                    <asp:DropDownList ID="ddlCuentas" runat="server" CssClass="textbox" Width="200px" />
                                </td>
                                <td style="text-align: left">
                                    Usuario<br />
                                    <asp:DropDownList ID="ddlUsuario" runat="server" CssClass="textbox" Width="200px" />
                                </td>
                                <td style="text-align: left">
                                    Ordenar Por:<br />
                                    <asp:DropDownList ID="ddlOrdenadoPor" runat="server" CssClass="textbox" Width="200px" />
                                </td>
                                <td style="text-align: left">
                                    Luego Por:<br />
                                    <asp:DropDownList ID="ddlLuegoPor" runat="server" CssClass="textbox" Width="200px" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </div>
                <asp:CollapsiblePanelExtender ID="cpeDemo" runat="Server" TargetControlID="pBusqueda"
                    ExpandControlID="pEncBusqueda" CollapseControlID="pEncBusqueda" Collapsed="false"
                    ExpandedSize="120" TextLabelID="lblMostrarDetalles" ImageControlID="imgExpand"
                    ExpandedText="(Click Aqui para Ocultar Detalles...)" CollapsedText="(Click Aqui para Mostrar Detalles...)"
                    ExpandedImage="~/Images/collapse.jpg" CollapsedImage="~/Images/expand.jpg" SuppressPostBack="true"
                    SkinID="CollapsiblePanelDemo" />
            </asp:Panel>
            <hr style="100%" />

            <asp:UpdatePanel ID="UpdData" runat="server">
                <ContentTemplate>
                    <asp:Panel ID="panelGrilla" runat="server">
                        <table style="width: 100%">
                            <tr>
                                <td style="text-align: left">
                                    <strong>Seleccione los Giros a Fusionar</strong><br />
                                    <div id="divGiros" runat="server" style="overflow: scroll; width: 100%;">
                                        <asp:GridView ID="gvLista" runat="server" Width="100%" AutoGenerateColumns="False"
                                            AllowPaging="True" PageSize="40" GridLines="Horizontal" ShowHeaderWhenEmpty="True"
                                            OnPageIndexChanging="gvLista_PageIndexChanging" DataKeyNames="idgiro" Style="font-size: x-small">
                                            <Columns>
                                                <asp:TemplateField HeaderStyle-CssClass="gridIco" ItemStyle-CssClass="gridIco">
                                                    <HeaderTemplate>
                                                        <asp:CheckBox ID="cbSeleccionarEncabezado" runat="server" Checked="false" OnCheckedChanged="cbSeleccionarEncabezado_CheckedChanged"
                                                            AutoPostBack="True" /></HeaderTemplate>
                                                    <ItemTemplate>
                                                        <cc1:CheckBoxGrid ID="cbSeleccionar" runat="server" Checked="false" AutoPostBack="true" /></ItemTemplate>
                                                    <HeaderStyle CssClass="gridIco"></HeaderStyle>
                                                    <ItemStyle CssClass="gridIco"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="idgiro" HeaderText="Num Giro">
                                                    <ItemStyle HorizontalAlign="Left" Width="50px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="fec_reg" HeaderText="Fecha Registro" DataFormatString="{0:d}">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="cod_persona" HeaderText="Cod.Per">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="identificacion" HeaderText="Identific.">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="nombre" HeaderText="Nombre">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="cod_ope" HeaderText="Cod.Ope">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="num_comp" HeaderText="No.Comp">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="nom_tipo_comp" HeaderText="Tipo Comp">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="nom_forma_pago" HeaderText="Forma Pago">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="nom_banco" HeaderText="Banco del Giro">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="num_referencia" HeaderText="Cta Bancaria del Giro">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="nom_banco1" HeaderText="Banco Destino">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="num_referencia1" HeaderText="Cta Bancaria Destino">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="valor" HeaderText="Valor" DataFormatString="{0:N0}">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="nom_generado" HeaderText="Generado">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="nom_estado" HeaderText="Estado">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                            </Columns>
                                            <HeaderStyle CssClass="gridHeader" />
                                            <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                                            <RowStyle CssClass="gridItem" />
                                        </asp:GridView>
                                    </div>
                                </td>
                            </tr>
                        </table>
                        <center>
                            <asp:Label ID="lblInfo" runat="server" Text="Su consulta no obtuvo ningún resultado"
                                Visible="false" /></center>
                        <table width="100%">
                            <tr>
                                <td style="text-align: center">
                                    Número de Giros a Fusionar
                                </td>
                                <td style="text-align: center">
                                    <asp:TextBox ID="txtNumGirosReali" runat="server" CssClass="textbox" Width="90px" />
                                </td>
                                <td style="text-align: center">
                                    Valor Giros a Fusionar
                                </td>
                                <td style="text-align: center">
                                    <uc1:decimales ID="txtVrFusiona" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:View>
        <asp:View ID="vwDetalle" runat="server">
            <br />
            <br />
            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                <ContentTemplate>
                    <asp:Panel ID="PanelDetalle" runat="server">
                        <table style="width: 730px; text-align: center" cellspacing="0" cellpadding="0">
                            <tr>
                                <td style="text-align: left; width: 150px">
                                    Código<br />
                                    <asp:TextBox ID="txtCodigo" runat="server" CssClass="textbox" Width="100px" Enabled="false" />
                                </td>
                                <td style="text-align: left; width: 150px">
                                    Fecha<br />
                                    <ucFecha:fecha ID="txtFecha" runat="server" CssClass="textbox" Enabled="false" />
                                </td>
                                <td style="text-align: left; width: 150px">
                                    Fecha Giro<br />
                                    <ucFecha:fecha ID="txtFechaGiroNuevo" runat="server" CssClass="textbox" Enabled="false" />
                                </td>
                                <td style="text-align: left; width: 280px">
                                    Tipo Acto Giro<br />
                                    <asp:DropDownList ID="ddlTipoGiro" runat="server" CssClass="textbox" Width="90%"
                                        AutoPostBack="True" />
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left; width: 150px">
                                    Código<br />
                                    <asp:TextBox ID="txtCodPersona" runat="server" CssClass="textbox" Width="90%" />
                                </td>
                                <td style="text-align: left; width: 150px">
                                    Identificación
                                    <asp:TextBox ID="txtIdPersona" runat="server" CssClass="textbox" Width="110px" OnTextChanged="txtIdPersona_TextChanged" />
                                    <asp:Button ID="btnConsultaPersonas" runat="server" CssClass="btn8" Height="26px"
                                        OnClick="btnConsultaPersonas_Click" Text="..." />
                                </td>
                                <td style="text-align: left;" colspan="2">
                                    Nombre<br />
                                    <uc1:ListadoPersonas ID="ctlBusquedaPersonas" runat="server" />
                                    <asp:TextBox ID="txtNomPersona" runat="server" CssClass="textbox" Enabled="false"
                                        Width="90%" />
                                    <asp:RequiredFieldValidator ID="rfvEncargado" runat="server" ControlToValidate="txtNomPersona"
                                        Display="Dynamic" ErrorMessage="Seleccione encargado" ForeColor="Red" InitialValue="0"
                                        Style="font-size: xx-small" ValidationGroup="vgGuardar" />
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left; width: 150px">
                                    Cod. Operación<br />
                                    <asp:TextBox ID="txtCodOperacion" runat="server" CssClass="textbox" Width="90%" />
                                </td>
                                <td style="text-align: left; width: 150px">
                                    Num. Radicación<br />
                                    <asp:TextBox ID="txtNumRadicacion" runat="server" CssClass="textbox" Width="90%"
                                        Enabled="false" />
                                </td>
                                <td style="text-align: left; width: 150px">
                                    Num Comprobante<br />
                                    <asp:TextBox ID="txtNumComprobante" runat="server" CssClass="textbox" Width="90%"
                                        Enabled="false" />
                                </td>
                                <td style="text-align: left">
                                    Tipo Comprobante<br />
                                    <asp:DropDownList ID="ddlTipoComprobante" runat="server" CssClass="textbox" Width="90%"
                                        Enabled="false" AppendDataBoundItems="true">
                                        <asp:ListItem Value="0" Text="Seleccione un item"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left" colspan="2">
                                    Valor<br />
                                    <uc1:decimales ID="txtValor" runat="server" Enabled="false" />
                                </td>
                                <td style="text-align: left">
                                    <asp:CheckBox ID="chkCobraComision" runat="server" TextAlign="Left" Text="Cobra Comisión" />
                                </td>
                                <td style="text-align: left">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" style="text-align: left">
                                    <hr style="width: 100%" />
                                </td>
                            </tr>
                        </table>
                        <table style="width: 100%">
                            <tr>
                                <td colspan="3" style="text-align: left">
                                    Forma de Pago<br />
                                    <asp:DropDownList ID="ddlForma_Desem" runat="server" CssClass="textbox" Width="191px"
                                        OnSelectedIndexChanged="ddlForma_Desem_SelectedIndexChanged" AutoPostBack="True">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                    <asp:Panel ID="PanelCheque" runat="server">
                                        <table width="700px">
                                            <tr>
                                                <td style="text-align: left; width: 250px">
                                                    Banco de donde se Gira<br />
                                                    <asp:DropDownList ID="ddlEntidad_giro2" runat="server" CssClass="textbox" Width="90%"
                                                        AutoPostBack="True" OnSelectedIndexChanged="ddlEntidad_giro2_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>
                                                <td style="text-align: left; width: 250px">
                                                    Cuenta de donde se Gira<br />
                                                    <asp:DropDownList ID="ddlCuenta_Giro2" runat="server" CssClass="textbox" Width="220px">
                                                    </asp:DropDownList>
                                                </td>
                                                <td style="text-align: left; width: 200px">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                    <asp:Panel ID="PanelTransfe" runat="server">
                                        <table width="700px">
                                            <tr>
                                                <td style="text-align: left; width: 250px">
                                                    Entidad Bancaria<br />
                                                    <asp:DropDownList ID="ddlEntidad2" runat="server" CssClass="textbox" Width="90%">
                                                    </asp:DropDownList>
                                                </td>
                                                <td style="text-align: left; width: 250px">
                                                    Num. Cuenta<br />
                                                    <asp:TextBox ID="txtNum_cuenta" runat="server" CssClass="textbox" Width="220px" />
                                                </td>
                                                <td style="text-align: left; width: 200px">
                                                    Tipo de Cuenta<br />
                                                    <asp:DropDownList ID="ddlTipo_cuenta" runat="server" CssClass="textbox" Width="90%">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
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
                        <td style="text-align: center; font-size: large;">
                            Se ha fusionado correctamente los Giros seleccionados
                            <br />
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
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </asp:View>
    </asp:MultiView>
    <uc4:mensajegrabar ID="ctlMensaje" runat="server" />
</asp:Content>
