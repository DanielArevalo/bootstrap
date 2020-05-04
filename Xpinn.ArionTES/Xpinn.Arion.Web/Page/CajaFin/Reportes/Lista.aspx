<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" EnableEventValidation="true" CodeFile="Lista.aspx.cs" Inherits="Page_CajaFin_Reportes_Lista" Title=".: Xpinn - Reporte Caja :."%>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" contentplaceholderid="cphMain"  runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:Panel ID="pConsulta" runat="server" Width="504px">
        <table style="width:62%;">
            <tr>
                <td align="center" colspan="2">
                    Consultar<br />
                    <asp:DropDownList ID="ddlConsultar" runat="server" AutoPostBack="True" 
                        CssClass="dropdown" onselectedindexchanged="ddlConsultar_SelectedIndexChanged">
                        <asp:ListItem Value="0">&lt;Seleccione un Item&gt;</asp:ListItem>
                        <asp:ListItem Value="1">MOVIMIENTO DIARIO DE CAJA</asp:ListItem>
                    </asp:DropDownList>
                    <br />
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Panel ID="Panel1" runat="server" HorizontalAlign="Center" Width="250px">
                        <asp:Label ID="LabelFecha_gara1" runat="server" Text="Fecha Inicial"></asp:Label>
                        <br />
                        <asp:TextBox ID="txtFechaIni" runat="server" cssClass="textbox" Height="23px" 
                            maxlength="10" Width="106px"></asp:TextBox>
                        <asp:CalendarExtender ID="CalendarExtender4" runat="server" Format="dd/MM/yyyy" 
                            PopupButtonID="Image1" TargetControlID="txtFechaIni">
                        </asp:CalendarExtender>
                        <asp:Label ID="Label3" runat="server" style="color: #FF3300"></asp:Label>
                    </asp:Panel>
                </td>
                <td align="center">
                    &nbsp;</td>
            </tr>
        </table>
    </asp:Panel>

    <asp:MultiView ID="mvLista" runat="server">        
        <asp:View ID="vGridMovDiario" runat="server">
            <div style="overflow:scroll;height:350px; width:1000px;">
                <div style="width:1500px;">
                    <asp:Button ID="btnExportar" runat="server" CssClass="btn8" 
                        onclick="btnExportar_Click" Text="Exportar a excel" />                       
                    <asp:GridView ID="gvReportemovdiario" runat="server"  
                        AutoGenerateColumns="False" DataKeyNames="cod_oficina" HeaderStyle-CssClass="gridHeader"   
                        PagerStyle-CssClass="gridPager"  RowStyle-CssClass="gridItem" 
                        Width="65%">
                        <Columns>                            
                            <asp:TemplateField HeaderStyle-CssClass="gridIco" ItemStyle-CssClass="gridIco">
                                <HeaderStyle CssClass="gridIco" />
                                <ItemStyle CssClass="gridIco" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="COD_OFICINA" HeaderText="C. OFICINA">
                            <ItemStyle HorizontalAlign="left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="NOMBRE_OFICINA" HeaderText="NOMBRE OFICINA">
                            <ItemStyle HorizontalAlign="left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="COD_CAJERO" HeaderText="CAJERO">
                            <ItemStyle HorizontalAlign="left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="saldoefectivo" DataFormatString="{0:C}" 
                                HeaderText="EFECTIVO INICIAL" />
                            <asp:BoundField DataField="saldocheque" DataFormatString="{0:C}" 
                                HeaderText="CHEQUE INICIAL" />
                            <asp:BoundField DataField="FECHA_MOVIMIENTO" HeaderText="FECHA MOVIMIENTO">
                            <ItemStyle HorizontalAlign="left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CANTIDAD_PAGOS" HeaderText="CANTIDAD PAGOS" DataFormatString="{0:d}">
                            <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="EFECTIVO" HeaderText="EFECTIVO" 
                                DataFormatString="{0:C}">
                            <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CHEQUE" HeaderText="CHEQUE" DataFormatString="{0:C}">
                            <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="EGRESOS_EFECTIVO" HeaderText="EGRESOS" 
                                DataFormatString="{0:C}">
                            <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="consignaciones" DataFormatString="{0:C}" 
                                HeaderText="CONSIGNACIONES" />
                            <asp:BoundField DataField="totalcheque" DataFormatString="{0:C}" 
                                HeaderText="CHEQUE FINAL" />
                            <asp:BoundField DataField="totalefectivo" DataFormatString="{0:C}" 
                                HeaderText="EFECTIVO FINAL" />
                        </Columns>
                        <HeaderStyle CssClass="gridHeader" />
                        <PagerStyle CssClass="gridPager" />
                        <RowStyle CssClass="gridItem" />
                    </asp:GridView>
                </div>
                <div class="align-rt" />
                    <table style="width:100%;">
                        <tr>
                            <td align="center" colspan="9">
                                Consultar<br />
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td align="center" style="width: 139px">
                                <asp:Label ID="lblTotalRegs" runat="server" />
                            </td>
                            <td align="center" style="width: 140px">
                                &nbsp;</td>
                            <td align="center" style="width: 92px">
                                &nbsp;</td>
                            <td align="center" style="width: 174px">
                                &nbsp;</td>
                            <td align="center" class="logo" style="width: 121px">
                                Total Valor Pago<asp:TextBox ID="txtTotalvalorPago" runat="server" 
                                    CssClass="textbox" Enabled="False" Height="19px" Style="text-align: center" 
                                    Width="104px"></asp:TextBox>
                                <asp:MaskedEditExtender ID="MaskedEditExtender2" runat="server" 
                                    DisplayMoney="Right" Mask="999,999,999" MaskType="Number" 
                                    TargetControlID="txtTotalvalorPago" />
                            </td>
                            <td align="center">
                                Total Cantidad<br />
                                <asp:TextBox ID="txtTotalcantidad" runat="server" CssClass="textbox" 
                                    Enabled="False" Height="19px" Style="text-align: center" Width="104px"></asp:TextBox>
                                <asp:MaskedEditExtender ID="txtTotalcantidad_MaskedEditExtender" runat="server" 
                                    DisplayMoney="Right" Mask="999,999,999" MaskType="Number" 
                                    TargetControlID="txtTotalcantidad" />
                            </td>
                            <td align="center">
                                Total Efectivo<br />
                                <asp:TextBox ID="txtTotalefectivo" runat="server" CssClass="textbox" 
                                    Enabled="False" Height="19px" Style="text-align: center" Width="104px"></asp:TextBox>
                                <asp:MaskedEditExtender ID="txtTotalefectivo_MaskedEditExtender" runat="server" 
                                    DisplayMoney="Right" Mask="999,999,999" MaskType="Number" 
                                    TargetControlID="txtTotalefectivo" />
                            </td>
                            <td align="center">
                                Total Cheques<br />
                                <asp:TextBox ID="txtTotalcheques" runat="server" CssClass="textbox" 
                                    Enabled="False" Height="19px" Style="text-align: center" Width="104px"></asp:TextBox>
                                <asp:MaskedEditExtender ID="txtTotalcheques_MaskedEditExtender" runat="server" 
                                    DisplayMoney="Right" Mask="999,999,999" MaskType="Number" 
                                    TargetControlID="txtTotalcheques" />
                            </td>
                            <td align="center">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                    <br />
                    <br />
                    &nbsp;
                </div>
        </asp:View>

    </asp:MultiView> 

</asp:Content>

