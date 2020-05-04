<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
<br/><br/>
<div id="gvDiv">
    <table cellpadding="1" cellspacing="0" style="width: 80%">
        <tr>
            <td style="text-align:left; width: 10%;">
                Fecha de Arqueo <br/>
                <asp:TextBox ID="txtFechaArqueo" Enabled="false" runat="server" CssClass="textbox"  Width="130px"></asp:TextBox>
            </td>
            <td style="text-align:left; width: 7%;">
                Oficina <br/>
                <asp:TextBox ID="txtOficina" runat="server" Enabled="False" CssClass="textbox" Width="150px"></asp:TextBox>
            </td>
             <td style="text-align:left; width: 7%;">
                Caja<br/>
                <asp:TextBox ID="txtCaja" runat="server" Enabled="False" CssClass="textbox"  Width="150px"></asp:TextBox>
            </td>
            <td style="text-align:left; width: 0%;">
                Cajero <br/>
                <asp:TextBox ID="txtCajero" runat="server" Enabled="False" CssClass="textbox" 
                    Width="220px"></asp:TextBox>
            </td>
            <td style="text-align:left; width: 0%;">
                &nbsp;</td>
        </tr>
    </table> 
    <table cellpadding="1" cellspacing="0" style="width: 100%; text-align:left">
        <tr>
            <td colspan="4">
                <div id="DivButtons">
                    <asp:Button ID="btnGenerarArqueo" runat="server" Text="Generar Arqueo" onclick="btnGenerarArqueo_Click"/>&#160;
                    <asp:Button ID="btnImprimirArqueo" runat="server" Text="Imprimir Arqueo" OnClick="btnImprimirArqueo_Click"/>&#160;
                    <asp:Button ID="btnVerMovimientos" runat="server" Text="Ver Movimientos" onclick="btnVerMovimientos_Click"/>&#160;
                    <asp:Button ID="btnVerCheques" runat="server" Text="Ver Cheques" onclick="btnVerCheques_Click"/>&#160
                    <asp:Button ID="btnReporte" runat="server" Text="  Reporte  " onclick="btnReporte_Click"/>
                </div>
            </td>
        </tr>
    </table> 
    <table cellpadding="2" cellspacing="0" style="width: 100%; text-align:center">
        <tr> 
            <td colspan="2"  style="text-align: center; color: #FFFFFF; background-color: #0066FF">
                Saldos
            </td>
        </tr>
        <tr> 
            <td colspan="2">
                 <asp:GridView ID="gvSaldos" runat="server" BackColor="White" 
                    BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
                    ForeColor="Black" AutoGenerateColumns="False" GridLines="Vertical" 
                     Width="714px">
                    <AlternatingRowStyle BackColor="White" />
                    <columns>
                        <asp:BoundField DataField="orden" HeaderStyle-CssClass="gridColNo" 
                            ItemStyle-CssClass="gridColNo" >
                            <HeaderStyle CssClass="gridColNo"></HeaderStyle>
                            <ItemStyle CssClass="gridColNo"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="cod_moneda" HeaderStyle-CssClass="gridColNo" 
                            ItemStyle-CssClass="gridColNo" >
                            <HeaderStyle CssClass="gridColNo"></HeaderStyle>
                            <ItemStyle CssClass="gridColNo"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="nom_moneda" HeaderText="Moneda" />
                        <asp:BoundField DataField="concepto" HeaderText="Concepto" > 
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="efectivo" HeaderText="Efectivo" DataFormatString="{0:N0}" >
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                          <asp:BoundField DataField="consignacion" HeaderText="Consignación" DataFormatString="{0:N0}" >
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                          <asp:BoundField DataField="datafono" HeaderText="Datáfono" DataFormatString="{0:N0}" >
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="cheque" HeaderText="Cheque" DataFormatString="{0:N0}" >
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="total" HeaderText="Total" DataFormatString="{0:N0}">
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                    </columns>
                    <FooterStyle BackColor="#CCCC99" />
                    <HeaderStyle CssClass="gridHeader" />
                    <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                    <RowStyle BackColor="#F7F7DE" />
                    <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#FBFBF2" />
                    <SortedAscendingHeaderStyle BackColor="#848384" />
                    <SortedDescendingCellStyle BackColor="#EAEAD3" />
                    <SortedDescendingHeaderStyle BackColor="#575357" />
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td>                
            </td>
        </tr>
    </table> 

    <asp:HiddenField ID="HiddenField1" runat="server" />
    
    <asp:ModalPopupExtender ID="mpeNuevo" runat="server" 
        PopupControlID="panelActividadReg" TargetControlID="HiddenField1"
         BackgroundCssClass="backgroundColor" >        
    </asp:ModalPopupExtender>
   
    <asp:Panel ID="panelActividadReg" runat="server" BackColor="White" 
        style="text-align: left" BorderStyle="Solid" Width="800px">    
        <div id="popupcontainer" style="width:792px">                    
            <div class="row popupcontainertitle">
                <div class="cell popupcontainercell" style="text-align: center">
                            Información</div>  
            </div>
            <div class="row">
                    <div class="cell popupcontainercell">
                        <div id="ordereditcontainer">
                        <asp:UpdatePanel ID="upActividadReg" runat="server">
                        <ContentTemplate>   
                            <div class="row">
                                <div class="cell ordereditcell">
                                    <br />
                                    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" 
                                        Font-Size="8pt" InteractiveDeviceInfos="(Colección)" 
                                        WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="763px">
                                        <LocalReport ReportPath="Page\CajaFin\ArqueoCaja\ReporteArqueo.rdlc" EnableExternalImages="True">
                                        </LocalReport>
                                    </rsweb:ReportViewer>
                                    <br>
                                </div>
                            </div>
                        </ContentTemplate>
                        </asp:UpdatePanel>
                        <div class="row">
                            <div class="cell" style="text-align:right">
                                <asp:Button ID="btnCloseReg" runat="server" Text="Cerrar" 
                                    CssClass="button"  CausesValidation="false" 
                                    Height="20px" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>   
    </asp:Panel>
</div>
</asp:Content>

