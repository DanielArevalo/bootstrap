<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Lista.aspx.cs" Inherits="Lista" Title=".: Xpinn - Realización de Giros :." %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>
<%@ Register Src="~/General/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/tnumero.ascx" TagName="numero" TagPrefix="ucnumero" %>
<%@ Register Src="~/General/Controles/ctlProcesoContable.ascx" TagName="procesoContable" TagPrefix="uc2" %>

<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:Panel ID="panelGeneral" runat="server">
        <asp:MultiView ID="mvPrincipal" runat="server" ActiveViewIndex="0">
            <asp:View ID="vwData" runat="server">
                <asp:Panel ID="pConsulta" runat="server">
                    <br />
                    <br />
                    <asp:Panel ID="pEncBusqueda" runat="server" CssClass="collapsePanelHeader" Height="30px">
                        <div style="padding: 5px; cursor: pointer; vertical-align: middle;">
                            <div style="float: left; color: #0066FF; font-size: small">
                                Criterios de Selección
                            </div>
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
                            <table>
                                <tr>
                                    <td style="text-align: left">Cod. Giro<br />
                                        <asp:TextBox ID="txtCodGiro" runat="server" CssClass="textbox" Width="100px" />
                                    </td>
                                    <td style="text-align: left">Identificación<br />
                                        <asp:TextBox ID="txtIdentificacion" runat="server" CssClass="textbox" Width="100px" />
                                    </td>
                                    <td style="text-align: left" colspan="2">Nombres<br />
                                        <asp:TextBox ID="txtNombres" runat="server" CssClass="textbox" Width="90%" />
                                    </td>
                                    <td style="text-align: left">Num. Comp<br />
                                        <asp:TextBox ID="txtNumComp" runat="server" CssClass="textbox" Width="100px" />
                                    </td>
                                    <td style="text-align: left">Tipo. Comp<br />
                                        <asp:DropDownList ID="ddlTipoComp" runat="server" CssClass="textbox" Width="200px"
                                            AppendDataBoundItems="true">
                                            <asp:ListItem Value="0" Text="Seleccione un item"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td style="text-align: left">Num. Radicación<br />
                                        <asp:TextBox ID="txtNumRadicacion" runat="server" CssClass="textbox" Width="100px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: left">Generado en:<br />
                                        <asp:DropDownList ID="ddlGenerado" runat="server" CssClass="textbox" Width="200px" />
                                    </td>
                                    <td style="text-align: left">Fecha Giro<br />
                                        <ucFecha:fecha ID="txtFechaGiro" runat="server" CssClass="textbox" />
                                    </td>
                                    <td style="text-align: left">Usuario Generación<br />
                                        <asp:DropDownList ID="ddlUsuarioGen" runat="server" AppendDataBoundItems="true" CssClass="textbox" Width="200px">
                                            <asp:ListItem Text="Seleccione un Item" Value="-1" />
                                        </asp:DropDownList>
                                    </td>
                                    <td style="text-align: left">Fecha Aprobación<br />
                                        <ucFecha:fecha ID="txtFechaAprobacion" runat="server" CssClass="textbox" />
                                    </td>
                                    <td style="text-align: left">Usuario Aprobación<br />
                                        <asp:DropDownList ID="ddlUsuarioApro" runat="server" AppendDataBoundItems="true" CssClass="textbox" Width="200px">
                                            <asp:ListItem Text="Seleccione un Item" Value="-1" />
                                        </asp:DropDownList>
                                    </td>
                                    <td style="text-align: left">Ordenar Por:<br />
                                        <asp:DropDownList ID="ddlOrdenadoPor" runat="server" CssClass="textbox" Width="200px" />
                                    </td>
                                    <td style="text-align: left">Luego Por:<br />
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
                <hr style="width: 100%" />
                <asp:UpdatePanel ID="UpdData" runat="server">
                    <ContentTemplate>
                        <table>
                            <tr>
                                <td style="text-align: left">Fecha Realización<br />
                                    <ucFecha:fecha ID="txtFechaRealiza" runat="server" CssClass="textbox" />
                                </td>
                                <td>Forma de Pago<br />
                                    <asp:RadioButtonList ID="rblFormaPago" runat="server" RepeatDirection="Horizontal"
                                        AutoPostBack="True" OnSelectedIndexChanged="rblFormaPago_SelectedIndexChanged">
                                        <asp:ListItem Value="Transferencia">Transferencia</asp:ListItem>
                                        <asp:ListItem>Otros</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td style="text-align: left">Banco Giro<br />
                                    <asp:DropDownList ID="ddlEntidad_giro" runat="server" CssClass="textbox" Width="250px"
                                        AutoPostBack="true" OnSelectedIndexChanged="ddlEntidad_giro_SelectedIndexChanged" />
                                </td>
                                <td style="text-align: left">Cuenta Giro<br />
                                    <asp:DropDownList ID="ddlCuenta_Giro" runat="server" CssClass="textbox" Width="200px" />
                                </td>
                                <td>Fecha Aplicación<br />
                                    <ucFecha:fecha ID="txtFechaAplicacion" runat="server" CssClass="textbox" />
                                </td>
                            </tr>
                        </table>
                        <asp:Panel ID="panelGrilla" runat="server">
                            <table>
                                <tr>
                                    <td style="text-align: left">
                                        <strong>Listado de Giros a Realizar</strong><br />
                                        <div id="divGiros" runat="server" style="overflow: scroll; width: 100%;">
                                            <asp:GridView ID="gvLista" runat="server" AutoGenerateColumns="False" Width="115%"
                                                AllowPaging="True" PageSize="400" GridLines="Horizontal" ShowHeaderWhenEmpty="True"
                                                OnPageIndexChanging="gvLista_PageIndexChanging" DataKeyNames="idgiro" Style="font-size: x-small" OnPageIndexChanged="gvLista_PageIndexChanged">
                                                <Columns>
                                                    <asp:TemplateField HeaderStyle-CssClass="gridIco" ItemStyle-CssClass="gridIco">
                                                        <HeaderTemplate>
                                                            <asp:CheckBox ID="cbSeleccionarEncabezado" runat="server" Checked="false" OnCheckedChanged="cbSeleccionarEncabezado_CheckedChanged"
                                                                AutoPostBack="True" />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <cc1:CheckBoxGrid ID="cbSeleccionar" runat="server" Checked="false" AutoPostBack="true" />
                                                        </ItemTemplate>
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
                                                    <asp:BoundField DataField="nombre" HeaderText="Nombre" HeaderStyle-Width="160px">
                                                        <HeaderStyle Width="160px" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="cod_ope" HeaderText="Cod.Ope">
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="num_comp" HeaderText="No.Comp">
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="nom_tipo_comp" HeaderText="Tipo Comp" HeaderStyle-Width="120px">
                                                        <HeaderStyle Width="120px" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="nom_forma_pago" HeaderText="Forma Pago" HeaderStyle-Width="80px">
                                                        <HeaderStyle Width="80px" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="nom_banco" HeaderText="Banco del Giro" HeaderStyle-Width="120px">
                                                        <HeaderStyle Width="120px" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField HeaderText="Cta Bancaria del Giro">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCtaBancaria" runat="server" Text='<%# Bind("num_referencia") %>'></asp:Label><cc1:ButtonGrid
                                                                ID="btnInfo" runat="server" Text=".." CssClass="btn8" Height="16px" OnClick="btnInfo_Click"
                                                                CommandArgument='<%#DataBinder.Eval(Container, "RowIndex") %>' Visible='<%# Eval("activar") %>' />
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="100px" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
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
                                                    <asp:BoundField DataField="nom_estado" HeaderText="Estado" HeaderStyle-Width="120px">
                                                        <HeaderStyle Width="120px" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField HeaderStyle-CssClass="gridIco" ItemStyle-CssClass="gridIco" HeaderText="Distribución">
                                                        <ItemTemplate>
                                                            <cc1:CheckBoxGrid ID="chkDistribuir" runat="server" Style="text-align: right" Checked='<%# Convert.ToBoolean(Eval("distribuir")) %>'
                                                                Enabled="false" />
                                                        </ItemTemplate>
                                                        <HeaderStyle CssClass="gridIco" />
                                                        <ItemStyle CssClass="gridIco"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="identif_bene" HeaderText="Identific. Beneficiario">
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="nombre_bene" HeaderText="Nombre Beneficiario">
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="nom_tipo_cuenta" HeaderText="Nom. Tipo Cuenta">
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="tipo_cuenta" HeaderText="Tipo Cuenta">
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
                            <table width="100%">
                                <tr>
                                    <td style="text-align: center">Número de Giros a realizar
                                    </td>
                                    <td style="text-align: center">
                                        <asp:TextBox ID="txtNumGirosReali" runat="server" CssClass="textbox" Width="90px" />
                                    </td>
                                    <td style="text-align: center">Valor Giros a realizar
                                    </td>
                                    <td style="text-align: center">
                                        <uc1:decimales ID="txtVrRealizar" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>

                        <asp:HiddenField ID="HiddenField1" runat="server" />
                        <asp:ModalPopupExtender ID="mpeChequera" runat="server" PopupControlID="Panel2"
                            TargetControlID="HiddenField1" BackgroundCssClass="backgroundColor">
                        </asp:ModalPopupExtender>
                        <asp:Panel ID="Panel2" runat="server" BackColor="White" Style="margin-bottom: 0px; padding: 10px"
                            BorderWidth="1px">
                            <table>
                                <tr>
                                    <td style="text-align: right">
                                        <asp:ImageButton ID="btnSalir" runat="server" OnClick="btnSalir_Click" ImageUrl="~/Images/gr_elim.jpg" ToolTip="Cerrar" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:GridView ID="gvListaInfo" runat="server" AutoGenerateColumns="False" GridLines="Vertical"
                                            HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager" Style="font-size: x-small"
                                            PageSize="7" RowStyle-CssClass="gridItem">
                                            <Columns>
                                                <asp:BoundField DataField="IDCHEQUERA" HeaderText="Número" />
                                                <asp:BoundField DataField="cheque_ini" HeaderText="Cheque Inicial" />
                                                <asp:BoundField DataField="cheque_fin" HeaderText="Cheque Final" />
                                                <asp:BoundField DataField="num_sig_che" HeaderText="Siguiente Cheque" />
                                            </Columns>
                                            <HeaderStyle CssClass="gridHeader" />
                                            <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                                            <RowStyle CssClass="gridItem" />
                                        </asp:GridView>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: center">
                                        <asp:Label ID="lblTotalRegs1" runat="server" Visible="False" Style="font-size: x-small" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>

                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="rblFormaPago" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="ddlEntidad_giro" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
                <center>
                    <br />
                    <asp:Label ID="lblInfo" runat="server" Text="Su consulta no obtuvo ningún resultado"
                        Visible="false" /></center>
            </asp:View>
            <asp:View ID="vwTransferencia" runat="server">
                <table width="100%">
                    <tr>
                        <td style="text-align: center; width: 100%" class="gridHeader" colspan="3">
                            <strong>Realización de Giros - Transferencias</strong><br />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 15%">&nbsp;
                        </td>
                        <td style="text-align: left; width: 25%">Banco Giro
                        </td>
                        <td style="text-align: left; width: 60%">
                            <asp:DropDownList ID="ddlEntidad" runat="server" CssClass="textbox" AppendDataBoundItems="true" Width="80%" Enabled="false">
                                <asp:ListItem Text="Seleccione un Item" Value="0" />
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 15%">&nbsp;
                        </td>
                        <td style="text-align: left; width: 25%">Cuenta Giro
                        </td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlCuentas" runat="server" AppendDataBoundItems="true" CssClass="textbox" Width="80%" Enabled="false">
                                <asp:ListItem Text="Seleccione un Item" Value="0" />
                            </asp:DropDownList>

                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 15%">&nbsp;
                        </td>
                        <td style="text-align: left">Estructura
                        </td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlEstructura" runat="server" CssClass="textbox" Width="80%" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 15%">&nbsp;
                        </td>
                        <td style="text-align: left">Nombre del Archivo
                        </td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtNombreArchivo" runat="server" CssClass="textbox" Width="78%" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center" colspan="3">
                            <asp:Label ID="lblMensj" runat="server" Style="color: Red; font-size: x-small" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center" colspan="3">
                            <asp:Button ID="btnAceptarEstructura" runat="server" Height="30px" Text="Generar Archivo"
                                Width="150px" CssClass="btn8" OnClick="btnAceptarEstructura_Click" />
                            &#160;&#160;&#160;
                            <asp:Button ID="btnCerrar" runat="server" Text="Cancelar" CssClass="btn8" Width="150px"
                                Height="30px" OnClick="btnCerrar_Click" />
                        </td>
                    </tr>
                </table>
            </asp:View>
            <asp:View ID="vwFinal" runat="server">
                <asp:Panel ID="PanelFinal" runat="server">
                    <table style="width: 100%;">
                        <tr>
                            <td style="text-align: center; font-size: large;" colspan="3">
                                <br />
                                <br />
                                <br />
                                <br />
                                Se ha realizado correctamente los Giros seleccionados
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: center; font-size: large; width: 35%">&#160;             
                            </td>
                            <td style="text-align: center; font-size: large; width: 30%">
                                <br />
                                <asp:GridView ID="gvOperacion" runat="server" AllowPaging="False" AutoGenerateColumns="False"
                                    BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="0px"
                                    CellPadding="0" ForeColor="Black" GridLines="Vertical" PageSize="5" Style="font-size: x-small; text-align: left;"
                                    Width="100%" DataKeyNames="num_comp" OnRowCommand="gvOperacion_RowCommand">
                                    <AlternatingRowStyle BackColor="White" />
                                    <Columns>
                                        <asp:TemplateField HeaderStyle-CssClass="gridIco" ItemStyle-CssClass="gridIco">
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="cbSeleccionarEnc" runat="server" Checked="false" OnCheckedChanged="cbSeleccionarEnc_CheckedChanged"
                                                    AutoPostBack="True" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="cbSeleccionar" runat="server" Checked="false" />
                                            </ItemTemplate>
                                            <HeaderStyle CssClass="gridIco"></HeaderStyle>
                                            <ItemStyle CssClass="gridIco"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="gridIco">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnImprimir" runat="server" ImageUrl="~/Images/gr_imp.gif" ToolTip="Imprimir"
                                                    CommandName="Imprimir" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex")%>' />
                                            </ItemTemplate>
                                            <HeaderStyle CssClass="gridIco" Width="80px" HorizontalAlign="Center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="cod_ope" HeaderText="Cod.Ope">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:HyperLinkField DataNavigateUrlFields="num_comp" DataNavigateUrlFormatString="../../Contabilidad/Comprobante/Lista.aspx?num_comp={0}"
                                            DataTextField="num_comp" HeaderText="Num.Comp" Target="_blank" Text="Número de Comprobante" />
                                        <%--<asp:BoundField DataField="num_comp" HeaderText="Nro. Comprobante">
                                            <ItemStyle HorizontalAlign="center" />
                                        </asp:BoundField>--%>
                                        <asp:BoundField DataField="tipo_comp" HeaderText="Tipo.Comp" />
                                        <asp:BoundField DataField="num_cheque" HeaderText="Nro Cheque">
                                            <ItemStyle HorizontalAlign="Left" />
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
                            <td style="text-align: center; font-size: large; width: 35%">&#160;             
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: center; font-size: large;" colspan="3">
                                <br />
                                <br />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </asp:View>
        </asp:MultiView>
    </asp:Panel>
    <asp:Panel ID="panelProceso" runat="server" Width="100%">
        <uc2:procesoContable ID="ctlproceso" runat="server" />
    </asp:Panel>

    <uc4:mensajegrabar ID="ctlMensaje" runat="server" />
</asp:Content>
