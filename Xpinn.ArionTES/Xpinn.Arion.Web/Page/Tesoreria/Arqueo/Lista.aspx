<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Nuevo" %>


<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc1" %>
<%@ Register Src="../../../General/Controles/fechaeditable.ascx" TagName="fechaEditable" TagPrefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
     <div id="gvDiv">
    <asp:MultiView ID="mvPrincipal" runat="server">
        <asp:View ID="vwFiltros" runat="server">
                <table cellpadding="1" cellspacing="0" style="width: 80%">
                    <tr>
                        <td colspan="3">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; width: 7%;">
                            Fecha de Arqueo
                            <br />
                            <%--<ucFecha:fecha ID="txtFechaArqueo" runat="server" Enabled="false" CssClass="textbox" />--%>
                             <uc2:fechaEditable ID="txtFechaArqueo" runat="server"
                                          OneventoCambiar="txtFechaArqueo_eventoCambiar" />
                        </td>
                        <td style="text-align: left; width: 15%;">
                            Oficina
                            <br />
                            <asp:TextBox ID="txtOficina" runat="server" Enabled="False" CssClass="textbox" Width="20%"></asp:TextBox>
                            <asp:TextBox ID="txtNomOficina" runat="server" Enabled="False" CssClass="textbox"
                                Width="60%"></asp:TextBox>
                        </td>
                        <td style="text-align: left; width: 40%;">
                            Usuario
                            <br />
                            <asp:TextBox ID="txtCodUsuario" runat="server" Enabled="False" CssClass="textbox"
                                Width="10%"></asp:TextBox>
                            <asp:TextBox ID="txtUsuario" runat="server" Enabled="False" CssClass="textbox" Width="70%"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <table cellpadding="1" cellspacing="0" style="width: 100%; text-align: left">
                    <tr>
                        <td colspan="4">
                            <div id="DivButtons">
                                <asp:Button ID="btnGenerarArqueo" runat="server" Text="Generar Arqueo" Height="27px"
                                    OnClick="btnGenerarArqueo_Click" />&#160;
                                <asp:Button ID="btnImprimirArqueo" runat="server" Text="Imprimir Arqueo" 
                                    Height="27px" onclick="btnImprimirArqueo_Click" />&#160;
                                <asp:Button ID="btnVerCheques" runat="server" Text="Ver Cheques" OnClick="btnVerCheques_Click"
                                    Height="27px" />&#160
                                <asp:Button ID="btnReporte" runat="server" Text="  Reporte  " Height="27px" OnClick="btnReporte_Click" />
                            </div>
                        </td>
                    </tr>
                </table>
                <table cellpadding="2" cellspacing="0" style="width: 100%; text-align: center">
                    <tr>
                        <td colspan="2" style="text-align: center; color: #FFFFFF; background-color: #0066FF">
                            <strong>Saldos</strong>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:GridView ID="gvSaldos" runat="server" BackColor="White" BorderStyle="None" BorderWidth="1px"
                                CellPadding="4" Style="font-size: x-small; margin-bottom: 0px;" ForeColor="Black"
                                AutoGenerateColumns="False" GridLines="Vertical" Width="714px">
                                <Columns>
                                    <asp:BoundField DataField="orden" HeaderStyle-CssClass="gridColNo" ItemStyle-CssClass="gridColNo"><HeaderStyle CssClass="gridColNo"></HeaderStyle><ItemStyle CssClass="gridColNo"></ItemStyle></asp:BoundField>
                                    <asp:BoundField DataField="cod_moneda" HeaderStyle-CssClass="gridColNo" ItemStyle-CssClass="gridColNo"><HeaderStyle CssClass="gridColNo"></HeaderStyle><ItemStyle CssClass="gridColNo"></ItemStyle></asp:BoundField>
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
                        </td>
                    </tr>
                </table>
        </asp:View>
        <asp:View ID="vwCheques" runat="server">
            <table width="100%">
                <tr>
                    <td style="text-align: center; color: #FFFFFF; background-color: #0066FF">
                        Cheques Pendientes
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:GridView ID="gvChequePendiente" runat="server" Width="100%" AutoGenerateColumns="False"
                            Style="font-size: x-small;" AllowPaging="False" PageSize="20" BackColor="White"
                            BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black"
                            GridLines="Vertical">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:BoundField DataField="cod_movimiento" HeaderText="Código Operación" />
                                <asp:BoundField DataField="num_documento" HeaderText="Número Cheque" />
                                <asp:BoundField DataField="nom_banco" HeaderText="Banco" />
                                <asp:BoundField DataField="titular" HeaderText="Títular" />
                                <asp:BoundField DataField="valor" HeaderText="Valor" DataFormatString="{0:N0}">
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                            </Columns>
                            <FooterStyle BackColor="#CCCC99" />
                            <HeaderStyle CssClass="gridHeader" />
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
                    <td style="text-align:center">
                       <asp:Label ID="lblTotPendiente" runat="server" Visible="false" />
                       <asp:Label ID="lblInfoPendiente" runat="server" Text="Su consulta no obtuvo ningún resultado" Visible="false" />                       
                    </td>
                </tr>
                <tr>
                    <td style="text-align:center">
                       &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center; color: #FFFFFF; background-color: #0066FF">
                        Cheques Asignados
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:GridView ID="gvChequeAsignado" runat="server" Width="100%" AutoGenerateColumns="False"
                            Style="font-size: x-small;" AllowPaging="False" PageSize="20" BackColor="White"
                            BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black"
                            GridLines="Vertical">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:BoundField DataField="cod_movimiento" HeaderText="Código Giro" />
                                <asp:BoundField DataField="num_documento" HeaderText="Número Cheque" />
                                <asp:BoundField DataField="nom_banco" HeaderText="Banco" />
                                <asp:BoundField DataField="titular" HeaderText="Títular" />
                                <asp:BoundField DataField="valor" HeaderText="Valor" DataFormatString="{0:N0}">
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="estado" HeaderText="Estado" />
                                <asp:BoundField DataField="fec_ope" DataFormatString="{0:d}" HeaderText="Fecha Asignación" />
                            </Columns>
                            <FooterStyle BackColor="#CCCC99" />
                            <HeaderStyle CssClass="gridHeader" />
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
                    <td style="text-align:center">
                       <asp:Label ID="lblTotAsignado" runat="server" Visible="false" />
                       <asp:Label ID="lblInfoAsignado" runat="server" Text="Su consulta no obtuvo ningún resultado" Visible="false" />
                    </td>
                </tr>
            </table>
        </asp:View>
        <asp:View ID="vwReporte" runat="server">
            <table width="100%">                
                <tr>
                    <td style="text-align: left">
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
                            Width="100%" Height="500px">
                            <LocalReport ReportPath="Page\Tesoreria\Arqueo\rptArqueoPagos.rdlc">
                            </LocalReport>
                        </rsweb:ReportViewer>
                    </td>
                </tr>
            </table>
        </asp:View>
        <asp:View ID="vfinal" runat="server" >
         <asp:Panel ID="PanelFinal" runat="server">
                <table style="width: 100%;">
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <br />
                            <br />
                            <br />
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <asp:Label ID="lblMensajeGrabar" runat="server" Text="Arqueos Generados Correctamente"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <br />
                            <br />
                        </td>
                    </tr>                    
                </table>
            </asp:Panel>
        </asp:View>
    </asp:MultiView>
    <uc1:mensajeGrabar ID="ctlMensaje" runat="server"/>
    </div>
</asp:Content>

