<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Nuevo" %>


<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Src="~/General/Controles/ctlseleccionmultipledropdown.ascx" TagName="seleccionmultiple" TagPrefix="uc3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
     <div id="gvDiv">
    <asp:MultiView ID="mvPrincipal" runat="server">
        <asp:View ID="vwFiltros" runat="server">
                <table cellpadding="1" cellspacing="0" style="width: 100%">
                    <tr>
                        <td colspan="3">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; width: 3%;">
                            Fecha de Arqueo
                            <br />
                            <ucFecha:fecha ID="txtFechaArqueo" runat="server" CssClass="textbox" />
                            </td>
                            <td>
                                <br />
                                a
                                <ucFecha:fecha ID="txtfechafinal" runat="server" CssClass="textbox" />
                                </td>
                                <td style="text-align: left; width: 19%;">
                                    Oficinas
                                    <br />
                                    <uc3:seleccionmultiple ID="ddloficina" runat="server" CssClass="textbox" 
                                        Enabled="False" Width="60%">
                                    </uc3:seleccionmultiple>
                                </td>
                                <td style="text-align: left; width: 40%;">
                                    Usuarios<br />
                                    <uc3:seleccionmultiple ID="ddlusuarios" runat="server" CssClass="textbox" 
                                        Enabled="True" Width="70%">
                                    </uc3:seleccionmultiple>
                                </td>
                    </tr>
                </table>
                <table cellpadding="1" cellspacing="0" style="width: 100%; text-align: left">
                    <tr>
                        <td colspan="4">
                            <div id="DivButtons">
                                &nbsp; &nbsp; &nbsp;
                            </div>
                        </td>
                    </tr>
                </table>
                <table cellpadding="2" cellspacing="0" style="width: 100%; text-align: left">
                    <tr>
                        <td>
                            <strong>Arqueos</strong></td>
                    </tr>
                    <tr>
                        <td colspan="2">
                        <div style="overflow:scroll;height:500px;width:100%;">
                            <asp:GridView ID="gvSaldos" runat="server" BackColor="White" BorderStyle="None" BorderWidth="1px"
                                CellPadding="4" Style="font-size: x-small; margin-bottom: 0px;" ForeColor="Black" Width="100%"
                                AutoGenerateColumns="False" GridLines="Vertical" >
                                <Columns>
                                    <%--<asp:BoundField DataField="orden" HeaderStyle-CssClass="gridColNo" ItemStyle-CssClass="gridColNo"><HeaderStyle CssClass="gridColNo"></HeaderStyle><ItemStyle CssClass="gridColNo"></ItemStyle></asp:BoundField>--%>
                                    <%--<asp:BoundField DataField="cod_moneda" HeaderStyle-CssClass="gridColNo" ItemStyle-CssClass="gridColNo"><HeaderStyle CssClass="gridColNo"></HeaderStyle><ItemStyle CssClass="gridColNo"></ItemStyle></asp:BoundField>--%>
                                    <asp:BoundField DataField="nom_moneda" HeaderText="Moneda" />
                                    <asp:BoundField DataField="concepto" HeaderText="Concepto"><ItemStyle HorizontalAlign="Left" /></asp:BoundField>
                                    <asp:BoundField DataField="efectivo" HeaderText="Efectivo" DataFormatString="{0:N0}"><ItemStyle HorizontalAlign="Right" /></asp:BoundField>
                                    <asp:BoundField DataField="cheque" HeaderText="Cheque" DataFormatString="{0:N0}"><ItemStyle HorizontalAlign="Right" /></asp:BoundField>
                                    <asp:BoundField DataField="datafono" DataFormatString="{0:N0}" HeaderText="Datafono"><ItemStyle HorizontalAlign="Right" /></asp:BoundField>
                                    <asp:BoundField DataField="consignacion" DataFormatString="{0:N0}" HeaderText="Otros"><ItemStyle HorizontalAlign="Right" /></asp:BoundField>
                                    <asp:BoundField DataField="total" HeaderText="Total" DataFormatString="{0:N0}"><ItemStyle HorizontalAlign="Right" /></asp:BoundField>
                                </Columns>
                                <FooterStyle BackColor="#CCCC99" />
                                <HeaderStyle CssClass="gridHeader" />
                                <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                                <RowStyle CssClass="gridItem" />
                                <SelectedRowStyle Font-Size="XX-Small"></SelectedRowStyle>
                            </asp:GridView>
                            </div>
                        </td>
                    </tr>
                </table>
        </asp:View>
        <asp:View ID="vwReporte" runat="server">
            <table width="100%">                
                <tr>
                    <td style="text-align: left; width: 131px;">
                        <asp:Button ID="btnImprime" runat="server" CssClass="btn8" Height="25px" Width="120px"
                            Text="Imprimir" OnClick="btnImprime_Click" />
                    </td>
                    <td style="text-align: left">
                    <asp:Button ID="btnCloseReg" runat="server" CssClass="btn8" Height="25px" Width="120px"
                            Text="  Cerrar  " OnClick="btnCloseReg_Click" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align: left">
                        <strong>Información</strong>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left" colspan="2">
                        <iframe id="frmPrint" name="IframeName" width="100%" src="../../Reportes/Reporte.aspx"
                            runat="server" style="border-style: dotted; float: left;" height="500px"></iframe>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left" colspan="2">
                        <rsweb:ReportViewer ID="rptArqueoPagos" runat="server" Font-Names="Verdana" Font-Size="8pt"
                            InteractiveDeviceInfos="(Colección)" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt"
                            Width="100%" Height="500px"><LocalReport ReportPath="Page\Tesoreria\Arqueo\rptArqueoPagos.rdlc"></LocalReport></rsweb:ReportViewer>
                    </td>
                </tr>
            </table>
        </asp:View>
    </asp:MultiView>

    </div>
</asp:Content>

