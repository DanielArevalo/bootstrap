<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ctlAvances.ascx.cs" Inherits="ListadoAvances"  Debug="true" %>

<%@ Register Src="~/General/Controles/ctlNumeroConDecimales.ascx" TagName="numero" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>

<asp:DragPanelExtender ID="dpeAvances" runat="server" TargetControlID="panelAvances" DragHandleID="panelTitulo" />
<asp:Panel ID="panelAvances" runat="server" BackColor="White" Style="text-align: right; position: absolute; top: 200px; left: 200px" 
    BorderWidth="1px" Width="600px" Visible="False">
    <asp:Panel ID="panelTitulo" runat="server" Width="100%" Height="20px" CssClass="sidebarheader">
        <table cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td style="text-align:center;" width="85%">
                    Listado de Avances
                </td>
                <td style="text-align:center; padding-top: 0px; z-index: 999" width="15%">
                    <asp:ImageButton ID="btnAceptar" runat="server" ImageUrl="../../Images/aplicada.png" onclick="bntAceptar_Click" />
                    <asp:ImageButton ID="btnCerrar"  runat="server" ImageUrl="../../Images/noaplicada.png" onclick="btnCerrar_Click" />                   
                </td>
            </tr>
        </table>
    </asp:Panel>    
    <asp:Panel ID="PanelListadoAvances" runat="server" BackColor="White" Width="100%" > 
        <asp:HiddenField ID="hfControl" runat="server" Visible="False" />
        <asp:HiddenField ID="hfRadicacion" runat="server" Visible="False" />
        <asp:HiddenField ID="hfIdAvance" runat="server" Visible="False" />   
        <asp:HiddenField ID="hfValorAPagar" runat="server" Visible="False" />           
        <table cellpadding="0" cellspacing="0" style="width: 100%" border="0">
            <tr style="background-color:#DEDFDE">
                <td colspan="6" style="background-color:#DEDFDE">
                    <asp:GridView ID="gvAvances" runat="server" AutoGenerateColumns="False" 
                        BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" 
                        CellPadding="4" ForeColor="Black" GridLines="Vertical" 
                        Width="100%" AllowPaging="True" AllowSorting="True" DataKeyNames="NumAvance"
                        style="font-size: xx-small" OnPageIndexChanging="gvAvances_PageIndexChanging" PageSize="40">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:TemplateField HeaderStyle-CssClass="gridIco">
                                <ItemTemplate>
                                    <cc1:CheckBoxGrid ID="cbListado" runat="server" CommandArgument='<%#DataBinder.Eval(Container, "RowIndex") %>' />
                                </ItemTemplate>
                                <HeaderStyle CssClass="gridIco"></HeaderStyle>
                            </asp:TemplateField>
                            <asp:BoundField DataField="NumAvance" HeaderText="Id Avance">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="FechaDesembolsi" HeaderText="Fecha Desembolso" DataFormatString="{0:d}">
                                <ItemStyle HorizontalAlign="left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="FechaProxPago" HeaderText="Fecha Prox.Pago" DataFormatString="{0:d}">
                                <ItemStyle HorizontalAlign="left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ValDesembolso" HeaderText="Valor Desembolso" DataFormatString="{0:N}" ItemStyle-HorizontalAlign="Right">
                                <ItemStyle HorizontalAlign="right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ValorCuota" HeaderText="Valor Cuota" DataFormatString="{0:N}" ItemStyle-HorizontalAlign="Right">
                                <ItemStyle HorizontalAlign="right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Plazo" HeaderText="Plazo">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="SaldoAvance" HeaderText="Saldo Avance" DataFormatString="{0:N}" ItemStyle-HorizontalAlign="Right">
                                <ItemStyle HorizontalAlign="right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ValorTotal" HeaderText="Tot. a Pagar" DataFormatString="{0:N}" ItemStyle-HorizontalAlign="Right">
                                <ItemStyle HorizontalAlign="right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Numero_Radicacion" HeaderText="No.Radic.">
                                <ItemStyle HorizontalAlign="center" />
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
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Panel>
<asp:DropShadowExtender ID="dse" runat="server" TargetControlID="panelAvances" Opacity=".2" TrackPosition="true" Radius="2" />