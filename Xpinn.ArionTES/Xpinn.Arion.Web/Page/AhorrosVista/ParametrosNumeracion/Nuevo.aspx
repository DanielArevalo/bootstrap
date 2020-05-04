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
            <table style="width: 90%; text-align: center" cellspacing="0" cellpadding="0">
                <tr>
                    <td style="text-align: left; width: 100%">
                        <hr style="width: 100%" />
                        <br />
                    </td>
                </tr>
                <tr>                    
                    <td style="text-align: left">
                        <strong>Tipo de Producto</strong>
                        <br />
                        <asp:DropDownList ID="ddlTipoCuenta" runat="server" AutoPostBack="True" 
                            CssClass="textbox" onselectedindexchanged="ddlTipoCuenta_SelectedIndexChanged1" 
                            ReadOnly="True" Width="180px" />
                    </td>
                </tr>
                <tr>                        
                    <td style="text-align: left; width: 100%;">
                        <div style="overflow: scroll; width: 100%; max-height: 500px">
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                <ContentTemplate>
                                    <asp:Button ID="btnProgramacion" runat="server" CssClass="btn8" OnClick="btnProgramacion_Click"
                                        OnClientClick="btnProgramacion_Click" Text="+ Adicionar Detalle" />
                                    <asp:GridView ID="gvProgramacion" runat="server" AutoGenerateColumns="False" DataKeyNames="idconsecutivo"
                                        OnRowDataBound="gvProgramacion_RowDataBound" OnRowDeleting="gvProgramacion_RowDeleting"
                                        Style="font-size: small; margin-bottom: 0px;">
                                        <Columns>
                                            <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/gr_elim.jpg" ShowDeleteButton="True">
                                                <ItemStyle HorizontalAlign="left" />
                                            </asp:CommandField>
                                            <asp:TemplateField HeaderText="Consecutivo" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="idconsecutivo" runat="server" Text='<%# Bind("idconsecutivo") %>' />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Posición" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtPosicion" runat="server" MaxLength="2" CssClass="textbox" Width="35px"
                                                        AutoPostBack="True" OnTextChanged="TxtPosicion_TextChanged" Text='<%# Bind("posicion") %>'></asp:TextBox>
                                                    <asp:FilteredTextBoxExtender ID="ftePosicion" runat="server" TargetControlID="TxtPosicion"
                                                        Enabled="True" FilterType="Numbers, Custom" ValidChars="-" />
                                                    &nbsp;
                                                    <asp:Label ID="lblerror" runat="server" ForeColor="#CC0000" Text="Esta posición ya existe"
                                                        Visible="False"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Tipo">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbltipo" runat="server" Text='<%# Bind("tipo_campo") %>' Visible="false"></asp:Label>
                                                    <cc1:DropDownListGrid ID="ddlTipo" runat="server" AutoPostBack="True" CommandArgument="<%#Container.DataItemIndex %>"
                                                        CssClass="textbox" OnSelectedIndexChanged="ddlTipo_SelectedIndexChanged" Width="200px">
                                                    </cc1:DropDownListGrid>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Valor">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtValores" runat="server" MaxLength="10" Text='<%# Bind("valor") %>'
                                                        CssClass="textbox"></asp:TextBox>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Longitud">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtLongitud" runat="server" AutoPostBack="True" CssClass="textbox"
                                                        MaxLength="2" Width="27px" OnTextChanged="TxtLongitud_TextChanged" Text='<%# Bind("longitud") %>'></asp:TextBox>
                                                    <asp:FilteredTextBoxExtender ID="fteLongitud" runat="server" TargetControlID="TxtLongitud"
                                                        Enabled="True" FilterType="Numbers, Custom" ValidChars="" />
                                                    &nbsp;
                                                    <br />
                                                    <asp:Label ID="lblerrorrango" runat="server" ForeColor="#CC0000" Text="Debe estar entre 1 y  20 "
                                                        Visible="False"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Alinear">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblalinear" runat="server" Text='<%# Bind("alinear") %>' Visible="False" />
                                                    <cc1:DropDownListGrid ID="ddlAlinear" runat="server" CommandArgument="<%#Container.DataItemIndex %>"
                                                        CssClass="textbox" Width="139px">
                                                    </cc1:DropDownListGrid>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Carácter de Llenado">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtCaracter" runat="server" CssClass="textbox" MaxLength="1" Width="69px"
                                                        Text='<%# Bind("caracter_llenado") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <HeaderStyle CssClass="gridHeader" />
                                        <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                                        <RowStyle CssClass="gridItem" />
                                        <SelectedRowStyle Font-Size="XX-Small" />
                                    </asp:GridView>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <center><asp:Label ID="lblTotalReg" runat="server" Visible="False" /></center>
                    </td>                  
                </tr>
                <tr>                    
                    <td style="text-align: left; width: 100%;">
                        <hr style="width: 100%" />
                        <br />
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
                                onclick="btnFinal_Click" style="height: 26px" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </asp:View>
    </asp:MultiView>

    <asp:HiddenField ID="HiddenField1" runat="server" />    
     
    <uc4:mensajeGrabar ID="ctlMensaje" runat="server"/>
</asp:Content>