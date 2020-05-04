<%@ Page Language="C#" MasterPageFile="~/General/Master/solicitud.master" AutoEventWireup="true"
    CodeFile="Lista.aspx.cs" Inherits="Lista" Title=".: Xpinn - SolicitudCreditosRecogidos :." %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:MultiView ID="mvCreditosRecogidos" runat="server" ActiveViewIndex="0">
        <asp:View ID="View1" runat="server">
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td colspan="3">
                        <asp:Panel ID="pConsulta" runat="server" Visible="false">
                            <table id="tbCriterios" border="0" cellpadding="5" cellspacing="0" width="100%">
                                <tr>
                                    <td class="tdI">
                                        Idsolicitudrecoge&nbsp;<asp:CompareValidator ID="cvIDSOLICITUDRECOGE" runat="server"
                                            ControlToValidate="txtIDSOLICITUDRECOGE" ErrorMessage="Solo se admiten n&uacute;meros"
                                            Operator="DataTypeCheck" SetFocusOnError="True" Type="Double" ValidationGroup="vgGuardar"
                                            Display="Dynamic" ForeColor="Red" /><br />
                                        <asp:TextBox ID="txtIdsolicitudrecoge" CssClass="textbox" runat="server" MaxLength="128" />
                                    </td>
                                    <td class="tdD">
                                        Numerosolicitud&nbsp;<asp:CompareValidator ID="cvNUMEROSOLICITUD" runat="server"
                                            ControlToValidate="txtNUMEROSOLICITUD" ErrorMessage="Solo se admiten n&uacute;meros"
                                            Operator="DataTypeCheck" SetFocusOnError="True" Type="Double" ValidationGroup="vgGuardar"
                                            Display="Dynamic" ForeColor="Red" /><br />
                                        <asp:TextBox ID="txtNumerosolicitud" CssClass="textbox" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdI">
                                        Numero_recoge&nbsp;<asp:CompareValidator ID="cvNUMERO_RECOGE" runat="server" ControlToValidate="txtNUMERO_RECOGE"
                                            ErrorMessage="Solo se admiten n&uacute;meros" Operator="DataTypeCheck" SetFocusOnError="True"
                                            Type="Double" ValidationGroup="vgGuardar" Display="Dynamic" ForeColor="Red" /><br />
                                        <asp:TextBox ID="txtNumero_recoge" CssClass="textbox" runat="server" MaxLength="128" />
                                    </td>
                                    <td class="tdD">
                                        Fecharecoge&nbsp;<asp:CompareValidator ID="cvFECHARECOGE" runat="server" ControlToValidate="txtFECHARECOGE"
                                            ErrorMessage="Formato de Fecha (dd/mm/aaaa)" Operator="DataTypeCheck" SetFocusOnError="True"
                                            ToolTip="Formato fecha" Type="Date" ValidationGroup="vgGuardar" Display="Dynamic"
                                            ForeColor="Red" /><br />
                                        <asp:TextBox ID="txtFecharecoge" CssClass="textbox" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdI">
                                        Valorrecoge&nbsp;<asp:CompareValidator ID="cvVALORRECOGE" runat="server" ControlToValidate="txtVALORRECOGE"
                                            ErrorMessage="Solo se admiten n&uacute;meros" Operator="DataTypeCheck" SetFocusOnError="True"
                                            Type="Double" ValidationGroup="vgGuardar" Display="Dynamic" ForeColor="Red" /><br />
                                        <asp:TextBox ID="txtValorrecoge" CssClass="textbox" runat="server" MaxLength="128" />
                                    </td>
                                    <td class="tdD">
                                        Fechapago&nbsp;<asp:CompareValidator ID="cvFECHAPAGO" runat="server" ControlToValidate="txtFECHAPAGO"
                                            ErrorMessage="Formato de Fecha (dd/mm/aaaa)" Operator="DataTypeCheck" SetFocusOnError="True"
                                            ToolTip="Formato fecha" Type="Date" ValidationGroup="vgGuardar" Display="Dynamic"
                                            ForeColor="Red" /><br />
                                        <asp:TextBox ID="txtFechapago" CssClass="textbox" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdI">
                                        Saldocapital&nbsp;<asp:CompareValidator ID="cvSALDOCAPITAL" runat="server" ControlToValidate="txtSALDOCAPITAL"
                                            ErrorMessage="Solo se admiten n&uacute;meros" Operator="DataTypeCheck" SetFocusOnError="True"
                                            Type="Double" ValidationGroup="vgGuardar" Display="Dynamic" ForeColor="Red" /><br />
                                        <asp:TextBox ID="txtSaldocapital" CssClass="textbox" runat="server" MaxLength="128" />
                                    </td>
                                    <td class="tdD">
                                        Saldointcorr&nbsp;<asp:CompareValidator ID="cvSALDOINTCORR" runat="server" ControlToValidate="txtSALDOINTCORR"
                                            ErrorMessage="Solo se admiten n&uacute;meros" Operator="DataTypeCheck" SetFocusOnError="True"
                                            Type="Double" ValidationGroup="vgGuardar" Display="Dynamic" ForeColor="Red" /><br />
                                        <asp:TextBox ID="txtSaldointcorr" CssClass="textbox" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdI">
                                        Saldointmora&nbsp;<asp:CompareValidator ID="cvSALDOINTMORA" runat="server" ControlToValidate="txtSALDOINTMORA"
                                            ErrorMessage="Solo se admiten n&uacute;meros" Operator="DataTypeCheck" SetFocusOnError="True"
                                            Type="Double" ValidationGroup="vgGuardar" Display="Dynamic" ForeColor="Red" /><br />
                                        <asp:TextBox ID="txtSaldointmora" CssClass="textbox" runat="server" MaxLength="128" />
                                    </td>
                                    <td class="tdD">
                                        Saldomipyme&nbsp;<asp:CompareValidator ID="cvSALDOMIPYME" runat="server" ControlToValidate="txtSALDOMIPYME"
                                            ErrorMessage="Solo se admiten n&uacute;meros" Operator="DataTypeCheck" SetFocusOnError="True"
                                            Type="Double" ValidationGroup="vgGuardar" Display="Dynamic" ForeColor="Red" /><br />
                                        <asp:TextBox ID="txtSaldomipyme" CssClass="textbox" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdI">
                                        Saldoivamipyme&nbsp;<asp:CompareValidator ID="cvSALDOIVAMIPYME" runat="server" ControlToValidate="txtSALDOIVAMIPYME"
                                            ErrorMessage="Solo se admiten n&uacute;meros" Operator="DataTypeCheck" SetFocusOnError="True"
                                            Type="Double" ValidationGroup="vgGuardar" Display="Dynamic" ForeColor="Red" /><br />
                                        <asp:TextBox ID="txtSaldoivamipyme" CssClass="textbox" runat="server" MaxLength="128" />
                                    </td>
                                    <td class="tdD">
                                        Saldootros&nbsp;<asp:CompareValidator ID="cvSALDOOTROS" runat="server" ControlToValidate="txtSALDOOTROS"
                                            ErrorMessage="Solo se admiten n&uacute;meros" Operator="DataTypeCheck" SetFocusOnError="True"
                                            Type="Double" ValidationGroup="vgGuardar" Display="Dynamic" ForeColor="Red" /><br />
                                        <asp:TextBox ID="txtSaldootros" CssClass="textbox" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <hr width="100%" noshade />
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        Identificación
                        <asp:RequiredFieldValidator ID="rfvIdentificacion" runat="server" ControlToValidate="txtIdentificacion"
                            ErrorMessage="Campo Requerido" SetFocusOnError="True" ValidationGroup="vgProductosProceso"
                            ForeColor="Red" Display="Dynamic" />
                        <br />
                        <asp:TextBox ID="txtIdentificacion" runat="server"></asp:TextBox>
                        <br />
                        Fecha
                        <%-- <asp:RequiredFieldValidator ID="rfvFecha" runat="server" ControlToValidate="txtFecha"
                    ErrorMessage="Campo Requerido" SetFocusOnError="True" ValidationGroup="vgGuardar"
                    ForeColor="Red" Display="Dynamic" />--%>
                        <asp:RequiredFieldValidator ID="rfvFecha" runat="server" ControlToValidate="txtFecha"
                            Display="Dynamic" ErrorMessage="Campo Requerido" ForeColor="Red" SetFocusOnError="True"
                            ValidationGroup="vgProductosProceso" />
                        <br />
                        <asp:TextBox ID="txtFecha" runat="server"></asp:TextBox>
                        <asp:MaskedEditExtender ID="mskFecha" runat="server" TargetControlID="txtFecha" Mask="99/99/9999"
                            MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError"
                            MaskType="Date" DisplayMoney="Left" AcceptNegative="Left" ErrorTooltipEnabled="True" />
                        <asp:MaskedEditValidator ID="mevFecha" runat="server" ControlExtender="mskFecha"
                            ControlToValidate="txtFecha" EmptyValueMessage="Fecha Requerida" InvalidValueMessage="Fecha No Valida"
                            Display="Dynamic" TooltipMessage="Seleccione una Fecha" EmptyValueBlurredText="Fecha No Valida"
                            InvalidValueBlurredMessage="Fecha No Valida" ValidationGroup="vgProductosProceso"
                            ForeColor="Red" />
                        <asp:CalendarExtender ID="txtFechanacimiento_CalendarExtender" runat="server" DaysModeTitleFormat="dd/MM/yyyy"
                            Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtFecha" TodaysDateFormat="dd/MM/yyyy">
                        </asp:CalendarExtender>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <asp:GridView ID="gvLista" runat="server" Width="100%" AutoGenerateColumns="False"
                            OnRowDataBound="gvLista_RowDataBound" OnRowDeleting="gvLista_RowDeleting" AllowPaging="True"
                            OnPageIndexChanging="gvLista_PageIndexChanging" OnSelectedIndexChanged="gvLista_SelectedIndexChanged"
                            OnRowEditing="gvLista_RowEditing" PageSize="20" HeaderStyle-CssClass="gridHeader"
                            PagerStyle-CssClass="gridPager" RowStyle-CssClass="gridItem" DataKeyNames="IDSOLICITUDRECOGE"
                            OnRowCommand="gvLista_RowCommand" AllowSorting="True">
                            <Columns>
                                <asp:TemplateField HeaderStyle-CssClass="gridIco" ItemStyle-CssClass="gridIco" Visible="false">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnInfo" runat="server" CommandName="Select" ImageUrl="~/Images/gr_info.jpg"
                                            ToolTip="Detalle" />
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="gridIco"></HeaderStyle>
                                    <ItemStyle CssClass="gridIco"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-CssClass="gridIco" ItemStyle-CssClass="gridIco" Visible="false">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnEditar" runat="server" CommandName="Edit" ImageUrl="~/Images/gr_edit.jpg"
                                            ToolTip="Modificar" />
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="gridIco"></HeaderStyle>
                                    <ItemStyle CssClass="gridIco"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-CssClass="gridIco" ItemStyle-CssClass="gridIco" Visible="false">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnBorrar" runat="server" CommandName="Delete" ImageUrl="~/Images/gr_elim.jpg"
                                            ToolTip="Borrar" />
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="gridIco"></HeaderStyle>
                                    <ItemStyle CssClass="gI" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="numeroRadicacion" HeaderText="Numero Radicacion" />
                                <asp:BoundField DataField="linea" HeaderText="Linea" />
                                <asp:BoundField DataField="monto" HeaderText="Monto" DataFormatString="{0:d}" />
                                <asp:BoundField DataField="saldocapitalTemp" HeaderText="Saldo Capital" />
                                <asp:BoundField DataField="interescorriente" HeaderText="Interes Corriente" />
                                <asp:BoundField DataField="interesmora" HeaderText="Interes Mora" />
                                <asp:BoundField DataField="seguro" HeaderText="Seguro" />
                                <asp:BoundField DataField="leymipyme" HeaderText="Ley Mipyme" />
                                <asp:BoundField DataField="ivaLeymipyme" HeaderText="Iva Ley Mipyme" />
                                <asp:BoundField DataField="otros" HeaderText="Otros" />
                                <asp:BoundField DataField="totalRecoger" HeaderText="Total Recoger" />
                                <%-- <asp:TemplateField HeaderText="Total Recoger" HeaderStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <asp:Label ID="lblTotalRecoger" runat="server" Text='<%# Eval("totalRecoger") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                        </asp:TemplateField>--%>
                                <asp:TemplateField HeaderText="Recoger" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkRecoger" runat="server" AutoPostBack="true" OnCheckedChanged="Check_Clicked" />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <%--  <asp:CheckBoxField  Visible="true"  DataField="null" />--%>
                            </Columns>
                            <HeaderStyle CssClass="gridHeader"></HeaderStyle>
                            <PagerStyle CssClass="gridPager"></PagerStyle>
                            <RowStyle CssClass="gridItem"></RowStyle>
                        </asp:GridView>
                        <asp:Label ID="lblTotalRegs" runat="server" Visible="False" />
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;</td>
                    <td>
                        <asp:ImageButton ID="btnLiquidacionCreditos" runat="server" ImageUrl="~/Images/btnLiquidacionCreditos.jpg"
                            OnClick="btnLiquidacionCreditos_Click" ValidationGroup="vgProductosProceso" />
                    </td>
                    <td>
                        Valor Total a Recoger:
                        <asp:TextBox ID="txtValorTotalRecoger" runat="server" ReadOnly="True"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </asp:View>
        <asp:View ID="View2" runat="server">
            <table width="100%">
                <tr>
                    <td style="text-align: center">
                        <br />
                        <br />
                        &nbsp;
                        <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                        <br />
                        <br />
                        <asp:ImageButton ID="btnAceptar" runat="server" ImageUrl="~/Images/btnAceptar.jpg"
                            OnClick="btnAceptar_Click" />
                    </td>
                </tr>
            </table>
        </asp:View>
    </asp:MultiView>
    <script type="text/javascript" language="javascript">
        function SetFocus() {
            document.getElementById('cphMain_txtIDSOLICITUDRECOGE').focus();
        }
        window.onload = SetFocus;
    </script>
</asp:Content>
