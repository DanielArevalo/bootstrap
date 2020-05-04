<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <asp:ImageButton runat="server" ID="btnGuardar"
        ImageUrl="~/Images/btnGuardar.jpg" ValidationGroup="vgGuardar"
        OnClick="btnGuardar_Click" ImageAlign="Right" />
    <asp:ImageButton runat="server" ID="btnCancelar"
        ImageUrl="~/Images/btnCancelar.jpg" OnClick="btnCancelar_Click"
        ImageAlign="Right" />
    <asp:MultiView ID="MultiView1" runat="server">
        <asp:View ID="View1" runat="server">
            <table cellpadding="5" cellspacing="0" style="width: 90%">

                <tr>
                    <td class="logo" style="width: 266px">Fecha del traslado
                        <br />
                        <asp:TextBox ID="txtFechaTraslado" runat="server" Enabled="false" CssClass="textbox"
                            MaxLength="10" Width="200px" Style="text-align: center"></asp:TextBox>
                    </td>
                    <td class="columnForm50" style="width: 406px">
                        <br />
                    </td>
                    <td class="tdI" style="width: 207px"></td>
                </tr>
                <tr>
                    <td style="background-color: #3599F7; text-align: center;" colspan="3">
                        <strong style="color: #FFFFFF">Información del traslado</strong></td>
                </tr>
                <tr>
                    <td class="logo" style="width: 266px; text-align: center;">Oficina<br />
                        <asp:TextBox ID="txtOficina" runat="server" Enabled="False" CssClass="textbox"
                            Width="200px"></asp:TextBox>
                    </td>
                    <td style="width: 406px; text-align: center;">Cajero<br />
                        <asp:TextBox ID="txtCajero" runat="server"
                            Enabled="False" CssClass="textbox" Width="200px"></asp:TextBox>
                        <br />
                    </td>
                    <td class="tdI" style="width: 207px; text-align: center;">Caja<br />
                        <asp:TextBox ID="txtCaja" runat="server" Enabled="False" CssClass="textbox"
                            Width="150px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="logo" style="width: 251px; text-align: center;">Moneda
            <br />
                        <asp:DropDownList ID="ddlMonedas" CssClass="dropdown" runat="server"
                            Height="27px" Width="213px">
                        </asp:DropDownList>
                        <br />
                        <br />
                        <br style="font-size: xx-small" />
                    </td>
                    <td style="width: 406px; text-align: center;">Cajero a quien se le entrega
            <br />
                        <asp:DropDownList ID="ddlCajeros"
                            CssClass="dropdown" runat="server"
                            Height="27px" Width="213px">
                        </asp:DropDownList>
                        <span style="font-size: xx-small">
                            <br />
                            <br />
                            &nbsp; </span>
                        <br />
                    </td>
                    <td class="tdI" style="width: 207px; text-align: center;" valign="top">Valor<br />
                        <asp:UpdatePanel runat="server">
                            <ContentTemplate>
                                <uc1:decimales ID="txtValor" runat="server" CssClass="textbox" Width="260px"
                                    MaxLength="9" style="text-align: right"></uc1:decimales>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td colspan="3" style="text-align: center; height: 30px;">
                        <strong style="color: #000000;">Saldo en Caja</strong>
                    </td>
                </tr>
                <tr>
                    <td style="width: 406px; text-align: center;">Valor en Efectivo<br />
                        <asp:TextBox ID="txtValorEfectivo" runat="server" Enabled="false" CssClass="textbox" Width="150px" Style="text-align: right"></asp:TextBox>
                        <asp:MaskedEditExtender ID="MaskedEditExtender4" runat="server" TargetControlID="txtValorEfectivo" Mask="999,999,999,999,999" MessageValidatorTip="true"
                            OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="Number" InputDirection="RightToLeft"
                            AcceptNegative="Left" DisplayMoney="Left" ErrorTooltipEnabled="True"></asp:MaskedEditExtender>
                        <br />
                    </td>
                    <td style="width: 406px; text-align: center;">Valor en Cheque<br />
                        <asp:TextBox ID="txtValorCheque" runat="server"
                            Enabled="false" CssClass="textbox" Width="150px"
                            Style="text-align: right"></asp:TextBox>
                        <asp:MaskedEditExtender ID="MaskedEditExtender5" runat="server" TargetControlID="txtValorCheque" Mask="999,999,999,999,999" MessageValidatorTip="true"
                            OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="Number" InputDirection="RightToLeft"
                            AcceptNegative="Left" DisplayMoney="Left" ErrorTooltipEnabled="True"></asp:MaskedEditExtender>
                        <br />
                    </td>
                    <td class="tdI" style="width: 207px; text-align: center;"></td>
                </tr>
                <tr>
                    <td colspan="3" style="background-color: #3599F7; text-align: center;"><strong style="color: #FFFFFF">Traslados pendientes por recibir</strong></td>
                </tr>
                <tr>
                    <td colspan="3" style="text-align: center;">
                        <asp:GridView ID="gvMovimiento" runat="server" AutoGenerateColumns="False" 
                            BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" 
                            BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical" PageSize="20" style="font-size: small"
                            OnRowDeleting="gvMovimiento_RowDeleting">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                        <asp:TemplateField HeaderStyle-CssClass="gridIco"><ItemTemplate><asp:ImageButton ID="btnEliminar" runat="server" CommandName="Delete" ImageUrl="~/Images/gr_elim.jpg"
                                    ToolTip="Eliminar" Width="16px" /></ItemTemplate></asp:TemplateField>
                                <asp:BoundField DataField="nom_oficina" HeaderText="Oficina" />
                                <asp:BoundField DataField="nom_caja" HeaderText="Caja" />
                                <asp:BoundField DataField="nom_moneda" HeaderText="Moneda" />
                                <asp:BoundField DataField="cod_ope" HeaderText="Cod.Ope"><ItemStyle HorizontalAlign="Left" /></asp:BoundField>
                                <asp:BoundField DataField="num_comp" HeaderText="Num.Comp"><ItemStyle HorizontalAlign="Left" /></asp:BoundField>
                                <asp:BoundField DataField="tipo_comp" HeaderText="T.Comp"><ItemStyle HorizontalAlign="Left" /></asp:BoundField>
                                <asp:BoundField DataField="fecha_movimiento" DataFormatString="{0:d}" HeaderText="Fecha" />
                                <asp:BoundField DataField="tipo_movimiento" HeaderText="Tipo Movimiento" />
                                <asp:BoundField DataField="num_producto" HeaderText="Num. Producto" />
                                <asp:BoundField DataField="valor_pago" DataFormatString="{0:N0}" HeaderText="Valor"><ItemStyle HorizontalAlign="Right" /></asp:BoundField>
                                <asp:BoundField DataField="nom_tipo_tran" HeaderText="Tipo Transacción" />
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
        </asp:View>
        <asp:View ID="View2" runat="server">

            <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana"
                Font-Size="8pt" InteractiveDeviceInfos="(Colección)"
                WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="789px"><LocalReport ReportPath="Page\CajaFin\TrasladoCaja\ReporteTraslado.rdlc"></LocalReport></rsweb:ReportViewer>
        </asp:View>
    </asp:MultiView>

</asp:Content>
