<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" %>
<%@ Register Src="../../../General/Controles/fecha.ascx" TagName="fecha" TagPrefix="uc1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>    
    <asp:Panel ID="Principal" runat="server">
        <asp:Panel ID="pConsulta" runat="server" Width="200px">
            <table style="width: 373%;">
                <tr>
                    <td colspan="6" style="font-size: x-small">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="font-size: x-small; text-align:left" colspan="6">
                        <strong>Críterios de Búsqueda</strong>
                    </td>
                </tr>
                <tr>
                    <td style="width: 189px; text-align:left">
                        Número de Comprobante</td>
                    <td colspan="2" style="text-align:left">
                        <asp:TextBox ID="txtNumComp" runat="server" CssClass="textbox" Width="121px"></asp:TextBox>
                    </td>
                    <td style="text-align:left">
                        Tipo de Comprobante</td>
                    <td>
                        <asp:DropDownList ID="ddlTipoComprobante" runat="server" CssClass="textbox" 
                            Width="197px" AppendDataBoundItems="True" >
                            <asp:ListItem Value="0">Seleccione un Item</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="width: 189px; text-align:left">
                        Fecha Elaboración
                    </td>
                    <td style="width: 20px; text-align:left">
                        <uc1:fecha ID="txtFechaIni" runat="server"></uc1:fecha>
                    </td>
                    <td style="text-align:left">
                        a
                    </td>
                    <td style="text-align:left">
                        <uc1:fecha ID="txtFechaFin" runat="server"></uc1:fecha>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="6">
                        &nbsp;
                    </td>
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
                            onpageindexchanging="gvLista_PageIndexChanging" style="font-size: x-small">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:TemplateField HeaderStyle-CssClass="gridIco">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnInfo" runat="server" CommandName="Select" ImageUrl="~/Images/gr_info.jpg"
                                            ToolTip="Detalle" Width="16px" />
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="gridIco"></HeaderStyle>
                                </asp:TemplateField>
                                <asp:BoundField DataField="num_comp" HeaderText="Número" />
                                <asp:BoundField DataField="tipo_comp" HeaderText="Tipo" />
                                <asp:BoundField DataField="fecha" DataFormatString="{0:d}" HeaderText="Fecha" />
                                <asp:BoundField DataField="descripcion_concepto" HeaderText="Concepto" />
                                <asp:BoundField DataField="ciudad" HeaderText="Ciudad" />
                                <asp:BoundField DataField="iden_benef" HeaderText="Identificacion" />
                                <asp:BoundField DataField="nombre" HeaderText="Nombre" />
                                <asp:BoundField DataField="elaboro" HeaderText="Elaborado por" />
                                <asp:BoundField DataField="aprobo" HeaderText="Aprobado por" />
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
