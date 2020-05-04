﻿<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ctlBusquedaRapida.ascx.cs" Inherits="BusquedaRapida"  Debug="true" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>

<asp:DragPanelExtender ID="dpeBusquedaRapida" runat="server" TargetControlID="panelBusquedaRapida" DragHandleID="panelTitulo" />
<asp:Panel ID="panelBusquedaRapida" runat="server" BackColor="White" Style="text-align: right; position: absolute;top: auto;left: auto;" 
    BorderWidth="1px" Width="600px" Visible="False">
    <asp:Panel ID="panelTitulo" runat="server" Width="100%" Height="20px" CssClass="sidebarheader">
        <table cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td style="text-align:center;" width="95%">
                    Listado de Personas
                </td>
                <td style="text-align:center; padding-top: 0px; z-index: 999" width="5%">
                    <asp:ImageButton ID="bntCerrar" runat="server" ImageUrl="../../Images/btnCerrar.jpg" onclick="bntCerrar_Click" />                   
                </td>
            </tr>
        </table>
    </asp:Panel>    
    <asp:Panel ID="PanelListadoPersonas" runat="server" BackColor="White" Width="100%" >
        <asp:HiddenField ID="hfCodigo" runat="server" Visible="False" />
        <asp:HiddenField ID="hfIdentificacion" runat="server" Visible="False" />
        <asp:HiddenField ID="hfTipoIdentificacion" runat="server" Visible="False" />
        <asp:HiddenField ID="hfNombre" runat="server" Visible="False" />    
        <asp:HiddenField ID="hfApellido" runat="server" Visible="False" />      
        <asp:HiddenField ID="hfControl" runat="server" Visible="False" />
        <asp:HiddenField ID="hfIdentificacion2" runat="server" Visible="False" />
        <asp:HiddenField ID="hfNombre2" runat="server" Visible="False" />    
        <asp:HiddenField ID="hfDireccion" runat="server" Visible="False" />
        <asp:HiddenField ID="hftelefono" runat="server" Visible="False" />      
        <asp:HiddenField ID="hfciudad" runat="server" Visible="False" />          
        <table cellpadding="0" cellspacing="0" style="width: 100%" border="0">
            <tr style="background-color:#DEDFDE">
                <td style="font-size: xx-small; text-align: left; width: 40px" rowspan="2">
                    <span style="font-weight: bold">Buscar por:</span></td>
                <td style="font-size: xx-small; text-align: left; width: 50px">
                    Código</td>
                <td style="font-size: xx-small; text-align: left; width: 100px">
                    Identificación</td>
                <td style="font-size: xx-small; text-align: left; width: 180px">
                    Nombres</td>
                <td style="font-size: xx-small; text-align: left; width: 180px">
                    Apellidos</td>
                <td style="font-size: xx-small; text-align: left; width: 50px" rowspan="2" class="style2">
                    <br />
                    <asp:Button ID="btnConsultar" runat="server" Text="Buscar" 
                        onclick="btnConsultar_Click" style="font-size: xx-small" Width="45px" />
                </td>
            </tr>
            <tr style="background-color:#DEDFDE">
                <td style="font-size: xx-small; text-align: left;">
                    <asp:TextBox ID="txtCod"  onkeypress="return isNumber(event)" runat="server" Width="45px" Font-Size="XX-Small"></asp:TextBox>
                </td>
                <td style="font-size: xx-small; text-align: left;">
                    <asp:TextBox ID="txtIde"  onkeypress="return isNumber(event)" runat="server" Width="95px" Font-Size="XX-Small"></asp:TextBox>
                </td>
                <td style="font-size: xx-small; text-align: left;">
                    <asp:TextBox ID="txtNom" runat="server" Width="175px" Font-Size="XX-Small"></asp:TextBox>
                </td>
                <td style="font-size: xx-small; text-align: left;">
                    <asp:TextBox ID="txtApe" runat="server" Width="175px" Font-Size="XX-Small"></asp:TextBox>
                </td>
            </tr>
            <tr style="background-color:#DEDFDE">
                <td colspan="6" style="background-color:#DEDFDE">
                    <asp:GridView ID="gvPersonas" runat="server" AutoGenerateColumns="False" 
                        BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" 
                        CellPadding="4" ForeColor="Black" GridLines="Vertical" 
                        Width="100%" AllowPaging="True" AllowSorting="True" 
                        onpageindexchanging="gvPersonas_PageIndexChanging" DataKeyNames="tipo_identificacion"
                        style="font-size: xx-small" onrowdatabound="gvPersonas_RowDataBound" >
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:TemplateField HeaderStyle-CssClass="gridIco">
                                <ItemTemplate>
                                    <cc1:ImageButtonGrid ID="btnSeleccionar" runat="server" 
                                            CommandArgument='<%#Container.DataItemIndex %>' ImageUrl="~/Images/gr_info.jpg" 
                                            onclick="btnSeleccionar_Click" OnClientClick="btnSeleccionar_Click" />
                                </ItemTemplate>
                                <HeaderStyle CssClass="gridIco" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:BoundField ApplyFormatInEditMode="True" DataField="cod_persona" HeaderText="Código">
                                <ItemStyle Width="20px" />
                                <HeaderStyle HorizontalAlign="Center"/>
                            </asp:BoundField>
                            <asp:BoundField DataField="identificacion" HeaderText="Identificación">
                                <ItemStyle HorizontalAlign="Left" Width="40px" />
                                <HeaderStyle HorizontalAlign="Center"/>
                            </asp:BoundField>                            
                            <asp:BoundField DataField="nomtipo_identificacion" HeaderText="Tipo Id.">
                                <ItemStyle HorizontalAlign="Left" Width="20px" />
                                <HeaderStyle HorizontalAlign="Center"/>
                            </asp:BoundField>
                            <asp:BoundField DataField="nombres" HeaderText="Nombres">
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Center"/>
                            </asp:BoundField>
                            <asp:BoundField DataField="apellidos" HeaderText="Apellidos">
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Center"/>
                            </asp:BoundField>
                            <asp:BoundField DataField="tipo_identificacion" HeaderText="Tipo Ident" Visible="false">
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="direccion" HeaderText="Dirección" />
                            <asp:BoundField DataField="telefono" HeaderText="Telefono" />
                            <asp:BoundField DataField="nomciudad_resid" HeaderText="Ciudad" />
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