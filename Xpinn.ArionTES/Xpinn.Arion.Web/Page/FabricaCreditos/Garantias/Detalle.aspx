<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/General/Master/site.master" CodeFile="Detalle.aspx.cs" Inherits="Detalle" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <script src="../../../Scripts/PCLBryan.js" type="text/javascript"></script>
    <asp:MultiView ID="mvGarantia" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwDatos" runat="server">
            <table cellpadding="5" cellspacing="0" style="width: 100%">
                <tr style="text-align: left">
                    <td>
                        <strong>Deudor</strong>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table style="width: 100%;">
                            <tr>
                                <td style="text-align: left; width: 15%">No Radicación
                                </td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtNroRadicacion" Enabled="false" runat="server" CssClass="textbox"></asp:TextBox>
                                    <asp:Label ID="lblCodOpe" runat="server" Visible="false"></asp:Label>
                                </td>
                                <td style="text-align: right; width: 15%">Tipo Identificación
                                </td>
                                <td style="text-align: left; width: 15%">
                                    <asp:DropDownList ID="ddlTipoIdentificacion" Enabled="false" runat="server" CssClass="textbox"
                                        Width="150px">
                                    </asp:DropDownList>
                                </td>
                                <td style="text-align: right; width: 15%">Identificación
                                </td>
                                <td style="text-align: left; width: 15%">
                                    <asp:TextBox ID="txtIdentificacion" Enabled="false" runat="server" CssClass="textbox" MaxLength="12"></asp:TextBox>
                                    <asp:Label ID="lblCodPersona" runat="server" Visible="false"></asp:Label>
                                </td>
                                <td style="text-align: left">&nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left; width: 15%">Nombres y Apellidos
                                </td>
                                <td style="text-align: left; width: 75%" colspan="5">
                                    <asp:TextBox ID="txtNombreCliente" runat="server" CssClass="textbox"
                                        Enabled="false" Width="98%"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left; width: 15%">Monto
                                </td>
                                <td style="text-align: left; width: 15%">
                                    <asp:TextBox ID="txtMonto" runat="server" Enabled="false" CssClass="textbox"></asp:TextBox>
                                    <asp:MaskedEditExtender ID="MaskedEditExtender3" runat="server" TargetControlID="txtMonto" Mask="999,999,999,999,999" MessageValidatorTip="true"
                                        OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="Number" InputDirection="RightToLeft"
                                        AcceptNegative="Left" DisplayMoney="Left" ErrorTooltipEnabled="True">
                                    </asp:MaskedEditExtender>
                                </td>
                                <td style="text-align: right; width: 15%">Línea
                                </td>
                                <td style="text-align: left; width: 15%">
                                    <asp:TextBox ID="txtLinea" runat="server" Enabled="false" CssClass="textbox"></asp:TextBox>
                                </td>
                                <td style="text-align: right; width: 15%">Nombre línea
                                </td>
                                <td style="text-align: left; width: 15%">
                                    <asp:TextBox ID="txtNombreLinea" runat="server" Enabled="false" CssClass="textbox"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>

            <table cellpadding="5" cellspacing="0" style="width: 90%" border="0">
                <tr>
                    <td align="left">
                        <b>Datos Del Bien en Garantía</b>
                    </td>
                    <td class="tdI" colspan="2" style="height: 27px; text-align: right;">
                        <asp:ImageButton ID="btnCrearActivoFijo" runat="server"
                            ImageUrl="~/Images/btnNuevoActivoFijo.jpg" OnClick="btnCrearActivoFijo_Click" />
                    </td>
                </tr>
                <tr>
                    <td colspan="3" style="text-align: left">
                        <asp:GridView ID="gvGarantiasHipo" runat="server"
                            AutoGenerateColumns="False" AllowPaging="False" PageSize="20" BackColor="White"
                            BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4"
                            ForeColor="Black" GridLines="Vertical" DataKeyNames="IdActivo"
                            OnRowCommand="gvBienesActivos_OnRowCommand"
                            OnRowDeleting="gvBienesActivos_RowDeleting"
                            OnRowEditing="gvBienesActivos_RowEditing"
                            Width="100%">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:TemplateField HeaderText="Selección">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkSelect" runat="server" OnClick="javascript:SelectSingleCheckBox(this.id)" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/gr_elim.jpg" ShowDeleteButton="True">
                                    <ItemStyle Width="16px" />
                                </asp:CommandField>
                                <asp:TemplateField HeaderStyle-CssClass="gridIco">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnEditar" runat="server" CommandName="Edit" ImageUrl="~/Images/gr_edit.jpg"
                                            ToolTip="Editar" Width="16px" />
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="gridIco"></HeaderStyle>
                                </asp:TemplateField>
                                <asp:BoundField DataField="descripcion_activo" HeaderText="Tipo de Activo" ItemStyle-Width="20%" />
                                <asp:BoundField DataField="Descripcion" HeaderText="Descripción" ItemStyle-Width="60%" />
                                <asp:BoundField DataField="valor_comercial" HeaderText="Valor Comercial" DataFormatString="{0:N0}" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="Fecha_adquisicionactivo" DataFormatString="{0:d}" HeaderText="Fecha de Adquisición" />
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hiddenTipoGarantia" Value='<%# Eval("tipo_garantia") %>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
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
                </tr>
            </table>

            <asp:Panel ID="Panels4" runat="server">
                <asp:UpdatePanel ID="UpdatePanels4" runat="server">
                    <ContentTemplate>
                        <table cellpadding="5" cellspacing="0" style="width: 90%">
                            <tr>
                                <td style="width: 140px; text-align: left">Fecha Garantía
                                </td>
                                <td style="width: 100px; text-align: left">
                                    <asp:TextBox ID="txtFechaSuscrip" CssClass="textbox" runat="server" Width="90px"></asp:TextBox>
                                    <asp:CalendarExtender ID="CalendarExtender1" runat="server"
                                        PopupButtonID="Image1"
                                        TargetControlID="txtFechaSuscrip"
                                        Format="dd/MM/yyyy">
                                    </asp:CalendarExtender>
                                    <img id="Image1" alt="Calendario" src="../../../Images/iconCalendario.png" />
                                    <asp:RequiredFieldValidator ID="rfvtxtFechaIni" runat="server" ValidationGroup="vgGuardar" ForeColor="Red" ControlToValidate="txtFechaSuscrip" ErrorMessage="*" Display="Dynamic"></asp:RequiredFieldValidator>&#160;
                                    <asp:RegularExpressionValidator ID="revtxtFechaIni" runat="server" ValidationGroup="vgGuardar" ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d" ControlToValidate="txtFechaSuscrip" ForeColor="Red" ErrorMessage="Formato Fecha Invalida" Display="Dynamic"></asp:RegularExpressionValidator>
                                </td>
                                <td style="width: 100px; text-align: left">Valor Garantía
                                </td>
                                <td style="width: 100px; text-align: left">
                                    <asp:TextBox ID="txtValorGarantia" onkeypress="return isNumber(event)" CssClass="textbox" runat="server" DataFormatString="{0:n0}" Width="90px"></asp:TextBox>
                                </td>
                                <td colspan="2" align="center"><b>Póliza</b></td>
                            </tr>
                            <tr>
                                <td style="width: 140px; text-align: left">Ubicación
                                </td>
                                <td colspan="3" style="text-align: left">
                                    <asp:TextBox ID="txtUbicacion" CssClass="textbox" runat="server" Width="360px"></asp:TextBox>
                                </td>
                                <td style="text-align: left">Aseguradora
                                </td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtAseguradora" CssClass="textbox" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left">Encargado
                                </td>
                                <td colspan="3" style="text-align: left">
                                    <asp:TextBox ID="txtEncargado" CssClass="textbox" runat="server" Width="360px"></asp:TextBox>
                                </td>
                                <td style="text-align: left">Fecha Vencimiento Seguro
                                </td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtFechaVenc" CssClass="textbox" runat="server" Width="105px"></asp:TextBox>
                                    <img id="Image2" alt="Calendario" src="../../../Images/iconCalendario.png" />
                                    <asp:CalendarExtender ID="CalendarExtender2" runat="server"
                                        PopupButtonID="Image2"
                                        TargetControlID="txtFechaVenc"
                                        Format="dd/MM/yyyy">
                                    </asp:CalendarExtender>
                                    <asp:RegularExpressionValidator ID="revFechaVenc" runat="server" ValidationGroup="vgGuardar" ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d" ControlToValidate="txtFechaVenc" ForeColor="Red" ErrorMessage="Formato Fecha Invalida" Display="Dynamic"></asp:RegularExpressionValidator>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left">Estado
                                </td>
                                <td style="text-align: left">
                                    <asp:DropDownList ID="ddlEstadoGarantia" CssClass="textbox" runat="server" Width="160px">
                                        <asp:ListItem Value="1">Activo</asp:ListItem>
                                        <asp:ListItem Value="2">Terminado/Liberado</asp:ListItem>
                                        <asp:ListItem Value="3">Anulado</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>&#160;</td>
                                <td style="width: 156px">&#160;</td>
                                <td style="width: 160px; text-align: left">Fecha Último Avaluo</td>
                                <td style="width: 160px; text-align: left">
                                    <asp:TextBox ID="txtFechaUltAvaluo" CssClass="textbox" runat="server" Width="105px"></asp:TextBox>
                                    <img id="Image3" alt="Calendario" src="../../../Images/iconCalendario.png" />
                                    <asp:CalendarExtender ID="CalendarExtender3" runat="server"
                                        PopupButtonID="Image3"
                                        TargetControlID="txtFechaUltAvaluo"
                                        Format="dd/MM/yyyy">
                                    </asp:CalendarExtender>
                                    <asp:RegularExpressionValidator ID="revFechaUltAvaluo" runat="server" ValidationGroup="vgGuardar" ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d" ControlToValidate="txtFechaUltAvaluo" ForeColor="Red" ErrorMessage="Formato Fecha Invalida" Display="Dynamic"></asp:RegularExpressionValidator>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 140px; text-align: left">Fecha Liberación Garantía
                                </td>
                                <td style="width: 160px; text-align: left">
                                    <asp:TextBox ID="txtFechaLib" CssClass="textbox" runat="server" Width="90px"></asp:TextBox>
                                    <img id="Image4" alt="Calendario" src="../../../Images/iconCalendario.png" />
                                    <asp:CalendarExtender ID="CalendarExtender4" runat="server"
                                        PopupButtonID="Image4"
                                        TargetControlID="txtFechaLib"
                                        Format="dd/MM/yyyy">
                                    </asp:CalendarExtender>
                                    <asp:RegularExpressionValidator ID="revFechaLib" runat="server" ValidationGroup="vgGuardar" ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d" ControlToValidate="txtFechaLib" ForeColor="Red" ErrorMessage="Formato Fecha Invalida" Display="Dynamic"></asp:RegularExpressionValidator>
                                </td>
                                <td>&#160;</td>
                                <td style="width: 156px">&#160;</td>
                                <td style="width: 160px; text-align: left">Valor Avaluo
                                </td>
                                <td style="width: 160px; text-align: left">
                                    <asp:TextBox ID="txtValorAvaluo" onkeypress="return isNumber(event)" CssClass="textbox" runat="server" DataFormatString="{0:n0}"></asp:TextBox>

                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </asp:Panel>
        </asp:View>
        <asp:View ID="mvActivoFijo" runat="server">
            <asp:Panel ID="panelMostrarModal" runat="server" BackColor="White" Style="text-align: left"
                Width="447px">
                <asp:UpdatePanel ID="upReclasificacion" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <table border="0" cellpadding="0" cellspacing="0" width="90%">
                            <tr>
                                <td class="tdI" style="font-weight: 700; text-align: left; height: 24px;"
                                    colspan="7">
                                    <hr style="width: 100%" />
                                </td>
                                <td class="tdD" style="text-align: left">&nbsp;</td>
                            </tr>
                            <tr>
                                <td class="tdI" style="height: 27px; text-align: left;" colspan="1">&nbsp;
                                    <strong>ACTIVOS FIJOS  </strong> <asp:TextBox ID="txtIDActivo" Visible="false" runat="server" />
                                </td>
                                <tr>
                                    <td class="tdI" style="height: 27px; text-align: left;" colspan="1">&nbsp;
                                        <strong>Datos del Crédito:  </strong>
                                    </td>
                                </tr>
                            <tr>
                                <td class="tdD" style="height: 36px; width: 148px;">Identificación
                                    <asp:TextBox ID="txtModalIdentificacion" Enabled="false" runat="server" CssClass="textbox"
                                        MaxLength="128" Width="196px" />
                                </td>
                                <td class="tdD" style="height: 36px; width: 148px;" valign="top">Tipo Identificación<br />
                                    <asp:DropDownList ID="ddlModalIdentificacion" Enabled="false" runat="server"
                                        CssClass="textbox" Width="199px">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="tdD" colspan="3" style="text-align: left">Nombres
                                    <asp:TextBox ID="txtModalNombres" Enabled="false" runat="server" CssClass="textbox" Style="text-transform: uppercase"
                                        MaxLength="128" Width="410px" />
                                </td>
                            </tr>
                            <tr>
                                <td class="tdD" style="height: 36px; width: 148px;">Tipo de Activo<br />
                                    <asp:DropDownList ID="ddlModalTipoActivo" runat="server"
                                        CssClass="textbox" Width="199px" AutoPostBack="true" OnSelectedIndexChanged="ddlModalTipoActivo_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                            </tr>

                            <tr>
                                <td class="tdD" colspan="3" style="text-align: left">Descripción
                                    <asp:TextBox ID="txtModalDescripcion" runat="server" CssClass="textbox" Style="text-transform: uppercase"
                                        MaxLength="128" Width="410px" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Panel ID="Panel1" runat="server" Width="130px">
                                        <asp:Label ID="LabelFecha_gara" runat="server" Text="Fecha Adquisición"></asp:Label>
                                        <asp:TextBox ID="txtModalFechaIni" MaxLength="10" CssClass="textbox"
                                            runat="server" Height="20px" Width="80px"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender5" runat="server"
                                            PopupButtonID="ImageCalendario1"
                                            TargetControlID="txtModalFechaIni"
                                            Format="dd/MM/yyyy">
                                        </asp:CalendarExtender>
                                        <img id="ImageCalendario1" alt="Calendario"
                                            src="../../../Images/iconCalendario.png" />
                                    </asp:Panel>
                                </td>
                                <td class="tdI" style="width: 171px; text-align: left" colspan="2">Valor Comercial:
                                    <br />
                                    <asp:TextBox ID="txtModalValorComercial" runat="server" onkeypress="return isNumber(event)" CssClass="textbox" MaxLength="128" Width="196px" Style="text-transform: uppercase" />

                                </td>
                                <td class="tdI" style="width: 171px; text-align: left" colspan="2">Valor Comprometido: 
                                    <br />
                                    <asp:TextBox ID="txtModalValorComprometido" runat="server" onkeypress="return isNumber(event)" CssClass="textbox" MaxLength="128" Width="196px" Style="text-transform: uppercase" />

                                </td>
                            </tr>
                            <tr>
                                <td class="tdI" colspan="7" style="text-align: left; width: 684px;">
                                    <hr style="width: 100%" />
                                </td>
                            </tr>
                        </table>
                        <asp:Panel ID="panelTipoActivoInmueble" Visible="false" runat="server">
                            <table>
                                <tr>
                                    <td class="tdD" style="height: 36px; width: 148px;">Dirección
                                        <asp:TextBox ID="txtModalDireccion" runat="server" CssClass="textbox"
                                            MaxLength="128" Width="196px" />
                                    </td>
                                    <td class="tdD" style="height: 36px; width: 148px;">Localización
                                        <asp:TextBox ID="txtModalLocalizacion" runat="server" CssClass="textbox"
                                            MaxLength="128" Width="196px" />
                                    </td>
                                    <td class="tdD" style="height: 36px; width: 148px;">VIS<br />
                                        <asp:DropDownList ID="ddlModalVIS" Width="180px" AutoPostBack="true" CssClass="textbox" runat="server"  OnSelectedIndexChanged="ddlModalVIS_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Sin VIS
                                            </asp:ListItem>
                                            <asp:ListItem Value="1">Con VIS
                                            </asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdD" style="height: 36px; width: 148px;">Nro. Matricula
                                        <asp:TextBox ID="txtModalNoMatricula" runat="server" CssClass="textbox"
                                            MaxLength="128" Width="196px" />
                                    </td>
                                    <td class="tdD" style="height: 36px; width: 148px;">Escritura
                                        <asp:TextBox ID="txtModalEscritura" runat="server" CssClass="textbox"
                                            MaxLength="128" Width="196px" />
                                    </td>
                                    <td class="tdD" style="height: 36px; width: 148px;">Notaria
                                        <asp:TextBox ID="txtModalNotaria" runat="server" CssClass="textbox"
                                            MaxLength="128" Width="196px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdD" style="height: 36px; width: 148px;">Entidad Redescuento<br />
                                        <%--<asp:TextBox ID="txtModalEntidadReDesc" runat="server" CssClass="textbox"
                                                MaxLength="128" Width="196px" />--%>
                                        <asp:DropDownList ID="ddlModalEntidadReDesc" runat="server"
                                            CssClass="textbox" Width="199px">
                                            <asp:ListItem Value="0">Ninguna</asp:ListItem>
                                            <asp:ListItem Value="1">FINDETER</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td class="tdD" style="height: 36px; width: 148px;">Margen Redescuento<br />
                                        <asp:TextBox ID="txtModalmargenReDesc" runat="server" CssClass="textbox"
                                            MaxLength="128" Width="196px" />
                                        <asp:FilteredTextBoxExtender ID="ftb12" runat="server" Enabled="True" FilterType="Numbers, Custom"
                                            TargetControlID="txtModalmargenReDesc" ValidChars=".," />
                                    </td>
                                    <td class="tdD" style="height: 36px; width: 148px;">Tipo Vivienda<br />
                                        <asp:DropDownList ID="ddlModalTipoVivienda" runat="server"
                                            CssClass="textbox" Width="199px">
                                            <asp:ListItem Value="1">Nueva
                                            </asp:ListItem>
                                            <asp:ListItem Value="2">Usada
                                            </asp:ListItem>
                                            <asp:ListItem Value="3">Mejoramiento
                                            </asp:ListItem>
                                            <asp:ListItem Value="4">Lote con servicios
                                            </asp:ListItem>
                                            <asp:ListItem Value="5">Construccion en sitio propio
                                            </asp:ListItem>
                                        </asp:DropDownList>
                                    </td>

                                </tr>
                                <tr>
                                    <td class="tdD" style="height: 36px; width: 148px;">Desembolso
                                        <%--<asp:TextBox ID="txtModalDesembolso" runat="server" CssClass="textbox"
                                            MaxLength="128" Width="196px" />--%>
                                        <asp:DropDownList ID="ddlModalDesembolso" runat="server"
                                            CssClass="textbox" Width="199px">
                                            <asp:ListItem Value="1">Desembolso Directo</asp:ListItem>
                                            <asp:ListItem Value="2">Desembolso a Constructor</asp:ListItem>
                                            <asp:ListItem Value="3">Subrogración</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td class="tdD" style="height: 36px; width: 148px;">Desembolso Directo
                                        <asp:TextBox ID="txtModalDesembolsoDirecto" runat="server" CssClass="textbox"
                                            MaxLength="128" Width="196px" />
                                    </td>
                                    <td class="tdD" style="height: 36px; width: 148px;">Rango Vivienda<br />
                                        <asp:DropDownList ID="ddlModalRangoVivienda" runat="server"
                                            CssClass="textbox" Width="199px">
                                        </asp:DropDownList>
                                    </td>

                                </tr>
                            </table>
                        </asp:Panel>
                        <asp:Panel ID="pnlTipoActivoMaquinaria" Visible="false" runat="server">
                            <table>
                                <tr>
                                    <td class="tdD" style="height: 36px; width: 148px;">Marca
                                        <asp:TextBox ID="txtModalMarca" runat="server" CssClass="textbox"
                                            MaxLength="128" Width="196px" />
                                    </td>
                                    <td class="tdD" style="height: 36px; width: 148px;">Referencia
                                        <asp:TextBox ID="txtModalReferencia" runat="server" CssClass="textbox"
                                            MaxLength="128" Width="196px" />
                                    </td>
                                    <td class="tdD" style="height: 36px; width: 148px;">Modelo
                                        <asp:TextBox ID="txtModalModelo" runat="server" CssClass="textbox"
                                            MaxLength="128" Width="196px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdD" style="height: 36px; width: 148px;">Uso<br />
                                        <asp:DropDownList ID="ddlModalUso" Width="180px" class="dropdown" runat="server" Height="35px">
                                            <asp:ListItem Value="1">Particular
                                            </asp:ListItem>
                                            <asp:ListItem Value="2">Publico
                                            </asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td class="tdD" style="height: 36px; width: 148px;">No.Chasis
                                        <asp:TextBox ID="txtModalNoChasis" runat="server" CssClass="textbox"
                                            MaxLength="128" Width="196px" />
                                    </td>
                                    <td class="tdD" style="height: 36px; width: 148px;">Capacidad
                                        <asp:TextBox ID="txtModalCapacidad" runat="server" CssClass="textbox"
                                            MaxLength="128" Width="196px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdD" style="height: 36px; width: 148px;">No.Serie Motor
                                        <asp:TextBox ID="txtModalNoSerieMotor" runat="server"
                                            CssClass="textbox" Width="199px" />
                                    </td>
                                    <td class="tdD" style="height: 36px; width: 148px;">Placa
                                        <asp:TextBox ID="txtModalPlaca" runat="server" CssClass="textbox"
                                            MaxLength="128" Width="196px" />
                                    </td>
                                    <td class="tdD" style="height: 36px; width: 148px;">Color
                                        <asp:TextBox ID="txtModalColor" runat="server" CssClass="textbox"
                                            MaxLength="128" Width="196px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdD" style="height: 36px; width: 148px;">Doc.Importación
                                        <asp:TextBox ID="txtModalDocImportacion" runat="server"
                                            CssClass="textbox" Width="199px" />
                                    </td>
                                    <td>
                                        <asp:Panel ID="Panel2" runat="server" Width="130px">
                                            <asp:Label ID="Label1" runat="server" Text="Fecha Importación"></asp:Label>
                                            <asp:TextBox ID="txtModalFechaImportacion" MaxLength="10" CssClass="textbox"
                                                runat="server" Height="20px" Width="80px"></asp:TextBox>
                                            <asp:CalendarExtender ID="CalendarExtender6" runat="server"
                                                PopupButtonID="ImageCalendario10"
                                                TargetControlID="txtModalFechaIni"
                                                Format="dd/MM/yyyy">
                                            </asp:CalendarExtender>
                                            <img id="ImageCalendario10" alt="Calendario"
                                                src="../../../Images/iconCalendario.png" />
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </ContentTemplate>
                </asp:UpdatePanel>
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
                            <asp:Label ID="lblMensaje" runat="server" Text="Operación Realizada Correctamente"
                                Style="color: #FF3300"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="btnRegresarGarantia" Visible="false" runat="server" Text="Regresar Garantía" OnClick="btnRegresarGarantia_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="btnRegresarLista" Visible="false" runat="server" Text="Regresar Lista" OnClick="btnRegresarLista_Click" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </asp:View>
    </asp:MultiView>
    <uc4:mensajegrabar ID="ctlMensaje" runat="server" />
    <uc4:mensajegrabar ID="ctlMensajeActivoFijo" runat="server" />
</asp:Content>
