<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" %>

<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>
<%@ Register Src="~/General/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:MultiView ID="mvAplicar" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwLista" runat="server">
            <table style="width: 90%; text-align: center" cellspacing="0" cellpadding="0">
                <tr>
                    <td style="text-align: left; width: 150px">Código<br />
                        <asp:TextBox ID="txtCodigo" runat="server" CssClass="textbox" Width="60%"></asp:TextBox>
                        <asp:FilteredTextBoxExtender ID="fte123" runat="server" TargetControlID="txtCodigo"
                            FilterType="Custom, Numbers" ValidChars="" />
                    </td>
                    <td style="text-align: left; width: 180px">Tipo de Servicio<br />
                        <asp:DropDownList ID="ddlTipoServicio" runat="server" CssClass="textbox" Width="90%" />
                    </td>
                    <td style="text-align: left;" colspan="2">Descripción<br />
                        <asp:TextBox ID="txtDescripcion" runat="server" CssClass="textbox" Width="90%" />
                    </td>
                    <td style="text-align: left; width: 180px">Orden<br />
                        <asp:TextBox ID="txtOrden" Width="90%" CssClass="textbox" runat="server" onkeypress="return isNumber(event)" />
                    </td>
                    <td style="text-align: left; width: 180px">Tipo Cuota<br />
                        <asp:DropDownList ID="ddlTipoCuota" runat="server" CssClass="textbox" Width="90%">
                            <%--<asp:ListItem Text="Cuota Escalonada" Value="2" />--%>
                            <asp:ListItem Text="Cuota Fija" Value="1" />
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left; width: 150px">Identificación<br />
                        <asp:TextBox ID="txtIdentificacion" runat="server" CssClass="textbox"
                            Width="100px" />
                    </td>
                    <td style="text-align: left; width: 330px" colspan="2">Nombre<br />
                        <asp:TextBox ID="txtNombre" runat="server" CssClass="textbox" Width="270px" />
                    </td>
                    <td style="text-align: left; width: 350px">
                        <asp:CheckBox ID="chkOrdenServicio" runat="server" Text="No Requiere Aprobación" />
                    </td>
                    <td style="text-align: left; width: 150px">#ServicioxAño<br />
                        <asp:TextBox ID="txtServicioAño" runat="server" CssClass="textbox" Style="text-align: right"
                            Width="60%" />
                        <asp:FilteredTextBoxExtender ID="fte100" runat="server" TargetControlID="txtServicioAño"
                            FilterType="Custom, Numbers" ValidChars="" />
                    </td>
                    <td style="text-align: left; width: 300px">Maximo Plazo Cuotas
                        <br />
                        <asp:TextBox ID="txtMaximoPlazo" onkeypress="return isNumber(event)" runat="server" CssClass="textbox" Style="text-align: right"
                            Width="80%" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left; width: 150px">Periodo Renovación<br />
                        <asp:DropDownList ID="ddlPerRenovacion" runat="server" CssClass="textbox" Width="90%"
                            AppendDataBoundItems="True" />
                    </td>
                    <td style="text-align: left; width: 180px">Periodo de Pago<br />
                        <asp:DropDownList ID="ddlPerPago" runat="server" CssClass="textbox" Width="90%"
                            AppendDataBoundItems="True" />
                    </td>
                    <td style="text-align: left; width: 150px">Fec. Pago Proveedor<br />
                        <ucFecha:fecha ID="txtFechaPago" runat="server" CssClass="textbox" />
                    </td>
                    <td class="logo" style="text-align: left; width: 294px">
                        <asp:CheckBox ID="check1" runat="server" Text="Requiere Beneficiarios" Checked="true" />
                    </td>
                    <td style="text-align: left; width: 150px">#Beneficiarios<br />
                        <asp:TextBox ID="txtBeneficiario" runat="server" CssClass="textbox" Style="text-align: right"
                            Width="60%" />
                        <asp:FilteredTextBoxExtender ID="fte99" runat="server" TargetControlID="txtBeneficiario"
                            FilterType="Custom, Numbers" ValidChars="" />
                    </td>
                    <td style="text-align: left; width: 300px">Maximo Valor
                        <br />
                        <asp:TextBox ID="txtMaximoValor" onkeypress="return isNumber(event)" runat="server" CssClass="textbox" Style="text-align: right"
                            Width="80%" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left">Destinaciones Asociadas:                                            
                    </td>
                    <td style="text-align: left">
                        <asp:UpdatePanel ID="upRecoger" runat="server">
                            <ContentTemplate>
                                <asp:HiddenField ID="hfValue" runat="server" Visible="false" />
                                <asp:TextBox ID="txtRecoger" CssClass="textbox" runat="server" Width="145px" ReadOnly="True" Style="text-align: right" TextMode="SingleLine"></asp:TextBox>
                                <asp:PopupControlExtender ID="txtRecoger_PopupControlExtender" runat="server"
                                    Enabled="True" ExtenderControlID="" TargetControlID="txtRecoger"
                                    PopupControlID="panelLista" OffsetY="22">
                                </asp:PopupControlExtender>
                                <asp:Panel ID="panelLista" runat="server" Height="200px" Width="300px"
                                    BorderStyle="Solid" BorderWidth="2px" Direction="LeftToRight"
                                    ScrollBars="Auto" BackColor="#CCCCCC" Style="display: none">
                                    <asp:GridView ID="gvRecoger" runat="server" Width="100%" AutoGenerateColumns="False"
                                        HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager" RowStyle-CssClass="gridItem" DataKeyNames="cod_destino">

                                        <Columns>
                                            <asp:TemplateField HeaderText="Código">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_destino" runat="server" Text='<%# Bind("cod_destino") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" Width="50px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Descripción">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_descripcion" runat="server" Text='<%# Bind("descripcion") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-CssClass="gridIco">
                                                <ItemTemplate>
                                                    <cc1:CheckBoxGrid ID="cbListado" runat="server" CommandArgument='<%#DataBinder.Eval(Container, "RowIndex") %>'
                                                        AutoPostBack="false" />
                                                </ItemTemplate>
                                                <HeaderStyle CssClass="gridIco"></HeaderStyle>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </asp:Panel>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>

                    <td class="logo" style="text-align: left; width: 294px">
                        <asp:CheckBox ID="chkCausacion" runat="server" Text="Realiza Causación" Checked="false" />
                    </td>
                    <td class="logo" style="text-align: left; width: 294px">
                        <asp:CheckBox ID="chkMaRetirados" runat="server" Text="Maneja Retirados" Checked="false" />
                    </td>
                    <td style="text-align: left; width: 294px">
                        <asp:CheckBox ID="chkNoGenerarVacaciones" runat="server" Text="No generar vacaciones" />
                    </td>
                     <td style="text-align: left; width: 294px">
                        <asp:CheckBox ID="chkServicioTelefonia" runat="server" Text="Servicio de Telefonía" />
                    </td>
                </tr>

                <tr>
                    <td colspan="2" style="text-align: left">
                        <asp:CheckBox ID="chkOcultaData" runat="server" Text="Ocultar Información" />
                    </td>
                    <td colspan="2" style="text-align: left">
                        <asp:CheckBox ID="chkOficinaVirtual" runat="server" Text="Mostrar en oficina virtual" />
                    </td>
                    <td colspan="6" style="text-align: left">Enlace <br />
                        <asp:TextBox ID="txtEnlace" runat="server" CssClass="textbox" Style="width:100%;" />
                    </td>
                </tr>
                <%-- <tr>
                    <td style="text-align: left; width: 150px;">
                        
                    </td>
                   <td style="text-align: left; width: 180px">
                        <asp:CheckBox ID="chkInteres" runat="server" Text="Cobra Interés" 
                            AutoPostBack="True" oncheckedchanged="chkInteres_CheckedChanged"/>
                    </td>
                    <td style="text-align: left; width: 150px">
                        Tasa Interés<br />
                        <asp:TextBox ID="txtTasaInteres" runat="server" CssClass="textbox" Width="80px" style="text-align:right"/>
                        <asp:FilteredTextBoxExtender ID="fte1" runat="server" TargetControlID="txtTasaInteres"
                            FilterType="Custom, Numbers" ValidChars="+-=/*().," />
                    </td>
                    <td style="text-align: left; width: 294px">
                        Tipo de Tasa<br />
                        <asp:DropDownList ID="ddlTipoTasa" runat="server" CssClass="textbox" Width="90%" 
                            AppendDataBoundItems="True"/>
                    </td>
                </tr>--%>
            </table>
            <hr style="width: 100%" />
            <table>
                <tr>
                    <td style="text-align: left">
                        <strong>Tasas de Interes</strong><br />
                        <asp:ImageButton ID="btnnuevodeduccion" runat="server" ImageUrl="~/Images/btnNuevo.jpg"
                            OnClick="btnnuevodeduccion_Click" />
                        <asp:Label ID="lblTasa" runat="server" Visible="false" Text="Debes crear la linea antes de asignar tasas" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:GridView ID="gvTasas" runat="server" PageSize="20" ShowHeaderWhenEmpty="True"
                            AutoGenerateColumns="False" SelectedRowStyle-Font-Size="X-Small" Style="font-size: small; margin-bottom: 0px;"
                            OnRowDeleting="gvTasas_RowDeleting" DataKeyNames="codrango"
                            GridLines="Horizontal" OnRowEditing="gvTasas_RowEditing">
                            <Columns>
                                <asp:CommandField ButtonType="Image" EditImageUrl="~/Images/gr_edit.jpg" ShowEditButton="True" />
                                <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/gr_elim.jpg" ShowDeleteButton="True" />
                                <asp:BoundField DataField="codrango" HeaderText="Código">
                                    <ItemStyle HorizontalAlign="left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="descripcion" HeaderText="Descripción">
                                    <ItemStyle HorizontalAlign="left" Width="400px" />
                                </asp:BoundField>
                            </Columns>
                            <HeaderStyle CssClass="gridHeader" />
                            <RowStyle CssClass="gridItem" />
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left">
                        <asp:Label ID="lblInfo" runat="server" Text="Su consulta no obtuvo ningún resultado" Visible="false" />
                        <asp:Label ID="lblTotalRegTasa" runat="server" Visible="false" />
                    </td>
                </tr>
            </table>
            <asp:Panel ID="panelGrilla" runat="server">
                <hr style="width: 100%" />
                <table>
                    <tr>
                        <td style="text-align: left">
                            <strong>Planes</strong><br />
                            <asp:Button ID="btnAdicionarFila" runat="server" CssClass="btn8" OnClick="btnAdicionarFila_Click"
                                OnClientClick="btnAdicionarFila_Click" Text="+ Adicionar Detalle" Height="22px" />
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                <ContentTemplate>
                                    <asp:GridView ID="gvPlanes" runat="server" PageSize="20" ShowHeaderWhenEmpty="True"
                                        AutoGenerateColumns="False" SelectedRowStyle-Font-Size="XX-Small" Style="font-size: small; margin-bottom: 0px;"
                                        OnRowDataBound="gvPlanes_RowDataBound" OnRowDeleting="gvPlanes_RowDeleting"
                                        DataKeyNames="cod_plan_servicio" GridLines="Horizontal">
                                        <Columns>
                                            <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/gr_elim.jpg" ShowDeleteButton="True">
                                                <ItemStyle HorizontalAlign="left" />
                                            </asp:CommandField>
                                            <asp:TemplateField HeaderText="Codigo" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                <ItemTemplate>
                                                    <cc1:TextBoxGrid ID="txtCodigo" runat="server" Text='<%# Bind("cod_plan_servicio") %>'
                                                        Width="80px" CssClass="textbox"></cc1:TextBoxGrid>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Descripción" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <cc1:TextBoxGrid ID="txtDescripcion" runat="server" Text='<%# Bind("nombre") %>'
                                                        Width="180px" CssClass="textbox"></cc1:TextBoxGrid>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Numero Usuarios" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <cc1:TextBoxGrid ID="txtNumUsuarios" runat="server" Text='<%# Bind("numero_usuarios") %>'
                                                        Width="80px" MaxLength="8" CssClass="textbox" />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Edad Inicial" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <cc1:TextBoxGrid ID="txtEdadInicial" runat="server" Text='<%# Bind("edad_minima") %>'
                                                        Width="50px" MaxLength="4" CssClass="textbox" />
                                                    <asp:FilteredTextBoxExtender ID="fte8" runat="server" TargetControlID="txtEdadInicial"
                                                        FilterType="Custom, Numbers" ValidChars="" />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Edad Final" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <cc1:TextBoxGrid ID="txtEdadFinal" runat="server" Text='<%# Bind("edad_maxima") %>'
                                                        Width="50px" MaxLength="4" CssClass="textbox" />
                                                    <asp:FilteredTextBoxExtender ID="fte7" runat="server" TargetControlID="txtEdadFinal"
                                                        FilterType="Custom, Numbers" ValidChars="" />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Grupo Familiar">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblGrupo" runat="server" Text='<%# Bind("codgrupo_familiar") %>' Visible="false" />
                                                    <cc1:DropDownListGrid ID="ddlGrupo" runat="server" CommandArgument="<%#Container.DataItemIndex %>"
                                                        CssClass="textbox" Width="160px">
                                             </cc1:DropDownListGrid>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Valor">
                                                <ItemTemplate>
                                                    <uc1:decimales ID="txtValor" runat="server" Text='<%# Bind("valor") %>' Width="80px"
                                                        Style="text-align: right" />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="center" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <HeaderStyle CssClass="gridHeader" />
                                        <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                                        <RowStyle CssClass="gridItem" />
                                        <SelectedRowStyle Font-Size="XX-Small"></SelectedRowStyle>
                                    </asp:GridView>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
                
            </asp:Panel>
            <table style="width: 740px">
                <tr>
                    <td style="width: 740px; text-align: center">
                        <asp:Label ID="lblTotalReg" runat="server" Visible="False" />
                    </td>
                </tr>
            </table>
         <div style="margin-right: 90px;">
                Cargar los Banner's Para La Oficina Virtual (Solo Es Para La Oficina Virtual).
                   <asp:Label ID="lblerrorB" Text="" runat="server" /><br />
                <asp:FileUpload ID="fuBanner" runat="server" BorderWidth="0px" Font-Size="XX-Small"
                            Height="20px" ToolTip="Seleccionar el archivo que contiene el banner" Width="200px" />
                <asp:HiddenField ID="hdFileNameB" runat="server" />
                <asp:HiddenField ID="hdFileNameThumbB" runat="server" />
                <asp:LinkButton ID="linkBtB" runat="server" OnClick="linkBtB_Click" ClientIDMode="Static" UseSubmitBehavior="false" />
                <br />
                <asp:Image ID="imgBanner" runat="server" Height="216px" Width="82%" />
                <br />
                <asp:Button ID="btnCargarImagenB" runat="server" Text="Cargar Banner" Font-Size="xx-Small"
                        Height="20px" Width="100px" OnClick="btnCargarBanner_Click" ClientIDMode="Static" UseSubmitBehavior="false" />
            </div>
            <div style="float:right; margin-top:-285.5px;">
                    <asp:Label ID="lblerror" Text="" runat="server" /><br />
                    <asp:FileUpload ID="fuFoto" runat="server" BorderWidth="0px" Font-Size="XX-Small"
                                Height="20px" ToolTip="Seleccionar el archivo que contiene la foto" Width="200px" />
                    <asp:HiddenField ID="hdFileName" runat="server" />
                    <asp:HiddenField ID="hdFileNameThumb" runat="server" />
                    <asp:LinkButton ID="linkBt" runat="server" OnClick="linkBt_Click" ClientIDMode="Static" UseSubmitBehavior="false" />
                    <br />
                    <asp:Image ID="imgFoto" runat="server" Height="160px" Width="121px" />
                    <br />
                    <asp:Button ID="btnCargarImagen" runat="server" Text="Cargar Imagen" Font-Size="xx-Small"
                            Height="20px" Width="100px" OnClick="btnCargarImagen_Click" ClientIDMode="Static" UseSubmitBehavior="false" />
                </div>

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
                        <td style="text-align: center; font-size: large; color: Red">Linea de Servicio
                            <asp:Label ID="lblmsj" runat="server"></asp:Label>
                            &nbsp;correctamente.<br />
                            Nro. de Servicio :
                            <asp:Label ID="lblNroMsj" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <br />
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;">&nbsp;</td>
                    </tr>
                </table>
            </asp:Panel>
        </asp:View>
    </asp:MultiView>

    <asp:HiddenField ID="HiddenField1" runat="server" />

    <uc4:mensajegrabar ID="ctlMensaje" runat="server" />

</asp:Content>
