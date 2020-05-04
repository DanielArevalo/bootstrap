<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"   CodeFile="Detalle.aspx.cs" Inherits="EstadoCuentaCreditoMovimientoGeneral" %>

<%@ PreviousPageType VirtualPath="~/Page/Asesores/MoviGralCredito/Lista.aspx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/imprimir.ascx" TagName="imprimir" TagPrefix="ucImprimir" %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>


<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <br />     
    <table style="width: 80%" align="left" cellpadding="0" cellspacing="0">        
        <tr>
            <td style="text-align: left;">
                Código Cliente<br/>
                <asp:TextBox ID="txtCodCliente" runat="server" Enabled="False" style="text-align: center"
                    Width="160px" ></asp:TextBox>
            </td>
            <td style="text-align: left;" colspan="2">
                Documento<br/>
                <span style="font-size: 12px; color: #000066; background-color: #F4F5FF">
                <asp:TextBox ID="txtTipoDoc" runat="server"  Enabled="false"  style="text-align: center"
                    Width="160px" ></asp:TextBox>
                </span>
            </td>
            <td colspan="2" style="text-align: left;">
                Nombres<br/>
                <asp:TextBox ID="txtNombres" runat="server" Enabled="false" Style="text-align: center"
                    Width="270px"></asp:TextBox>
            </td>
            <td style="text-align: left;">
                Estado Credito<br/>
                <asp:TextBox ID="TxtEstado" runat="server"  Enabled="false"
                Width="151px" ></asp:TextBox>
            </td>
            <td style="text-align: left;">
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            </td>
        </tr>
        <tr>
            <td style="text-align: left;">
                No Crédito<br />
                <asp:TextBox ID="txtNoCredito" runat="server"  Enabled="False" style="text-align: center"
                    Width="160px"  ></asp:TextBox>
            </td>
            <td style="text-align: left;" colspan="2" >
                Saldo Capital<br />
                <asp:TextBox ID="txtSaldoCapital" runat="server"  Enabled="false" style="text-align: center"
                    Width="160px" ></asp:TextBox>
                <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server" DisplayMoney="Left"
                    Mask="999,999,999" MaskType="Number" TargetControlID="txtSaldoCapital" />
            </td>
            <td style="text-align: left" colspan="2">
                Línea Crédito<br />
                <asp:TextBox ID="txtLinea" runat="server"  Enabled="false" 
                    Width="270px"  ></asp:TextBox>
            </td>
            <td style="text-align: left">
                Teléfono<br />
                <asp:TextBox ID="txttelefono" runat="server"  Enabled="false" style="text-align: center"
                    Width="151px" ></asp:TextBox>
            </td>
            <td style="text-align: left">
                &nbsp;</td>
        </tr>
        <tr>
            <td style="text-align: left;">
                Monto Solicitado<br /> 
                <asp:TextBox ID="txtMontoSolicitado" runat="server"  Enabled="false" style="text-align: center"
                    Width="160px" ></asp:TextBox>
                <asp:MaskedEditExtender ID="MaskedEditExtender4" runat="server" DisplayMoney="Left"
                    Mask="99,999,999" MaskType="Number" TargetControlID="txtMontoSolicitado" />
            </td>
            <td style="text-align: left;" colspan="2">
                # Creditos&nbsp; terminados<br />
                <asp:TextBox ID="txtcreditosterminados" runat="server"  Enabled="false" 
                    Width="160px" ></asp:TextBox>
            </td>
            <td style="text-align: left;" colspan="2">
                Dirección<br />
                <asp:TextBox ID="txtDireccion" runat="server"  Enabled="false"  style="text-align: Left; margin-left: 0px;"
                    Width="270px"  ></asp:TextBox>
            </td>
            <td style="text-align: left;">
                Periodicidad<br />
                <asp:TextBox ID="txtperiodicidad" runat="server"  Enabled="false" style="text-align: center"
                    Width="151px" ></asp:TextBox>
            </td>
            <td style="text-align: left;">
                &nbsp;</td>
        </tr>
        <tr>
            <td style="text-align: left; font-family: Arial, Helvetica, sans-serif; font-size: small;">
                <span style="font-family: Arial, Helvetica, sans-serif">
                    <strong><span style="font-size: small">Monto Aprobado</span><br style="font-size: small" /></strong>
                </span>
                <strong>
                <asp:TextBox ID="txtMontoAprobado" runat="server"  Enabled="false" style="text-align: center; font-family: Arial, Helvetica, sans-serif; font-size: medium; font-weight: bold;"
                    Width="160px" ></asp:TextBox>
                <asp:MaskedEditExtender ID="txtMontoAprobado_MaskedEditExtender" 
                    runat="server" DisplayMoney="Left" Mask="999,999,999" MaskType="Number" 
                    TargetControlID="txtMontoAprobado" />
                </strong>
            </td>
            <td style="text-align: left;">
                <span style="font-family: Arial, Helvetica, sans-serif">
                    <strong><span style="font-size: small">Plazo</span><br style="font-size: small" /> 
                <asp:TextBox ID="txtPlazo" runat="server"  Enabled="false" style="text-align: center; font-family: Arial, Helvetica, sans-serif; font-size: medium; font-weight: bold;"
                    Width="75px" ></asp:TextBox>
                </strong>
                </span>
            </td>
            <td style="text-align: left; width: 88px;">
                <asp:Label ID="Lblcuotaspagas" runat="server" Text="Cuot. Pagas"></asp:Label>
                <strong> 
                <span style="font-family: Arial, Helvetica, sans-serif">
                    <span style="font-size: x-small"><br />
                <asp:TextBox ID="txtCuotasPagas" runat="server"  Enabled="false" style="text-align: center; font-family: Arial, Helvetica, sans-serif; font-size: medium; font-weight: bold;"
                    Width="81px" ></asp:TextBox>
                </span>
                </span>
                </strong>
            </td>
            <td style="text-align: left;">
                <span style="font-family: Arial, Helvetica, sans-serif">
                    <strong><span style="font-size: small">Cuota</span><br style="font-size: small" /></strong>
                </span>
                <strong>
                <asp:TextBox ID="txtCuota" runat="server"  Enabled="false" style="text-align: center; font-family: Arial, Helvetica, sans-serif; font-size: medium; font-weight: bold;"
                    Width="80px" ></asp:TextBox>
                </strong>
                <asp:MaskedEditExtender ID="MaskedEditExtender2" runat="server" DisplayMoney="Left"
                    Mask="99,999,999" MaskType="Number" TargetControlID="txtCuota" />         
            </td>
            <td style="text-align: left;">
                <span style="font-family: Arial, Helvetica, sans-serif">
                    <strong><span style="font-size: small">Tasa Interés</span><br style="font-size: small" /></strong>
                </span>
                <strong>
                <asp:TextBox ID="txtTasaInteres" runat="server"  Enabled="false" style="text-align: center; font-family: Arial, Helvetica, sans-serif; font-size: medium; font-weight: bold;"
                    Width="100px" ></asp:TextBox>                    
                </strong>                    
            </td>
            <td style="text-align: left;">
                <span style="font-family: Arial, Helvetica, sans-serif">
                    <strong><span style="font-size: small">Calificación</span><br style="font-size: small" /></strong>
                </span>
                <strong>
                    <asp:TextBox ID="txtCalifPromedio" runat="server"  Enabled="false"
                        Width="50px" style="margin-bottom: 0px; font-family: Arial, Helvetica, sans-serif; font-size: medium; font-weight: bold;" ></asp:TextBox>         
                </strong>         
            </td>
            <td style="text-align: left;">
                &nbsp;</td>
        </tr>
        <tr>
            <td style="text-align: left;">
                Fecha Desembolso<br />
                <asp:TextBox ID="TxtFechaDesembolso" runat="server" Enabled="False" style="text-align: center"
                    Width="160px"></asp:TextBox>
            </td>
            <td style="text-align: left;" colspan="2">
                Fecha Últ Pago<br />
                <asp:TextBox ID="TxtFechaUltimoPago" runat="server" Enabled="False" style="text-align: center"
                    Width="160px" ></asp:TextBox>                    
            </td>
            <td style="text-align: left">
                Fecha Próx Pago<br />
                <asp:TextBox ID="TxtFechaCuota" runat="server"  style="text-align: center"
                    Enabled="False" Width="151px" ></asp:TextBox>                    
            </td>
            <td colspan="2" style="text-align: center">
                <strong>
                    <asp:TextBox ID="txttipolinea" runat="server"  Enabled="false" Visible="false"
                        Width="50px" style="margin-bottom: 0px; font-family: Arial, Helvetica, sans-serif; font-size: medium; font-weight: bold;" ></asp:TextBox>         
                </strong>         
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
            <td style="text-align: left" colspan="2">
                <asp:Label ID="lblFechaFinal" runat="server" Text="Fecha Final"></asp:Label>
                <br />
                <asp:TextBox ID="TxtFechaFinal" runat="server" Enabled="False" style="text-align: center"
                    Width="150px" ></asp:TextBox>               
                <ucFecha:fecha ID="ucFechaFinal" runat="server" style="text-align: center" />
            </td>                        
            <td style="text-align: left">
            </td>
            <td style="text-align: left">
            </td>
        </tr>
        <tr>
            <td>    
                <asp:ImageButton ID="btnConsultar" runat="server" ImageUrl="~/Images/btnConsultar.jpg" onclick="btnConsular_Click" Visible="False" />
            </td>
            <td colspan="2">    
                <asp:CheckBox ID="cbDetallado" runat="server"  Text="Detallado"
                    oncheckedchanged="cbDetallado_CheckedChanged" AutoPostBack="True" />
            </td>
            <td colspan="2">    
                <asp:Button ID="btnExportarExcel" runat="server" CssClass="btn8" 
                    onclick="btnExportarExcel_Click" Text="Exportar a Excel"  Width="124px" 
                    Height="28px" />                                    
                &nbsp;
                <asp:Button ID="btnImprimir" runat="server" CssClass="btn8" 
                    Text="Visualizar informe"  Width="124px" 
                    Height="28px" onclick="btnImprimir_Click" />                                    
                &nbsp;
            </td>
        </tr>   
    </table>
    <hr style=" width:100%"/>   
    <table style=" width:100%">
        <tr>
            <td> 
                <div style="overflow:scroll;height:500px;width:100%;">
                    <div style="width:100%;">
                        <asp:GridView ID="gvMovGeneral" runat="server" Width="100%" 
                                PageSize="3" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False"
                            OnPageIndexChanging="gvMovGeneral_PageIndexChanging"  
                            SelectedRowStyle-Font-Size="XX-Small" style="font-size:x-small" 
                            onselectedindexchanged="gvMovGeneral_SelectedIndexChanged">                    
                            <Columns>
                                <asp:BoundField DataField="NumeroRadicacion" HeaderText="Número de Radicación" Visible="false" />
                                 <asp:BoundField DataField="idavance" HeaderText="Num. Avance">  <ItemStyle HorizontalAlign="center" /></asp:BoundField>                                     
                                 <asp:BoundField DataField="FechaCuota" HeaderText="Fecha Cuota" ><ItemStyle Width="10%" /></asp:BoundField>
                                 <asp:BoundField DataField="NumCuota" HeaderText="Cuota Paga Avance">  <ItemStyle HorizontalAlign="center" /></asp:BoundField>                                     
                                <asp:BoundField DataField="plazo_avance" HeaderText="Plazo Avance" />
                                <asp:BoundField DataField="FechaPago" HeaderText="Fecha Pago" ></asp:BoundField>
                                <asp:BoundField DataField="DiasMora" HeaderText="Días Mora" />
                                <asp:BoundField DataField="TipoOperacion" HeaderText="No Operac" />
                                <asp:BoundField DataField="num_comp" HeaderText="Num Comprobante"><ItemStyle HorizontalAlign="Right" /></asp:BoundField>
                                <asp:BoundField DataField="TIPO_COMP" HeaderText="Tipo Comprobante"><ItemStyle HorizontalAlign="Right" /></asp:BoundField>
                                <asp:BoundField DataField="Transaccion" HeaderText="Transacción" />
                                <asp:BoundField DataField="TipoMovimiento" HeaderText="Tipo  Mov" />
                                <asp:BoundField DataField="Capital" HeaderText="Capital" DataFormatString="{0:c}" ItemStyle-Width="90">                    
                                <ItemStyle Wrap="True" Width="90" /></asp:BoundField>
                                <asp:BoundField DataField="IntCte" HeaderText="Int Corriente" DataFormatString="{0:c}"><ItemStyle HorizontalAlign="Right" Width="100"/></asp:BoundField>
                                <asp:BoundField DataField="IntMora" HeaderText="Int Mora" DataFormatString="{0:c}"><ItemStyle HorizontalAlign="Right" Width="100" /></asp:BoundField>
                                <asp:BoundField DataField="Poliza" HeaderText="Poliza" DataFormatString="{0:c}"><ItemStyle HorizontalAlign="Right" /></asp:BoundField>
                                <asp:BoundField DataField="Seguros" HeaderText="Seguros" DataFormatString="{0:c}"><ItemStyle HorizontalAlign="Right" /></asp:BoundField>
                                <asp:BoundField DataField="LeyMiPyme" HeaderText="Ley MiPyme" DataFormatString="{0:c}"><ItemStyle HorizontalAlign="Right" /></asp:BoundField>
                                <asp:BoundField DataField="ivaMiPyme" HeaderText="Iva MiPyme" DataFormatString="{0:c}"><ItemStyle HorizontalAlign="Right" /></asp:BoundField>
                                <asp:BoundField DataField="otros" HeaderText="Otros" DataFormatString="{0:c}"><ItemStyle HorizontalAlign="Right" Width="100"/></asp:BoundField>
                                <asp:BoundField DataField="Prejuridico" HeaderText="Pre-Juridico" />
                                <asp:BoundField DataFormatString="{0:c}" HeaderText="Total Pagado" DataField="totalpago"><ItemStyle HorizontalAlign="Right" Width="100" /></asp:BoundField>
                                <asp:BoundField DataField="Saldo" HeaderText="Saldo" DataFormatString="{0:c}">
                                 <ItemStyle HorizontalAlign="center" Width="100"/></asp:BoundField>
                                <asp:BoundField DataField="Calificacion" HeaderText="Calificación" Visible="False">  <ItemStyle HorizontalAlign="center" /></asp:BoundField>                                     
                                 <asp:BoundField DataField="desc_tran" HeaderText="Tipo_Tran" DataFormatString="{0:c}"/>
                            </Columns>
                            <HeaderStyle CssClass="gridHeader" />
                            <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                            <RowStyle CssClass="gridItem" />
                            <SelectedRowStyle Font-Size="XX-Small"></SelectedRowStyle>
                        </asp:GridView>
                        <br />
                        <rsweb:ReportViewer ID="Rpview1" runat="server" Width="100%" 
                            Font-Names="Verdana" Font-Size="8pt" InteractiveDeviceInfos="(Colección)" 
                            WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt">
                            <LocalReport ReportPath="Page\Asesores\EstadoCuenta\Creditos\MovimientoGeneral\ReportMovimiento.rdlc">
                            </LocalReport>
                        </rsweb:ReportViewer>
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
</asp:Content>
