<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" Title=".: Xpinn - Tipo de Cupo :." %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/ctlNumero.ascx" TagName="numero" TagPrefix="uc2" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <br />
    <script type="text/javascript" src="../../../Scripts/gridviewScroll.min.js"></script>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
        <Scripts>
            <asp:ScriptReference Path="../../../Scripts/PopUp.js" />
        </Scripts>
    </asp:ScriptManager>
    <asp:Panel ID="pnlLoading" runat="server" Width="200" Height="100" HorizontalAlign="Center"
        CssClass="ModalPopup" EnableViewState="false" Style="display: none">
        <asp:Image ID="Image1" ImageUrl="../../../Images/loading.gif" runat="server" />
        <br />
        .........               
    </asp:Panel>
    <asp:Button ID="btnLoading" runat="server" Style="display: none" />

    <asp:MultiView ID="mvPresupuesto" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwEncabezado" runat="server">
            <table id="tbEncabezado" border="0" cellpadding="5" cellspacing="0" width="100%" style="text-align: left">
                <tr>
                    <td class="tdD" style="width: 120px; text-align: left">Código<br />
                        <asp:TextBox ID="txtCodigo" runat="server" CssClass="textbox" Width="50%" ReadOnly="true" />
                    </td>
                    <td class="tdD" style="width: 154PX; text-align: left">Descripción<br />
                        <asp:TextBox ID="txtDescripcion" runat="server" CssClass="textbox"
                            Width="78%" />
                    </td>
                    <td class="tdD" style="width: 228px; text-align: left" >
                        <asp:CheckBox ID="chkRestar" runat="server" Width="100%" Text="Restar Saldos de Crèditos"  TabIndex="23"></asp:CheckBox>
                    </td>
                    <td class="tdD" style="width: 280px; text-align: left" />
                </tr>
                <tr>
                    <td colspan="3" style="text-align: left">
                        <strong>Variables</strong>
                        <hr style="width: 100%" />
                    </td>
                    <td class="tdD" style="width: 280px; text-align: left" />
                </tr>
                <tr>
                    <td colspan="3" style="text-align: left">
                        <asp:UpdatePanel runat="server">
                            <ContentTemplate>
                                <asp:Button ID="btnDetalle" runat="server" CssClass="btn8" OnClick="btnDetalle_Click"
                                    OnClientClick="btnDetalle_Click" Text="+ Adicionar Detalle" />
                                <asp:GridView ID="gvLista" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                    DataKeyNames="iddetalle" HeaderStyle-CssClass="gridHeader" OnRowDataBound="gvLista_RowDataBound"
                                    PagerStyle-CssClass="gridPager" PageSize="20" RowStyle-CssClass="gridItem" Style="font-size: x-small"
                                    GridLines="Horizontal" OnRowDeleting="gvLista_RowDeleting">
                                    <Columns>
                                        <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/gr_elim.jpg" ShowDeleteButton="True" />
                                        <asp:TemplateField HeaderText="Codigo" ItemStyle-HorizontalAlign="Center" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblcodigo" runat="server" Text='<%# Bind("iddetalle") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" Width="120px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Descripción">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCondicion" runat="server" Text='<%# Bind("idvariable") %>' Visible="false"></asp:Label>
                                                <cc1:DropDownListGrid ID="ddlCondicion" runat="server" AppendDataBoundItems="True" CommandArgument='<%#Container.DataItemIndex %>'
                                                    CssClass="textbox" Style="font-size: xx-small; text-align: left" Width="120px">
                                                    <asp:ListItem Text="(Seleccione un Item)" Value="0" />
                                                    <asp:ListItem Text="Aportes" Value="1" />
                                                    <asp:ListItem Text="Ahorro Permanente" Value="2" />
                                                    <asp:ListItem Text="Ingresos" Value="3" />
                                                </cc1:DropDownListGrid>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" Width="200px" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Valor" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtValor" runat="server" Style="font-size: xx-small; text-align: left"
                                                    Text='<%# Bind("valor") %>' Width="130px" CssClass="textbox" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="center" Width="130px" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        
                                    </Columns>
                                    <HeaderStyle CssClass="gridHeader" />
                                    <PagerStyle CssClass="gridPager" />
                                    <RowStyle CssClass="gridItem" />
                                </asp:GridView>
                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="gvLista" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                    <td class="tdD" style="width: 280px; text-align: left" />
                </tr>
            </table>
        </asp:View>

    </asp:MultiView>
    <uc4:mensajegrabar ID="ctlMensaje" runat="server" />

</asp:Content>
