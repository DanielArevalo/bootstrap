<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/ctlPlanCuentas.ascx" TagName="ListadoPlanCtas" TagPrefix="uc1" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table cellpadding="5" cellspacing="0" style="width: 100%">
        <tr>
            <td colspan="2" style="text-align: left">
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
                                    <asp:CheckBox ID="chkPermiso" runat="server" />
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
            <td style="text-align: left; width: 130px;">C&oacute;digo</td>
            <td style="text-align: left">
                <asp:Label ID="lblCodigo" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="text-align: left; width: 130px;">Fecha de Creación</td>
            <td style="text-align: left">
                <asp:TextBox ID="txtFechaCreacion" runat="server" Enabled="true" CssClass="textbox"
                    MaxLength="10" Width="135px"></asp:TextBox>
                  &nbsp;<asp:CalendarExtender ID="CalendarExtender1" runat="server" 
                        PopupButtonID="Image1" 
                        TargetControlID="txtFechaCreacion"
                        Format="dd/MM/yyyy" >
                    </asp:CalendarExtender>
            </td>
        </tr>
        <tr>
            <td class="tdI" style="text-align: left; width: 130px;">Oficina</td>
            <td class="tdI" style="text-align: left">
                <asp:Label ID="lblCodOficina" runat="server"></asp:Label>&nbsp;
                <asp:Label ID="lblOficina" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="text-align: left; width: 130px;">Nombre Caja * 
            </td>
            <td style="text-align: left">
                <asp:TextBox ID="txtCaja" runat="server" MaxLength="50" CssClass="textbox" Width="210px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="text-align: left; width: 130px;">Tipo de Caja
            </td>
            <td style="text-align: left">
                <asp:RadioButtonList
                    ID="radTipoCaja" runat="server" RepeatDirection="Horizontal"
                    RepeatLayout="Flow">
                    <asp:ListItem Value="1">Principal</asp:ListItem>
                    <asp:ListItem Value="0">Auxiliar</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td style="text-align: left; width: 130px;">Estado Actual &#160</td>
            <td style="text-align: left">
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
            <td colspan ="2" style="width:50%">
                <asp:UpdatePanel runat="server">
                    <ContentTemplate>
                        <table>
                            <tr>
                                <td style="width: 120px; text-align: left">Cod Cuenta<br />
                                    <cc1:TextBoxGrid ID="txtCodCuenta" runat="server" AutoPostBack="True" BackColor="#F4F5FF"
                                        CssClass="textbox" OnTextChanged="txtCodCuenta_TextChanged" Style="text-align: left"
                                        Width="100"></cc1:TextBoxGrid>
                                    <uc1:ListadoPlanCtas ID="ctlListadoPlan" runat="server" />
                                </td>
                                <td style="width: 35px; text-align: center">
                                    <br />
                                    <cc1:ButtonGrid ID="btnListadoPlan" runat="server" CssClass="btnListado" OnClick="btnListadoPlan_Click"
                                        Width="95%" Text="..." />
                                </td>
                                <td style="width: 230px; text-align: left">Nombre de la Cuenta<br />
                                    <cc1:TextBoxGrid ID="txtNomCuenta" runat="server" CssClass="textbox" Enabled="false"
                                        Width="190px" />
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
            <td colspan="2" style="text-align: left">
                <strong>Topes Permitidos para la Caja</strong></td>
        </tr>
        <tr>
            <td colspan="2" style="text-align: left; vertical-align: top">
                <asp:GridView ID="gvTopes" runat="server" BackColor="White"
                    BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4"
                    ForeColor="Black" GridLines="Vertical"
                    AutoGenerateColumns="False">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:BoundField DataField="tipotope" HeaderStyle-CssClass="gridColNo" ItemStyle-CssClass="gridColNo">
                            <HeaderStyle CssClass="gridColNo"></HeaderStyle>
                            <ItemStyle CssClass="gridColNo"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="descTope" HeaderText="Tipo" />
                        <asp:BoundField DataField="cod_moneda" HeaderStyle-CssClass="gridColNo" ItemStyle-CssClass="gridColNo">
                            <HeaderStyle CssClass="gridColNo"></HeaderStyle>
                            <ItemStyle CssClass="gridColNo"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="descMoneda" HeaderText="Moneda" />
                        <asp:TemplateField HeaderText="Mínimo">
                            <ItemTemplate>
                                <uc1:decimales ID="txtMinimo" runat="server" CssClass="textbox" Width="100"
                                    MaxLength="10" style="text-align: right"></uc1:decimales>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Máximo">
                            <ItemTemplate>
                                <uc1:decimales ID="txtMaximo" runat="server" CssClass="textbox" Width="100"
                                    MaxLength="10" style="text-align: right"></uc1:decimales>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="" Visible="False">
                            <ItemTemplate>
                                <asp:TextBox ID="txtMinimotex" runat="server"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="" Visible="False">
                            <ItemTemplate>
                                <asp:TextBox ID="txtMaximotex" runat="server"></asp:TextBox>
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



