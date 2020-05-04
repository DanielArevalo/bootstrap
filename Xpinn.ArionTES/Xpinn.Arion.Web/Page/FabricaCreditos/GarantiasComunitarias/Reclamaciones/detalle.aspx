<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Detalle.aspx.cs" Inherits="garantiascomunitarias" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div id="gvDiv">
        <table style="width: 100%">
            <tr>
                <td style="text-align: left" colspan="2">
                    <asp:Label ID="Label3" runat="server" Text="Fecha Inicial"></asp:Label><br />
                    <ucFecha:fecha ID="ucFecha" runat="server" />
                </td>
                <td style="text-align: left" colspan="2">
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td style="text-align: left">
                    <br />
                </td>
            </tr>
        </table>
        <table width="100%">
            <tr>
                <td style="text-align:left" >
                    <asp:Button ID="Button2" runat="server" CssClass="btn8" OnClick="btnExportarExcel_Click"
                        Text="Exportar a Excel" Width="124px" Height="28px" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <hr />
                </td>
            </tr>
        </table>
        <table style="width: 100%">
            <tr>
                <td>
                    <div style="overflow: scroll; height: 500px; width: 100%;">
                        <div style="width: 100%;">
                              <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                <ContentTemplate>
                              <asp:GridView ID="gvMovGeneral" runat="server" Width="100%" PageSize="3" GridLines="Horizontal"
                                ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" OnPageIndexChanging="gvMovGeneral_PageIndexChanging"
                                SelectedRowStyle-Font-Size="XX-Small" 
                                        Style="font-size: x-small; margin-bottom: 0px;">
                                <Columns>
                                    <asp:BoundField DataField="NUMERO_RADICACION" HeaderText="Número del Crédito" />
                                    <asp:BoundField DataField="NITENTIDAD" HeaderText="Nit de la Entidad" />
                                    <asp:BoundField DataField="IDENTIFICACION" HeaderText="Cedula/Nit" />
                                    <asp:BoundField DataField="fechaproxpago" HeaderText="Fecha Proximo Pago" />
                                    <asp:BoundField DataField="diasmora" HeaderText="Dias Mora" />
                                    <asp:BoundField DataField="CAPITAL" HeaderText="Capital" 
                                        DataFormatString="{0:d}" >
                                    <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="INT_CORRIENTES" HeaderText="Int Corriente" 
                                        DataFormatString="{0:d}" >
                                    <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="INT_MORA" HeaderText="Int Mora" 
                                        DataFormatString="{0:d}" >
                                    <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="CUOTAS_RECLAMAR" HeaderText="Cuota Reclamación" />
                                    <asp:BoundField DataField="RECLAMACION" HeaderText="Tipo Reclamacion" />
                                </Columns>
                                <HeaderStyle CssClass="gridHeader" />
                                <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                                <RowStyle CssClass="gridItem" />
                                <SelectedRowStyle Font-Size="XX-Small"></SelectedRowStyle>
                            </asp:GridView>
                            </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    <asp:Label ID="lblTotalReg" runat="server" Visible="False" />
                    <asp:Label ID="lblInfo" runat="server" Text="Su consulta no obtuvo ningun resultado."
                        Visible="False" />
                </td>
            </tr>
            <tr>
                <td>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
