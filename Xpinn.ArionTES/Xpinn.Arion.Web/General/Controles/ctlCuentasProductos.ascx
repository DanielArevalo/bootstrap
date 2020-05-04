<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ctlCuentasProductos.ascx.cs" Inherits="ctlCuentasProductos" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>

<asp:DragPanelExtender ID="dpeBusquedaRapida" runat="server" TargetControlID="panelBusquedaRapida" DragHandleID="panelTitulo" />
<asp:Panel ID="panelBusquedaRapida" runat="server" BackColor="White" Style="text-align: right; position: absolute; top: 200px; left: 200px" 
    BorderWidth="1px" Width="665px" Visible="False">
    <asp:Panel ID="panelTitulo" runat="server" Width="100%" Height="20px" CssClass="sidebarheader">
        <table cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td style="text-align:center;" width="95%">
                    Lista de Productos [ <asp:Label ID="lblTipoProduct" runat="server" /> ]
                </td>
                <td style="text-align:center; padding-top: 0px; z-index: 999" width="5%">
                    <asp:ImageButton ID="bntCerrar" runat="server" ImageUrl="../../Images/btnCerrar.jpg" onclick="bntCerrar_Click" />                   
                </td>
            </tr>
        </table>
    </asp:Panel>    
    <asp:Panel ID="PanelListadoProducto" runat="server" BackColor="White" Width="100%">
        <asp:HiddenField ID="hfTipoProd" runat="server" Visible="False" />
        <asp:HiddenField ID="hfNroProduc" runat="server" Visible="False" />
        <asp:HiddenField ID="hfCodigo" runat="server" Visible="False" />
        <asp:HiddenField ID="hfIdentificacion" runat="server" Visible="False" />
        <asp:HiddenField ID="hfNombre" runat="server" Visible="False" />
        <asp:HiddenField ID="hfLinea" runat="server" Visible="False" />
        <asp:HiddenField ID="hfOficina" runat="server" Visible="False" />

        <table cellpadding="0" cellspacing="0" style="width: 100%" border="0">
            <tr style="background-color:#DEDFDE">
                <td style="font-size: xx-small; text-align: left;" colspan="2" rowspan="2">
                    <span style="font-weight: bold">Buscar por:</span>
                </td>
                <td style="font-size: xx-small; text-align: left; width: 110px">
                    # Producto</td>
                <td style="font-size: xx-small; text-align: left; width: 110px">
                    Código</td>
                <td style="font-size: xx-small; text-align: left; width: 110px">
                    Identificación</td>
                <td style="font-size: xx-small; text-align: left; width:210px">
                    Nombre</td>
                <td style="font-size: xx-small; text-align: left; width:90px" colspan="2" rowspan="2" class="style2">
                    <br />
                    <asp:Button ID="btnConsultar" runat="server" Text="Buscar" 
                        onclick="btnConsultar_Click" style="font-size: xx-small" Width="50px" />
                </td>
            </tr>
            <tr style="background-color:#DEDFDE">
                <td style="font-size: xx-small; text-align: left;">
                    <asp:TextBox ID="txtNroProduc" runat="server" Width="95px" Font-Size="XX-Small"></asp:TextBox>
                </td>
                <td style="font-size: xx-small; text-align: left;">
                    <asp:TextBox ID="txtCod" runat="server" Width="95px" Font-Size="XX-Small"></asp:TextBox>
                </td>
                <td style="font-size: xx-small; text-align: left;">
                    <asp:TextBox ID="txtIdenti" runat="server" Width="95px" Font-Size="XX-Small"></asp:TextBox>
                </td>
                <td style="font-size: xx-small; text-align: left;">
                    <asp:TextBox ID="txtNom" runat="server" Width="95%" Font-Size="XX-Small"></asp:TextBox>
                </td>
            </tr>
            <tr style="background-color:#DEDFDE">
                <td colspan="8" style="background-color:#DEDFDE">
                    <asp:GridView ID="gvProductos" runat="server" AutoGenerateColumns="False" 
                        BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" 
                        CellPadding="4" ForeColor="Black" GridLines="Vertical" 
                        Width="100%" AllowPaging="True" AllowSorting="True" PageSize="15" 
                        onpageindexchanging="gvProductos_PageIndexChanging" 
                        style="font-size: xx-small" onrowdatabound="gvProductos_RowDataBound" >
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:TemplateField HeaderStyle-CssClass="gridIco">
                                <ItemTemplate>
                                    <cc1:ImageButtonGrid ID="btnSeleccionar" runat="server"
                                        CommandArgument='<%#Container.DataItemIndex %>' ImageUrl="~/Images/gr_info.jpg"
                                        OnClick="btnSeleccionar_Click" />
                                </ItemTemplate>
                                <HeaderStyle CssClass="gridIco" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:BoundField ApplyFormatInEditMode="True" DataField="cod_persona" HeaderText="Cod Persona">
                                <ItemStyle Width="20px" HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="identificacion" HeaderText="Identificación">
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="nombre" HeaderText="Nombre">
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="numero_cuenta" HeaderText="# Producto">
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="nom_linea" HeaderText="Línea Producto">
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="cod_oficina" HeaderText="Cod Oficina">
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Center" />
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
<asp:DropShadowExtender ID="dse" runat="server" TargetControlID="panelBusquedaRapida" Opacity=".2" TrackPosition="true" Radius="2" />