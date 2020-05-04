<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:ImageButton runat="server" ID="btnConsultar" ImageUrl="~/Images/btnConsultar.jpg" OnClick="btnConsultar_Click" ImageAlign="Right" />
    <asp:Panel ID="Principal" runat="server">
        <asp:Panel ID="pConsulta" runat="server" Width="200px">
            <table style="width: 373%;">
                <tr>
                    <td colspan="6" style="font-size: x-small">
                        &nbsp;</td>
                    <tr>
                        <td style="font-size: x-small;" colspan="6">
                            <strong>Críterios de Búsqueda</strong></td>
                        <tr>
                            <td class="logo" style="width: 348px">
                                Número de Comprobante</td>
                            <td style="width: 238px">
                                <asp:TextBox ID="txtNumComp" runat="server" CssClass="textbox" Width="121px"></asp:TextBox>
                            </td>
                            <td style="width: 290px">
                                Tipo de Comprobante</td>
                            <td style="width: 168px">
                                <asp:DropDownList ID="ddlTipoComprobante" runat="server" CssClass="textbox" 
                                    Width="197px" 
                                    onselectedindexchanged="ddlTipoComprobante_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td>
                                &nbsp;</td>
                            <td style="width: 48px">
                                &nbsp;</td>
                            <tr>
                                <td colspan="6">
                                    &nbsp;</td>
                            </tr>
                        </tr>
                    </tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="Listado" runat="server">
            <table style="width: 100%;">
                <tr>
                    <td>
                        <asp:GridView ID="gvLista" runat="server" AllowPaging="True" 
                            AutoGenerateColumns="False" BackColor="White" BorderColor="#DEDFDE" 
                            BorderStyle="None" BorderWidth="1px" CellPadding="4"  
                            ForeColor="Black" GridLines="Vertical" PageSize="20" Width="100%"
                            OnSelectedIndexChanged="gvLista_SelectedIndexChanged" 
                            onrowediting="gvLista_RowEditing" 
                            onpageindexchanging="gvLista_PageIndexChanging">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:TemplateField HeaderStyle-CssClass="gridIco">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnEditar" runat="server" CommandName="Edit" ImageUrl="~/Images/gr_edit.jpg"
                                            ToolTip="Editar" Width="16px" />
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="gridIco" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="num_comp" HeaderText="Número" />
                                <asp:BoundField DataField="tipo_comp" HeaderText="Tipo" />
                                <asp:BoundField DataField="fecha" DataFormatString="{0:d}" HeaderText="Fecha" />
                                <asp:BoundField DataField="descripcion_concepto" HeaderText="Concepto" />
                                <asp:BoundField DataField="ciudad" HeaderText="Ciudad" />
                                <asp:BoundField DataField="iden_benef" HeaderText="Identificacion" />
                                <asp:BoundField DataField="nombre" HeaderText="Nombre" />
                                <asp:BoundField DataField="cod_elaboro" HeaderText="Elaborado por" />
                                <asp:BoundField DataField="cod_aprobo" HeaderText="Aprobado por" />
                                <asp:BoundField DataField="estado" HeaderText="Estado" />
                                <asp:BoundField DataField="totalcom" DataFormatString="{0:N0}" HeaderText="Valor"> <ItemStyle HorizontalAlign="Right" /></asp:BoundField>
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
                        <asp:Label ID="lblTotalRegs" runat="server" Visible="False"/>
                        <asp:Label ID="lblInfo" runat="server" Text="Su consulta no obtuvo ningun resultado." Visible="False"/>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </asp:Panel>
</asp:Content>
