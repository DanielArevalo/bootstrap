<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" Title=".: Xpinn - Usuario :." EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>
<%@ Register src="../../../General/Controles/fecha.ascx" tagname="fecha" tagprefix="ucFecha" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">        
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <br />
    <asp:MultiView ID="mvPrincipal" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwData" runat="server">
            <asp:CollapsiblePanelExtender ID="cpeDemo" runat="Server" TargetControlID="pConsulta"
                ExpandControlID="pEncBusqueda" CollapseControlID="pEncBusqueda" Collapsed="False"
                TextLabelID="lblMostrarDetalles" ImageControlID="imgExpand" ExpandedText="(Click Aqui para Ocultar Detalles...)"
                CollapsedText="(Click Aqui para Mostrar Detalles...)" ExpandedImage="~/Images/collapse.jpg"
                CollapsedImage="~/Images/expand.jpg" SuppressPostBack="true" SkinID="CollapsiblePanelDemo" />
            <asp:Panel ID="pEncBusqueda" runat="server" CssClass="collapsePanelHeader" Height="30px">
                <div style="padding: 5px; cursor: pointer; vertical-align: middle;">
                    <div style="float: left; color: #0066FF; font-size: small">
                        Criterios de Búsqueda</div>
                    <div style="float: left; margin-left: 5px; color: #0066FF; font-size: small">
                        <asp:Label ID="lblMostrarDetalles" runat="server">(Mostrar Detalles...)</asp:Label>
                    </div>
                    <div style="float: right; vertical-align: middle;">
                        <asp:ImageButton ID="imgExpand" runat="server" ImageUrl="~/Images/expand.jpg" AlternateText="(Show Details...)" />
                    </div>
                </div>
            </asp:Panel>
            <asp:Panel ID="pConsulta" runat="server">
                <table id="tbCriterios" border="0" cellpadding="0" cellspacing="5" width="90%">
                    <tr>
                        <td>
                            Código Cuenta<br />
                            <asp:TextBox ID="txtCodCuenta" runat="server" CssClass="textbox" Width="100px"></asp:TextBox>
                            <br />
                        </td>
                        <td class="tdD">
                            Nombre de Cuenta<br />
                            <asp:TextBox ID="txtNombre" runat="server" CssClass="textbox" Width="180px" />
                        </td>
                        <td class="tdD">
                            Tipo<br />
                            <asp:DropDownList ID="ddlTipo" runat="server" Width="100px" CssClass="textbox">
                                <asp:ListItem Value=""></asp:ListItem>
                                <asp:ListItem Value="D">Débito</asp:ListItem>
                                <asp:ListItem Value="C">Crédito</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td class="tdD">
                            Nivel<br />
                            <asp:TextBox ID="txtNivel" runat="server" Width="40px" CssClass="textbox"></asp:TextBox>
                        </td>
                        <td class="tdD">
                            Depende de<br />
                            <asp:DropDownList ID="ddlDepende" runat="server" CssClass="textbox" Width="120px"
                                AppendDataBoundItems="True">
                                <asp:ListItem Value=""></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td class="tdI">
                            Estado<br />
                            <asp:DropDownList ID="ddlEstado" runat="server" Width="120px" CssClass="textbox">
                                <asp:ListItem Value=""></asp:ListItem>
                                <asp:ListItem Value="0">Inactiva</asp:ListItem>
                                <asp:ListItem Value="1">Activa</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td class="tdD">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="tdD" colspan="5" style="text-align: left">
                            <asp:CheckBox ID="chkTerceros" runat="server" Style="font-size: x-small" Text="Maneja Terceros" />
                            &nbsp;
                            <asp:CheckBox ID="chkCentroCosto" runat="server" Style="font-size: x-small" Text="Maneja Centro Costo" />
                            &nbsp;
                            <asp:CheckBox ID="chkImpuestos" runat="server" Style="font-size: x-small" Text="Maneja Impuestos" />
                            <asp:CheckBox ID="chkCentroGestion" runat="server" Style="font-size: x-small" Text="Maneja Centros Gestión" />
                            &nbsp;
                            <asp:CheckBox ID="chkGiro" runat="server" Style="font-size: x-small" Text="Maneja Cuenta x Pagar" />
                            &nbsp;
                            <br />
                        </td>
                        <td class="tdD" style="text-align: left">
                        </td>
                        <td class="tdD" style="text-align: left">
                        </td>
                    </tr>                    
                </table>
            </asp:Panel>
            <hr />
            <asp:GridView ID="gvLista" runat="server" Width="100%" AutoGenerateColumns="False"
                OnRowDataBound="gvLista_RowDataBound" OnRowDeleting="gvLista_RowDeleting" AllowPaging="True"
                OnPageIndexChanging="gvLista_PageIndexChanging" OnSelectedIndexChanged="gvLista_SelectedIndexChanged"
                OnRowEditing="gvLista_RowEditing" PageSize="20" HeaderStyle-CssClass="gridHeader"
                PagerStyle-CssClass="gridPager" RowStyle-CssClass="gridItem" DataKeyNames="cod_cuenta"
                Style="font-size: x-small">
                <Columns>
                    <%--<asp:BoundField DataField="cod_cuenta" HeaderStyle-CssClass="gridColNo gridIco" ItemStyle-CssClass="gridColNo">
                        <%--<HeaderStyle CssClass="gridIco"></HeaderStyle>
                    </asp:BoundField>--%>
                    <asp:TemplateField HeaderStyle-CssClass="gridIco" ItemStyle-CssClass="gridIco">
                        <ItemTemplate>
                            <asp:ImageButton ID="btnInfo" runat="server" CommandName="Select" ImageUrl="~/Images/gr_info.jpg"
                                ToolTip="Detalle" /></ItemTemplate>
                        <HeaderStyle CssClass="gridIco"></HeaderStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderStyle-CssClass="gridIco" ItemStyle-CssClass="gridIco">
                        <ItemTemplate>
                            <asp:ImageButton ID="btnEditar" runat="server" CommandName="Edit" ImageUrl="~/Images/gr_edit.jpg"
                                ToolTip="Modificar" /></ItemTemplate>
                        <HeaderStyle CssClass="gridIco"></HeaderStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderStyle-CssClass="gridIco" ItemStyle-CssClass="gridIco">
                        <ItemTemplate>
                            <asp:ImageButton ID="btnBorrar" runat="server" CommandName="Delete" ImageUrl="~/Images/gr_elim.jpg"
                                ToolTip="Borrar" /></ItemTemplate>
                        <HeaderStyle CssClass="gridIco"></HeaderStyle>
                    </asp:TemplateField>
                    <asp:BoundField DataField="cod_cuenta" HeaderText="Cod.Cuenta Local">
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="nombre" HeaderText="Nombre">
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="tipo" HeaderText="Tipo" />
                    <asp:BoundField DataField="nivel" HeaderText="Nivel" />
                    <asp:BoundField DataField="depende_de" HeaderText="Depende de" />
                    <asp:BoundField DataField="moneda" HeaderText="Moneda" />
                    <asp:BoundField DataField="estado" HeaderText="Activa" />
                    <asp:TemplateField HeaderText="Terceros">
                        <ItemTemplate>
                            <asp:CheckBox ID="chkTerceros" runat="server" EnableViewState="true" Checked='<%#Convert.ToBoolean(Eval("maneja_ter")) %>'
                                Enabled="False" /></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Maneja C/C">
                        <ItemTemplate>
                            <asp:CheckBox ID="chkManejaCC" runat="server" EnableViewState="true" Checked='<%#Convert.ToBoolean(Eval("maneja_cc")) %>'
                                Enabled="False" /></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Maneja C/G">
                        <ItemTemplate>
                            <asp:CheckBox ID="chkManejaCG" runat="server" EnableViewState="true" Checked='<%#Convert.ToBoolean(Eval("maneja_sc")) %>'
                                Enabled="False" /></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Maneja Impuestos">
                        <ItemTemplate>
                            <asp:CheckBox ID="chkManejaImp" runat="server" EnableViewState="true" Checked='<%#Convert.ToBoolean(Eval("impuesto")) %>'
                                Enabled="False" /></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Maneja CxP">
                        <ItemTemplate>
                            <asp:CheckBox ID="chkManejaCP" runat="server" EnableViewState="true" Checked='<%#Convert.ToBoolean(Eval("maneja_gir")) %>'
                                Enabled="False" /></ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="base_minima" HeaderText="Base Mínima" />
                    <asp:BoundField DataField="porcentaje_impuesto" HeaderText="% Impuesto" />                    
                </Columns>
                <HeaderStyle CssClass="gridHeader"></HeaderStyle>
                <PagerStyle CssClass="gridPager"></PagerStyle>
                <RowStyle CssClass="gridItem"></RowStyle>
            </asp:GridView>
            <asp:Label ID="lblTotalRegs" runat="server" Visible="False" />
        </asp:View>
         <asp:View ID="vwImportacion" runat="server">
             <table cellpadding="2">
                 <tr>
                     <td style="text-align: left;" colspan="4">
                         <strong>Criterios de carga</strong>
                     </td>
                 </tr>
                 <tr>
                     <td style="text-align: left">
                         Fecha de Carga<br />
                         <ucFecha:fecha ID="ucFecha" runat="server" Enabled="true" />
                     </td>
                     <td style="text-align: left">
                         Formato de fecha<br />
                         <asp:DropDownList ID="ddlFormatoFecha" runat="server" CssClass="textbox" Width="160px">
                         </asp:DropDownList>
                     </td>
                      <td style="text-align: left">
                        Tipo de Carga<br />
                        <asp:UpdatePanel ID="updPanel" runat="server">
                            <ContentTemplate>
                                <asp:RadioButtonList ID="rblTipoCarga" runat="server" 
                                    RepeatDirection="Horizontal" AutoPostBack="true"
                                    Width="220px" onselectedindexchanged="rblTipoCarga_SelectedIndexChanged">
                                    <asp:ListItem Value="1" Selected="True">Plan de Cuentas</asp:ListItem>
                                    <asp:ListItem Value="2">Balance</asp:ListItem>
                                </asp:RadioButtonList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                     <td style="text-align: left">
                         <asp:FileUpload ID="fupArchivoPersona" runat="server" />
                     </td>
                 </tr>
                 <tr>
                     <td colspan="4" style="text-align: left">
                         <strong>Separador del archivo : </strong>&nbsp;&nbsp;&nbsp;|
                     </td>
                 </tr>
                 <tr>
                     <td colspan="4" style="text-align: left; font-size: x-small">
                          <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <asp:Panel ID="panelPlanCta" runat="server">
                                    <table width="100%">
                                        <tr>
                                            <td style="text-align: left; font-size: x-small">
                                                <strong>Estructura de archivo : </strong>&nbsp;&nbsp;&nbsp; Cod Cuenta, Nombre Cuenta, Tipo, Nivel, Depende de,
                                                Cod Moneda, Maneja CC, Maneja Ter, Maneja CG, Estado.
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <asp:Panel ID="panelBalance" runat="server">
                                    <table width="100%">
                                        <tr>
                                            <td style="text-align: left; font-size: x-small">
                                            <strong>Estructura de archivo : </strong>&nbsp;&nbsp;&nbsp; Centro Costo, Cod Cuenta, Fecha, Valor.
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </ContentTemplate>
                            <Triggers><asp:AsyncPostBackTrigger ControlID="rblTipoCarga" EventName="SelectedIndexChanged" /></Triggers>
                        </asp:UpdatePanel>
                     </td>
                 </tr>
                 <tr>
                     <td style="text-align: left" colspan="3">
                         <asp:Button ID="btnCargarCuentas" runat="server" CssClass="btn8" OnClick="btnCargarCuentas_Click"
                             Height="22px" Text="Cargar Cuentas" Width="150px" />
                     </td>
                 </tr>
             </table>
             <hr style="width: 100%" />
             <table cellpadding="2" width="100%">
                 <tr>
                     <td>
                         <asp:Panel ID="pErrores" runat="server">
                             <asp:Panel ID="pEncBusqueda1" runat="server" CssClass="collapsePanelHeader" Height="30px">
                                 <div style="padding: 5px; cursor: pointer; vertical-align: middle;">
                                     <div style="float: left; margin-left: 5px; color: #0066FF; font-size: small">
                                         <asp:Label ID="lblMostrarDetalles1" runat="server" />
                                         <asp:ImageButton ID="imgExpand1" runat="server" ImageUrl="~/Images/expand.jpg" />
                                     </div>
                                 </div>
                             </asp:Panel>
                             <asp:Panel ID="pConsultaAfiliacion" runat="server" Width="100%">
                                 <div style="border-style: none; border-width: medium; background-color: #f5f5f5">
                                     <asp:GridView ID="gvErrores" runat="server" Width="100%" ShowHeaderWhenEmpty="True"
                                         AutoGenerateColumns="False" SelectedRowStyle-Font-Size="XX-Small" Style="font-size: x-small;
                                         margin-bottom: 0px;">
                                         <Columns>
                                             <asp:BoundField DataField="numero_registro" HeaderText="No." ItemStyle-Width="50"
                                                 ItemStyle-HorizontalAlign="Left" />
                                             <asp:BoundField DataField="datos" HeaderText="Datos" ItemStyle-Width="200" ItemStyle-HorizontalAlign="Left" />
                                             <asp:BoundField DataField="error" HeaderText="Error" ItemStyle-Width="200" ItemStyle-HorizontalAlign="Left" />
                                         </Columns>
                                         <HeaderStyle CssClass="gridHeader" />
                                         <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                                         <RowStyle CssClass="gridItem" />
                                         <SelectedRowStyle Font-Size="XX-Small"></SelectedRowStyle>
                                     </asp:GridView>
                                 </div>
                             </asp:Panel>
                             <asp:CollapsiblePanelExtender ID="cpeDemo1" runat="Server" CollapseControlID="pEncBusqueda1"
                                 Collapsed="True" CollapsedImage="~/Images/expand.jpg" CollapsedText="(Click Aqui para Mostrar Detalles...)"
                                 ExpandControlID="pEncBusqueda1" ExpandedImage="~/Images/collapse.jpg" ExpandedText="(Click Aqui para Ocultar Detalles...)"
                                 ImageControlID="imgExpand1" SkinID="CollapsiblePanelDemo" SuppressPostBack="true"
                                 TargetControlID="pConsultaAfiliacion" TextLabelID="lblMostrarDetalles1" />
                             <br />
                         </asp:Panel>
                     </td>
                 </tr>
                 <tr>
                    <td>
                        <asp:Panel ID="panelPlanCta2" runat="server">
                            <div style="overflow: scroll; max-height: 550px; width: 100%">
                                <asp:GridView ID="gvDatos" runat="server" Width="90%" AutoGenerateColumns="False"
                                    GridLines="Horizontal" ShowHeaderWhenEmpty="True" OnRowDeleting="gvDatos_RowDeleting"
                                    DataKeyNames="cod_cuenta" Style="font-size: x-small">
                                    <Columns>
                                        <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/gr_elim.jpg" ShowDeleteButton="True" />
                                        <asp:BoundField DataField="cod_cuenta" HeaderText="Cod. Cuenta">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="nombre" HeaderText="Nombre">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="tipo" HeaderText="Tipo">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="nivel" HeaderText="Nivel">
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="depende_de" HeaderText="Depende de">
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="cod_moneda" HeaderText="Cod. Moneda">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Maneja CC">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkCC" runat="server" EnableViewState="true" Checked='<%#Convert.ToBoolean(Eval("maneja_cc")) %>'
                                                    Enabled="False" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Terceros">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkTerceros" runat="server" EnableViewState="true" Checked='<%#Convert.ToBoolean(Eval("maneja_ter")) %>'
                                                    Enabled="False" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>                                        
                                        <asp:TemplateField HeaderText="Maneja CG">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkCG" runat="server" EnableViewState="true" Checked='<%#Convert.ToBoolean(Eval("maneja_sc")) %>'
                                                    Enabled="False" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="estado" HeaderText="Estado">
                                            <ItemStyle HorizontalAlign="left" />
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
                 <tr>
                     <td>
                         <asp:Panel ID="panelBalance2" runat="server">
                             <div style="overflow: scroll; max-height: 550px; width: 100%">
                                 <asp:GridView ID="gvBalance" runat="server" AutoGenerateColumns="False" GridLines="Horizontal"
                                     ShowHeaderWhenEmpty="True" OnRowDeleting="gvBalance_RowDeleting" DataKeyNames="cod_cuenta"
                                     Style="font-size: x-small">
                                     <Columns>
                                         <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/gr_elim.jpg" ShowDeleteButton="True" />
                                         <asp:BoundField DataField="centro_costo" HeaderText="Centro Costo">
                                             <ItemStyle HorizontalAlign="Left" />
                                         </asp:BoundField>
                                         <asp:BoundField DataField="fecha" HeaderText="Fecha" DataFormatString="{0:d}">
                                             <ItemStyle HorizontalAlign="Center" />
                                         </asp:BoundField>
                                         <asp:BoundField DataField="cod_cuenta" HeaderText="Cod. Cuenta">
                                             <ItemStyle HorizontalAlign="Left" />
                                         </asp:BoundField>
                                         <asp:BoundField DataField="valor" HeaderText="Valor">
                                             <ItemStyle HorizontalAlign="Right" />
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
         </asp:View>
         <asp:View ID="vwFinal" runat="server">
            <table style="width: 100%;">
                <tr>
                    <td style="text-align: center; font-size: large;">
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center; font-size: large;">
                        Importación de datos generada correctamente.
                    </td>
                </tr>
            </table>
        </asp:View>
    </asp:MultiView>
    <uc4:mensajegrabar ID="ctlMensaje" runat="server" />
</asp:Content>