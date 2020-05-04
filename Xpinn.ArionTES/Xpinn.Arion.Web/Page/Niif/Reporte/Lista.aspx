<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Src="~/General/Controles/decimalesGrid.ascx" TagName="decimalesGrid" TagPrefix="uc1" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:MultiView ID="mvMatrizRiesgo" runat="server" ActiveViewIndex="0">
        <asp:View ID="vw0" runat="server">
            <asp:Panel ID="pConsulta" runat="server" Style="margin-bottom: 0px">
                <table style="width: 100%;">
                    <tr>
                        <td colspan="4">
                            <asp:Label ID="Lblerror" runat="server" CssClass="align-rt" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; height: 25px;" colspan="4">
                            <asp:Label ID="LblTitulo" runat="server" Text="Paramétros de Búsqueda" Style="font-weight: 700;
                                font-size: small;"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left">
                            Fecha Corte<br />
                            <asp:DropDownList ID="ddlFechaCorte" runat="server" CssClass="textbox" Width="227px">
                            </asp:DropDownList>
                            <br />
                        </td>
                        <td style="text-align: left">
                            Centro Costo<br />
                            <asp:DropDownList ID="ddlcentrocosto" runat="server" AppendDataBoundItems="True"
                                CssClass="textbox" Width="243px">
                            </asp:DropDownList>
                            <br />
                        </td>
                        <td style="text-align: left">
                            Moneda<br />
                            <asp:DropDownList ID="ddlMoneda" runat="server" AppendDataBoundItems="True" CssClass="textbox"
                                Width="168px">
                            </asp:DropDownList>
                            <br />
                        </td>
                        <td style="text-align: left">
                            Nivel<br />
                            <asp:DropDownList ID="ddlNivel" runat="server" AppendDataBoundItems="True" CssClass="textbox"
                                Width="147px">
                            </asp:DropDownList>
                            <br />
                        </td>
                        <td style="text-align: left">
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <table style="width: 100%;">
                <tr>
                    <td style="height: 25px; font-weight: 700;" colspan="4" align="left">
                        <asp:CheckBox ID="chkmuestraceros" Text="Mostrar Cuentas Con Saldos En  Ceros" runat="server" />
                    </td>
                </tr> 
                <tr >
                    <td style="height: 25px; font-weight: 700;" colspan="4">
                        <asp:GridView ID="gvMatrizRiesgo" runat="server" Width="97%" AutoGenerateColumns="False"
                            PageSize="5" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px"
                            CellPadding="4" GridLines="Vertical" ShowFooter="True"
                            Style="font-size: xx-small" OnRowDataBound="gvMatrizRiesgo_RowDataBound" OnRowEditing="gvMatrizRiesgo_RowEditing"
                            DataKeyNames="cod_cuenta_niif">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:BoundField DataField="cod_cuenta_niif" HeaderText="Cod.Cuenta" >
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="centro_costo" HeaderText="Centro Costo" >
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="nombre" HeaderText="Nombre Cuenta" >
                                    <ItemStyle HorizontalAlign="Left" Width="400px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="saldo" HeaderText="Valor" DataFormatString="{0:c}" >
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                            </Columns>
                            <FooterStyle CssClass="gridHeader" />
                            <HeaderStyle CssClass="gridHeader" HorizontalAlign="Center" />
                            <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                            <RowStyle CssClass="gridItem" />
                            <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                            <SortedAscendingCellStyle BackColor="#FBFBF2" />
                            <SortedAscendingHeaderStyle BackColor="#848384" />
                            <SortedDescendingCellStyle BackColor="#EAEAD3" />
                            <SortedDescendingHeaderStyle BackColor="#575357" />
                        </asp:GridView>
                    </td>
                </tr>
                
                <tr>
                    <td style="height: 25px" colspan="4">
                        <asp:Label ID="lblTotalRegs" runat="server" Visible="False" />
                        <asp:Label ID="lblInfo" runat="server" Text="Su consulta no obtuvo ningun resultado."
                            Visible="False" />
                    </td>
                </tr>
                <tr>
                    <td style="height: 25px" colspan="3">
                    </td>
                    <td style="height: 25px">
                    </td>
                </tr>
            </table>
        </asp:View>
        <asp:View ID="View3" runat="server">
            <table width="100%">
                    <tr>
                        <td style="text-align: left">
                            <asp:Button ID="btnRegresarOrden" runat="server" CssClass="btn8" OnClientClick="btnInforme4_Click"
                                Text="Regresar" OnClick="btnInforme4_Click" Height="25px" Width="120px" />
                            &#160;&#160;
                            <asp:Button ID="btnImprime" runat="server" CssClass="btn8" Height="25px" Width="120px"
                                Text="Imprimir" OnClick="btnImprime_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <iframe id="frmPrint" name="IframeName" width="100%" src="../../Reportes/Reporte.aspx" height="500px"
                                runat="server" style="border-style: dotted; float: left;"></iframe>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div style="overflow: scroll; max-width: 100%; max-height: 600px">
                                <rsweb:ReportViewer ID="ReportViewer2" runat="server" Font-Names="Verdana" Font-Size="8pt"
                                    InteractiveDeviceInfos="(Colección)" AsyncRendering="false" WaitMessageFont-Names="Verdana"
                                    WaitMessageFont-Size="14pt" Width="100%" Height="100%" SizeToReportContent="True"
                                    DocumentMapWidth="25%">
                                    <LocalReport ReportPath="Page\Niif\ReporteNif\ReporteNif.rdlc">
                                        <DataSources>
                                            <rsweb:ReportDataSource />
                                        </DataSources>
                                    </LocalReport>
                                </rsweb:ReportViewer>
                            </div>
                        </td>
                    </tr>
                </table>    
        </asp:View>
    </asp:MultiView>
</asp:Content>
