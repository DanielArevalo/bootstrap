<%@ Page Language="C#" MasterPageFile="~/General/Master/solicitudSub.master" AutoEventWireup="true"
    CodeFile="Lista.aspx.cs" Inherits="Lista" Title=".: Xpinn - EstacionalidadMensual :." %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc1" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td>
                <asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>
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
                                Enero
                                <asp:CompareValidator ID="cvENERO" runat="server" ControlToValidate="txtENERO" ErrorMessage="Solo se admiten n&uacute;meros"
                                    Operator="DataTypeCheck" SetFocusOnError="True" Type="Double" ValidationGroup="vgGuardar"
                                    Display="Dynamic" ForeColor="Red" /><br />
                                <asp:TextBox ID="txtEnero" CssClass="textbox" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdI">
                                Febrero
                                <asp:CompareValidator ID="cvFEBRERO" runat="server" ControlToValidate="txtFEBRERO"
                                    ErrorMessage="Solo se admiten n&uacute;meros" Operator="DataTypeCheck" SetFocusOnError="True"
                                    Type="Double" ValidationGroup="vgGuardar" Display="Dynamic" ForeColor="Red" /><br />
                                <asp:TextBox ID="txtFebrero" CssClass="textbox" runat="server" MaxLength="128" />
                            </td>
                            <td class="tdD">
                                Marzo
                                <asp:CompareValidator ID="cvMARZO" runat="server" ControlToValidate="txtMARZO" ErrorMessage="Solo se admiten n&uacute;meros"
                                    Operator="DataTypeCheck" SetFocusOnError="True" Type="Double" ValidationGroup="vgGuardar"
                                    Display="Dynamic" ForeColor="Red" /><br />
                                <asp:TextBox ID="txtMarzo" CssClass="textbox" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdI">
                                Abril
                                <asp:CompareValidator ID="cvABRIL" runat="server" ControlToValidate="txtABRIL" ErrorMessage="Solo se admiten n&uacute;meros"
                                    Operator="DataTypeCheck" SetFocusOnError="True" Type="Double" ValidationGroup="vgGuardar"
                                    Display="Dynamic" ForeColor="Red" /><br />
                                <asp:TextBox ID="txtAbril" CssClass="textbox" runat="server" MaxLength="128" />
                            </td>
                            <td class="tdD">
                                Mayo
                                <asp:CompareValidator ID="cvMAYO" runat="server" ControlToValidate="txtMAYO" ErrorMessage="Solo se admiten n&uacute;meros"
                                    Operator="DataTypeCheck" SetFocusOnError="True" Type="Double" ValidationGroup="vgGuardar"
                                    Display="Dynamic" ForeColor="Red" /><br />
                                <asp:TextBox ID="txtMayo" CssClass="textbox" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdI">
                                Junio
                                <asp:CompareValidator ID="cvJUNIO" runat="server" ControlToValidate="txtJUNIO" ErrorMessage="Solo se admiten n&uacute;meros"
                                    Operator="DataTypeCheck" SetFocusOnError="True" Type="Double" ValidationGroup="vgGuardar"
                                    Display="Dynamic" ForeColor="Red" /><br />
                                <asp:TextBox ID="txtJunio" CssClass="textbox" runat="server" MaxLength="128" />
                            </td>
                            <td class="tdD">
                                Julio
                                <asp:CompareValidator ID="cvJULIO" runat="server" ControlToValidate="txtJULIO" ErrorMessage="Solo se admiten n&uacute;meros"
                                    Operator="DataTypeCheck" SetFocusOnError="True" Type="Double" ValidationGroup="vgGuardar"
                                    Display="Dynamic" ForeColor="Red" /><br />
                                <asp:TextBox ID="txtJulio" CssClass="textbox" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdI">
                                Agosto
                                <asp:CompareValidator ID="cvAGOSTO" runat="server" ControlToValidate="txtAGOSTO"
                                    ErrorMessage="Solo se admiten n&uacute;meros" Operator="DataTypeCheck" SetFocusOnError="True"
                                    Type="Double" ValidationGroup="vgGuardar" Display="Dynamic" ForeColor="Red" /><br />
                                <asp:TextBox ID="txtAgosto" CssClass="textbox" runat="server" MaxLength="128" />
                            </td>
                            <td class="tdD">
                                Septiembre
                                <asp:CompareValidator ID="cvSEPTIEMBRE" runat="server" ControlToValidate="txtSEPTIEMBRE"
                                    ErrorMessage="Solo se admiten n&uacute;meros" Operator="DataTypeCheck" SetFocusOnError="True"
                                    Type="Double" ValidationGroup="vgGuardar" Display="Dynamic" ForeColor="Red" /><br />
                                <asp:TextBox ID="txtSeptiembre" CssClass="textbox" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdI">
                                Octubre
                                <asp:CompareValidator ID="cvOCTUBRE" runat="server" ControlToValidate="txtOCTUBRE"
                                    ErrorMessage="Solo se admiten n&uacute;meros" Operator="DataTypeCheck" SetFocusOnError="True"
                                    Type="Double" ValidationGroup="vgGuardar" Display="Dynamic" ForeColor="Red" /><br />
                                <asp:TextBox ID="txtOctubre" CssClass="textbox" runat="server" MaxLength="128" />
                            </td>
                            <td class="tdD">
                                Noviembre
                                <asp:CompareValidator ID="cvNOVIEMBRE" runat="server" ControlToValidate="txtNOVIEMBRE"
                                    ErrorMessage="Solo se admiten n&uacute;meros" Operator="DataTypeCheck" SetFocusOnError="True"
                                    Type="Double" ValidationGroup="vgGuardar" Display="Dynamic" ForeColor="Red" /><br />
                                <asp:TextBox ID="txtNoviembre" CssClass="textbox" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdI">
                                Diciembre
                                <asp:CompareValidator ID="cvDICIEMBRE" runat="server" ControlToValidate="txtDICIEMBRE"
                                    ErrorMessage="Solo se admiten n&uacute;meros" Operator="DataTypeCheck" SetFocusOnError="True"
                                    Type="Double" ValidationGroup="vgGuardar" Display="Dynamic" ForeColor="Red" /><br />
                                <asp:TextBox ID="txtDiciembre" CssClass="textbox" runat="server" MaxLength="128" />
                            </td>
                            <td class="tdD">
                                Total
                                <asp:CompareValidator ID="cvTOTAL" runat="server" ControlToValidate="txtTOTAL" ErrorMessage="Solo se admiten n&uacute;meros"
                                    Operator="DataTypeCheck" SetFocusOnError="True" Type="Double" ValidationGroup="vgGuardar"
                                    Display="Dynamic" ForeColor="Red" /><br />
                                <asp:TextBox ID="txtTotal" CssClass="textbox" runat="server" />
                            </td>
                        </tr>
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
    <table style="text-align: center">
        <tr>
            <td class="tdI" style="text-align: center; font-weight: bold;">
                Tipo de Ventas
            </td>
            <td class="tdI" style="text-align: center; font-weight: bold;">
                Valor</td>
            <td class="tdI" style="text-align: center; font-weight: bold;">
                <asp:UpdatePanel ID="UpChkEne" runat="server">
                    <ContentTemplate>
                        <b>
                            <asp:CheckBox ID="ChkEne" runat="server" AutoPostBack="True" Checked="True" OnCheckedChanged="ChkEne_CheckedChanged"
                                Text="Ene" Width="20px" />
                        </b>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td class="tdI" style="text-align: center; font-weight: bold;">
                <asp:UpdatePanel ID="UpChkFeb" runat="server">
                    <ContentTemplate>
                        <b>
                            <asp:CheckBox ID="ChkFeb" runat="server" AutoPostBack="True" Checked="True" OnCheckedChanged="ChkFeb_CheckedChanged"
                                Text="Feb" Width="20px" />
                        </b>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td class="tdI" style="text-align: center; font-weight: bold;">
                <asp:UpdatePanel ID="UpChkMar" runat="server">
                    <ContentTemplate>
                        <b>
                            <asp:CheckBox ID="ChkMar" runat="server" AutoPostBack="True" Checked="True" OnCheckedChanged="ChkMar_CheckedChanged"
                                Text="Mar" Width="20px" />
                        </b>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td class="tdI" style="text-align: center; font-weight: bold;">
                <asp:UpdatePanel ID="UpChkAbr" runat="server">
                    <ContentTemplate>
                        <b>
                            <asp:CheckBox ID="ChkAbr" runat="server" AutoPostBack="True" Checked="True" OnCheckedChanged="ChkAbr_CheckedChanged"
                                Text="Abr" Width="20px" />
                        </b>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td class="tdI" style="text-align: center; font-weight: bold;">
                <asp:UpdatePanel ID="UpChkMay" runat="server">
                    <ContentTemplate>
                        <b>
                            <asp:CheckBox ID="ChkMay" runat="server" AutoPostBack="True" Checked="True" OnCheckedChanged="ChkMay_CheckedChanged"
                                Text="May" Width="20px" />
                        </b>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td class="tdI" style="text-align: center; font-weight: bold;">
                <asp:UpdatePanel ID="UpChkJun" runat="server">
                    <ContentTemplate>
                        <b>
                            <asp:CheckBox ID="ChkJun" runat="server" AutoPostBack="True" Checked="True" OnCheckedChanged="ChkJun_CheckedChanged"
                                Text="Jun" Width="20px" />
                        </b>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td class="tdI" style="text-align: center; font-weight: bold;">
                <asp:UpdatePanel ID="UpChkJul" runat="server">
                    <ContentTemplate>
                        <b>
                            <asp:CheckBox ID="ChkJul" runat="server" AutoPostBack="True" Checked="True" OnCheckedChanged="ChkJul_CheckedChanged"
                                Text="Jul" Width="20px" />
                        </b>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td class="tdI" style="text-align: center; font-weight: bold;">
                <asp:UpdatePanel ID="UpChkAgo" runat="server">
                    <ContentTemplate>
                        <b>
                            <asp:CheckBox ID="ChkAgo" runat="server" AutoPostBack="True" Checked="True" OnCheckedChanged="ChkAgo_CheckedChanged"
                                Text="Ago" Width="20px" />
                        </b>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td class="tdI" style="text-align: center; font-weight: bold;">
                <asp:UpdatePanel ID="UpChkSep" runat="server">
                    <ContentTemplate>
                        <b>
                            <asp:CheckBox ID="ChkSep" runat="server" AutoPostBack="True" Checked="True" OnCheckedChanged="ChkSep_CheckedChanged"
                                Text="Sep" Width="20px" />
                        </b>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td class="tdI" style="text-align: center; font-weight: bold;">
                <asp:UpdatePanel ID="UpChkOct" runat="server">
                    <ContentTemplate>
                        <b>
                            <asp:CheckBox ID="ChkOct" runat="server" AutoPostBack="True" Checked="True" OnCheckedChanged="ChkOct_CheckedChanged"
                                Text="Oct" Width="20px" />
                        </b>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td class="tdI" style="text-align: center; font-weight: bold;">
                <asp:UpdatePanel ID="UpChkNov" runat="server">
                    <ContentTemplate>
                        <b>
                            <asp:CheckBox ID="ChkNov" runat="server" AutoPostBack="True" Checked="True" OnCheckedChanged="ChkNov_CheckedChanged"
                                Text="Nov" Width="20px" />
                        </b>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td class="tdI" style="text-align: center; font-weight: bold;">
                <asp:UpdatePanel ID="UpChkDic" runat="server">
                    <ContentTemplate>
                        <b>
                            <asp:CheckBox ID="ChkDic" runat="server" AutoPostBack="True" Checked="True" OnCheckedChanged="ChkDic_CheckedChanged"
                                Text="Dic" Width="20px" />
                        </b>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td class="tdI" style="text-align: left">
                Buenas
            </td>
            <td class="tdI" align="center">
                <uc1:decimales runat="server" ID="txtValorB" />
            </td>
            <td class="tdI" rowspan="3" align="center">
                <asp:UpdatePanel ID="UpRbEne" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:RadioButtonList ID="rblEne" runat="server" Height="50px" Width="40px">
                            <asp:ListItem Value="B">&amp;nbsp</asp:ListItem>
                            <asp:ListItem Value="R">&amp;nbsp</asp:ListItem>
                            <asp:ListItem Value="M">&amp;nbsp</asp:ListItem>
                        </asp:RadioButtonList>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="chkEne" EventName="CheckedChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
            <td class="tdI" rowspan="3" align="center">
                <asp:UpdatePanel ID="UpRbFeb" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:RadioButtonList ID="rblFeb" runat="server" Height="50px" Width="40px">
                            <asp:ListItem Value="B">&amp;nbsp</asp:ListItem>
                            <asp:ListItem Value="R">&amp;nbsp</asp:ListItem>
                            <asp:ListItem Value="M">&amp;nbsp</asp:ListItem>
                        </asp:RadioButtonList>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="chkFeb" EventName="CheckedChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
            <td class="tdI" rowspan="3" align="center">
                <asp:UpdatePanel ID="UpRbMar" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:RadioButtonList ID="rblMar" runat="server" Height="50px" Width="40px">
                            <asp:ListItem Value="B">&amp;nbsp</asp:ListItem>
                            <asp:ListItem Value="R">&amp;nbsp</asp:ListItem>
                            <asp:ListItem Value="M">&amp;nbsp</asp:ListItem>
                        </asp:RadioButtonList>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="chkMar" EventName="CheckedChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
            <td class="tdI" rowspan="3" align="center">
                <asp:UpdatePanel ID="UpRbAbr" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:RadioButtonList ID="rblAbr" runat="server" Height="50px" Width="40px">
                            <asp:ListItem Value="B">&amp;nbsp</asp:ListItem>
                            <asp:ListItem Value="R">&amp;nbsp</asp:ListItem>
                            <asp:ListItem Value="M">&amp;nbsp</asp:ListItem>
                        </asp:RadioButtonList>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="chkAbr" EventName="CheckedChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
            <td class="tdI" rowspan="3" align="center">
                <asp:UpdatePanel ID="UpRbMay" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:RadioButtonList ID="rblMay" runat="server" Height="50px" Width="40px">
                            <asp:ListItem Value="B">&amp;nbsp</asp:ListItem>
                            <asp:ListItem Value="R">&amp;nbsp</asp:ListItem>
                            <asp:ListItem Value="M">&amp;nbsp</asp:ListItem>
                        </asp:RadioButtonList>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="chkMay" EventName="CheckedChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
            <td class="tdI" rowspan="3" align="center">
                <asp:UpdatePanel ID="UpRbJun" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:RadioButtonList ID="rblJun" runat="server" Height="50px" Width="40px">
                            <asp:ListItem Value="B">&amp;nbsp</asp:ListItem>
                            <asp:ListItem Value="R">&amp;nbsp</asp:ListItem>
                            <asp:ListItem Value="M">&amp;nbsp</asp:ListItem>
                        </asp:RadioButtonList>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="chkJun" EventName="CheckedChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
            <td class="tdI" rowspan="3" align="center">
                <asp:UpdatePanel ID="UpRbJul" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:RadioButtonList ID="rblJul" runat="server" Height="50px" Width="40px">
                            <asp:ListItem Value="B">&amp;nbsp</asp:ListItem>
                            <asp:ListItem Value="R">&amp;nbsp</asp:ListItem>
                            <asp:ListItem Value="M">&amp;nbsp</asp:ListItem>
                        </asp:RadioButtonList>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="chkJul" EventName="CheckedChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
            <td class="tdI" rowspan="3" align="center">
                <asp:UpdatePanel ID="UpRbAgo" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:RadioButtonList ID="rblAgo" runat="server" Height="50px" Width="40px">
                            <asp:ListItem Value="B">&amp;nbsp</asp:ListItem>
                            <asp:ListItem Value="R">&amp;nbsp</asp:ListItem>
                            <asp:ListItem Value="M">&amp;nbsp</asp:ListItem>
                        </asp:RadioButtonList>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="chkAgo" EventName="CheckedChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
            <td class="tdI" rowspan="3" align="center">
                <asp:UpdatePanel ID="UpRbSep" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:RadioButtonList ID="rblSep" runat="server" Height="50px" Width="40px">
                            <asp:ListItem Value="B">&amp;nbsp</asp:ListItem>
                            <asp:ListItem Value="R">&amp;nbsp</asp:ListItem>
                            <asp:ListItem Value="M">&amp;nbsp</asp:ListItem>
                        </asp:RadioButtonList>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="chkSep" EventName="CheckedChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
            <td class="tdI" rowspan="3" align="center">
                <asp:UpdatePanel ID="UpRbOct" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:RadioButtonList ID="rblOct" runat="server" Height="50px" Width="40px">
                            <asp:ListItem Value="B">&amp;nbsp</asp:ListItem>
                            <asp:ListItem Value="R">&amp;nbsp</asp:ListItem>
                            <asp:ListItem Value="M">&amp;nbsp</asp:ListItem>
                        </asp:RadioButtonList>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="chkOct" EventName="CheckedChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
            <td class="tdI" rowspan="3" align="center">
                <asp:UpdatePanel ID="UpRbNov" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:RadioButtonList ID="rblNov" runat="server" Height="50px" Width="40px">
                            <asp:ListItem Value="B">&amp;nbsp</asp:ListItem>
                            <asp:ListItem Value="R">&amp;nbsp</asp:ListItem>
                            <asp:ListItem Value="M">&amp;nbsp</asp:ListItem>
                        </asp:RadioButtonList>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="chkNov" EventName="CheckedChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
            <td class="tdI" rowspan="3" align="center">
                <asp:UpdatePanel ID="UpRbDic" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:RadioButtonList ID="rblDic" runat="server" Height="50px" Width="40px">
                            <asp:ListItem Value="B">&amp;nbsp</asp:ListItem>
                            <asp:ListItem Value="R">&amp;nbsp</asp:ListItem>
                            <asp:ListItem Value="M">&amp;nbsp</asp:ListItem>
                        </asp:RadioButtonList>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="chkDic" EventName="CheckedChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td class="tdI" style="text-align: left">
                Regulares
             </td>
            <td class="tdI" align="center">
                <uc1:decimales runat="server" ID="txtValorR" />
            </td>
        </tr>
        <tr>
            <td class="tdI" style="text-align: left">
                Malas
            </td>
            <td class="tdI" align="center">
                <uc1:decimales runat="server" ID="txtValorM" />
            </td>
        </tr>
        <tr>
            <td class="tdI" style="text-align: left; font-weight: bold;" colspan="14">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="tdI" style="text-align: left;" colspan="14">
                <strong>Ventas Promedio Por Mes<br />
                </strong>
                <uc1:decimales ID="txtPromedioMes" runat="server" />
                <br />
            </td>
        </tr>
        <tr>
            <td class="tdI" style="text-align: left;" colspan="14">
                <asp:Label ID="lblVentasMes" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="tdI" style="text-align: left;" colspan="14">
                &nbsp;
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
