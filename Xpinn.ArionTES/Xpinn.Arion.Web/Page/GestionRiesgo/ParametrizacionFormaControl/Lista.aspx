<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Nuevo" Title=".: Xpinn - Grado de Automatizaciòn :." %>

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
            <table id="tbEncabezado" border="0" cellpadding="5" cellspacing="0" width="70%" style="text-align: left">
                <tr>
                    <td style="width: 100%; text-align: right"><br />
                        <asp:TextBox ID="txtCodigo" runat="server" CssClass="textbox" Visible="false" Width="90%" ReadOnly="true" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left">
                        <strong>Paràmetros Forma de Control</strong>
                        <hr style="width: 100%" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left">
                        <asp:UpdatePanel runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="gvLista" runat="server"  AutoGenerateColumns="False" Width="100%"
                                    DataKeyNames="cod_formacontrol" HeaderStyle-CssClass="gridHeader" OnRowDataBound="gvLista_RowDataBound"
                                    PagerStyle-CssClass="gridPager" PageSize="100" RowStyle-CssClass="gridItem"  OnDataBound="gvLista_OnDataBound"
                                    OnRowDeleting="gvLista_RowDeleting">
                                    <Columns>
                                        <asp:BoundField DataField="cod_formacontrol" HeaderText="Item" />
                                        <asp:BoundField DataField="cod_atributo" HeaderText="Cod." />
                                        <asp:BoundField DataField="atributo" HeaderText="Atributo" />
                                        <asp:BoundField DataField="cod_opcion" HeaderText="Cod.Opc" />
                                        <asp:BoundField DataField="opcion" HeaderText="Opciòn" />
                                        <asp:TemplateField HeaderText="Valor" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtValor" runat="server" Style="font-size: xx-small; text-align: left"
                                                             Text='<%# Bind("valor") %>' Width="100px" CssClass="textbox" Visible="true" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" Width="10%" />
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
        </asp:View>

    </asp:MultiView>
    <uc4:mensajegrabar ID="ctlMensaje" runat="server" />

</asp:Content>
