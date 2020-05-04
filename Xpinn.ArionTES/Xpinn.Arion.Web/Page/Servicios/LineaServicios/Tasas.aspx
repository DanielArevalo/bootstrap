<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Tasas.aspx.cs" Inherits="Nuevo" Title=".: Xpinn - LineasCredito :." %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/ctlTasa.ascx" TagName="tasa" TagPrefix="uc1" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <asp:UpdatePanel runat="server" RenderMode="Inline" UpdateMode="Conditional">
    <ContentTemplate>
    
        <table border="0" cellpadding="5" cellspacing="0" width="100%">
            <tr>
                <td style="text-align: left">
                    Linea de Servicio
                </td>
                <td style="text-align: left">
                    <asp:Label ID="lblCodLineaServicio" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="text-align: left">
                    Descripción Grupo<br />
                </td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtNombreGrupo" runat="server" CssClass="textbox" MaxLength="128"
                        Width="234px" />
                    &nbsp;
                    <asp:Label ID="lblCodRango" runat="server" Height="16px" Visible="False" />
                </td>
            </tr>
        </table>
        <table width="100%">
            <tr>
                <td style="text-align:left;width:50%">
                    <asp:Button ID="btnDetalleTopes" runat="server" CssClass="btn8" OnClick="btnDetalleTopes_Click" Text="+ Adicionar Detalle" />
                    <br />
                    <asp:GridView ID="gvTopes" runat="server" AutoGenerateColumns="False" BackColor="White"
                        BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="0" ForeColor="Black"
                        OnRowDataBound="gvTopes_RowDataBound" ShowFooter="True" Style="font-size: xx-small;
                        margin-right: 0px;" DataKeyNames="codtope" OnRowDeleting="gvTopes_RowDeleting">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Label ID="lbltope" runat="server" Text='<%# Bind("codtope") %>' Visible="False"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/gr_elim.jpg" ShowDeleteButton="True" />
                            <asp:TemplateField HeaderText="Descripcion" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lbldescripciontope" runat="server" Text='<%# Bind("tipo_tope") %>'
                                        Visible="False"></asp:Label>
                                    <cc1:DropDownListGrid ID="ddlDescrpTope" runat="server" AppendDataBoundItems="True"
                                        AutoPostBack="True" CommandArgument="<%#Container.DataItemIndex %>" CssClass="textbox"
                                        Style="font-size: xx-small; text-align: left" Width="120px">
                                    </cc1:DropDownListGrid>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="120px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Tope Minimo" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:TextBox ID="txttopeminimo" runat="server" CssClass="textbox" Style="font-size: xx-small;
                                        text-align: left" Text='<%# Bind("minimo") %>' Width="100px"></asp:TextBox>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" Width="100px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Tope Maximo" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:TextBox ID="txttopemaximo" runat="server" CssClass="textbox" Style="font-size: xx-small;
                                        text-align: left" Text='<%# Bind("maximo") %>' Width="100px"></asp:TextBox>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle CssClass="gridHeader" />
                        <HeaderStyle CssClass="gridHeader" />
                        <RowStyle CssClass="gridItem" />
                    </asp:GridView>
                </td>
                <td style="text-align: left; width: 50%; vertical-align: top">
                    <strong>Tasa de Intereses:</strong><asp:Label ID="lblCodAtrSer" runat="server" Visible="false" />
                    <br />
                    <uc1:tasa ID="ctlTasa" runat="server" />
                </td>
            </tr>
        </table>
    </ContentTemplate>
    </asp:UpdatePanel>
    

    <script type="text/javascript" language="javascript">
        function SetFocus() {
            document.getElementById('cphMain_txtCOD_LINEA_CREDITO').focus();
        }
        window.onload = SetFocus;
    </script>
</asp:Content>
