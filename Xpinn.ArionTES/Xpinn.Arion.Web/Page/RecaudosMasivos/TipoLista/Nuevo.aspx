<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" Title=".: Xpinn - Tipo Lista:." %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>    
    <asp:MultiView ID="mvAplicar" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwLista" runat="server">
            <table border="0" cellpadding="5" cellspacing="0" width="100%" >
                <tr>
                    <td class="tdI" style="text-align:left;">
                    Tipo de Lista&nbsp;*&nbsp;<asp:RequiredFieldValidator ID="rfvtipolista" runat="server" ControlToValidate="txtTipoLista" ErrorMessage="Campo Requerido" SetFocusOnError="True" ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic"/><br />
                    <asp:TextBox ID="txtTipoLista" runat="server" CssClass="textbox" MaxLength="128" />
                    <asp:CheckBox ID="chkConsolidada" runat="server" AutoPostBack="true" Text="Ordenar Lista Consolidada" OnCheckedChanged="chkConsolidada_CheckedChanged"/> 
                    </td>
                    <td class="tdD">
                         
                    </td>
                </tr>
                <tr>
                    <td class="tdI" style="text-align:left">
                    Descripción&nbsp;*&nbsp;<asp:RequiredFieldValidator ID="rfvDescripcion" runat="server" ControlToValidate="txtDescripcion" ErrorMessage="Campo Requerido" SetFocusOnError="True" ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic"/><br />
                    <asp:TextBox ID="txtDescripcion" runat="server" CssClass="textbox" MaxLength="128" Width="519px" />
                    </td>
                    <td class="tdD">
                    </td>
                </tr>
                <tr>
                    <td class="tdI" style="text-align:left">
                        <strong>Líneas de Producto que Conforman el Tipo de Lista de Recaudos:</strong><br />
                        <asp:Button ID="btnDetalle" runat="server" CssClass="btn8" 
                            onclick="btnDetalle_Click" onclientclick="btnDetalle_Click" 
                            Text="+ Adicionar Detalle" />
                        <asp:GridView ID="gvProgramacion" runat="server" Width="80%" PageSize="20" ShowHeaderWhenEmpty="True"
                            AutoGenerateColumns="False" SelectedRowStyle-Font-Size="XX-Small" Style="font-size: small; margin-bottom: 0px;" 
                            OnRowDataBound="gvProgramacion_RowDataBound" 
                            OnSelectedIndexChanged="gvProgramacion_SelectedIndexChanged"
                            OnRowDeleting="gvProgramacion_RowDeleting" DataKeyNames="codtipo_lista_detalle">
                            <Columns>
                                <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/gr_elim.jpg" ShowDeleteButton="True">
                                    <ItemStyle HorizontalAlign="left" Width="3%" />
                                </asp:CommandField>
                                <asp:TemplateField HeaderText="Codigo" ItemStyle-HorizontalAlign="Center" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcodtipo_lista_detalle" runat="server" Text='<%# Bind("codtipo_lista_detalle") %>' />                                                       
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" Width="30%" />
                                </asp:TemplateField>                                            
                                <asp:TemplateField HeaderText="Tipo de Producto">
                                    <ItemTemplate>                                               
                                        <asp:Label ID="lbltipo" runat="server" Text='<%# Bind("tipo_producto") %>' Visible="false"></asp:Label>
                                        <cc1:DropDownListGrid ID="ddlTipo" runat="server" CommandArgument="<%#Container.DataItemIndex %>" CssClass="textbox" Width="90%"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlTipo_SelectedIndexChanged" ></cc1:DropDownListGrid>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="center" Width="10%" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Línea de Producto">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCodLinea" runat="server" Text='<%# Bind("cod_linea") %>' Visible="false"></asp:Label>
                                        <cc1:DropDownListGrid ID="ddlCodLinea" runat="server" CommandArgument="<%#Container.DataItemIndex %>" CssClass="textbox" Width="90%">
                                        </cc1:DropDownListGrid>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="center" Width="20%" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                            </Columns>
                            <HeaderStyle CssClass="gridHeader" />
                            <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                            <RowStyle CssClass="gridItem" />
                            <SelectedRowStyle Font-Size="XX-Small"></SelectedRowStyle>
                        </asp:GridView>
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
                            El Tipo de Lista de Recaudo fue
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
    </asp:MultiView>
    <asp:HiddenField ID="HiddenField1" runat="server" />        
    <uc4:mensajeGrabar ID="ctlMensaje" runat="server"/>
</asp:Content>