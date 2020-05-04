<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Detalle.aspx.cs" Inherits="Detalle" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/ctlPlanCuentas.ascx" TagName="ListadoPlanCtas" TagPrefix="uc1" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table cellpadding="5" cellspacing="0" style="width: 104%">
        <tr>
            <td style="text-align: left" colspan="2">
                <strong>Datos de la Caja</strong></td>
            <td rowspan="9" valign="top" style="text-align: left">
                <strong>Operaciones Permitidas</strong><br />
                <div id="divOperaciones" runat="server" style="height: 400px; overflow: scroll">
                <asp:GridView ID="gvOperaciones" runat="server" BackColor="White" BorderColor="#DEDFDE"
                    BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black"
                    GridLines="Vertical" AutoGenerateColumns="False">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:BoundField DataField="tipo_tran" HeaderStyle-CssClass="gridColNo" ItemStyle-CssClass="gridColNo" />
                        <asp:BoundField DataField="nom_tipo_tran" HeaderText="Operación" />
                        <asp:TemplateField HeaderText="Permitir">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkPermiso" Enabled="false" runat="server" />
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
                </div>
            </td>
        </tr>
        <tr>
            <td style="text-align: left; width: 139px;">C&oacute;digo</td>
            <td style="text-align: left; width: 238px;">
                <asp:Label ID="lblCodigo" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="text-align: left; width: 139px;">Fecha de Creación 
            </td>
            <td style="text-align: left; width: 238px;">
                <asp:TextBox ID="txtFechaCreacion" runat="server" CssClass="textbox"
                    Width="144px" Enabled="false" MaxLength="10"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="tdI" style="text-align: left; width: 139px;">Oficina</td>
            <td class="tdI" style="text-align: left; width: 238px;">
                <asp:Label ID="lblCodOficina" runat="server"></asp:Label>
                &nbsp;<asp:Label ID="lblOficina" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="tdI" style="text-align: left; width: 139px;">Nombre Caja</td>
            <td class="tdI" style="text-align: left; width: 238px;">
                <asp:TextBox ID="txtCaja" runat="server" CssClass="textbox" Enabled="false" Width="210px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="text-align: left; width: 139px;">Tipo de Caja &#160
            </td>
            <td style="text-align: left; width: 238px;">
                <asp:RadioButtonList
                    ID="radTipoCaja" Enabled="false" runat="server" RepeatDirection="Horizontal"
                    RepeatLayout="Flow">
                    <asp:ListItem Value="1">Principal</asp:ListItem>
                    <asp:ListItem Value="0">Auxiliar</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td style="text-align: left; width: 139px;">Estado Actual</td>
            <td style="text-align: left; width: 238px;">
                <asp:RadioButtonList
                    ID="Radestado" runat="server" RepeatDirection="Horizontal"
                    RepeatLayout="Flow">
                    <asp:ListItem Value="1">Activa</asp:ListItem>
                    <asp:ListItem Value="0">Inactiva</asp:ListItem>
                    <asp:ListItem Value="2">Cerrada</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="width: 50%">
                <asp:UpdatePanel runat="server">
                    <ContentTemplate>
                        <table>
                            <tr>
                                <td style="width: 120px; text-align: left">Cod Cuenta<br />
                                    <cc1:textboxgrid id="txtCodCuenta" runat="server" autopostback="True" backcolor="#F4F5FF"
                                        cssclass="textbox" style="text-align: left"
                                        width="100"></cc1:textboxgrid>
                                    <uc1:listadoplanctas id="ctlListadoPlan" runat="server" />
                                </td>
                                <td style="width: 35px; text-align: center">
                                    <br />
                                    <cc1:buttongrid id="btnListadoPlan" runat="server" cssclass="btnListado"
                                        width="95%" text="..." />
                                </td>
                                <td style="width: 230px; text-align: left">Nombre de la Cuenta<br />
                                    <cc1:textboxgrid id="txtNomCuenta" runat="server" cssclass="textbox" enabled="false"
                                        width="190px" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td style="text-align: left; width: 130px;">Cod. Datafono
            </td>
            <td style="text-align: left">
                <asp:TextBox ID="txtDatafono" runat="server" MaxLength="20" CssClass="textbox" Width="210px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="text-align: left" colspan="2">
                <strong>Topes Permitidos para la Caja</strong></td>
        </tr>
        <tr>
            <td style="text-align: left" colspan="2">
                <asp:GridView ID="gvTopes" runat="server" BackColor="White"
                    BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4"
                    ForeColor="Black" GridLines="Vertical"
                    AutoGenerateColumns="False" Width="485px">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:BoundField DataField="tipotope" HeaderStyle-CssClass="gridColNo" ItemStyle-CssClass="gridColNo" />
                        <asp:BoundField DataField="descTope" HeaderText="Tipo" />
                        <asp:BoundField DataField="cod_moneda" HeaderStyle-CssClass="gridColNo" ItemStyle-CssClass="gridColNo" />
                        <asp:BoundField DataField="descMoneda" HeaderText="Moneda" />
                        <asp:TemplateField HeaderText="Mínimo">
                            <ItemTemplate>
                                <asp:TextBox ID="txtMinimo" Enabled="false" Width="100" runat="server"></asp:TextBox>
                                <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtMinimo" Mask="999,999,999,999,999" MessageValidatorTip="true"
                                    OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="Number" InputDirection="RightToLeft"
                                    AcceptNegative="Left" DisplayMoney="Left" ErrorTooltipEnabled="True">
                                </asp:MaskedEditExtender>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Máximo">
                            <ItemTemplate>
                                <asp:TextBox ID="txtMaximo" Enabled="false" Width="100" runat="server"></asp:TextBox>
                                <asp:MaskedEditExtender ID="MaskedEditExtender2" runat="server" TargetControlID="txtMaximo" Mask="999,999,999,999,999" MessageValidatorTip="true"
                                    OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="Number" InputDirection="RightToLeft"
                                    AcceptNegative="Left" DisplayMoney="Left" ErrorTooltipEnabled="True">
                                </asp:MaskedEditExtender>
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
</asp:Content>



