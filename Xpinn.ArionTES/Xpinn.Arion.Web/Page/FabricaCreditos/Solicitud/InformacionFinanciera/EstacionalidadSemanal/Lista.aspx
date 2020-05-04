<%@ Page Language="C#" MasterPageFile="~/General/Master/solicitudSub.master" AutoEventWireup="true"
    CodeFile="Lista.aspx.cs" Inherits="Lista" Title=".: Xpinn - VentasSemanales :." %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../../../../../General/Controles/decimales.ascx" TagName="decimales"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td>
                <asp:Panel ID="pConsulta" runat="server" Visible="false">
                    <table id="tbCriterios" border="0" cellpadding="5" cellspacing="0" width="100%">
                        <tr>
                            <td class="tdI">
                                Cod_ventas
                                <asp:CompareValidator ID="cvCOD_VENTAS" runat="server" ControlToValidate="txtCOD_VENTAS"
                                    ErrorMessage="Solo se admiten n&uacute;meros" Operator="DataTypeCheck" SetFocusOnError="True"
                                    Type="Double" ValidationGroup="vgGuardar" Display="Dynamic" ForeColor="Red" /><br />
                                <asp:TextBox ID="txtCod_ventas" CssClass="textbox" runat="server" MaxLength="128" />
                            </td>
                            <td class="tdD">
                                Tipoventas
                                <br />
                                <asp:TextBox ID="txtTipoventas" CssClass="textbox" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdI">
                                Valor
                                <asp:CompareValidator ID="cvVALOR" runat="server" ControlToValidate="txtVALOR" ErrorMessage="Solo se admiten n&uacute;meros"
                                    Operator="DataTypeCheck" SetFocusOnError="True" Type="Double" ValidationGroup="vgGuardar"
                                    Display="Dynamic" ForeColor="Red" /><br />
                                <asp:TextBox ID="txtValor" CssClass="textbox" runat="server" MaxLength="128" />
                            </td>
                            <td class="tdD">
                                Lunes
                                <asp:CompareValidator ID="cvLUNES" runat="server" ControlToValidate="txtLUNES" ErrorMessage="Solo se admiten n&uacute;meros"
                                    Operator="DataTypeCheck" SetFocusOnError="True" Type="Double" ValidationGroup="vgGuardar"
                                    Display="Dynamic" ForeColor="Red" /><br />
                                <asp:TextBox ID="txtLunes" CssClass="textbox" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdI">
                                Martes
                                <asp:CompareValidator ID="cvMARTES" runat="server" ControlToValidate="txtMARTES"
                                    ErrorMessage="Solo se admiten n&uacute;meros" Operator="DataTypeCheck" SetFocusOnError="True"
                                    Type="Double" ValidationGroup="vgGuardar" Display="Dynamic" ForeColor="Red" /><br />
                                <asp:TextBox ID="txtMartes" CssClass="textbox" runat="server" MaxLength="128" />
                            </td>
                            <td class="tdD">
                                Miercoles
                                <asp:CompareValidator ID="cvMIERCOLES" runat="server" ControlToValidate="txtMIERCOLES"
                                    ErrorMessage="Solo se admiten n&uacute;meros" Operator="DataTypeCheck" SetFocusOnError="True"
                                    Type="Double" ValidationGroup="vgGuardar" Display="Dynamic" ForeColor="Red" /><br />
                                <asp:TextBox ID="txtMiercoles" CssClass="textbox" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdI">
                                Jueves
                                <asp:CompareValidator ID="cvJUEVES" runat="server" ControlToValidate="txtJUEVES"
                                    ErrorMessage="Solo se admiten n&uacute;meros" Operator="DataTypeCheck" SetFocusOnError="True"
                                    Type="Double" ValidationGroup="vgGuardar" Display="Dynamic" ForeColor="Red" /><br />
                                <asp:TextBox ID="txtJueves" CssClass="textbox" runat="server" MaxLength="128" />
                            </td>
                            <td class="tdD">
                                Viernes
                                <asp:CompareValidator ID="cvVIERNES" runat="server" ControlToValidate="txtVIERNES"
                                    ErrorMessage="Solo se admiten n&uacute;meros" Operator="DataTypeCheck" SetFocusOnError="True"
                                    Type="Double" ValidationGroup="vgGuardar" Display="Dynamic" ForeColor="Red" /><br />
                                <asp:TextBox ID="txtViernes" CssClass="textbox" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdI">
                                Sabados
                                <asp:CompareValidator ID="cvSABADOS" runat="server" ControlToValidate="txtSABADOS"
                                    ErrorMessage="Solo se admiten n&uacute;meros" Operator="DataTypeCheck" SetFocusOnError="True"
                                    Type="Double" ValidationGroup="vgGuardar" Display="Dynamic" ForeColor="Red" /><br />
                                <asp:TextBox ID="txtSabados" CssClass="textbox" runat="server" MaxLength="128" />
                            </td>
                            <td class="tdD">
                                Domingo
                                <asp:CompareValidator ID="cvDOMINGO" runat="server" ControlToValidate="txtDOMINGO"
                                    ErrorMessage="Solo se admiten n&uacute;meros" Operator="DataTypeCheck" SetFocusOnError="True"
                                    Type="Double" ValidationGroup="vgGuardar" Display="Dynamic" ForeColor="Red" /><br />
                                <asp:TextBox ID="txtDomingo" CssClass="textbox" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdI">
                                Total
                                <asp:CompareValidator ID="cvTOTAL" runat="server" ControlToValidate="txtTOTAL" ErrorMessage="Solo se admiten n&uacute;meros"
                                    Operator="DataTypeCheck" SetFocusOnError="True" Type="Double" ValidationGroup="vgGuardar"
                                    Display="Dynamic" ForeColor="Red" /><br />
                                <asp:TextBox ID="txtTotal" CssClass="textbox" runat="server" MaxLength="128" />
                            </td>
                            <td class="tdD">
                                &nbsp;
                            </td>
                            <tr>
                                <td class="tdI">
                                    <asp:Label ID="lblTotalRegs" runat="server" Visible="False" />
                                </td>
                                <td class="tdD">
                                </td>
                            </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td class="tdI" style="text-align: center; font-weight: bold;">
                Tipo de Ventas
            </td>
            <td align="center">
                <b>Valor </b>
            </td>
            <td align="center">
                <asp:UpdatePanel ID="UpChkLunes" runat="server">
                    <ContentTemplate>
                        <b>
                            <asp:CheckBox ID="ChkLunes" runat="server" AutoPostBack="True" Checked="True" OnCheckedChanged="ChkLunes_CheckedChanged"
                                Text="Lunes" />
                        </b>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td align="center">
                <asp:UpdatePanel ID="UpChkMartes" runat="server">
                    <ContentTemplate>
                        <b>
                            <asp:CheckBox ID="ChkMartes" runat="server" AutoPostBack="True" Checked="True" OnCheckedChanged="ChkMartes_CheckedChanged"
                                Text="Martes" />
                        </b>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td align="center">
                <asp:UpdatePanel ID="UpChkMiercoles" runat="server">
                    <ContentTemplate>
                        <b>
                            <asp:CheckBox ID="ChkMiercoles" runat="server" AutoPostBack="True" Checked="True"
                                OnCheckedChanged="ChkMiercoles_CheckedChanged" Text="Miercoles" />
                        </b>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td align="center">
                <asp:UpdatePanel ID="UpChkJueves" runat="server">
                    <ContentTemplate>
                        <b>
                            <asp:CheckBox ID="ChkJueves" runat="server" AutoPostBack="True" Checked="True" OnCheckedChanged="ChkJueves_CheckedChanged"
                                Text="Jueves" />
                        </b>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td align="center">
                <asp:UpdatePanel ID="UpChkViernes" runat="server">
                    <ContentTemplate>
                        <b>
                            <asp:CheckBox ID="ChkViernes" runat="server" AutoPostBack="True" Checked="True" OnCheckedChanged="ChkViernes_CheckedChanged"
                                Text="Viernes" />
                        </b>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td align="center">
                <asp:UpdatePanel ID="UpChkSabado" runat="server">
                    <ContentTemplate>
                        <b>
                            <asp:CheckBox ID="ChkSabado" runat="server" AutoPostBack="True" Checked="True" OnCheckedChanged="ChkSabado_CheckedChanged"
                                Text="Sabado" />
                        </b>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td align="center">
                <asp:UpdatePanel ID="UpChkDomingo" runat="server">
                    <ContentTemplate>
                        <b>
                            <asp:CheckBox ID="ChkDomingo" runat="server" AutoPostBack="True" Checked="True" OnCheckedChanged="ChkDomingo_CheckedChanged"
                                Text="Domingo" />
                        </b>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td align="center" style="text-align: left; width: 105px;">
                Buenas
            </td>
            <td align="center">
                <uc1:decimales ID="txtValorB" runat="server" />
            </td>
            <td rowspan="3" align="center">
                <asp:UpdatePanel ID="UpRbLunes" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:RadioButtonList ID="rblLunes" runat="server" Height="60px" Width="40px" >
                            <asp:ListItem Value="B">&amp;nbsp<br /></asp:ListItem>
                            <asp:ListItem Value="R">&amp;nbsp<br /></asp:ListItem>
                            <asp:ListItem Value="M">&amp;nbsp</asp:ListItem>
                        </asp:RadioButtonList>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="chkLunes" EventName="CheckedChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
            <td rowspan="3" align="center">
                <asp:UpdatePanel ID="UpRbMartes" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:RadioButtonList ID="rblMartes" runat="server" Height="60px" Width="40px" 
                            CellSpacing="1">
                            <asp:ListItem Value="B">&amp;nbsp<br /></asp:ListItem>
                            <asp:ListItem Value="R">&amp;nbsp<br /></asp:ListItem>
                            <asp:ListItem Value="M">&amp;nbsp</asp:ListItem>
                        </asp:RadioButtonList>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="chkMartes" EventName="CheckedChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
            <td rowspan="3" align="center">
                <asp:UpdatePanel ID="UpRbMiercoles" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:RadioButtonList ID="rblMiercoles" runat="server" Height="60px" 
                            Width="40px">
                            <asp:ListItem Value="B">&amp;nbsp<br /></asp:ListItem>
                            <asp:ListItem Value="R">&amp;nbsp<br /></asp:ListItem>
                            <asp:ListItem Value="M">&amp;nbsp</asp:ListItem>
                        </asp:RadioButtonList>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="chkMiercoles" EventName="CheckedChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
            <td rowspan="3" align="center">
                <asp:UpdatePanel ID="UpRbJueves" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:RadioButtonList ID="rblJueves" runat="server" Height="60px" Width="40px">
                            <asp:ListItem Value="B">&amp;nbsp<br /></asp:ListItem>
                            <asp:ListItem Value="R">&amp;nbsp<br /></asp:ListItem>
                            <asp:ListItem Value="M">&amp;nbsp</asp:ListItem>
                        </asp:RadioButtonList>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="chkJueves" EventName="CheckedChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
            <td rowspan="3" align="center">
                <asp:UpdatePanel ID="UpRbViernes" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:RadioButtonList ID="rblViernes" runat="server" Height="60px" Width="40px">
                            <asp:ListItem Value="B">&amp;nbsp<br /></asp:ListItem>
                            <asp:ListItem Value="R">&amp;nbsp<br /></asp:ListItem>
                            <asp:ListItem Value="M">&amp;nbsp</asp:ListItem>
                        </asp:RadioButtonList>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="chkViernes" EventName="CheckedChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
            <td rowspan="3" align="center">
                <asp:UpdatePanel ID="UpRbSabado" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:RadioButtonList ID="rblSabado" runat="server" Height="60px" Width="40px">
                            <asp:ListItem Value="B">&amp;nbsp<br /></asp:ListItem>
                            <asp:ListItem Value="R">&amp;nbsp<br /></asp:ListItem>
                            <asp:ListItem Value="M">&amp;nbsp</asp:ListItem>
                        </asp:RadioButtonList>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="chkSabado" EventName="CheckedChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
            <td rowspan="3" align="center">
                <asp:UpdatePanel ID="UpRbDomingo" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:RadioButtonList ID="rblDomingo" runat="server" Height="60px" Width="40px">
                            <asp:ListItem Value="B">&amp;nbsp<br /></asp:ListItem>
                            <asp:ListItem Value="R">&amp;nbsp<br /></asp:ListItem>
                            <asp:ListItem Value="M">&amp;nbsp</asp:ListItem>
                        </asp:RadioButtonList>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="chkDomingo" EventName="CheckedChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td align="center" style="text-align: left; width: 105px;">
                Regulares 
            </td>
            <td align="center">
                <uc1:decimales ID="txtValorR" runat="server" />
            </td>
        </tr>
        <tr>
            <td align="center" style="text-align: left; width: 105px;">
                Malas 
            </td>
            <td align="center">
                <uc1:decimales ID="txtValorM" runat="server" />
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td>
                <b>Total&nbsp;Semanal<asp:TextBox ID="txtPorContado" runat="server" MaxLength="3"
                    Style="font-weight: bold" Width="50px" Visible="False">100</asp:TextBox>
                    <br />
                </b>
                <uc1:decimales ID="txtTotalSemanal" runat="server" Enabled="False" />
            </td>
            <td>
                <b>Ventas Al Mes<br />
                </b>
                <uc1:decimales ID="txtVentasMes" runat="server" Enabled="False" />
            </td>
            <td>
                <b>Ventas de Contado<br />
                    <uc1:decimales ID="txtVentasContado" runat="server" Enabled="False" />
                </b>
                <asp:TextBox ID="txtVentasCredito" runat="server" ReadOnly="True" Style="font-weight: bold"
                    Visible="False"></asp:TextBox>
                <asp:MaskedEditExtender ID="txtVentasCredito_MaskedEditExtender" runat="server" TargetControlID="txtVentasCredito"
                    Mask="99,999,999,999" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus"
                    OnInvalidCssClass="MaskedEditError" MaskType="Number" InputDirection="RightToLeft"
                    AcceptNegative="Left" DisplayMoney="Left" ErrorTooltipEnabled="True" />
            </td>
        </tr>
    </table>
    <%--  <script type="text/javascript" language="javascript">
        function SetFocus() {
            document.getElementById('ctl00_cphMain_txtTIPOVENTAS').focus();
        }
        window.onload = SetFocus;
    </script>--%>
</asp:Content>
