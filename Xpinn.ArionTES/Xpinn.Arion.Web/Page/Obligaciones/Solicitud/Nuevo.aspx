<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../../../General/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <asp:MultiView ID="mvSolicitud" runat="server">
        <asp:View ID="vwSolicitud" runat="server">
            <table cellpadding="5" cellspacing="0" style="width: 95%">
                <tr>
                    <td>
                        <asp:Panel ID="Panel1" runat="server">
                            <table style="width: 100%;">
                                <tr>
                                    <td style="text-align: center; color: #FFFFFF; background-color: #0066FF">
                                        <strong>Solicitud de Obligación</strong>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Panel ID="Panel2" runat="server">
                                            <table style="width: 70%;">
                                                <tr>
                                                    <td style="width: 60px; font-size: x-small;">Usuario
                                                    </td>
                                                    <td style="width: 89px">
                                                        <asp:TextBox ID="txtCajero" runat="server" CssClass="textbox" Enabled="False"
                                                            Width="346px"></asp:TextBox>
                                                    </td>
                                                    <td style="font-size: x-small; text-align: right">Fecha
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtFechaTransaccion" runat="server" CssClass="textbox"
                                                            Enabled="False" Width="145px"></asp:TextBox>
                                                    </td>
                                                    <td>&nbsp;
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
                <tr style="text-align: left">
                    <td>
                        <hr />
                    </td>
                </tr>
                <tr style="text-align: left">
                    <td>
                        <strong>Información General de la Obligación</strong>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Panel ID="Panel3" runat="server">
                            <table style="width: 100%;">
                                <tr valign="top">
                                    <td style="width: 100px; text-align: left">Entidad<br />
                                        <asp:DropDownList ID="ddlEntidad" runat="server" CssClass="textbox" Height="27px" Width="302px" OnSelectedIndexChanged="CargarNit_SelectedIndexChanged" AutoPostBack="True">
                                        </asp:DropDownList>
                                    </td>
                                    <td style="width: 100px; text-align: left">Nit Entidad<br />
                                        <asp:TextBox ID="TxtNit" CssClass="textbox" Width="120px" MaxLength="20" Enabled="false"
                                            runat="server"></asp:TextBox>
                                    </td>
                                    <td class="gridIco" style="width: 120px; text-align: left">Línea Obligación<br />
                                        <asp:DropDownList ID="ddlLineaObligacion" runat="server" CssClass="textbox"
                                            Height="27px" Width="183px" AutoPostBack="True"
                                            OnSelectedIndexChanged="ddlLineaObligacion_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="gridIco" style="width: 120px; text-align: left">Fecha Solicitud<br />
                                        <asp:TextBox ID="txtFechaSolicitud" runat="server" CssClass="textbox"
                                            Width="90px"></asp:TextBox>
                                        <img id="Image1" alt="Calendario" src="../../../Images/iconCalendario.png" />
                                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True"
                                            Format="dd/MM/yyyy" PopupButtonID="Image1" TargetControlID="txtFechaSolicitud">
                                        </asp:CalendarExtender>
                                        <asp:RequiredFieldValidator ID="rfvtxtFechaIni" runat="server"
                                            ControlToValidate="txtFechaSolicitud" ErrorMessage="*" ForeColor="Red"
                                            ValidationGroup="vgGuardar" Display="Dynamic"></asp:RequiredFieldValidator>
                                        &nbsp;
                                        <asp:RegularExpressionValidator ID="revtxtFechaIni" runat="server"
                                            ControlToValidate="txtFechaSolicitud" ErrorMessage="Formato Fecha Invalida"
                                            ForeColor="Red"
                                            ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d"
                                            ValidationGroup="vgGuardar" Display="Dynamic"></asp:RegularExpressionValidator>
                                    </td>
                                    <td style="width: 120px; text-align: left">Fecha Desembolso<br />
                                        <asp:TextBox ID="txtFechaDesembolso" runat="server" CssClass="textbox"
                                            MaxLength="12" Width="100px"></asp:TextBox>
                                        <img id="Image2" alt="Calendario" src="../../../Images/iconCalendario.png" />
                                        <asp:CalendarExtender ID="ceFechaDesembolso" runat="server" Enabled="True"
                                            Format="dd/MM/yyyy" PopupButtonID="Image2" TargetControlID="txtFechaDesembolso">
                                        </asp:CalendarExtender>
                                        <asp:RequiredFieldValidator ID="rfvFechaDesembolso" runat="server"
                                            ControlToValidate="txtFechaDesembolso" ErrorMessage="*" ForeColor="Red"
                                            ValidationGroup="vgGuardar" Display="Dynamic"></asp:RequiredFieldValidator>
                                        &nbsp;
                                    <asp:RegularExpressionValidator ID="revFechaDesembolso" runat="server"
                                        ControlToValidate="txtFechaDesembolso" ErrorMessage="Formato Fecha Invalida" ForeColor="Red"
                                        ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d"
                                        ValidationGroup="vgGuardar" Display="Dynamic"></asp:RegularExpressionValidator>
                                    </td>
                                    <td style="width: 120px; text-align: left">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td class="gridIco" style="width: 120px; text-align: left">Cuenta Desembolso<br />
                                        <asp:DropDownList ID="ddlCuentas" runat="server" CssClass="textbox"
                                            Height="27px" Width="183px" AutoPostBack="True"
                                            OnSelectedIndexChanged="ddlCuentas_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
                <tr style="text-align: left">
                    <td>
                        <strong>Detalle de la Obligación</strong>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Panel ID="Panel4" runat="server" Width="1071px">
                            <table style="width: 100%;">
                                <tr valign="top">
                                    <td style="width: 460px; text-align: left">Monto Solicitado<br />
                                        <uc1:decimales ID="txtMontoSol" runat="server" MaxLength="15" CssClass="textbox" Width="100px" />
                                    </td>
                                    <td class="gridIco" style="width: 300px; text-align: left">Monto Aprobado<br />
                                        <asp:TextBox ID="txtMontoApro" CssClass="textbox" Enabled="False"
                                            Width="183px" runat="server"></asp:TextBox>
                                        <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtMontoApro"
                                            Mask="999,999,999,999,999" MaskType="Number" InputDirection="RightToLeft"
                                            AcceptNegative="Left" DisplayMoney="Left"
                                            ErrorTooltipEnabled="True" CultureAMPMPlaceholder=""
                                            CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                            CultureDatePlaceholder="" CultureDecimalPlaceholder=""
                                            CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True">
                                        </asp:MaskedEditExtender>
                                    </td>
                                    <td style="width: 447px; text-align: left">Tipo Moneda<br />
                                        <asp:DropDownList ID="ddlTipoMoneda" runat="server" CssClass="textbox"
                                            Width="161px">
                                        </asp:DropDownList>
                                    </td>
                                    <td style="width: 370px; text-align: left">Tipo de Cuota<br />
                                        <asp:DropDownList ID="ddlTipoCuota" runat="server" CssClass="textbox"
                                            Height="25px" Width="138px" Enabled="false">
                                        </asp:DropDownList>
                                    </td>

                                    <td style="width: 700px; text-align: left">No Pagaré<br />
                                        <asp:TextBox ID="txtPagare" CssClass="textbox" Width="120px" MaxLength="20"
                                            runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvPagare" runat="server"
                                            ControlToValidate="txtPagare" ErrorMessage="*" ForeColor="Red"
                                            ValidationGroup="vgGuardar" Display="Dynamic"></asp:RequiredFieldValidator>
                                        &nbsp;
                                    <asp:FilteredTextBoxExtender ID="ftbPagare" runat="server" TargetControlID="txtPagare" FilterType="Custom, Numbers" ValidChars="0123456789" />
                                    </td>
                                    <td style="width: 700px; text-align: left">&nbsp;</td>
                                </tr>
                            </table>
                            <table style="width: 100%;">
                                <tr valign="top">
                                    <td style="width: 179px; text-align: left">Plazo<br />
                                        <asp:TextBox ID="txtPlazo" CssClass="textbox" MaxLength="3" Width="65px"
                                            runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvPlazo" runat="server" ValidationGroup="vgGuardar" ForeColor="Red" ControlToValidate="txtPlazo"
                                            ErrorMessage="*" Display="Dynamic"></asp:RequiredFieldValidator>
                                        <asp:FilteredTextBoxExtender ID="ftbPlazo" runat="server" TargetControlID="txtPlazo" FilterType="Custom, Numbers" ValidChars="0123456789" />
                                    </td>
                                    <td style="width: 901px; text-align: left">
                                        <asp:UpdatePanel ID="upGracia" runat="server">
                                            <ContentTemplate>
                                                Plazo Gracia<br />
                                                <asp:TextBox ID="txtGracia" CssClass="textbox" MaxLength="2" Width="65px"
                                                    runat="server"></asp:TextBox>
                                                <asp:FilteredTextBoxExtender ID="ftbGracia" runat="server" TargetControlID="txtGracia" FilterType="Custom, Numbers" ValidChars="0123456789" />
                                                <asp:DropDownList ID="ddlTipoGracia" runat="server" AutoPostBack="True"
                                                    CssClass="dropdown" Height="27px" Width="109px">
                                                    <asp:ListItem Value="0">Sin Gracia</asp:ListItem>
                                                    <asp:ListItem Value="1">Sobre Capital</asp:ListItem>
                                                    <asp:ListItem Value="2">Muerta</asp:ListItem>
                                                </asp:DropDownList>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        <asp:RequiredFieldValidator ID="rfvGracia" runat="server"
                                            ValidationGroup="vgGuardar" ForeColor="Red" ControlToValidate="txtGracia"
                                            ErrorMessage="*" Display="Dynamic"></asp:RequiredFieldValidator>&#160;
                                    </td>
                                    <td class="gridIco" style="width: 199px; text-align: left">Periodicidad Cuotas<br />
                                        <asp:DropDownList ID="ddlPeriodCuotas" runat="server" CssClass="textbox"
                                            Height="27px" Width="143px">
                                        </asp:DropDownList>
                                    </td>
                                    <td style="width: 901px; text-align: left">
                                        <asp:RadioButtonList ID="rbCalculoTasa" runat="server"
                                            Style="font-size: x-small" AutoPostBack="True"
                                            OnSelectedIndexChanged="rbCalculoTasa_SelectedIndexChanged">
                                            <asp:ListItem Value="1">Tasa Fija</asp:ListItem>
                                            <asp:ListItem Value="2">Tasa Variable</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                    <td style="width: 401px; text-align: left">Tipo de Tasa<br />
                                        <asp:DropDownList ID="ddlTipoTasa" runat="server" CssClass="textbox"
                                            Height="27px" Width="149px" AutoPostBack="True"
                                            OnSelectedIndexChanged="ddlTipoTasa_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                    <td style="width: 271px; text-align: left">Valor Tasa (%)<br />
                                        <%--<asp:TextBox ID="txtValorTasa" runat="server" CssClass="textbox" Enabled="true" Height="22px" Width="101px"></asp:TextBox>
                                        <asp:MaskedEditExtender ID="MaskedEditExtender2" runat="server" TargetControlID="txtValorTasa"
                                            Mask="99.99" MaskType="Number" InputDirection="RightToLeft"
                                            AcceptNegative="Left"
                                            ErrorTooltipEnabled="True" CultureAMPMPlaceholder=""
                                            CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                            CultureDatePlaceholder="" CultureDecimalPlaceholder=""
                                            CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True">
                                        </asp:MaskedEditExtender>--%>
                                        <asp:TextBox ID="txtValorTasa" runat="server" CssClass="textbox" Enabled="true" Height="22px" Width="101px" onkeypress="return isDecimalNumber(event);" ></asp:TextBox>
                                    </td>
                                    <td style="width: 700px; text-align: left">Puntos Adicionales(%)<br />
                                        <asp:TextBox ID="txtPuntosads" CssClass="textbox" Width="100px" MaxLength="6" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table width="40%">
                            <tr valign="top">
                                <td align="center">COMPONENTES ADICIONALES
                                    <br />
                                    <asp:GridView ID="gvComponente" runat="server" Width="100%" AutoGenerateColumns="False"
                                        BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px"
                                        CellPadding="4" ForeColor="Black" GridLines="Vertical" Height="16px" ShowFooter="True"
                                        OnRowDataBound="gvComponente_RowDataBound" OnRowCommand="gvComponente_RowCommand"
                                        OnRowDeleting="gvComponente_RowDeleting"
                                        Style="font-size: xx-small; font-weight: 700; text-align: center;">
                                        <AlternatingRowStyle BackColor="White" />
                                        <Columns>
                                            <asp:TemplateField ShowHeader="False">
                                                <FooterTemplate>
                                                    <asp:ImageButton ID="btnNuevo0" runat="server" CausesValidation="False" CommandName="AddNew"
                                                        ImageUrl="~/Images/gr_nuevo.jpg" ToolTip="Crear Nuevo" Width="16px" />
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="btnEliminar" runat="server" CommandName="Delete" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                                        ImageUrl="~/Images/gr_elim.jpg" ToolTip="Eliminar" Width="16px" />
                                                </ItemTemplate>
                                                <HeaderStyle CssClass="gridIco" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Componente">
                                                <FooterTemplate>
                                                    <asp:DropDownList ID="ddlComponente" runat="server">
                                                    </asp:DropDownList>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblComponente" runat="server"
                                                        Text='<%# Bind("nomcomponente") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Fórmula">
                                                <FooterTemplate>
                                                    <asp:DropDownList ID="ddlFormula" runat="server">
                                                        <asp:ListItem Value="1">Constante</asp:ListItem>
                                                        <asp:ListItem Value="2">Monto por Valor</asp:ListItem>
                                                        <asp:ListItem Value="3">Saldo por Valor</asp:ListItem>
                                                        <asp:ListItem Value="4">Cuota por Valor</asp:ListItem>
                                                        <asp:ListItem Value="5">(Valor por Monto)/plazo</asp:ListItem>
                                                    </asp:DropDownList>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblFormula" runat="server" Text='<%# Bind("nomformula") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Valor">
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtnewvalor" MaxLength="19" runat="server" Text='<%# Eval("valor","{0:N2}") %>'></asp:TextBox>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblvalor" runat="server" Text='<%# Eval("valor","{0:N2}") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Financiado">
                                                <FooterTemplate>
                                                    <asp:CheckBox ID="chkFinanciado" runat="server" />
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkFinanciado" runat="server" Enabled="false" />
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
                                </td>
                                <td align="center">PAGOS EXTRAORDINARIOS<br />
                                    <asp:GridView ID="gvPagosExt" runat="server" Width="100%"
                                        AutoGenerateColumns="False" BackColor="White" BorderColor="#DEDFDE"
                                        BorderStyle="None" BorderWidth="1px"
                                        CellPadding="4" ForeColor="Black" GridLines="Vertical" Height="16px"
                                        ShowFooter="True" OnRowDataBound="gvPagosExt_RowDataBound" OnRowCommand="gvPagosExt_RowCommand"
                                        OnRowDeleting="gvPagosExt_RowDeleting"
                                        Style="font-size: xx-small; font-weight: 700; text-align: center;">
                                        <AlternatingRowStyle BackColor="White" />
                                        <Columns>
                                            <asp:TemplateField ShowHeader="False">
                                                <FooterTemplate>
                                                    <asp:ImageButton ID="btnNuevo0" runat="server" CausesValidation="False" CommandName="AddNew"
                                                        ImageUrl="~/Images/gr_nuevo.jpg" ToolTip="Crear Nuevo" Width="16px" />
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="btnEliminar" runat="server" CommandName="Delete" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                                        ImageUrl="~/Images/gr_elim.jpg" ToolTip="Eliminar" Width="16px" />
                                                </ItemTemplate>
                                                <HeaderStyle CssClass="gridIco" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Periodo">
                                                <FooterTemplate>
                                                    <asp:DropDownList ID="ddPeriodo" runat="server">
                                                    </asp:DropDownList>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPeriodo" runat="server"
                                                        Text='<%# Bind("nomperiodo") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Valor">
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtnewvalor" MaxLength="19" runat="server" Text='<%# Eval("valor","{0:N2}") %>'></asp:TextBox>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblvalor2" runat="server" Text='<%# Eval("valor","{0:N2}") %>'></asp:Label>
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
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </asp:View>

        <asp:View ID="vwPlanPagos" runat="server">
            <table cellpadding="5" cellspacing="0" style="width: 95%">
                <tr>
                    <td>
                        <asp:Panel ID="Panel5" runat="server">
                            <table style="width: 100%;">
                                <tr>
                                    <td style="text-align: center; color: #FFFFFF; background-color: #0066FF">
                                        <strong>Reporte del Plan de Pagos de la Obligación Financiera</strong></td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
                <tr style="text-align: left">
                    <td>
                        <hr />
                    </td>
                </tr>
                <tr style="text-align: left">
                    <td>
                        <strong>Plan de Amortización</strong>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Panel ID="Panel6" runat="server">
                            <table style="width: 82%;">
                                <tr valign="top">
                                    <td style="width: 100px; text-align: left">No Obligación:</td>
                                    <td class="gridIco" style="width: 120px; text-align: left">
                                        <asp:Label ID="lblNroObligacion" runat="server"></asp:Label>
                                    </td>
                                    <td class="gridIco" style="width: 120px; text-align: left">Valor Solicitado</td>
                                    <td style="width: 120px; text-align: left">
                                        <asp:TextBox ID="txtValSol" runat="server" Enabled="False" CssClass="textbox"
                                            Width="183px"></asp:TextBox>
                                        &nbsp;</td>
                                </tr>
                                <tr valign="top">
                                    <td style="width: 100px; text-align: left">Tipo Tasa:</td>
                                    <td class="gridIco" style="width: 120px; text-align: left">
                                        <asp:Label ID="lblTasaEfectiva" runat="server"></asp:Label>
                                    </td>
                                    <td class="gridIco" style="width: 120px; text-align: left">Valores Descontados</td>
                                    <td style="width: 120px; text-align: left" rowspan="3">
                                        <asp:ListBox ID="lbxValDescon" runat="server" Enabled="False" Width="193px"></asp:ListBox>
                                        <br />
                                    </td>
                                </tr>
                                <tr valign="top">
                                    <td style="width: 100px; text-align: left">Tasa Interés:
                                    </td>
                                    <td class="gridIco" style="width: 120px; text-align: left">
                                        <asp:Label ID="lblTasaIntPer" runat="server"></asp:Label>
                                    </td>
                                    <td class="gridIco" style="width: 120px; text-align: left" rowspan="2"></td>
                                </tr>
                                <tr valign="top">
                                    <td style="width: 100px; text-align: left">Cuota:</td>
                                    <td class="gridIco" style="width: 120px; text-align: left">
                                        <asp:Label ID="lblCuota" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr valign="top">
                                    <td style="width: 100px; text-align: left">&nbsp;</td>
                                    <td class="gridIco" style="width: 120px; text-align: left">&nbsp;</td>
                                    <td class="gridIco" style="width: 120px; text-align: left">Valor Desembolsado
                                    </td>
                                    <td style="width: 120px; text-align: left">
                                        <asp:TextBox ID="txtValDesem" runat="server" CssClass="textbox" Enabled="False"
                                            Width="183px"></asp:TextBox>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                            <ContentTemplate>
                                                <asp:GridView ID="gvObPlan" runat="server"
                                                    AutoGenerateColumns="False" PageSize="20" BackColor="White"
                                                    BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4"
                                                    ForeColor="Black" GridLines="Vertical" OnRowCommand="gvObPlan_RowCommand" Style="font-size: x-small" Width="813px">
                                                    <AlternatingRowStyle BackColor="White" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderStyle-CssClass="gridIco">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="btnDistPagos" runat="server"
                                                                    CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                                                    CommandName="DetallePago" ImageUrl="~/Images/gr_info.jpg"
                                                                    ToolTip="Dist Pagos" />
                                                            </ItemTemplate>
                                                            <HeaderStyle CssClass="gridIco"></HeaderStyle>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="nrocuota" HeaderText="No" />
                                                        <asp:BoundField DataField="fecha" DataFormatString="{0:d}" HeaderText="Fecha" />
                                                        <asp:BoundField DataField="amort_cap" HeaderText="Amortización Capital" DataFormatString="{0:N0}">
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="interes_corriente" HeaderText="Interéses" DataFormatString="{0:N0}">
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="seguro" HeaderText="Seguro" DataFormatString="{0:N0}">
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="otros" HeaderText="Otros" DataFormatString="{0:N0}">
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="cuotaextra" HeaderText="Cuota Extra" DataFormatString="{0:N0}">
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="cuotanormal" HeaderText="Cuota Normal" DataFormatString="{0:N0}">
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="saldo" HeaderText="Saldo" DataFormatString="{0:N0}">
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="cuotatotal" HeaderText="Cuota Total" DataFormatString="{0:N0}">
                                                            <ItemStyle HorizontalAlign="Right" />
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
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
            </table>

            <asp:HiddenField ID="HiddenField1" runat="server" />
            <asp:ModalPopupExtender ID="mpeRegObPlanPago" runat="server" Enabled="True"
                TargetControlID="HiddenField1" PopupControlID="Panels1"
                BackgroundCssClass="modalBackground" DropShadow="true"
                CancelControlID="CancelButton"
                OnCancelScript="onCancel()">
            </asp:ModalPopupExtender>

            <asp:Panel ID="Panels1" runat="server" BackColor="White">
                <asp:UpdatePanel ID="UpdatePanels1" runat="server">
                    <ContentTemplate>
                        <table cellpadding="5" cellspacing="0" style="width: 50%" border="0">
                            <tr>
                                <td>No Cuota 
                                <asp:TextBox ID="txtNroCuota" runat="server" Enabled="false" CssClass="textbox"></asp:TextBox>
                                </td>
                                <td>Fecha
                                <asp:TextBox ID="txtFechaCuota" runat="server" Enabled="false" CssClass="textbox"></asp:TextBox>
                                </td>
                            </tr>
                            <tr align="center">
                                <td>
                                    <b>Componente</b>
                                </td>
                                <td>
                                    <b>Valor</b>
                                </td>
                            </tr>
                            <tr>
                                <td>Capital
                                </td>
                                <td>
                                    <uc1:decimales ID="txtCapital" runat="server" CssClass="textbox" Width="163px" />
                                </td>
                            </tr>
                            <tr>
                                <td>Int. Corriente
                                </td>
                                <td>
                                    <uc1:decimales ID="txtIntCorr" runat="server" CssClass="textbox" Width="163px" />
                                </td>
                            </tr>
                            <tr>
                                <td>Interes Mora
                                </td>
                                <td>
                                    <uc1:decimales ID="txtIntMora" runat="server" CssClass="textbox" Width="163px" />
                                </td>
                            </tr>
                            <tr>
                                <td>Seguro
                                </td>
                                <td>
                                    <uc1:decimales ID="txtSeguro" runat="server" CssClass="textbox" Width="163px" />
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="2">
                                    <asp:Button ID="AceptarButton" runat="server" Text="Guardar" CssClass="btn8" OnClick="AceptarButton_Click" />&#160;
                                <asp:Button ID="CancelButton" runat="server" Text="Cancelar" CssClass="btn8" />
                                </td>
                            </tr>
                        </table>
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
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <asp:Label ID="lblMensajeGrabar" runat="server" Text="Obligación No. Grabada Correctamente"></asp:Label>
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
                            <asp:Button ID="btnContinuar" runat="server" Text="Continuar"
                                OnClick="btnContinuar_Click" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </asp:View>

    </asp:MultiView>

</asp:Content>
