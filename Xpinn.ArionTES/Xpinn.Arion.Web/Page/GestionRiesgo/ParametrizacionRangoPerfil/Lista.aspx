<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Nuevo" Title=".: Xpinn - Segmentacion :." %>

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


    <asp:Panel ID="pnlLoading" runat="server" Width="200" Height="100" HorizontalAlign="Center"
        CssClass="ModalPopup" EnableViewState="false" Style="display: none">
        <asp:Image ID="Image1" ImageUrl="../../../Images/loading.gif" runat="server" />
        <br />
        Generando el Presupuesto Ejecutado...        
    </asp:Panel>
    <asp:Button ID="btnLoading" runat="server" Style="display: none" />

    <asp:MultiView ID="mvPresupuesto" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwEncabezado" runat="server">
            <%--        <asp:UpdatePanel ID="UpdatePanel1" runat="server" ChildrenAsTriggers="true">
            <ContentTemplate>         --%>
            <table id="tbEncabezado" border="0" cellpadding="5" cellspacing="0" width="700px" style="text-align: left">
                <tr>
                    <td style="width: 120px; text-align: left">Código rango<br />
                        <asp:TextBox ID="txtCodigo" runat="server" CssClass="textbox" Width="90%" ReadOnly="true" />
                    </td>
                    <td style="width: 280px; text-align: left"></td>
                </tr>
                <tr>
                    <td colspan="3" style="text-align: left">
                        <strong>Condiciones de Clientes</strong>
                        <hr style="width: 100%" />
                    </td>
                </tr>
                <tr>
                    <td colspan="3" style="text-align: left">
                        <asp:UpdatePanel runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="gvLista" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                    DataKeyNames="cod_rango_perfil" HeaderStyle-CssClass="gridHeader" OnRowDataBound="gvLista_RowDataBound"
                                    PagerStyle-CssClass="gridPager" PageSize="20" RowStyle-CssClass="gridItem" Style="font-size: x-small"
                                    GridLines="Horizontal" OnRowDeleting="gvLista_RowDeleting">
                                    <Columns>
                                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblcodigoRango" runat="server" Text='<%# Bind("cod_rango_perfil") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" Width="120px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Calificación">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCalificacion" runat="server" Text='<%# Bind("calificacion") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" Width="120px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Rango minimo" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtRangoMinimo" runat="server" Style="font-size: xx-small; text-align: left"
                                                             Text='<%# Bind("rango_minimo") %>' Width="130px" CssClass="textbox" Visible="true" />
                                                <asp:FilteredTextBoxExtender ID="ftb12" runat="server" Enabled="True" FilterType="Numbers, Custom"
                                                    TargetControlID="txtRangoMinimo" ValidChars="-()" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="center" Width="130px" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Rango maximo" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtRangoMaximo" runat="server" Style="font-size: xx-small; text-align: left"
                                                    Text='<%# Bind("rango_maximo") %>' Width="130px" CssClass="textbox" Visible="true"/>
                                                <asp:FilteredTextBoxExtender ID="ftb13" runat="server" Enabled="True" FilterType="Numbers, Custom"
                                                    TargetControlID="txtRangoMaximo" ValidChars="-()" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="center" Width="130px" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Monitoreo" ItemStyle-HorizontalAlign="Center" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="lblMonitoreo" runat="server" Text='<%# Bind("cod_monitoreo") %>' Visible="false"></asp:Label>
                                                <cc1:DropDownListGrid 
                                                    ID="ddlMonitoreo" runat="server" AppendDataBoundItems="True" CommandArgument='<%#Container.DataItemIndex %>'
                                                    CssClass="textbox" Style="font-size: xx-small; text-align: left" Width="120px">
                                                </cc1:DropDownListGrid>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" Width="120px" />
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
