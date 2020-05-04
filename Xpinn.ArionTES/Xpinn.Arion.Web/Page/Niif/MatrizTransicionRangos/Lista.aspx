<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" %>  
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>    
    <asp:MultiView ID="mvAplicar" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwLista" runat="server">
            <table style="width: 600px ; text-align : center" cellspacing="0" cellpadding="0">
                <tr>
                    <td style="text-align: left; width: 100%" >                      
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                <ContentTemplate>
                                 <asp:Button ID="btnAdicionar" runat="server" CssClass="btn8" 
                                onclick="btnAdicionar_Click" onclientclick="btnAdicionar_Click" 
                                Text="+ Adicionar Rangos" />
                                    <asp:GridView ID="gvLista" runat="server" PageSize="20" ShowHeaderWhenEmpty="True"
                                        AutoGenerateColumns="False" SelectedRowStyle-Font-Size="XX-Small" Style="font-size: small;
                                        margin-bottom: 0px;"
                                        OnRowDeleting="gvLista_RowDeleting" DataKeyNames="codrango">
                                        <Columns>
                                            <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/gr_elim.jpg" ShowDeleteButton="True">
                                            <ItemStyle HorizontalAlign="left"/>
                                            </asp:CommandField>
                                              <asp:TemplateField HeaderText="Codigo" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtCodigo" runat="server" CssClass="textbox" Width="100px" Text='<%# Bind("codrango") %>' />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left"/>
                                            </asp:TemplateField>                                            
                                            <asp:TemplateField HeaderText="Nombre" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtNombre" runat="server" CssClass="textbox" Width="250px" Text='<%# Bind("descripcion") %>'/>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="center"/>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Días Mínimo">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtDiasMin" runat="server" CssClass="textbox" Width="100px" MaxLength="3"
                                                    Text='<%# Bind("dias_minimo") %>' style="text-align:right"/>
                                                     <asp:FilteredTextBoxExtender ID="fte1" runat="server"
                                    Enabled="True" FilterType="Numbers, Custom" TargetControlID="txtDiasMin" ValidChars="" />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center"/>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Días Máximo">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtDiasMax" runat="server" CssClass="textbox" Width="100px" MaxLength="4"
                                                    Text='<%# Bind("dias_maximo") %>' style="text-align:right"/>
                                                     <asp:FilteredTextBoxExtender ID="fte2" runat="server"
                                    Enabled="True" FilterType="Numbers, Custom" TargetControlID="txtDiasMax" ValidChars="" />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="center"/>
                                            </asp:TemplateField>
                                        </Columns>
                                        <HeaderStyle CssClass="gridHeader" />
                                        <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                                        <RowStyle CssClass="gridItem" />
                                        <SelectedRowStyle Font-Size="XX-Small"></SelectedRowStyle>
                                    </asp:GridView>
                                </ContentTemplate>
                                </asp:UpdatePanel>                                       
                    </td>                    
                </tr>
                <tr>
                <td style="text-align:center">
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
                        <td style="text-align: center; font-size: large; color:Red">
                            Número de Registros Grabados :
                            <asp:Label ID="lblmsj" runat="server"></asp:Label>
                            <br />
                            Número de Registros Modificados :
                            <asp:Label ID="lblmsj1" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <br /><br />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            &nbsp;</td>
                    </tr>
                </table>
            </asp:Panel>
        </asp:View>
    </asp:MultiView>

    <asp:HiddenField ID="HiddenField1" runat="server" />    
     
     <uc4:mensajeGrabar ID="ctlMensaje" runat="server"/>
</asp:Content>