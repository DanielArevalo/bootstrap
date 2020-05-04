<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" %>  
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>    
    <asp:MultiView ID="mvAplicar" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwLista" runat="server">
            <table style="width: 80%" cellspacing="2" cellpadding="2">
                <tr>
                    <td colspan="2" style="text-align:left;">
                        <table width="100%">
                            <tr>
                                <td style="text-align: left; width: 50%">
                                    Banco:<br />
                                    <asp:DropDownList ID="ddlBancos" runat="server" CssClass="textbox" Width="90%" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <table cellpadding="0" cellspacing="0" style="width: 100%">
                <tr>
                    <td colspan="4" style="text-align: left; width: 100%">
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <asp:Button ID="btnAgregar" runat="server" CssClass="btn8" 
                                    OnClick="btnAgregar_Click" Text="+ Adicionar Detalle" />
                                <asp:GridView ID="gvDetalle" runat="server" PageSize="20" ShowHeaderWhenEmpty="True"
                                    AutoGenerateColumns="False" SelectedRowStyle-Font-Size="XX-Small" Style="font-size: small;
                                    margin-bottom: 0px;" OnRowDataBound="gvDetalle_RowDataBound" OnRowDeleting="gvDetalle_RowDeleting" 
                                    DataKeyNames="cod_banco">
                                    <Columns>
                                        <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/gr_elim.jpg" ShowDeleteButton="True">
                                        <ItemStyle HorizontalAlign="center" />
                                        </asp:CommandField>
                                        <asp:TemplateField HeaderText="Codigo" ItemStyle-HorizontalAlign="Center" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblcod_estructura_detalle" runat="server" Text='<%# Bind("cod_concepto") %>' />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="codigo" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblcodigo_campo" runat="server" Text='<%# Bind("cod_concepto") %>'
                                                    Visible="false">
                                                </asp:Label>
                                                <cc1:TextBoxGrid ID="txtcod_campo" runat="server" CommandArgument="<%#Container.DataItemIndex %>" Text='<%# Bind("cod_concepto") %>'
                                                    CssClass="textbox" Width="160px">
                                                </cc1:TextBoxGrid>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Descripción">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtdescripcion" runat="server" CssClass="textbox" Style="font-size: xx-small;
                                                    text-align: left" Text='<%# Bind("descripcion") %>' Width="90px" ></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Tipo Concepto">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="ddlconcepto"  DataTextField="tipo_concepto" DataValueField="tipo_concepto" runat="server" CssClass="textbox" Style="font-size: xx-small;
                                                    text-align: left"  Width="180px" >
                                                    <asp:ListItem Value="0">Seleccione Un Item</asp:ListItem>
                                                    <asp:ListItem Value="1">CHEQUE</asp:ListItem>
                                                    <asp:ListItem Value="2">CONSIGNACION</asp:ListItem>
                                                    <asp:ListItem Value="3">NOTA DEBITO</asp:ListItem>
                                                    <asp:ListItem Value="4">NOTA CREDITO</asp:ListItem>
                                                    </asp:DropDownList>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="center"/>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText=" ">
                                            <ItemTemplate>
                                                <asp:Label ID="lblconceptobancario" Visible="false" runat="server" Text='<%# Bind("id_conceptobancario") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="center" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <HeaderStyle CssClass="gridHeader" />
                                    <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                                    <RowStyle CssClass="gridItem" />
                                    <SelectedRowStyle Font-Size="XX-Small"></SelectedRowStyle>
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <asp:Label ID="lblTotalReg" runat="server" Visible="False" />
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
                        <td style="text-align: center; font-size: large;">
                            Se ha
                            <asp:Label ID="lblmsj" runat="server"></asp:Label>
                            &nbsp;correctamente la Estructura</td>
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