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
                    <td style="width: 120px; text-align: left"><br />
                        <asp:TextBox ID="txtCodigo" runat="server" Visible="false" CssClass="textbox" Width="90%" ReadOnly="true" />
                    </td>
                    <td style="width: 280px; text-align: left"></td>
                </tr>
                <tr>
                    <td colspan="3" style="text-align: left">
                        <hr style="width: 100%" />
                    </td>
                </tr>
                <tr>
                    <td colspan="3" style="text-align: left">
                        <asp:UpdatePanel runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="gvLista" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                    DataKeyNames="cod_variable" HeaderStyle-CssClass="gridHeader" OnRowDataBound="gvLista_RowDataBound"
                                    PagerStyle-CssClass="gridPager" PageSize="20" RowStyle-CssClass="gridItem" Style="font-size: x-small"
                                    GridLines="Horizontal" OnRowDeleting="gvLista_RowDeleting">
                                    <Columns>
                                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblcodigoVariable" runat="server" Text='<%# Bind("cod_variable") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" Width="120px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Factor Riesgo" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtNomVariable" runat="server" Style="font-size: xx-small; text-align: left"
                                                             Text='<%# Bind("nombre_variable") %>' Width="130px" CssClass="textbox" Enabled="false" Visible="true" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="center" Width="130px" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Riesgo Bajo" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtRiesgoBajo" runat="server" Style="font-size: xx-small; text-align: left"
                                                             Text='<%# Bind("riesgo_bajo") %>' Width="130px" CssClass="textbox" Visible="true" />
                                                <asp:FilteredTextBoxExtender ID="ftb12" runat="server" Enabled="True" FilterType="Numbers, Custom"
                                                    TargetControlID="txtRiesgoBajo" ValidChars="-()" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="center" Width="130px" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Riesgo Moderado" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtRiesgoModerado" runat="server" Style="font-size: xx-small; text-align: left"
                                                    Text='<%# Bind("riesgo_moderado") %>' Width="130px" CssClass="textbox" Visible="true"/>
                                                <asp:FilteredTextBoxExtender ID="ftb13" runat="server" Enabled="True" FilterType="Numbers, Custom"
                                                    TargetControlID="txtRiesgoModerado" ValidChars="-()" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="center" Width="130px" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Riesgo Alto" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtRiesgoAlto" runat="server" Style="font-size: xx-small; text-align: left"
                                                    Text='<%# Bind("riesgo_alto") %>' Width="130px" CssClass="textbox" Visible="true"/>
                                                <asp:FilteredTextBoxExtender ID="ftb14" runat="server" Enabled="True" FilterType="Numbers, Custom"
                                                    TargetControlID="txtRiesgoAlto" ValidChars="-()" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="center" Width="130px" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Riesgo Extremo" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtRiesgoExtremo" runat="server" Style="font-size: xx-small; text-align: left"
                                                    Text='<%# Bind("riesgo_extremo") %>' Width="130px" CssClass="textbox" Visible="true"/>
                                                <asp:FilteredTextBoxExtender ID="ftb15" runat="server" Enabled="True" FilterType="Numbers, Custom"
                                                    TargetControlID="txtRiesgoExtremo" ValidChars="-()" />
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
