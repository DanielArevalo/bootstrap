<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Rangos.aspx.cs" Inherits="Nuevo" Title=".: Xpinn - LineasCredito :." %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/tnumero.ascx" TagName="txtPesos" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/ctlDecimales.ascx" TagName="decimales" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/decimalesGridRow.ascx" TagName="decimalesGridRow" TagPrefix="uc1" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <script type="text/javascript" language="javascript">
        function SetFocus() {
            document.getElementById('cphMain_ddlAtributos').focus();
        }
        window.onload = SetFocus;
        function Forzar() {
            __doPostBack('', '');
        }
    </script>
    <asp:MultiView ID="mvParametrosAtributos" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwRangos" runat="server">
            <asp:UpdatePanel runat="server" ChildrenAsTriggers="true">
                <ContentTemplate>
                    <table border="0" cellpadding="5" cellspacing="0" width="80%" style="margin-bottom: 0px">
                        <tr><br/>
                            <td style="text-align: Center;">
                                <asp:Label ID="lblAtributo" runat="server" Text="Atributo"></asp:Label>
                                <br />
                                <asp:DropDownList ID="ddlAtributos" runat="server" AutoPostBack="True" CssClass="textbox" />
                            </td>
                            <asp:Panel ID="panelValores" runat="server" Visible="false">
                                <td style="text-align: Center;">
                                    <asp:Label ID="lblTipoTope" runat="server" Text="Tipo de Tope"></asp:Label><br />
                                    <asp:DropDownList ID="ddlTipoTope" runat="server" AutoPostBack="True" CssClass="textbox">
                                        <asp:ListItem Text="Seleccione un item" Selected="True" />
                                        <asp:ListItem Text="Valor" Value="0" />
                                        <asp:ListItem Text="SMLV" Value="1" />
                                        <asp:ListItem Text="Edad en años" Value="2" />
                                    </asp:DropDownList>
                                </td>
                                <td style="text-align: Center;">
                                    <asp:Label ID="lblTipoValor" runat="server" Text="Tipo de Valor"></asp:Label><br />
                                    <asp:DropDownList ID="ddlTipoValor" runat="server" AutoPostBack="True" CssClass="textbox">
                                        <asp:ListItem Text="Seleccione un item" Selected="True" />
                                        <asp:ListItem Text="Valor" Value="1" />
                                        <asp:ListItem Text="Porcentaje" Value="2" />
                                    </asp:DropDownList>
                                </td>
                            </asp:Panel>
                        </tr>
                    </table>
                    <table border="0" cellpadding="5" cellspacing="0" width="80%">
                        <tr>
                            <td style="align-items: center;">
                                <br />
                                <asp:Button ID="btnDetalle" runat="server" CssClass="btn8" OnClick="btnDetalle_Click"
                                    OnClientClick="btnDetalle_Click" Text="+ Adicionar Detalle" Visible="false" />
                                <asp:GridView ID="gvRangos" runat="server" AutoGenerateColumns="False" BackColor="White"
                                    BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="0" ForeColor="Black"
                                    OnRowDeleting="gvRangos_RowDeleting" HorizontalAlign="Center"
                                    ShowFooter="True" Style="font-size: small;" Width="80%" Visible="false"
                                    PageSize="2">
                                    <AlternatingRowStyle BackColor="White" HorizontalAlign="Center" />
                                    <Columns>
                                        <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/gr_elim.jpg" ShowDeleteButton="True" DeleteText="Eliminar" />
                                        <asp:TemplateField HeaderText="Desde" ItemStyle-HorizontalAlign="center">
                                            <ItemTemplate>
                                                <span>
                                                    <asp:TextBox ID="txtDesde" runat="server" CssClass="textbox" Style="font-size: xx-small; text-align: center"
                                                        Text='<%# Bind("minimo") %>' Width="100px"></asp:TextBox>
                                                    <asp:FilteredTextBoxExtender ID="fte1" runat="server" FilterType="Numbers"
                                                        ValidChars=",." TargetControlID="txtDesde" />
                                                </span>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Hasta" ItemStyle-HorizontalAlign="center">
                                            <ItemTemplate>
                                                <span>
                                                    <asp:TextBox ID="txtHasta" runat="server" CssClass="textbox" Style="font-size: xx-small; text-align: center"
                                                        Text='<%# Bind("maximo") %>' Width="100px"></asp:TextBox>
                                                    <asp:FilteredTextBoxExtender ID="fte2" runat="server" FilterType="Numbers"
                                                        ValidChars=",." TargetControlID="txtHasta" />
                                                </span>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Valor" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <span>
                                                    <uc1:decimales ID="txtValor" runat="server" Text='<%# Eval("valor") %>' style="text-align: center"
                                                        Habilitado="True" AutoPostBack_="True" Enabled="True" Width_="100" />
                                                    <%--<asp:TextBox ID="txtValor" runat="server" CssClass="textbox" Style="font-size: xx-small; text-align: center"
                                                        Text='<%# Bind("valor") %>' Width="100px"></asp:TextBox>
                                                    <asp:FilteredTextBoxExtender ID="fte3" runat="server" FilterType="Custom,Numbers"
                                                        ValidChars=",." TargetControlID="txtValor" />--%>
                                                </span>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle CssClass="gridHeader" />
                                    <HeaderStyle CssClass="gridHeader" />
                                    <RowStyle CssClass="gridItem" />
                                    <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                    <SortedAscendingCellStyle BackColor="#FBFBF2" />
                                    <SortedAscendingHeaderStyle BackColor="#848384" />
                                    <SortedDescendingCellStyle BackColor="#EAEAD3" />
                                    <SortedDescendingHeaderStyle BackColor="#575357" />
                                </asp:GridView>
                                <br />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:View>
        <asp:View ID="vwMensaje" runat="server">
            <asp:Panel runat="server">
                <table style="width: 100%;">
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <br />
                            <br />
                            <br />
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <asp:Label ID="lblMensajeGrabar" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <br />
                            <br />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </asp:View>
    </asp:MultiView>
</asp:Content>
