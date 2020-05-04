<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" Title=".: Xpinn - Plan Cuentas NIIF :." %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <br />
    <br />
    <asp:Panel ID="pErrores" runat="server">
        <asp:Panel ID="pEncBusqueda" runat="server" CssClass="collapsePanelHeader" Height="30px">
            <div style="padding: 5px; cursor: pointer; vertical-align: middle;">
                <div style="float: left; margin-left: 5px; color: #0066FF; font-size: small">
                    <asp:Label ID="lblMostrarDetalles" runat="server" />
                    <asp:ImageButton ID="imgExpand" runat="server" ImageUrl="~/Images/expand.jpg" />
                </div>
            </div>
        </asp:Panel>
        <asp:Panel ID="pConsultaAfiliacion" runat="server" Width="100%">
            <div style="border-style: none; border-width: medium; background-color: #f5f5f5">
                <asp:GridView ID="gvErrores" runat="server" Width="100%" ShowHeaderWhenEmpty="True"
                    AutoGenerateColumns="False" SelectedRowStyle-Font-Size="XX-Small" Style="font-size: x-small;
                    margin-bottom: 0px;">
                    <Columns>
                        <asp:BoundField DataField="error" HeaderText="Error" ItemStyle-HorizontalAlign="Left" />
                    </Columns>
                    <HeaderStyle CssClass="gridHeader" />
                    <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                    <RowStyle CssClass="gridItem" />
                    <SelectedRowStyle Font-Size="XX-Small"></SelectedRowStyle>
                </asp:GridView>
            </div>
        </asp:Panel>
        <asp:CollapsiblePanelExtender ID="cpeDemo" runat="Server" CollapseControlID="pEncBusqueda"
            Collapsed="True" CollapsedImage="~/Images/expand.jpg" CollapsedText="(Click Aqui para Mostrar Detalles...)"
            ExpandControlID="pEncBusqueda" ExpandedImage="~/Images/collapse.jpg" ExpandedText="(Click Aqui para Ocultar Detalles...)"
            ImageControlID="imgExpand" SkinID="CollapsiblePanelDemo" SuppressPostBack="true"
            TargetControlID="pConsultaAfiliacion" TextLabelID="lblMostrarDetalles" />
        <br />
    </asp:Panel>

    <asp:Panel ID="pErroresGrabar" runat="server">
        <table>
            <tr>
                <td style="text-align: left">
                    <asp:Button ID="btnExportarErroneos" runat="server" CssClass="btn8" OnClick="btnExportarErroneos_Click"
                        Text="Exportar a Excel" />
                </td>
            </tr>
        </table>
        <asp:Panel ID="pEncBusqueda2" runat="server" CssClass="collapsePanelHeader" Height="30px">
            <div style="padding: 5px; cursor: pointer; vertical-align: middle;">
                <div style="float: left; margin-left: 5px; color: #0066FF; font-size: small">
                    <asp:Label ID="lblMostrarDetalles2" runat="server" />
                    <asp:ImageButton ID="imgExpand2" runat="server" ImageUrl="~/Images/expand.jpg" />
                </div>
            </div>
        </asp:Panel>
        <asp:Panel ID="pConsultaAfiliacion2" runat="server" Width="100%">
            <div style="border-style: none; border-width: medium; background-color: #f5f5f5;max-height:500px;overflow:scroll">
                <asp:GridView ID="gvDatosErrados" runat="server" Width="100%" AutoGenerateColumns="False"
                    PageSize="30" HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager"
                    RowStyle-CssClass="gridItem" DataKeyNames="cod_cuenta_niif" Style="font-size: x-small">
                    <Columns>
                    <asp:BoundField DataField="cod_cuenta_niif" HeaderText="Cod Cuenta Niif">
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="nombre" HeaderText="Nombre">
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="tipo" HeaderText="Tipo" />
                    <asp:BoundField DataField="nivel" HeaderText="Nivel" />
                    <asp:BoundField DataField="depende_de" HeaderText="Depende de" />
                    <asp:BoundField DataField="cod_moneda" HeaderText="Moneda" />
                    <asp:BoundField DataField="estado" HeaderText="Activa" />
                    <asp:BoundField DataField="maneja_ter" HeaderText="Terceros" />                    
                    <asp:BoundField DataField="maneja_ter" HeaderText="Terceros" />
                    <asp:BoundField DataField="maneja_cc" HeaderText="Maneja C/C" />                    
                    <asp:BoundField DataField="maneja_sc" HeaderText="Maneja C/G" />
                    <asp:BoundField DataField="impuesto" HeaderText="Maneja Impuestos" />
                    <asp:BoundField DataField="maneja_gir" HeaderText="Maneja CxP" />
                    <asp:BoundField DataField="base_minima" HeaderText="Base Mínima" />
                    <asp:BoundField DataField="porcentaje_impuesto" HeaderText="% Impuesto" />
                    <asp:BoundField DataField="cod_cuenta" HeaderText="Cod Cuenta Homologada">
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                </Columns>
                <HeaderStyle CssClass="gridHeader"></HeaderStyle>
                <PagerStyle CssClass="gridPager"></PagerStyle>
                <RowStyle CssClass="gridItem"></RowStyle>
            </asp:GridView>
            </div>
        </asp:Panel>
        <asp:CollapsiblePanelExtender ID="cpeDemo2" runat="Server" CollapseControlID="pEncBusqueda2"
            Collapsed="True" CollapsedImage="~/Images/expand.jpg" CollapsedText="(Click Aqui para Mostrar Detalles de datos no grabados...)"
            ExpandControlID="pEncBusqueda2" ExpandedImage="~/Images/collapse.jpg" ExpandedText="(Click Aqui para Ocultar Detalles...)"
            ImageControlID="imgExpand2" SkinID="CollapsiblePanelDemo" SuppressPostBack="true"
            TargetControlID="pConsultaAfiliacion2" TextLabelID="lblMostrarDetalles2" />
        <br />
    </asp:Panel>

    <asp:MultiView ID="mvPlanCuentasNIIF" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwGenerar" runat="server">
            
            <asp:GridView ID="gvLista" runat="server" Width="100%" AllowPaging="True" AutoGenerateColumns="False"
                OnPageIndexChanging="gvLista_PageIndexChanging" OnRowDeleting="gvLista_RowDeleting"
                PageSize="30" HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager"
                RowStyle-CssClass="gridItem" DataKeyNames="cod_cuenta_niif" Style="font-size: x-small">
                <Columns>                    
                    <asp:TemplateField HeaderStyle-CssClass="gridIco" ItemStyle-CssClass="gridIco">
                        <ItemTemplate>
                            <asp:ImageButton ID="btnBorrar" runat="server" CommandName="Delete" ImageUrl="~/Images/gr_elim.jpg"
                                ToolTip="Borrar" />
                        </ItemTemplate>
                        <HeaderStyle CssClass="gridIco"></HeaderStyle>
                        <ItemStyle CssClass="gI" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="cod_cuenta_niif" HeaderText="Cod Cuenta Niif">
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="nombre" HeaderText="Nombre">
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="tipo" HeaderText="Tipo" />
                    <asp:BoundField DataField="nivel" HeaderText="Nivel" />
                    <asp:BoundField DataField="depende_de" HeaderText="Depende de" />
                    <asp:BoundField DataField="cod_moneda" HeaderText="Moneda" />
                    <asp:BoundField DataField="estado" HeaderText="Activa" />
                    <asp:TemplateField HeaderText="Terceros">
                        <ItemTemplate>
                            <asp:CheckBox ID="chkTerceros" runat="server" Checked='<%#Convert.ToBoolean(Eval("maneja_ter")) %>'
                                Enabled="False" EnableViewState="true" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Maneja C/C">
                        <ItemTemplate>
                            <asp:CheckBox ID="chkManejaCC" runat="server" Checked='<%#Convert.ToBoolean(Eval("maneja_cc")) %>'
                                Enabled="False" EnableViewState="true" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Maneja C/G">
                        <ItemTemplate>
                            <asp:CheckBox ID="chkManejaCG" runat="server" Checked='<%#Convert.ToBoolean(Eval("maneja_sc")) %>'
                                Enabled="False" EnableViewState="true" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Maneja Impuestos">
                        <ItemTemplate>
                            <asp:CheckBox ID="chkManejaImp" runat="server" Checked='<%#Convert.ToBoolean(Eval("impuesto")) %>'
                                Enabled="False" EnableViewState="true" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Maneja CxP">
                        <ItemTemplate>
                            <asp:CheckBox ID="chkManejaCP" runat="server" Checked='<%#Convert.ToBoolean(Eval("maneja_gir")) %>'
                                Enabled="False" EnableViewState="true" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="base_minima" HeaderText="Base Mínima" />
                    <asp:BoundField DataField="porcentaje_impuesto" HeaderText="% Impuesto" />
                    <asp:BoundField DataField="cod_cuenta" HeaderText="Cod Cuenta Homologada">
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="corriente" HeaderText="Corriente">
                        <ItemStyle HorizontalAlign="center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="nocorriente" HeaderText="No Corriente">
                        <ItemStyle HorizontalAlign="center" />
                    </asp:BoundField>
                </Columns>
                <HeaderStyle CssClass="gridHeader"></HeaderStyle>
                <PagerStyle CssClass="gridPager"></PagerStyle>
                <RowStyle CssClass="gridItem"></RowStyle>
            </asp:GridView>
            <asp:GridView ID="gvExport" runat="server" Width="100%" AutoGenerateColumns="False"
                HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager" Visible="false"
                RowStyle-CssClass="gridItem" DataKeyNames="cod_cuenta_niif" Style="font-size: x-small">
                <Columns>
                    <asp:BoundField DataField="cod_cuenta_niif" HeaderText="Cod Cuenta Niif">
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="nombre" HeaderText="Nombre">
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="tipo" HeaderText="Tipo" />
                    <asp:BoundField DataField="nivel" HeaderText="Nivel" />
                    <asp:BoundField DataField="depende_de" HeaderText="Depende de" />
                    <asp:BoundField DataField="cod_moneda" HeaderText="Moneda" />
                    <asp:BoundField DataField="estado" HeaderText="Activa" />
                    <asp:BoundField DataField="maneja_ter" HeaderText="Terceros" />
                    <asp:BoundField DataField="maneja_cc" HeaderText="Maneja C/C" />
                    <asp:BoundField DataField="maneja_sc" HeaderText="Maneja C/G">
                        <ItemStyle HorizontalAlign="center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="impuesto" HeaderText="Maneja Impuestos">
                        <ItemStyle HorizontalAlign="center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="maneja_gir" HeaderText="Maneja CxP">
                        <ItemStyle HorizontalAlign="center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="base_minima" HeaderText="Base Mínima">
                        <ItemStyle HorizontalAlign="center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="porcentaje_impuesto" HeaderText="% Impuesto">
                        <ItemStyle HorizontalAlign="center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="cod_cuenta" HeaderText="Cod Cuenta Homologada">
                        <ItemStyle HorizontalAlign="center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="corriente" HeaderText="Corriente">
                        <ItemStyle HorizontalAlign="center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="nocorriente" HeaderText="No Corriente">
                        <ItemStyle HorizontalAlign="center" />
                    </asp:BoundField>
                </Columns>
            </asp:GridView>
            <asp:Label ID="lblTotalRegs" runat="server" Visible="False" />
        </asp:View>
        <asp:View ID="View1" runat="server">
            <asp:Panel ID="Seleccionar_Archivo" runat="server">
                <table border="0" cellpadding="2" cellspacing="0" style="border-style: none; border-color: inherit;
                    width: 100%;">
                    <tr>
                        <td colspan="3" style="text-align: left; font-size: x-small">
                            <strong>Orden de Carga : </strong>&#160;&#160; Cod Cuenta Niif ; Nombre ; Tipo ;
                            Nivel ; Depende de (Opcional); Cod Moneda (Opcional); Activa (Opcional); Maneja Ter (Opcional); Maneja CC (Opcional); Maneja CG (Opcional);
                            Maneja Impuesto (Opcional); Maneja CxP (Opcional); Base Minima (Opcional); Porcentaje (Opcional); Cod Cuenta(Opcional), Corriente , No Corriente.
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; font-size: x-small">
                            <strong>Seperador : </strong>&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;|
                        </td>
                        <td colspan="2" style="text-align: left; font-size: x-small">
                            <strong>Tipo de Archivo a cargar : </strong>&#160;&#160; Texto
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; width: 30%;">
                            <br />
                            Seleccione el archivo a cargar<br />
                            <asp:FileUpload ID="FileUpload2" runat="server" Style="font-size: x-small" Height="20px"
                                Width="200px" />&nbsp;&nbsp;
                            <asp:Button ID="btnGuardarReg" runat="server" CssClass="btn8" Height="25px" OnClick="btnGuardarReg_Click"
                                PostBackUrl="~/Page/Niif/PlanCuentas/Lista.aspx" Style="margin-right: 10px;"
                                Text="Cargar" Width="100px" />
                        </td>
                        <td style="text-align: left; width: 15%;">
                            Fecha De Carga<br />
                            <ucFecha:fecha ID="txtAprobacion_fin" runat="server" AutoPostBack="True" CssClass="textbox"
                                MaxLength="1" ValidationGroup="vgGuardar" Width="148px" />
                        </td>
                        <td style="text-align: left; width: 55%;">
                            <asp:CheckBox ID="chkCambiarArchivoConsulta" runat="server" Style="font-size: xx-small"
                                Text="Borrar Cuentas NIIF Actuales" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" style="text-align: center; height: 23px;">
                            &#160;&#160;&#160;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" style="text-align: center; font-size: large;">
                            <asp:Label ID="Label1" runat="server" Text="  " Visible="false"></asp:Label>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
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
                            <asp:Label ID="lblMensajeGrabar" runat="server" Text="Plan de Cuentas NIIF Generado Correctamente"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <br />
                            <br />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </asp:View>
    </asp:MultiView>
    <uc4:mensajegrabar ID="ctlMensaje" runat="server" />
</asp:Content>
