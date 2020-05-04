<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Lista.aspx.cs" Inherits="Lista" Title=".: Xpinn - Desembolso Masivo :." %>

<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/tnumero.ascx" TagName="numero" TagPrefix="ucnumero" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:MultiView ID="mvPrincipal" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwFiltros" runat="server">
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
                <div id="divBusqueda" runat="server" style="overflow: scroll; width: 100%; height: 310px;">
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
                            <tr>
                                <td class="tdD" style="text-align:left"><span style="color: rgb(64, 65, 66); font-family:Segoe UI, Arial, Helvetica, Verdana, sans-serif; font-size: 14px; font-style: normal; font-variant-ligatures: normal; font-variant-caps: normal; font-weight: 400; letter-spacing: normal; orphans: 2; text-align: center; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; background-color: rgb(255, 255, 255); text-decoration-style: initial; text-decoration-color: initial; display: inline !important; float: none;">Primer Nombre</span><br/><asp:TextBox ID="txtPrimerNombre" runat="server" CssClass="textbox" MaxLength="128" Width="120px" />
                                </td>
                                <td class="tdD" style="text-align:left"><span style="color: rgb(64, 65, 66); font-family:Segoe UI, Arial, Helvetica, Verdana, sans-serif; font-size: 14px; font-style: normal; font-variant-ligatures: normal; font-variant-caps: normal; font-weight: 400; letter-spacing: normal; orphans: 2; text-align: center; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; background-color: rgb(255, 255, 255); text-decoration-style: initial; text-decoration-color: initial; display: inline !important; float: none;">Segundo Nombre </span>
                                    <br/>
                                    <asp:TextBox ID="txtSegundoNombre" runat="server" CssClass="textbox" MaxLength="128" Width="120px" />
                                </td>
                                <td class="tdD" style="text-align:left"><span style="color: rgb(64, 65, 66); font-family:Segoe UI, Arial, Helvetica, Verdana, sans-serif; font-size: 14px; font-style: normal; font-variant-ligatures: normal; font-variant-caps: normal; font-weight: 400; letter-spacing: normal; orphans: 2; text-align: center; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; background-color: rgb(255, 255, 255); text-decoration-style: initial; text-decoration-color: initial; display: inline !important; float: none;">Primer Apellido </span>
                                    <br/>
                                    <asp:TextBox ID="txtPrimerApellido" runat="server" CssClass="textbox" MaxLength="128" Width="120px" />
                                </td>
                                <td class="tdD" style="text-align:left"><span style="letter-spacing: normal; background-color: #FFFFFF">Segundo</span><span style="color: rgb(64, 65, 66); font-family: Segoe UI, Arial, Helvetica, Verdana, sans-serif; font-size: 14px; font-style: normal; font-variant-ligatures: normal; font-variant-caps: normal; font-weight: 400; letter-spacing: normal; orphans: 2; text-align: center; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; background-color: rgb(255, 255, 255); text-decoration-style: initial; text-decoration-color: initial; display: inline !important; float: none;"> Apellido<asp:TextBox ID="txtSegundoApellido" runat="server" CssClass="textbox" MaxLength="128" Width="120px" />
                                    </span></td>
                                <td style="text-align: left">&nbsp;</td>
                                <td style="text-align: left">&nbsp;</td>
                                <td style="text-align: left">&nbsp;</td>
                            </tr>
                        </table>
                    </asp:Panel>
                </div>
                <center><asp:Label ID="lblInfo" runat="server" Text="Su consulta no obtuvo ningún resultado" Visible="false"/></center>
                <%--<asp:CollapsiblePanelExtender ID="cpeDemo" runat="Server" TargetControlID="pBusqueda"
            ExpandControlID="pEncBusqueda" CollapseControlID="pEncBusqueda" Collapsed="false"
            TextLabelID="lblMostrarDetalles" ImageControlID="imgExpand" ExpandedText="(Click Aqui para Ocultar Detalles...)"
            CollapsedText="(Click Aqui para Mostrar Detalles...)" ExpandedImage="~/Images/collapse.jpg"
            CollapsedImage="~/Images/expand.jpg" SuppressPostBack="true" SkinID="CollapsiblePanelDemo" />--%>
            </asp:Panel>
            <hr style="width=100%" />
        </asp:View>
        <asp:View ID="vwData" runat="server">
            <br />
            <br />
            <table>
                <tr>
                    <td style="text-align: left;" colspan="3">
                        <strong>Datos de la Aprobación</strong>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left">
                        Fecha Aprobación<br />
                        <ucFecha:fecha ID="txtFechaApro" runat="server" CssClass="textbox" />
                    </td>
                    <td style="text-align: left">
                        Banco<br />
                        <asp:DropDownList ID="ddlEntidad_giro" runat="server" CssClass="textbox" Width="250px"
                            AutoPostBack="true" OnSelectedIndexChanged="ddlEntidad_giro_SelectedIndexChanged" />
                    </td>
                    <td style="text-align: left">
                        Cuenta Giro<br />
                        <asp:DropDownList ID="ddlCuenta_Giro" runat="server" CssClass="textbox" Width="200px" />
                    </td>
                </tr>
            </table>
            <table style="width: 100%">
                <tr>
                    <td style="text-align: left">
                        <asp:Panel ID="panelGrilla" runat="server">
                            <strong>Listado de Giros a Aprobar</strong><br />
                            <div id="divGiros" runat="server" style="overflow: scroll; width: 100%;">
                                <asp:GridView ID="gvLista" runat="server" Width="100%" AutoGenerateColumns="False"
                                    AllowPaging="True" PageSize="20" GridLines="Horizontal" ShowHeaderWhenEmpty="True"
                                    OnPageIndexChanging="gvLista_PageIndexChanging" 
                                    DataKeyNames="idgiro,numero_radicacion" Style="font-size: x-small"
                                    OnRowEditing="gvLista_RowEditing" onrowcommand="gvLista_RowCommand" >
                                    <Columns>
                                        <asp:TemplateField HeaderStyle-CssClass="gridIco">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnEditar" runat="server" CommandName="Edit" ImageUrl="~/Images/gr_info.jpg"
                                                    ToolTip="Editar" Width="16px" /></ItemTemplate>
                                            <HeaderStyle CssClass="gridIco"></HeaderStyle>
                                        </asp:TemplateField>
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
                                        <asp:TemplateField HeaderStyle-CssClass="gridIco" ItemStyle-CssClass="gridIco" HeaderText="Distribuir">
                                            <ItemTemplate>
                                                <cc1:ImageButtonGrid ID="btnDistribuir" runat="server" ToolTip="Ver Detalle" Visible='<%# Convert.ToBoolean(Eval("distribuir")) %>'
                                                CommandName="Ver" ImageUrl="~/Images/gr_detall.jpg" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'  />
                                                <%--<cc1:CheckBoxGrid ID="chkDistribuir" runat="server" Style="text-align: right" Checked='<%# Convert.ToBoolean(Eval("distribuir")) %>' Enabled="false"/>--%>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="gridIco" HorizontalAlign="Center"/>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="numero_radicacion" HeaderText="Num. Radicación" Visible="false">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
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
            <table width="100%">
                <tr>
                    <td style="text-align: center">
                        Número total de Registros
                    </td>
                    <td style="text-align: center">
                        <asp:TextBox ID="txtNumTotalReg" runat="server" CssClass="textbox" Width="90px" />
                    </td>
                    <td style="text-align: center">
                        Valor total de Giros
                    </td>
                    <td style="text-align: center">
                        <uc1:decimales ID="txtVrTotal" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center">
                        Número de Giros a Aprobar
                    </td>
                    <td style="text-align: center">
                        <asp:TextBox ID="txtNumGirosApro" runat="server" CssClass="textbox" Width="90px" />
                    </td>
                    <td style="text-align: center">
                        Valor Giros a Aprobar
                    </td>
                    <td style="text-align: center">
                        <uc1:decimales ID="txtVrAprobar" runat="server" />
                    </td>
                </tr>
            </table>
        </asp:View>
        <asp:View ID="vwDetalle" runat="server">
        <br /><br />
            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                <contenttemplate>
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
                                    <ucFecha:fecha ID="Fecha1" runat="server" CssClass="textbox" Enabled="false" />
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
                                    <asp:TextBox ID="txtIdPersona" runat="server" CssClass="textbox"
                                        Enabled="false" Width="110px" />
                                   <%-- <asp:Button ID="btnConsultaPersonas" runat="server" CssClass="btn8" Height="26px"
                                        Enabled="false" OnClick="btnConsultaPersonas_Click" Text="..." />--%>
                                </td>
                                <td style="text-align: left;" colspan="2">
                                    Nombre<br />
                                    <%--<uc1:ListadoPersonas ID="ctlBusquedaPersonas" runat="server" />--%>
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
                                    <asp:TextBox ID="txtNumRadicacion" runat="server" CssClass="textbox" Width="90%" />
                                </td>
                                <td style="text-align: left; width: 150px">
                                    Num Comprobante<br />
                                    <asp:TextBox ID="txtNumComprobante" runat="server" CssClass="textbox" Width="90%" />
                                </td>
                                <td style="text-align: left">
                                    Tipo Comprobante<br />
                                    <asp:DropDownList ID="ddlTipoComprobante" runat="server" CssClass="textbox" Width="90%"
                                       AppendDataBoundItems="true">
                                        <asp:ListItem Value="0" Text="Seleccione un item"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left" colspan="2">
                                    Valor<br />
                                    <uc1:decimales ID="txtValor" runat="server" />
                                </td>
                                <td style="text-align: left">
                                    <asp:CheckBox ID="chkCobraComision" runat="server" TextAlign="Left" Text="Cobra Comisión" />
                                </td>
                                <td style="text-align: left">
                                    <asp:DropDownList ID="ddlEstado" runat="server" CssClass="textbox" Width="90%" />
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
                            <tr>
                                <td colspan="4">
                                    <table style="width:90%">
                                        <tr>
                                            <td style="width:32%;text-align:left">
                                                Usuario Genera<br />
                                                <asp:DropDownList ID="ddlUsuarioGenera" runat="server" CssClass="textbox" Width="95%" />
                                            </td>
                                            <td style="width:33%;text-align:left">
                                                Usuario Aprueba<br />
                                                <asp:DropDownList ID="ddlUsuarioAproba" runat="server" CssClass="textbox" Width="95%" />
                                            </td>
                                            <td style="width:30%;text-align:left">
                                                Usuario Aplica<br />
                                                <asp:DropDownList ID="ddlUsuarioAplica" runat="server" CssClass="textbox" Width="95%" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </contenttemplate>
            </asp:UpdatePanel>
        </asp:View>
        <asp:View ID="vwReporte" runat="server">
            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                <tr>
                    <td style="text-align: left">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        <rsweb:ReportViewer ID="rvGirosAprobados" runat="server" Font-Names="Verdana" Font-Size="8pt"
                            Enabled="false" InteractiveDeviceInfos="(Colección)" WaitMessageFont-Names="Verdana"
                            WaitMessageFont-Size="10pt" Width="100%" Height="500px">
                            <LocalReport ReportPath="Page\Tesoreria\AprobacionGiros\rptAprobacionGiros.rdlc">
                                <DataSources>
                                    <rsweb:ReportDataSource />
                                </DataSources>
                            </LocalReport>
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
                        <td style="text-align: center; font-size: large;">
                            Se ha aprobado correctamente los Giros seleccionados
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

     <asp:ModalPopupExtender ID="mpeDistribucion" runat="server" 
        PopupControlID="PanelDistribucion" TargetControlID="HiddenField1"
        BackgroundCssClass="backgroundColor" >
    </asp:ModalPopupExtender>                     
    <asp:Panel ID="PanelDistribucion" runat="server" Width="500px">
        <asp:HiddenField ID="HiddenField1" runat="server" />
        <asp:UpdatePanel ID="upPanelEstructura" runat="server">            
            <ContentTemplate>
                <div style="border:2px groove #2E9AFE;width:500px; background-color: #FFFFFF; padding:10px">
                    <table style="width: 100%">
                        <tr>
                            <td style="text-align: left">
                                <strong>Detalle de Distribución</strong>                                
                            </td>
                            <td style="text-align: right; vertical-align:top">
                                <asp:ImageButton ID="btnCancelar" runat="server" ImageUrl="~/Images/gr_elim.jpg"
                                                    ToolTip="Cerrar" Width="16px" OnClick="btnCancelar_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="text-align: left; width: 100%">
                                <hr style="width: 100%" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:GridView ID="gvDistribucion" runat="server" Width="100%" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False"
                                    HeaderStyle-Height="30px" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None"
                                    DataKeyNames="idgiro" ForeColor="Black" GridLines="Horizontal"
                                    Style="font-size: x-small">
                                    <Columns>
                                        <asp:BoundField DataField="idgiro" HeaderText="Código" Visible="false" HeaderStyle-HorizontalAlign="Center">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="identificacion" HeaderText="Identificación" HeaderStyle-HorizontalAlign="Center">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="nombre" HeaderText="Nombre" HeaderStyle-HorizontalAlign="Center">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="nom_tipo" HeaderText="Tipo" HeaderStyle-HorizontalAlign="Center">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>                                        
                                        <asp:BoundField DataField="valor" HeaderText="Valor" 
                                            HeaderStyle-HorizontalAlign="Center" DataFormatString="{0:n0}">                                            
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>                                        
                                    </Columns>
                                    <FooterStyle CssClass="gridHeader" />
                                    <HeaderStyle CssClass="gridHeader" />
                                    <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                                    <RowStyle CssClass="gridItem" />
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: center" colspan="2">
                                <asp:Label ID="lblmsjDis" runat="server" Text="Su consulta no obtuvo ningún resultado"
                                    Visible="false" />
                            </td>
                        </tr>
                    </table>
                </div>        
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>

    <uc4:mensajegrabar ID="ctlMensaje" runat="server" />
</asp:Content>
