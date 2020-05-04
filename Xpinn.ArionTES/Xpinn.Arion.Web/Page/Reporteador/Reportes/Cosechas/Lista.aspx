<%@ Page Title=".: Xpinn - Resumen Cosechas." Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" %>

<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>



<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:Panel ID="pConsulta" runat="server">

        <div id="gvDiv" runat="server">
            <table id="tbCriterios" border="0" cellpadding="0" cellspacing="0" width="40%" runat="server">
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
                </tr>
            </table>

            <table style="width: 100%" runat="server">
                <tr>
                    <td>
                        <asp:Label ID="lblTitulo1" runat="server" Text="COLOCACIÓN" Visible="false"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div style="overflow: auto; width: 100%;" runat="server">
                            <asp:Button ID="BtnExpColo" runat="server" CssClass="btn8" OnClick="ExportarColo" Text="Exportar a excel" Visible="false" /><br />
                            <asp:Button ID="BtnDetallado" runat="server" CssClass="btn8" OnClick="ExportarDet" Text="Exportar Detallado" Visible="false" /><br />
                            <asp:GridView ID="gvColocacion" runat="server"
                                AutoGenerateColumns="False"
                                HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager"
                                RowStyle-CssClass="gridItem" Style="font-size: x-small" Width="90%" Visible="false"
                                OnDataBound="gvColocacion_OnRowDataBound">
                                <Columns>
                                    <asp:BoundField DataField="stg_comportamiento" HeaderText="COMPORTAMIENTO">
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="valor" HeaderText="VALOR" DataFormatString="{0:c0}">
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="cantidad" HeaderText="CANTIDAD" DataFormatString="{0:g}">
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                </Columns>

                                <HeaderStyle CssClass="gridHeader" />
                                <PagerStyle CssClass="gridPager" />
                                <RowStyle CssClass="gridItem" />
                            </asp:GridView>
                            &nbsp;
                        </div>
                    </td>

                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblTitulo2" runat="server" Text="CARTERA VENCIDA MILL$" Visible="false"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div style="overflow: auto; width: 100%;" runat="server">
                            <asp:Button ID="BtnExpCarVen" runat="server" CssClass="btn8" OnClick="ExportarCarVen" Text="Exportar a excel" Visible="false" /><br />
                            <asp:GridView ID="gvConceptos" runat="server" AutoGenerateColumns="false" OnRowCreated="gvConceptos_RowCreated"
                                HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager" RowStyle-CssClass="gridItem" ShowFooter="False"
                                Style="font-size: small" Width="90%" GridLines="Both" OnDataBound="gvConceptos_OnDataBound">
                                <Columns runat="server">
                                </Columns>
                                <HeaderStyle CssClass="gridHeader" />
                                <PagerStyle CssClass="gridPager" />
                                <RowStyle CssClass="gridItem" />
                            </asp:GridView>
                            &nbsp;
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblTitulo3" runat="server" Text="INDICADOR DE CALIDAD DE COSECHA % CON ARRASTRE" Visible="false"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                         <div style="overflow: auto; width: 100%;" runat="server">
                            <asp:Button ID="BtnExpCalidad" runat="server" CssClass="btn8" OnClick="ExpCalidadCart" Text="Exportar a excel" Visible="false" /><br />
                            <asp:GridView ID="gvCalidad" runat="server" AutoGenerateColumns="false" OnRowCreated="gvCalidad_RowCreated"
                                HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager" RowStyle-CssClass="gridItem" ShowFooter="False"
                                Style="font-size: small" Width="90%" GridLines="Both" OnDataBound="gvCalidad_OnDataBound">
                                <Columns runat="server">
                                </Columns>
                                <HeaderStyle CssClass="gridHeader" />
                                <PagerStyle CssClass="gridPager" />
                                <RowStyle CssClass="gridItem" />
                            </asp:GridView>
                            &nbsp;
                        </div>
                    </td>
                </tr>
            </table>
        </div>
    </asp:Panel>
</asp:Content>

