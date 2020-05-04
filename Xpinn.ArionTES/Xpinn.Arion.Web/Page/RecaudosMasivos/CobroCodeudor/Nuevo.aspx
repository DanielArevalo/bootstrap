<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/General/Master/site.master" CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/ctlListarCreditosPorFiltro.ascx" TagName="ListarCreditos" TagPrefix="uc3" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <script src="../../../Scripts/PCLBryan.js" type="text/javascript"></script>
    <asp:MultiView ActiveViewIndex="0" ID="mvNuevoCobro" runat="server">
        <asp:View runat="server">
            <uc3:ListarCreditos ID="ctlListarCreditos" runat="server" style="text-align: right;" />
        </asp:View>
        <asp:View runat="server">
            <table cellpadding="5" cellspacing="0" style="width: 100%">
                <tr style="text-align: left">
                    <td>
                        <strong>COBRO CODEUDOR</strong>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table style="width: 75%;">
                            <tr>
                                <td style="text-align: left; width: 15%">Fecha
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left; width: 75%" colspan="4">
                                    <asp:TextBox ID="txtFecha" runat="server" CssClass="textbox"
                                        Enabled="false" Width="15%"></asp:TextBox>
                                    <asp:CalendarExtender ID="CalendarExtender1" runat="server"
                                        PopupButtonID="Image1"
                                        TargetControlID="txtFecha"
                                        Format="dd/MM/yyyy">
                                    </asp:CalendarExtender>
                                    <img id="Image1" alt="Calendario" src="../../../Images/iconCalendario.png" />
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left; width: 15%">No Radicación
                                </td>
                                <td style="text-align: left; width: 15%" colspan="2">Línea
                                </td>
                                <td style="text-align: left; width: 15%">Fecha Desembolso
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left; width: 15%">
                                    <asp:TextBox ID="txtNroRadicacion" Enabled="false" runat="server" Width="90%" CssClass="textbox"></asp:TextBox>
                                </td>

                                <td style="text-align: left; width: 15%" colspan="2">
                                    <asp:TextBox ID="txtLinea" runat="server" CssClass="textbox" Enabled="false" Width="98%"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtFechaDesembolso" Style="margin-left: 10px" runat="server" CssClass="Textbox"
                                        Enabled="false" Width="70%"></asp:TextBox>
                                    <asp:CalendarExtender ID="CalendarExtender2" runat="server"
                                        PopupButtonID="Image2"
                                        TargetControlID="txtFechaDesembolso"
                                        Enabled="false"
                                        Format="dd/MM/yyyy">
                                    </asp:CalendarExtender>
                                    <img id="Image2" alt="Calendario" src="../../../Images/iconCalendario.png" />
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left; width: 15%">Monto
                                </td>
                                <td style="text-align: left; width: 15%">Saldo Capital
                                </td>
                                <td style="text-align: left; width: 15%">Cuota
                                </td>
                                <td style="text-align: left; width: 15%">Plazo
                                </td>
                                <td style="text-align: left; width: 15%">Fecha Proximo Pago
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left; width: 15%">
                                    <asp:TextBox ID="txtMonto" runat="server" Enabled="false" Width="90%" CssClass="textbox"></asp:TextBox>
                                </td>
                                <td style="text-align: left; width: 15%">
                                    <asp:TextBox ID="txtSaldoCapital" runat="server" Enabled="false" CssClass="textbox"></asp:TextBox>
                                </td>
                                <td style="text-align: left; width: 11%">
                                    <asp:TextBox ID="txtCuota" runat="server" Enabled="false" CssClass="textbox"></asp:TextBox>
                                </td>
                                <td style="text-align: left; width: 15%">
                                    <asp:TextBox ID="txtPlazo" runat="server" Enabled="false" CssClass="textbox"></asp:TextBox>
                                </td>
                                <td style="text-align: left; width: 15%">
                                    <asp:TextBox ID="txtFechaProximoPago" runat="server" CssClass="Textbox"
                                        Enabled="false" Width="70%"></asp:TextBox>
                                    <asp:CalendarExtender ID="CalendarExtender4" runat="server"
                                        PopupButtonID="Image3"
                                        TargetControlID="txtFechaDesembolso"
                                        Enabled="false"
                                        Format="dd/MM/yyyy">
                                    </asp:CalendarExtender>
                                    <img id="Image3" alt="Calendario" src="../../../Images/iconCalendario.png" />
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left; width: 15%">Identificación
                                </td>
                                <td style="text-align: left; width: 15%" colspan="2">Nombre
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left; width: 15%">
                                    <asp:TextBox ID="txtIdentificacion" Enabled="false" runat="server" Width="90%" CssClass="textbox"></asp:TextBox>
                                </td>

                                <td style="text-align: left; width: 15%" colspan="2">
                                    <asp:TextBox ID="txtNombre" runat="server" CssClass="textbox" Enabled="false" Width="98%"></asp:TextBox>
                                </td>
                                <td style="text-align: left; width: 15%" colspan="2">
                                    <asp:TextBox ID="txtCodDeudor" Visible="false" runat="server" CssClass="textbox" Enabled="false" Width="98%"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <asp:UpdatePanel runat="server">
                <ContentTemplate>
                    <table cellpadding="5" cellspacing="0" style="width: 90%" border="0">
                        <tr>
                            <td align="left">
                                <b>COBRO CODEUDOR</b>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" style="text-align: left">
                                <asp:GridView ID="gvCodeudores" runat="server"
                                    AutoGenerateColumns="False" AllowPaging="False" PageSize="20" BackColor="White"
                                    BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4"
                                    ForeColor="Black" GridLines="Vertical" Width="80%" DataKeyNames="idcobrocodeud"
                                    OnRowDataBound="gvCodeudores_OnRowDataBound">
                                    <AlternatingRowStyle BackColor="White" />
                                    <Columns>
                                        <asp:BoundField DataField="cod_persona" HeaderText="Código" ItemStyle-HorizontalAlign="Left" />
                                        <asp:BoundField DataField="identificacion_codeudor" HeaderText="Identificacion" ItemStyle-HorizontalAlign="Left" />
                                        <asp:BoundField DataField="nombreYApellidoCodeudor" HeaderText="Nombres" ItemStyle-HorizontalAlign="Left" />
                                        <asp:TemplateField HeaderText="Porcentaje" ItemStyle-Width="15%">
                                            <ItemTemplate>
                                                <cc1:TextBoxGrid ID="txtPorcentaje" runat="server" Text='<%# Eval("porcentaje") %>' onkeypress="return isNumber(event)"
                                                    CommandArgument='<%#Container.DataItemIndex %>' AutoPostBack="true" OnTextChanged="txtPorcentajeGVCOD_TextChanged" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Valor" ItemStyle-Width="15%">
                                            <ItemTemplate>
                                                <asp:Label ID="txtValor" Text='<%# Eval("valor", "${0:N}") %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Empresa recaudo" ItemStyle-Width="20%">
                                            <ItemTemplate>
                                                <cc1:DropDownListGrid ID="ddlEmpresaRecaudo" runat="server" CssClass="textbox" Width="95%">
                                                </cc1:DropDownListGrid>
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
                </ContentTemplate>
            </asp:UpdatePanel>
            <uc4:mensajegrabar ID="ctlMensaje" runat="server" />
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
                            <asp:Label ID="lblMensaje" runat="server" Text="Modificación Realizada Correctamente"
                                Style="color: #FF3300"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <br />
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;"></td>
                    </tr>
                </table>
            </asp:Panel>
        </asp:View>
    </asp:MultiView>
</asp:Content>
