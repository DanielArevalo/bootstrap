<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Nuevo" Title=".: Xpinn - Matriz de Calor :." %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../../../General/Controles/fecha.ascx" TagName="fecha" TagPrefix="uc1" %>
<%@ Register Src="../../../General/Controles/ctlNumero.ascx" TagName="numero" TagPrefix="uc2" %>
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

    <asp:Button ID="btnLoading" runat="server" Style="display: none" />

    <asp:MultiView ID="mvPresupuesto" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwEncabezado" runat="server">
            <%--        <asp:UpdatePanel ID="UpdatePanel1" runat="server" ChildrenAsTriggers="true">
            <ContentTemplate>         --%>
            <table id="tbEncabezado" border="0" cellpadding="5" cellspacing="0" width="700px" style="text-align: left">
                <tr>
                    <td  style="width: 120px; text-align: left"><br />
                        <asp:TextBox ID="txtCodigo" runat="server" CssClass="textbox" Visible="false" Width="90%" ReadOnly="true" />
                    </td>
                    <td style="width: 280px; text-align: left"></td>
                </tr>
                <tr>
                    <td colspan="3" style="text-align: left">
                        <strong>Matriz</strong>
                        <hr style="width: 100%" />
                    </td>
                </tr>
                <tr>
                    <td colspan="3" style="text-align: left">
                        <asp:UpdatePanel runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="gvLista" runat="server"  AutoGenerateColumns="False"
                                    DataKeyNames="cod_impacto" HeaderStyle-CssClass="gridHeader" OnRowDataBound="gvLista_RowDataBound"
                                    PagerStyle-CssClass="gridPager" PageSize="20" RowStyle-CssClass="gridItem" Style="font-size: x-small"
                                    GridLines="Horizontal" OnRowDeleting="gvLista_RowDeleting">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Probabilidad" ItemStyle-HorizontalAlign="Center" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="lblProbabilidad" runat="server" Text='<%# Bind("cod_probabilidad") %>' Visible="false"></asp:Label>
                                                <cc1:DropDownListGrid 
                                                    ID="dllProbabilidad" runat="server" AppendDataBoundItems="True" disabled="true" CommandArgument='<%#Container.DataItemIndex %>'
                                                    CssClass="textbox" Style="font-size: xx-small; text-align: left" Width="120px">
                                                </cc1:DropDownListGrid>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" Width="120px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Imapacto" ItemStyle-HorizontalAlign="Center" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="lblImpacto" runat="server" Text='<%# Bind("cod_impacto") %>' Visible="false"></asp:Label>
                                                <cc1:DropDownListGrid 
                                                    ID="dllImpacto" runat="server" AppendDataBoundItems="True" disabled="true" CommandArgument='<%#Container.DataItemIndex %>'
                                                    CssClass="textbox" Style="font-size: xx-small; text-align: left" Width="120px">
                                                </cc1:DropDownListGrid>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" Width="120px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Calificación" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtCalificacion" runat="server" Style="font-size: xx-small; text-align: left"
                                                             Text='<%# Bind("calificacion") %>' Width="130px" CssClass="textbox" Visible="true" />
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
                </tr>

            </table>

            <%--    </ContentTemplate>
        </asp:UpdatePanel>--%>
        </asp:View>

    </asp:MultiView>
    <uc4:mensajegrabar ID="ctlMensaje" runat="server" />

</asp:Content>
