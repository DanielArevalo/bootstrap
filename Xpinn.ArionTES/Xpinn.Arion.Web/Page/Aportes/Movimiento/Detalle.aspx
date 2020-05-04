<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"   CodeFile="Detalle.aspx.cs" Inherits="EstadoCuentaCreditoMovimientoGeneral" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ PreviousPageType VirtualPath="~/Page/Asesores/MoviGralCredito/Lista.aspx" %>
<%@ Register Src="~/General/Controles/imprimir.ascx" TagName="imprimir" TagPrefix="ucImprimir" %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="Fecha" TagPrefix="cuFecha" %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha0" %>
<%@ Register Src="~/General/Controles/ctlBusquedaMovimientosAportes.ascx" TagName="BusquedaMovimientos" TagPrefix="uc1" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <br /> 
    
    <asp:Panel ID="pConsulta" runat="server">
    <table style="width: 80%"   ="left" cellpadding="0" cellspacing="0">        
        <tr>
            <td style="text-align: left;">
                Código Cliente<br/>
                <asp:TextBox ID="txtCodCliente" runat="server" Enabled="false" style="text-align: center"
                    Width="160px" ></asp:TextBox>
            </td>
            <td style="text-align: left;">
                Documento<br/>
                <span style="font-size: 12px; color: #000066; background-color: #F4F5FF">
                <asp:TextBox ID="txtNumDoc" runat="server" Enabled="false" style="text-align: center"
                    Width="160px" ></asp:TextBox>
                </span>
            </td>
            <td colspan="2" style="text-align: left;">
                Nombres<br/>
                <asp:TextBox ID="txtNombres" runat="server" Enabled="false" Style="text-align: center"
                    Width="270px"></asp:TextBox>
            </td>
            <td style="text-align: left;">
                Estado&nbsp; Aporte<br/>
                <asp:TextBox ID="TxtEstado" runat="server"  Enabled="false"
                Width="151px" ></asp:TextBox>
            </td>
            <td style="text-align: left;">
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            </td>
        </tr>
        <tr>
            <td style="text-align: left;" colspan="2">
                Dirección<br />
                <asp:TextBox ID="txtDireccion" runat="server"  Enabled="false"  style="text-align: Left"
                    Width="90%"  ></asp:TextBox>
            </td>
            <td style="text-align: left" colspan="2">
                Teléfono<br />
                <asp:TextBox ID="txttelefono" runat="server"  Enabled="false" style="text-align: center"
                    Width="151px" ></asp:TextBox>
            </td>
            <td style="text-align: left">
                &nbsp;</td>
            <td style="text-align: left">
                &nbsp;</td>
        </tr>
        <tr>
            <td style="text-align: left; margin-left: 240px;">
                No Aporte<br />
                <asp:TextBox ID="txtNoAporte" runat="server" Enabled="false" Style="text-align: center"
                    Width="120px"></asp:TextBox>
                <cc1:ButtonGrid ID="btnBusquedaMovimiento" CssClass="btnListado" runat="server" Text="..." Enabled="false"
                    OnClick="btnBusquedaMovimiento_Click" />
                <uc1:BusquedaMovimientos ID="ctlBusquedaMovi" runat="server" />
            </td>
            <td style="text-align: left;">
                Saldo <br /> 
                <asp:TextBox ID="txtSaldo" runat="server"  Enabled="false" style="text-align: center"
                    Width="160px" ></asp:TextBox>
            </td>
            <td style="text-align: left;" colspan="2">
                Línea Aporte<br />
                <asp:TextBox ID="txtLinea" runat="server"  Enabled="false" 
                    Width="270px"  ></asp:TextBox>
            </td>
            <td style="text-align: left;">
                Cuota<br />
                <asp:TextBox ID="txtcuota" runat="server"  Enabled="false" style="text-align: center"
                    Width="151px" ></asp:TextBox>
            </td>
            <td style="text-align: left;">
                &nbsp;</td>
        </tr>
        <tr>
            <td style="text-align: left;">
                Fecha Apertura<br />
                <asp:TextBox ID="TxtFechaApertura" runat="server" Enabled="False" style="text-align: center"
                    Width="160px"></asp:TextBox>
            </td>
            <td style="text-align: left;">
                Fecha Últ Pago <br />
                <asp:TextBox ID="TxtFechaUltimoPago" runat="server" Enabled="False" style="text-align: center"
                    Width="160px" ></asp:TextBox>                    
            </td>
            <td style="text-align: left">
                Fecha Próx Pago<br />
                <asp:TextBox ID="TxtFechaProxPago" runat="server"  style="text-align: center"
                    Enabled="False" Width="151px" ></asp:TextBox>                    
            </td>
            <td colspan="2" style="text-align: left">
                Periodicidad<br />
                <asp:TextBox ID="txtperiodicidad" runat="server"  Enabled="false" style="text-align: center"
                    Width="151px" ></asp:TextBox>
                &nbsp;
            </td>
            <td style="text-align: center">
                &nbsp;</td>
        </tr>
        <tr >
            <td style="text-align: left;">                
                <asp:Label ID="lblFechaInicial" runat="server" Text="Fecha Inicial"></asp:Label>
                <br />
                <asp:TextBox ID="TxtFechaInicial" runat="server" Enabled="False" style="text-align: center"
                    Width="160px" ></asp:TextBox>           
                <ucFecha:fecha ID="ucFechaInicial" runat="server" style="text-align: center" />
            </td>
            <td style="text-align: left">
                <asp:Label ID="lblFechaFinal" runat="server" Text="Fecha Final"></asp:Label>
                <br />
                <asp:TextBox ID="TxtFechaFinal" runat="server" Enabled="False" style="text-align: center"
                    Width="150px" ></asp:TextBox>               
                <ucFecha:fecha ID="ucFechaFinal" runat="server" style="text-align: center" />
            </td>                        
            <td style="text-align: left">
               &nbsp;
            </td>
            <td style="text-align: left">
            </td>
        </tr>
        <tr>
            <td style="height: 62px">    
                
            </td>
            <td style="height: 62px">    
                <asp:CheckBox ID="cbDetallado" runat="server"  Text="Detallado"
                    oncheckedchanged="cbDetallado_CheckedChanged" AutoPostBack="True" />
            </td>
            <td colspan="3" style="height: 12px"  >    
                <asp:Button ID="btnExportarExcel" runat="server" CssClass="btn8" 
                    onclick="btnExportarExcel_Click" Text="Exportar a Excel"  Width="124px" 
                    Height="23px" />   
                <asp:Button ID="btnImprimir" runat="server" CssClass="btn8" 
                    Text="Visualizar informe"  Width="124px" 
                    Height="20px" onclick="btnImprimir_Click"/>    
                <asp:Button ID="btnMovimientosHistoricos" runat="server" CssClass="btn8" 
                    Height="20px" onclick="btnMovimi_Click" Text="Movimientos Historicos"/>
            </td>
        </tr>   
    </table>
    </asp:Panel>    
    
    <hr style=" width:100%"/>   
    <table style=" width:100%">
        <tr>
            <td> 
                <div style="overflow:scroll;height:500px;width:100%;">
                <asp:GridView ID="gvMovGeneral" runat="server" Width="100%" 
                        PageSize="3" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False"
                    OnPageIndexChanging="gvMovGeneral_PageIndexChanging"  
                    SelectedRowStyle-Font-Size="XX-Small" style="font-size:small" 
                    onselectedindexchanged="gvMovGeneral_SelectedIndexChanged">                    
                    <Columns>
                        <asp:BoundField DataField="numero_aporte" HeaderText="Número de Radicación" Visible="false" />
                        <asp:BoundField DataField="FechaCuota" HeaderText="Fecha Cuota" >
                            <ItemStyle Width="10%" /></asp:BoundField>
                        <asp:BoundField DataField="FechaPago" HeaderText="Fecha Pago" />                        
                        <asp:BoundField DataField="Transaccion" HeaderText="Transaccion" />
                        <asp:BoundField DataField="TipoOperacion" HeaderText="No Operac" />
                        <asp:BoundField DataField="num_comp" HeaderText="Num.Comprobante">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="tipo_comp" HeaderText="Tipo Comp.">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>                        
                        <asp:BoundField DataField="nom_tipo_comp" HeaderText="Descripción">
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="TipoMovimiento" HeaderText="Tipo Mov" />
                        <asp:BoundField DataField="Capital" HeaderText="Valor" DataFormatString="{0:c}" ItemStyle-HorizontalAlign="Right" />
                        <asp:BoundField DataField="Saldo" HeaderText="Saldo" DataFormatString="{0:c}" ItemStyle-HorizontalAlign="Right">
                            <ItemStyle Width="10%" />
                        </asp:BoundField>                        
                    </Columns>
                    <HeaderStyle CssClass="gridHeader" />
                    <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                    <RowStyle CssClass="gridItem" />
                    <SelectedRowStyle Font-Size="XX-Small"></SelectedRowStyle>
                </asp:GridView>
                <br />
                <rsweb:ReportViewer ID="Rpview1" runat="server" Width="100%" 
                    Font-Names="Verdana" Font-Size="8pt" InteractiveDeviceInfos="(Colección)" 
                    WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Height="450px">
                    <LocalReport ReportPath="Page\Aportes\Movimiento\ReportMovimiento.rdlc">
                    </LocalReport>
                </rsweb:ReportViewer>
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
</asp:Content>
