<%@ Page Title=".: Xpinn - Flujo de Caja :." Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" %>

<%@ Register Src="../../../General/Controles/fecha.ascx" TagName="fecha" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:MultiView ID="mvPrincipal" runat="server" ActiveViewIndex="0">
        <asp:View ID="vConsulta" runat="server">
            <asp:Panel ID="pConsulta" runat="server">
                <div id="gvDiv" runat="server">
                    <table id="tbCriterios" border="0" cellpadding="0" cellspacing="0" width="70%" runat="server">
                        <tr>
                            <td class="tdD" style="text-align: center; width: 100px">
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdD" style="text-align: center; width: 100px">Fecha Inicio</td>
                            <td class="tdD" style="text-align: center; width: 100px">Fecha Final</td>
                        </tr>
                        <tr>
                            <td class="tdD" style="text-align: center; width: 100px">
                                <uc1:fecha ID="txtFechaInicio" runat="server"></uc1:fecha>
                            </td>
                            <td class="tdD" style="text-align: center; width: 100px">
                                <uc1:fecha ID="txtFechaFinal" runat="server"></uc1:fecha>
                            </td>
                            <td class="tdD" style="text-align: center; width: 100px">
                                <asp:Button ID="btnExportar" runat="server" CssClass="btn8" onclick="btnExportar_Click" Text="Exportar a excel" Visible="false" /><br/>
                            </td>
                        </tr>
                    </table>
                    <table border="0" cellpadding="0" cellspacing="0" width="100%" runat="server">
                        <tr>
                            <td>
                                <br/>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <div style="overflow: auto; width: 100%;" runat="server">                                    
                                    <asp:GridView ID="gvConceptos" runat="server"  AutoGenerateColumns="false"
                                        HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager" RowStyle-CssClass="gridItem" ShowFooter="False"
                                        Style="font-size: small" Width="90%" GridLines="Both" >
                                        <Columns runat="server">
                                        </Columns>
                                        <HeaderStyle CssClass="gridHeader" />
                                        <PagerStyle CssClass="gridPager" />
                                        <RowStyle CssClass="gridItem" />
                                    </asp:GridView>
                                </div>
                            </td>
                        </tr>
                    </table>

                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td colspan="2">
                                <div style="overflow: auto; width: 100%;">
                                    <asp:GridView ID="gvTotales" runat="server" AutoGenerateColumns="false"
                                        HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager" RowStyle-CssClass="gridItem" ShowFooter="False"
                                        Style="font-size: small" Width="90%" GridLines="Both">
                                        <Columns>
                                        </Columns>
                                        <HeaderStyle CssClass="gridHeader" />
                                        <PagerStyle CssClass="gridPager" />
                                        <RowStyle CssClass="gridItem" />
                                    </asp:GridView>
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
            </asp:Panel>
        </asp:View>
    </asp:MultiView>

</asp:Content>

