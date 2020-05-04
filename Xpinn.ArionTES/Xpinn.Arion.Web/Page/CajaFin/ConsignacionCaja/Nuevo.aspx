<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <asp:MultiView ID="MultiView1" runat="server">
        <asp:View ID="View1" runat="server">
            <asp:ImageButton runat="server" ID="btnCancelar" ImageUrl="~/Images/btnCancelar.jpg" OnClick="btnCancelar_Click" ImageAlign="Right" />
            <asp:ImageButton runat="server" ID="btnGuardar" ImageUrl="~/Images/btnGuardar.jpg"
                ValidationGroup="vgGuardar" OnClick="btnGuardar_Click" ImageAlign="Right" />
            <br />
            <table cellpadding="5" cellspacing="0" style="width: 100%">
                <tr>
                    <td style="width: 20%; text-align: center;">Fecha de Consignación<br />
                        <asp:TextBox ID="txtFechaConsignacion" Enabled="false" runat="server" CssClass="textbox"
                            MaxLength="10" Width="85%" Style="text-align: center"></asp:TextBox>
                    </td>
                    <td style="width: 20%; text-align: center;">Oficina<br />
                        <asp:TextBox ID="txtOficina" runat="server" Enabled="False" CssClass="textbox"
                            Width="90%"></asp:TextBox>
                    </td>
                    <td style="text-align: center; width: 20%;">Caja<br />
                        <asp:TextBox ID="txtCaja" runat="server" Enabled="False" CssClass="textbox"
                            Width="90%"></asp:TextBox>
                    </td>
                    <td style="width: 20%; text-align: center;">Cajero<br />
                        <asp:TextBox ID="txtCajero" runat="server" Enabled="False" CssClass="textbox"
                            Width="90%"></asp:TextBox>
                    </td>
                    <td style="width: 20%">&nbsp;</td>
                </tr>
            </table>
            <hr />
            <table cellpadding="5" cellspacing="0" style="width: 100%; margin-right: 0px;">
                <tr>
                    <td colspan="4" style="text-align: center;" valign="bottom">
                        <strong style="color: #000000;">Saldo en Caja</strong></td>
                </tr>
                <tr>
                    <td style="width: 20%">&nbsp;</td>
                    <td style="text-align: center; width: 25%">Valor en Efectivo<br />
                        <asp:TextBox ID="txtValorEfectivo" runat="server" Enabled="false" CssClass="textbox" Width="150px" Style="text-align: right"></asp:TextBox>
                        <asp:MaskedEditExtender ID="MaskedEditExtender4" runat="server" TargetControlID="txtValorEfectivo" Mask="999,999,999,999,999" MessageValidatorTip="true"
                            OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="Number" InputDirection="RightToLeft"
                            AcceptNegative="Left" DisplayMoney="Left" ErrorTooltipEnabled="True">
                        </asp:MaskedEditExtender>
                    </td>
                    <td style="text-align: center; width: 25%">Valor en Cheque<br />
                        <asp:TextBox ID="txtValorCheque" runat="server"
                            Enabled="false" CssClass="textbox" Width="150px"
                            OnTextChanged="txtValorCheque_TextChanged" Style="text-align: right"></asp:TextBox>
                        <asp:MaskedEditExtender ID="MaskedEditExtender5" runat="server" TargetControlID="txtValorCheque" Mask="999,999,999,999,999" MessageValidatorTip="true"
                            OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="Number" InputDirection="RightToLeft"
                            AcceptNegative="Left" DisplayMoney="Left" ErrorTooltipEnabled="True">
                        </asp:MaskedEditExtender>
                    </td>
                    <td style="width: 30%;">&nbsp;</td>
                </tr>
            </table>
            <table cellpadding="5" cellspacing="0" style="width: 100%; margin-right: 0px;">
                <tr>
                    <td style="background-color: #3599F7; text-align: center;" colspan="5">
                        <strong style="color: #FFFFFF">Datos de la Consignación</strong>
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%; text-align: center;">Banco<br />
                        <asp:DropDownList ID="ddlBancos" runat="server" AutoPostBack="True"
                            CssClass="textbox"
                            OnSelectedIndexChanged="ddlBancos_SelectedIndexChanged" Style="width: 85%; min-width: 100px">
                        </asp:DropDownList>
                    </td>
                    <td style="width: 20%; text-align: center;">Cuenta<br />
                        <asp:DropDownList ID="ddlCuenta" runat="server"
                            CssClass="textbox" Style="width: 80%; min-width: 100px">
                        </asp:DropDownList>
                    </td>
                    <td style="width: 20%; text-align: center;">Moneda<br />
                        <asp:DropDownList ID="ddlMonedas" CssClass="textbox" runat="server"
                            OnSelectedIndexChanged="ddlMonedas_SelectedIndexChanged"
                            AutoPostBack="true" Style="width: 85%; min-width: 100px">
                        </asp:DropDownList>
                    </td>
                    <td style="width: 20%; text-align: center;">Valor a Consignar en Cheque<br />
                        <uc1:decimales ID="txtValorConsigCheque" runat="server" CssClass="textbox" Enabled ="false" AutoPostBack_="false"
                            MaxLength="9" style="width: 85%; min-width: 80px; text-align: right" />
                    </td>
                    <td style="width: 20%; text-align: center;">Valor a Consignar en Efectivo<br />
                        <uc1:decimales ID="txtValorConsigEfecty" runat="server" CssClass="textbox" AutoPostBack_="false"
                            MaxLength="9" style="width: 85%; min-width: 80px; text-align: right"></uc1:decimales>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center;" colspan="2">Observaciones<br />
                        <asp:TextBox ID="txtObservacion" runat="server"
                            TextMode="MultiLine" Height="30px"
                            Width="80%" Style="margin-right: 0px; margin-left: 0px;"></asp:TextBox>
                    </td>
                    <td style="text-align: center;">
                        <asp:Button ID="btnValoEfe" runat="server" Text="Calcular Total a Consignar" CssClass="btn8"
                            OnClick="btnValoEfe_Click" Width="165px" Height="30px" />
                    </td>
                    <td style="text-align: center;">Total Consignación<br />
                        <asp:TextBox ID="txtValorTotalAConsig" runat="server" Enabled="false" Width="80%" CssClass="textbox" Style="text-align: right"></asp:TextBox>
                        <asp:MaskedEditExtender ID="MaskedEditExtender2" runat="server" TargetControlID="txtValorTotalAConsig" Mask="999,999,999,999,999" MessageValidatorTip="true"
                            OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="Number" InputDirection="RightToLeft"
                            AcceptNegative="Left" DisplayMoney="Left" ErrorTooltipEnabled="True">
                        </asp:MaskedEditExtender>
                    </td>
                    <td>&nbsp;</td>
                </tr>
            </table>
            <table style="width: 100%;">
                <tr>
                    <td style="text-align: center;" class="align-rt">
                        <strong>Cheques a Consignar</strong>
                    </td>
                </tr>
                <tr>
                    <td style="width: 900px" align="center">
                        <asp:GridView ID="gvConsignacion" runat="server" BackColor="White"
                            BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4"
                            ForeColor="Black" AutoGenerateColumns="false" GridLines="Vertical" Style="text-align: center">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:BoundField DataField="cod_movimiento" HeaderStyle-CssClass="gridColNo" ItemStyle-CssClass="gridColNo" />
                                <asp:BoundField DataField="fec_ope" HeaderText="Fecha Operación" DataFormatString="{0:d}" />
                                <asp:BoundField DataField="num_documento" HeaderText="Núm. Cheque" />
                                <asp:BoundField DataField="cod_banco" HeaderText="Banco" />
                                <asp:BoundField DataField="nom_banco" HeaderText="Nombre Banco" />
                                <asp:BoundField DataField="valor" HeaderText="Valor" DataFormatString="{0:N0}">
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="nom_moneda" HeaderText="Moneda" />
                                <asp:TemplateField HeaderText="Recibe">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkRecibe" runat="server" OnCheckedChanged="chkRecibe_CheckedChanged" AutoPostBack="true" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle BackColor="#CCCC99" />
                            <HeaderStyle CssClass="gridHeader" />
                            <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                            <RowStyle BackColor="#F7F7DE" />
                            <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                            <SortedAscendingCellStyle BackColor="#FBFBF2" />
                            <SortedAscendingHeaderStyle BackColor="#848384" />
                            <SortedDescendingCellStyle BackColor="#EAEAD3" />
                            <SortedDescendingHeaderStyle BackColor="#575357" />
                        </asp:GridView>
                        <br />
                        <asp:Button ID="btnInforme0" runat="server" CssClass="btn8"
                            OnClick="btnInforme0_Click" OnClientClick="btnInforme0_Click"
                            Text="Visualizar informe" Visible="false" />
                    </td>
                </tr>
            </table>
        </asp:View>
        <asp:View ID="View2" runat="server">
            <asp:ImageButton ID="btnCancelar0" runat="server" ImageAlign="Right"
                ImageUrl="~/Images/btnCancelar.jpg" OnClick="btnCancelarreporte_Click" />
            <br />
            <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="783px"
                Font-Names="Verdana" Font-Size="8pt" InteractiveDeviceInfos="(Colección)"
                WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt">
                <LocalReport
                    ReportPath="Page\CajaFin\ConsignacionCaja\ReportConsignacion.rdlc">
                </LocalReport>
            </rsweb:ReportViewer>
        </asp:View>
    </asp:MultiView>
</asp:Content>

