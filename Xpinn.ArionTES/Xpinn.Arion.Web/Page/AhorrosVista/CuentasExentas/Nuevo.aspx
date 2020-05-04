                                                                      <%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" EnableEventValidation="false"%>  

<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>
<%@ Register Src="~/General/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/ctlCuentasProductos.ascx" TagName="ListadoProductos" TagPrefix="uc1" %>
 
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
     <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>  
   
    <asp:MultiView ID="mvAplicar" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwLista" runat="server">
            <asp:Panel ID="pConsulta" runat="server">            
        <table cellpadding="5" cellspacing="0" style="width: 70%">
           
            <tr>
                <td style="height: 15px; text-align: left; font-size: x-small;" colspan="5">
                    <strong>Criterios de Búsqueda:</strong></td>
            </tr>
            <tr>
                <td style="height: 15px; text-align: left;">
                    Número Cuenta<br />
                    <asp:TextBox ID="txtCuenta" runat="server" CssClass="textbox" Width="119px"></asp:TextBox>
                </td>
                <td style="height: 15px; text-align: left;">
                    Producto<br />
                    <asp:DropDownList ID="ddlTipoProducto" runat="server" CssClass="textbox" 
                        Width="210px"  />
                    <br />
                </td>
                <td style="height: 15px; text-align: left;">
                    Identificación<br />
                    <asp:TextBox ID="txtIdentificacion" runat="server" 
                        CssClass="textbox"  Width="119px"></asp:TextBox>
                </td>
                <td colspan="2" style="height: 15px; text-align: left;">
                    <asp:Button ID="btnConsultarTodas" runat="server" Text="Consultar Todas" CssClass="btn8" Width="100px" OnClick="btnConsultarTodas_Click" />
                </td>
            </tr>
            <tr>
                <td class="tdI" style="text-align: left; width: 205px;">
                </td>
                <td class="tdI" style="text-align: left">
                </td>
                <td class="tdI" style="text-align: left">
                    &nbsp;</td>
                <td class="tdI" style="text-align: left">
                </td>
                <td class="tdI" style="text-align: left">
                </td>
            </tr>
        </table>
    </asp:Panel>

            <table style="text-align: center" width="100%" cellspacing="0" cellpadding="0">                                       
                <tr>                    
                    <td style="text-align: left; width: 100%;">
                        <div style="overflow: scroll; width: 100%; max-height:1100px">
                            <div style="width: 100%;">
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                    <ContentTemplate>
                                        <asp:Button ID="btnAdicionar" runat="server" CssClass="btn8"
                                            OnClick="btnAdicionar_Click"
                                            Text="+ Adicionar Detalle" />
                                        <asp:GridView ID="gvCuentas" runat="server" AutoGenerateColumns="False"
                                            DataKeyNames="idexenta" OnRowDataBound="gvCuentas_RowDataBound"
                                            OnRowDeleting="gvCuentas_RowDeleting" SelectedRowStyle-Font-Size="XX-Small" ShowHeaderWhenEmpty="True"
                                            Style="font-size: small; margin-bottom: 0px;">
                                            <Columns>
                                                <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/gr_elim.jpg" HeaderStyle-CssClass="gridIco"
                                                    ShowDeleteButton="True" />
                                                <asp:TemplateField HeaderText="Codigo" ItemStyle-HorizontalAlign="Center"
                                                    Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblidexenta" runat="server" Text='<%# Bind("idexenta") %>' />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="left"/>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Tipo de Cuenta"
                                                    ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="165px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCodTipoCuenta" runat="server" Text='<%# Bind("cod_tipo_cuenta") %>'
                                                            Visible="false"></asp:Label>
                                                        <cc1:DropDownListGrid ID="ddlTipoCuenta" runat="server" CssClass="textbox" Width="164px">
                                                        </cc1:DropDownListGrid>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Número de Cuenta" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="150px">
                                                    <ItemTemplate>
                                                        <table cellpadding="2" cellspacing="0">
                                                            <tr>
                                                                <td>
                                                                    <cc1:TextBoxGrid ID="txtNumeroCuenta" runat="server" CssClass="textbox" Width="115px" Text='<%# Bind("numero_cuenta") %>' />
                                                                </td>
                                                                <td>
                                                                    <cc1:ButtonGrid ID="btnNumeroCuenta" runat="server" CssClass="btn8" Style="padding: 1px 4px"
                                                                        Text="..." OnClick="btnNumeroCuenta_Click" CommandArgument='<%#((GridViewRow) Container).RowIndex %>' />
                                                                    <uc1:ListadoProductos ID="ctlListadoProductos" runat="server" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Línea" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="160px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblLinea" runat="server" Style="font-size: x-small"
                                                            Text='<%# Bind("nom_linea") %>' />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Identificación"
                                                    ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="100px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCodigoUsuario" runat="server"
                                                            Text='<%# Bind("cod_usuario") %>' Visible="false" />
                                                        <asp:Label ID="lblCodPersona" runat="server"
                                                            Text='<%# Bind("cod_persona") %>' Visible="false" />
                                                        <asp:Label ID="lblIdentificacion" runat="server" Style="font-size: x-small"
                                                            Text='<%# Bind("identificacion") %>' />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Nombre" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="185px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblNombre" runat="server" Style="font-size: x-small"
                                                            Text='<%# Bind("nombre") %>' />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Oficina" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="70px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOficina" runat="server" Style="font-size: x-small"
                                                            Text='<%# Bind("cod_oficina") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="F.Expedición" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="100px">
                                                    <ItemTemplate>
                                                        <ucFecha:fecha ID="txtFecha_Exenta" runat="server"
                                                            Text='<%# Eval("fecha_exenta", "{0:d}") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Monto" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="100px">
                                                    <ItemTemplate>
                                                        <uc1:decimales ID="txtMonto" runat="server" Text='<%# Bind("monto") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <HeaderStyle CssClass="gridHeader" />
                                            <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                                            <RowStyle CssClass="gridItem" />
                                        </asp:GridView>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </td>
                </tr>
            </table>
        </asp:View>
        <asp:View ID="vwFinal" runat="server">
                <asp:Panel id="PanelFinal" runat="server">
                <table style="width: 100%;">
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <br /><br /><br /><br />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large; color:Red">
                            La numeración fue
                            <asp:Label ID="lblmsj" runat="server"></asp:Label>
                            &nbsp;correctamente</td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <br /><br />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <asp:Button ID="btnFinal" runat="server" Text="Continuar" 
                                onclick="btnFinal_Click" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </asp:View>
        <asp:View ID="vwListadoCuentas" runat="server">
            <asp:GridView ID="gvLista" runat="server" AutoGenerateColumns="False" DataKeyNames="numero_cuenta" GridLines="Horizontal"  PageIndex="5" ShowHeaderWhenEmpty="True" Style="font-size: xx-small" Width="100%">
                <Columns>
                    <asp:TemplateField HeaderStyle-CssClass="gridIco" Visible="False">
                        <ItemTemplate>
                            <asp:ImageButton ID="btnInfo" runat="server" CommandName="Select" ImageUrl="~/Images/gr_info.jpg" ToolTip="Detalle" Width="16px" />
                        </ItemTemplate>
                        <HeaderStyle CssClass="gridIco" />
                    </asp:TemplateField>                
                                       
                    <asp:BoundField DataField="nom_tipocuenta" HeaderText="Tipo Producto">
                    <ItemStyle HorizontalAlign="Left" Width="50px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="numero_cuenta" HeaderText="No.Cuenta">
                    <ItemStyle HorizontalAlign="Left" Width="50px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="identificacion" HeaderText="Identificación">
                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                    </asp:BoundField>
                      <asp:BoundField DataField="nombre" HeaderText="Nombre">
                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                    </asp:BoundField>
                      <asp:BoundField DataField="cod_linea" HeaderText="Cod Linea">
                    <ItemStyle HorizontalAlign="Left" Width="50px" />
                    </asp:BoundField>
                 
                      <asp:BoundField DataField="nom_linea" HeaderText="Linea">
                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="cod_oficina" HeaderText="Cod Oficina">
                    <ItemStyle HorizontalAlign="Left" Width="50px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="fecha_exenta" HeaderText="fecha_exenta"  DataFormatString="{0:d}">
                    <ItemStyle HorizontalAlign="Left" Width="50px" />
                    </asp:BoundField>



                </Columns>
                <HeaderStyle CssClass="gridHeader" />
                <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                <RowStyle CssClass="gridItem" />
            </asp:GridView>
        </asp:View>
        <br />
    </asp:MultiView>

    <asp:HiddenField ID="HiddenField1" runat="server" /> 
    <uc4:mensajeGrabar ID="ctlMensaje" runat="server"/>
</asp:Content>